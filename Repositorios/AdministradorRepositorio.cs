using ProjetoAcademia.Interfaces;
using ProjetoAcademia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAcademia.Repositorios
{
    public class AdministradorRepositorio : RepositorioGenerico<Administrador>, IAdministradorRepositorio
    {
        private readonly Contexto _contexto;

        public AdministradorRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public bool AdministradorExiste(string email, string senha)
        {
            return _contexto.Administradores.Any(a => a.Email == email && a.Senha == senha);
        }
    }
}
