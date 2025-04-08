using Dapper;
using MySql.Data.MySqlClient;
using TFG_BACK.Models;

namespace TFG_BACK.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly string _connectionString;

        public QuizRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Quiz>> GetAllAsync()
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = "SELECT IdQuizz, Nombre, Descripcion, IdAsignatura, IdUsuario FROM Quiz";
            var quizzList = await connection.QueryAsync<Quiz>(query);
            return quizzList.ToList();
        }

        public async Task<Quiz> GetByIdAsync(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = "SELECT IdQuizz, Nombre, Descripcion, IdAsignatura, IdUsuario FROM Quiz WHERE IdQuizz = @Id";
            return await connection.QueryFirstOrDefaultAsync<Quiz>(query, new { Id = id });
        }

        public async Task AddAsync(Quiz quiz)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = @"INSERT INTO Quiz (Nombre, Descripcion, IdAsignatura, IdUsuario)
                          VALUES (@Nombre, @Descripcion, @IdAsignatura, @IdUsuario)";
            await connection.ExecuteAsync(query, quiz);
        }

        public async Task UpdateAsync(Quiz quiz)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = @"UPDATE Quiz 
                          SET Nombre = @Nombre, Descripcion = @Descripcion, 
                              IdAsignatura = @IdAsignatura, IdUsuario = @IdUsuario
                          WHERE IdQuizz = @IdQuizz";
            await connection.ExecuteAsync(query, quiz);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = "DELETE FROM Quiz WHERE IdQuizz = @Id";
            var result = await connection.ExecuteAsync(query, new { Id = id });
            return result > 0;
        }
    }
}
