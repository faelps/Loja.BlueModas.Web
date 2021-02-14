using BlueModas.Web.Models;
using BlueModas.Web.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BlueModas.Web.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly string uriBlueModas;
        public ProdutoRepository(IConfiguration config)
        {
            this.uriBlueModas = config["UriBlueModas"];
        }

        public async Task<Produto> ObterPorId(int id)
        {
            RestClient client = new RestClient(uriBlueModas);
            var rest = $"api/Produto/ObterProdutoPorId/" + id;
            var request = new RestRequest(rest, Method.GET);
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var respostaJson = JsonConvert.DeserializeObject<Produto>(response.Content);
                return respostaJson;
            }
            return null;
        }

        public async Task<List<Produto>> ObterTodosOsProdutos()
        {
            RestClient client = new RestClient(uriBlueModas);
            var rest = $"api/Produto/ObterTodosOsProdutos";
            var request = new RestRequest(rest, Method.GET);
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var respostaJson = JsonConvert.DeserializeObject<List<Produto>>(response.Content);
                return respostaJson;
            }
            return null;
        }

        public Request SalvarProduto(Produto produto)
        {
            RestClient restClient = new RestClient(uriBlueModas);
            var rest = $"api/Produto/SalvarProduto/" + produto;
            var request = new RestRequest(rest, Method.POST) { RequestFormat = DataFormat.Json};
            request.AddJsonBody(produto);
            var response = restClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var respostaJson = JsonConvert.DeserializeObject<Request>(response.Content);
                return respostaJson;
            }
            var badRequest = new Request(false, "Ocorreu um erro, por favor contate o administrador");
            return badRequest;
        }
    }
}
