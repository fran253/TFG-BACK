using TFG_BACK.Models;

namespace TFG_BACK.Services
{
    public interface IVideoService
    {
        Task<List<Video>> GetAllAsync();
        Task<Video> GetByIdAsync(int id);
        Task AddAsync(Video video);
        Task UpdateAsync(Video video);
        Task<bool> DeleteAsync(int id);
    }
}
