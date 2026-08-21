using Microsoft.EntityFrameworkCore;
using PontoPecas.Data;
using PontoPecas.DTOs;
using PontoPecas.Entities;

namespace PontoPecas.Services
{
    public class ProdutoService
    {
        private readonly AppDbContext _context;
        //constructor 
        public ProdutoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProdutoResponse> CriarProdutoAsync(CriarProdutoRequest request)
        {
            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(c => c.Id == request.CategoriaId);

            if (categoria == null)
            {
                throw new Exception("Categoria não encontrada");
            }

            if (request.PrecoSaida <= request.PrecoEntrada)
            {
                throw new Exception("O preço de saída deve ser maior que o preço de entrada");
            }

            if(request.QuantidadeEstoque < 0)
            {
                throw new Exception("A quantidade em estoque não pode ser negativa");
            }

            var produto = new Produto
            {
                Sku = request.Sku,
                Nome = request.Nome,
                PrecoEntrada = request.PrecoEntrada,
                PrecoSaida = request.PrecoSaida,
                QuantidadeEstoque = request.QuantidadeEstoque,
                Observacao = request.Observacao,
                CategoriaId = request.CategoriaId
            };

            _context.Produtos.Add(produto);

            await _context.SaveChangesAsync();

            return new ProdutoResponse
            {

                Id = produto.Id,
                Sku = produto.Sku,
                Nome = produto.Nome,
                PrecoEntrada = produto.PrecoEntrada,
                PrecoSaida = produto.PrecoSaida,
                QuantidadeEstoque = produto.QuantidadeEstoque,
                Observacao = produto.Observacao,
                CategoriaId = produto.CategoriaId,
                CategoriaNome = categoria.Nome
            };
        }

        public async Task<List<ProdutoResponse>> ListarProdutosAsync()
        {
            var produtos = await _context.Produtos
                .Select(produto => new ProdutoResponse
                {
                    Id = produto.Id,
                    Sku = produto.Sku,
                    Nome = produto.Nome,
                    PrecoEntrada = produto.PrecoEntrada,
                    PrecoSaida = produto.PrecoSaida,
                    QuantidadeEstoque = produto.QuantidadeEstoque,
                    Observacao = produto.Observacao,
                    CategoriaId = produto.CategoriaId,
                    CategoriaNome = produto.Categoria.Nome
                }).ToListAsync();
            return produtos;
                
        }
    }
}