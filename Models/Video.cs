public class Video
{
    public int IdVideo { get; set; }
    public string NombreVideo { get; set; }
    public string Descripcion { get; set; }
    public string MeGusta { get; set; }
    public string Miniatura { get; set; }
    public string IdTablaMinutos { get; set; }

    public int IdAsignatura { get; set; }
    public Asignatura Asignatura { get; set; }

    public int IdUsuario { get; set; }
    public Usuario Usuario { get; set; }
}
