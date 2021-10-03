﻿using ApiCatalogoLivrosAutistas.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoLivrosAutistas.Repositories
{
    public interface ILivroRepository : IDisposable
    {
        
         
        Task<List<Livro>> Obter(int pagina, int quantidade);
        Task<Livro> Obter(Guid id);
        Task<List<Livro>> Obter(string nomeLivro, string editora);
        Task Inserir(Livro livro);
        Task Atualizar(Livro livro);
        Task Remover(Guid id);


        
    }
}
