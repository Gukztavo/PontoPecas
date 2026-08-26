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
        public async Task<IActionResult> ListarProdutos()
        {
            var produtos = await _service.ListarProdutosAsync();
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
    }
}
