using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TFG_BACK.Models.DTOs;

public class QuizService : IQuizService
{
    private readonly AcademIQDbContext _context;

    public QuizService(AcademIQDbContext context)
    {
        _context = context;
    }

    public async Task<List<Quiz>> GetAllAsync()
    {
        return await _context.Quizzes
            .Include(q => q.Asignatura)
            .Include(q => q.Usuario)
            .ToListAsync();
    }

    public async Task<Quiz?> GetByIdAsync(int id)
    {
        return await _context.Quizzes
            .Include(q => q.Detalles)
            .Include(q => q.Asignatura)
            .Include(q => q.Usuario)
            .FirstOrDefaultAsync(q => q.IdQuiz == id);
    }

    public async Task<int> AddAsync(Quiz quiz)
    {
        _context.Quizzes.Add(quiz);
        await _context.SaveChangesAsync();
        return quiz.IdQuiz;
    }

    public async Task UpdateAsync(Quiz quiz)
    {
        _context.Quizzes.Update(quiz);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var quiz = await _context.Quizzes.FindAsync(id);
        if (quiz != null)
        {
            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Quiz>> GetByCursoAsync(int idCurso)
    {
        return await _context.Quizzes
            .Where(q => q.IdCurso == idCurso)
            .Include(q => q.Asignatura)
            .Include(q => q.Usuario)
            .ToListAsync();
    }

    public async Task<List<Quiz>> GetByAsignaturaAsync(int idAsignatura)
    {
        return await _context.Quizzes
            .Where(q => q.IdAsignatura == idAsignatura)
            .Include(q => q.Usuario)
            .Include(q => q.Curso)
            .ToListAsync();
    }

    public async Task<List<Quiz>> GetByCursoYAsignaturaAsync(int idCurso, int idAsignatura)
    {
        return await _context.Quizzes
            .Where(q => q.IdCurso == idCurso && q.IdAsignatura == idAsignatura)
            .Include(q => q.Usuario)
            .Include(q => q.Curso)
            .ToListAsync();
    }

    // Implementación de los nuevos métodos
    public async Task<int> CrearQuizCompletoAsync(CrearQuizDTO quizDTO)
    {
        // Validar número máximo de preguntas
        if (quizDTO.Preguntas.Count > 20)
            throw new ArgumentException("Un quiz no puede tener más de 20 preguntas.");

        // Crear el quiz
        var quiz = new Quiz
        {
            Nombre = quizDTO.Nombre,
            Descripcion = quizDTO.Descripcion,
            IdAsignatura = quizDTO.IdAsignatura,
            IdUsuario = quizDTO.IdUsuario,
            IdCurso = quizDTO.IdCurso
        };

        _context.Quizzes.Add(quiz);
        await _context.SaveChangesAsync();

        // Crear las preguntas (DetalleQuiz)
        foreach (var preguntaDTO in quizDTO.Preguntas)
        {
            // Validar número máximo de opciones
            if (preguntaDTO.Opciones.Count > 4)
                throw new ArgumentException($"La pregunta '{preguntaDTO.Pregunta}' tiene más de 4 opciones.");

            // Validar que hay al menos una respuesta correcta
            if (preguntaDTO.RespuestasCorrectas.Count == 0)
                throw new ArgumentException($"La pregunta '{preguntaDTO.Pregunta}' debe tener al menos una respuesta correcta.");

            // Validar que los índices de respuestas correctas son válidos
            foreach (var respuestaCorrecta in preguntaDTO.RespuestasCorrectas)
            {
                if (respuestaCorrecta < 0 || respuestaCorrecta >= preguntaDTO.Opciones.Count)
                    throw new ArgumentException($"Índice de respuesta correcta inválido en la pregunta '{preguntaDTO.Pregunta}'.");
            }

            var detalle = new DetalleQuiz
            {
                IdQuiz = quiz.IdQuiz,
                Pregunta = preguntaDTO.Pregunta,
                Opciones = JsonSerializer.Serialize(preguntaDTO.Opciones),
                RespuestasCorrectas = string.Join(",", preguntaDTO.RespuestasCorrectas)
            };

            _context.DetallesQuiz.Add(detalle);
        }

        await _context.SaveChangesAsync();
        return quiz.IdQuiz;
    }

    public async Task<ResultadoQuizDTO> ProcesarRespuestasAsync(ResponderQuizDTO respuestasDTO)
    {
        // Obtener el quiz y sus detalles
        var quiz = await _context.Quizzes
            .Include(q => q.Detalles)
            .FirstOrDefaultAsync(q => q.IdQuiz == respuestasDTO.IdQuiz);

        if (quiz == null)
            throw new KeyNotFoundException($"No se encontró el quiz con ID {respuestasDTO.IdQuiz}");

        // Preparar resultado
        var resultado = new ResultadoQuizDTO
        {
            TotalPreguntas = quiz.Detalles.Count,
            RespuestasCorrectas = 0,
            ResultadosDetallados = new List<ResultadoDetallePreguntaDTO>()
        };

        // Procesar cada respuesta
        foreach (var respuestaDTO in respuestasDTO.Respuestas)
        {
            var detalle = quiz.Detalles.FirstOrDefault(d => d.IdDetalleQuiz == respuestaDTO.IdDetalleQuiz);
            
            if (detalle == null)
                continue;

            var opciones = JsonSerializer.Deserialize<List<string>>(detalle.Opciones) ?? new List<string>();
            var respuestasCorrectas = detalle.RespuestasCorrectas.Split(',')
                .Select(int.Parse)
                .ToList();

            // Convertir a conjuntos para comparar
            var conjuntoRespuestasCorrectas = new HashSet<int>(respuestasCorrectas);
            var conjuntoRespuestasSeleccionadas = new HashSet<int>(respuestaDTO.IndicesSeleccionados);

            // Determinar si la respuesta es correcta (deben ser exactamente iguales)
            bool esCorrecta = conjuntoRespuestasCorrectas.SetEquals(conjuntoRespuestasSeleccionadas);
            
            if (esCorrecta)
                resultado.RespuestasCorrectas++;

            // Añadir al resultado detallado
            resultado.ResultadosDetallados.Add(new ResultadoDetallePreguntaDTO
            {
                IdDetalleQuiz = detalle.IdDetalleQuiz,
                Pregunta = detalle.Pregunta,
                Opciones = opciones,
                RespuestasCorrectas = respuestasCorrectas,
                RespuestasSeleccionadas = respuestaDTO.IndicesSeleccionados,
                EsCorrecta = esCorrecta
            });
        }

        // Calcular porcentaje
        resultado.Porcentaje = resultado.TotalPreguntas > 0 
            ? (decimal)resultado.RespuestasCorrectas / resultado.TotalPreguntas * 100 
            : 0;

        // Guardar resultado en la base de datos
        var resultadoQuiz = new ResultadoQuiz
        {
            IdUsuario = respuestasDTO.IdUsuario,
            IdQuiz = respuestasDTO.IdQuiz,
            Puntuacion = resultado.Porcentaje,
            Fecha = DateTime.Now,
            RespuestasSeleccionadas = JsonSerializer.Serialize(
                respuestasDTO.Respuestas.Select(r => new { r.IdDetalleQuiz, r.IndicesSeleccionados }).ToList()
            )
        };

        _context.ResultadosQuiz.Add(resultadoQuiz);
        await _context.SaveChangesAsync();

        return resultado;
    }

    public async Task<List<Quiz>> GetQuizzesPopularesAsync(int limite = 10)
    {
        return await _context.Quizzes
            .OrderByDescending(q => q.Resultados.Count)
            .Take(limite)
            .Include(q => q.Asignatura)
            .Include(q => q.Usuario)
            .ToListAsync();
    }

    public async Task<List<Quiz>> GetQuizzesPorUsuarioAsync(int idUsuario)
    {
        return await _context.Quizzes
            .Where(q => q.IdUsuario == idUsuario)
            .Include(q => q.Asignatura)
            .Include(q => q.Curso)
            .ToListAsync();
    }

    public async Task<int> GetTotalPreguntasAsync(int idQuiz)
    {
        return await _context.DetallesQuiz
            .CountAsync(d => d.IdQuiz == idQuiz);
    }
}