namespace TFG_BACK.Models;

public class Asignatura
{
    public int IdAsignatura { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }

    public ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
    public ICollection<Video> Videos { get; set; } = new List<Video>();
}
