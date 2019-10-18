using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoAcademia.Models
{
    public class Objetivo
    {
        public int ObjetivoId {get; set;}

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Remote("ObjetivoExiste", "Objetivo", AdditionalFields = "ObjetivoId")]
        public string Nome {get; set;}

        [Required(ErrorMessage = "Campo obrigatório.")]
        [StringLength(500, ErrorMessage = "Use menos caracteres.")]
        public string Descricao {get; set;}

        public ICollection<Aluno> Alunos {get; set;}
    }
}