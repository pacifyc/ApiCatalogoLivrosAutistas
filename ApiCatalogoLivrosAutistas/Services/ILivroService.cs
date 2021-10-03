using ApiCatalogoLivrosAutistas.InputModel;
using ApiCatalogoLivrosAutistas.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoLivrosAutistas.Services
{
    public interface ILivroService : IDisposable
    {
        Task<List<LivroViewModel>> Obter(int pagina, int quantidae);

        Task<LivroViewModel> Obter(Guid id);

        Task<LivroViewModel> Inserir(LivroInputModel livro);

        Task Atualizar(Guid id, LivroInputModel livro);

        Task Atualizar(Guid id, double preco);

        Task Remover(Guid id);



    }
}
