using Microsoft.EntityFrameworkCore;
using ProjetoAcademia.Interfaces;
using ProjetoAcademia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAcademia.Repositorios
{
    public class ObjetivoRepositorio : RepositorioGenerico<Objetivo>, IObjetivoRepositorio
    {
        private readonly Contexto _contexto;

        public ObjetivoRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> ObjetivoExiste(string Nome, int ObjetivoId)
        {
            return await _contexto.Objetivos.AnyAsync(o => o.Nome == Nome && o.ObjetivoId != ObjetivoId);
        }

        public async Task<bool> VerificaObjetivoPeloNome(string Nome)
        {
            return await _contexto.Objetivos.AnyAsync(o => o.Nome == Nome);
        }
    }
}
