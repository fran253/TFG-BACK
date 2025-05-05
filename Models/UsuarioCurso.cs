using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class UsuarioCurso
{
    [Key, Column(Order = 0)]
    public int IdUsuario { get; set; }

    [Key, Column(Order = 1)]
    public int IdCurso { get; set; }

    public Usuario Usuario { get; set; }
    public Curso Curso { get; set; }
}
