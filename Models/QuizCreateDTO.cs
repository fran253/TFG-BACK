using System.ComponentModel.DataAnnotations;

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