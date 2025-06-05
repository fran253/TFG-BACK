-- IMPORTANTE LEER ANTES DE INSERTAR DATOS
-- Estos inserts estan en orden de ejecucion desde Dbeaver nunca nos deja ejecutarlos todos a la vez asi que siempre los ejecutamos uno por uno
-- Muchas tablas tienen el campo id originalmente en autoincremental, en el rds esta asi, lo que hace que cada vez que alguien despliegue desde local se renueven id de usuarios por ejemplo y no se inserten bien los datos de tablas que dependen de dichos id
-- por lo tanto hemos forzado id fijos de primeras y por ultimo un comando que hace que los campos vuelvan a ser autoincrementales y asi tienes los datos correctos pero si añades cosas salen con id autoincremental


-- 1. PRIMERO: Tabla Rol (sin dependencias)
INSERT INTO Rol (idRol,nombre) VALUES
	 (3,'Administrador'),
	 (1,'Alumno'),
	 (2,'Profesor');

-- 2. SEGUNDO: Tabla Usuario (depende de Rol) - CON IDs ESPECÍFICOS
INSERT INTO Usuario (idUsuario,avatar,nombre,apellidos,gmail,telefono,contraseña,idRol,CursosSeguidos,Token) VALUES
	 (1,'avatar4.png','Usuario','usuariez','usuariez@example.com','666777999','hash_contraseña_segura4',2,'',NULL),
	 (2,NULL,'Einar','el guay','einar@gmail.com','123456789','123456',1,'',NULL),
	 (22,NULL,'balatro',NULL,'balatrobalatrez@gmail.com','666888999','ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f',2,'','25364507-2bd9-4d2c-986d-69df7fb1ee11'),
	 (23,NULL,'balatrero',NULL,'balatringutierrez@gmail.com','666888999','ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f',2,NULL,NULL),
	 (29,NULL,'fran','rebollo','franrebollo@gmail.com','123456789','ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f',3,NULL,'393a7446-1440-406e-9044-cf5de1532362'),
	 (30,NULL,'balatro','balatrini','balatringutierrez33@gmail.com','666888999','ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f',2,NULL,'18711306-c39a-400d-85d3-91e23c29ed50'),
	 (31,NULL,'Usuario Test',NULL,'test@example.com',NULL,'123456',1,NULL,NULL),
	 (32,NULL,'Carlos','Carlez','carlos.prof@example.com','666555666','ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f',1,NULL,'9b8ecfb3-39ef-4305-aa43-8ca565744a47'),
	 (33,NULL,'ejemplo','ejemplo','ejemploejemplez@gmail.com','123456768','ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f',2,NULL,'8ffa5b2a-738c-45fe-87d0-9b303e7d1cb1'),
	 (34,NULL,'Judini','Cesar','judini@gmail.com','683512911','sin_cambio',1,NULL,NULL);

