using ProjetoAcademia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAcademia.Interfaces
{
    public interface IProfessorRepositorio : IRepositorioGenerico<Professor>
    {
        Task<bool> VerificaProfessorPeloNome(string Nome);

        Task<bool> ProfessorExiste(string Nome, int ProfessorId);
    }
}
