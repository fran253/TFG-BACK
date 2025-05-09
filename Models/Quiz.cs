using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Quiz
{
    [Key]
    public int IdQuiz { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; }

    public string? Descripcion { get; set; }

    [ForeignKey("Asignatura")]
    public int IdAsignatura { get; set; }
    public Asignatura Asignatura { get; set; }

    [ForeignKey("Usuario")]
    public int IdUsuario { get; set; }
    public Usuario Usuario { get; set; }

    [ForeignKey("Curso")]
    public int? IdCurso { get; set; }
    public Curso? Curso { get; set; }

    public ICollection<DetalleQuiz> Detalles { get; set; }
    public ICollection<ResultadoQuiz> Resultados { get; set; }
}
