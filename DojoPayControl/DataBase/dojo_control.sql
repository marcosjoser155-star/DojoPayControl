/* ============================================================
Hey esta es la dase de datos tienes que instalar MySQL 
y ejecutar este script para crear la base de datos y 
las tablas necesarias para el proyecto.

tambien tienen que agregar un usuario con permisos para esa 
base de datos y configurar la conexion en el proyecto de Visual Studio.
y por ultimo pueden usar el script para hacer consultas a la 
base de datos desde Visual Studio.
si no le entienden algo se lo mandan a gpt y que 
les explique o busquen en yt xd.
   ============================================================ */

/* ============================================================
   BASE DE DATOS: dojo_control
   PROYECTO: Sistema de control de mensualidades para dojo

   Este script crea la estructura principal de la base de datos.
   No borra información existente, porque está pensado para usarse
   a futuro desde Visual Studio.
   ============================================================ */
 
CREATE DATABASE IF NOT EXISTS dojo_control
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci;

USE dojo_control;

/* =========================
   TABLA: Usuario
   Guarda los usuarios del sistema.
   ========================= */

CREATE TABLE IF NOT EXISTS Usuario (
    idUsuario INT AUTO_INCREMENT PRIMARY KEY,
    usuario VARCHAR(50) NOT NULL UNIQUE,
    contrasena VARCHAR(255) NOT NULL,
    rol VARCHAR(30) NOT NULL
);

/* =========================
   TABLA: Estudiante
   Guarda los datos principales de cada estudiante.
   ========================= */

CREATE TABLE IF NOT EXISTS Estudiante (
    idEstudiante INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    cedula VARCHAR(30),
    telefono VARCHAR(20),
    fechaIngreso DATE NOT NULL,
    estado VARCHAR(30) NOT NULL DEFAULT 'Pendiente',
    activo TINYINT NOT NULL DEFAULT 1
);

/* =========================
   TABLA: PausaEstudiante
   Guarda pausas temporales de estudiantes.
   ========================= */