INSERT INTO Usuario (idUsuario,avatar,nombre,apellidos,gmail,telefono,contraseña,idRol,CursosSeguidos,Token) VALUES
	 (35,NULL,'diego@gmail.com',NULL,'diego@gmail.com','5453254654','c0f9fda42308017c288314b350117d494ecac53201ead588de3076ae6df4e26b',3,NULL,'82a810c9-9a6b-432f-b306-c0d6dc702f0b'),
	 (36,NULL,'einar','el colega','einar12@gmail.com','123456789','ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f',1,NULL,'43aac478-cc1f-4e57-b7ba-2121bd1e8eba'),
	 (37,NULL,'Nicolas ','','Ferrial@gmail.com','','744b6f384ae8f75a7674bd8f32e8cc72004a9b1b9f451589686cead88378aa42',1,NULL,'572279c7-e897-4d7b-b90f-8d4c3b392d98'),
	 (38,NULL,'Stańczyk','Sin Apellido','Stańczyk@gmail.com','788 67 67 87','41cf724e1b5bcbecedad0f2b78494c9146d41bb91cd0a224529f8ef853ad3b80',1,NULL,'6685b3c5-bb4b-4d7a-83c1-9a88e3ebbe06'),
	 (39,NULL,'Jane','Foole','Jane@gmail.com','893 89 09 89','fb838ea1b6df3cbf884a613387e2262df027552c94cc9e9e2f7573f2873b766b',2,NULL,'fc949104-c80e-4586-b453-13375ea98c1b'),
	 (40,NULL,'anton','antonez','antonantonez@gmail.com','123456789','ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f',1,NULL,'7e0a8b98-01ba-4c93-aaa2-dbc57b7c384d'),
	 (41,NULL,'Sofie','Reyes','Sofi@gmail.com','678 09 58 89','141b7bed25e12b51889944ca8dc4af5b216257547f692a642196f1aa7b64beba',1,NULL,'0e641a03-0f8d-4c8f-b884-45bd85e92851'),
	 (42,NULL,'balatro','balatrez','balatrobalatrez22@gmail.com','123456789','ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f',1,NULL,'9e5c9a6b-6fd4-4534-9cd5-fc64a8dd0fdd'),
	 (43,NULL,'fran','rebo','franrebo2004@gmail.com','123456789','ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f',2,NULL,'db5fb77c-2f9f-45c7-9321-58e1b58f7d8e'),
	 (44,NULL,'hola','holez','holaholez@gmail.com','123456789','ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f',1,NULL,'39d5a958-172d-4da3-a4b7-2fe313d7f2e1');

INSERT INTO Usuario (idUsuario,avatar,nombre,apellidos,gmail,telefono,contraseña,idRol,CursosSeguidos,Token) VALUES
	 (45,NULL,'Dammian','Piezcochib','Dam811ian@gmail.com','897 89 89 98','72921590a2eadf2f54f72648c92828672ae860e17f464fe3be45c28b1984cc77',1,NULL,'87f796b0-1aa6-4a0d-aa24-fba944ad9ab4'),
	 (46,NULL,'William  JR','Dafoe','WilliamJR@gmail.com','8964895493','57d119816e51009df8f7a64002d67ddf7d49b242883b6a3df13a024eeea94eeb',1,NULL,'1b8b8eeb-074a-475a-a571-0a9bdc918ce3'),
	 (47,NULL,'SUPER','MARIO','MARIO@gmail.com','7890457896','82c71d4e882ea1778639c30d14465f9b55eee99c7cae9f24e50b28db752ebb87',1,NULL,'a6813021-decc-4ba0-b5ce-fe06bef09bc5'),
	 (48,NULL,'balatro','balatrez','balatrobalatrez@balatro.com','123456789','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92',1,NULL,'835124a8-9fd7-4e79-9a87-c55d4ffb5409'),
	 (49,NULL,'paco','hernandez','pacohernandez@gmail.com','123456789','ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f',1,NULL,'4fd277e1-4dc5-40d0-ba76-c5654108af06'),
	 (50,NULL,'tu','tu','tu@gmail.com','1111111111','ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f',1,NULL,'c66c0e39-9f68-48ef-bdf6-78c32605d957'),
	 (55,NULL,'Pepito','Halzador de Esquirlas','enverdadelcpestabien@vivaelcp.com','+34 000 000','bcba9b04bee61cca8c6c014fdaf2b2e7ecf69ff3fe0b4678da85bcdd128f2649',1,NULL,'7f93ea4a-6611-4b6c-930c-a8c5dd71a906');

