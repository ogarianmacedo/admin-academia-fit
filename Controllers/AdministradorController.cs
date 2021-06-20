using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoAcademia.Interfaces;
using ProjetoAcademia.Models;
using ProjetoAcademia.ViewModels;

namespace ProjetoAcademia.Controllers
{
    public class AdministradorController : Controller
    {
        private readonly IAdministradorRepositorio _repositorio;

        public AdministradorController(IAdministradorRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                HttpContext.SignOutAsync();
                HttpContext.Session.Clear();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdministradorViewModel adminViewModel)
        {
            if(!_repositorio.AdministradorExiste(adminViewModel.Email, adminViewModel.Senha))
            {
                ModelState.AddModelError(string.Empty, "E-mail e/ou senha inválidos.");
                return View(adminViewModel);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, adminViewModel.Email)
            };

            var userIdentity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            await HttpContext.SignInAsync(principal);

            HttpContext.Session.SetString("Usuario", adminViewModel.Email);
            ViewData["Usuario"] = HttpContext.Session.GetString("Usuario");

            return RedirectToAction("Index", "Professor");
        }

        [Authorize]
        public async Task<IActionResult> Sair()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdminId,Nome,Email,Senha")] Administrador administrador)
        {
            if (ModelState.IsValid)
            {
                await _repositorio.Inserir(administrador);
                return RedirectToAction("Index", "Professor");
            }

            return View(administrador);
        }
    }
}