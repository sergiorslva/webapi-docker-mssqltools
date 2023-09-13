using System.Data;
using System.Data.SqlClient;

namespace webapidocker
{
    public class ClienteService
    {
        private readonly string connectionString = @"Data Source=sqlserver;Initial Catalog=DemoDB;User ID=sa;Password=@Password123;TrustServerCertificate=true";

        public IEnumerable<ClienteModel> GetClientes()
        {
            List<ClienteModel> clientes = new();

            string sql = "SELECT * FROM Cliente";

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sql, connection);
                sqlCommand.Connection.Open();

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        clientes.Add(new ClienteModel
                        {
                            Id = (int)reader["Id"],
                            Nome = (string)reader["Nome"],
                            Email = (string)reader["Email"]
                        });
                    }
                }
            }

            return clientes;
        }

        public void AddCliente(ClienteModel clienteModel)
        {
            string sql = "INSERT INTO Cliente (Nome, Email) VALUES (@nome, @email)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sql, connection);
                sqlCommand.Parameters.Add("@nome", SqlDbType.VarChar).Value =  clienteModel.Nome;
                sqlCommand.Parameters.Add("@email", SqlDbType.VarChar).Value = clienteModel.Email;
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Connection.Open();

                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
