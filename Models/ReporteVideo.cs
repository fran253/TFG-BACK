using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TuProyecto.Models
{
    [Table("ReporteVideo")] // Asegúrate de que el nombre coincida con la tabla real
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

        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }

        [ForeignKey("IdVideo")]
        public Video? Video { get; set; }
    }
}