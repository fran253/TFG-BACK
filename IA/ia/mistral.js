const axios = require("axios");
require("dotenv").config({ path: __dirname + "/../.env" });

const responderIA = async (pregunta, contexto) => {
  // Crear un prompt más estructurado y específico para AcademIQ
  const promptSistema = `Eres un asistente inteligente de AcademIQ, una plataforma educativa online. 

Tu función es ayudar a usuarios (estudiantes y profesores) con información sobre:
- Cursos disponibles y sus contenidos
- Videos educativos y materiales
- Asignaturas y sus descripciones  
- Quizzes y evaluaciones
- Funcionalidades de la plataforma
- Registro y uso del sistema

INSTRUCCIONES IMPORTANTES:
1. Sé específico y menciona nombres exactos de cursos, videos y asignaturas cuando sea relevante
2. Si no tienes información específica, sugiere alternativas relacionadas del contexto
3. Mantén un tono amigable y educativo
4. Si preguntan sobre algo que no está en el contexto, sugiere contactar soporte
5. Siempre prioriza la información más reciente del contexto

CONTEXTO ACTUAL DE LA PLATAFORMA:
${contexto}

Responde de manera útil y precisa basándote en el contexto proporcionado.`;

  const prompt = `Pregunta del usuario: ${pregunta}

Responde basándote únicamente en la información del contexto de AcademIQ proporcionado arriba.`;

  try {
    console.log('🤖 Enviando petición a OpenRouter...');
    
    const response = await axios.post(
      "https://openrouter.ai/api/v1/chat/completions",
      {
        model: "openrouter/auto", // Usa el mejor modelo disponible automáticamente
        messages: [
          { 
            role: "system", 
            content: promptSistema
          },
          { 
            role: "user", 
            content: prompt 
          }
        ],
        max_tokens: 500, // Limitar respuesta para mantener relevancia
        temperature: 0.7, // Balance entre creatividad y precisión
        top_p: 0.9,
        frequency_penalty: 0.0,
        presence_penalty: 0.0
      },
      {
        headers: {
          Authorization: `Bearer ${process.env.OPENROUTER_API_KEY}`,
          "Content-Type": "application/json",
          "HTTP-Referer": "http://localhost:3001", // Para identificar la aplicación
          "X-Title": "AcademIQ IA Assistant"
        },
        timeout: 30000 // 30 segundos de timeout
      }
    );

    if (!response.data || !response.data.choices || response.data.choices.length === 0) {
      throw new Error('Respuesta vacía de la API');
    }

    const respuestaIA = response.data.choices[0].message.content.trim();
    
    console.log(' Respuesta recibida de OpenRouter');
    console.log(` Longitud: ${respuestaIA.length} caracteres`);
    
    // Log de uso para monitoreo (opcional)
    if (response.data.usage) {
      console.log(` Tokens usados: ${response.data.usage.total_tokens}`);
    }

    return respuestaIA;

  } catch (error) {
    console.error(" Error en llamada a IA:", error.message);
    
    // Logging detallado del error
    if (error.response) {
      console.error(" Status:", error.response.status);
      console.error(" Status Text:", error.response.statusText);
      console.error(" Response Data:", JSON.stringify(error.response.data, null, 2));
      
      // Errores específicos de la API
      if (error.response.status === 401) {
        return " Error de autenticación con el servicio de IA. Por favor contacta al administrador.";
      } else if (error.response.status === 429) {
        return " El servicio de IA está temporalmente sobrecargado. Inténtalo de nuevo en unos momentos.";
      } else if (error.response.status >= 500) {
        return " El servicio de IA está experimentando problemas técnicos. Inténtalo más tarde.";
      }
    } else if (error.code === 'ECONNABORTED') {
      return " La consulta tardó demasiado tiempo. Inténtalo con una pregunta más específica.";
    } else if (error.code === 'ENOTFOUND' || error.code === 'ECONNREFUSED') {
      return " No se pudo conectar al servicio de IA. Verifica tu conexión a internet.";
    }
    
    // Error genérico con sugerencias
    return ` Lo siento, no pude procesar tu pregunta en este momento. 

Mientras tanto, puedes:
- Explorar los cursos disponibles en la plataforma
- Buscar videos por asignatura
- Revisar las preguntas frecuentes
- Contactar al soporte técnico

Inténtalo de nuevo en unos momentos.`;
  }
};

// Función auxiliar para validar la configuración
const validarConfiguracion = () => {
  if (!process.env.OPENROUTER_API_KEY) {
    console.error(' OPENROUTER_API_KEY no está configurada en el archivo .env');
    return false;
  }
  
  if (process.env.OPENROUTER_API_KEY.length < 20) {
    console.error(' OPENROUTER_API_KEY parece inválida (muy corta)');
    return false;
  }
  
  console.log(' Configuración de IA validada correctamente');
  return true;
};

// Función para probar la conectividad
const probarConectividad = async () => {
  try {
    console.log(' Probando conectividad con OpenRouter...');
    
    const response = await axios.post(
      "https://openrouter.ai/api/v1/chat/completions",
      {
        model: "openrouter/auto",
        messages: [
          { role: "user", content: "Hola, ¿funcionas correctamente?" }
        ],
        max_tokens: 10
      },
      {
        headers: {
          Authorization: `Bearer ${process.env.OPENROUTER_API_KEY}`,
          "Content-Type": "application/json"
        },
        timeout: 10000
      }
    );
    
    console.log(' Conectividad con IA confirmada');
    return true;
    
  } catch (error) {
    console.error(' Error de conectividad:', error.message);
    return false;
  }
};

module.exports = { 
  responderIA, 
  validarConfiguracion, 
  probarConectividad 
};