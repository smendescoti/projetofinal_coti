using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto.Infra.Data.Contracts;
using Projeto.Infra.Data.Entities;
using Projeto.Services.Models;

namespace Projeto.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        //atributo
        private readonly IClienteRepository clienteRepository;

        //construtor para injeção de dependência
        public ClienteController(IClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository;
        }

        [HttpPost]
        public IActionResult Post(ClienteCadastroModel model)
        {
            //verificar se algum campo da model está com erro!
            if (!ModelState.IsValid)
                return BadRequest(); //HTTP 400 (BadRequest)

            try
            {
                var cliente = new Cliente();

                cliente.Nome = model.Nome;
                cliente.Email = model.Email;
                cliente.DataCriacao = DateTime.Now;

                clienteRepository.Inserir(cliente);

                var result = new
                {
                    mensagem = "Cliente cadastrado com sucesso.",
                    cliente //dados do cliente cadastrado no banco
                };

                return Ok(result);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(ClienteEdicaoModel model)
        {
            //verificar se algum campo da model está com erro!
            if (!ModelState.IsValid)
                return BadRequest(); //HTTP 400 (BadRequest)

            try
            {
                var cliente = clienteRepository.ObterPorId(model.IdCliente);

                if (cliente == null) //se cliente não foi encontrado
                    return BadRequest("Cliente não encontrado.");

                cliente.Nome = model.Nome;
                cliente.Email = model.Email;

                clienteRepository.Alterar(cliente);

                var result = new
                {
                    mensagem = "Cliente atualizado com sucesso.",
                    cliente //dados do cliente atualizado no banco
                };

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var cliente = clienteRepository.ObterPorId(id);

                if (cliente == null) //se cliente não foi encontrado
                    return BadRequest("Cliente não encontrado.");

                clienteRepository.Excluir(cliente);

                var result = new
                {
                    mensagem = "Cliente excluido com sucesso.",
                    cliente //dados do cliente atualizado no banco
                };

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ClienteConsultaModel>))]
        public IActionResult GetAll()
        {
            try
            {
                //executando a consulta de clientes
                var consulta = clienteRepository.Consultar();
                var result = new List<ClienteConsultaModel>();

                foreach (var item in consulta)
                {
                    var model = new ClienteConsultaModel()
                    {
                        IdCliente = item.IdCliente,
                        Nome = item.Nome,
                        Email = item.Email,
                        DataCriacao = item.DataCriacao
                    };

                    result.Add(model); //adicionando na lista
                }

                return Ok(result);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ClienteConsultaModel))]
        public IActionResult GetById(int id)
        {
            try
            {
                var consulta = clienteRepository.ObterPorId(id);

                if (consulta == null)
                    return NoContent(); //vazio..

                var result = new ClienteConsultaModel()
                {
                    IdCliente = consulta.IdCliente,
                    Nome = consulta.Nome,
                    Email = consulta.Email,
                    DataCriacao = consulta.DataCriacao
                };

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }
    }
}