const express = require("express");
const cors = require("cors");
const { responderIA } = require("./ia/mistral");
const { obtenerContextoDinamico, cerrarConexion } = require("./ia/database");

const app = express();

// Middleware
app.use(cors({
  origin: ["http://localhost:5173", "http://localhost:3000"], // Permitir frontend
  credentials: true
}));
app.use(express.json({ limit: '10mb' }));

// Cache para optimizar rendimiento
let cacheContexto = null;
let ultimaActualizacion = null;
const DURACION_CACHE = 2 * 60 * 1000; // 2 minutos

const obtenerContextoConCache = async () => {
  const ahora = Date.now();
  
  // Si no hay cache o es muy viejo, actualizar
  if (!cacheContexto || !ultimaActualizacion || (ahora - ultimaActualizacion) > DURACION_CACHE) {
    try {
      console.log(' Actualizando contexto desde BD...');
      cacheContexto = await obtenerContextoDinamico();
      ultimaActualizacion = ahora;
      console.log(' Contexto actualizado y cacheado');
    } catch (error) {
      console.error(' Error actualizando contexto:', error.message);
      // Si hay cache anterior, usarlo
      if (cacheContexto) {
        console.log(' Usando contexto cacheado anterior');
      } else {
        throw error;
      }
    }
  } else {
    console.log('Usando contexto desde cache');
  }
  
  return cacheContexto;
};

// Endpoint principal de IA
app.post("/api/ia", async (req, res) => {
  const { pregunta } = req.body;
  
  if (!pregunta || pregunta.trim().length === 0) {
    return res.status(400).json({ 
      error: "Pregunta requerida",
      ejemplo: "Envía: { \"pregunta\": \"¿Qué cursos tienen disponibles?\" }"
    });
  }

  try {
    console.log(` Nueva pregunta: "${pregunta}"`);
    
    // Obtener contexto actualizado
    const contextoActual = await obtenerContextoConCache();
    
    // Preparar contexto para la IA
    const contextoString = contextoActual
      .map(item => {
        // Formatear cada elemento del contexto de manera más legible
        if (item.tipo === 'plataforma') {
          return `PLATAFORMA: ${item.nombre} - ${item.descripcion}. Estadísticas: ${JSON.stringify(item.estadisticas)}`;
        }
        if (item.tipo === 'curso') {
          return `CURSO: "${item.nombre}" - ${item.descripcion} (Creador: ${item.creador})`;
        }
        if (item.tipo === 'video') {
          return `VIDEO: "${item.titulo}" - ${item.descripcion} (Asignatura: ${item.asignatura}, Curso: ${item.curso})`;
        }
        if (item.tipo === 'asignatura') {
          return `ASIGNATURA: "${item.nombre}" del curso "${item.curso}" - ${item.descripcion}`;
        }
        if (item.tipo === 'quiz') {
          return `QUIZ: "${item.nombre}" - ${item.descripcion} (Asignatura: ${item.asignatura})`;
        }
        if (item.tipo === 'faq') {
          return `FAQ: ${item.pregunta} → ${item.respuesta}`;
        }
        return JSON.stringify(item);
      })
      .join('\n');
    
    console.log(` Enviando contexto de ${contextoActual.length} elementos a la IA`);
    
    // Llamar a la IA
    const respuesta = await responderIA(pregunta, contextoString);
    
    console.log(` Respuesta generada exitosamente`);
    
    res.json({ 
      respuesta,
      metadatos: {
        elementosContexto: contextoActual.length,
        fechaContexto: new Date(ultimaActualizacion).toISOString(),
        usandoCache: (Date.now() - ultimaActualizacion) < DURACION_CACHE
      }
    });
    
  } catch (error) {
    console.error(' Error procesando pregunta:', error.message);
    
    res.status(500).json({ 
      error: "Error interno del servidor",
      mensaje: "No se pudo procesar tu pregunta en este momento. Inténtalo de nuevo.",
      detalles: process.env.NODE_ENV === 'development' ? error.message : undefined
    });
  }
});

