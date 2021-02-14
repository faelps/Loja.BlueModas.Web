using BlueModas.Web.Models;
using BlueModas.Web.Repository.Interface;
using BlueModas.Web.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueModas.Web.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoService produtoService;
        public ProdutoController(IProdutoService produtoService)
        {
            this.produtoService = produtoService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SalvarProduto()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SalvarProduto(Produto produto, IFormFile arquivo)
        {
            try
            {
                var request = produtoService.SalvarProduto(produto, arquivo);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> DetalhesDoProduto(int id)
        {
            var produto = await produtoService.ObterProdutoPorId(id);
            return View(produto);
        }
    }
}
