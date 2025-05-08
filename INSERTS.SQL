-- Insertar roles
INSERT INTO Rol (idRol, nombre) VALUES 
(1, 'Administrador'),
(2, 'Profesor'),
(3, 'Alumno');

-- Insertar dificultades
INSERT INTO Dificultad (idDificultad, nombre, descripcion) VALUES 
(1, 'Principiante', 'Para alumnos que están comenzando con la materia'),
(2, 'Intermedio', 'Para alumnos con conocimientos básicos de la materia'),
(3, 'Avanzado', 'Para alumnos con conocimientos sólidos de la materia'),
(4, 'Experto', 'Para alumnos con dominio completo de la materia');

-- Insertar preferencias
INSERT INTO Preferencias (idPreferencia, colorFondo, colorBordes, imagenFondo) VALUES 
(1, '#f5f5f5', '#3498db', 'https://images.unsplash.com/photo-1557683316-973673baf926'),
(2, '#ffffff', '#2ecc71', 'https://images.unsplash.com/photo-1558591710-4b4a1ae0f04d'),
(3, '#f0f8ff', '#9b59b6', 'https://images.unsplash.com/photo-1557682250-62937dbace77');

-- Insertar usuarios
INSERT INTO Usuario (idUsuario, avatar, nombre, apellidos, gmail, telefono, contraseña, idRol, idPreferencia) VALUES 
-- Admin
(1, 'https://randomuser.me/api/portraits/women/22.jpg', 'Ana', 'García López', 'admin@educaplus.com', '612345678', '$2a$12$5JWCDw4I6OYz4wOcKjkNbe9CZ4SuIBVWR5HUQvfgKqKXANzFsLOSq', 1, 1),

-- Profesores
(2, 'https://randomuser.me/api/portraits/men/42.jpg', 'Carlos', 'Rodríguez Martínez', 'carlos.rodriguez@educaplus.com', '623456789', '$2a$12$p9ACqN48VK.H.C/Bl59x6.wpTbfuLmXOSRgUK7asqFJOKK4O5BXUC', 2, 2),
(3, 'https://randomuser.me/api/portraits/women/32.jpg', 'Laura', 'Fernández Sánchez', 'laura.fernandez@educaplus.com', '634567890', '$2a$12$jOl6Q.c3e2ZEA3Upwx3W7.U0vMJ.L.YPmH5PVavrJfR6D.BSs7F/6', 2, 3),
(4, 'https://randomuser.me/api/portraits/men/52.jpg', 'Miguel', 'Torres Ruiz', 'miguel.torres@educaplus.com', '645678901', '$2a$12$0wkVTNf.FvyvVJRD3WYyguGcLvN8TZvb3J2IK.0/UupYwsLOEsVoO', 2, 1),

-- Alumnos
(5, 'https://randomuser.me/api/portraits/women/12.jpg', 'Sara', 'Martín Gómez', 'sara.martin@alumno.edu', '656789012', '$2a$12$CQE3V22uiP3FyZ7l.Y6BnODQadDFZ.QEWRqL8MbY0Y4xLyPjZ5qQ2', 3, 2),
(6, 'https://randomuser.me/api/portraits/men/18.jpg', 'David', 'López Vega', 'david.lopez@alumno.edu', '667890123', '$2a$12$MIQ8L8qlH3PuQAL4xSBgC.pOsYJDTBW9KRKGfVf9mX9VH2jDf7yg2', 3, 3),
(7, 'https://randomuser.me/api/portraits/women/25.jpg', 'Elena', 'Moreno Díaz', 'elena.moreno@alumno.edu', '678901234', '$2a$12$aUYdGUwpP.iF1G/j5E6RLuTMQzQIZbKR3D3XKTlHFg9YKOE0f9Oy.', 3, 1),
(8, 'https://randomuser.me/api/portraits/men/28.jpg', 'Javier', 'Sánchez Navarro', 'javier.sanchez@alumno.edu', '689012345', '$2a$12$qTgRWnqS0Ic9MK3.LmCXmubxoD8Xvb40E3SJpxH7aQjE1qqHrCFIm', 3, 2),
(9, 'https://randomuser.me/api/portraits/women/36.jpg', 'María', 'Jiménez Castro', 'maria.jimenez@alumno.edu', '690123456', '$2a$12$NpU8xDDZ9Ul82F8OYrUWSeVgRJmxu.M9VtOxHG3P1tLEZmqV.sEmi', 3, 3),
(10, 'https://randomuser.me/api/portraits/men/33.jpg', 'Pablo', 'González Ortiz', 'pablo.gonzalez@alumno.edu', '601234567', '$2a$12$RjFSv3dxZvD8v9YcVF5KnOLPxvdJBfH7hJYe0FGgIYf1otDT9yj8O', 3, 1);

