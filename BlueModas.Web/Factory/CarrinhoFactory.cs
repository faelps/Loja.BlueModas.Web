using BlueModas.Web.Models;
using BlueModas.Web.Models.Dto;
using BlueModas.Web.ViewModel;
using System;
using System.Collections.Generic;

namespace BlueModas.Web.Factory
{
    public static class CarrinhoFactory
    {
        internal static Pedido MontarPedido(InicioViewModel inicio, Cliente cliente)
        {
            var pedido = new Pedido
            {
                DataDoPedido = DateTime.Now,
                Cliente = MontarDadosDoCliente(cliente),
                ItensDoPedido = MontarItens(inicio.Carrinho.ItemDoCarrinho)
            };
            return pedido;
        }

        private static List<ItemDoCarrinho> MontarItens(List<ItemDoCarrinho> itemDoCarrinho)
        {
            var lista = new List<ItemDoCarrinho>();

            foreach (var item in itemDoCarrinho)
            {
                var pedido = new ItemDoCarrinho
                {
                    Produto = item.Produto,
                    Quantidade = item.Quantidade,
                    ValorUnitario = item.Produto.Preco,
                    ProdutoId = item.Produto.Id
                };
                lista.Add(pedido);
            }
            return lista;
        }

        private static ClienteDto MontarDadosDoCliente(Cliente cliente)
        {
            return new ClienteDto
            {
                Email = cliente.Email,
                Nome = cliente.FullName,
                Id = cliente.Id,
                Telefone = cliente.PhoneNumber
            };
        }

    }
}