CREATE TABLE IF NOT EXISTS PausaEstudiante (
    idPausa INT AUTO_INCREMENT PRIMARY KEY,
    idEstudiante INT NOT NULL,
    fechaInicio DATE NOT NULL,
    fechaFin DATE,
    motivo VARCHAR(255),
    estadoPausa VARCHAR(30) NOT NULL DEFAULT 'Activa',

    CONSTRAINT fk_pausa_estudiante
    FOREIGN KEY (idEstudiante) REFERENCES Estudiante(idEstudiante)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

/* =========================
   TABLA: Mensualidad
   Controla mensualidades por estudiante, mes y año.
   ========================= */

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

/* =========================
   TABLA: Anualidad
   Controla anualidades por estudiante y año.
   ========================= */

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

/* =========================
   TABLA: Pago
   Guarda el historial de pagos.
   ========================= */

CREATE TABLE IF NOT EXISTS Pago (
    idPago INT AUTO_INCREMENT PRIMARY KEY,
    numeroRecibo VARCHAR(50),
    idEstudiante INT NOT NULL,
    idMensualidad INT,
    idAnualidad INT,
    monto DECIMAL(10,2) NOT NULL,
    fechaPago DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    metodoPago VARCHAR(50) NOT NULL,
    tipoPago VARCHAR(30) NOT NULL,
    observaciones VARCHAR(255),

    CONSTRAINT fk_pago_estudiante
    FOREIGN KEY (idEstudiante) REFERENCES Estudiante(idEstudiante)
    ON UPDATE CASCADE
    ON DELETE CASCADE,

    CONSTRAINT fk_pago_mensualidad
    FOREIGN KEY (idMensualidad) REFERENCES Mensualidad(idMensualidad)
    ON UPDATE CASCADE
    ON DELETE SET NULL,

    CONSTRAINT fk_pago_anualidad
    FOREIGN KEY (idAnualidad) REFERENCES Anualidad(idAnualidad)
    ON UPDATE CASCADE
    ON DELETE SET NULL
);

/* =========================
   VISTA: VistaDashboard
   Devuelve datos listos para el dashboard de Visual Studio.
   ========================= */

CREATE OR REPLACE VIEW VistaDashboard AS
SELECT 
    e.idEstudiante,
    e.nombre,
    e.cedula,
    e.telefono,
    e.fechaIngreso,
    e.estado AS estadoEstudiante,
    e.activo,

    m.idMensualidad,
    m.mesCorrespondiente,
    m.anioCorrespondiente AS anioMensualidad,
    m.monto AS montoMensualidad,
    m.fechaLimite AS fechaLimiteMensualidad,
    m.estado AS estadoMensualidad,

    a.idAnualidad,
    a.anioCorrespondiente AS anioAnualidad,
    a.monto AS montoAnualidad,
    a.fechaLimite AS fechaLimiteAnualidad,
    a.estado AS estadoAnualidad

FROM Estudiante e

LEFT JOIN Mensualidad m
    ON e.idEstudiante = m.idEstudiante
    AND m.mesCorrespondiente = MONTH(CURDATE())
    AND m.anioCorrespondiente = YEAR(CURDATE())

LEFT JOIN Anualidad a
    ON e.idEstudiante = a.idEstudiante
    AND a.anioCorrespondiente = YEAR(CURDATE())

WHERE e.activo = 1;

/* =========================
   VISTA: VistaHistorialPagos
   Devuelve el historial de pagos.
   ========================= */

CREATE OR REPLACE VIEW VistaHistorialPagos AS
SELECT
    p.idPago,
    p.numeroRecibo,

    e.idEstudiante,
    e.nombre AS estudiante,

    p.tipoPago,
    p.monto,
    p.metodoPago,
    p.fechaPago,
    p.observaciones,

    m.idMensualidad,
    m.mesCorrespondiente,
    m.anioCorrespondiente AS anioMensualidad,

    a.idAnualidad,
    a.anioCorrespondiente AS anioAnualidad

FROM Pago p

INNER JOIN Estudiante e
    ON p.idEstudiante = e.idEstudiante

LEFT JOIN Mensualidad m
    ON p.idMensualidad = m.idMensualidad

LEFT JOIN Anualidad a
    ON p.idAnualidad = a.idAnualidad;

/* =========================
   VISTA: VistaPausasEstudiantes
   Devuelve el historial de pausas.
   ========================= */

CREATE OR REPLACE VIEW VistaPausasEstudiantes AS
SELECT
    ps.idPausa,
    e.idEstudiante,
    e.nombre AS estudiante,
    ps.fechaInicio,
    ps.fechaFin,
    ps.motivo,
    ps.estadoPausa
FROM PausaEstudiante ps
INNER JOIN Estudiante e
    ON ps.idEstudiante = e.idEstudiante;

/* =========================
   ÍNDICES
   Mejoran la velocidad de búsqueda.
   ========================= */

CREATE INDEX idx_estudiante_nombre
ON Estudiante(nombre);

CREATE INDEX idx_pago_estudiante
ON Pago(idEstudiante);

CREATE INDEX idx_mensualidad_estudiante_fecha
ON Mensualidad(idEstudiante, mesCorrespondiente, anioCorrespondiente);

CREATE INDEX idx_anualidad_estudiante_anio
ON Anualidad(idEstudiante, anioCorrespondiente);

CREATE INDEX idx_pausa_estudiante
ON PausaEstudiante(idEstudiante);

/* =========================
   CONSULTAS PRINCIPALES PARA VISUAL STUDIO

   Dashboard:
   SELECT * FROM VistaDashboard;

   Historial de pagos:
   SELECT * FROM VistaHistorialPagos
   WHERE idEstudiante = 1;

   Historial de pausas:
   SELECT * FROM VistaPausasEstudiantes
   WHERE idEstudiante = 1;
   ========================= */