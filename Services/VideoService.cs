using TFG_BACK.Models;
using TFG_BACK.Repositories;

namespace TFG_BACK.Services
{
    public class VideoService : IVideoService
    {
        private readonly IVideoRepository _videoRepository;

        public VideoService(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public Task<List<Video>> GetAllAsync()
        {
            return _videoRepository.GetAllAsync();
        }

        public Task<Video> GetByIdAsync(int id)
        {
            return _videoRepository.GetByIdAsync(id);
        }

        public Task AddAsync(Video video)
        {
            return _videoRepository.AddAsync(video);
        }

        public Task UpdateAsync(Video video)
        {
            return _videoRepository.UpdateAsync(video);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _videoRepository.DeleteAsync(id);
        }
    }
}
