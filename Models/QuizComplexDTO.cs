using System.ComponentModel.DataAnnotations;

namespace TFG_BACK.Models.DTOs
{
    // DTO para crear un quiz completo con preguntas y respuestas
    public class CrearQuizCompletoDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Nombre { get; set; }
        
        public string? Descripcion { get; set; }
        
        [Required]
        public int IdUsuario { get; set; }
        
        [Required]
        [MaxLength(20, ErrorMessage = "Un quiz no puede tener más de 20 preguntas")]
        public List<CrearPreguntaDTO> Preguntas { get; set; } = new List<CrearPreguntaDTO>();
    }
    
    // DTO para crear una pregunta con sus respuestas
    public class CrearPreguntaDTO
    {
        [Required]
        public string Descripcion { get; set; }
        
        [Required]
        [MaxLength(4, ErrorMessage = "Una pregunta no puede tener más de 4 respuestas")]
        [MinLength(2, ErrorMessage = "Una pregunta debe tener al menos 2 respuestas")]
        public List<CrearRespuestaDTO> Respuestas { get; set; } = new List<CrearRespuestaDTO>();
    }
    
    // DTO para crear una respuesta
    public class CrearRespuestaDTO
    {
        [Required]
        [MaxLength(255)]
        public string Texto { get; set; }
        
        public bool EsCorrecta { get; set; } = false;
    }
    
    // DTO para responder un quiz
    public class ResponderQuizDTO
    {
        [Required]
        public int IdQuiz { get; set; }
        
        [Required]
        public int IdUsuario { get; set; }
        
        [Required]
        public List<RespuestaUsuarioDTO> Respuestas { get; set; } = new List<RespuestaUsuarioDTO>();
    }
    
    // DTO para una respuesta del usuario
    public class RespuestaUsuarioDTO
    {
        [Required]
        public int IdPregunta { get; set; }
        
        [Required]
        public List<int> IdRespuestasSeleccionadas { get; set; } = new List<int>();
    }
    
    // DTO para el resultado del quiz
    public class ResultadoQuizDTO
    {
        public int TotalPreguntas { get; set; }
        public int RespuestasCorrectas { get; set; }
        public decimal Porcentaje { get; set; }
        public List<ResultadoDetallePreguntaDTO> Detalles { get; set; } = new List<ResultadoDetallePreguntaDTO>();
    }
    
    // DTO para el detalle del resultado por pregunta
    public class ResultadoDetallePreguntaDTO
    {
        public int IdPregunta { get; set; }
        public string Descripcion { get; set; }
        public List<RespuestaDetalleDTO> Respuestas { get; set; } = new List<RespuestaDetalleDTO>();
        public List<int> RespuestasSeleccionadas { get; set; } = new List<int>();
        public bool EsCorrecta { get; set; }
    }
    
    // DTO para mostrar respuesta con información de corrección
    public class RespuestaDetalleDTO
    {
        public int IdRespuesta { get; set; }
        public string Texto { get; set; }
        public bool EsCorrecta { get; set; }
        public bool FueSeleccionada { get; set; }
    }
    
    // DTO para valorar un quiz
    public class ValorarQuizDTO
    {
        [Required]
        public int IdQuiz { get; set; }
        
        [Required]
        public int IdUsuario { get; set; }
        
        [Required]
        [Range(1, 5)]
        public int Puntuacion { get; set; }
        
        [MaxLength(500)]
        public string? Comentario { get; set; }
    }
    
    // DTO para actualizar una pregunta específica
    public class ActualizarPreguntaDTO
    {
        [Required]
        public int IdPregunta { get; set; }
        
        [Required]
        public string Descripcion { get; set; }
        
        public int Orden { get; set; }
    }
    
    // DTO para actualizar una respuesta específica
    public class ActualizarRespuestaDTO
    {
        [Required]
        public int IdRespuesta { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Texto { get; set; }
        
        public bool EsCorrecta { get; set; }
        
        public int Orden { get; set; }
    }
    
    // DTO para estadísticas de un quiz
    public class EstadisticasQuizDTO
    {
        public int IdQuiz { get; set; }
        public string Nombre { get; set; }
        public int TotalPreguntas { get; set; }
        public int TotalRespuestas { get; set; }
        public decimal PromedioCalificaciones { get; set; }
        public double PromedioValoraciones { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string NombreCreador { get; set; }
    }

    // DTO para respuesta de creación de quiz
    public class QuizCreadoResponseDTO
    {
        public int IdQuiz { get; set; }
        public string Mensaje { get; set; }
        public string Nombre { get; set; }
        public string NombreCreador { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    // DTO para validación de quiz
    public class ValidacionQuizDTO
    {
        public bool EsValido { get; set; }
        public List<string> Errores { get; set; } = new List<string>();
        public List<string> Advertencias { get; set; } = new List<string>();
    }
}