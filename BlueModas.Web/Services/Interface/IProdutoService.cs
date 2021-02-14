using BlueModas.Web.Models;
using BlueModas.Web.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueModas.Web.Services.Interface
{
    public interface IProdutoService
    {
        Task<Request> SalvarProduto(Produto produto, IFormFile arquivo);
        Task<List<Produto>> ObterTodosOsProdutos();
        Task<Produto> ObterProdutoPorId(int id);
        InicioViewModel AdicionarProdutoParaOInicio(List<Produto> produtos, Carrinho carrinho);
    }
}
