using BlueModas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueModas.Web.Repository.Interface
{
    public interface ICarrinhoRepository
    {
        Task<Request> GravarPedido(Pedido pedido);
        Task<Pedido> GravarPedidoEstatico(Pedido pedido);
        Task<List<Pedido>> ObterPedidoPeloIdDoCliente(string id);
    }
}
