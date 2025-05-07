using System.ComponentModel.DataAnnotations.Schema;

public class Seguimiento
{
    [ForeignKey("Alumno")]
    public int IdAlumno { get; set; }
    public Usuario Alumno { get; set; }

    [ForeignKey("Profesor")]
    public int IdProfesor { get; set; }
    public Usuario Profesor { get; set; }
}
