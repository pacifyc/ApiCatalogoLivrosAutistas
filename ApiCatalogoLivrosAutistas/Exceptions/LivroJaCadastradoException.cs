using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoLivrosAutistas.Exceptions
{
    public class LivroJaCadastradoException : Exception
    {

        public LivroJaCadastradoException()

            : base("Este Livro já está cadastrado")

        { }
    }
}
