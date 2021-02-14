using BlueModas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueModas.Web.ViewModel
{
    public class InicioViewModel
    {
        public List<Produto> Produtos { get; set; }
        public Carrinho Carrinho { get; set; }
        public Pedido Pedido { get; set; }
        public List<Pedido> Pedidos { get; set; }

        public int CalcularTotalDeProdutos()
        {
            return Carrinho != null ? Carrinho.ItemDoCarrinho.Count : 0;
        }
        public decimal CalcularPrecoTotalDoCarrinho()
        {

          return  Carrinho.ItemDoCarrinho.Sum(x => x.Produto.Preco * x.Quantidade);

        }

    }
}
