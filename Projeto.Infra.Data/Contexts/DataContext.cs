using Microsoft.EntityFrameworkCore;
using Projeto.Infra.Data.Entities;
using Projeto.Infra.Data.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Infra.Data.Contexts
{
    //REGRA 1: HERDAR DbContext
    public class DataContext : DbContext
    {
        //REGRA 2: Construtor para receber via injeção de dependência
        //as configurações de acesso ao banco de dados (connectionstring)
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) //construtor da superclasse
        {

        }

        //REGRA 3: Sobrescrita do método OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //adicionar cada classe de mapeamento..
            modelBuilder.ApplyConfiguration(new ClienteMapping());
        }

        //REGRA 4: Declarar uma propriedade DbSet para cada entidade
        //DbSet permite o uso do LAMBDA para cada entidade mapeada
        public DbSet<Cliente> Cliente { get; set; }
    }
}
