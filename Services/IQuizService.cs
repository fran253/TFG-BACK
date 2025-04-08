using TFG_BACK.Models;

namespace TFG_BACK.Services
{
    public interface IQuizService
    {
        Task<List<Quiz>> GetAllAsync();
        Task<Quiz> GetByIdAsync(int id);
        Task AddAsync(Quiz quiz);
        Task UpdateAsync(Quiz quiz);
        Task<bool> DeleteAsync(int id);
    }
}