-- Seguimiento de alumnos a profesores (mantengo la clave compuesta como está)
INSERT INTO Seguimiento (idAlumno, idProfesor) VALUES 
(5, 2), -- Sara sigue a Carlos
(5, 3), -- Sara sigue a Laura
(6, 2), -- David sigue a Carlos
(6, 4), -- David sigue a Miguel
(7, 3), -- Elena sigue a Laura
(7, 4), -- Elena sigue a Miguel
(8, 2), -- Javier sigue a Carlos
(9, 3), -- María sigue a Laura
(10, 4); -- Pablo sigue a Miguel

-- Insertar cursos
INSERT INTO Curso (idCurso, nombre, imagen, descripcion, fechaCreacion) VALUES 
(1, 'Programación Avanzada', 'https://images.unsplash.com/photo-1498050108023-c5249f4df085', 'Curso completo de programación con los lenguajes más demandados', '2024-01-10 09:00:00'),
(2, 'Diseño Gráfico Digital', 'https://images.unsplash.com/photo-1558655146-d09347e92766', 'Aprende las técnicas más actuales en diseño digital', '2024-01-15 10:30:00'),
(3, 'Marketing Digital', 'https://images.unsplash.com/photo-1460925895917-afdab827c52f', 'Estrategias efectivas para el marketing en plataformas digitales', '2024-01-20 14:00:00');

-- Relación usuarios-cursos (mantengo la clave compuesta como está)
INSERT INTO Usuario_Curso (idUsuario, idCurso) VALUES 
-- Profesores asignados a cursos
(2, 1), -- Carlos - Programación
(3, 2), -- Laura - Diseño
(4, 3), -- Miguel - Marketing
-- Alumnos inscritos en cursos
(5, 1), -- Sara - Programación
(5, 2), -- Sara - Diseño
(6, 1), -- David - Programación
(7, 2), -- Elena - Diseño
(8, 1), -- Javier - Programación
(9, 2), -- María - Diseño
(10, 3); -- Pablo - Marketing

-- Insertar asignaturas
INSERT INTO Asignatura (idAsignatura, nombre, descripcion, imagen, fechaCreacion, idCurso) VALUES 
-- Asignaturas de Programación
(1, 'Java Fundamentals', 'Fundamentos del lenguaje Java y programación orientada a objetos', 'https://images.unsplash.com/photo-1516116216624-53e697fedbea', '2024-01-12 09:30:00', 1),
(2, 'Python para Data Science', 'Aprende a utilizar Python para análisis de datos', 'https://images.unsplash.com/photo-1526379879527-8559ecfcaec0', '2024-01-13 11:00:00', 1),
(3, 'Desarrollo Web Frontend', 'HTML, CSS y JavaScript para crear interfaces web interactivas', 'https://images.unsplash.com/photo-1517180102446-f3ece451e9d8', '2024-01-14 10:00:00', 1),

-- Asignaturas de Diseño Gráfico
(4, 'Adobe Photoshop', 'Domina el editor de imágenes profesional', 'https://images.unsplash.com/photo-1573867639040-6dd25fa5f597', '2024-01-16 09:00:00', 2),
(5, 'Ilustración Digital', 'Creación de ilustraciones con herramientas digitales', 'https://images.unsplash.com/photo-1563206767-5b18f218e8de', '2024-01-17 14:30:00', 2),
(6, 'Diseño UX/UI', 'Principios y prácticas para interfaces centradas en usuario', 'https://images.unsplash.com/photo-1586717791821-3f44a563fa4c', '2024-01-18 13:00:00', 2),

