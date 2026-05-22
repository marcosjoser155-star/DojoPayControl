/* ============================================================
   BASE DE DATOS: dojo_control
   PROYECTO: Sistema de control de mensualidades para dojo

   DESCRIPCIÓN GENERAL:
   Esta base de datos pertenece a un sistema desarrollado en
   Windows Forms con C# y Visual Studio.

   Su objetivo es controlar:
   - Usuarios del sistema.
   - Estudiantes del dojo.
   - Mensualidades.
   - Anualidades.
   - Pagos realizados.
   - Pausas temporales de estudiantes.

   IMPORTANTE:
   Este script NO borra la base de datos.
   Este script NO contiene DROP DATABASE.
   Está pensado para crear la estructura de la base y seguir
   usándola a futuro sin perder información.

   La clase ConexionDB pertenece al código C#.
   Por eso NO se crea una tabla llamada ConexionDB.
   ============================================================ */


/* ============================================================
   1. CONFIGURACIÓN INICIAL
   ------------------------------------------------------------
   SET NAMES utf8mb4 permite manejar caracteres especiales como:
   - acentos
   - ñ
   - símbolos
   ============================================================ */

SET NAMES utf8mb4;


/* ============================================================
   2. CREACIÓN DE LA BASE DE DATOS
   ------------------------------------------------------------
   CREATE DATABASE IF NOT EXISTS crea la base solo si no existe.

   Si la base dojo_control ya existe, MySQL no la borra ni la
   reemplaza. Solamente la conserva.
   ============================================================ */

CREATE DATABASE IF NOT EXISTS dojo_control
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci;


/* ============================================================
   3. SELECCIONAR LA BASE DE DATOS
   ------------------------------------------------------------
   USE indica que todos los comandos siguientes se ejecutarán
   dentro de la base de datos dojo_control.
   ============================================================ */

USE dojo_control;


/* ============================================================
   4. TABLA: Usuario
   ------------------------------------------------------------
   Esta tabla guarda los usuarios que podrán iniciar sesión
   en el sistema.

   Campos:
   - idUsuario:
     Identificador único del usuario.

   - usuario:
     Nombre de usuario usado para iniciar sesión.

   - contrasena:
     Contraseña del usuario. En un sistema real debería guardarse
     cifrada, pero para el proyecto escolar se puede manejar así.

   - rol:
     Define el tipo de usuario dentro del sistema.
     Ejemplos: Administrador, Recepcionista, Instructor.
   ============================================================ */

CREATE TABLE IF NOT EXISTS Usuario (
    idUsuario INT AUTO_INCREMENT PRIMARY KEY,
    usuario VARCHAR(50) NOT NULL UNIQUE,
    contrasena VARCHAR(255) NOT NULL,
    rol VARCHAR(30) NOT NULL
);


/* ============================================================
   5. TABLA: Estudiante
   ------------------------------------------------------------
   Esta tabla guarda la información principal de los estudiantes
   registrados en el dojo.

   Estados posibles del estudiante:
   - Al día:
     El estudiante tiene sus pagos correspondientes realizados.

   - Pendiente:
     El estudiante todavía puede pagar dentro del tiempo permitido.

   - Restringido:
     El estudiante superó el tiempo permitido y no puede ingresar.

   - Pausado:
     El estudiante no asistirá temporalmente y no debe contarse
     como deudor.

   - Revisar anualidad:
     El estudiante pagó mensualidad, pero tiene anualidad pendiente.

   Campo activo:
   - 1 = estudiante activo.
   - 0 = estudiante inactivo.

   Esto permite conservar el historial sin borrar estudiantes.
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
   6. TABLA: PausaEstudiante
   ------------------------------------------------------------
   Esta tabla registra las pausas temporales de los estudiantes.

   Se usa cuando un estudiante deja de asistir por un tiempo.
   Mientras está en pausa, no debe aparecer como deudor activo.

   Campos:
   - idPausa:
     Identificador único de la pausa.

   - idEstudiante:
     Relaciona la pausa con un estudiante existente.

   - fechaInicio:
     Fecha en que inicia la pausa.

   - fechaFin:
     Fecha en que finaliza la pausa. Puede quedar vacía mientras
     la pausa sigue activa.

   - motivo:
     Razón por la que el estudiante se pausa.

   - estadoPausa:
     Puede ser Activa o Finalizada.
   ============================================================ */

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


