const axios = require("axios");
require("dotenv").config({ path: __dirname + "/../.env" });

const responderIA = async (pregunta, contexto) => {
  const prompt = `Contexto:\n${contexto}\n\nPregunta: ${pregunta}\nRespuesta:`;

  try {
    const response = await axios.post(
      "https://openrouter.ai/api/v1/chat/completions",
      {
         model: "openrouter/auto",
        messages: [
          { role: "system", content: "Eres un asistente útil que responde dudas sobre la plataforma." },
          { role: "user", content: prompt }
        ]
      },
      {
        headers: {
          Authorization: `Bearer ${process.env.OPENROUTER_API_KEY}`,
          "Content-Type": "application/json"
        }
      }
    );

    return response.data.choices[0].message.content;
  } catch (error) {
    console.error("❌ Error en la IA:", error.message);
    if (error.response) {
      console.error("🔍 Código:", error.response.status);
      console.error("🔍 Detalles:", error.response.data);
    }
    return "Error al generar respuesta.";
  }
};

module.exports = { responderIA };