-- Asignaturas de Marketing
(7, 'SEO y SEM', 'Optimización para motores de búsqueda y marketing en buscadores', 'https://images.unsplash.com/photo-1599658880436-c61792e70672', '2024-01-21 10:00:00', 3),
(8, 'Marketing en Redes Sociales', 'Estrategias efectivas para principales plataformas sociales', 'https://images.unsplash.com/photo-1611262588024-d12430b98920', '2024-01-22 15:30:00', 3),
(9, 'Email Marketing', 'Diseño de campañas efectivas de email marketing', 'https://images.unsplash.com/photo-1596526131083-e8c633c948d2', '2024-01-23 11:00:00', 3);

-- Relación usuarios-asignaturas (mantengo la clave compuesta como está)
INSERT INTO Usuario_Asignatura (idUsuario, idAsignatura) VALUES 
-- Profesores asignados a asignaturas
(2, 1), -- Carlos - Java
(2, 2), -- Carlos - Python
(2, 3), -- Carlos - Desarrollo Web
(3, 4), -- Laura - Photoshop
(3, 5), -- Laura - Ilustración Digital
(3, 6), -- Laura - UX/UI
(4, 7), -- Miguel - SEO/SEM
(4, 8), -- Miguel - Redes Sociales
(4, 9), -- Miguel - Email Marketing

-- Alumnos inscritos en asignaturas
(5, 1), -- Sara - Java
(5, 2), -- Sara - Python
(5, 4), -- Sara - Photoshop
(6, 1), -- David - Java
(6, 3), -- David - Desarrollo Web
(7, 4), -- Elena - Photoshop
(7, 5), -- Elena - Ilustración Digital
(8, 2), -- Javier - Python
(8, 3), -- Javier - Desarrollo Web
(9, 4), -- María - Photoshop
(9, 6), -- María - UX/UI
(10, 7), -- Pablo - SEO/SEM
(10, 8); -- Pablo - Redes Sociales

-- Insertar vídeos
INSERT INTO Video (idVideo, titulo, descripcion, url, miniatura, fechaSubida, idAsignatura, idUsuario) VALUES 
-- Videos de Java
(1, 'Introducción a Java', 'Conceptos básicos del lenguaje y su entorno de desarrollo', 'https://example.com/videos/java-intro', 'https://images.unsplash.com/photo-1517694712202-14dd9538aa97', '2024-01-15 10:00:00', 1, 2),
(2, 'Programación Orientada a Objetos en Java', 'Aprende los conceptos de clases, objetos, herencia y polimorfismo', 'https://example.com/videos/java-poo', 'https://images.unsplash.com/photo-1537432376769-00f5c2f4c8d2', '2024-01-16 11:30:00', 1, 2),
(3, 'Collections Framework', 'Uso de List, Set, Map y sus implementaciones', 'https://example.com/videos/java-collections', 'https://images.unsplash.com/photo-1516116216624-53e697fedbea', '2024-01-17 09:45:00', 1, 2),

-- Videos de Python
(4, 'Python para principiantes', 'Introducción al lenguaje Python y sus características', 'https://example.com/videos/python-intro', 'https://images.unsplash.com/photo-1515879218367-8466d910aaa4', '2024-01-15 14:00:00', 2, 2),
(5, 'Análisis de datos con Pandas', 'Manipulación de datos tabulares con Pandas', 'https://example.com/videos/python-pandas', 'https://images.unsplash.com/photo-1551288049-bebda4e38f71', '2024-01-16 15:30:00', 2, 2),
(6, 'Visualización de datos con Matplotlib', 'Creación de gráficos y visualizaciones interactivas', 'https://example.com/videos/python-matplotlib', 'https://images.unsplash.com/photo-1551288049-bebda4e38f71', '2024-01-17 16:00:00', 2, 2),

