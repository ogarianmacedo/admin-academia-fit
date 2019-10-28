using ProjetoAcademia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAcademia.Interfaces
{
    public interface IListaExercicioRepositorio : IRepositorioGenerico<ListaExercicio>
    {
        Task<bool> ExisteExercicioFicha(int ExercicioId, int FichaId);
    }
}
