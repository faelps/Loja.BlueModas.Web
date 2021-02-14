using BlueModas.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueModas.Web.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime DataDoPedido { get; set; }
        public string NumeroDoPedido { get; set; }
        public ClienteDto Cliente { get; set; } = new ClienteDto();
        public List<ItemDoCarrinho> ItensDoPedido { get; set; }
    }
}
