namespace TFG_BACK.Models;

public class Seguimiento
{
    public int IdSeguimiento { get; set; }
    public int IdAlumno { get; set; }
    public int IdProfesor { get; set; }

    public ICollection<Usuario> Usuarios { get; set; }
}
