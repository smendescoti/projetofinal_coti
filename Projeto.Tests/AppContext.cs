using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Projeto.Tests
{
    public class AppContext
    {
        //classe para executar chamadas HTTP na API..
        //prop + 2x[tab]
        public HttpClient Client { get; set; }

        //servidor de testes
        private readonly TestServer testServer;

        //contrutor -> ctor + 2x[tab]
        public AppContext()
        {
            //lendo o arquivo appsettings.json
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            //inicializar o servidor de testes do projeto (TestServer)
            //este projeto de testes irá executar a API por meio da classe 'Startup'
            testServer = new TestServer(new WebHostBuilder()
                .UseConfiguration(configuration)
                .UseStartup<Services.Startup>());

            //instanciando a classe utilizada para executar as chamadas na API
            Client = testServer.CreateClient();
        }
    }
}
