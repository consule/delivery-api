using api.Data;
using api.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly DbSession _db;
        public ClienteRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        #region SQL 
        const string _cmdGet = @"SELECT * FROM DLV_CLIENTE";
        #endregion
        public async Task<List<ClienteModel>> Get()
        {
            using var conn = _db.Connection;
            List<ClienteModel> cliente = (await conn.QueryAsync<ClienteModel>(sql: _cmdGet)).ToList();
            return cliente;
        }
    }
}
