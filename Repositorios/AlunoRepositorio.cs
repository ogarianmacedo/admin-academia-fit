using Microsoft.EntityFrameworkCore;
using ProjetoAcademia.Interfaces;
using ProjetoAcademia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAcademia.Repositorios
{
    public class AlunoRepositorio : RepositorioGenerico<Aluno>, IAlunoRepositorio
    {
        private readonly Contexto _contexto;

        public AlunoRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> AlunoExiste(string NomeCompleto, int AlunoId)
        {
            return await _contexto.Alunos.AnyAsync(a => a.NomeCompleto == NomeCompleto && a.AlunoId != AlunoId);
        }

        public async Task<bool> BuscarAlunoPorNome(string NomeCompleto)
        {
            return await _contexto.Alunos.AnyAsync(a => a.NomeCompleto == NomeCompleto);
        }

        public async Task<Aluno> BuscarDadosAlunoPorId(int AlunoId)
        {
            return await _contexto.Alunos
                .Include(a => a.Objetivo)
                .Include(a => a.Professor)
                .Where(a => a.AlunoId == AlunoId).FirstAsync();
        }

        public string BuscarNomeAlunoPorId(int id)
        {
            return _contexto.Alunos.Where(a => a.AlunoId == id).Select(a => a.NomeCompleto).First();
        }

        public new async Task<IEnumerable<Aluno>> BuscarTodos()
        {
            return await _contexto.Alunos
                .Include(a => a.Objetivo)
                .Include(a => a.Professor).ToListAsync();
        }
    }
}
