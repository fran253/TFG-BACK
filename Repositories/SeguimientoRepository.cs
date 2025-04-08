using Dapper;
using MySql.Data.MySqlClient;
using TFG_BACK.Models;

namespace TFG_BACK.Repositories
{
    public class SeguimientoRepository : ISeguimientoRepository
    {
        private readonly string _connectionString;

        public SeguimientoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Seguimiento>> GetAllAsync()
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = "SELECT IdSeguimiento, IdAlumno, IdProfesor FROM Seguimiento";
            var seguimientos = await connection.QueryAsync<Seguimiento>(query);
            return seguimientos.ToList();
        }

        public async Task<Seguimiento> GetByIdAsync(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = "SELECT IdSeguimiento, IdAlumno, IdProfesor FROM Seguimiento WHERE IdSeguimiento = @Id";
            return await connection.QueryFirstOrDefaultAsync<Seguimiento>(query, new { Id = id });
        }

        public async Task AddAsync(Seguimiento seguimiento)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = @"INSERT INTO Seguimiento (IdAlumno, IdProfesor) 
                          VALUES (@IdAlumno, @IdProfesor)";
            await connection.ExecuteAsync(query, seguimiento);
        }

        public async Task UpdateAsync(Seguimiento seguimiento)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = @"UPDATE Seguimiento 
                          SET IdAlumno = @IdAlumno, IdProfesor = @IdProfesor 
                          WHERE IdSeguimiento = @IdSeguimiento";
            await connection.ExecuteAsync(query, seguimiento);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = "DELETE FROM Seguimiento WHERE IdSeguimiento = @Id";
            var result = await connection.ExecuteAsync(query, new { Id = id });
            return result > 0;
        }
    }
}
