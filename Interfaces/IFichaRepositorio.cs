using ProjetoAcademia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAcademia.Interfaces
{
    public interface IFichaRepositorio : IRepositorioGenerico<Ficha>
    {
        Task<IEnumerable<Ficha>> BuscarTodasFichasPeloAlunoId(int id);

        Task<Ficha> BuscarFichaAlunoPorId(int id);

        Task<bool> BuscaFichaPorNome(string Nome);

        Task<bool> FichaExiste(string Nome, int FichaId);
    }
}
