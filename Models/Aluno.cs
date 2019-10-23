using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoAcademia.Models
{
    public class Aluno
    {
        public int AlunoId {get; set;}

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Remote("AlunoExiste", "Aluno", AdditionalFields = "AlunoId")]
        public string NomeCompleto {get; set;}

        [Required(ErrorMessage = "Campo obrigatório.")]
        public int Idade {get; set;}

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Range(10, 150, ErrorMessage = "Peso inválido.")]
        public double Peso {get; set;}

        public int ObjetivoId { get; set; }

        public Objetivo Objetivo { get; set; }

        public int ProfessorId {get; set;}

        public Professor Professor {get; set;}

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Range(1, 7, ErrorMessage = "Frequência inválida.")]
        public int FrequenciaSemanal {get; set;}

        public ICollection<Ficha> Fichas {get; set;}
    }
}