using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoAcademia.Interfaces;
using ProjetoAcademia.Models;

namespace ProjetoAcademia.Controllers
{
    [Authorize]
    public class ProfessorController : Controller
    {
        private readonly IProfessorRepositorio _repositorio;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProfessorController(IProfessorRepositorio repositorio, IHostingEnvironment hostingEnvironment)
        {
            _repositorio = repositorio;
            _hostingEnvironment = hostingEnvironment;
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
        public async Task<IActionResult> Create([Bind("ProfessorId,Nome,Foto,Telefone,Turno")] Professor professor, IFormFile arquivo)
        {
            if (ModelState.IsValid)
            {
                var linkUpload = Path.Combine(_hostingEnvironment.WebRootPath, "Imagens");

                if(arquivo != null)
                {
                    using(var fileStream = new FileStream(Path.Combine(linkUpload, arquivo.FileName), FileMode.Create))
                    {
                        await arquivo.CopyToAsync(fileStream);
                        professor.Foto = "~/Imagens/" + arquivo.FileName;
                    }

                }

                await _repositorio.Inserir(professor);
                return RedirectToAction(nameof(Index));
            }

            return View(professor);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var professor = await _repositorio.BuscarPeloId(id);
            if (professor == null)
            {
                return NotFound();
            }

            TempData["Professor"] = professor.Foto;
            return View(professor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProfessorId,Nome,Foto,Telefone,Turno")] Professor professor, IFormFile arquivo)
        {
            if (id != professor.ProfessorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var linkUpload = Path.Combine(_hostingEnvironment.WebRootPath, "Imagens");

                if (arquivo != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(linkUpload, arquivo.FileName), FileMode.Create))
                    {
                        await arquivo.CopyToAsync(fileStream);
                        professor.Foto = "~/Imagens/" + arquivo.FileName;
                    }

                }
                else
                {
                    professor.Foto = TempData["Professor"].ToString();
                }

                await _repositorio.Atualizar(professor);
                return RedirectToAction(nameof(Index));
            }
            return View(professor);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _repositorio.Excluir(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<JsonResult> ProfessorExiste(string Nome, int ProfessorId)
        {
            if(ProfessorId == 0)
            {
                if(await _repositorio.VerificaProfessorPeloNome(Nome))
                {
                    return Json("Professor já existe");
                }

                return Json(true);
            }
            else
            {
                if(await _repositorio.ProfessorExiste(Nome, ProfessorId))
                {
                    return Json("Professor já existe");
                }

                return Json(true);
            }
        }
    }
}
