using BlueModas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueModas.Web.Repository.Interface
{
    public interface IProdutoRepository
    {
        Request SalvarProduto(Produto produto);
        Task<List<Produto>> ObterTodosOsProdutos();
        Task<Produto> ObterPorId(int id);
    }
}
