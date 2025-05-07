-- Tabla de roles
CREATE TABLE Rol (
    idRol INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(50) UNIQUE NOT NULL
);

-- Preferencias visuales del usuario
CREATE TABLE Preferencias (
    idPreferencia INT PRIMARY KEY AUTO_INCREMENT,
    colorFondo VARCHAR(100),
    colorBordes VARCHAR(100),
    imagenFondo VARCHAR(255)
);

-- Usuarios
CREATE TABLE Usuario (
    idUsuario INT PRIMARY KEY AUTO_INCREMENT,
    avatar TEXT,
    nombre VARCHAR(100) NOT NULL,
    apellidos VARCHAR(100),
    gmail VARCHAR(255) UNIQUE NOT NULL,
    telefono VARCHAR(20),
    contraseña VARCHAR(255) NOT NULL,
    idRol INT NOT NULL,
    idPreferencia INT,
    FOREIGN KEY (idRol) REFERENCES Rol(idRol) ON DELETE CASCADE,
    FOREIGN KEY (idPreferencia) REFERENCES Preferencias(idPreferencia) ON DELETE SET NULL
);

-- Seguimiento entre usuarios (alumnos siguen a profesores)
CREATE TABLE Seguimiento (
    idAlumno INT NOT NULL,
    idProfesor INT NOT NULL,
    PRIMARY KEY (idAlumno, idProfesor),
    FOREIGN KEY (idAlumno) REFERENCES Usuario(idUsuario) ON DELETE CASCADE,
    FOREIGN KEY (idProfesor) REFERENCES Usuario(idUsuario) ON DELETE CASCADE
);

-- Cursos
CREATE TABLE Curso (
    idCurso INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(255) NOT NULL,
    imagen TEXT,
    descripcion TEXT,
    fechaCreacion DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Relación N:M entre usuarios y cursos
CREATE TABLE Usuario_Curso (
    idUsuario INT NOT NULL,
    idCurso INT NOT NULL,
    PRIMARY KEY (idUsuario, idCurso),
    FOREIGN KEY (idUsuario) REFERENCES Usuario(idUsuario) ON DELETE CASCADE,
    FOREIGN KEY (idCurso) REFERENCES Curso(idCurso) ON DELETE CASCADE
);

-- Asignaturas
CREATE TABLE Asignatura (
    idAsignatura INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(255) NOT NULL,
    descripcion TEXT,
    imagen TEXT,
    fechaCreacion DATETIME DEFAULT CURRENT_TIMESTAMP,
    idCurso INT NOT NULL,
    FOREIGN KEY (idCurso) REFERENCES Curso(idCurso) ON DELETE CASCADE
);

-- Relación N:M entre usuarios y asignaturas
CREATE TABLE Usuario_Asignatura (
    idUsuario INT NOT NULL,
    idAsignatura INT NOT NULL,
    PRIMARY KEY (idUsuario, idAsignatura),
    FOREIGN KEY (idUsuario) REFERENCES Usuario(idUsuario) ON DELETE CASCADE,
    FOREIGN KEY (idAsignatura) REFERENCES Asignatura(idAsignatura) ON DELETE CASCADE
);

-- Vídeos subidos por profesores
CREATE TABLE Video (
    idVideo INT PRIMARY KEY AUTO_INCREMENT,
    titulo VARCHAR(150) NOT NULL,
    descripcion TEXT,
    url TEXT NOT NULL,
    miniatura TEXT,
    fechaSubida DATETIME DEFAULT CURRENT_TIMESTAMP,
    idAsignatura INT NOT NULL,
    idUsuario INT NOT NULL,
    FOREIGN KEY (idAsignatura) REFERENCES Asignatura(idAsignatura) ON DELETE CASCADE,
    FOREIGN KEY (idUsuario) REFERENCES Usuario(idUsuario) ON DELETE CASCADE
);

-- Marcadores de momentos dentro del vídeo
CREATE TABLE MarcadorVideo (
    idMarcador INT PRIMARY KEY AUTO_INCREMENT,
    idVideo INT NOT NULL,
    minutoInicio DECIMAL(5,2) NOT NULL,
    minutoFin DECIMAL(5,2) NOT NULL,
    titulo VARCHAR(100),
    FOREIGN KEY (idVideo) REFERENCES Video(idVideo) ON DELETE CASCADE
);

-- Vídeos marcados como favoritos por usuarios
CREATE TABLE Favorito (
    idUsuario INT NOT NULL,
    idVideo INT NOT NULL,
    fecha DATETIME DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (idUsuario, idVideo),
    FOREIGN KEY (idUsuario) REFERENCES Usuario(idUsuario) ON DELETE CASCADE,
    FOREIGN KEY (idVideo) REFERENCES Video(idVideo) ON DELETE CASCADE
);

-- Comentarios de usuarios en vídeos
CREATE TABLE ComentarioVideo (
    idComentario INT PRIMARY KEY AUTO_INCREMENT,
    idUsuario INT NOT NULL,
    idVideo INT NOT NULL,
    texto TEXT NOT NULL,
    fecha DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (idUsuario) REFERENCES Usuario(idUsuario) ON DELETE CASCADE,
    FOREIGN KEY (idVideo) REFERENCES Video(idVideo) ON DELETE CASCADE
);

-- Quizzes creados por profesores
CREATE TABLE Quiz (
    idQuiz INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(100) NOT NULL,
    descripcion TEXT,
    idAsignatura INT NOT NULL,
    idUsuario INT NOT NULL,
    FOREIGN KEY (idAsignatura) REFERENCES Asignatura(idAsignatura) ON DELETE CASCADE,
    FOREIGN KEY (idUsuario) REFERENCES Usuario(idUsuario) ON DELETE CASCADE
);

-- Preguntas y opciones en cada quiz
CREATE TABLE DetalleQuiz (
    idDetalleQuiz INT PRIMARY KEY AUTO_INCREMENT,
    idQuiz INT NOT NULL,
    pregunta TEXT NOT NULL,
    opciones TEXT NOT NULL, -- JSON o string separado por ; si no usas tabla aparte
    FOREIGN KEY (idQuiz) REFERENCES Quiz(idQuiz) ON DELETE CASCADE
);

-- Resultados de usuarios en quizzes
CREATE TABLE ResultadoQuiz (
    idResultado INT PRIMARY KEY AUTO_INCREMENT,
    idUsuario INT NOT NULL,
    idQuiz INT NOT NULL,
    puntuacion DECIMAL(5,2),
    fecha DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (idUsuario) REFERENCES Usuario(idUsuario) ON DELETE CASCADE,
    FOREIGN KEY (idQuiz) REFERENCES Quiz(idQuiz) ON DELETE CASCADE
);
<<<<<<< HEAD
=======

-- Historial de videos para los usuarios
CREATE TABLE HistorialVideo (
    idHistorial INT PRIMARY KEY AUTO_INCREMENT,
    idUsuario INT NOT NULL,
    idVideo INT NOT NULL,
    fechaVisualizacion DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (idUsuario) REFERENCES Usuario(idUsuario) ON DELETE CASCADE,
    FOREIGN KEY (idVideo) REFERENCES Video(idVideo) ON DELETE CASCADE
);

>>>>>>> 80ab0711d3b5cb08e81a76ee11431b31e2e0bac9
