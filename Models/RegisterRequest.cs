namespace TFG_BACK.Auth
{
    public class RegisterRequest
    {
        public string NombreUsuario { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contraseña { get; set; }
        public string Rol { get; set; } // "Alumno" o "Profesor"
    }
}
