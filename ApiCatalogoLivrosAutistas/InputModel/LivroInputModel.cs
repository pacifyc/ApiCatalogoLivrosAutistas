using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoLivrosAutistas.InputModel
{
    public class LivroInputModel
    {

        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do livro deve conter entre 3 e 100 caracteres")]

        public string NomeLivro { get; set; }


        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome da editora deve conter entre 3 e 100 caracteres")]

        public string Editora { get; set; }


        [Range(1, 1000, ErrorMessage = "O preço deve ser de no mínimo 1 real e no maximo 1000 reais")]

        public double Preco { get; set; }
    }
}
