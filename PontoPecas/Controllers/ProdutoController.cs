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
    }
}
