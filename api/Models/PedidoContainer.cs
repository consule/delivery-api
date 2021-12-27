using api.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class PedidoContainer
    {
        public long Contador { get; set; }
        public List<Pedido> Pedidos { get; set; }
    }
}
