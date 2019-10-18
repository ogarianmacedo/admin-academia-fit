using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAcademia.Interfaces
{
    public interface IRepositorioGenerico<TEntity> where TEntity : class
    {
        IQueryable<TEntity> BuscarTodos();
        Task<TEntity> BuscarPeloId(int id);
        Task Inserir(TEntity entity);
        Task Atualizar(TEntity entity);
        Task Excluir(int id);
    }
}