/* ============================================================
   7. TABLA: Mensualidad
   ------------------------------------------------------------
   Esta tabla controla la mensualidad de cada estudiante por mes
   y año.

   Campos:
   - idMensualidad:
     Identificador único de la mensualidad.

   - idEstudiante:
     Estudiante al que pertenece la mensualidad.

   - mesCorrespondiente:
     Mes de la mensualidad. Ejemplo:
     1 = enero, 2 = febrero, 3 = marzo.

   - anioCorrespondiente:
     Año de la mensualidad.

   - monto:
     Cantidad a pagar.

   - fechaLimite:
     Fecha máxima permitida para realizar el pago.

   - estado:
     Puede ser Pendiente, Pagada o Vencida.

   Restricción UNIQUE:
   Evita que un mismo estudiante tenga dos mensualidades del
   mismo mes y año.
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
   Esta tabla controla el pago anual de cada estudiante.

   Campos:
   - idAnualidad:
     Identificador único de la anualidad.

   - idEstudiante:
     Estudiante al que pertenece la anualidad.

   - anioCorrespondiente:
     Año al que corresponde la anualidad.

   - monto:
     Cantidad a pagar.

   - fechaLimite:
     Fecha máxima para pagar la anualidad.

   - estado:
     Puede ser Pendiente, Pagada o Vencida.

   Restricción UNIQUE:
   Evita que un estudiante tenga dos anualidades registradas
   para el mismo año.
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
   Esta tabla guarda el historial de pagos realizados.

   Cada pago pertenece a un estudiante.
   Un pago puede estar relacionado con:
   - una mensualidad
   - una anualidad
   - ambas

   Campos:
   - idPago:
     Identificador único del pago.

   - numeroRecibo:
     Folio o número de recibo del pago.

   - idEstudiante:
     Estudiante que realizó el pago.

   - idMensualidad:
     Mensualidad relacionada, si el pago corresponde a mensualidad.

   - idAnualidad:
     Anualidad relacionada, si el pago corresponde a anualidad.

   - monto:
     Cantidad pagada.

   - fechaPago:
     Fecha y hora del pago. Si no se escribe manualmente, MySQL
     coloca la fecha y hora actual.

   - metodoPago:
     Forma de pago. Ejemplos: Efectivo, Transferencia, Tarjeta.

   - tipoPago:
     Tipo de pago. Ejemplos: Mensualidad, Anualidad, Ambos.

   - observaciones:
     Notas adicionales del pago.

   ON DELETE SET NULL:
   Si una mensualidad o anualidad se elimina, el pago no se borra.
   Solo se elimina la relación, conservando el historial.
   ============================================================ */

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


/* ============================================================
   10. VISTA: VistaDashboard
   ------------------------------------------------------------
   Esta vista prepara la información principal para el dashboard
   del sistema en Visual Studio.

   Una vista NO guarda datos nuevos.
   Solo muestra datos que ya existen en las tablas.

   Esta vista muestra:
   - datos del estudiante
   - estado general del estudiante
   - mensualidad del mes actual
   - anualidad del año actual

   Visual Studio puede consultar esta vista con:

   SELECT * FROM VistaDashboard;
   ============================================================ */

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


/* ============================================================
   11. VISTA: VistaHistorialPagos
   ------------------------------------------------------------
   Esta vista prepara el historial de pagos.

   Permite que Visual Studio consulte pagos sin tener que escribir
   una consulta larga cada vez.

   Visual Studio puede consultar un estudiante específico con:

   SELECT * FROM VistaHistorialPagos
   WHERE idEstudiante = 1;

   El número 1 se reemplaza en C# por el estudiante seleccionado.
   ============================================================ */

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


/* ============================================================
   12. VISTA: VistaPausasEstudiantes
   ------------------------------------------------------------
   Esta vista muestra el historial de pausas de los estudiantes.

   Sirve para revisar:
   - cuándo inició una pausa
   - cuándo terminó
   - motivo
   - estado de la pausa

   Visual Studio puede consultar un estudiante específico con:

   SELECT * FROM VistaPausasEstudiantes
   WHERE idEstudiante = 1;
   ============================================================ */

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


