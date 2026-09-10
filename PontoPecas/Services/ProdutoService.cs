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

        public async Task<List<ProdutoResponse>> ListarProdutosAsync(int page, int pageSize)
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
                })
                .Skip((page -1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return produtos;
                
        }

        public async Task<ProdutoResponse?> BuscarProdutoPorIdAsync(int id)
        {
            var produto = await _context.Produtos
                .Where(p => p.Id == id)
                .Select(p => new ProdutoResponse
                {
                    Id = p.Id,
                    Sku = p.Sku,
                    Nome = p.Nome,
                    PrecoEntrada = p.PrecoEntrada,
                    PrecoSaida = p.PrecoSaida,
                    QuantidadeEstoque = p.QuantidadeEstoque,
                    Observacao = p.Observacao,
                    CategoriaId = p.CategoriaId,
                    CategoriaNome = p.Categoria.Nome
                })
                .FirstOrDefaultAsync();
            return produto;
        }

        public async Task<ProdutoResponse?> AtualizarProdutoAsync(int id, AtualizarProdutoRequest request)
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null)
            {
                return null;
            }

            produto.Sku = request.Sku;
            produto.Nome = request.Nome;
            produto.PrecoEntrada = request.PrecoEntrada;
            produto.PrecoSaida = request.PrecoSaida;
            produto.QuantidadeEstoque = request.QuantidadeEstoque;
            produto.Observacao = request.Observacao;
            produto.CategoriaId = request.CategoriaId;

            await _context.SaveChangesAsync();
            return await BuscarProdutoPorIdAsync(id);
        }
        // agora precisamos pegar o valor da categoria para atualizar o produto pra isso temos a função que 
        // busca no banco de dados a categoria pelo id e se não encontrar lança uma exceção
        // pra proximo passso chamar no controle e ver a rota de atualizar produto
    }
}