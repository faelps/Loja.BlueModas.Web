using System.Collections.Generic;
using System.Linq;

namespace BlueModas.Web.Models
{
    public class Carrinho
    {
        public Carrinho()
        {
            ItemDoCarrinho = new List<ItemDoCarrinho>();
        }
        public string CarrinhoId { get; set; }
        public Cliente Cliente { get; set; }
        public List<ItemDoCarrinho> ItemDoCarrinho { get; set; }


        public decimal CalcularValorTotalDoCarrinho()
        {

            var v = ItemDoCarrinho.Sum(x => x.Produto.Preco * x.Quantidade);
            return v;
        }
    }
}
