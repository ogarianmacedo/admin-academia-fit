using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoAcademia.Models 
{
    public class CategoriaExercicio
    {
        public int CatExercicioId {get; set;}

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Remote("CategoriaExiste", "CategoriaExercicio", AdditionalFields = "CatExercicioid")]
        public string Nome {get; set;}

        public ICollection<Exercicio> Exercicios {get; set;}
    }
}