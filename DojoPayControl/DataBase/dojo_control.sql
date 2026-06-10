/* ============================================================
   BASE DE DATOS: dojo_control
   PROYECTO: Sistema de control de mensualidades de dojo

   Este script crea y ajusta todo lo necesario para:
   - FrmLogin
   - FrmDashboard
   - FrmNuevoEstudiante
   - FrmRegistrarPago
   - FrmAusenciaTemporal
   - FrmReactivarEstudiante

   No borra la base de datos.
   No borra las tablas.
   No borra información guardada.
   ============================================================ */

SET NAMES utf8mb4;

CREATE DATABASE IF NOT EXISTS dojo_control
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci;

USE dojo_control;

/* ============================================================
   TABLA: Usuario
   Guarda los usuarios que pueden iniciar sesión.
   ============================================================ */

CREATE TABLE IF NOT EXISTS Usuario (
    idUsuario INT AUTO_INCREMENT PRIMARY KEY,
    usuario VARCHAR(50) NOT NULL,
    contrasena VARCHAR(255) NOT NULL,
    rol VARCHAR(30) NOT NULL
);
/* ============================================================
   USUARIO INICIAL PARA PROBAR EL LOGIN
   ============================================================ */

INSERT INTO Usuario (usuario, contrasena, rol)
VALUES ('instructor01', '1234', 'Administrador')
ON DUPLICATE KEY UPDATE
    contrasena = '1234',
    rol = 'Administrador';
/* ============================================================
   TABLA: Estudiante
   Guarda los datos principales de cada estudiante.
   ============================================================ */

CREATE TABLE IF NOT EXISTS Estudiante (
    idEstudiante INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    apellido VARCHAR(100) NOT NULL,
    cedula VARCHAR(30) NOT NULL,
    telefono VARCHAR(20) NOT NULL,
    fechaIngreso DATE NOT NULL,
    estado VARCHAR(30) NOT NULL DEFAULT 'Pendiente',
    activo TINYINT NOT NULL DEFAULT 1,
    montoMensualidad DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    montoAnualidad DECIMAL(10,2) NOT NULL DEFAULT 0.00
);

/* ============================================================
   TABLA: Mensualidad
   Guarda el estado de mensualidad por estudiante, mes y año.
   ============================================================ */

