-- Inserts para la tabla ROL
INSERT INTO Rol (IdRol, Nombre) VALUES (1, 'Administrador');
INSERT INTO Rol (IdRol, Nombre) VALUES (2, 'Profesor');
INSERT INTO Rol (IdRol, Nombre) VALUES (3, 'Alumno');

-- Inserts para la tabla PREFERENCIAS
INSERT INTO Preferencias (IdPreferencia, ColorFondo, ColorBordes, ImagenFondo) VALUES (1, '#FFFFFF', '#000000', 'default.jpg');
INSERT INTO Preferencias (IdPreferencia, ColorFondo, ColorBordes, ImagenFondo) VALUES (2, '#F5F5F5', '#336699', 'azul.jpg');
INSERT INTO Preferencias (IdPreferencia, ColorFondo, ColorBordes, ImagenFondo) VALUES (3, '#E0F7FA', '#00796B', 'verde.jpg');
INSERT INTO Preferencias (IdPreferencia, ColorFondo, ColorBordes, ImagenFondo) VALUES (4, '#FFF3E0', '#FF5722', 'naranja.jpg');
INSERT INTO Preferencias (IdPreferencia, ColorFondo, ColorBordes, ImagenFondo) VALUES (5, '#F3E5F5', '#9C27B0', 'morado.jpg');

-- Inserts para la tabla SEGUIMIENTO
-- Nota: Esta tabla tiene referencias a IdAlumno e IdProfesor que asumimos son IDs de Usuario
INSERT INTO Seguimiento (IdSeguimiento, IdAlumno, IdProfesor) VALUES (1, 3, 2);
INSERT INTO Seguimiento (IdSeguimiento, IdAlumno, IdProfesor) VALUES (2, 4, 2);
INSERT INTO Seguimiento (IdSeguimiento, IdAlumno, IdProfesor) VALUES (3, 5, 6);
INSERT INTO Seguimiento (IdSeguimiento, IdAlumno, IdProfesor) VALUES (4, 7, 6);
INSERT INTO Seguimiento (IdSeguimiento, IdAlumno, IdProfesor) VALUES (5, 8, 9);

-- Inserts para la tabla USUARIO
INSERT INTO Usuario (IdUsuario, NombreUsuario, Gmail, Contraseña, NumeroTelefono, IdRol, IdPreferencia, IdSeguimiento) VALUES (1, 'admin', 'admin@example.com', 'admin123', '123456789', 1, 1, 1);
INSERT INTO Usuario (IdUsuario, NombreUsuario, Gmail, Contraseña, NumeroTelefono, IdRol, IdPreferencia, IdSeguimiento) VALUES (2, 'profesor1', 'profesor1@example.com', 'prof123', '234567890', 2, 2, 1);
INSERT INTO Usuario (IdUsuario, NombreUsuario, Gmail, Contraseña, NumeroTelefono, IdRol, IdPreferencia, IdSeguimiento) VALUES (3, 'alumno1', 'alumno1@example.com', 'alum123', '345678901', 3, 3, 1);
INSERT INTO Usuario (IdUsuario, NombreUsuario, Gmail, Contraseña, NumeroTelefono, IdRol, IdPreferencia, IdSeguimiento) VALUES (4, 'alumno2', 'alumno2@example.com', 'alum234', '456789012', 3, 4, 2);
INSERT INTO Usuario (IdUsuario, NombreUsuario, Gmail, Contraseña, NumeroTelefono, IdRol, IdPreferencia, IdSeguimiento) VALUES (5, 'alumno3', 'alumno3@example.com', 'alum345', '567890123', 3, 5, 3);
INSERT INTO Usuario (IdUsuario, NombreUsuario, Gmail, Contraseña, NumeroTelefono, IdRol, IdPreferencia, IdSeguimiento) VALUES (6, 'profesor2', 'profesor2@example.com', 'prof234', '678901234', 2, 1, 3);
INSERT INTO Usuario (IdUsuario, NombreUsuario, Gmail, Contraseña, NumeroTelefono, IdRol, IdPreferencia, IdSeguimiento) VALUES (7, 'alumno4', 'alumno4@example.com', 'alum456', '789012345', 3, 2, 4);
INSERT INTO Usuario (IdUsuario, NombreUsuario, Gmail, Contraseña, NumeroTelefono, IdRol, IdPreferencia, IdSeguimiento) VALUES (8, 'alumno5', 'alumno5@example.com', 'alum567', '890123456', 3, 3, 5);
INSERT INTO Usuario (IdUsuario, NombreUsuario, Gmail, Contraseña, NumeroTelefono, IdRol, IdPreferencia, IdSeguimiento) VALUES (9, 'profesor3', 'profesor3@example.com', 'prof345', '901234567', 2, 4, 5);

