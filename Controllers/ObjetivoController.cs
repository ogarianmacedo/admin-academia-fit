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
    public class ObjetivoController : Controller
    {
        private readonly IObjetivoRepositorio _repositorio;

        public ObjetivoController(IObjetivoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repositorio.BuscarTodos().ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ObjetivoId,Nome,Descricao")] Objetivo objetivo)
        {
            if (ModelState.IsValid)
            {
                await _repositorio.Inserir(objetivo);
                return RedirectToAction(nameof(Index));
            }
            return View(objetivo);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var objetivo = await _repositorio.BuscarPeloId(id);
            if (objetivo == null)
            {
                return NotFound();
            }
            return View(objetivo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ObjetivoId,Nome,Descricao")] Objetivo objetivo)
        {
            if (id != objetivo.ObjetivoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _repositorio.Atualizar(objetivo);
                return RedirectToAction(nameof(Index));
            }
            return View(objetivo);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _repositorio.Excluir(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<JsonResult> ObjetivoExiste(string Nome, int ObjetivoId)
        {
            if(ObjetivoId == 0)
            {
                if(await _repositorio.VerificaObjetivoPeloNome(Nome))
                {
                    return Json("Objetivo já existe");
                }

                return Json(true);
            }
            else
            {
                if (await _repositorio.ObjetivoExiste(Nome, ObjetivoId))
                {
                    return Json("Objetivo já existe");
                }

                return Json(true);
            }
        }
    }
}
