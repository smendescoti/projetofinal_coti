using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Infra.Data.Contracts
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        void Inserir(TEntity obj);
        void Alterar(TEntity obj);
        void Excluir(TEntity obj);
        List<TEntity> Consultar();
        TEntity ObterPorId(int id);
    }
}