-- Inserts para la tabla ASIGNATURA
INSERT INTO Asignatura (IdAsignatura, Nombre, Descripcion) VALUES (1, 'Matemáticas', 'Curso básico de matemáticas');
INSERT INTO Asignatura (IdAsignatura, Nombre, Descripcion) VALUES (2, 'Física', 'Principios fundamentales de física');
INSERT INTO Asignatura (IdAsignatura, Nombre, Descripcion) VALUES (3, 'Química', 'Introducción a la química general');
INSERT INTO Asignatura (IdAsignatura, Nombre, Descripcion) VALUES (4, 'Programación', 'Fundamentos de programación en Java');
INSERT INTO Asignatura (IdAsignatura, Nombre, Descripcion) VALUES (5, 'Base de Datos', 'Diseño y gestión de bases de datos relacionales');

-- Inserts para la tabla USUARIO_ASIGNATURA
INSERT INTO UsuarioAsignatura (IdUsuario, IdAsignatura) VALUES (2, 1); -- profesor1 enseña Matemáticas
INSERT INTO UsuarioAsignatura (IdUsuario, IdAsignatura) VALUES (2, 2); -- profesor1 enseña Física
INSERT INTO UsuarioAsignatura (IdUsuario, IdAsignatura) VALUES (6, 3); -- profesor2 enseña Química
INSERT INTO UsuarioAsignatura (IdUsuario, IdAsignatura) VALUES (6, 4); -- profesor2 enseña Programación
INSERT INTO UsuarioAsignatura (IdUsuario, IdAsignatura) VALUES (9, 5); -- profesor3 enseña Base de Datos
INSERT INTO UsuarioAsignatura (IdUsuario, IdAsignatura) VALUES (3, 1); -- alumno1 cursa Matemáticas
INSERT INTO UsuarioAsignatura (IdUsuario, IdAsignatura) VALUES (3, 2); -- alumno1 cursa Física
INSERT INTO UsuarioAsignatura (IdUsuario, IdAsignatura) VALUES (4, 1); -- alumno2 cursa Matemáticas
INSERT INTO UsuarioAsignatura (IdUsuario, IdAsignatura) VALUES (5, 3); -- alumno3 cursa Química
INSERT INTO UsuarioAsignatura (IdUsuario, IdAsignatura) VALUES (5, 4); -- alumno3 cursa Programación
INSERT INTO UsuarioAsignatura (IdUsuario, IdAsignatura) VALUES (7, 4); -- alumno4 cursa Programación
INSERT INTO UsuarioAsignatura (IdUsuario, IdAsignatura) VALUES (8, 5); -- alumno5 cursa Base de Datos

-- Inserts para la tabla QUIZ
INSERT INTO Quiz (IdQuizz, Nombre, Descripcion, IdAsignatura, IdUsuario) VALUES (1, 'Quiz de Álgebra', 'Evaluación de conceptos básicos de álgebra', 1, 2);
INSERT INTO Quiz (IdQuizz, Nombre, Descripcion, IdAsignatura, IdUsuario) VALUES (2, 'Quiz de Mecánica', 'Evaluación sobre leyes de Newton', 2, 2);
INSERT INTO Quiz (IdQuizz, Nombre, Descripcion, IdAsignatura, IdUsuario) VALUES (3, 'Quiz de Química Orgánica', 'Evaluación sobre compuestos orgánicos', 3, 6);
INSERT INTO Quiz (IdQuizz, Nombre, Descripcion, IdAsignatura, IdUsuario) VALUES (4, 'Quiz de Java', 'Evaluación sobre conceptos básicos de Java', 4, 6);
INSERT INTO Quiz (IdQuizz, Nombre, Descripcion, IdAsignatura, IdUsuario) VALUES (5, 'Quiz de SQL', 'Evaluación sobre consultas SQL', 5, 9);

