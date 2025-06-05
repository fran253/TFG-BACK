INSERT INTO Asignatura (nombre,descripcion,imagen,fechaCreacion,idCurso) VALUES
	 ('Contenedores','',NULL,'2025-05-26 14:25:06',16),
	 ('EC2','',NULL,'2025-05-26 14:25:14',16),
	 ('RDS','',NULL,'2025-05-26 14:25:18',16),
	 ('Security Groups','',NULL,'2025-05-26 14:25:27',16),
	 ('MySQL','',NULL,'2025-05-27 13:57:48',17),
	 ('Aurora DB','',NULL,'2025-05-27 13:57:56',17),
	 ('PostGreSQL','',NULL,'2025-05-27 13:58:12',17),
	 ('vinland 1','',NULL,'2025-05-27 15:41:58',18),
	 ('Vinland 2','',NULL,'2025-05-27 15:42:03',18),
	 ('Clases y Objetos','',NULL,'2025-05-30 23:57:13',24);
INSERT INTO Asignatura (nombre,descripcion,imagen,fechaCreacion,idCurso) VALUES
	 ('Encapsulamiento','',NULL,'2025-05-30 23:57:26',24),
	 ('Herencia','',NULL,'2025-05-30 23:57:35',24),
	 ('Abstracción','',NULL,'2025-05-30 23:57:40',24),
	 ('Matematicas','',NULL,'2025-06-01 10:18:00',28),
	 ('Logistica Factorial','',NULL,'2025-06-01 10:18:15',28),
	 ('robotin','',NULL,'2025-06-04 08:02:47',29);
