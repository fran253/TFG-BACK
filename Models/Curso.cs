using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Curso
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCurso { get; set; }

    [Required]
    [MaxLength(255)]
    public string Nombre { get; set; }

    public string? Imagen { get; set; }

    public string? Descripcion { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    public int IdUsuario { get; set; }

    [ForeignKey("IdUsuario")]
    public Usuario Usuario { get; set; }

    public ICollection<Asignatura> Asignaturas { get; set; }
    public ICollection<UsuarioCurso> UsuarioCursos { get; set; }
}
