using Dapper;
using MySql.Data.MySqlClient;
using TFG_BACK.Models;

namespace TFG_BACK.Repositories
{
    public class PreferenciasRepository : IPreferenciasRepository
    {
        private readonly string _connectionString;

        public PreferenciasRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Preferencias>> GetAllAsync()
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = "SELECT IdPreferencia, ColorFondo, ColorBordes, ImagenFondo FROM Preferencias";
            var prefs = await connection.QueryAsync<Preferencias>(query);
            return prefs.ToList();
        }

        public async Task<Preferencias> GetByIdAsync(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = "SELECT IdPreferencia, ColorFondo, ColorBordes, ImagenFondo FROM Preferencias WHERE IdPreferencia = @Id";
            return await connection.QueryFirstOrDefaultAsync<Preferencias>(query, new { Id = id });
        }

        public async Task AddAsync(Preferencias pref)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = @"INSERT INTO Preferencias (ColorFondo, ColorBordes, ImagenFondo) 
                          VALUES (@ColorFondo, @ColorBordes, @ImagenFondo)";
            await connection.ExecuteAsync(query, pref);
        }

        public async Task UpdateAsync(Preferencias pref)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = @"UPDATE Preferencias 
                          SET ColorFondo = @ColorFondo, ColorBordes = @ColorBordes, ImagenFondo = @ImagenFondo 
                          WHERE IdPreferencia = @IdPreferencia";
            await connection.ExecuteAsync(query, pref);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = "DELETE FROM Preferencias WHERE IdPreferencia = @Id";
            var result = await connection.ExecuteAsync(query, new { Id = id });
            return result > 0;
        }
    }
}
