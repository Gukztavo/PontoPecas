using Microsoft.AspNetCore.Mvc; 
using PontoPecas.Services;
using PontoPecas.DTOs;

namespace PontoPecas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _service;

        public ProdutoController(ProdutoService service)
            {
            _service = service;
            }

        [HttpPost]
        public async Task<IActionResult> CriarProduto(
            CriarProdutoRequest request)
        {
            var produto = await _service.CriarProdutoAsync(request);
            return Ok(produto);
        }

        [HttpGet]
        public async Task<IActionResult> ListarProdutos(int page = 1,int pageSize = 10)
        {
            if (page < 1)
            {
                return BadRequest("A página deve ser maior ou igual a 1.");
            }

            if (pageSize < 1)
            {
                return BadRequest("O tamanho da página deve ser maior ou igual a 1.");
            }

            var produtos = await _service.ListarProdutosAsync(page, pageSize);

            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscasProdutoPorId(int id)
        {
            var produto = await _service.BuscarProdutoPorIdAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return Ok(produto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarProduto(int id, AtualizarProdutoRequest request)
        {
            var produto = await _service.AtualizarProdutoAsync(id, request);
            if (produto == null)
            {
                return NotFound();
            }
            return Ok(produto);
        }
    }
}
