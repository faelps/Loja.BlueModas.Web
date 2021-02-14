using BlueModas.Web.Models;
using BlueModas.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueModas.Web.Factory
{
    public static class ProdutoFactory
    {
        internal static InicioViewModel CriarProdutoParaListarNaHome(List<Produto> produtoModel, Carrinho carrinho)
        {
            return new InicioViewModel
            {
                Produtos = produtoModel,
                Carrinho = carrinho != null ? carrinho : new Carrinho()
            };
        }

    }
}
