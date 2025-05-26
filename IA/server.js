// IA/server.js
const express = require("express");
const cors = require("cors");
const { responderIA } = require("./ia/mistral");
const app = express();
app.use(cors());
app.use(express.json());

const datos = require("./datosIA.json");

app.post("/api/ia", async (req, res) => {
  const { pregunta } = req.body;
  if (!pregunta) return res.status(400).json({ error: "Pregunta requerida" });

  const contexto = datos.map(d => JSON.stringify(d)).join("\n");
  const respuesta = await responderIA(pregunta, contexto);
  res.json({ respuesta });
});

app.listen(3001, () => console.log("🔁 Servidor IA escuchando en puerto 3001"));