-- Videos de Desarrollo Web
(7, 'HTML5 Esencial', 'Estructura, semántica y nuevas características de HTML5', 'https://example.com/videos/html5', 'https://images.unsplash.com/photo-1621839673705-6617adf9e890', '2024-01-15 09:00:00', 3, 2),
(8, 'CSS Avanzado y Flexbox', 'Estilos avanzados y diseño responsive con Flexbox', 'https://example.com/videos/css-flexbox', 'https://images.unsplash.com/photo-1507721999472-8ed4421c4af2', '2024-01-16 10:30:00', 3, 2),
(9, 'JavaScript Moderno', 'ES6 y características modernas del lenguaje', 'https://example.com/videos/js-moderno', 'https://images.unsplash.com/photo-1627398242454-45a1465c2479', '2024-01-17 11:00:00', 3, 2),

-- Videos de Photoshop
(10, 'Introducción a Photoshop', 'Interfaz y herramientas básicas', 'https://example.com/videos/photoshop-intro', 'https://images.unsplash.com/photo-1523152737819-9470aff7d077', '2024-01-15 13:00:00', 4, 3),
(11, 'Selecciones y capas', 'Domina las técnicas de selección y organización por capas', 'https://example.com/videos/photoshop-layers', 'https://images.unsplash.com/photo-1611162616475-46b635cb6868', '2024-01-16 14:30:00', 4, 3),
(12, 'Retoque fotográfico profesional', 'Técnicas avanzadas de retoque facial y corporal', 'https://example.com/videos/photoshop-retoque', 'https://images.unsplash.com/photo-1611162616305-c69b3267bc43', '2024-01-17 15:00:00', 4, 3),

-- Videos de Ilustración Digital
(13, 'Dibujo digital con Procreate', 'Introducción a la ilustración en iPad', 'https://example.com/videos/procreate-intro', 'https://images.unsplash.com/photo-1535587566541-97121a128dc5', '2024-01-15 10:00:00', 5, 3),
(14, 'Técnicas de sombreado digital', 'Aprende a dar volumen a tus ilustraciones', 'https://example.com/videos/sombreado-digital', 'https://images.unsplash.com/photo-1619017098958-57b1e2d275e4', '2024-01-16 11:30:00', 5, 3),
(15, 'Composición y color', 'Principios de composición y teoría del color aplicada', 'https://example.com/videos/composicion-color', 'https://images.unsplash.com/photo-1513542789411-b6a5d4f31634', '2024-01-17 12:00:00', 5, 3),

-- Videos de UX/UI
(16, 'Principios de Diseño UX', 'Fundamentos del diseño centrado en usuario', 'https://example.com/videos/ux-principios', 'https://images.unsplash.com/photo-1586717791821-3f44a563fa4c', '2024-01-15 09:30:00', 6, 3),
(17, 'Figma para UI Design', 'Diseño de interfaces con Figma', 'https://example.com/videos/figma-ui', 'https://images.unsplash.com/photo-1618788372246-79faff0c3742', '2024-01-16 10:45:00', 6, 3),
(18, 'Testing de usabilidad', 'Métodos para evaluar la usabilidad de tus diseños', 'https://example.com/videos/testing-usabilidad', 'https://images.unsplash.com/photo-1589578527966-fdac0f44566c', '2024-01-17 11:15:00', 6, 3),

-- Videos de SEO/SEM
(19, 'Fundamentos de SEO', 'Conceptos básicos de optimización para buscadores', 'https://example.com/videos/seo-fundamentos', 'https://images.unsplash.com/photo-1562577309-4932fdd64cd1', '2024-01-15 14:00:00', 7, 4),
(20, 'Análisis de keywords', 'Investigación y selección de palabras clave efectivas', 'https://example.com/videos/keyword-research', 'https://images.unsplash.com/photo-1532622785990-d2c36a76f5a6', '2024-01-16 15:30:00', 7, 4),
(21, 'Campañas de Google Ads', 'Configuración y optimización de campañas SEM', 'https://example.com/videos/google-ads', 'https://images.unsplash.com/photo-1611079830811-ca8c8fd65321', '2024-01-17 16:00:00', 7, 4),

