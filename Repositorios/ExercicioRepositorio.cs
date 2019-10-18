using Microsoft.EntityFrameworkCore;
using ProjetoAcademia.Interfaces;
using ProjetoAcademia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAcademia.Repositorios
{
    public class ExercicioRepositorio : RepositorioGenerico<Exercicio>, IExercicioRepositorio
    {
        private readonly Contexto _contexto;

        public ExercicioRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> BuscaExercicioPorNome(string nome)
        {
            return await _contexto.Exercicios.AnyAsync(e => e.Nome == nome);
        }

        public async Task<bool> ExercicioExiste(string nome, int ExercicioId)
        {
            return await _contexto.Exercicios.AnyAsync(e => e.Nome == nome && e.ExercicioId != ExercicioId);
        }

        public new async Task<IEnumerable<Exercicio>> BuscarTodos()
        {
            return await _contexto.Exercicios.Include(e => e.CategoriaExercicio).ToListAsync();
        }
    }
}
