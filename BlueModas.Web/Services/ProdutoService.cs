using BlueModas.Web.Factory;
using BlueModas.Web.Models;
using BlueModas.Web.Repository.Interface;
using BlueModas.Web.Services.Interface;
using BlueModas.Web.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlueModas.Web.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository produtoRepository;
        public static IWebHostEnvironment _environment;
        public ProdutoService(IProdutoRepository produtoRepository, IWebHostEnvironment environment)
        {
            this.produtoRepository = produtoRepository;
            _environment = environment;
        }
        public async Task<Request> SalvarProduto(Produto produto, IFormFile arquivo)
        {
            var imagem = await UploadDeImagem(arquivo);
            var produtoModel = new Produto(produto.Nome, produto.Preco);
            produtoModel.AdicionarImagem(imagem);
            var request =  produtoRepository.SalvarProduto(produtoModel);
            return request;
        }

        public async Task<List<Produto>> ObterTodosOsProdutos()
        {
            var produtoModel = await produtoRepository.ObterTodosOsProdutos();
            return produtoModel;
        }
        public async Task<Produto> ObterProdutoPorId(int id)
        {
            return await produtoRepository.ObterPorId(id);
        }
        private async Task<string> UploadDeImagem(IFormFile arquivo)
        {
            if (arquivo.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\imagens\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\imagens\\");
                    }
                    using (FileStream filestream = File.Create(_environment.WebRootPath + "\\imagens\\" + arquivo.FileName))
                    {
                        await arquivo.CopyToAsync(filestream);
                        filestream.Flush();
                        return "/imagens/" + arquivo.FileName;
                    }
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            return null;
        }

        public InicioViewModel AdicionarProdutoParaOInicio(List<Produto> produtos, Carrinho carrinho)
        {
            var inicioViewModel = ProdutoFactory.CriarProdutoParaListarNaHome(produtos, carrinho);
            return inicioViewModel;
        }
    }
}