-- Inserts para la tabla DETALLE QUIZZ
INSERT INTO DetalleQuizz (IdDetalleQuizz, IdQuizz, Pregunta, Opciones) VALUES (1, 1, '¿Cuánto es 2+2?', '{"A":"3","B":"4","C":"5","D":"6","Correcta":"B"}');
INSERT INTO DetalleQuizz (IdDetalleQuizz, IdQuizz, Pregunta, Opciones) VALUES (2, 1, '¿Cuál es el valor de x en la ecuación 2x-5=3?', '{"A":"3","B":"4","C":"5","D":"6","Correcta":"B"}');
INSERT INTO DetalleQuizz (IdDetalleQuizz, IdQuizz, Pregunta, Opciones) VALUES (3, 2, '¿Cuál es la primera ley de Newton?', '{"A":"Ley de gravitación universal","B":"Ley de la inercia","C":"Ley de acción y reacción","D":"Ley de la conservación de la energía","Correcta":"B"}');
INSERT INTO DetalleQuizz (IdDetalleQuizz, IdQuizz, Pregunta, Opciones) VALUES (4, 3, '¿Qué es un alcano?', '{"A":"Compuesto con dobles enlaces","B":"Compuesto con triples enlaces","C":"Compuesto con enlaces simples carbono-carbono","D":"Compuesto aromático","Correcta":"C"}');
INSERT INTO DetalleQuizz (IdDetalleQuizz, IdQuizz, Pregunta, Opciones) VALUES (5, 4, '¿Cuál es la palabra clave para declarar una clase en Java?', '{"A":"class","B":"struct","C":"type","D":"object","Correcta":"A"}');
INSERT INTO DetalleQuizz (IdDetalleQuizz, IdQuizz, Pregunta, Opciones) VALUES (6, 5, '¿Cuál es la sentencia SQL para seleccionar todos los campos de una tabla?', '{"A":"SELECT * FROM tabla","B":"SELECT ALL FROM tabla","C":"GET * FROM tabla","D":"RETRIEVE * FROM tabla","Correcta":"A"}');

-- Inserts para la tabla VIDEO
INSERT INTO Video (IdVideo, NombreVideo, Descripcion, MeGusta, Miniatura, IdTablaMinutos, IdAsignatura, IdUsuario) VALUES (1, 'Introducción al Álgebra', 'Video introductorio sobre conceptos de álgebra', 25, 'algebra_thumb.jpg', NULL, 1, 2);
INSERT INTO Video (IdVideo, NombreVideo, Descripcion, MeGusta, Miniatura, IdTablaMinutos, IdAsignatura, IdUsuario) VALUES (2, 'Leyes de Newton', 'Explicación detallada de las tres leyes de Newton', 42, 'newton_thumb.jpg', NULL, 2, 2);
INSERT INTO Video (IdVideo, NombreVideo, Descripcion, MeGusta, Miniatura, IdTablaMinutos, IdAsignatura, IdUsuario) VALUES (3, 'Tabla Periódica', 'Recorrido por la tabla periódica de los elementos', 18, 'tabla_periodica_thumb.jpg', NULL, 3, 6);
INSERT INTO Video (IdVideo, NombreVideo, Descripcion, MeGusta, Miniatura, IdTablaMinutos, IdAsignatura, IdUsuario) VALUES (4, 'Programación Orientada a Objetos', 'Conceptos básicos de POO en Java', 37, 'poo_thumb.jpg', NULL, 4, 6);
INSERT INTO Video (IdVideo, NombreVideo, Descripcion, MeGusta, Miniatura, IdTablaMinutos, IdAsignatura, IdUsuario) VALUES (5, 'Consultas SQL Avanzadas', 'Tutorial sobre joins, subconsultas y funciones agregadas', 29, 'sql_thumb.jpg', NULL, 5, 9);