using System.ComponentModel.DataAnnotations;

namespace TFG_BACK.Models.DTOs
{
    // DTO para crear un nuevo quiz con todas sus preguntas
    public class CrearQuizDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Nombre { get; set; }
        
        public string? Descripcion { get; set; }
        
        [Required]
        public int IdAsignatura { get; set; }
        
        [Required]
        public int IdUsuario { get; set; }
        
        public int? IdCurso { get; set; }
        
        [Required]
        [MaxLength(20, ErrorMessage = "Un quiz no puede tener más de 20 preguntas")]
        public List<PreguntaDTO> Preguntas { get; set; }
    }
    
    // DTO para representar una pregunta con sus opciones
    public class PreguntaDTO
    {
        [Required]
        public string Pregunta { get; set; }
        
        [Required]
        [MaxLength(4, ErrorMessage = "Una pregunta no puede tener más de 4 opciones")]
        [MinLength(2, ErrorMessage = "Una pregunta debe tener al menos 2 opciones")]
        public List<string> Opciones { get; set; }
        
        [Required]
        [MinLength(1, ErrorMessage = "Debe seleccionar al menos una respuesta correcta")]
        public List<int> RespuestasCorrectas { get; set; } // Índices de las respuestas correctas (0-based)
    }
    
    // DTO para responder un quiz
    public class ResponderQuizDTO
    {
        [Required]
        public int IdQuiz { get; set; }
        
        [Required]
        public int IdUsuario { get; set; }
        
        [Required]
        public List<RespuestaPreguntaDTO> Respuestas { get; set; }
    }
    
    // DTO para una respuesta individual a una pregunta
    public class RespuestaPreguntaDTO
    {
        [Required]
        public int IdDetalleQuiz { get; set; }
        
        [Required]
        public List<int> IndicesSeleccionados { get; set; } // Índices de las opciones seleccionadas
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
    
    // DTO para mostrar el resultado después de responder un quiz
    public class ResultadoQuizDTO
    {
        public int TotalPreguntas { get; set; }
        public int RespuestasCorrectas { get; set; }
        public decimal Porcentaje { get; set; }
        public List<ResultadoDetallePreguntaDTO> ResultadosDetallados { get; set; }
    }
    
    // DTO para mostrar el resultado detallado de cada pregunta
    public class ResultadoDetallePreguntaDTO
    {
        public int IdDetalleQuiz { get; set; }
        public string Pregunta { get; set; }
        public List<string> Opciones { get; set; }
        public List<int> RespuestasCorrectas { get; set; }
        public List<int> RespuestasSeleccionadas { get; set; }
        public bool EsCorrecta { get; set; }
    }
}