using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Repositories.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime DataHoraPedido { get; set; }
        public DateTime DataHoraPedidoEntregue { get; set; }
        public int IdCliente { get; set; }
        public int IdProduto { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

    }
}
