public class UsuarioEstadisticasDTO
{
    public int IdUsuario { get; set; }
    public int TotalCursos { get; set; }
    public int TotalVideos { get; set; }
    public int TotalQuizzes { get; set; }
    public DateTime? FechaUltimaActividad { get; set; }
}