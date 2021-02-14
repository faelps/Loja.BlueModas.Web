using BlueModas.Web.Helpers;
using BlueModas.Web.Models;
using BlueModas.Web.Services;
using BlueModas.Web.Services.Interface;
using BlueModas.Web.ViewModel;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BlueModas.Web.Controllers
{
    public class CarrinhoController : BaseController
    {
        private readonly IProdutoService produtoService;
        private readonly ICarrinhoService carrinhoService;
        private readonly ClienteService clienteService;
        public CarrinhoController(IProdutoService produtoService, ICarrinhoService carrinhoService, ClienteService clienteService)
        {
            this.produtoService = produtoService;
            this.carrinhoService = carrinhoService;
            this.clienteService = clienteService;
        }
        public async Task<ActionResult> AdicionarAoCarrinho(int produtoId, string quant)
        {
            var produto = await produtoService.ObterProdutoPorId(produtoId);
            if (produto != null)
            {
                if (SessionHelper.ObterObjetoDoJson<Carrinho>(HttpContext.Session, "carrinho") == null)
                {
                    var cart = new Carrinho();
                    cart.CarrinhoId = HttpContext.Session.Id;
                    cart.ItemDoCarrinho.Add(new ItemDoCarrinho { Produto = produto, Quantidade = int.Parse(quant), ValorUnitario = produto.Preco });
                    SessionHelper.ObterObjetoJson(HttpContext.Session, "carrinho", cart);
                    MensagemSucesso = "Item adicionado ao carrinho";
                }
                else
                {
                    Carrinho carrinho = SessionHelper.ObterObjetoDoJson<Carrinho>(HttpContext.Session, "carrinho");
                    int index = SeExistir(produtoId);
                    if (index != -1)
                    {
                        carrinho.ItemDoCarrinho[index].Quantidade = carrinho.ItemDoCarrinho[index].Quantidade + int.Parse(quant);
                        MensagemSucesso = "Quantidade alterada";
                    }
                    else
                    {
                        carrinho.ItemDoCarrinho.Add(new ItemDoCarrinho { Produto = await produtoService.ObterProdutoPorId(produtoId), Quantidade = int.Parse(quant)});
                        MensagemSucesso = "Item adicionado ao carrinho";
                    }
                    SessionHelper.ObterObjetoJson(HttpContext.Session, "carrinho", carrinho);
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> VerCarrinho()
        {
            var carrinho = SessionHelper.ObterObjetoDoJson<Carrinho>(HttpContext.Session, "carrinho");
            var inicio = new InicioViewModel();
            inicio.Carrinho = carrinho;
            return View(inicio);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarQuantidadeDeItem(int produtoId)
        {
            var carrinho = SessionHelper.ObterObjetoDoJson<Carrinho>(HttpContext.Session, "carrinho");
            var itemParaAdicionar = carrinho.ItemDoCarrinho.Find(x => x.Produto.Id == produtoId);
            itemParaAdicionar.Quantidade++;
            SessionHelper.ObterObjetoJson(HttpContext.Session, "carrinho", carrinho);
            MensagemSucesso = "Quantidade Alterada";
            return RedirectToAction("VerCarrinho");
        }
        [HttpPost]
        public async Task<IActionResult> RemoverQuantidadeDeItem(int produtoId)
        {
            var carrinho = SessionHelper.ObterObjetoDoJson<Carrinho>(HttpContext.Session, "carrinho");
            var itemParaAdicionar = carrinho.ItemDoCarrinho.Find(x => x.Produto.Id == produtoId);
            itemParaAdicionar.Quantidade--;
            SessionHelper.ObterObjetoJson(HttpContext.Session, "carrinho", carrinho);
            MensagemSucesso = "Quantidade Alterada";
            return RedirectToAction("VerCarrinho");
        }
        public async Task<IActionResult> RemoverItem(int itemId)
        {
            var carrinho = SessionHelper.ObterObjetoDoJson<Carrinho>(HttpContext.Session, "carrinho");
            var itemParaRemover = carrinho.ItemDoCarrinho.Find(x => x.Produto.Id == itemId);
            carrinho.ItemDoCarrinho.Remove(itemParaRemover);
            SessionHelper.ObterObjetoJson(HttpContext.Session, "carrinho", carrinho);
            MensagemSucesso = "Item removido com sucesso";
            return RedirectToAction("VerCarrinho");

        }
        public async Task<IActionResult> Checkout()
        {
            var usuarioLogadoId = await clienteService.ObterUsuarioLogadoId();
            var usuarioIdentity = await clienteService.BuscarUsuarioPorId(usuarioLogadoId);
            if (!string.IsNullOrEmpty(usuarioLogadoId))
            {
                var claims = await clienteService.BuscarClaims(usuarioLogadoId);
                if (claims != null)
                {
                    foreach (var item in claims)
                    {
                        if (item.Type == "Cliente")
                        {
                            var carrinho = SessionHelper.ObterObjetoDoJson<Carrinho>(HttpContext.Session, "carrinho");
                            var inicio = new InicioViewModel();
                            inicio.Carrinho = carrinho;
                            return View(inicio);
                        }
                    }

                }
            }

            return RedirectToAction("Register", "Account");
        }

        public async Task<IActionResult> FinalizarCompra()
        {

            var usuarioLogadoId = await clienteService.ObterUsuarioLogadoId();
            var usuarioIdentity = await clienteService.BuscarUsuarioPorId(usuarioLogadoId);

            var carrinho = SessionHelper.ObterObjetoDoJson<Carrinho>(HttpContext.Session, "carrinho");
            var inicio = new InicioViewModel();
            inicio.Carrinho = carrinho;
            //trecho de código, para quando tiver produtos gravado no banco
            var request  = await carrinhoService.EfetuarCompra(inicio);

            //gera um pedido estatico, que não salva no banco, e nem precisa ter produtos cadastrados no banco
            //var pedidoEstatico = await carrinhoService.EfetuarCompraPedidoEstatico(inicio, usuarioIdentity);
            if (request.Success)
            {
                MensagemSucesso = "Pedido Realizado com sucesso";
            }
            //TempData["MeusPedidos"] = pedidoEstatico;
            return RedirectToAction("MeuPedido");
        }
        [Authorize]
        public async Task<IActionResult> MeuPedido()
        {
            var usuarioLogadoId = await clienteService.ObterUsuarioLogadoId();
            var usuarioIdentity = await clienteService.BuscarUsuarioPorId(usuarioLogadoId);
            var pedido = await carrinhoService.ObterCarrinhoPeloIdDoCliente(usuarioLogadoId);

            InicioViewModel inicio = new InicioViewModel();
            inicio.Pedidos = pedido;
            inicio.Carrinho = new Carrinho();
            return View(inicio);
        }
        private int SeExistir(int id)
        {
            Carrinho carrinho = SessionHelper.ObterObjetoDoJson<Carrinho>(HttpContext.Session, "carrinho");
            for (int i = 0; i < carrinho.ItemDoCarrinho.Count; i++)
            {
                if (carrinho.ItemDoCarrinho[i].Produto.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

    }
}
