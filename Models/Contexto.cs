using Microsoft.EntityFrameworkCore;
using ProjetoAcademia.Mapeamentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAcademia.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> opcoes) : base(opcoes)
        {
        }

        public DbSet<Administrador> Administradores {get; set;}
        public DbSet<Aluno> Alunos {get; set;}
        public DbSet<CategoriaExercicio> CategoriasExercicios {get; set;}
        public DbSet<Exercicio> Exercicios {get; set;}
        public DbSet<Ficha> Fichas {get; set;}
        public DbSet<ListaExercicio> ListasExercicios {get; set;}
        public DbSet<Objetivo> Objetivos {get; set;}
        public DbSet<Professor> Professores {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AdministradorMap());
            modelBuilder.ApplyConfiguration(new AlunoMap());
            modelBuilder.ApplyConfiguration(new CategoriaExercicioMap());
            modelBuilder.ApplyConfiguration(new ExercicioMap());
            modelBuilder.ApplyConfiguration(new FichaMap());
            modelBuilder.ApplyConfiguration(new ListaExercicioMap());
            modelBuilder.ApplyConfiguration(new ObjetivoMap());
            modelBuilder.ApplyConfiguration(new ProfessorMap());
        }

    }
}
