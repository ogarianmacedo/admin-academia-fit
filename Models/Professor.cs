using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoAcademia.Models
{
    public class Professor
    {
        public int ProfessorId {get; set;}

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Remote("ProfessorExiste", "Professor", AdditionalFields = "ProfessorId")]
        public string Nome {get; set;}

        public string Foto {get; set;}

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Telefone {get; set;}

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Turno {get; set;}

        public ICollection<Aluno> Alunos {get; set;}
    }
}