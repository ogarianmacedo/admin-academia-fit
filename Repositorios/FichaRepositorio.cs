using Microsoft.EntityFrameworkCore;
using ProjetoAcademia.Interfaces;
using ProjetoAcademia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAcademia.Repositorios
{
    public class FichaRepositorio : RepositorioGenerico<Ficha>, IFichaRepositorio
    {
        public readonly Contexto _contexto;

        public FichaRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> BuscaFichaPorNome(string Nome)
        {
            return await _contexto.Fichas.AnyAsync(f => f.Nome == Nome);
        }

        public async Task<Ficha> BuscarFichaPeloAlunoId(int id)
        {
            return await _contexto.Fichas.Include(f => f.Aluno).FirstOrDefaultAsync(f => f.AlunoId == id);
        }

        public async Task<IEnumerable<Ficha>> BuscarTodasFichasPeloAlunoId(int id)
        {
            return await _contexto.Fichas.Include(f => f.Aluno)
                .ThenInclude(f => f.Objetivo).Where(f => f.AlunoId == id)
                .ToListAsync();
        }

        public async Task<bool> FichaExiste(string Nome, int FichaId)
        {
            return await _contexto.Fichas.AnyAsync(f => f.Nome == Nome && f.FichaId == FichaId);
        }
    }
}