-- 3. TERCERO: Tabla Curso (depende de Usuario) - CON IDs ESPECÍFICOS
INSERT INTO Curso (idCurso,nombre,imagen,descripcion,fechaCreacion,idUsuario) VALUES
	 (16,'AWS','https://archivos-academiq.s3.amazonaws.com/cursos/aws-servicios-principales.jpg','Curso de AWS donde se suben videos desde crear un EC2 hasta subir imágenes a S3.','2025-05-26 14:24:13',29),
	 (17,'BBDD','https://archivos-academiq.s3.amazonaws.com/cursos/Bases-de-datos3.jpg','Curso de BBDD donde se aprenderá lo básico y esencial','2025-05-27 13:57:40',29),
	 (18,'Vinland','https://archivos-academiq.s3.amazonaws.com/cursos/Descargar fondos de pantalla 4k, Alpes, prados, montañas, verano, Alemania, Europa libre_ Imágenes fondos de descarga gratuita.jfif','hola','2025-05-27 15:41:51',29),
	 (24,'La Programación Orientada a Objetos (POO)','https://archivos-academiq.s3.amazonaws.com/cursos/1366_2000.jpg','La programación orientada a objetos es un modelo de programación en el que el diseño de software se organiza alrededor de datos u objetos, en vez de usar funciones y lógica. Se enfoca en los objetos que los programadores necesitan manipular, en lugar de centrarse en la lógica necesaria para esa manipulación.','2025-05-30 23:56:39',45),
	 (28,'Calculo Avanzado Matematico','https://archivos-academiq.s3.amazonaws.com/cursos/un-genio-matemtico-en-espera-de-biografa.jpg','Aprende calculo avanzado como un autentico profesional','2025-06-01 10:17:50',35),
	 (29,'Hola','https://archivos-academiq.s3.amazonaws.com/cursos/Captura de pantalla 2025-05-29 123443.png','hola','2025-06-04 08:02:27',29);

-- 4. CUARTO: Tabla Asignatura (depende de Curso) - CON IDs ESPECÍFICOS  
INSERT INTO Asignatura (idAsignatura,nombre,descripcion,imagen,fechaCreacion,idCurso) VALUES
	 (15,'Contenedores','',NULL,'2025-05-26 14:25:06',16),
	 (16,'EC2','',NULL,'2025-05-26 14:25:14',16),
	 (17,'RDS','',NULL,'2025-05-26 14:25:18',16),
	 (18,'Security Groups','',NULL,'2025-05-26 14:25:27',16),
	 (19,'MySQL','',NULL,'2025-05-27 13:57:48',17),
	 (20,'Aurora DB','',NULL,'2025-05-27 13:57:56',17),
	 (21,'PostGreSQL','',NULL,'2025-05-27 13:58:12',17),
	 (22,'vinland 1','',NULL,'2025-05-27 15:41:58',18),
	 (23,'Vinland 2','',NULL,'2025-05-27 15:42:03',18),
	 (30,'Clases y Objetos','',NULL,'2025-05-30 23:57:13',24);

INSERT INTO Asignatura (idAsignatura,nombre,descripcion,imagen,fechaCreacion,idCurso) VALUES
	 (31,'Encapsulamiento','',NULL,'2025-05-30 23:57:26',24),
	 (32,'Herencia','',NULL,'2025-05-30 23:57:35',24),
	 (33,'Abstracción','',NULL,'2025-05-30 23:57:40',24),
	 (36,'Matematicas','',NULL,'2025-06-01 10:18:00',28),
	 (37,'Logistica Factorial','',NULL,'2025-06-01 10:18:15',28),
	 (38,'robotin','',NULL,'2025-06-04 08:02:47',29);

