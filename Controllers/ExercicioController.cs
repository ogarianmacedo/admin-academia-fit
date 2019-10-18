using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoAcademia.Interfaces;
using ProjetoAcademia.Models;

namespace ProjetoAcademia.Controllers
{
    [Authorize]
    public class ExercicioController : Controller
    {
        private readonly IExercicioRepositorio _repositorio;
        private readonly ICategoriaExercicioRepositorio _catRepositorio;

        public ExercicioController(IExercicioRepositorio repositorio, ICategoriaExercicioRepositorio catRepositorio)
        {
            _repositorio = repositorio;
            _catRepositorio = catRepositorio;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repositorio.BuscarTodos());
        }

        public async Task<IActionResult> Create()
        {
            ViewData["CatExercicioId"] = new SelectList(_catRepositorio.BuscarTodos(), "CatExercicioId", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExercicioId,Nome,CatExercicioId")] Exercicio exercicio)
        {
            if (ModelState.IsValid)
            {
                await _repositorio.Inserir(exercicio);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatExercicioId"] = new SelectList(_catRepositorio.BuscarTodos(), "CatExercicioId", "Nome", exercicio.CatExercicioId);
            return View(exercicio);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var exercicio = await _repositorio.BuscarPeloId(id);
            if (exercicio == null)
            {
                return NotFound();
            }
            ViewData["CatExercicioId"] = new SelectList(_catRepositorio.BuscarTodos(), "CatExercicioId", "Nome", exercicio.CatExercicioId);
            return View(exercicio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExercicioId,Nome,CatExercicioId")] Exercicio exercicio)
        {
            if (id != exercicio.ExercicioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _repositorio.Atualizar(exercicio);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatExercicioId"] = new SelectList(_catRepositorio.BuscarTodos(), "CatExercicioId", "Nome", exercicio.CatExercicioId);
            return View(exercicio);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _repositorio.Excluir(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<JsonResult> ExercicioExiste(string nome, int ExercicioId)
        {
            if(ExercicioId == 0)
            {
                if(await _repositorio.BuscaExercicioPorNome(nome))
                {
                    return Json("Exercício já existe.");
                }

                return Json(true);
            }
            else
            {
                if(await _repositorio.ExercicioExiste(nome, ExercicioId))
                {
                    return Json("Exercício já existe");
                }

                return Json(true);
            }
        }
    }
}
