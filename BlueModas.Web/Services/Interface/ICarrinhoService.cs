using BlueModas.Web.Models;
using BlueModas.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueModas.Web.Services.Interface
{
    public interface ICarrinhoService
    {
        Task<List<Pedido>> ObterCarrinhoPeloIdDoCliente(string id);
        Task<Request> EfetuarCompra(InicioViewModel inicio);
        Task<Pedido> EfetuarCompraPedidoEstatico(InicioViewModel inicio, Cliente usuarioIdentity);
    }
}
