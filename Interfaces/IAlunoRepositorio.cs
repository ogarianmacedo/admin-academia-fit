using ProjetoAcademia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAcademia.Interfaces
{
    public interface IAlunoRepositorio : IRepositorioGenerico<Aluno>
    {
        new Task<IEnumerable<Aluno>> BuscarTodos();

        string BuscarNomeAlunoPorId(int id);

        Task<Aluno> BuscarDadosAlunoPorId(int AlunoId);

        Task<bool> BuscarAlunoPorNome(string NomeCompleto);

        Task<bool> AlunoExiste(string NomeCompleto, int AlunoId);
    }
}
