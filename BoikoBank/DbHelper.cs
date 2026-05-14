using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
 
namespace BoikoBank
{
    public static class DbHelper
    {
        // Строка подключения (измените Database=boikobank, если название базы другое)
        private static string connStr = "Host=localhost;Username=postgres;Password=2005;Database=boikobank";

        private static readonly NpgsqlDataSource _ds = new NpgsqlDataSourceBuilder(connStr).Build();

        public static async Task CreateDbAsync()
        {
            var createClientsTableQuery = @"
                CREATE TABLE IF NOT EXISTS clients (
                    id SERIAL PRIMARY KEY,
                    first_name VARCHAR(255) NOT NULL,
                    last_name VARCHAR(255) NOT NULL,
                    birth_date VARCHAR(50) NOT NULL,
                    balance DECIMAL(18, 2) NOT NULL DEFAULT 0
                );";

            var createCreditsTableQuery = @"
                CREATE TABLE IF NOT EXISTS credits (
                    id SERIAL PRIMARY KEY,
                    client_id INTEGER NOT NULL,
                    amount DECIMAL(18, 2) NOT NULL,
                    interest_rate DECIMAL(5, 2) NOT NULL,
                    term_months INTEGER NOT NULL,
                    CONSTRAINT fk_client FOREIGN KEY (client_id) REFERENCES clients(id) ON DELETE CASCADE
                );";

            await using var conn = _ds.CreateConnection();
            await conn.OpenAsync();

            await using var cmd1 = new NpgsqlCommand(createClientsTableQuery, conn);
            await cmd1.ExecuteNonQueryAsync();

            await using var cmd2 = new NpgsqlCommand(createCreditsTableQuery, conn);
            await cmd2.ExecuteNonQueryAsync();
        }

        // Метод для получения всех клиентов
        public static async Task<List<Client>> GetClientsAsync()
        {
            var clients = new List<Client>();
            var query = "SELECT id, last_name, first_name, birth_date, balance FROM clients ORDER BY id;";

            await using var conn = _ds.CreateConnection();
            await conn.OpenAsync();
            await using var cmd = new NpgsqlCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                clients.Add(new Client(
                    reader.GetInt32(0),      // Id
                    reader.GetString(1),   // LastName
                    reader.GetString(2),   // FirstName
                    reader.GetString(3),   // DataR (BirthDate)
                    reader.GetDecimal(4)   // Balance
                ));
            }
            return clients;
        }

        // Метод для добавления нового клиента
        public static async Task AddClientAsync(Client client)
        {
            var query = @"INSERT INTO clients (first_name, last_name, birth_date, balance) 
                          VALUES (@FirstName, @LastName, @BirthDate, @Balance) RETURNING id;";

            await using var conn = _ds.CreateConnection();
            await conn.OpenAsync();
            await using var cmd = new NpgsqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@FirstName", client.FirstName);
            cmd.Parameters.AddWithValue("@LastName", client.LastName);
            cmd.Parameters.AddWithValue("@BirthDate", client.DataR);
            cmd.Parameters.AddWithValue("@Balance", client.Balance);

            client.Id = (int)await cmd.ExecuteScalarAsync();
        }

        // Метод для удаления клиента по Id
        public static async Task DeleteClientByIdAsync(int clientId)
        {
            var query = "DELETE FROM clients WHERE id = @Id;";
            await using var conn = _ds.CreateConnection();
            await conn.OpenAsync();
            await using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", clientId);
            await cmd.ExecuteNonQueryAsync();
        }
    }
}