﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjetoAcademia.Interfaces;
using ProjetoAcademia.Models;
using ProjetoAcademia.Repositorios;
using Rotativa.AspNetCore;

namespace ProjetoAcademia
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //configura conexao com banco
            services.AddDbContext<Contexto>(opcoes => opcoes.UseSqlServer(Configuration.GetConnectionString("Conexao")));

            //incluindo os repositorios
            services.AddTransient<ICategoriaExercicioRepositorio, CategoriaExercicioRepositorio>();
            services.AddTransient<IAdministradorRepositorio, AdministradorRepositorio>();
            services.AddTransient<IExercicioRepositorio, ExercicioRepositorio>();
            services.AddTransient<IProfessorRepositorio, ProfessorRepositorio>();
            services.AddTransient<IObjetivoRepositorio, ObjetivoRepositorio>();

            //configura http para utilizar sessões
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSession(opcoes =>
            {
                opcoes.IdleTimeout = TimeSpan.FromHours(1);
            });

            //configura autenticação 
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opcoes => {
                    opcoes.LoginPath = "/Administradores/Login";
                });
             
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseSession();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            RotativaConfiguration.Setup(env);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Administrador}/{action=Login}/{id?}");
            });
        }
    }
}