INSERT INTO ComentarioVideo (idUsuario,idVideo,texto,fecha,NumeroReportes) VALUES
	 (30,64,'Aquí tenéis el video que tanto me habéis pedido','2025-05-26 15:47:39',0),
	 (35,64,'Videazo','2025-05-26 16:30:17',0),
	 (35,66,'REPORTADO NO ES DE MYSQL','2025-05-27 14:28:05',0),
	 (35,67,'ES CLICK  BAIT ! ESTO NO ES UNA R AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA','2025-05-27 16:41:18',0),
	 (29,66,'pero quien ha subido esto!!!!','2025-05-27 17:02:40',0),
	 (35,69,'OTRO QUE TAMPOCO ES SOBRE AWS REPORTADO','2025-05-27 19:24:26',0),
	 (29,69,'este canal...','2025-06-03 10:57:23',0),
	 (40,64,'Hola soy YO!','2025-06-04 08:04:12',0),
	 (40,101,'Test','2025-06-04 08:10:48',0),
	 (35,65,'Buenos dias modesto 
','2025-06-04 08:44:47',0);
INSERT INTO ComentarioVideo (idUsuario,idVideo,texto,fecha,NumeroReportes) VALUES
	 (40,65,'Muy buenos dias gente!','2025-06-04 08:45:12',0),
	 (55,105,'Video bastante efectivo, muchas gracias','2025-06-05 08:12:32',0),
	 (55,67,'THAT IS NOT SOLID SNAKE!!!!1!','2025-06-05 08:14:56',0);
INSERT INTO Curso (nombre,imagen,descripcion,fechaCreacion,idUsuario) VALUES
	 ('AWS','https://archivos-academiq.s3.amazonaws.com/cursos/aws-servicios-principales.jpg','Curso de AWS donde se suben videos desde crear un EC2 hasta subir imágenes a S3.','2025-05-26 14:24:13',29),
	 ('BBDD','https://archivos-academiq.s3.amazonaws.com/cursos/Bases-de-datos3.jpg','Curso de BBDD donde se aprenderá lo básico y esencial','2025-05-27 13:57:40',29),
	 ('Vinland','https://archivos-academiq.s3.amazonaws.com/cursos/Descargar fondos de pantalla 4k, Alpes, prados, montañas, verano, Alemania, Europa libre_ Imágenes fondos de descarga gratuita.jfif','hola','2025-05-27 15:41:51',29),
	 ('La Programación Orientada a Objetos (POO)','https://archivos-academiq.s3.amazonaws.com/cursos/1366_2000.jpg','La programación orientada a objetos es un modelo de programación en el que el diseño de software se organiza alrededor de datos u objetos, en vez de usar funciones y lógica. Se enfoca en los objetos que los programadores necesitan manipular, en lugar de centrarse en la lógica necesaria para esa manipulación.','2025-05-30 23:56:39',45),
	 ('Calculo Avanzado Matematico','https://archivos-academiq.s3.amazonaws.com/cursos/un-genio-matemtico-en-espera-de-biografa.jpg','Aprende calculo avanzado como un autentico profesional','2025-06-01 10:17:50',35),
	 ('Hola','https://archivos-academiq.s3.amazonaws.com/cursos/Captura de pantalla 2025-05-29 123443.png','hola','2025-06-04 08:02:27',29);
INSERT INTO Favorito (idUsuario,idVideo) VALUES
	 (29,64),
	 (29,67),
	 (29,103),
	 (29,105),
	 (55,105);
INSERT INTO MarcadorVideo (idVideo,minutoImportante,titulo) VALUES
	 (64,55.48,'Inicio del video'),
	 (64,88.30,'Creación de bucket'),
	 (64,144.71,'Explicación y github'),
	 (64,318.79,'Configuración del bucket'),
	 (65,130.05,'inicio'),
	 (65,226.94,'Creación Instancia'),
	 (65,834.51,'añadir reglas al Security Group'),
	 (65,999.99,'Mas configuración'),
	 (101,0.00,'Esto no es realmente matemática avanzada'),
	 (101,5.03,'Morritos en el minuto 00:05');
INSERT INTO MarcadorVideo (idVideo,minutoImportante,titulo) VALUES
	 (105,106.68,'eeee'),
	 (105,272.16,'rerrere');
INSERT INTO PeticionProfesor (idUsuario,DocumentacionUrl,Texto,FechaPeticion) VALUES
	 (22,'https://tu-servidor.com/uploads/2193459a-9ee7-4ebe-ac5f-09fa65aa6989.png','quiero ser profesorquiero ser profesorquiero ser profesorquiero ser profesorquiero ser profesor','2025-05-22 18:34:40'),
	 (55,'https://archivos-academiq.s3.amazonaws.com/peticiones/1066504563550601297.gif','me gastaría ser profesor, para educar a los alumnos sobre Cruelty Squad','2025-06-05 08:17:47');
INSERT INTO Pregunta (IdQuiz,Descripcion,Orden) VALUES
	 (10,'BALATROBALATROBALATRO ESTILO DE VIDA?',1),
	 (10,'BALATROBALATROBALATROBALATRO',2),
	 (10,'BALATROBALATROBALATRO',3),
	 (11,'Base de Datos',1),
	 (12,'Te gusta  mysql?',1),
	 (13,'Te gusta aurora DB?',1),
	 (13,'Te gusta bbdd?',2),
	 (20,'LEY Y ....',1),
	 (26,'23233232',1),
	 (28,'heeyyyy',1);
INSERT INTO Pregunta (IdQuiz,Descripcion,Orden) VALUES
	 (30,'¿Qué es una clase?',1),
	 (36,'Te gusta la Matematica avanzada quizz',1),
	 (37,'12212121',1),
	 (38,'peperoni',1),
	 (39,'1234',1),
	 (40,'ewewew',1),
	 (41,'qwwqwqw',1),
	 (42,'robot?',1),
	 (43,'weewew',1),
	 (44,'sdsdsd',1);
INSERT INTO Pregunta (IdQuiz,Descripcion,Orden) VALUES
	 (45,'TESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTEST',1);
INSERT INTO Quiz (Nombre,Descripcion,IdUsuario,FechaCreacion,IdCurso,IdAsignatura) VALUES
	 ('Rodriguez','asd',22,'2025-05-25 19:40:49',16,16),
	 ('supra','supra',22,'2025-05-25 20:12:22',16,16),
	 ('supra','supra',22,'2025-05-25 20:12:37',16,16),
	 ('ejemplo','ejemplo',22,'2025-05-25 20:16:09',16,16),
	 ('asda','sd',22,'2025-05-25 20:20:53',16,16),
	 ('Prueba sobre BALATRO','BALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATROBALATRO',22,'2025-05-26 00:15:59',16,15),
	 ('Base de Datos','Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos Base de Datos ',22,'2025-05-26 15:26:19',17,15),
	 ('MySQL quiz','',29,'2025-05-27 16:38:57',17,19),
	 ('Aurora DB','',29,'2025-05-27 16:49:09',17,20),
	 ('LEY Y ORDEN','Ley Y orden de tu pais bobo',35,'2025-05-27 21:44:02',NULL,NULL);
INSERT INTO Quiz (Nombre,Descripcion,IdUsuario,FechaCreacion,IdCurso,IdAsignatura) VALUES
	 ('23333','3',29,'2025-05-28 20:39:07',16,18),
	 ('Holaaa','',45,'2025-05-29 19:03:13',16,17),
	 ('Conocimientos Basicos de POO','Un quizz con datos básicos sobre la programación orientada a objetos',45,'2025-05-31 02:10:49',24,30),
	 ('Matematica avanzada quizz','Matematica avanzada quizz',35,'2025-06-01 12:21:27',28,36),
	 ('21122','122121',29,'2025-06-03 10:58:08',16,15),
	 ('pizza','',29,'2025-06-04 08:03:47',29,38),
	 ('12354','',29,'2025-06-04 15:17:46',16,17),
	 ('etttt','we',29,'2025-06-04 15:21:52',29,38),
	 ('prueba','',29,'2025-06-04 15:23:05',29,38),
	 ('robot','',29,'2025-06-04 15:27:34',29,38);
INSERT INTO Quiz (Nombre,Descripcion,IdUsuario,FechaCreacion,IdCurso,IdAsignatura) VALUES
	 ('por favor','',29,'2025-06-04 15:29:55',29,38),
	 ('vinland 1','sdsdsd',29,'2025-06-04 15:31:41',29,38),
	 ('TEST','TEST',35,'2025-06-05 08:23:48',28,36);
INSERT INTO ReporteVideo (idVideo,idUsuario,motivo,fecha) VALUES
	 (69,35,'Contenido engañoso','2025-05-27 21:24:31'),
	 (69,29,'Contenido explícito','2025-05-29 00:00:13'),
	 (101,29,'Contenido engañoso','2025-06-01 17:13:48'),
	 (69,29,'Contenido engañoso','2025-06-04 12:23:12'),
	 (67,29,'Spam','2025-06-04 15:25:36'),
	 (67,29,'Spam','2025-06-04 15:25:46'),
	 (67,29,'Spam','2025-06-04 15:25:57'),
	 (101,29,'Acoso o bullying','2025-06-04 19:34:32'),
	 (101,29,'Acoso o bullying','2025-06-04 19:34:38'),
	 (101,29,'Acoso o bullying','2025-06-04 19:34:40');
INSERT INTO ReporteVideo (idVideo,idUsuario,motivo,fecha) VALUES
	 (101,29,'Acoso o bullying','2025-06-04 19:34:42'),
	 (101,29,'Acoso o bullying','2025-06-04 19:34:44'),
	 (101,29,'Contenido engañoso','2025-06-04 19:36:43'),
	 (101,29,'Contenido engañoso','2025-06-04 19:36:47'),
	 (105,55,'Otro','2025-06-05 08:12:14');
INSERT INTO Respuesta (IdPregunta,Texto,EsCorrecta,Orden) VALUES
	 (7,'BALATRO SUPREME',1,1),
	 (7,'BALATRO SUPER BOWL',0,2),
	 (7,'BALATRO MEGA NIGGER',0,3),
	 (8,'BALATRO',0,1),
	 (8,'BALATRO',0,2),
	 (8,'BALATRO',0,3),
	 (8,'BALATRO',1,4),
	 (9,'BALATROBALATRO',1,1),
	 (9,'BALATRO',0,2),
	 (10,'Base de Datos',0,1);
INSERT INTO Respuesta (IdPregunta,Texto,EsCorrecta,Orden) VALUES
	 (10,'Base de Datos',0,2),
	 (10,'Base de Datos',0,3),
	 (10,'Base de Datos',1,4),
	 (11,'si',1,1),
	 (11,'no',0,2),
	 (12,'si',1,1),
	 (12,'no',0,2),
	 (13,'no',0,1),
	 (13,'por supuesto',1,2),
	 (15,'Orden',1,1);
INSERT INTO Respuesta (IdPregunta,Texto,EsCorrecta,Orden) VALUES
	 (15,'Caos',0,2),
	 (22,'23',1,1),
	 (22,'3',0,2),
	 (24,'3',1,1),
	 (24,'2',0,2),
	 (26,'Una función especial',1,1),
	 (26,'Una estructura que define atributos y métodos',0,2),
	 (32,'S',1,1),
	 (32,'N',0,2),
	 (33,'1',1,1);
INSERT INTO Respuesta (IdPregunta,Texto,EsCorrecta,Orden) VALUES
	 (33,'3',0,2),
	 (34,'no',1,1),
	 (34,'si',0,2),
	 (35,'123',1,1),
	 (35,'233',0,2),
	 (36,'weewew',1,1),
	 (36,'ewweewewewewewewweew',0,2),
	 (37,':wq',1,1),
	 (37,':uuuu',0,2),
	 (38,'si',1,1);
INSERT INTO Respuesta (IdPregunta,Texto,EsCorrecta,Orden) VALUES
	 (38,'no',0,2),
	 (39,'1',1,1),
	 (39,'2',0,2),
	 (40,'sddsad',1,1),
	 (40,'adadad',0,2),
	 (41,'NO THAT IS NOT SOLID SNAKE',1,1),
	 (41,'NIGRO THAT IS SOLID SNAKE',0,2);
INSERT INTO Rol (idRol,nombre) VALUES
	 (3,'Administrador'),
	 (1,'Alumno'),
	 (2,'Profesor');
INSERT INTO Usuario (avatar,nombre,apellidos,gmail,telefono,contraseña,idRol,CursosSeguidos,Token) VALUES
	 ('avatar4.png','Usuario','usuariez','usuariez@example.com','666777999','hash_contraseña_segura4',2,'',NULL),
	 (NULL,'Einar','el guay','einar@gmail.com','123456789','123456',1,'',NULL),
	 (NULL,'balatro',NULL,'balatrobalatrez@gmail.com','666888999','ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f',2,'','25364507-2bd9-4d2c-986d-69df7fb1ee11'),
	 (NULL,'balatrero',NULL,'balatringutierrez@gmail.com','666888999','ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f',2,NULL,NULL),
	 (NULL,'fran','rebollo','franrebollo@gmail.com','123456789','ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f',3,NULL,'393a7446-1440-406e-9044-cf5de1532362'),
	 (NULL,'balatro','balatrini','balatringutierrez33@gmail.com','666888999','ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f',2,NULL,'18711306-c39a-400d-85d3-91e23c29ed50'),
	 (NULL,'Usuario Test',NULL,'test@example.com',NULL,'123456',1,NULL,NULL),
	 (NULL,'Carlos','Carlez','carlos.prof@example.com','666555666','ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f',1,NULL,'9b8ecfb3-39ef-4305-aa43-8ca565744a47'),
	 (NULL,'ejemplo','ejemplo','ejemploejemplez@gmail.com','123456768','ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f',2,NULL,'8ffa5b2a-738c-45fe-87d0-9b303e7d1cb1'),
	 (NULL,'Judini','Cesar','judini@gmail.com','683512911','sin_cambio',1,NULL,NULL);
INSERT INTO Usuario (avatar,nombre,apellidos,gmail,telefono,contraseña,idRol,CursosSeguidos,Token) VALUES
	 (NULL,'diego@gmail.com',NULL,'diego@gmail.com','5453254654','c0f9fda42308017c288314b350117d494ecac53201ead588de3076ae6df4e26b',3,NULL,'82a810c9-9a6b-432f-b306-c0d6dc702f0b'),
	 (NULL,'einar','el colega','einar12@gmail.com','123456789','ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f',1,NULL,'43aac478-cc1f-4e57-b7ba-2121bd1e8eba'),
	 (NULL,'Nicolas ','','Ferrial@gmail.com','','744b6f384ae8f75a7674bd8f32e8cc72004a9b1b9f451589686cead88378aa42',1,NULL,'572279c7-e897-4d7b-b90f-8d4c3b392d98'),
	 (NULL,'Stańczyk','Sin Apellido','Stańczyk@gmail.com','788 67 67 87','41cf724e1b5bcbecedad0f2b78494c9146d41bb91cd0a224529f8ef853ad3b80',1,NULL,'6685b3c5-bb4b-4d7a-83c1-9a88e3ebbe06'),
	 (NULL,'Jane','Foole','Jane@gmail.com','893 89 09 89','fb838ea1b6df3cbf884a613387e2262df027552c94cc9e9e2f7573f2873b766b',2,NULL,'fc949104-c80e-4586-b453-13375ea98c1b'),
	 (NULL,'anton','antonez','antonantonez@gmail.com','123456789','ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f',1,NULL,'7e0a8b98-01ba-4c93-aaa2-dbc57b7c384d'),
	 (NULL,'Sofie','Reyes','Sofi@gmail.com','678 09 58 89','141b7bed25e12b51889944ca8dc4af5b216257547f692a642196f1aa7b64beba',1,NULL,'0e641a03-0f8d-4c8f-b884-45bd85e92851'),
	 (NULL,'balatro','balatrez','balatrobalatrez22@gmail.com','123456789','ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f',1,NULL,'9e5c9a6b-6fd4-4534-9cd5-fc64a8dd0fdd'),
	 (NULL,'fran','rebo','franrebo2004@gmail.com','123456789','ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f',2,NULL,'db5fb77c-2f9f-45c7-9321-58e1b58f7d8e'),
	 (NULL,'hola','holez','holaholez@gmail.com','123456789','ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f',1,NULL,'39d5a958-172d-4da3-a4b7-2fe313d7f2e1');
INSERT INTO Usuario (avatar,nombre,apellidos,gmail,telefono,contraseña,idRol,CursosSeguidos,Token) VALUES
	 (NULL,'Dammian','Piezcochib','Dam811ian@gmail.com','897 89 89 98','72921590a2eadf2f54f72648c92828672ae860e17f464fe3be45c28b1984cc77',1,NULL,'87f796b0-1aa6-4a0d-aa24-fba944ad9ab4'),
	 (NULL,'William  JR','Dafoe','WilliamJR@gmail.com','8964895493','57d119816e51009df8f7a64002d67ddf7d49b242883b6a3df13a024eeea94eeb',1,NULL,'1b8b8eeb-074a-475a-a571-0a9bdc918ce3'),
	 (NULL,'SUPER','MARIO','MARIO@gmail.com','7890457896','82c71d4e882ea1778639c30d14465f9b55eee99c7cae9f24e50b28db752ebb87',1,NULL,'a6813021-decc-4ba0-b5ce-fe06bef09bc5'),
	 (NULL,'balatro','balatrez','balatrobalatrez@balatro.com','123456789','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92',1,NULL,'835124a8-9fd7-4e79-9a87-c55d4ffb5409'),
	 (NULL,'paco','hernandez','pacohernandez@gmail.com','123456789','ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f',1,NULL,'4fd277e1-4dc5-40d0-ba76-c5654108af06'),
	 (NULL,'tu','tu','tu@gmail.com','1111111111','ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f',1,NULL,'c66c0e39-9f68-48ef-bdf6-78c32605d957'),
	 (NULL,'Pepito','Halzador de Esquirlas','enverdadelcpestabien@vivaelcp.com','+34 000 000','bcba9b04bee61cca8c6c014fdaf2b2e7ecf69ff3fe0b4678da85bcdd128f2649',1,NULL,'7f93ea4a-6611-4b6c-930c-a8c5dd71a906');
INSERT INTO Usuario_Curso (idUsuario,idCurso) VALUES
	 (22,16),
	 (29,16),
	 (35,16),
	 (40,16),
	 (45,16),
	 (29,17),
	 (40,17),
	 (40,18);
INSERT INTO Video (titulo,descripcion,duracion,url,miniatura,fechaSubida,idAsignatura,idUsuario,numReportes,idCurso,ContadorLikes) VALUES
	 ('Amazon S3 Static Web Site','GitHub
https://github.com/santos-pardos/Hand...','','https://archivos-academiq.s3.amazonaws.com/video/S3 Static Web.mp4','https://archivos-academiq.s3.amazonaws.com/miniatura/1_Qz8qHl2TEWNK_1rmPZ-t-A.jpg','2025-05-26 17:44:39',15,30,0,16,2),
	 ('EC2','Aws ec2 explicacion como montar una instancia','','https://archivos-academiq.s3.amazonaws.com/video/EC2 Linux Apache.mp4','https://archivos-academiq.s3.amazonaws.com/miniatura/1_rwAyCH8oA4BmpJojOu8nXA.png','2025-05-26 20:21:27',16,29,0,16,0),
	 ('mySQL','Introduccion a MySQL','','https://archivos-academiq.s3.amazonaws.com/video/¡ULTRAS-DEL-ESPANYOL-SALTAN-AL-CAMPO-Y-PERSIGUEN-A-06f71387-235c-4844-9d6f-f5bb116f957c.mp4','https://archivos-academiq.s3.amazonaws.com/miniatura/Bases-de-datos3.jpg','2025-05-27 16:26:03',19,29,0,17,0),
	 ('r','r','','https://archivos-academiq.s3.amazonaws.com/video/¡ULTRAS DEL ESPANYOL SALTAN AL CAMPO Y PERSIGUEN A LOS JUGADORES DEL BARÇA! (1).mp4','https://archivos-academiq.s3.amazonaws.com/miniatura/aaaa.PNG','2025-05-27 18:31:28',23,29,3,18,1),
	 ('vagabundo','vagabundez','','https://archivos-academiq.s3.amazonaws.com/video/¡ULTRAS-DEL-ESPANYOL-SALTAN-AL-CAMPO-Y-PERSIGUEN-A-06f71387-235c-4844-9d6f-f5bb116f957c.mp4','https://archivos-academiq.s3.amazonaws.com/miniatura/Captusssssssssra.PNG','2025-05-27 20:01:52',15,29,3,16,0),
	 ('Inicialización a la matemática avanzada','Inicialización a la matemática avanzada','','https://archivos-academiq.s3.amazonaws.com/video/ssstwitter.com_1747222672312.mp4','https://archivos-academiq.s3.amazonaws.com/miniatura/images.png','2025-06-01 17:09:13',36,35,7,28,0),
	 ('cine','cine','','https://archivos-academiq.s3.amazonaws.com/video/¡ULTRAS DEL ESPANYOL SALTAN AL CAMPO Y PERSIGUEN A LOS JUGADORES DEL BARÇA!.mp4','https://archivos-academiq.s3.amazonaws.com/miniatura/Capturassss.PNG','2025-06-03 15:27:14',22,29,0,18,1),
	 ('AWSado','awsado','','https://archivos-academiq.s3.amazonaws.com/video/S3 Static Web.mp4','https://archivos-academiq.s3.amazonaws.com/miniatura/pexels-pixabay-301920.jpg','2025-06-04 19:30:49',16,29,1,16,2);
