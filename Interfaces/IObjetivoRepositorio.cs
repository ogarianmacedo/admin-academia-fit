using ProjetoAcademia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAcademia.Interfaces
{
    public interface IObjetivoRepositorio : IRepositorioGenerico<Objetivo>
    {
        Task<bool> VerificaObjetivoPeloNome(string Nome);
        Task<bool> ObjetivoExiste(string Nome, int ObjetivoId);
    }
}
