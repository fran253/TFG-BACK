using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TFG_BACK.Models.DTOs;

public class QuizManagementService : IQuizManagementService
{
    private readonly AcademIQDbContext _context;
    private readonly IQuizService _quizService;
    private readonly IPreguntaService _preguntaService;
    private readonly IRespuestaService _respuestaService;

    public QuizManagementService(
        AcademIQDbContext context,
        IQuizService quizService,
        IPreguntaService preguntaService,
        IRespuestaService respuestaService)
    {
        _context = context;
        _quizService = quizService;
        _preguntaService = preguntaService;
        _respuestaService = respuestaService;
    }

    public async Task<int> CrearQuizCompletoAsync(CrearQuizCompletoDTO quizDTO)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        
        try
        {
            // Validar límites
            if (quizDTO.Preguntas.Count > 20)
                throw new ArgumentException("Un quiz no puede tener más de 20 preguntas.");

            // Crear el quiz
            var quiz = new Quiz
            {
                Nombre = quizDTO.Nombre,
                Descripcion = quizDTO.Descripcion,
                IdUsuario = quizDTO.IdUsuario,
                FechaCreacion = DateTime.Now
            };

            var idQuiz = await _quizService.AddAsync(quiz);

            // Crear preguntas y respuestas
            int ordenPregunta = 1;
            foreach (var preguntaDTO in quizDTO.Preguntas)
            {
                // Validar respuestas
                if (preguntaDTO.Respuestas.Count > 4)
                    throw new ArgumentException($"La pregunta '{preguntaDTO.Descripcion}' tiene más de 4 respuestas.");

                if (preguntaDTO.Respuestas.Count < 2)
                    throw new ArgumentException($"La pregunta '{preguntaDTO.Descripcion}' debe tener al menos 2 respuestas.");

                if (!preguntaDTO.Respuestas.Any(r => r.EsCorrecta))
                    throw new ArgumentException($"La pregunta '{preguntaDTO.Descripcion}' debe tener al menos una respuesta correcta.");

                // Crear pregunta
                var pregunta = new Pregunta
                {
                    IdQuiz = idQuiz,
                    Descripcion = preguntaDTO.Descripcion,
                    Orden = ordenPregunta++
                };

                var idPregunta = await _preguntaService.AddAsync(pregunta);

                // Crear respuestas
                int ordenRespuesta = 1;
                foreach (var respuestaDTO in preguntaDTO.Respuestas)
                {
                    var respuesta = new Respuesta
                    {
                        IdPregunta = idPregunta,
                        Texto = respuestaDTO.Texto,
                        EsCorrecta = respuestaDTO.EsCorrecta,
                        Orden = ordenRespuesta++
                    };

                    await _respuestaService.AddAsync(respuesta);
                }
            }

            await transaction.CommitAsync();
            return idQuiz;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<ResultadoQuizDTO> ProcesarRespuestasAsync(ResponderQuizDTO respuestasDTO)
    {
        var quiz = await _context.Quizzes
            .Include(q => q.Preguntas)
                .ThenInclude(p => p.Respuestas)
            .FirstOrDefaultAsync(q => q.IdQuiz == respuestasDTO.IdQuiz);

        if (quiz == null)
            throw new KeyNotFoundException($"No se encontró el quiz con ID {respuestasDTO.IdQuiz}");

        var resultado = new ResultadoQuizDTO
        {
            TotalPreguntas = quiz.Preguntas.Count,
            RespuestasCorrectas = 0,
            Detalles = new List<ResultadoDetallePreguntaDTO>()
        };

        // Procesar cada respuesta
        foreach (var pregunta in quiz.Preguntas.OrderBy(p => p.Orden))
        {
            var respuestaUsuario = respuestasDTO.Respuestas
                .FirstOrDefault(r => r.IdPregunta == pregunta.IdPregunta);

            var respuestasCorrectas = pregunta.Respuestas
                .Where(r => r.EsCorrecta)
                .Select(r => r.IdRespuesta)
                .ToHashSet();

            var respuestasSeleccionadas = respuestaUsuario?.IdRespuestasSeleccionadas?.ToHashSet() 
                ?? new HashSet<int>();

            // Verificar si la respuesta es correcta
            bool esCorrecta = respuestasCorrectas.SetEquals(respuestasSeleccionadas);
            
            if (esCorrecta)
                resultado.RespuestasCorrectas++;

            // Crear detalle de respuesta
            var detalleRespuestas = pregunta.Respuestas.OrderBy(r => r.Orden).Select(r => new RespuestaDetalleDTO
            {
                IdRespuesta = r.IdRespuesta,
                Texto = r.Texto,
                EsCorrecta = r.EsCorrecta,
                FueSeleccionada = respuestasSeleccionadas.Contains(r.IdRespuesta)
            }).ToList();

            resultado.Detalles.Add(new ResultadoDetallePreguntaDTO
            {
                IdPregunta = pregunta.IdPregunta,
                Descripcion = pregunta.Descripcion,
                Respuestas = detalleRespuestas,
                RespuestasSeleccionadas = respuestasSeleccionadas.ToList(),
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
            RespuestasSeleccionadas = JsonSerializer.Serialize(respuestasDTO.Respuestas),
            Fecha = DateTime.Now
        };

        _context.ResultadosQuiz.Add(resultadoQuiz);
        await _context.SaveChangesAsync();

        return resultado;
    }

    public async Task<EstadisticasQuizDTO> ObtenerEstadisticasQuizAsync(int idQuiz)
    {
        var quiz = await _context.Quizzes
            .Include(q => q.Usuario)
            .Include(q => q.Preguntas)
            .Include(q => q.Resultados)
            .Include(q => q.Valoraciones)
            .FirstOrDefaultAsync(q => q.IdQuiz == idQuiz);

        if (quiz == null)
            throw new KeyNotFoundException($"No se encontró el quiz con ID {idQuiz}");

        var totalRespuestas = quiz.Preguntas.Sum(p => p.Respuestas.Count);
        var promedioCalificaciones = quiz.Resultados.Any() 
            ? quiz.Resultados.Average(r => r.Puntuacion) 
            : 0;
        var promedioValoraciones = quiz.Valoraciones.Any() 
            ? quiz.Valoraciones.Average(v => v.Puntuacion) 
            : 0;

        return new EstadisticasQuizDTO
        {
            IdQuiz = quiz.IdQuiz,
            Nombre = quiz.Nombre,
            TotalPreguntas = quiz.Preguntas.Count,
            TotalRespuestas = totalRespuestas,
            PromedioCalificaciones = promedioCalificaciones,
            PromedioValoraciones = promedioValoraciones,
            FechaCreacion = quiz.FechaCreacion,
            NombreCreador = quiz.Usuario.Nombre
        };
    }

    public async Task<bool> EliminarQuizCompletoAsync(int idQuiz, int idUsuario)
    {
        var quiz = await _context.Quizzes
            .FirstOrDefaultAsync(q => q.IdQuiz == idQuiz && q.IdUsuario == idUsuario);

        if (quiz == null)
            return false;

        // Entity Framework se encargará de eliminar las relaciones en cascada
        await _quizService.DeleteAsync(idQuiz);
        return true;
    }

    public async Task<Quiz> ObtenerQuizParaResponderAsync(int idQuiz)
    {
        var quiz = await _context.Quizzes
            .Include(q => q.Usuario)
            .Include(q => q.Preguntas.OrderBy(p => p.Orden))
                .ThenInclude(p => p.Respuestas.Where(r => !r.EsCorrecta).OrderBy(r => r.Orden)) // Solo respuestas no correctas
            .FirstOrDefaultAsync(q => q.IdQuiz == idQuiz);

        if (quiz == null)
            throw new KeyNotFoundException($"No se encontró el quiz con ID {idQuiz}");

        // Mezclar respuestas para cada pregunta
        foreach (var pregunta in quiz.Preguntas)
        {
            var todasLasRespuestas = await _context.Respuestas
                .Where(r => r.IdPregunta == pregunta.IdPregunta)
                .OrderBy(r => Guid.NewGuid()) // Orden aleatorio
                .ToListAsync();
            
            pregunta.Respuestas = todasLasRespuestas;
        }

        return quiz;
    }

    public async Task<Quiz> ObtenerQuizCompletoAsync(int idQuiz)
    {
        var quiz = await _context.Quizzes
            .Include(q => q.Usuario)
            .Include(q => q.Preguntas.OrderBy(p => p.Orden))
                .ThenInclude(p => p.Respuestas.OrderBy(r => r.Orden))
            .FirstOrDefaultAsync(q => q.IdQuiz == idQuiz);

        if (quiz == null)
            throw new KeyNotFoundException($"No se encontró el quiz con ID {idQuiz}");

        return quiz;
    }
}