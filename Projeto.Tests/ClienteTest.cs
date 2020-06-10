using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.WsFederation;
using Newtonsoft.Json;
using Projeto.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Projeto.Tests
{
    public class ClienteTest
    {
        //atributos..
        private readonly AppContext appContext;
        private readonly string endpoint;

        //construtor -> ctor + 2x[tab]
        public ClienteTest()
        {
            appContext = new AppContext();
            endpoint = "/api/Cliente";
        }

        [Fact] //método para execução de teste do XUnit
        //async -> método executado como uma Thread (assincrono)
        public async Task Cliente_Post_ReturnsOk()
        {
            //cadastrando um cliente na API
            var modelCadastro = new ClienteCadastroModel()
            {
                Nome = "Sergio Mendes",
                Email = "sergio.coti@gmail.com"
            };

            StringContent requestCadastro = SerializarObjeto(modelCadastro);
            var responseCadastro = await appContext.Client.PostAsync(endpoint, requestCadastro);
            ResultModel resposta = DeserializarObjeto(responseCadastro);

            //verificação de teste
            responseCadastro.StatusCode.Should().Be(HttpStatusCode.OK);
            resposta.Mensagem.Should().Be("Cliente cadastrado com sucesso.");
        }        

        [Fact] //método para execução de teste do XUnit
        //async -> método executado como uma Thread (assincrono)
        public async Task Cliente_Post_ReturnsBadRequest()
        {
            //preencher os campos da model
            var model = new ClienteCadastroModel()
            {
                Nome = string.Empty, //vazio
                Email = string.Empty //vazio
            };

            //montando os dados em JSON que serão enviados para a API
            var request = SerializarObjeto(model);
            var response = await appContext.Client.PostAsync(endpoint, request);

            //critério de teste (Serviço da API retornar HTTP BADREQUEST (400))
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact] //método para execução de teste do XUnit
        //async -> método executado como uma Thread (assincrono)
        public async Task Cliente_Put_ReturnsOk()
        {
            //--------------cadastrando um cliente na API
            var modelCadastro = new ClienteCadastroModel()
            {
                Nome = "Sergio Mendes",
                Email = "sergio.coti@gmail.com"
            };

            var requestCadastro = SerializarObjeto(modelCadastro);
            var responseCadastro = await appContext.Client.PostAsync(endpoint, requestCadastro);
            var respostaCadastro = DeserializarObjeto(responseCadastro);

            //verificação de teste
            responseCadastro.StatusCode.Should().Be(HttpStatusCode.OK);
            respostaCadastro.Mensagem.Should().Be("Cliente cadastrado com sucesso.");

            //--------------atualizando o cliente cadastrado na API
            var modelEdicao = new ClienteEdicaoModel()
            {
                IdCliente = respostaCadastro.Cliente.IdCliente,
                Nome = "Sergio da Silva Mendes",
                Email = "sergio.coti@yahoo.com"
            };

            var requestEdicao = SerializarObjeto(modelEdicao);
            var responseEdicao = await appContext.Client.PutAsync(endpoint, requestEdicao);
            var respostaEdicao = DeserializarObjeto(responseEdicao);

            //verificação de teste
            responseEdicao.StatusCode.Should().Be(HttpStatusCode.OK);
            respostaEdicao.Mensagem.Should().Be("Cliente atualizado com sucesso.");
        }

        [Fact] //método para execução de teste do XUnit
        //async -> método executado como uma Thread (assincrono)
        public async Task Cliente_Put_ReturnsBadRequest()
        {
            //preencher os campos da model
            var model = new ClienteEdicaoModel()
            {
                IdCliente = 0,
                Nome = string.Empty, //vazio
                Email = string.Empty //vazio
            };

            //montando os dados em JSON que serão enviados para a API
            var request = SerializarObjeto(model);
            var response = await appContext.Client.PutAsync(endpoint, request);

            //critério de teste (Serviço da API retornar HTTP BADREQUEST (400))
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact] //método para execução de teste do XUnit
        //async -> método executado como uma Thread (assincrono)
        public async Task Cliente_Delete_ReturnsOk()
        {
            //--------------cadastrando um cliente na API
            var modelCadastro = new ClienteCadastroModel()
            {
                Nome = "Sergio Mendes",
                Email = "sergio.coti@gmail.com"
            };

            var requestCadastro = SerializarObjeto(modelCadastro);
            var responseCadastro = await appContext.Client.PostAsync(endpoint, requestCadastro);
            var respostaCadastro = DeserializarObjeto(responseCadastro);

            //verificação de teste
            responseCadastro.StatusCode.Should().Be(HttpStatusCode.OK);
            respostaCadastro.Mensagem.Should().Be("Cliente cadastrado com sucesso.");

            //--------------excluindo o cliente cadastrado na API  
            var responseExclusao = await appContext.Client.DeleteAsync
                (endpoint + "/" + respostaCadastro.Cliente.IdCliente);
            var respostaExclusao = DeserializarObjeto(responseExclusao);

            //verificação de teste
            responseExclusao.StatusCode.Should().Be(HttpStatusCode.OK);
            respostaExclusao.Mensagem.Should().Be("Cliente excluido com sucesso.");
        }

        [Fact] //método para execução de teste do XUnit
        //async -> método executado como uma Thread (assincrono)
        public async Task Cliente_GetAll_ReturnsOk()
        {
            //executando o serviço da API..
            var response = await appContext.Client.GetAsync(endpoint);

            //critério de teste (Serviço da API retornar HTTP OK (200))
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact] //método para execução de teste do XUnit
        //async -> método executado como uma Thread (assincrono)
        public async Task Cliente_GetById_ReturnsOk()
        {
            //--------------cadastrando um cliente na API
            var modelCadastro = new ClienteCadastroModel()
            {
                Nome = "Sergio Mendes",
                Email = "sergio.coti@gmail.com"
            };

            var requestCadastro = SerializarObjeto(modelCadastro);
            var responseCadastro = await appContext.Client.PostAsync(endpoint, requestCadastro);
            var respostaCadastro = DeserializarObjeto(responseCadastro);

            //executando o serviço da API..
            var response = await appContext.Client.GetAsync
                (endpoint + "/" + respostaCadastro.Cliente.IdCliente);

            //critério de teste (Serviço da API retornar HTTP OK (200))
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.StatusCode.Should().NotBe(HttpStatusCode.NoContent);
        }

        [Fact] //método para execução de teste do XUnit
        //async -> método executado como uma Thread (assincrono)
        public async Task Cliente_GetById_ReturnsNoContent()
        {
            //executando o serviço da API..
            var response = await appContext.Client.GetAsync(endpoint + "/999999");

            //critério de teste (Serviço da API retornar HTTP NOCONTENT (204))
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            response.StatusCode.Should().NotBe(HttpStatusCode.OK);
        }

        private static StringContent SerializarObjeto(object model)
        {
            return new StringContent(JsonConvert.SerializeObject(model),
                            Encoding.UTF8, "application/json");
        }

        private static ResultModel DeserializarObjeto(HttpResponseMessage response)
        {
            var result = string.Empty;
            using (HttpContent content = response.Content)
            {
                Task<string> r = content.ReadAsStringAsync();
                result += r.Result;
            }

            var resposta = JsonConvert.DeserializeObject<ResultModel>(result);
            return resposta;
        }
    }

    //Modelos de dados para obter o retorno da API..
    public class ResultModel
    {
        public string Mensagem { get; set; }
        public Cliente Cliente { get; set; }
    }

    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
