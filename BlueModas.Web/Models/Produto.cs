using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueModas.Web.Models
{
    public class Produto
    {
        public Produto()
        {

        }
        public Produto(string nome, decimal preco)
        {
            this.Nome = nome;
            this.Preco = preco;
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Imagem { get; set; }

        public void AdicionarImagem(string imagem)
        {
            this.Imagem = imagem;
        }
    }
}
