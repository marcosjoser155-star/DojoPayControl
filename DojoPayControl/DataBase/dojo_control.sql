/* ============================================================
   BASE DE DATOS: dojo_control
   PROYECTO: Sistema de control de mensualidades para dojo

   DESCRIPCIÓN:
   Esta base de datos trabaja con el proyecto DojoPayControl
   desarrollado en Windows Forms con C#.

   Esta versión está ajustada para funcionar con las clases:

   - ConexionDB
   - Usuario
   - Estudiante
   - Dashboard
   - Pago
   - Mensualidad
   - Anualidad
   - AusenciaTemporal

   IMPORTANTE:
   Este script NO usa DROP DATABASE.
   Por lo tanto, NO borra la base de datos completa.
   Está pensado para crear la estructura y permitir seguir
   usando la información a futuro.

   También se cambió el concepto anterior de PausaEstudiante
   por AusenciaTemporal.
   ============================================================ */


/* ============================================================
   1. CONFIGURACIÓN DE CARACTERES
   ------------------------------------------------------------
   Permite guardar correctamente acentos, ñ y caracteres especiales.
   ============================================================ */

SET NAMES utf8mb4;


/* ============================================================
   2. CREAR BASE DE DATOS
   ------------------------------------------------------------
   IF NOT EXISTS evita borrar o reemplazar una base ya existente.
   ============================================================ */

CREATE DATABASE IF NOT EXISTS dojo_control
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci;


/* ============================================================
   3. SELECCIONAR BASE DE DATOS
   ------------------------------------------------------------
   A partir de aquí, todas las tablas se crean dentro de dojo_control.
   ============================================================ */

USE dojo_control;


/* ============================================================
   4. TABLA: Usuario
   ------------------------------------------------------------
   Guarda los usuarios que pueden iniciar sesión en el sistema.

   idUsuario:
   Identificador único del usuario.

   usuario:
   Nombre usado para iniciar sesión.

   contrasena:
   Contraseña del usuario.

   rol:
   Tipo de usuario dentro del sistema.
   Ejemplo: Administrador, Instructor, Recepcionista.
   ============================================================ */

CREATE TABLE IF NOT EXISTS Usuario (
    idUsuario INT AUTO_INCREMENT PRIMARY KEY,
    usuario VARCHAR(50) NOT NULL UNIQUE,
    contrasena VARCHAR(255) NOT NULL,
    rol VARCHAR(30) NOT NULL
);


/* ============================================================
   5. USUARIO INICIAL DEL SISTEMA
   ------------------------------------------------------------
   Este usuario permite probar el login inicial del sistema.

   INSERT IGNORE evita duplicarlo si ya existe.
   ============================================================ */

INSERT IGNORE INTO Usuario (usuario, contrasena, rol)
VALUES ('instructor01', 'ArthurSar27*', 'Administrador');


/* ============================================================
   6. TABLA: Estudiante
   ------------------------------------------------------------
   Guarda la información principal de los estudiantes.

   estado:
   Representa la situación actual del estudiante.

   Estados usados por el sistema:
   - Al día
   - Pendiente
   - Restringido
   - Pausado
   - Revisar anualidad

   activo:
   Permite hacer eliminación lógica.
   1 = estudiante activo.
   0 = estudiante eliminado/inactivo.

   Esto evita borrar definitivamente al estudiante y permite
   conservar su historial.
   ============================================================ */

CREATE TABLE IF NOT EXISTS Estudiante (
    idEstudiante INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    cedula VARCHAR(30),
    telefono VARCHAR(20),
    fechaIngreso DATE NOT NULL,
    estado VARCHAR(30) NOT NULL DEFAULT 'Pendiente',
    activo TINYINT NOT NULL DEFAULT 1
);


/* ============================================================
   7. TABLA: Mensualidad
   ------------------------------------------------------------
   Controla el estado de la mensualidad de cada estudiante.

   mesCorrespondiente:
   Mes de la mensualidad.
   1 = enero, 2 = febrero, etc.

   anioCorrespondiente:
   Año de la mensualidad.

   monto:
   Cantidad a pagar.

   fechaLimite:
   Fecha límite para pagar.

   estado:
   Puede ser Pendiente, Pagada o Vencida.

   UNIQUE:
   Evita que un mismo estudiante tenga dos mensualidades
   para el mismo mes y año.
   ============================================================ */

