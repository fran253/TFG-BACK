using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TuProyecto.Api.DTOs
{
    public class RegistrarCursoRequest
    {
        [Required]
        [MaxLength(255)]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        // Archivo de imagen para la portada del curso
        public IFormFile Imagen { get; set; }

        [Required]
        public int IdUsuarioCreador { get; set; }
    }
}