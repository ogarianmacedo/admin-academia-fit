using Microsoft.EntityFrameworkCore;
using ProjetoAcademia.Interfaces;
using ProjetoAcademia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAcademia.Repositorios
{
    public class ListaExercicioRepositorio : RepositorioGenerico<ListaExercicio>, IListaExercicioRepositorio
    {
        private readonly Contexto _contexto;

        public ListaExercicioRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> ExisteExercicioFicha(int ExercicioId, int FichaId)
        {
            return await _contexto.ListasExercicios.AnyAsync(e => e.ExercicioId == ExercicioId && e.FichaId == FichaId);
        }
    }
}