CREATE TABLE IF NOT EXISTS Mensualidad (
    idMensualidad INT AUTO_INCREMENT PRIMARY KEY,
    idEstudiante INT NOT NULL,
    mesCorrespondiente INT NOT NULL,
    anioCorrespondiente INT NOT NULL,
    monto DECIMAL(10,2) NOT NULL,
    fechaLimite DATE NOT NULL,
    estado VARCHAR(30) NOT NULL DEFAULT 'Pendiente',

    CONSTRAINT fk_mensualidad_estudiante
    FOREIGN KEY (idEstudiante) REFERENCES Estudiante(idEstudiante)
    ON UPDATE CASCADE
    ON DELETE CASCADE,

    CONSTRAINT uq_mensualidad_estudiante_mes_anio
    UNIQUE (idEstudiante, mesCorrespondiente, anioCorrespondiente)
);


/* ============================================================
   8. TABLA: Anualidad
   ------------------------------------------------------------
   Controla el pago anual de cada estudiante.

   anioCorrespondiente:
   Año al que pertenece la anualidad.

   monto:
   Cantidad a pagar.

   fechaLimite:
   Fecha máxima para pagar la anualidad.

   estado:
   Puede ser Pendiente, Pagada o Vencida.

   UNIQUE:
   Evita duplicar la anualidad del mismo estudiante en el mismo año.
   ============================================================ */

CREATE TABLE IF NOT EXISTS Anualidad (
    idAnualidad INT AUTO_INCREMENT PRIMARY KEY,
    idEstudiante INT NOT NULL,
    anioCorrespondiente INT NOT NULL,
    monto DECIMAL(10,2) NOT NULL,
    fechaLimite DATE NOT NULL,
    estado VARCHAR(30) NOT NULL DEFAULT 'Pendiente',

    CONSTRAINT fk_anualidad_estudiante
    FOREIGN KEY (idEstudiante) REFERENCES Estudiante(idEstudiante)
    ON UPDATE CASCADE
    ON DELETE CASCADE,

    CONSTRAINT uq_anualidad_estudiante_anio
    UNIQUE (idEstudiante, anioCorrespondiente)
);


/* ============================================================
   9. TABLA: Pago
   ------------------------------------------------------------
   Guarda el historial de pagos realizados.

   Esta tabla está adaptada para el Form de Pagos.

   numeroRecibo:
   Número o código del recibo.

   idEstudiante:
   Estudiante que realizó el pago.

   monto:
   Cantidad pagada.

   fechaPago:
   Fecha del pago.

   metodoPago:
   Ejemplo: Efectivo, Yappy, Transferencia.

   tipoPago:
   Mensualidad o Anualidad.

   mesCorrespondiente:
   Solo se usa cuando el pago es de mensualidad.
   Para anualidad puede quedar NULL.

   anioCorrespondiente:
   Año del pago.

   observaciones:
   Comentarios adicionales.
   ============================================================ */

CREATE TABLE IF NOT EXISTS Pago (
    idPago INT AUTO_INCREMENT PRIMARY KEY,
    numeroRecibo VARCHAR(50),
    idEstudiante INT NOT NULL,
    monto DECIMAL(10,2) NOT NULL,
    fechaPago DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    metodoPago VARCHAR(50) NOT NULL,
    tipoPago VARCHAR(30) NOT NULL,
    mesCorrespondiente INT NULL,
    anioCorrespondiente INT NOT NULL,
    observaciones VARCHAR(255),

    CONSTRAINT fk_pago_estudiante
    FOREIGN KEY (idEstudiante) REFERENCES Estudiante(idEstudiante)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);


/* ============================================================
   10. TABLA: AusenciaTemporal
   ------------------------------------------------------------
   Reemplaza la tabla anterior llamada PausaEstudiante.

   Se usa cuando un estudiante no asistirá temporalmente.

   Mientras el estudiante está en ausencia temporal:
   - Su estado visual puede aparecer como Pausado.
   - No debe contarse como deudor activo.
   - No debe pasar automáticamente a Restringido.

   estadoAusencia:
   Puede ser Activa o Finalizada.
   ============================================================ */

CREATE TABLE IF NOT EXISTS AusenciaTemporal (
    idAusencia INT AUTO_INCREMENT PRIMARY KEY,
    idEstudiante INT NOT NULL,
    fechaInicio DATE NOT NULL,
    fechaFin DATE NOT NULL,
    motivo VARCHAR(100) NOT NULL,
    observacion VARCHAR(255),
    estadoAusencia VARCHAR(30) NOT NULL DEFAULT 'Activa',

    CONSTRAINT fk_ausencia_estudiante
    FOREIGN KEY (idEstudiante) REFERENCES Estudiante(idEstudiante)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);