// Endpoint de salud del sistema
app.get("/api/ia/health", async (req, res) => {
  try {
    const contexto = await obtenerContextoConCache();
    
    const salud = {
      status: "OK",
      timestamp: new Date().toISOString(),
      contexto: {
        elementos: contexto.length,
        ultimaActualizacion: ultimaActualizacion ? new Date(ultimaActualizacion).toISOString() : null,
        cacheActivo: cacheContexto !== null
      },
      servidor: {
        memoria: process.memoryUsage(),
        tiempoActivo: process.uptime()
      }
    };
    
    res.json(salud);
    
  } catch (error) {
    console.error(' Error en health check:', error.message);
    
    res.status(500).json({ 
      status: "ERROR",
      timestamp: new Date().toISOString(),
      error: error.message,
      contexto: {
        elementos: cacheContexto ? cacheContexto.length : 0,
        cacheActivo: cacheContexto !== null
      }
    });
  }
});

// Endpoint para limpiar cache manualmente
app.post("/api/ia/refresh", async (req, res) => {
  try {
    console.log(' Limpiando cache y actualizando contexto...');
    
    cacheContexto = null;
    ultimaActualizacion = null;
    
    const nuevoContexto = await obtenerContextoConCache();
    
    res.json({
      mensaje: "Cache actualizado exitosamente",
      elementos: nuevoContexto.length,
      timestamp: new Date().toISOString()
    });
    
  } catch (error) {
    console.error(' Error refrescando cache:', error.message);
    res.status(500).json({ 
      error: "Error actualizando cache",
      mensaje: error.message 
    });
  }
});

// Endpoint para estadísticas de uso
app.get("/api/ia/stats", (req, res) => {
  res.json({
    servidor: {
      tiempoActivo: process.uptime(),
      memoria: process.memoryUsage(),
      version: process.version
    },
    cache: {
      activo: cacheContexto !== null,
      elementos: cacheContexto ? cacheContexto.length : 0,
      ultimaActualizacion: ultimaActualizacion ? new Date(ultimaActualizacion).toISOString() : null,
      duracionCache: DURACION_CACHE / 1000 + ' segundos'
    }
  });
});

// Manejo de errores global
app.use((error, req, res, next) => {
  console.error(' Error no manejado:', error);
  res.status(500).json({
    error: "Error interno del servidor",
    timestamp: new Date().toISOString()
  });
});

// Endpoint 404
app.use('*', (req, res) => {
  res.status(404).json({
    error: "Endpoint no encontrado",
    disponibles: [
      "POST /api/ia - Hacer pregunta a la IA",
      "GET /api/ia/health - Estado del sistema", 
      "POST /api/ia/refresh - Actualizar cache",
      "GET /api/ia/stats - Estadísticas del servidor"
    ]
  });
});

// Iniciar servidor
const PORT = process.env.PORT || 3001;

app.listen(PORT, async () => {
  console.log(` Servidor IA iniciado en puerto ${PORT}`);
  console.log(` Endpoints disponibles:`);
  console.log(`   - POST http://localhost:${PORT}/api/ia`);
  console.log(`   - GET  http://localhost:${PORT}/api/ia/health`);
  console.log(`   - POST http://localhost:${PORT}/api/ia/refresh`);
  console.log(`   - GET  http://localhost:${PORT}/api/ia/stats`);
  
  // Precargar contexto al iniciar
  try {
    console.log('Precargando contexto inicial...');
    await obtenerContextoConCache();
    console.log(' Contexto inicial cargado exitosamente');
  } catch (error) {
    console.error('  Advertencia: No se pudo cargar contexto inicial:', error.message);
  }
});

// Manejo limpio del cierre del servidor
process.on('SIGINT', async () => {
  console.log('\n Cerrando servidor IA...');
  await cerrarConexion();
  console.log(' Servidor IA cerrado correctamente');
  process.exit(0);
});

process.on('SIGTERM', async () => {
  console.log('\n Cerrando servidor IA (SIGTERM)...');
  await cerrarConexion();
  process.exit(0);
});