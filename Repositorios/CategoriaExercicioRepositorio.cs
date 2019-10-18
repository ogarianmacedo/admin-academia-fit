using Microsoft.EntityFrameworkCore;
using ProjetoAcademia.Interfaces;
using ProjetoAcademia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAcademia.Repositorios
{
    public class CategoriaExercicioRepositorio : RepositorioGenerico<CategoriaExercicio>, ICategoriaExercicioRepositorio
    {
        private readonly Contexto _contexto;

        public CategoriaExercicioRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> CategoriaExiste(string categoria, int CatExercicioId)
        {
            return await _contexto.CategoriasExercicios.AnyAsync(c => c.Nome == categoria && c.CatExercicioId != CatExercicioId);
        }

        public async Task<bool> VerificaCategoria(string categoria)
        {
            return await _contexto.CategoriasExercicios.AnyAsync(c => c.Nome == categoria);
        }
    }
}
