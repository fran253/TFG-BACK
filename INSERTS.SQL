-- Insertar roles
INSERT INTO Rol (idRol, nombre) VALUES
(3, 'Administrador'),
(2, 'Profesor'),
(1, 'Alumno');

-- Insertar dificultades
INSERT INTO Dificultad (idDificultad, nombre, descripcion) VALUES
(1, 'Básico', 'Nivel introductorio para principiantes'),
(2, 'Intermedio', 'Nivel para usuarios con conocimientos previos'),
(3, 'Avanzado', 'Nivel para usuarios con amplia experiencia');

-- Insertar preferencias visuales
INSERT INTO Preferencias (colorFondo, colorBordes, imagenFondo) VALUES
('#FFFFFF', '#000000', 'default_bg.jpg'),
('#F5F5F5', '#3498DB', 'blue_bg.jpg'),
('#E8F4F8', '#2ECC71', 'green_bg.jpg');

-- Insertar usuarios
INSERT INTO Usuario (avatar, nombre, apellidos, gmail, telefono, contraseña, idRol, idPreferencia) VALUES
('avatar1.png', 'Admin', 'Principal', 'admin@example.com', '666111222', 'hash_contraseña_segura1', 1, 1),
('avatar2.png', 'María', 'González', 'maria.prof@example.com', '666333444', 'hash_contraseña_segura2', 2, 2),
('avatar3.png', 'Carlos', 'Fernández', 'carlos.prof@example.com', '666555666', 'hash_contraseña_segura3', 2, 3),
('avatar4.png', 'Laura', 'Martínez', 'laura.alu@example.com', '666777888', 'hash_contraseña_segura4', 3, 1),
('avatar5.png', 'David', 'López', 'david.alu@example.com', '666999000', 'hash_contraseña_segura5', 3, 2);

-- Insertar seguimientos (alumnos siguen a profesores)
INSERT INTO Seguimiento (idAlumno, idProfesor) VALUES
(4, 2), -- Laura sigue a María
(4, 3), -- Laura sigue a Carlos
(5, 2); -- David sigue a María

-- Insertar cursos
INSERT INTO Curso (idCurso, nombre, imagen, descripcion) VALUES
(1, 'Desarrollo Web', 'curso_web.jpg', 'Curso completo de desarrollo web desde cero'),
(2, 'Matemáticas Aplicadas', 'curso_mates.jpg', 'Fundamentos matemáticos para ciencias e ingeniería'),
(3, 'Programación en Python', 'curso_python.jpg', 'Aprende Python desde los conceptos básicos hasta aplicaciones avanzadas');

-- Insertar relaciones usuario-curso
INSERT INTO Usuario_Curso (idUsuario, idCurso) VALUES
(2, 1), -- María enseña Desarrollo Web
(2, 2), -- María enseña Matemáticas
(3, 3), -- Carlos enseña Python
(4, 1), -- Laura estudia Desarrollo Web
(4, 3), -- Laura estudia Python
(5, 1), -- David estudia Desarrollo Web
(5, 2); -- David estudia Matemáticas

-- Insertar asignaturas
INSERT INTO Asignatura (idAsignatura, nombre, descripcion, imagen, idCurso) VALUES
(1, 'HTML y CSS', 'Fundamentos de HTML5 y CSS3', 'html_css.jpg', 1),
(2, 'JavaScript', 'Programación con JavaScript', 'javascript.jpg', 1),
(3, 'Álgebra Lineal', 'Sistemas lineales y matrices', 'algebra.jpg', 2),
(4, 'Cálculo', 'Cálculo diferencial e integral', 'calculo.jpg', 2),
(5, 'Python Básico', 'Sintaxis y estructuras básicas', 'python_basico.jpg', 3),
(6, 'Python Avanzado', 'Programación orientada a objetos y aplicaciones', 'python_avanzado.jpg', 3);

-- Insertar relaciones usuario-asignatura
INSERT INTO Usuario_Asignatura (idUsuario, idAsignatura) VALUES
(2, 1), -- María enseña HTML y CSS
(2, 2), -- María enseña JavaScript
(2, 3), -- María enseña Álgebra
(2, 4), -- María enseña Cálculo
(3, 5), -- Carlos enseña Python Básico
(3, 6), -- Carlos enseña Python Avanzado
(4, 1), -- Laura estudia HTML y CSS
(4, 2), -- Laura estudia JavaScript
(4, 5), -- Laura estudia Python Básico
(5, 1), -- David estudia HTML y CSS
(5, 3), -- David estudia Álgebra
(5, 4); -- David estudia Cálculo

