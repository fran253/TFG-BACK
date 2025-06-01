const mysql = require('mysql2/promise');

let connection = null;

const conectarBD = async () => {
  if (!connection) {
    try {
      connection = await mysql.createConnection({
        host: 'databasev3.cl6scf9wczz0.us-east-1.rds.amazonaws.com',
        port: 3306,
        user: 'rooy',
        password: 'FMfcgzQZTCqZPnrwkWsNZDNDsqtphSfr',
        database: 'bbddacademIQ',
        timeout: 10000,
        acquireTimeout: 10000,
        reconnect: true
      });
      console.log('✅ Conectado a la base de datos MySQL');
    } catch (error) {
      console.error('❌ Error conectando a BD:', error.message);
      throw error;
    }
  }
  return connection;
};

const obtenerContextoDinamico = async () => {
  try {
    const db = await conectarBD();
    
    // 1. Obtener cursos recientes con información del creador
    const [cursos] = await db.execute(`
      SELECT c.Nombre, c.Descripcion, u.Nombre as NombreCreador, c.FechaCreacion
      FROM Curso c
      LEFT JOIN Usuario u ON c.IdUsuario = u.IdUsuario
      ORDER BY c.FechaCreacion DESC 
      LIMIT 12
    `);

    // 2. Obtener videos recientes con asignatura y curso
    const [videos] = await db.execute(`
      SELECT 
        v.Titulo, 
        v.Descripcion, 
        a.Nombre as NombreAsignatura,
        c.Nombre as NombreCurso,
        u.Nombre as NombreCreador,
        v.FechaSubida
      FROM Video v 
      JOIN Asignatura a ON v.IdAsignatura = a.IdAsignatura 
      JOIN Curso c ON a.IdCurso = c.IdCurso
      LEFT JOIN Usuario u ON v.IdUsuario = u.IdUsuario
      ORDER BY v.FechaSubida DESC 
      LIMIT 15
    `);

    // 3. Obtener asignaturas por curso
    const [asignaturas] = await db.execute(`
      SELECT 
        a.Nombre, 
        a.Descripcion, 
        c.Nombre as NombreCurso,
        a.FechaCreacion
      FROM Asignatura a
      JOIN Curso c ON a.IdCurso = c.IdCurso
      ORDER BY a.FechaCreacion DESC 
      LIMIT 12
    `);

    // 4. Obtener quizzes populares
    const [quizzes] = await db.execute(`
      SELECT 
        q.Nombre, 
        q.Descripcion,
        a.Nombre as NombreAsignatura,
        c.Nombre as NombreCurso,
        u.Nombre as NombreCreador
      FROM Quiz q
      JOIN Asignatura a ON q.IdAsignatura = a.IdAsignatura
      JOIN Curso c ON q.IdCurso = c.IdCurso
      LEFT JOIN Usuario u ON q.IdUsuario = u.IdUsuario
      ORDER BY q.FechaCreacion DESC 
      LIMIT 10
    `);

    // 5. Obtener estadísticas generales
    const [stats] = await db.execute(`
      SELECT 
        (SELECT COUNT(*) FROM Curso) as totalCursos,
        (SELECT COUNT(*) FROM Video) as totalVideos,
        (SELECT COUNT(*) FROM Usuario WHERE IdRol = 2) as totalProfesores,
        (SELECT COUNT(*) FROM Usuario WHERE IdRol = 1) as totalEstudiantes,
        (SELECT COUNT(*) FROM Quiz) as totalQuizzes
    `);

    const estadisticas = stats[0];

    // Construir contexto estructurado
    const contexto = [
      // Información general de la plataforma
      {
        tipo: "plataforma",
        nombre: "AcademIQ",
        descripcion: "Plataforma educativa con cursos, videos y quizzes interactivos",
        estadisticas: {
          cursos: estadisticas.totalCursos,
          videos: estadisticas.totalVideos,
          profesores: estadisticas.totalProfesores,
          estudiantes: estadisticas.totalEstudiantes,
          quizzes: estadisticas.totalQuizzes
        }
      },

      // Cursos disponibles
      ...cursos.map(c => ({
        tipo: "curso",
        nombre: c.Nombre,
        descripcion: c.Descripcion,
        creador: c.NombreCreador,
        fechaCreacion: c.FechaCreacion
      })),
      
      // Videos disponibles
      ...videos.map(v => ({
        tipo: "video",
        titulo: v.Titulo,
        descripcion: v.Descripcion,
        asignatura: v.NombreAsignatura,
        curso: v.NombreCurso,
        creador: v.NombreCreador,
        fechaSubida: v.FechaSubida
      })),
      
      // Asignaturas disponibles
      ...asignaturas.map(a => ({
        tipo: "asignatura", 
        nombre: a.Nombre,
        descripcion: a.Descripcion,
        curso: a.NombreCurso,
        fechaCreacion: a.FechaCreacion
      })),

      // Quizzes disponibles
      ...quizzes.map(q => ({
        tipo: "quiz",
        nombre: q.Nombre,
        descripcion: q.Descripcion,
        asignatura: q.NombreAsignatura,
        curso: q.NombreCurso,
        creador: q.NombreCreador
      })),
      
      // FAQs estáticas importantes
      {
        tipo: "faq",
        categoria: "registro",
        pregunta: "¿Cómo me registro en la plataforma?",
        respuesta: "Haz clic en el botón 'Registrarse' en la esquina superior derecha y completa el formulario con tu información personal."
      },
      {
        tipo: "faq",
        categoria: "contenido",
        pregunta: "¿Cómo subo un video?",
        respuesta: "Como profesor, ve a tu panel, selecciona 'Subir Video', elige el archivo, selecciona curso y asignatura, y completa la información descriptiva."
      },
      {
        tipo: "faq",
        categoria: "cursos",
        pregunta: "¿Cómo me inscribo a un curso?",
        respuesta: "Busca el curso que te interese, haz clic en 'Ver detalles' y luego en 'Inscribirse'. Necesitas estar registrado como estudiante."
      },
      {
        tipo: "faq",
        categoria: "quizzes",
        pregunta: "¿Cómo funciona el sistema de quizzes?",
        respuesta: "Los profesores crean quizzes con preguntas de opción múltiple. Los estudiantes pueden responderlos y ver sus resultados inmediatamente."
      },
      {
        tipo: "faq",
        categoria: "roles",
        pregunta: "¿Cuál es la diferencia entre estudiante y profesor?",
        respuesta: "Los estudiantes pueden ver contenido, hacer quizzes y seguir profesores. Los profesores pueden crear cursos, subir videos y crear quizzes."
      }
    ];

    console.log(` Contexto dinámico cargado: ${contexto.length} elementos`);
    console.log(`- ${cursos.length} cursos`);
    console.log(`- ${videos.length} videos`);
    console.log(`- ${asignaturas.length} asignaturas`);
    console.log(`- ${quizzes.length} quizzes`);
    
    return contexto;
    
  } catch (error) {
    console.error(' Error obteniendo contexto dinámico:', error.message);
    
    // Fallback al archivo estático si hay error
    console.log(' Usando datos estáticos como fallback...');
    try {
      return require('../datosIA.json');
    } catch (fallbackError) {
      console.error(' Error cargando fallback:', fallbackError.message);
      // Contexto mínimo de emergencia
      return [
        {
          tipo: "plataforma",
          nombre: "AcademIQ", 
          descripcion: "Plataforma educativa (modo offline)"
        },
        {
          tipo: "faq",
          pregunta: "¿Cómo me registro?",
          respuesta: "Haz clic en 'Registrarse' y completa el formulario."
        }
      ];
    }
  }
};

// Cerrar conexión limpiamente
const cerrarConexion = async () => {
  if (connection) {
    await connection.end();
    connection = null;
    console.log('🔌 Conexión a BD cerrada');
  }
};

// Manejar cierre del proceso
process.on('SIGINT', async () => {
  await cerrarConexion();
  process.exit(0);
});

module.exports = { 
  obtenerContextoDinamico, 
  cerrarConexion,
  conectarBD 
};