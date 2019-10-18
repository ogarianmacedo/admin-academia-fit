using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ProjetoAcademia.Models
{
    public class Ficha
    {
        public int FichaId {get; set;}

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome {get; set;}

        public DateTime Cadastro {get; set;}

        public DateTime Validade {get; set;}

        public int AlunoId {get; set;}

        public Aluno Aluno {get; set;}

        public ICollection<ListaExercicio> ListaExercicios {get; set;}
    }
}