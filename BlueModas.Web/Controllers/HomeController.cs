using BlueModas.Web.Helpers;
using BlueModas.Web.Models;
using BlueModas.Web.Services;
using BlueModas.Web.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlueModas.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProdutoService produtoService;
        private readonly ClienteService clienteService;
        public HomeController(IProdutoService produtoService, ILogger<HomeController> logger, ClienteService clienteService)
        {
            _logger = logger;
            this.produtoService = produtoService;
            this.clienteService = clienteService;
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await produtoService.ObterTodosOsProdutos();
            var carrinho = SessionHelper.ObterObjetoDoJson<Carrinho>(HttpContext.Session, "carrinho");
            var inicio = produtoService.AdicionarProdutoParaOInicio(produtos, carrinho);

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
                }
                else
                {
                    Carrinho carrinho = SessionHelper.ObterObjetoDoJson<Carrinho>(HttpContext.Session, "carrinho");
                    int index = SeExistir(produtoId);
                    if (index != -1)
                    {
                        carrinho.ItemDoCarrinho[index].Quantidade  = carrinho.ItemDoCarrinho[index].Quantidade + int.Parse(quant);
                    }
                    else
                    {
                        carrinho.ItemDoCarrinho.Add(new ItemDoCarrinho { Produto = await produtoService.ObterProdutoPorId(produtoId), Quantidade = 1 });
                    }
                    SessionHelper.ObterObjetoJson(HttpContext.Session, "carrinho", carrinho);
                }
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Carrinho()
        {
            var carrinho = SessionHelper.ObterObjetoDoJson<Carrinho>(HttpContext.Session, "carrinho");
            return View(carrinho);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
