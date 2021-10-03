using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoLivrosAutistas.Exceptions
{
    public class LivroNaoCadastradoException : Exception
    {
        public LivroNaoCadastradoException()

            : base("Este Livro não está cadastrado")

        { }
    }
}