CREATE TABLE IF NOT EXISTS Mensualidad (
    idMensualidad INT AUTO_INCREMENT PRIMARY KEY,
    idEstudiante INT NOT NULL,
    mesCorrespondiente INT NOT NULL,
    anioCorrespondiente INT NOT NULL,
    monto DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    fechaLimite DATE NOT NULL,
    estado VARCHAR(30) NOT NULL DEFAULT 'Pendiente',

    CONSTRAINT fk_mensualidad_estudiante
    FOREIGN KEY (idEstudiante) REFERENCES Estudiante(idEstudiante)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

/* ============================================================
   TABLA: Anualidad
   Guarda el estado de anualidad por estudiante y año.
   ============================================================ */

CREATE TABLE IF NOT EXISTS Anualidad (
    idAnualidad INT AUTO_INCREMENT PRIMARY KEY,
    idEstudiante INT NOT NULL,
    anioCorrespondiente INT NOT NULL,
    monto DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    fechaLimite DATE NOT NULL,
    estado VARCHAR(30) NOT NULL DEFAULT 'Pendiente',

    CONSTRAINT fk_anualidad_estudiante
    FOREIGN KEY (idEstudiante) REFERENCES Estudiante(idEstudiante)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

/* ============================================================
   TABLA: Pago
   Guarda el historial de pagos realizados.
   ============================================================ */

CREATE TABLE IF NOT EXISTS Pago (
    idPago INT AUTO_INCREMENT PRIMARY KEY,
    numeroRecibo VARCHAR(50) NULL,
    idEstudiante INT NOT NULL,
    idMensualidad INT NULL,
    idAnualidad INT NULL,
    monto DECIMAL(10,2) NOT NULL,
    fechaPago DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    metodoPago VARCHAR(50) NOT NULL,
    tipoPago VARCHAR(30) NOT NULL,
    mesCorrespondiente INT NULL,
    anioCorrespondiente INT NOT NULL,
    observaciones VARCHAR(255) NULL,

    CONSTRAINT fk_pago_estudiante
    FOREIGN KEY (idEstudiante) REFERENCES Estudiante(idEstudiante)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

/* ============================================================
   TABLA: AusenciaTemporal
   Guarda las ausencias temporales de estudiantes.
   ============================================================ */

CREATE TABLE IF NOT EXISTS AusenciaTemporal (
    idAusencia INT AUTO_INCREMENT PRIMARY KEY,
    idEstudiante INT NOT NULL,
    fechaInicio DATE NOT NULL,
    fechaFin DATE NOT NULL,
    motivo VARCHAR(100) NOT NULL,
    observacion VARCHAR(255) NULL,
    estadoAusencia VARCHAR(30) NOT NULL DEFAULT 'Activa',

    CONSTRAINT fk_ausencia_estudiante
    FOREIGN KEY (idEstudiante) REFERENCES Estudiante(idEstudiante)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

/* ============================================================
   PROCEDIMIENTOS TEMPORALES PARA AJUSTAR TABLAS ANTIGUAS
   Si alguna columna o índice falta, lo agrega.
   ============================================================ */

DROP PROCEDURE IF EXISTS AgregarColumnaSiNoExiste;
DROP PROCEDURE IF EXISTS AgregarIndiceUnicoSiNoExiste;

DELIMITER $$

CREATE PROCEDURE AgregarColumnaSiNoExiste(
    IN nombreTabla VARCHAR(64),
    IN nombreColumna VARCHAR(64),
    IN definicionColumna VARCHAR(255)
)
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE TABLE_SCHEMA = DATABASE()
        AND TABLE_NAME = nombreTabla
        AND COLUMN_NAME = nombreColumna
    ) THEN
        SET @sql = CONCAT(
            'ALTER TABLE `', nombreTabla,
            '` ADD COLUMN ', definicionColumna
        );

        PREPARE stmt FROM @sql;
        EXECUTE stmt;
        DEALLOCATE PREPARE stmt;
    END IF;
END$$

CREATE PROCEDURE AgregarIndiceUnicoSiNoExiste(
    IN nombreTabla VARCHAR(64),
    IN nombreIndice VARCHAR(64),
    IN columnas VARCHAR(255)
)
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM INFORMATION_SCHEMA.STATISTICS
        WHERE TABLE_SCHEMA = DATABASE()
        AND TABLE_NAME = nombreTabla
        AND INDEX_NAME = nombreIndice
    ) THEN
        SET @sql = CONCAT(
            'ALTER TABLE `', nombreTabla,
            '` ADD CONSTRAINT `', nombreIndice,
            '` UNIQUE (', columnas, ')'
        );

        PREPARE stmt FROM @sql;
        EXECUTE stmt;
        DEALLOCATE PREPARE stmt;
    END IF;
END$$

DELIMITER ;

/* ============================================================
   AJUSTES DE COLUMNAS NECESARIAS
   ============================================================ */

CALL AgregarColumnaSiNoExiste('Estudiante', 'apellido', 'apellido VARCHAR(100) NOT NULL DEFAULT '''' AFTER nombre');
CALL AgregarColumnaSiNoExiste('Estudiante', 'activo', 'activo TINYINT NOT NULL DEFAULT 1');
CALL AgregarColumnaSiNoExiste('Estudiante', 'montoMensualidad', 'montoMensualidad DECIMAL(10,2) NOT NULL DEFAULT 0.00');
CALL AgregarColumnaSiNoExiste('Estudiante', 'montoAnualidad', 'montoAnualidad DECIMAL(10,2) NOT NULL DEFAULT 0.00');

CALL AgregarColumnaSiNoExiste('Pago', 'numeroRecibo', 'numeroRecibo VARCHAR(50) NULL');
CALL AgregarColumnaSiNoExiste('Pago', 'idMensualidad', 'idMensualidad INT NULL');
CALL AgregarColumnaSiNoExiste('Pago', 'idAnualidad', 'idAnualidad INT NULL');
CALL AgregarColumnaSiNoExiste('Pago', 'mesCorrespondiente', 'mesCorrespondiente INT NULL');
CALL AgregarColumnaSiNoExiste('Pago', 'anioCorrespondiente', 'anioCorrespondiente INT NOT NULL DEFAULT 2026');
CALL AgregarColumnaSiNoExiste('Pago', 'observaciones', 'observaciones VARCHAR(255) NULL');

CALL AgregarColumnaSiNoExiste('AusenciaTemporal', 'observacion', 'observacion VARCHAR(255) NULL');
CALL AgregarColumnaSiNoExiste('AusenciaTemporal', 'estadoAusencia', 'estadoAusencia VARCHAR(30) NOT NULL DEFAULT ''Activa''');

/* ============================================================
   AJUSTES DE DATOS NULOS ANTES DE FORZAR NOT NULL
   ============================================================ */

UPDATE Estudiante SET apellido = '' WHERE apellido IS NULL;
UPDATE Estudiante SET activo = 1 WHERE activo IS NULL;
UPDATE Estudiante SET montoMensualidad = 0.00 WHERE montoMensualidad IS NULL;
UPDATE Estudiante SET montoAnualidad = 0.00 WHERE montoAnualidad IS NULL;
UPDATE Estudiante SET estado = 'Pendiente' WHERE estado IS NULL OR estado = '';

UPDATE Pago SET anioCorrespondiente = YEAR(CURDATE()) WHERE anioCorrespondiente IS NULL;

UPDATE AusenciaTemporal SET estadoAusencia = 'Activa'
WHERE estadoAusencia IS NULL OR estadoAusencia = '';

/* ============================================================
   AJUSTES DE TIPOS DE COLUMNAS
   ============================================================ */

ALTER TABLE Estudiante MODIFY COLUMN estado VARCHAR(30) NOT NULL DEFAULT 'Pendiente';
ALTER TABLE Estudiante MODIFY COLUMN activo TINYINT NOT NULL DEFAULT 1;
ALTER TABLE Estudiante MODIFY COLUMN montoMensualidad DECIMAL(10,2) NOT NULL DEFAULT 0.00;
ALTER TABLE Estudiante MODIFY COLUMN montoAnualidad DECIMAL(10,2) NOT NULL DEFAULT 0.00;

ALTER TABLE Pago MODIFY COLUMN tipoPago VARCHAR(30) NOT NULL;
ALTER TABLE Pago MODIFY COLUMN metodoPago VARCHAR(50) NOT NULL;
ALTER TABLE Pago MODIFY COLUMN anioCorrespondiente INT NOT NULL;

ALTER TABLE AusenciaTemporal MODIFY COLUMN estadoAusencia VARCHAR(30) NOT NULL DEFAULT 'Activa';

/* ============================================================
   ÍNDICES ÚNICOS NECESARIOS
   Evitan duplicar mensualidades y anualidades.
   ============================================================ */

CALL AgregarIndiceUnicoSiNoExiste(
    'Usuario',
    'uq_usuario_usuario',
    '`usuario`'
);

CALL AgregarIndiceUnicoSiNoExiste(
    'Mensualidad',
    'uq_mensualidad_estudiante_mes_anio',
    '`idEstudiante`, `mesCorrespondiente`, `anioCorrespondiente`'
);

CALL AgregarIndiceUnicoSiNoExiste(
    'Anualidad',
    'uq_anualidad_estudiante_anio',
    '`idEstudiante`, `anioCorrespondiente`'
);

/* ============================================================
   USUARIO INICIAL PARA PROBAR LOGIN
   ============================================================ */

INSERT INTO Usuario (usuario, contrasena, rol)
VALUES ('instructor01', '1234', 'Administrador')
ON DUPLICATE KEY UPDATE
contrasena = '1234',
rol = 'Administrador';

/* ============================================================
   VISTA: VistaDashboard
   Muestra información principal del dashboard.
   ============================================================ */

CREATE OR REPLACE VIEW VistaDashboard AS
SELECT 
    e.idEstudiante,
    CONCAT(e.nombre, ' ', e.apellido) AS estudiante,
    e.nombre,
    e.apellido,
    e.cedula,
    e.telefono,
    e.fechaIngreso,
    e.estado AS estadoEstudiante,
    e.activo,
    e.montoMensualidad,
    e.montoAnualidad,
    IFNULL(m.estado, 'Pendiente') AS estadoMensualidad,
    IFNULL(a.estado, 'Pendiente') AS estadoAnualidad,
    m.mesCorrespondiente,
    m.anioCorrespondiente AS anioMensualidad,
    a.anioCorrespondiente AS anioAnualidad
FROM Estudiante e
LEFT JOIN Mensualidad m
    ON e.idEstudiante = m.idEstudiante
    AND m.mesCorrespondiente = MONTH(CURDATE())
    AND m.anioCorrespondiente = YEAR(CURDATE())
LEFT JOIN Anualidad a
    ON e.idEstudiante = a.idEstudiante
    AND a.anioCorrespondiente = YEAR(CURDATE())
WHERE e.activo = 1;

/* ============================================================
   VISTA: VistaPagos
   Muestra historial de pagos.
   ============================================================ */

CREATE OR REPLACE VIEW VistaPagos AS
SELECT
    p.idPago,
    p.numeroRecibo,
    e.idEstudiante,
    CONCAT(e.nombre, ' ', e.apellido) AS estudiante,
    p.tipoPago,
    p.mesCorrespondiente,
    p.anioCorrespondiente,
    p.monto,
    p.metodoPago,
    p.fechaPago,
    p.observaciones
FROM Pago p
INNER JOIN Estudiante e
    ON p.idEstudiante = e.idEstudiante
WHERE e.activo = 1;

/* ============================================================
   VISTA: VistaAusenciasTemporales
   Muestra ausencias temporales.
   ============================================================ */

CREATE OR REPLACE VIEW VistaAusenciasTemporales AS
SELECT
    a.idAusencia,
    e.idEstudiante,
    CONCAT(e.nombre, ' ', e.apellido) AS estudiante,
    a.fechaInicio,
    a.fechaFin,
    a.motivo,
    a.observacion,
    a.estadoAusencia
FROM AusenciaTemporal a
INNER JOIN Estudiante e
    ON a.idEstudiante = e.idEstudiante
WHERE e.activo = 1;

/* ============================================================
   PROCEDIMIENTO: ActualizarEstadosEstudiantes
   Actualiza estados según mensualidad, anualidad y ausencia.
   ============================================================ */

DROP PROCEDURE IF EXISTS ActualizarEstadosEstudiantes;

DELIMITER $$

CREATE PROCEDURE ActualizarEstadosEstudiantes()
BEGIN
    UPDATE Estudiante e
    SET e.estado =
        CASE
            WHEN e.estado = 'Ausencia Temporal' THEN 'Ausencia Temporal'

            WHEN NOT EXISTS (
                SELECT 1
                FROM Mensualidad m
                WHERE m.idEstudiante = e.idEstudiante
                AND m.mesCorrespondiente = MONTH(CURDATE())
                AND m.anioCorrespondiente = YEAR(CURDATE())
                AND m.estado = 'Pagada'
            )
            AND DAY(CURDATE()) > 7 THEN 'Restringido'

            WHEN NOT EXISTS (
                SELECT 1
                FROM Mensualidad m
                WHERE m.idEstudiante = e.idEstudiante
                AND m.mesCorrespondiente = MONTH(CURDATE())
                AND m.anioCorrespondiente = YEAR(CURDATE())
                AND m.estado = 'Pagada'
            ) THEN 'Pendiente'

            WHEN NOT EXISTS (
                SELECT 1
                FROM Anualidad a
                WHERE a.idEstudiante = e.idEstudiante
                AND a.anioCorrespondiente = YEAR(CURDATE())
                AND a.estado = 'Pagada'
            ) THEN 'Revisar anualidad'

            ELSE 'Al día'
        END
    WHERE e.activo = 1;
END$$

DELIMITER ;

/* ============================================================
   TRIGGER: Cuando se registra un pago
   Crea o actualiza mensualidad/anualidad como pagada.
   ============================================================ */

DROP TRIGGER IF EXISTS trg_pago_insertar_mensualidad_anualidad;

DELIMITER $$

CREATE TRIGGER trg_pago_insertar_mensualidad_anualidad
AFTER INSERT ON Pago
FOR EACH ROW
BEGIN
    IF NEW.tipoPago = 'Mensualidad' AND NEW.mesCorrespondiente IS NOT NULL THEN

        INSERT INTO Mensualidad
        (
            idEstudiante,
            mesCorrespondiente,
            anioCorrespondiente,
            monto,
            fechaLimite,
            estado
        )
        VALUES
        (
            NEW.idEstudiante,
            NEW.mesCorrespondiente,
            NEW.anioCorrespondiente,
            NEW.monto,
            STR_TO_DATE(
                CONCAT(NEW.anioCorrespondiente, '-', LPAD(NEW.mesCorrespondiente, 2, '0'), '-07'),
                '%Y-%m-%d'
            ),
            'Pagada'
        )
        ON DUPLICATE KEY UPDATE
            monto = NEW.monto,
            fechaLimite = STR_TO_DATE(
                CONCAT(NEW.anioCorrespondiente, '-', LPAD(NEW.mesCorrespondiente, 2, '0'), '-07'),
                '%Y-%m-%d'
            ),
            estado = 'Pagada';

    END IF;

    IF NEW.tipoPago = 'Anualidad' THEN

        INSERT INTO Anualidad
        (
            idEstudiante,
            anioCorrespondiente,
            monto,
            fechaLimite,
            estado
        )
        VALUES
        (
            NEW.idEstudiante,
            NEW.anioCorrespondiente,
            NEW.monto,
            STR_TO_DATE(CONCAT(NEW.anioCorrespondiente, '-01-07'), '%Y-%m-%d'),
            'Pagada'
        )
        ON DUPLICATE KEY UPDATE
            monto = NEW.monto,
            fechaLimite = STR_TO_DATE(CONCAT(NEW.anioCorrespondiente, '-01-07'), '%Y-%m-%d'),
            estado = 'Pagada';

    END IF;

    CALL ActualizarEstadosEstudiantes();
END$$

DELIMITER ;

/* ============================================================
   TRIGGER: Cuando se registra ausencia temporal
   Cambia el estado del estudiante a Ausencia Temporal.
   ============================================================ */

DROP TRIGGER IF EXISTS trg_ausencia_insertar_estado;

DELIMITER $$

CREATE TRIGGER trg_ausencia_insertar_estado
AFTER INSERT ON AusenciaTemporal
FOR EACH ROW
BEGIN
    IF NEW.estadoAusencia = 'Activa' THEN
        UPDATE Estudiante
        SET estado = 'Ausencia Temporal'
        WHERE idEstudiante = NEW.idEstudiante;
    END IF;
END$$

DELIMITER ;

/* ============================================================
   TRIGGER: Cuando se finaliza ausencia temporal
   Reactiva al estudiante en estado Pendiente.
   ============================================================ */

DROP TRIGGER IF EXISTS trg_ausencia_actualizar_estado;

DELIMITER $$

CREATE TRIGGER trg_ausencia_actualizar_estado
AFTER UPDATE ON AusenciaTemporal
FOR EACH ROW
BEGIN
    IF NEW.estadoAusencia = 'Finalizada' THEN
        UPDATE Estudiante
        SET estado = 'Pendiente'
        WHERE idEstudiante = NEW.idEstudiante;
    END IF;
END$$

DELIMITER ;

/* ============================================================
   LIMPIEZA DE PROCEDIMIENTOS TEMPORALES
   No borra datos.
   ============================================================ */

DROP PROCEDURE IF EXISTS AgregarColumnaSiNoExiste;
DROP PROCEDURE IF EXISTS AgregarIndiceUnicoSiNoExiste;

/* ============================================================
   ACTUALIZACIÓN INICIAL DE ESTADOS
   ============================================================ */

CALL ActualizarEstadosEstudiantes();

/* ============================================================
   VERIFICACIÓN FINAL
   ============================================================ */

SHOW TABLES;

SELECT * FROM Usuario;