-- 5. QUINTO: Tabla Video (depende de Asignatura, Usuario y Curso) - CON IDs ESPECÍFICOS
INSERT INTO Video (idVideo,titulo,descripcion,duracion,url,miniatura,fechaSubida,idAsignatura,idUsuario,numReportes,idCurso,ContadorLikes) VALUES
	 (64,'Amazon S3 Static Web Site','GitHub https://github.com/santos-pardos/Hand...','','https://archivos-academiq.s3.amazonaws.com/video/S3 Static Web.mp4','https://archivos-academiq.s3.amazonaws.com/miniatura/1_Qz8qHl2TEWNK_1rmPZ-t-A.jpg','2025-05-26 17:44:39',15,30,0,16,2),
	 (65,'EC2','Aws ec2 explicacion como montar una instancia','','https://archivos-academiq.s3.amazonaws.com/video/EC2 Linux Apache.mp4','https://archivos-academiq.s3.amazonaws.com/miniatura/1_rwAyCH8oA4BmpJojOu8nXA.png','2025-05-26 20:21:27',16,29,0,16,0),
	 (66,'mySQL','Introduccion a MySQL','','https://archivos-academiq.s3.amazonaws.com/video/¡ULTRAS-DEL-ESPANYOL-SALTAN-AL-CAMPO-Y-PERSIGUEN-A-06f71387-235c-4844-9d6f-f5bb116f957c.mp4','https://archivos-academiq.s3.amazonaws.com/miniatura/Bases-de-datos3.jpg','2025-05-27 16:26:03',19,29,0,17,0),
	 (67,'r','r','','https://archivos-academiq.s3.amazonaws.com/video/¡ULTRAS DEL ESPANYOL SALTAN AL CAMPO Y PERSIGUEN A LOS JUGADORES DEL BARÇA! (1).mp4','https://archivos-academiq.s3.amazonaws.com/miniatura/aaaa.PNG','2025-05-27 18:31:28',23,29,3,18,1),
	 (69,'vagabundo','vagabundez','','https://archivos-academiq.s3.amazonaws.com/video/¡ULTRAS-DEL-ESPANYOL-SALTAN-AL-CAMPO-Y-PERSIGUEN-A-06f71387-235c-4844-9d6f-f5bb116f957c.mp4','https://archivos-academiq.s3.amazonaws.com/miniatura/Captusssssssssra.PNG','2025-05-27 20:01:52',15,29,3,16,0),
	 (101,'Inicialización a la matemática avanzada','Inicialización a la matemática avanzada','','https://archivos-academiq.s3.amazonaws.com/video/ssstwitter.com_1747222672312.mp4','https://archivos-academiq.s3.amazonaws.com/miniatura/images.png','2025-06-01 17:09:13',36,35,7,28,0),
	 (103,'cine','cine','','https://archivos-academiq.s3.amazonaws.com/video/¡ULTRAS DEL ESPANYOL SALTAN AL CAMPO Y PERSIGUEN A LOS JUGADORES DEL BARÇA!.mp4','https://archivos-academiq.s3.amazonaws.com/miniatura/Capturassss.PNG','2025-06-03 15:27:14',22,29,0,18,1),
	 (105,'AWSado','awsado','','https://archivos-academiq.s3.amazonaws.com/video/S3 Static Web.mp4','https://archivos-academiq.s3.amazonaws.com/miniatura/pexels-pixabay-301920.jpg','2025-06-04 19:30:49',16,29,1,16,2);

-- 6. SEXTO: Tablas de relación Usuario-Curso
INSERT INTO Usuario_Curso (idUsuario,idCurso) VALUES
	 (22,16),
	 (29,16),
	 (35,16),
	 (40,16),
	 (45,16),
	 (29,17),
	 (40,17),
	 (40,18);

-- 7. SÉPTIMO: Quiz (depende de Usuario, Curso y Asignatura) - CON IDs ESPECÍFICOS
INSERT INTO Quiz (IdQuiz,Nombre,Descripcion,IdUsuario,FechaCreacion,IdCurso,IdAsignatura) VALUES
	 (10,'Rodriguez','asd',22,'2025-05-25 19:40:49',16,16),
	 (11,'supra','supra',22,'2025-05-25 20:12:22',16,16),
	 (12,'supra','supra',22,'2025-05-25 20:12:37',16,16),
	 (13,'ejemplo','ejemplo',22,'2025-05-25 20:16:09',16,16),
	 (14,'asda','sd',22,'2025-05-25 20:20:53',16,16),
	 (15,'Prueba sobre BALATRO','BALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATRO',22,'2025-05-26 00:15:59',16,15),
	 (20,'Base de Datos','Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos ',22,'2025-05-26 15:26:19',17,15),
	 (26,'MySQL quiz','',29,'2025-05-27 16:38:57',17,19),
	 (28,'Aurora DB','',29,'2025-05-27 16:49:09',17,20),
	 (30,'LEY Y ORDEN','Ley Y orden de tu pais bobo',35,'2025-05-27 21:44:02',NULL,NULL);

