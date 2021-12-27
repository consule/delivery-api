using api.Data;
using api.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly DbSession _db;

        public ProdutoRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        #region SQL
        const string _cmdGet = @"SELECT * FROM DLV_PRODUTO";
        #endregion
        public async Task<List<ProdutoModel>> GetAll()
        {
            using var conn = _db.Connection;
            List<ProdutoModel> produto = (await conn.QueryAsync<ProdutoModel>(sql: _cmdGet)).ToList();
            return produto;
        }
    }
}
