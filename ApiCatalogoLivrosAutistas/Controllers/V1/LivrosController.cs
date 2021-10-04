﻿using ApiCatalogoLivrosAutistas.Exceptions;
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

        /// <summary>
        /// Buscar todos os livros de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os livros sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de reistros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de Livros</response>
        /// <response code="204">Caso não haja Livros</response> 
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

        /// <summary>
        /// Buscar um livro pelo seu Id
        /// </summary>
        /// <param name="idLivro">Id do livro buscado</param>
        /// <response code="200">Retorna o livro filtrado</response>
        /// <response code="204">Caso não haja livro com este id</response>
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

        /// <summary>
        /// Inserir um livro no catálogo
        /// </summary>
        /// <param name="livroInputModel">Dados do livro a ser inserido</param>
        /// <response code="200">Caso o livro seja inserido com sucesso</response>
        /// <response code="422">Caso já exista um livro com mesmo nome para a mesma produtora</response>
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

        /// <summary>
        /// Atualizar um livro no catálogo
        /// </summary>
        /// /// <param name="idLivro">Id do livro a ser atualizado</param>
        /// <param name="livroInputModel">Novos dados para atualizar o livro indicado</param>
        /// <response code="200">Caso o livro seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um livro com este Id</response>
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

        /// <summary>
        /// Excluir um livro
        /// </summary>
        /// /// <param name="idLivro">Id do Livro a ser excluído</param>
        /// <response code="200">Caso o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um livro com este Id</response>
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
