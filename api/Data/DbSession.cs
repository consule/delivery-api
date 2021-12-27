using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace api.Data
{
    public class DbSession : IDisposable
    {
        public IDbConnection  Connection { get; set; }

        public DbSession (IConfiguration configuration)
        {
            Connection = new MySqlConnection(configuration
                .GetConnectionString("DefaultConnection"));

            Connection.Open();
        }
        public void Dispose() => Connection?.Dispose();
    }
}
