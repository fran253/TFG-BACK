using System.ComponentModel.DataAnnotations;

namespace TFG_BACK.Models.DTOs
{
    // DTO para crear quiz básico
    public class QuizCreateDto
    {
        [Required(ErrorMessage = "El nombre del quiz es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string Nombre { get; set; }
        
        [MaxLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
        public string? Descripcion { get; set; }
        
        [Required(ErrorMessage = "El ID del usuario es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del usuario debe ser mayor a 0")]
        public int IdUsuario { get; set; }
    }

    // DTO para respuesta del quiz con información del usuario
    public class QuizResponseDto
    {
        public int IdQuiz { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int TotalPreguntas { get; set; } = 0;
    }

    // DTO para lista de quizzes
    public class QuizListDto
    {
        public int IdQuiz { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string NombreCreador { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int TotalPreguntas { get; set; } = 0;
    }

    // DTO para actualizar quiz
    public class QuizUpdateDto
    {
        [Required]
        public int IdQuiz { get; set; }
        
        [Required(ErrorMessage = "El nombre del quiz es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string Nombre { get; set; }
        
        [MaxLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
        public string? Descripcion { get; set; }
    }

    // DTO para estadísticas básicas de quiz
    public class QuizStatsDto
    {
        public int IdQuiz { get; set; }
        public string Nombre { get; set; }
        public string NombreCreador { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int TotalPreguntas { get; set; }
        public int TotalRespuestas { get; set; }
        public int VecesRespondido { get; set; }
    }
}