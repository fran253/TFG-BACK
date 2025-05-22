public interface IPeticionProfesorService
{
    Task CrearPeticionAsync(PeticionProfesorDTO dto);
    Task<List<PeticionProfesor>> ObtenerTodasAsync();
    Task AprobarPeticionAsync(int id);
    Task RechazarPeticionAsync(int id);
}