-- Videos de Redes Sociales
(22, 'Estrategia en Instagram', 'Cómo construir una presencia efectiva en Instagram', 'https://example.com/videos/instagram-estrategia', 'https://images.unsplash.com/photo-1611162616305-c69b3267bc43', '2024-01-15 10:30:00', 8, 4),
(23, 'Marketing en TikTok', 'Tácticas para alcanzar audiencias jóvenes', 'https://example.com/videos/tiktok-marketing', 'https://images.unsplash.com/photo-1611605698335-8b1569810432', '2024-01-16 11:45:00', 8, 4),
(24, 'Publicidad en LinkedIn', 'Campañas B2B efectivas en LinkedIn', 'https://example.com/videos/linkedin-ads', 'https://images.unsplash.com/photo-1611944212129-29977ae1398c', '2024-01-17 12:15:00', 8, 4),

-- Videos de Email Marketing
(25, 'Fundamentos de Email Marketing', 'Bases para campañas de email efectivas', 'https://example.com/videos/email-fundamentos', 'https://images.unsplash.com/photo-1596526131083-e8c633c948d2', '2024-01-15 14:30:00', 9, 4),
(26, 'Diseño de emails responsive', 'Técnicas de diseño para múltiples dispositivos', 'https://example.com/videos/email-responsive', 'https://images.unsplash.com/photo-1557200134-90327ee9fafa', '2024-01-16 15:45:00', 9, 4),
(27, 'Segmentación y automatización', 'Personalización avanzada de campañas', 'https://example.com/videos/email-automatizacion', 'https://images.unsplash.com/photo-1523966211575-eb4a01e7dd51', '2024-01-17 16:15:00', 9, 4);

-- Insertar marcadores de video
INSERT INTO MarcadorVideo (idMarcador, idVideo, minutoInicio, minutoFin, titulo) VALUES 
(1, 1, 2.30, 5.45, 'Instalación de JDK'),
(2, 1, 7.15, 9.20, 'Primer Hola Mundo'),
(3, 2, 3.50, 7.25, 'Definición de clases'),
(4, 2, 10.10, 15.30, 'Herencia y polimorfismo'),
(5, 3, 5.20, 8.15, 'ArrayList y LinkedList'),
(6, 3, 12.45, 18.30, 'HashMap y TreeMap'),
(7, 4, 1.30, 4.15, 'Sintaxis básica de Python'),
(8, 8, 6.45, 12.35, 'Estructura semántica HTML5'),
(9, 10, 3.20, 8.40, 'Herramientas de selección'),
(10, 16, 5.50, 9.25, 'Wireframing en Figma'),
(11, 19, 4.15, 8.30, 'Análisis de competencia');

-- Insertar favoritos (mantengo la clave compuesta como está)
INSERT INTO Favorito (idUsuario, idVideo, fecha) VALUES 
(5, 1, '2024-01-18 09:30:00'),
(5, 2, '2024-01-18 10:15:00'),
(6, 1, '2024-01-18 14:00:00'),
(6, 7, '2024-01-18 15:30:00'),
(7, 10, '2024-01-18 11:00:00'),
(8, 4, '2024-01-18 16:45:00'),
(9, 13, '2024-01-18 13:15:00'),
(10, 19, '2024-01-18 10:45:00');

-- Insertar comentarios en videos
INSERT INTO ComentarioVideo (idComentario, idUsuario, idVideo, texto, fecha) VALUES 
-- Comentarios en videos de Java
(1, 5, 1, 'Excelente introducción, me ha ayudado mucho a entender los conceptos básicos. ¿Habrá una segunda parte sobre entornos de desarrollo?', '2024-01-19 10:15:00'),
(2, 6, 1, 'Muy clara la explicación sobre la JVM. Por fin entiendo cómo funciona Java por debajo.', '2024-01-19 11:30:00'),
(3, 8, 1, 'Me encantó el video. ¿Podrías recomendar algún IDE específico para principiantes?', '2024-01-20 09:45:00'),
(4, 2, 1, 'Gracias por vuestros comentarios. @Sara, sí, habrá una segunda parte enfocada en Eclipse y IntelliJ. @Javier, para principiantes recomiendo VS Code con las extensiones de Java.', '2024-01-20 14:30:00'),

