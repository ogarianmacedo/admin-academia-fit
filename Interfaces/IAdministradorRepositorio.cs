using ProjetoAcademia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAcademia.Interfaces
{
    public interface IAdministradorRepositorio : IRepositorioGenerico<Administrador>
    {
        bool AdministradorExiste(string email, string senha);
    }
}
