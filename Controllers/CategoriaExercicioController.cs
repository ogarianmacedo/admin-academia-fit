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
    public class CategoriaExercicioController : Controller
    {
        private readonly ICategoriaExercicioRepositorio _repositorio;

        public CategoriaExercicioController(ICategoriaExercicioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IActionResult> Index()
        {
            return View(_repositorio.BuscarTodos());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatExercicioId,Nome")] CategoriaExercicio categoriaExercicio)
        {
            if (ModelState.IsValid)
            {
                await _repositorio.Inserir(categoriaExercicio);
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaExercicio);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaExercicio = await _repositorio.BuscarPeloId(id);
            if (categoriaExercicio == null)
            {
                return NotFound();
            }
            return View(categoriaExercicio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CatExercicioId,Nome")] CategoriaExercicio categoriaExercicio)
        {
            if (id != categoriaExercicio.CatExercicioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _repositorio.Atualizar(categoriaExercicio);
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaExercicio);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _repositorio.Excluir(id);
            return Json("Categoria excluída com sucesso.");
        }

        public async Task<JsonResult> CategoriaExiste(string nome, int CatExercicioId)
        {
            if(CatExercicioId == 0)
            {
                if(await _repositorio.VerificaCategoria(nome))
                {
                    return Json("Já existe essa categoria");
                }

                return Json(true);
            }
            else
            {
                if(await _repositorio.CategoriaExiste(nome, CatExercicioId))
                {
                    return Json("Já existe essa categoria");
                }

                return Json(true);
            }
        }
    }
}
