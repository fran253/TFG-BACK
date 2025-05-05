using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class UsuarioAsignatura
{
    [Key, Column(Order = 0)]
    public int IdUsuario { get; set; }

    [Key, Column(Order = 1)]
    public int IdAsignatura { get; set; }

    public Usuario Usuario { get; set; }
    public Asignatura Asignatura { get; set; }
}
