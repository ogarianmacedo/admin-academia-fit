using ProjetoAcademia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAcademia.Interfaces
{
    public interface ICategoriaExercicioRepositorio : IRepositorioGenerico<CategoriaExercicio>
    {
        Task<bool> VerificaCategoria(string categoria);
        Task<bool> CategoriaExiste(string categoria, int CatExercicioId);
    }
}
