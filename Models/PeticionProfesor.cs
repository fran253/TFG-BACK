public class PeticionProfesor
{
    public int Id { get; set; }
    public int IdUsuario { get; set; }
    public string DocumentacionUrl { get; set; }
    public string Texto { get; set; }
    public DateTime FechaPeticion { get; set; }

    public Usuario Usuario { get; set; }
}
