using Microsoft.EntityFrameworkCore;
using Projeto.Infra.Data.Contexts;
using Projeto.Infra.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace Projeto.Infra.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        //atributo
        private readonly DataContext dataContext;

        //construtor para injeção de dependência
        public BaseRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void Inserir(TEntity obj)
        {
            dataContext.Entry(obj).State = EntityState.Added;
            dataContext.SaveChanges();
        }

        public void Alterar(TEntity obj)
        {
            dataContext.Entry(obj).State = EntityState.Modified;
            dataContext.SaveChanges();
        }

        public void Excluir(TEntity obj)
        {
            dataContext.Entry(obj).State = EntityState.Deleted;
            dataContext.SaveChanges();
        }

        public List<TEntity> Consultar()
        {
            return dataContext.Set<TEntity>().ToList();
        }

        public TEntity ObterPorId(int id)
        {
            return dataContext.Set<TEntity>().Find(id);
        }
    }
}
