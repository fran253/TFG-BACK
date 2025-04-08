using Dapper;
using MySql.Data.MySqlClient;
using TFG_BACK.Models;

namespace TFG_BACK.Repositories
{
    public class DetalleQuizzRepository : IDetalleQuizzRepository
    {
        private readonly string _connectionString;

        public DetalleQuizzRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<DetalleQuizz>> GetAllAsync()
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = "SELECT IdDetalleQuizz, IdQuizz, Pregunta, Opciones FROM DetalleQuizz";
            var detalles = await connection.QueryAsync<DetalleQuizz>(query);
            return detalles.ToList();
        }

        public async Task<DetalleQuizz> GetByIdAsync(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = "SELECT IdDetalleQuizz, IdQuizz, Pregunta, Opciones FROM DetalleQuizz WHERE IdDetalleQuizz = @Id";
            return await connection.QueryFirstOrDefaultAsync<DetalleQuizz>(query, new { Id = id });
        }

        public async Task AddAsync(DetalleQuizz detalle)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = @"INSERT INTO DetalleQuizz (IdQuizz, Pregunta, Opciones)
                          VALUES (@IdQuizz, @Pregunta, @Opciones)";
            await connection.ExecuteAsync(query, detalle);
        }

        public async Task UpdateAsync(DetalleQuizz detalle)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = @"UPDATE DetalleQuizz
                          SET IdQuizz = @IdQuizz, Pregunta = @Pregunta, Opciones = @Opciones
                          WHERE IdDetalleQuizz = @IdDetalleQuizz";
            await connection.ExecuteAsync(query, detalle);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = "DELETE FROM DetalleQuizz WHERE IdDetalleQuizz = @Id";
            var result = await connection.ExecuteAsync(query, new { Id = id });
            return result > 0;
        }
    }
}
