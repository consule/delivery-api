using api.Data;
using api.Models;
using api.Repositories.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly DbSession _db;

        public PedidoRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        #region scripts SQLs

        const string _cmdGetPedidosAsync = @"
            SELECT count(*) FROM DLV_PEDIDO WHERE DATAHORAPEDIDOENTREGUE = '0000-00-00 00:00:00';
            SELECT 
	            PED.ID,
	            PED.DATAHORAPEDIDO,
	                CASE
		                WHEN 
                            PED.DATAHORAPEDIDOENTREGUE = 0 THEN STR_TO_DATE('0000,00,0',' % Y,% m,% d % h,% i,% s')
		                ELSE 
                            PED.DATAHORAPEDIDOENTREGUE
	                END AS DATAHORAPEDIDOENTREGUE,
	            PED.IDCLIENTE,
	            PED.IDPRODUTO,
	            CLI.NOME,
	            PRO.DESCRICAO
            FROM
	            DLV_PEDIDO PED
            INNER JOIN DLV_CLIENTE CLI ON
	            PED.IDCLIENTE = CLI.ID
            INNER JOIN DLV_PRODUTO PRO ON
	            PED.IDPRODUTO = PRO.ID
            AND PED.DATAHORAPEDIDOENTREGUE = '0000-00-00 00:00:00'
            ORDER BY PED.ID ASC;";


        const string _cmdGetPedidoPorIDAsync = @"
            SELECT
	            PED.ID,
	            PED.DATAHORAPEDIDO,
	                CASE
		                WHEN 
                            PED.DATAHORAPEDIDOENTREGUE = 0 THEN STR_TO_DATE('0000,00,0',' % Y,% m,% d % h,% i,% s')
		                ELSE 
                            PED.DATAHORAPEDIDOENTREGUE
	                END AS DATAHORAPEDIDOENTREGUE,
	            PED.IDCLIENTE,
	            PED.IDPRODUTO,
	            CLI.NOME,
	            PRO.DESCRICAO
            FROM
	            DLV_PEDIDO PED
            INNER JOIN DLV_CLIENTE CLI ON
	            PED.IDCLIENTE = CLI.ID
            INNER JOIN DLV_PRODUTO PRO ON
	            PED.IDPRODUTO = PRO.ID
            WHERE PED.Id = @Id
            ORDER BY PED.ID DESC";


        const string _cmdGetPedidosPorNomeClienteAsync = @"
            SELECT count(*) FROM DLV_PEDIDO WHERE DATAHORAPEDIDOENTREGUE = '0000-00-00 00:00:00';
            SELECT
	            PED.ID,
	            PED.DATAHORAPEDIDO,
	                CASE
		                WHEN 
                            PED.DATAHORAPEDIDOENTREGUE = 0 THEN STR_TO_DATE('0000,00,0',' % Y,% m,% d % h,% i,% s')
		                ELSE 
                            PED.DATAHORAPEDIDOENTREGUE
	                END AS DATAHORAPEDIDOENTREGUE,
	            PED.IDCLIENTE,
	            PED.IDPRODUTO,
	            CLI.NOME,
	            PRO.DESCRICAO
            FROM
	            DLV_PEDIDO PED
            INNER JOIN DLV_CLIENTE CLI ON
	            PED.IDCLIENTE = CLI.ID
            INNER JOIN DLV_PRODUTO PRO ON
	            PED.IDPRODUTO = PRO.ID
            WHERE PED.DATAHORAPEDIDOENTREGUE = '0000-00-00 00:00:00'
            AND CLI.NOME LIKE CONCAT('%',@NomeCliente,'%')
            ORDER BY PED.ID DESC";

        const string _cmdGetPedidosPorNomeClienteEntregue = @"
            SELECT count(*) FROM DLV_PEDIDO WHERE DATAHORAPEDIDOENTREGUE = '0000-00-00 00:00:00';
            SELECT
	            PED.ID,
	            PED.DATAHORAPEDIDO,
	                CASE
		                WHEN 
                            PED.DATAHORAPEDIDOENTREGUE = 0 THEN STR_TO_DATE('0000,00,0',' % Y,% m,% d % h,% i,% s')
		                ELSE 
                            PED.DATAHORAPEDIDOENTREGUE
	                END AS DATAHORAPEDIDOENTREGUE,
	            PED.IDCLIENTE,
	            PED.IDPRODUTO,
	            CLI.NOME,
	            PRO.DESCRICAO
            FROM
	            DLV_PEDIDO PED
            INNER JOIN DLV_CLIENTE CLI ON
	            PED.IDCLIENTE = CLI.ID
            INNER JOIN DLV_PRODUTO PRO ON
	            PED.IDPRODUTO = PRO.ID
            WHERE PED.DATAHORAPEDIDOENTREGUE != '0000-00-00 00:00:00'
            AND CLI.NOME LIKE CONCAT('%',@NomeCliente,'%')
            ORDER BY PED.ID DESC";

        const string _cmdPostPedidoAsync = @"
            INSERT INTO DLV_PEDIDO (IDCLIENTE, IDPRODUTO) VALUES (@IdCliente, @IdProduto)";

        const string _cmdMarcaPedidoComoEntregue = @"
            UPDATE DLV_PEDIDO SET DATAHORAPEDIDOENTREGUE = NOW() WHERE ID = @Id";

        const string _cmdGetPedidosEntregues = @"
            SELECT count(*) FROM DLV_PEDIDO WHERE DATAHORAPEDIDOENTREGUE != '0000-00-00 00:00:00';
            SELECT 
	            PED.ID,
	            PED.DATAHORAPEDIDO,
	                CASE
		                WHEN 
                            PED.DATAHORAPEDIDOENTREGUE = 0 THEN STR_TO_DATE('0000,00,0',' % Y,% m,% d % h,% i,% s')
		                ELSE 
                            PED.DATAHORAPEDIDOENTREGUE
	                END AS DATAHORAPEDIDOENTREGUE,
	            PED.IDCLIENTE,
	            PED.IDPRODUTO,
	            CLI.NOME,
	            PRO.DESCRICAO
            FROM
	            DLV_PEDIDO PED
            INNER JOIN DLV_CLIENTE CLI ON
	            PED.IDCLIENTE = CLI.ID
            INNER JOIN DLV_PRODUTO PRO ON
	            PED.IDPRODUTO = PRO.ID
            AND PED.DATAHORAPEDIDOENTREGUE != '0000-00-00 00:00:00'
            ORDER BY PED.ID DESC;";
        #endregion scripts SQLs

        public async Task<PedidoContainer> GetPedidosAsync()
        {
            using var conn = _db.Connection;
            var reader = await conn.QueryMultipleAsync(sql: _cmdGetPedidosAsync);

            return new PedidoContainer
            {
                Contador = (await reader.ReadAsync<long>()).FirstOrDefault(),
                Pedidos = (await reader.ReadAsync<Pedido>()).ToList()
            };
        }

        public async Task<List<Pedido>> GetPedidoPorIDAsync(int Id)
        {
            var parametro = new 
            { 
                Id 
            };

            using var conn = _db.Connection;
            List<Pedido> resultado = (await conn.QueryAsync<Pedido>(sql: _cmdGetPedidoPorIDAsync, parametro)).ToList();
            return resultado;
        }

        public async Task<PedidoContainer> GetPedidosPorNomeClienteAsync(string nomeCliente)
        {
            var parametro = new 
            { 
                nomeCliente 
            };

            using var conn = _db.Connection;
            var reader = await conn.QueryMultipleAsync(sql: _cmdGetPedidosPorNomeClienteAsync, parametro);

            return new PedidoContainer 
            {
                Contador = (await reader.ReadAsync<long>()).FirstOrDefault(),
                Pedidos = (await reader.ReadAsync<Pedido>()).ToList()
            };        
        }

        public async Task<PedidoContainer> GetPedidosPorNomeClienteEntregue(string nomeCliente)
        {
            var parametro = new
            {
                nomeCliente
            };

            using var conn = _db.Connection;
            var reader = await conn.QueryMultipleAsync(sql: _cmdGetPedidosPorNomeClienteEntregue, parametro);

            return new PedidoContainer
            {
                Contador = (await reader.ReadAsync<long>()).FirstOrDefault(),
                Pedidos = (await reader.ReadAsync<Pedido>()).ToList()
            };
        }


        public async Task<int> PostPedidoAsync(Pedido ped)
        {
            var parametro = new 
            { 
                ped.IdCliente, 
                ped.IdProduto 
            };

            using var conn = _db.Connection;
            var resultado = (await conn.ExecuteAsync(sql: _cmdPostPedidoAsync, parametro));
            return resultado;
        }

        public async Task<int> MarcaPedidoComoEntregue(int Id, Pedido ped)
        {
            var parametro = new
            { 
                Id
            };

            using var conn = _db.Connection;
            var resultado = (await conn.ExecuteAsync(sql: _cmdMarcaPedidoComoEntregue, parametro));
            return resultado;
        }

        public async Task<PedidoContainer> GetPedidoEntregues()
        {
            using var conn = _db.Connection;
            var reader = await conn.QueryMultipleAsync(sql: _cmdGetPedidosEntregues);

            return new PedidoContainer
            {
                Contador = (await reader.ReadAsync<long>()).FirstOrDefault(),
                Pedidos = (await reader.ReadAsync<Pedido>()).ToList()
            };
        }
    }
}
