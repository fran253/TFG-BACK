namespace TFG_BACK.Models;

public class UsuarioAsignatura
{
    public int IdUsuario { get; set; }
    public Usuario Usuario { get; set; }

    public int IdAsignatura { get; set; }
    public Asignatura Asignatura { get; set; }
}
