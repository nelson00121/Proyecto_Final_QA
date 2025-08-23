create database bodega;
use bodega;

CREATE TABLE `usuario` (
  `id` integer PRIMARY KEY AUTO_INCREMENT,
  `usuario` varchar(50) UNIQUE NOT NULL,
  `is_admin` boolean NOT NULL,
  `password` varchar(100) NOT NULL,
  `fecha_commit` datetime DEFAULT (now())
);

CREATE TABLE `empleado` (
  `id` integer PRIMARY KEY AUTO_INCREMENT,
  `primer_nombre` varchar(50) NOT NULL,
  `segundo_nombre` varchar(50) NOT NULL,
  `primer_apellido` varchar(50) NOT NULL,
  `segundo_apellido` varchar(50) NOT NULL,
  `id_usuario` integer UNIQUE NOT NULL,
  `estado` ENUM ('activo', 'inactivo', 'vacaciones') DEFAULT 'activo'
);

CREATE TABLE `area` (
  `id` integer PRIMARY KEY AUTO_INCREMENT,
  `nombre` varchar(50) NOT NULL,
  `id_usuario` integer NOT NULL
);

CREATE TABLE `marca` (
  `id` integer PRIMARY KEY AUTO_INCREMENT,
  `nombre` varchar(50) NOT NULL
);

CREATE TABLE `equipo` (
  `id` integer PRIMARY KEY AUTO_INCREMENT,
  `identificador` varchar(20),
  `nombre` varchar(50) NOT NULL,
  `id_marca` integer NOT NULL,
  `color` varchar(50) NOT NULL,
  `valor` double (10,2) NOT NULL,
  `serie` varchar(50) NOT NULL,
  `extras` text,
  `tipo_alimentacion` ENUM ('110v', '220v', 'diesel', 'regular', 'super', 'bateria', 'ninguna') DEFAULT 'ninguna',
  `id_empleado` integer,
  `estado` ENUM ('activo', 'inactivo', 'mantenimiento', 'suspendido') DEFAULT 'activo',
  `fecha_commit` datetime DEFAULT (now())
);

CREATE TABLE `vehiculo` (
  `id` integer PRIMARY KEY AUTO_INCREMENT,
  `no_motor` varchar(20) NOT NULL,
  `vin` varchar(20) NOT NULL,
  `cilindrada` integer NOT NULL,
  `placa` varchar(10) NOT NULL,
  `modelo` integer NOT NULL,
  `id_equipo` integer NOT NULL
);

CREATE TABLE `electronico` (
  `id` integer PRIMARY KEY AUTO_INCREMENT,
  `imei` varchar(20),
  `sistema_operativo` varchar(20),
  `conectividad` ENUM ('bluetooth', 'wifi', 'gsm', 'nfc', 'bluetooth_wifi', 'bluetooth_gsm', 'bluetooth_nfc', 'wifi_gsm', 'wifi_nfc', 'gsm_nfc', 'bluetooth_wifi_gsm', 'bluetooth_wifi_nfc', 'bluetooth_gsm_nfc', 'wifi_gsm_nfc', 'bluetooth_wifi_gsm_nfc', 'ninguno') DEFAULT 'ninguno',
  `operador` ENUM ('starlink', 'claro', 'tigo', 'comnet', 'verasat', 'telecom', 'ninguno') DEFAULT 'ninguno',
  `id_equipo` integer NOT NULL
);

CREATE TABLE `mobiliario` (
  `id` integer PRIMARY KEY AUTO_INCREMENT,
  `material` varchar(50) NOT NULL,
  `altura` float(8,2) NOT NULL,
  `ancho` float(8,2) NOT NULL,
  `profundidad` float(8,2) NOT NULL,
  `cantidad_piezas` integer DEFAULT 1,
  `id_equipo` integer NOT NULL
);

CREATE TABLE `herramienta` (
  `id` integer PRIMARY KEY AUTO_INCREMENT,
  `material` varchar(50) NOT NULL,
  `id_equipo` integer NOT NULL
);

CREATE TABLE `reporte` (
  `id` integer PRIMARY KEY AUTO_INCREMENT,
  `observacion` text,
  `id_causa` integer NOT NULL,
  `id_equipo` integer NOT NULL,
  `id_empleado` integer NOT NULL,
  `fecha_commit` datetime DEFAULT (now())
);

CREATE TABLE `imagen` (
  `id` integer PRIMARY KEY AUTO_INCREMENT,
  `url` text NOT NULL,
  `id_reporte` integer NOT NULL
);

CREATE TABLE `causa` (
  `id` integer PRIMARY KEY AUTO_INCREMENT,
  `nombre` varchar(50) NOT NULL,
  `descripcion` varchar(300)
);

CREATE TABLE `asignacion` (
  `id` integer PRIMARY KEY AUTO_INCREMENT,
  `id_equipo` integer NOT NULL,
  `id_empleado` integer NOT NULL,
  `fecha_inicio` datetime NOT NULL,
  `fecha_fin` datetime,
  `observacion` text
);

CREATE TABLE `mantenimiento` (
  `id` integer PRIMARY KEY AUTO_INCREMENT,
  `id_equipo` integer NOT NULL,
  `fecha` datetime NOT NULL,
  `tipo` varchar(20) NOT NULL,
  `descripcion` text,
  `costo` double(10,2)
);

CREATE TABLE `log_movimiento` (
  `id` integer PRIMARY KEY AUTO_INCREMENT,
  `id_usuario` integer NOT NULL,
  `accion` varchar(100) NOT NULL,
  `descripcion` text,
  `fecha_commit` datetime DEFAULT (now())
);

ALTER TABLE `empleado` ADD FOREIGN KEY (`id_usuario`) REFERENCES `usuario` (`id`);

ALTER TABLE `area` ADD FOREIGN KEY (`id_usuario`) REFERENCES `usuario` (`id`);

ALTER TABLE `equipo` ADD FOREIGN KEY (`id_marca`) REFERENCES `marca` (`id`);

ALTER TABLE `equipo` ADD FOREIGN KEY (`id_empleado`) REFERENCES `empleado` (`id`);

ALTER TABLE `vehiculo` ADD FOREIGN KEY (`id_equipo`) REFERENCES `equipo` (`id`);

ALTER TABLE `electronico` ADD FOREIGN KEY (`id_equipo`) REFERENCES `equipo` (`id`);

ALTER TABLE `mobiliario` ADD FOREIGN KEY (`id_equipo`) REFERENCES `equipo` (`id`);

ALTER TABLE `herramienta` ADD FOREIGN KEY (`id_equipo`) REFERENCES `equipo` (`id`);

ALTER TABLE `reporte` ADD FOREIGN KEY (`id_causa`) REFERENCES `causa` (`id`);

ALTER TABLE `reporte` ADD FOREIGN KEY (`id_equipo`) REFERENCES `equipo` (`id`);

ALTER TABLE `reporte` ADD FOREIGN KEY (`id_empleado`) REFERENCES `empleado` (`id`);

ALTER TABLE `imagen` ADD FOREIGN KEY (`id_reporte`) REFERENCES `reporte` (`id`);

ALTER TABLE `asignacion` ADD FOREIGN KEY (`id_equipo`) REFERENCES `equipo` (`id`);

ALTER TABLE `asignacion` ADD FOREIGN KEY (`id_empleado`) REFERENCES `empleado` (`id`);

ALTER TABLE `mantenimiento` ADD FOREIGN KEY (`id_equipo`) REFERENCES `equipo` (`id`);

ALTER TABLE `log_movimiento` ADD FOREIGN KEY (`id_usuario`) REFERENCES `usuario` (`id`);