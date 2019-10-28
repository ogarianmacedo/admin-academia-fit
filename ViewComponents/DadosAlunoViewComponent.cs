using Microsoft.AspNetCore.Mvc;
using ProjetoAcademia.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAcademia.ViewComponents
{
    public class DadosAlunoViewComponent : ViewComponent
    {
        private readonly IAlunoRepositorio _alunoRepositorio;

        public DadosAlunoViewComponent(IAlunoRepositorio alunoRepositorio)
        {
            _alunoRepositorio = alunoRepositorio;
        }

        public async Task<IViewComponentResult> InvokeAsync(int AlunoId)
        {
            return View(await _alunoRepositorio.BuscarDadosAlunoPorId(AlunoId));
        }
    }
}
