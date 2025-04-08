using TFG_BACK.Models;

namespace TFG_BACK.Repositories
{
    public interface IQuizRepository
    {
        Task<List<Quiz>> GetAllAsync();
        Task<Quiz> GetByIdAsync(int id);
        Task AddAsync(Quiz quiz);
        Task UpdateAsync(Quiz quiz);
        Task<bool> DeleteAsync(int id);
    }
}
