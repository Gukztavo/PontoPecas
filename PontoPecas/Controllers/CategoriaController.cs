using Microsoft.AspNetCore.Mvc;
using PontoPecas.DTOs;
using PontoPecas.Services;

namespace PontoPecas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaService _service;

        public CategoriaController(CategoriaService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> CriarCategoria(
            CriarCategoriaRequest request)
        {
            var categoria = await _service.CriarCategoriaAsync(request);
            return Ok(categoria);
        }
    }
}
