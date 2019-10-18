using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjetoAcademia.Models
{
    public class Administrador
    {
        public int AdminId {get; set;}
        
        [Required(ErrorMessage = "Campo obrigat�rio.")]
        public string Nome {get; set;}

        [Required(ErrorMessage = "Campo obrigat�rio.")]
        public string Email {get; set;}

        [Required(ErrorMessage = "Campo obrigat�rio.")]
        public string Senha {get; set;}
    }
}