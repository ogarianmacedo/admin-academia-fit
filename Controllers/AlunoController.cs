using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoAcademia.Interfaces;
using ProjetoAcademia.Models;

namespace ProjetoAcademia.Controllers
{
    public class AlunoController : Controller
    {
        private readonly IAlunoRepositorio _repositorio;
        private readonly IObjetivoRepositorio _objetivoRepositorio;
        private readonly IProfessorRepositorio _professorRepositorio;

        public AlunoController(
            IAlunoRepositorio repositorio,
            IObjetivoRepositorio objetivoRepositorio,
            IProfessorRepositorio professorRepositorio
        )
        {
            _repositorio = repositorio;
            _objetivoRepositorio = objetivoRepositorio;
            _professorRepositorio = professorRepositorio;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repositorio.BuscarTodos());
        }

        public IActionResult Create()
        {
            ViewData["ObjetivoId"] = new SelectList(_objetivoRepositorio.BuscarTodos(), "ObjetivoId", "Descricao");
            ViewData["ProfessorId"] = new SelectList(_professorRepositorio.BuscarTodos(), "ProfessorId", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlunoId,NomeCompleto,Idade,Peso,ObjetivoId,ProfessorId,FrequenciaSemanal")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                await _repositorio.Inserir(aluno);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ObjetivoId"] = new SelectList(_objetivoRepositorio.BuscarTodos(), "ObjetivoId", "Descricao", aluno.ObjetivoId);
            ViewData["ProfessorId"] = new SelectList(_professorRepositorio.BuscarTodos(), "ProfessorId", "Nome", aluno.ProfessorId);
            return View(aluno);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var aluno = await _repositorio.BuscarPeloId(id);
            if (aluno == null)
            {
                return NotFound();
            }
            ViewData["ObjetivoId"] = new SelectList(_objetivoRepositorio.BuscarTodos(), "ObjetivoId", "Descricao", aluno.ObjetivoId);
            ViewData["ProfessorId"] = new SelectList(_professorRepositorio.BuscarTodos(), "ProfessorId", "Nome", aluno.ProfessorId);
            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlunoId,NomeCompleto,Idade,Peso,ObjetivoId,ProfessorId,FrequenciaSemanal")] Aluno aluno)
        {
            if (id != aluno.AlunoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _repositorio.Atualizar(aluno);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ObjetivoId"] = new SelectList(_objetivoRepositorio.BuscarTodos(), "ObjetivoId", "Descricao", aluno.ObjetivoId);
            ViewData["ProfessorId"] = new SelectList(_professorRepositorio.BuscarTodos(), "ProfessorId", "Nome", aluno.ProfessorId);
            return View(aluno);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _repositorio.Excluir(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<JsonResult> AlunoExiste(string NomeCompleto, int AlunoId)
        {
            if(AlunoId == 0)
            {
                if(await _repositorio.BuscarAlunoPorNome(NomeCompleto))
                {
                    return Json("Aluno já existe");
                }

                return Json(true);
            }
            else
            {
                if (await _repositorio.AlunoExiste(NomeCompleto, AlunoId))
                {
                    return Json("Aluno já existe");
                }

                return Json(true);
            }
        }
    }
}
