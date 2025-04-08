using TFG_BACK.Models;
using TFG_BACK.Repositories;

namespace TFG_BACK.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;

        public QuizService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public Task<List<Quiz>> GetAllAsync()
        {
            return _quizRepository.GetAllAsync();
        }

        public Task<Quiz> GetByIdAsync(int id)
        {
            return _quizRepository.GetByIdAsync(id);
        }

        public Task AddAsync(Quiz quiz)
        {
            return _quizRepository.AddAsync(quiz);
        }

        public Task UpdateAsync(Quiz quiz)
        {
            return _quizRepository.UpdateAsync(quiz);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _quizRepository.DeleteAsync(id);
        }
    }
}
