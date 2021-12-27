using api.Models;
using api.Repositories.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Repositories
{
    public interface IPedidoRepository
    {
        Task<PedidoContainer> GetPedidosAsync();
        Task<List<Pedido>> GetPedidoPorIDAsync(int Id);
        Task<PedidoContainer> GetPedidosPorNomeClienteAsync(string nomeCliente);
        Task<PedidoContainer> GetPedidosPorNomeClienteEntregue(string nomeCliente);
        Task<int> PostPedidoAsync(Pedido ped);
        Task<int> MarcaPedidoComoEntregue(int Id, Pedido ped);
        Task<PedidoContainer> GetPedidoEntregues();
    }
}
