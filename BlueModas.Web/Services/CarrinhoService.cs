using BlueModas.Web.Factory;
using BlueModas.Web.Models;
using BlueModas.Web.Repository.Interface;
using BlueModas.Web.Services.Interface;
using BlueModas.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueModas.Web.Services
{
    public class CarrinhoService : ICarrinhoService
    {
        private readonly ICarrinhoRepository carrinhoRepository;
        private readonly ClienteService clienteService;
        public CarrinhoService(ICarrinhoRepository carrinhoRepository, ClienteService clienteService)
        {
            this.carrinhoRepository = carrinhoRepository;
            this.clienteService = clienteService;
        }

        private List<ItemDoCarrinho> AdicionarItem(Produto produto, int quantidade)
        {
            var listaItem = new List<ItemDoCarrinho>();
            var item = new ItemDoCarrinho();
            item.Produto = produto;
            item.Quantidade = quantidade;
            item.ValorUnitario = produto.Preco;

            listaItem.Add(item);
            return listaItem;
        }

        public async Task<List<Pedido>> ObterCarrinhoPeloIdDoCliente(string id)
        {
            return await carrinhoRepository.ObterPedidoPeloIdDoCliente(id);
        }

        public async Task<Request> EfetuarCompra(InicioViewModel inicio)
        {
            
            var usuarioLogadoId = await clienteService.ObterUsuarioLogadoId();
            var usuarioIdentity = await clienteService.BuscarUsuarioPorId(usuarioLogadoId);
            var pedido = CarrinhoFactory.MontarPedido(inicio, usuarioIdentity);
            var request =  await carrinhoRepository.GravarPedido(pedido);
            return request;
        }

        public async Task<Pedido> EfetuarCompraPedidoEstatico(InicioViewModel inicio, Cliente cliente)
        {
            var pedido = CarrinhoFactory.MontarPedido(inicio, cliente);
            var request = await carrinhoRepository.GravarPedidoEstatico(pedido);
            return request;
        }
    }
}