/* ============================================================
   13. PROCEDIMIENTO AUXILIAR PARA CREAR ÍNDICES
   ------------------------------------------------------------
   Los índices ayudan a que las búsquedas sean más rápidas.

   MySQL puede dar error si intentamos crear un índice que ya
   existe. Por eso se crea este procedimiento temporal.

   Este procedimiento:
   - recibe el nombre de la tabla
   - recibe el nombre del índice
   - recibe las columnas del índice
   - revisa si el índice ya existe
   - si no existe, lo crea

   Después de usarlo, se elimina para dejar la base limpia.
   ============================================================ */

DROP PROCEDURE IF EXISTS CrearIndiceSiNoExiste;

DELIMITER $$

CREATE PROCEDURE CrearIndiceSiNoExiste(
    IN nombreTabla VARCHAR(64),
    IN nombreIndice VARCHAR(64),
    IN columnasIndice VARCHAR(255)
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
            'CREATE INDEX ', nombreIndice,
            ' ON ', nombreTabla,
            ' (', columnasIndice, ')'
        );

        PREPARE stmt FROM @sql;
        EXECUTE stmt;
        DEALLOCATE PREPARE stmt;
    END IF;
END$$

DELIMITER ;


/* ============================================================
   14. CREACIÓN DE ÍNDICES
   ------------------------------------------------------------
   Estos índices mejoran el rendimiento cuando haya muchos datos.

   idx_estudiante_nombre:
   Ayuda a buscar estudiantes por nombre.

   idx_pago_estudiante:
   Ayuda a buscar pagos por estudiante.

   idx_mensualidad_estudiante_fecha:
   Ayuda a buscar mensualidades por estudiante, mes y año.

   idx_anualidad_estudiante_anio:
   Ayuda a buscar anualidades por estudiante y año.

   idx_pausa_estudiante:
   Ayuda a buscar pausas por estudiante.
   ============================================================ */

CALL CrearIndiceSiNoExiste(
    'Estudiante',
    'idx_estudiante_nombre',
    'nombre'
);

CALL CrearIndiceSiNoExiste(
    'Pago',
    'idx_pago_estudiante',
    'idEstudiante'
);

CALL CrearIndiceSiNoExiste(
    'Mensualidad',
    'idx_mensualidad_estudiante_fecha',
    'idEstudiante, mesCorrespondiente, anioCorrespondiente'
);

CALL CrearIndiceSiNoExiste(
    'Anualidad',
    'idx_anualidad_estudiante_anio',
    'idEstudiante, anioCorrespondiente'
);

CALL CrearIndiceSiNoExiste(
    'PausaEstudiante',
    'idx_pausa_estudiante',
    'idEstudiante'
);


/* ============================================================
   15. ELIMINAR PROCEDIMIENTO AUXILIAR
   ------------------------------------------------------------
   El procedimiento CrearIndiceSiNoExiste solo se necesitaba para
   crear los índices de manera segura.

   Al eliminarlo, los índices quedan creados, pero la base queda
   más limpia.
   ============================================================ */

DROP PROCEDURE IF EXISTS CrearIndiceSiNoExiste;


/* ============================================================
   16. CONSULTAS DE VERIFICACIÓN
   ------------------------------------------------------------
   Estas consultas sirven para comprobar que la base se creó
   correctamente.

   SHOW TABLES muestra las tablas y vistas existentes dentro de
   dojo_control.
   ============================================================ */

SHOW TABLES;


/* ============================================================
   17. CONSULTAS PRINCIPALES PARA VISUAL STUDIO
   ------------------------------------------------------------
   Estas consultas quedan documentadas como referencia para C#.

   Dashboard:
   SELECT * FROM VistaDashboard;

   Historial de pagos:
   SELECT * FROM VistaHistorialPagos
   WHERE idEstudiante = 1;

   Historial de pausas:
   SELECT * FROM VistaPausasEstudiantes
   WHERE idEstudiante = 1;

   NOTA:
   El número 1 representa el idEstudiante y se debe reemplazar
   desde Visual Studio según el estudiante seleccionado.
   ============================================================ */