-- Tabla ROL
CREATE TABLE Rol (
    IdRol INT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL
);

-- Tabla PREFERENCIAS
CREATE TABLE Preferencias (
    IdPreferencia INT PRIMARY KEY,
    ColorFondo VARCHAR(100),
    ColorBordes VARCHAR(100),
    ImagenFondo VARCHAR(255)
);

-- Tabla SEGUIMIENTO
CREATE TABLE Seguimiento (
    IdSeguimiento INT PRIMARY KEY,
    IdAlumno INT NOT NULL,
    IdProfesor INT NOT NULL
);

-- Tabla USUARIO
CREATE TABLE Usuario (
    IdUsuario INT PRIMARY KEY,
    NombreUsuario VARCHAR(100) NOT NULL,
    Gmail VARCHAR(100) NOT NULL,
    Contraseña VARCHAR(100) NOT NULL,
    NumeroTelefono VARCHAR(20),
    IdRol INT NOT NULL,
    IdPreferencia INT NOT NULL,
    IdSeguimiento INT NOT NULL,
    FOREIGN KEY (IdRol) REFERENCES Rol(IdRol),
    FOREIGN KEY (IdPreferencia) REFERENCES Preferencias(IdPreferencia),
    FOREIGN KEY (IdSeguimiento) REFERENCES Seguimiento(IdSeguimiento)
);

-- Tabla ASIGNATURA
CREATE TABLE Asignatura (
    IdAsignatura INT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion TEXT
);

-- Tabla USUARIO_ASIGNATURA
CREATE TABLE UsuarioAsignatura (
    IdUsuario INT NOT NULL,
    IdAsignatura INT NOT NULL,
    PRIMARY KEY (IdUsuario, IdAsignatura),
    FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario),
    FOREIGN KEY (IdAsignatura) REFERENCES Asignatura(IdAsignatura)
);

-- Tabla QUIZ
CREATE TABLE Quiz (
    IdQuizz INT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion TEXT,
    IdAsignatura INT NOT NULL,
    IdUsuario INT NOT NULL,
    FOREIGN KEY (IdAsignatura) REFERENCES Asignatura(IdAsignatura),
    FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
);

-- Tabla DETALLE QUIZZ
CREATE TABLE DetalleQuizz (
    IdDetalleQuizz INT PRIMARY KEY,
    IdQuizz INT NOT NULL,
    Pregunta TEXT NOT NULL,
    Opciones TEXT NOT NULL,
    FOREIGN KEY (IdQuizz) REFERENCES Quiz(IdQuizz)
);

-- Tabla VIDEO
CREATE TABLE Video (
    IdVideo INT PRIMARY KEY,
    NombreVideo VARCHAR(150) NOT NULL,
    Descripcion TEXT,
    MeGusta INT DEFAULT 0,
    Miniatura VARCHAR(255),
    IdTablaMinutos INT, --debemos crear tabla para esto
    IdAsignatura INT NOT NULL,
    IdUsuario INT NOT NULL,
    FOREIGN KEY (IdAsignatura) REFERENCES Asignatura(IdAsignatura),
    FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
);
