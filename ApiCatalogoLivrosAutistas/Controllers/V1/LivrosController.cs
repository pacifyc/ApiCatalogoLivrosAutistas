using ApiCatalogoLivrosAutistas.Exceptions;
using ApiCatalogoLivrosAutistas.InputModel;
using ApiCatalogoLivrosAutistas.Services;
using ApiCatalogoLivrosAutistas.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoLivrosAutistas.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroService _livroService;
        public LivrosController(ILivroService livroService)
        {
            _livroService = livroService;

        }

        /*
        [HttpGet]
        public async Task<ActionResult<List<object>>> Obter()
        {
            return Ok();
        }
        */
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LivroViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var livro = await _livroService.Obter(pagina, quantidade);

            if (livro.Count() == 0)
                return NoContent();

            return Ok(livro);
        }


        /*
        [HttpGet("{idLivro:guid}")]
        public async Task<ActionResult<LivroViewModel>> (Guid idLivro)
        {
            return Ok();

        }
        */
        
        [HttpGet("{idLivro:guid}")]
        public async Task<ActionResult<LivroViewModel>> Obter([FromRoute] Guid idLivro)
        {
            var livro = await _livroService.Obter(idLivro);

            if (livro == null)
                return NoContent();

            return Ok(livro);
        }


        /*
        [HttpPost]
        public async Task<ActionResult<LivroViewModel>> InserirLivro(LivroInputModel livro)
        {
            return Ok();
        }
        */

        [HttpPost]
        public async Task<ActionResult<LivroViewModel>> InserirLivro([FromBody] LivroInputModel livroInputModel)
        {
            try
            {
                var livro = await _livroService.Inserir(livroInputModel);

                return Ok(livro);
            }
            catch (LivroJaCadastradoException)
            //atch(Exception ex)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }
        }


        /*
        [HttpPut("{idLivro:guid}")]
        public async Task<ActionResult> AtualizarLivro(Guid idLivro, LivroImputModel)
        {
            return Ok();
        }
        */

        
        [HttpPut("{idLivro:guid}")]
        public async Task<ActionResult> AtualizarLivro([FromRoute] Guid idLivro, [FromBody] LivroInputModel livroInputModel)
        {
            try
            {
                await _livroService.Atualizar(idLivro, livroInputModel);

                return Ok();
            }
            catch (LivroNaoCadastradoException)
            //catch (Exception ex)
            {
                return NotFound("Não existe este jogo");
            }
        }



        /*
        [HttpPut("{idLivro:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarLivro(Guid idLivro, object livro livro)
        {
            return Ok();
        }
        */

        
        [HttpPatch("{idLivro:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarLivro([FromRoute] Guid idLivro, [FromRoute] double preco)
        {
            try
            {
                await _livroService.Atualizar(idLivro, preco);

                return Ok();
            }
            catch (LivroNaoCadastradoException)
            //catch (Exception ex)
            {
                return NotFound("Não existe este jogo");
            }
        }


        /*
        [HttpDelete("{idLivro:guid}")]
        public async Task<ActionResult> ApagarLivro(Guid idLivro)
        {
            return Ok();
        }
        */

        
        [HttpDelete("{idLivro:guid}")]
        public async Task<ActionResult> ApagarLivro([FromRoute] Guid idLivro)
        {
            try
            {
                await _livroService.Remover(idLivro);

                return Ok();
            }
            catch (LivroNaoCadastradoException)
            //catch(Exception ex)
            {
                return NotFound("Não existe este jogo");
            }
        }


        
    }
}
