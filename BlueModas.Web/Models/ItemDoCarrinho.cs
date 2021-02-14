using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueModas.Web.Models
{
    public class ItemDoCarrinho
    {
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }

        public decimal CalcularValorPorItem()
        {
            return Produto.Preco * Quantidade;
        }

    }
}
