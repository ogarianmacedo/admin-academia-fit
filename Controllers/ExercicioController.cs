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
        private readonly IListaExercicioRepositorio _listaRepositorio;

        public ExercicioController(
            IExercicioRepositorio repositorio, 
            ICategoriaExercicioRepositorio catRepositorio,
            IListaExercicioRepositorio listaRepositorio
        )
        {
            _repositorio = repositorio;
            _catRepositorio = catRepositorio;
            _listaRepositorio = listaRepositorio;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repositorio.BuscarTodos());
        }

        public async Task<IActionResult> Listagem(int FichaId, int AlunoId)
        {
            ViewData["FichaId"] = FichaId;
            ViewData["AlunoId"] = AlunoId;

            return View(await _repositorio.BuscarTodos());
        }

        public async Task<IActionResult> AdicionarExercicio(int ExercicioId, int frequencia, int repeticoes, int carga, int FichaId)
        {
            if(await _listaRepositorio.ExisteExercicioFicha(ExercicioId, FichaId))
            {
                return Json(false);
            }

            ListaExercicio lista = new ListaExercicio
            {
                ExercicioId = ExercicioId,
                Frequencia = frequencia,
                Repeticoes = repeticoes,
                Carga = carga,
                FichaId = FichaId
            };

            if (ModelState.IsValid)
            {
                await _listaRepositorio.Inserir(lista);
                return Json(true);
            }
            else
            {
                return Json(false);
            }
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
        public async Task<JsonResult> Delete(int id)
        {
            await _repositorio.Excluir(id);
            return Json("Exercício excluído com sucesso.");
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
