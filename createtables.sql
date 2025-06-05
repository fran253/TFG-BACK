-- bbddacademIQ.Rol definition

CREATE TABLE `Rol` (
  `idRol` int NOT NULL,
  `nombre` varchar(50) NOT NULL,
  PRIMARY KEY (`idRol`),
  UNIQUE KEY `nombre` (`nombre`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- bbddacademIQ.`__EFMigrationsHistory` definition

CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- bbddacademIQ.Usuario definition

CREATE TABLE `Usuario` (
  `idUsuario` int NOT NULL AUTO_INCREMENT,
  `avatar` text,
  `nombre` varchar(100) NOT NULL,
  `apellidos` varchar(100) DEFAULT NULL,
  `gmail` varchar(255) NOT NULL,
  `telefono` varchar(20) DEFAULT NULL,
  `contraseña` varchar(255) NOT NULL,
  `idRol` int NOT NULL,
  `CursosSeguidos` text,
  `Token` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`idUsuario`),
  UNIQUE KEY `gmail` (`gmail`),
  KEY `idRol` (`idRol`),
  CONSTRAINT `Usuario_ibfk_1` FOREIGN KEY (`idRol`) REFERENCES `Rol` (`idRol`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=56 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- bbddacademIQ.Curso definition

CREATE TABLE `Curso` (
  `idCurso` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(255) NOT NULL,
  `imagen` text,
  `descripcion` text,
  `fechaCreacion` datetime DEFAULT CURRENT_TIMESTAMP,
  `idUsuario` int DEFAULT NULL,
  PRIMARY KEY (`idCurso`),
  KEY `FK_Curso_Usuario` (`idUsuario`),
  CONSTRAINT `FK_Curso_Usuario` FOREIGN KEY (`idUsuario`) REFERENCES `Usuario` (`idUsuario`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- bbddacademIQ.PeticionProfesor definition

CREATE TABLE `PeticionProfesor` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `idUsuario` int NOT NULL,
  `DocumentacionUrl` text NOT NULL,
  `Texto` text NOT NULL,
  `FechaPeticion` datetime NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `idUsuario` (`idUsuario`),
  CONSTRAINT `PeticionProfesor_ibfk_1` FOREIGN KEY (`idUsuario`) REFERENCES `Usuario` (`idUsuario`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- bbddacademIQ.Usuario_Curso definition

CREATE TABLE `Usuario_Curso` (
  `idUsuario` int NOT NULL,
  `idCurso` int NOT NULL,
  PRIMARY KEY (`idUsuario`,`idCurso`),
  KEY `Usuario_Curso_ibfk_2` (`idCurso`),
  CONSTRAINT `Usuario_Curso_ibfk_1` FOREIGN KEY (`idUsuario`) REFERENCES `Usuario` (`idUsuario`) ON DELETE CASCADE,
  CONSTRAINT `Usuario_Curso_ibfk_2` FOREIGN KEY (`idCurso`) REFERENCES `Curso` (`idCurso`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- bbddacademIQ.Asignatura definition

CREATE TABLE `Asignatura` (
  `idAsignatura` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(255) NOT NULL,
  `descripcion` text,
  `imagen` text,
  `fechaCreacion` datetime DEFAULT CURRENT_TIMESTAMP,
  `idCurso` int NOT NULL,
  PRIMARY KEY (`idAsignatura`),
  KEY `Asignatura_ibfk_1` (`idCurso`),
  CONSTRAINT `Asignatura_ibfk_1` FOREIGN KEY (`idCurso`) REFERENCES `Curso` (`idCurso`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=40 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- bbddacademIQ.Quiz definition

CREATE TABLE `Quiz` (
  `IdQuiz` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(100) NOT NULL,
  `Descripcion` text,
  `IdUsuario` int NOT NULL,
  `FechaCreacion` datetime DEFAULT CURRENT_TIMESTAMP,
  `IdCurso` int DEFAULT NULL,
  `IdAsignatura` int DEFAULT NULL,
  PRIMARY KEY (`IdQuiz`),
  KEY `IdUsuario` (`IdUsuario`),
  KEY `FK_Quiz_Curso` (`IdCurso`),
  KEY `FK_Quiz_Asignatura` (`IdAsignatura`),
  CONSTRAINT `FK_Quiz_Asignatura` FOREIGN KEY (`IdAsignatura`) REFERENCES `Asignatura` (`idAsignatura`),
  CONSTRAINT `FK_Quiz_Curso` FOREIGN KEY (`IdCurso`) REFERENCES `Curso` (`idCurso`),
  CONSTRAINT `Quiz_ibfk_1` FOREIGN KEY (`IdUsuario`) REFERENCES `Usuario` (`idUsuario`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=46 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- bbddacademIQ.ResultadoQuiz definition

CREATE TABLE `ResultadoQuiz` (
  `IdResultado` int NOT NULL AUTO_INCREMENT,
  `IdUsuario` int NOT NULL,
  `IdQuiz` int NOT NULL,
  `Puntuacion` decimal(5,2) NOT NULL,
  `RespuestasSeleccionadas` text,
  `Fecha` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`IdResultado`),
  KEY `IdUsuario` (`IdUsuario`),
  KEY `IdQuiz` (`IdQuiz`),
  CONSTRAINT `ResultadoQuiz_ibfk_1` FOREIGN KEY (`IdUsuario`) REFERENCES `Usuario` (`idUsuario`) ON DELETE CASCADE,
  CONSTRAINT `ResultadoQuiz_ibfk_2` FOREIGN KEY (`IdQuiz`) REFERENCES `Quiz` (`IdQuiz`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- bbddacademIQ.Usuario_Asignatura definition

CREATE TABLE `Usuario_Asignatura` (
  `idUsuario` int NOT NULL,
  `idAsignatura` int NOT NULL,
  PRIMARY KEY (`idUsuario`,`idAsignatura`),
  KEY `Usuario_Asignatura_ibfk_2` (`idAsignatura`),
  CONSTRAINT `Usuario_Asignatura_ibfk_1` FOREIGN KEY (`idUsuario`) REFERENCES `Usuario` (`idUsuario`) ON DELETE CASCADE,
  CONSTRAINT `Usuario_Asignatura_ibfk_2` FOREIGN KEY (`idAsignatura`) REFERENCES `Asignatura` (`idAsignatura`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- bbddacademIQ.ValoracionQuiz definition

CREATE TABLE `ValoracionQuiz` (
  `IdValoracion` int NOT NULL AUTO_INCREMENT,
  `IdUsuario` int NOT NULL,
  `IdQuiz` int NOT NULL,
  `Puntuacion` int NOT NULL,
  `Comentario` varchar(500) DEFAULT NULL,
  `Fecha` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`IdValoracion`),
  UNIQUE KEY `unique_valoracion` (`IdUsuario`,`IdQuiz`),
  KEY `IdQuiz` (`IdQuiz`),
  CONSTRAINT `ValoracionQuiz_ibfk_1` FOREIGN KEY (`IdUsuario`) REFERENCES `Usuario` (`idUsuario`) ON DELETE CASCADE,
  CONSTRAINT `ValoracionQuiz_ibfk_2` FOREIGN KEY (`IdQuiz`) REFERENCES `Quiz` (`IdQuiz`) ON DELETE CASCADE,
  CONSTRAINT `ValoracionQuiz_chk_1` CHECK ((`Puntuacion` between 1 and 5))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- bbddacademIQ.Video definition

CREATE TABLE `Video` (
  `idVideo` int NOT NULL AUTO_INCREMENT,
  `titulo` varchar(150) NOT NULL,
  `descripcion` text,
  `duracion` text,
  `url` text NOT NULL,
  `miniatura` text,
  `fechaSubida` datetime DEFAULT CURRENT_TIMESTAMP,
  `idAsignatura` int NOT NULL,
  `idUsuario` int NOT NULL,
  `numReportes` int DEFAULT '0',
  `idCurso` int DEFAULT NULL,
  `ContadorLikes` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`idVideo`),
  KEY `idUsuario` (`idUsuario`),
  KEY `Video_ibfk_1` (`idAsignatura`),
  KEY `Video_ibfk_2` (`idCurso`),
  CONSTRAINT `Video_ibfk_1` FOREIGN KEY (`idAsignatura`) REFERENCES `Asignatura` (`idAsignatura`) ON DELETE CASCADE,
  CONSTRAINT `Video_ibfk_2` FOREIGN KEY (`idCurso`) REFERENCES `Curso` (`idCurso`),
  CONSTRAINT `Video_ibfk_3` FOREIGN KEY (`idUsuario`) REFERENCES `Usuario` (`idUsuario`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=107 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- bbddacademIQ.ComentarioVideo definition

CREATE TABLE `ComentarioVideo` (
  `idComentario` int NOT NULL AUTO_INCREMENT,
  `idUsuario` int NOT NULL,
  `idVideo` int NOT NULL,
  `texto` text NOT NULL,
  `fecha` datetime DEFAULT CURRENT_TIMESTAMP,
  `NumeroReportes` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`idComentario`),
  KEY `idUsuario` (`idUsuario`),
  KEY `idVideo` (`idVideo`),
  CONSTRAINT `ComentarioVideo_ibfk_1` FOREIGN KEY (`idUsuario`) REFERENCES `Usuario` (`idUsuario`) ON DELETE CASCADE,
  CONSTRAINT `ComentarioVideo_ibfk_2` FOREIGN KEY (`idVideo`) REFERENCES `Video` (`idVideo`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=49 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- bbddacademIQ.Favorito definition

CREATE TABLE `Favorito` (
  `idUsuario` int NOT NULL,
  `idVideo` int NOT NULL,
  PRIMARY KEY (`idUsuario`,`idVideo`),
  KEY `idVideo` (`idVideo`),
  CONSTRAINT `Favorito_ibfk_1` FOREIGN KEY (`idUsuario`) REFERENCES `Usuario` (`idUsuario`) ON DELETE CASCADE,
  CONSTRAINT `Favorito_ibfk_2` FOREIGN KEY (`idVideo`) REFERENCES `Video` (`idVideo`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- bbddacademIQ.HistorialVideo definition

CREATE TABLE `HistorialVideo` (
  `idHistorial` int NOT NULL AUTO_INCREMENT,
  `idUsuario` int NOT NULL,
  `idVideo` int NOT NULL,
  `fechaVisualizacion` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`idHistorial`),
  KEY `idUsuario` (`idUsuario`),
  KEY `idVideo` (`idVideo`),
  CONSTRAINT `HistorialVideo_ibfk_1` FOREIGN KEY (`idUsuario`) REFERENCES `Usuario` (`idUsuario`) ON DELETE CASCADE,
  CONSTRAINT `HistorialVideo_ibfk_2` FOREIGN KEY (`idVideo`) REFERENCES `Video` (`idVideo`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- bbddacademIQ.MarcadorVideo definition

CREATE TABLE `MarcadorVideo` (
  `idMarcador` int NOT NULL AUTO_INCREMENT,
  `idVideo` int NOT NULL,
  `minutoImportante` decimal(5,2) NOT NULL,
  `titulo` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`idMarcador`),
  KEY `idVideo` (`idVideo`),
  CONSTRAINT `MarcadorVideo_ibfk_1` FOREIGN KEY (`idVideo`) REFERENCES `Video` (`idVideo`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=78 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- bbddacademIQ.Pregunta definition

CREATE TABLE `Pregunta` (
  `IdPregunta` int NOT NULL AUTO_INCREMENT,
  `IdQuiz` int NOT NULL,
  `Descripcion` text NOT NULL,
  `Orden` int NOT NULL DEFAULT '1',
  PRIMARY KEY (`IdPregunta`),
  KEY `IdQuiz` (`IdQuiz`),
  CONSTRAINT `Pregunta_ibfk_1` FOREIGN KEY (`IdQuiz`) REFERENCES `Quiz` (`IdQuiz`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=42 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- bbddacademIQ.ReporteVideo definition

CREATE TABLE `ReporteVideo` (
  `idReporte` int NOT NULL AUTO_INCREMENT,
  `idVideo` int NOT NULL,
  `idUsuario` int NOT NULL,
  `motivo` varchar(100) NOT NULL,
  `fecha` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`idReporte`),
  KEY `idVideo` (`idVideo`),
  KEY `idUsuario` (`idUsuario`),
  CONSTRAINT `ReporteVideo_ibfk_1` FOREIGN KEY (`idVideo`) REFERENCES `Video` (`idVideo`) ON DELETE CASCADE,
  CONSTRAINT `ReporteVideo_ibfk_2` FOREIGN KEY (`idUsuario`) REFERENCES `Usuario` (`idUsuario`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=38 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


-- bbddacademIQ.Respuesta definition

CREATE TABLE `Respuesta` (
  `IdRespuesta` int NOT NULL AUTO_INCREMENT,
  `IdPregunta` int NOT NULL,
  `Texto` varchar(255) NOT NULL,
  `EsCorrecta` tinyint(1) NOT NULL DEFAULT '0',
  `Orden` int NOT NULL DEFAULT '1',
  PRIMARY KEY (`IdRespuesta`),
  KEY `IdPregunta` (`IdPregunta`),
  CONSTRAINT `Respuesta_ibfk_1` FOREIGN KEY (`IdPregunta`) REFERENCES `Pregunta` (`IdPregunta`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=80 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
