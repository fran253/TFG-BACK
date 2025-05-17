using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuProyecto.Models
{
    public class ReporteVideo
    {
        [Key]
        public int IdReporte { get; set; }

        [Required]
        public int IdVideo { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        [Required]
        [MaxLength(100)]
        public string Motivo { get; set; } = string.Empty;

        public DateTime Fecha { get; set; } = DateTime.Now;

        // Relaciones
        public Usuario? Usuario { get; set; }
        public Video? Video { get; set; }
    }
}