INSERT INTO Quiz (IdQuiz,Nombre,Descripcion,IdUsuario,FechaCreacion,IdCurso,IdAsignatura) VALUES
	 (36,'23333','3',29,'2025-05-28 20:39:07',16,18),
	 (37,'Holaaa','',45,'2025-05-29 19:03:13',16,17),
	 (38,'Conocimientos Basicos de POO','Un quizz con datos básicos sobre la programación orientada a objetos',45,'2025-05-31 02:10:49',24,30),
	 (39,'Matematica avanzada quizz','Matematica avanzada quizz',35,'2025-06-01 12:21:27',28,36),
	 (40,'21122','122121',29,'2025-06-03 10:58:08',16,15),
	 (41,'pizza','',29,'2025-06-04 08:03:47',29,38),
	 (42,'12354','',29,'2025-06-04 15:17:46',16,17),
	 (43,'etttt','we',29,'2025-06-04 15:21:52',29,38),
	 (44,'prueba','',29,'2025-06-04 15:23:05',29,38),
	 (45,'robot','',29,'2025-06-04 15:27:34',29,38);

INSERT INTO Quiz (IdQuiz,Nombre,Descripcion,IdUsuario,FechaCreacion,IdCurso,IdAsignatura) VALUES
	 (46,'por favor','',29,'2025-06-04 15:29:55',29,38),
	 (47,'vinland 1','sdsdsd',29,'2025-06-04 15:31:41',29,38),
	 (48,'TEST','TEST',35,'2025-06-05 08:23:48',28,36);

-- 8. OCTAVO: Pregunta (depende de Quiz) - CON IDs ESPECÍFICOS
INSERT INTO Pregunta (IdPregunta,IdQuiz,Descripcion,Orden) VALUES
	 (7,10,'BALATROBALATROBALATRO ESTILO DE VIDA?',1),
	 (8,10,'BALATROBALATROBALATROBALATRO',2),
	 (9,10,'BALATROBALATROBALATRO',3),
	 (10,11,'Base de Datos',1),
	 (11,12,'Te gusta  mysql?',1),
	 (12,13,'Te gusta aurora DB?',1),
	 (13,13,'Te gusta bbdd?',2),
	 (15,20,'LEY Y ....',1),
	 (22,26,'23233232',1),
	 (24,28,'heeyyyy',1);

INSERT INTO Pregunta (IdPregunta,IdQuiz,Descripcion,Orden) VALUES
	 (26,30,'¿Qué es una clase?',1),
	 (32,36,'Te gusta la Matematica avanzada quizz',1),
	 (33,37,'12212121',1),
	 (34,38,'peperoni',1),
	 (35,39,'1234',1),
	 (36,40,'ewewew',1),
	 (37,41,'qwwqwqw',1),
	 (38,42,'robot?',1),
	 (39,43,'weewew',1),
	 (40,44,'sdsdsd',1);

INSERT INTO Pregunta (IdPregunta,IdQuiz,Descripcion,Orden) VALUES
	 (41,45,'TESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTEST',1);

-- 9. NOVENO: Respuesta (depende de Pregunta) - CON IDs ESPECÍFICOS
INSERT INTO Respuesta (IdRespuesta,IdPregunta,Texto,EsCorrecta,Orden) VALUES
	 (1,7,'BALATRO SUPREME',1,1),
	 (2,7,'BALATRO SUPER BOWL',0,2),
	 (3,7,'BALATRO MEGA NIGGER',0,3),
	 (4,8,'BALATRO',0,1),
	 (5,8,'BALATRO',0,2),
	 (6,8,'BALATRO',0,3),
	 (7,8,'BALATRO',1,4),
	 (8,9,'BALATROBALATRO',1,1),
	 (9,9,'BALATRO',0,2),
	 (10,10,'Base de Datos',0,1);

