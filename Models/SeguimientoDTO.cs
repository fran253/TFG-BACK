using System.ComponentModel.DataAnnotations;

namespace TFG_BACK.Models.DTOs
{
    public class SeguimientoDTO
    {
        [Required]
        public int IdAlumno { get; set; }
        
        [Required]
        public int IdProfesor { get; set; }
    }
    
    public class SeguimientoResponseDTO
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string? Apellidos { get; set; }
        public string Gmail { get; set; }
        public string? Telefono { get; set; }
        public int IdRol { get; set; }
        public string? NombreRol { get; set; }
    }
}