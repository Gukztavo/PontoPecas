using Microsoft.EntityFrameworkCore;
using PontoPecas.Data;
using PontoPecas.DTOs;
using PontoPecas.Entities;

namespace PontoPecas.Services
{
    public class CategoriaService
    {
        private readonly AppDbContext _context;
    
        public CategoriaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Categoria> CriarCategoriaAsync(CriarCategoriaRequest request)
        {
          var categoriaExistente = await _context.Categorias
                .FirstOrDefaultAsync(c => c.Nome == request.Nome);
        
            if (categoriaExistente != null)
            {
                throw new Exception("Categoria já existe.");
            }

            var categoria = new Categoria
            {
                Nome = request.Nome
            };

            _context.Categorias.Add(categoria);

            await _context.SaveChangesAsync();

            return categoria;
        }

    }
}