INSERT INTO Respuesta (IdRespuesta,IdPregunta,Texto,EsCorrecta,Orden) VALUES
	 (11,10,'Base de Datos',0,2),
	 (12,10,'Base de Datos',0,3),
	 (13,10,'Base de Datos',1,4),
	 (14,11,'si',1,1),
	 (15,11,'no',0,2),
	 (16,12,'si',1,1),
	 (17,12,'no',0,2),
	 (18,13,'no',0,1),
	 (19,13,'por supuesto',1,2),
	 (20,15,'Orden',1,1);

INSERT INTO Respuesta (IdRespuesta,IdPregunta,Texto,EsCorrecta,Orden) VALUES
	 (21,15,'Caos',0,2),
	 (22,22,'23',1,1),
	 (23,22,'3',0,2),
	 (24,24,'3',1,1),
	 (25,24,'2',0,2),
	 (26,26,'Una función especial',1,1),
	 (27,26,'Una estructura que define atributos y métodos',0,2),
	 (28,32,'S',1,1),
	 (29,32,'N',0,2),
	 (30,33,'1',1,1);

INSERT INTO Respuesta (IdRespuesta,IdPregunta,Texto,EsCorrecta,Orden) VALUES
	 (31,33,'3',0,2),
	 (32,34,'no',1,1),
	 (33,34,'si',0,2),
	 (34,35,'123',1,1),
	 (35,35,'233',0,2),
	 (36,36,'weewew',1,1),
	 (37,36,'ewweewewewewewewweew',0,2),
	 (38,37,':wq',1,1),
	 (39,37,':uuuu',0,2),
	 (40,38,'si',1,1);

INSERT INTO Respuesta (IdRespuesta,IdPregunta,Texto,EsCorrecta,Orden) VALUES
	 (41,38,'no',0,2),
	 (42,39,'1',1,1),
	 (43,39,'2',0,2),
	 (44,40,'sddsad',1,1),
	 (45,40,'adadad',0,2),
	 (46,41,'NO THAT IS NOT SOLID SNAKE',1,1),
	 (47,41,'NIGRO THAT IS SOLID SNAKE',0,2);

-- 10. DÉCIMO: Resto de tablas (que dependen de Video y Usuario)
INSERT INTO ComentarioVideo (idComentario,idUsuario,idVideo,texto,fecha,NumeroReportes) VALUES
	 (1,30,64,'Aquí tenéis el video que tanto me habéis pedido','2025-05-26 15:47:39',0),
	 (2,35,64,'Videazo','2025-05-26 16:30:17',0),
	 (3,35,66,'REPORTADO NO ES DE MYSQL','2025-05-27 14:28:05',0),
	 (4,35,67,'ES CLICK  BAIT ! ESTO NO ES UNA R AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA','2025-05-27 16:41:18',0),
	 (5,29,66,'pero quien ha subido esto!!!!','2025-05-27 17:02:40',0),
	 (6,35,69,'OTRO QUE TAMPOCO ES SOBRE AWS REPORTADO','2025-05-27 19:24:26',0),
	 (7,29,69,'este canal...','2025-06-03 10:57:23',0),
	 (8,40,64,'Hola soy YO!','2025-06-04 08:04:12',0),
	 (9,40,101,'Test','2025-06-04 08:10:48',0),
	 (10,35,65,'Buenos dias modesto','2025-06-04 08:44:47',0);

INSERT INTO ComentarioVideo (idComentario,idUsuario,idVideo,texto,fecha,NumeroReportes) VALUES
	 (11,40,65,'Muy buenos dias gente!','2025-06-04 08:45:12',0),
	 (12,55,105,'Video bastante efectivo, muchas gracias','2025-06-05 08:12:32',0),
	 (13,55,67,'THAT IS NOT SOLID SNAKE!!!!1!','2025-06-05 08:14:56',0);

INSERT INTO Favorito (idUsuario,idVideo) VALUES
	 (29,64),
	 (29,67),
	 (29,103),
	 (29,105),
	 (55,105);

