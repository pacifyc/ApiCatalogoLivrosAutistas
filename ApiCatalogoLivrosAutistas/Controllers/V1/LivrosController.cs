using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoLivrosAutistas.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<object>>> Obter()
        {
            return Ok();
        }


        [HttpGet("{idLivro:guid}")]
        public async Task<ActionResult<object>> ( Guid idLivro )
        {
	        return Ok();

        }

        [HttpPost]
        public async Task<ActionResult<object>> InserirLiro(object jogo)
        {
            return Ok();
        }


        [HttpPut("{idLivro:guid}")]
        public async Task<ActionResult AtualizarLivro(Guid idLivro, object livro)
        {
	        return Ok();
        }

    }
}
