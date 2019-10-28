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
    public class FichaController : Controller
    {
        private readonly IFichaRepositorio _repositorio;
        private readonly IAlunoRepositorio _alunoRepositorio;

        public FichaController(
            IFichaRepositorio repositorio,
            IAlunoRepositorio alunoRepositorio
        )
        {
            _repositorio = repositorio;
            _alunoRepositorio = alunoRepositorio;
        }

        public async Task<IActionResult> Index(int AlunoId)
        {
            ViewBag.AlunoId = AlunoId;
            return View(await _repositorio.BuscarTodasFichasPeloAlunoId(AlunoId));
        }

        public async Task<IActionResult> Details(int FichaId)
        {
            var ficha = await _repositorio.BuscarFichaPeloAlunoId(FichaId);
            if (ficha == null)
            {
                return NotFound();
            }

            return View(ficha);
        }

        public IActionResult Create(int AlunoId)
        {
            Ficha ficha = new Ficha();
            ficha.AlunoId = AlunoId;
            return View(ficha);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FichaId,Nome,Cadastro,Validade,AlunoId")] Ficha ficha)
        {
            ficha.Cadastro = DateTime.Parse(DateTime.Now.ToShortDateString());
            ficha.Validade = DateTime.Parse(DateTime.Now.AddYears(1).ToShortDateString());

            if (ModelState.IsValid)
            {
                await _repositorio.Inserir(ficha);
                return RedirectToAction(nameof(Index), new { AlunoId = ficha.AlunoId });
            }
            return View(ficha);
        }

        public async Task<IActionResult> Edit(int FichaId)
        {
            var ficha = await _repositorio.BuscarPeloId(FichaId);
            if (ficha == null)
            {
                return NotFound();
            }
            return View(ficha);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int FichaId, [Bind("FichaId,Nome,Cadastro,Validade,AlunoId")] Ficha ficha)
        {
            if (FichaId != ficha.FichaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                ficha.Cadastro = DateTime.Parse(DateTime.Now.ToShortDateString());
                ficha.Validade = DateTime.Parse(DateTime.Now.AddYears(1).ToShortDateString());

                await _repositorio.Atualizar(ficha);
                return RedirectToAction(nameof(Index), new { AlunoId = ficha.AlunoId });
            }
            return View(ficha);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _repositorio.Excluir(id);
            return Json("Ficha excluída com sucesso.");
        }

        public async Task<JsonResult> FichaExiste(string Nome, int FichaId)
        {
            if(FichaId == 0)
            {
                if(await _repositorio.BuscaFichaPorNome(Nome))
                {
                    return Json("Ficha já existe");
                }
                return Json(true);
            }
            else
            {
                if(await _repositorio.FichaExiste(Nome, FichaId))
                {
                    return Json("Ficha já existe");
                }
                return Json(true);
            }
        }
    }
}