(5, 5, 2, 'Los ejemplos de herencia son muy claros. Me ha costado entender el polimorfismo pero con tus explicaciones lo he comprendido mejor.', '2024-01-21 10:00:00'),
(6, 5, 2, 'Los ejemplos de herencia son muy claros. Me ha costado entender el polimorfismo pero con tus explicaciones lo he comprendido mejor.', '2024-01-21 10:00:00'),
(7, 6, 2, 'Gran video. ¿Podrías profundizar en interfaces y clases abstractas en otro video?', '2024-01-21 16:15:00'),
(8, 2, 2, 'Claro, David. Ya estoy preparando un video específico sobre interfaces y clases abstractas que publicaré la próxima semana.', '2024-01-22 09:30:00'),

-- Comentarios en videos de Python
(9, 5, 4, 'Vengo de Java y me está costando adaptarme a la indentación de Python. ¿Algún consejo?', '2024-01-23 11:45:00'),
(10, 8, 4, 'La explicación sobre las listas y diccionarios fue excelente. Es mucho más intuitivo que en otros lenguajes.', '2024-01-23 15:00:00'),
(11, 2, 4, 'Sara, te recomiendo configurar tu editor para que muestre los espacios en blanco, así te resultará más fácil visualizar la indentación. También puedes usar PyCharm que tiene buenas herramientas para esto.', '2024-01-24 09:15:00'),

-- Comentarios en videos de Desarrollo Web
(12, 6, 7, 'Por fin entiendo la diferencia entre las etiquetas semánticas. Muy claro tu ejemplo de estructura de página.', '2024-01-25 10:30:00'),
(13, 8, 7, 'Excelente explicación sobre los formularios en HTML5. ¿Podrías hacer un video sobre validación de formularios con JavaScript?', '2024-01-25 14:45:00'),
(14, 2, 7, 'Gracias por la sugerencia, Javier. Añadiré un video sobre validación de formularios a la lista de próximos contenidos.', '2024-01-26 11:00:00'),

-- Comentarios en videos de Photoshop
(15, 7, 10, 'La técnica de selección por canales alfa me ha salvado la vida. Nunca había podido hacer selecciones tan precisas de cabello.', '2024-01-27 09:45:00'),
(16, 9, 10, 'Increíble tutorial. ¿Funcionan estas técnicas igual en la versión 2023 de Photoshop?', '2024-01-27 13:30:00'),
(17, 3, 10, 'María, sí, estas técnicas funcionan perfectamente en la versión 2023 e incluso han mejorado algunas herramientas de selección automática.', '2024-01-28 10:15:00'),

-- Comentarios en videos de Ilustración Digital
(18, 7, 13, 'Los atajos de teclado que mostraste son muy útiles. He aumentado mi productividad enormemente.', '2024-01-29 11:30:00'),
(19, 9, 13, 'Las configuraciones de pinceles son geniales. ¿Podrías compartir tu set de pinceles personalizados?', '2024-01-29 15:45:00'),
(20, 3, 13, 'Claro María, he subido mi pack de pinceles a la plataforma. Puedes descargarlo desde la sección de recursos de la asignatura.', '2024-01-30 09:00:00'),

-- Comentarios en videos de Marketing
(21, 10, 19, 'Las estrategias de contenido para Instagram son muy actuales. He implementado el calendario de publicaciones y ya estoy viendo resultados.', '2024-01-31 10:30:00'),
(22, 4, 19, 'Me alegra mucho escuchar eso, Pablo. Recuerda que la consistencia es clave en Instagram. Mantén ese calendario de publicaciones.', '2024-01-31 14:15:00'),
(23, 10, 19, 'Definitivamente. También estoy aplicando las técnicas de hashtags y la interacción ha aumentado un 30%.', '2024-02-01 09:45:00');

-- Insertar historial de visualizaciones
INSERT INTO HistorialVideo (idHistorial, idUsuario, idVideo, fechaVisualizacion) VALUES 
(1, 5, 1, '2024-01-18 09:00:00'),
(2, 5, 2, '2024-01-18 10:00:00'),
(3, 5, 3, '2024-01-18 11:00:00'),
(4, 5, 4, '2024-01-19 09:30:00'),
(5, 6, 1, '2024-01-18 14:00:00'),
(6, 6, 7, '2024-01-18 15:00:00'),
(7, 6, 8, '2024-01-19 14:30:00');