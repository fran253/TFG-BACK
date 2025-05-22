using System.Collections.Generic;
using System.Threading.Tasks;
using TFG_BACK.Models.DTOs;

public interface IQuizManagementService
{
    Task<int> CrearQuizCompletoAsync(CrearQuizCompletoDTO quizDTO);
    Task<ResultadoQuizDTO> ProcesarRespuestasAsync(ResponderQuizDTO respuestasDTO);
    Task<EstadisticasQuizDTO> ObtenerEstadisticasQuizAsync(int idQuiz);
    Task<bool> EliminarQuizCompletoAsync(int idQuiz, int idUsuario);
    Task<Quiz> ObtenerQuizParaResponderAsync(int idQuiz); // Sin mostrar respuestas correctas
    Task<Quiz> ObtenerQuizCompletoAsync(int idQuiz); // Con todas las respuestas
}