INSERT INTO MarcadorVideo (idMarcador,idVideo,minutoImportante,titulo) VALUES
	 (1,64,55.48,'Inicio del video'),
	 (2,64,88.30,'Creación de bucket'),
	 (3,64,144.71,'Explicación y github'),
	 (4,64,318.79,'Configuración del bucket'),
	 (5,65,130.05,'inicio'),
	 (6,65,226.94,'Creación Instancia'),
	 (7,65,834.51,'añadir reglas al Security Group'),
	 (8,65,999.99,'Mas configuración'),
	 (9,101,0.00,'Esto no es realmente matemática avanzada'),
	 (10,101,5.03,'Morritos en el minuto 00:05');

INSERT INTO MarcadorVideo (idMarcador,idVideo,minutoImportante,titulo) VALUES
	 (11,105,106.68,'eeee'),
	 (12,105,272.16,'rerrere');

INSERT INTO PeticionProfesor (Id,idUsuario,DocumentacionUrl,Texto,FechaPeticion) VALUES
	 (1,22,'https://tu-servidor.com/uploads/2193459a-9ee7-4ebe-ac5f-09fa65aa6989.png','quiero ser profesorquiero ser profesorquiero ser profesorquiero ser profesorquiero ser profesor','2025-05-22 18:34:40'),
	 (2,55,'https://archivos-academiq.s3.amazonaws.com/peticiones/1066504563550601297.gif','me gastaría ser profesor, para educar a los alumnos sobre Cruelty Squad','2025-06-05 08:17:47');

INSERT INTO ReporteVideo (idReporte,idVideo,idUsuario,motivo,fecha) VALUES
	 (1,69,35,'Contenido engañoso','2025-05-27 21:24:31'),
	 (2,69,29,'Contenido explícito','2025-05-29 00:00:13'),
	 (3,101,29,'Contenido engañoso','2025-06-01 17:13:48'),
	 (4,69,29,'Contenido engañoso','2025-06-04 12:23:12'),
	 (5,67,29,'Spam','2025-06-04 15:25:36'),
	 (6,67,29,'Spam','2025-06-04 15:25:46'),
	 (7,67,29,'Spam','2025-06-04 15:25:57'),
	 (8,101,29,'Acoso o bullying','2025-06-04 19:34:32'),
	 (9,101,29,'Acoso o bullying','2025-06-04 19:34:38'),
	 (10,101,29,'Acoso o bullying','2025-06-04 19:34:40');

INSERT INTO ReporteVideo (idReporte,idVideo,idUsuario,motivo,fecha) VALUES
	 (11,101,29,'Acoso o bullying','2025-06-04 19:34:42'),
	 (12,101,29,'Acoso o bullying','2025-06-04 19:34:44'),
	 (13,101,29,'Contenido engañoso','2025-06-04 19:36:43'),
	 (14,101,29,'Contenido engañoso','2025-06-04 19:36:47'),
	 (15,105,55,'Otro','2025-06-05 08:12:14');

-- Reactivar verificaciones de foreign keys
SET FOREIGN_KEY_CHECKS = 1;

-- Ajustar los AUTO_INCREMENT para evitar conflictos futuros
ALTER TABLE Usuario AUTO_INCREMENT = 56;
ALTER TABLE Curso AUTO_INCREMENT = 31;
ALTER TABLE Asignatura AUTO_INCREMENT = 40;
ALTER TABLE Video AUTO_INCREMENT = 107;
ALTER TABLE Quiz AUTO_INCREMENT = 50;
ALTER TABLE Pregunta AUTO_INCREMENT = 42;
ALTER TABLE Respuesta AUTO_INCREMENT = 80;
ALTER TABLE ComentarioVideo AUTO_INCREMENT = 49;
ALTER TABLE MarcadorVideo AUTO_INCREMENT = 78;
ALTER TABLE PeticionProfesor AUTO_INCREMENT = 17;
ALTER TABLE ReporteVideo AUTO_INCREMENT = 38;
