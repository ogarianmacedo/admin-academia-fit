using Microsoft.EntityFrameworkCore;
using ProjetoAcademia.Interfaces;
using ProjetoAcademia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAcademia.Repositorios
{
    public class ProfessorRepositorio : RepositorioGenerico<Professor>, IProfessorRepositorio
    {
        private readonly Contexto _contexto;

        public ProfessorRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> ProfessorExiste(string Nome, int ProfessorId)
        {
            return await _contexto.Professores.AnyAsync(p => p.Nome == Nome && p.ProfessorId != ProfessorId);
        }

        public async Task<bool> VerificaProfessorPeloNome(string Nome)
        {
            return await _contexto.Professores.AnyAsync(p => p.Nome == Nome);
        }
    }
}
