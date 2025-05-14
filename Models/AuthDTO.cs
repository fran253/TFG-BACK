// BACKEND: TFG-BACK/Models/DTOs/AuthDTO.cs
using System.ComponentModel.DataAnnotations;

namespace TFG_BACK.Models.DTOs
{
    public class RegistroDTO
    {
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        
        public string? Apellidos { get; set; }
        
        [Required]
        [EmailAddress]
        public string Gmail { get; set; }
        
        public string? Telefono { get; set; }
        
        [Required]
        [MinLength(6)]
        public string Contraseña { get; set; }
        
        // IdRol por defecto sería el de usuario normal
        public int IdRol { get; set; } = 1; // Asumiendo que 1 es el rol de usuario estándar
    }

    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Gmail { get; set; }
        
        [Required]
        public string Contraseña { get; set; }
    }

    public class AuthResponseDTO
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Token { get; set; }
        public string Rol { get; set; }
    }
}