-- Insertar vídeos
INSERT INTO Video (titulo, descripcion, duracion, url, miniatura, idAsignatura, idCurso, idUsuario) VALUES
('Introducción a HTML5', 'Conceptos básicos de HTML5', '15:30', 'https://example.com/videos/html_intro', 'thumb_html.jpg', 1, 1, 2),
('Selectores CSS', 'Cómo usar selectores en CSS', '12:45', 'https://example.com/videos/css_selectores', 'thumb_css.jpg', 1, 1, 2),
('Variables en JavaScript', 'Declaración y ámbito de variables', '20:10', 'https://example.com/videos/js_variables', 'thumb_js.jpg', 2, 1, 2),
('Matrices y determinantes', 'Operaciones con matrices', '25:00', 'https://example.com/videos/matrices', 'thumb_matrices.jpg', 3, 2, 2),
('Primeros pasos en Python', 'Tu primer programa en Python', '18:20', 'https://example.com/videos/python_inicio', 'thumb_python.jpg', 5, 3, 3);

-- Insertar marcadores de vídeo
INSERT INTO MarcadorVideo (idVideo, minutoImportante, titulo) VALUES
(1, 2.30, 'Estructura básica HTML'),
(1, 8.45, 'Etiquetas semánticas'),
(2, 5.15, 'Selectores de clase'),
(3, 7.20, 'Let vs Const'),
(4, 12.50, 'Cálculo del determinante');

-- Insertar favoritos
INSERT INTO Favorito (idUsuario, idVideo) VALUES
(4, 1), -- Laura marca como favorito el vídeo de HTML
(4, 3), -- Laura marca como favorito el vídeo de JavaScript
(5, 4); -- David marca como favorito el vídeo de matrices

-- Insertar comentarios
INSERT INTO ComentarioVideo (idUsuario, idVideo, texto) VALUES
(4, 1, '¡Excelente explicación! Me ha ayudado mucho.'),
(5, 1, 'Muy claro, gracias profesora.'),
(4, 3, '¿Podrías profundizar más en la diferencia entre var, let y const?'),
(5, 4, 'No entiendo bien la parte del cálculo de la inversa, ¿podrías hacer un vídeo sobre eso?');

-- Insertar quizzes
INSERT INTO Quiz (idQuiz, nombre, descripcion, duracionPromedio, idDificultad, idAsignatura, idUsuario, idCurso) VALUES
(1, 'Fundamentos HTML', 'Test sobre conceptos básicos de HTML', '10 min', 1, 1, 2, 1),
(2, 'JavaScript Básico', 'Evaluación de conocimientos básicos de JS', '15 min', 2, 2, 2, 1),
(3, 'Álgebra: Matrices', 'Test sobre operaciones con matrices', '20 min', 2, 3, 2, 2),
(4, 'Python: Sintaxis', 'Evaluación de sintaxis básica', '10 min', 1, 5, 3, 3);

-- Insertar detalles de quiz
INSERT INTO DetalleQuiz (idQuiz, pregunta, opciones) VALUES
(1, '¿Qué significa HTML?', 'HyperText Markup Language | High Tech Modern Language | Home Tool Markup Language | Hyperlinks Text Mode Language'),
(1, '¿Qué etiqueta se usa para crear un párrafo?', '<p> | <paragraph> | <text> | <par>'),
(2, '¿Cuál es la forma correcta de declarar una variable en JavaScript moderno?', 'let x = 5; | var x = 5; | variable x = 5; | x := 5;'),
(2, '¿Qué método se usa para imprimir en la consola?', 'console.log() | print() | System.out.println() | echo()'),
(3, '¿Qué dimensión tiene la matriz identidad?', 'n x n | 1 x n | n x 1 | No tiene dimensión fija'),
(4, '¿Cómo se declara una lista en Python?', 'mi_lista = [1, 2, 3] | mi_lista = (1, 2, 3) | mi_lista = {1, 2, 3} | mi_lista = array(1, 2, 3)');

-- Insertar resultados de quiz
INSERT INTO ResultadoQuiz (idUsuario, idQuiz, puntuacion) VALUES
(4, 1, 90.00), -- Laura en quiz de HTML
(4, 2, 85.50), -- Laura en quiz de JavaScript
(5, 1, 75.00), -- David en quiz de HTML
(5, 3, 82.25); -- David en quiz de Álgebra

-- Insertar historial de vídeos
INSERT INTO HistorialVideo (idUsuario, idVideo) VALUES
(4, 1), -- Laura vio el vídeo de HTML
(4, 2), -- Laura vio el vídeo de CSS
(4, 3), -- Laura vio el vídeo de JavaScript
(5, 1), -- David vio el vídeo de HTML
(5, 4); -- David vio el vídeo de matrices