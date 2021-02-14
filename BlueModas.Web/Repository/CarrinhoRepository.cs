using BlueModas.Web.Models;
using BlueModas.Web.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BlueModas.Web.Repository
{
    public class CarrinhoRepository : ICarrinhoRepository
    {
        private readonly string uriBlueModas;
        public CarrinhoRepository(IConfiguration config)
        {
            this.uriBlueModas = config["UriBlueModas"];
        }
        public async Task<Request> GravarPedido(Pedido pedido)
        {
            RestClient restClient = new RestClient(uriBlueModas);
            var rest = $"api/Pedido/GravarPedido/" + pedido;
            var request = new RestRequest(rest, Method.POST) { RequestFormat = DataFormat.Json };
            request.AddJsonBody(pedido);
            var response = await restClient.ExecuteAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var respostaJson = JsonConvert.DeserializeObject<Request>(response.Content);
                return respostaJson;
            }
            var badRequest = new Request(false, "Ocorreu um erro, por favor contate o administrador");
            return badRequest;
        }

        public async Task<Pedido> GravarPedidoEstatico(Pedido pedido)
        {
            RestClient restClient = new RestClient(uriBlueModas);
            var rest = $"api/Pedido/GravarPedido/" + pedido;
            var request = new RestRequest(rest, Method.POST) { RequestFormat = DataFormat.Json };
            request.AddJsonBody(pedido);
            var response = await restClient.ExecuteAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var respostaJson = JsonConvert.DeserializeObject<Pedido>(response.Content);
                return respostaJson;
            }
            return null;
        }

        public async Task<List<Pedido>> ObterPedidoPeloIdDoCliente(string id)
        {
            RestClient client = new RestClient(uriBlueModas);
            var rest = $"api/Pedido/ObterPedidoPeloIdDoCliente/" + id;
            var request = new RestRequest(rest, Method.GET);
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var respostaJson = JsonConvert.DeserializeObject<List<Pedido>>(response.Content);
                return respostaJson;
            }
            return null;
        }
    }
}
