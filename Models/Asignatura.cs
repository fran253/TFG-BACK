public class Asignatura
{
    public int IdAsignatura { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    

    public ICollection<UsuarioAsignatura> UsuarioAsignaturas { get; set; }
    public ICollection<Quiz> Quizzes { get; set; }
    public ICollection<Video> Videos { get; set; }
}
