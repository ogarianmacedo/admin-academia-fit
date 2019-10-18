using ProjetoAcademia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAcademia.Interfaces
{
    public interface IExercicioRepositorio : IRepositorioGenerico<Exercicio>
    {
        new Task<IEnumerable<Exercicio>> BuscarTodos();

        Task<bool> BuscaExercicioPorNome(string nome);
        Task<bool> ExercicioExiste(string nome, int ExercicioId);
    }
}
