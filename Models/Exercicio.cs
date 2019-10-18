using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoAcademia.Models
{
    public class Exercicio
    {
        public int ExercicioId {get; set;}

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Remote("ExercicioExiste", "Exercicio", AdditionalFields = "ExercicioId")]
        public string Nome {get; set;}

        public int CatExercicioId {get; set;}

        public CategoriaExercicio CategoriaExercicio {get; set;}

        public ICollection<ListaExercicio> ListaExercicios {get; set;}
    }
}