public class Quiz
{
    public int IdQuizz { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }

    public int IdAsignatura { get; set; }
    public Asignatura Asignatura { get; set; }

    public int IdUsuario { get; set; }
    public Usuario Usuario { get; set; }

    public ICollection<DetalleQuizz> Detalles { get; set; }
}
