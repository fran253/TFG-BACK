public interface IDetalleQuizService
{
    Task<List<DetalleQuiz>> GetByQuizIdAsync(int idQuiz);
    Task<DetalleQuiz?> GetByIdAsync(int id);
    Task AddAsync(DetalleQuiz detalle);
    Task UpdateAsync(DetalleQuiz detalle);
    Task DeleteAsync(int id);
}
