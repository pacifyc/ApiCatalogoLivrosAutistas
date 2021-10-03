using ApiCatalogoLivrosAutistas.InputModel;
using ApiCatalogoLivrosAutistas.Repositories;
using ApiCatalogoLivrosAutistas.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoLivrosAutistas.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;

        public LivroService(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public async Task<List<LivroViewModel>> Obter(int pagina, int quantidade)
        {
            var livro = await _livroRepository.Obter(pagina, quantidade);

            return livro.Select(livro => new LivroViewModel
            {
                Id = livro.Id,
                NomeLivro = livro.NomeLivro,
                Editora = livro.Editora,
                Preco = livro.Preco
            })
                               .ToList();
        }

        public async Task<LivroViewModel> Obter(Guid id)
        {
            var livro = await _livroRepository.Obter(id);

            if (livro == null)
                return null;

            return new LivroViewModel
            {
                Id = livro.Id,
                NomeLivro = livro.NomeLivro,
                Editora = livro.Editora,
                Preco = livro.Preco
            };
        }

        public async Task<LivroViewModel> Inserir(LivroInputModel livro)
        {
            var entidadeLivro = await _livroRepository.Obter(livro.NomeLivro, livro.Editora);

            if (entidadeLivro.Count > 0)
                throw new LivroJaCadastradoException();

            var livroInsert = new Livro
            {
                Id = Guid.NewGuid(),
                NomeLivro = livro.NomeLivro,
                Editora = livro.Editora,
                Preco = livro.Preco
            };

            await _livroRepository.Inserir(livroInsert);

            return new LivroViewModel
            {
                Id = livroInsert.Id,
                NomeLivro = livro.NomeLivro,
                Editora = livro.Editora,
                Preco = livro.Preco
            };
        }

        public async Task Atualizar(Guid id, LivroInputModel livro)
        {
            var entidadeLivro = await _livroRepository.Obter(id);

            if (entidadeLivro == null)
                throw new LivroNaoCadastradoException();

            entidadeLivro.NomeLivro = livro.NomeLivro;
            entidadeLivro.Editora = livro.Editora;
            entidadeLivro.Preco = livro.Preco;

            await _livroRepository.Atualizar(entidadeLivro);
        }

        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeJogo = await _livroRepository.Obter(id);

            if (entidadeJogo == null)
                throw new LivroNaoCadastradoException();

            entidadeJogo.Preco = preco;

            await _livroRepository.Atualizar(entidadeJogo);
        }

        public async Task Remover(Guid id)
        {
            var jogo = await _livroRepository.Obter(id);

            if (jogo == null)
                throw new LivroNaoCadastradoException();

            await _livroRepository.Remover(id);
        }

        public void Dispose()
        {
            _livroRepository?.Dispose();
        }

    }
}