/* ============================================================
   11. PROCEDIMIENTO AUXILIAR: AgregarColumnaSiNoExiste
   ------------------------------------------------------------
   Sirve para modificar bases anteriores sin borrar datos.

   Si una columna ya existe, no hace nada.
   Si una columna falta, la agrega.

   Esto ayuda si ya habías ejecutado una versión anterior
   de la base de datos.
   ============================================================ */

DROP PROCEDURE IF EXISTS AgregarColumnaSiNoExiste;

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
            'ALTER TABLE ', nombreTabla,
            ' ADD COLUMN ', definicionColumna
        );

        PREPARE stmt FROM @sql;
        EXECUTE stmt;
        DEALLOCATE PREPARE stmt;
    END IF;
END$$

DELIMITER ;


/* ============================================================
   12. AJUSTES PARA TABLAS YA EXISTENTES
   ------------------------------------------------------------
   Estas líneas permiten que una base anterior quede compatible
   con las clases actuales.

   No borran información.
   Solo agregan columnas si faltan.
   ============================================================ */

CALL AgregarColumnaSiNoExiste('Estudiante', 'activo', 'activo TINYINT NOT NULL DEFAULT 1');

CALL AgregarColumnaSiNoExiste('Pago', 'numeroRecibo', 'numeroRecibo VARCHAR(50) NULL');
CALL AgregarColumnaSiNoExiste('Pago', 'mesCorrespondiente', 'mesCorrespondiente INT NULL');
CALL AgregarColumnaSiNoExiste('Pago', 'anioCorrespondiente', 'anioCorrespondiente INT NOT NULL DEFAULT 2026');
CALL AgregarColumnaSiNoExiste('Pago', 'observaciones', 'observaciones VARCHAR(255) NULL');

CALL AgregarColumnaSiNoExiste('AusenciaTemporal', 'observacion', 'observacion VARCHAR(255) NULL');
CALL AgregarColumnaSiNoExiste('AusenciaTemporal', 'estadoAusencia', 'estadoAusencia VARCHAR(30) NOT NULL DEFAULT ''Activa''');


/* ============================================================
   13. ELIMINAR PROCEDIMIENTO AUXILIAR
   ------------------------------------------------------------
   Ya no se necesita después de agregar las columnas faltantes.
   ============================================================ */

DROP PROCEDURE IF EXISTS AgregarColumnaSiNoExiste;


/* ============================================================
   14. VISTA: VistaDashboard
   ------------------------------------------------------------
   Esta vista prepara información para el dashboard.

   Una vista NO guarda datos.
   Solo muestra datos combinados de varias tablas.

   Muestra:
   - estudiante
   - estado general
   - estado de mensualidad actual
   - estado de anualidad actual
   ============================================================ */

CREATE OR REPLACE VIEW VistaDashboard AS
SELECT 
    e.idEstudiante,
    e.nombre AS estudiante,
    e.cedula,
    e.telefono,
    e.fechaIngreso,
    e.estado AS estadoEstudiante,
    e.activo,

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
   15. VISTA: VistaPagos
   ------------------------------------------------------------
   Prepara la información para el Form de Pagos.

   Permite mostrar:
   - nombre del estudiante
   - tipo de pago
   - mes
   - año
   - monto
   - método
   - recibo
   - fecha
   ============================================================ */

CREATE OR REPLACE VIEW VistaPagos AS
SELECT
    p.idPago,
    e.idEstudiante,
    e.nombre AS estudiante,
    p.tipoPago,
    p.mesCorrespondiente,
    p.anioCorrespondiente,
    p.monto,
    p.metodoPago,
    p.numeroRecibo,
    p.fechaPago,
    p.observaciones
FROM Pago p
INNER JOIN Estudiante e
    ON p.idEstudiante = e.idEstudiante
WHERE e.activo = 1;


/* ============================================================
   16. VISTA: VistaAusenciasTemporales
   ------------------------------------------------------------
   Prepara el historial de ausencias temporales.

   Sirve para ver:
   - estudiante
   - fecha de inicio
   - fecha de fin
   - motivo
   - observación
   - estado de la ausencia
   ============================================================ */

CREATE OR REPLACE VIEW VistaAusenciasTemporales AS
SELECT
    a.idAusencia,
    e.idEstudiante,
    e.nombre AS estudiante,
    a.fechaInicio,
    a.fechaFin,
    a.motivo,
    a.observacion,
    a.estadoAusencia
FROM AusenciaTemporal a
INNER JOIN Estudiante e
    ON a.idEstudiante = e.idEstudiante;


/* ============================================================
   17. VERIFICACIÓN FINAL
   ------------------------------------------------------------
   Muestra las tablas y vistas creadas.
   ============================================================ */

SHOW TABLES;