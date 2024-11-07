
using FazendaAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTO;

namespace FazendaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly FazendaAPIContext _context;

        public ProdutosController(FazendaAPIContext context)
        {
            _context = context;
        }

        // GET: api/Produtos
        [HttpGet("ativos")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutoAtivo()
        {
            if (_context.Produto == null)
            {
                return NotFound();
            }

            var buscar = await _context.Produto.Include(c => c.ColheitaOrigem).Include(c => c.ColheitaOrigem.Plantacao).Where(p => p.Status == "Ativo").ToListAsync();

            if (buscar.Count == 0)
            {
                return NotFound("Nenhum produto encontrado");
            }

            return buscar;
        }

        [HttpGet("inativos")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutoInativo()
        {
            if (_context.Produto == null)
            {
                return NotFound();
            }

            var buscar = await _context.Produto.Include(c => c.ColheitaOrigem).Where(p => p.Status == "Inativo").ToListAsync();

            if (buscar.Count == 0)
            {
                return NotFound("Nenhum produto encontrado");
            }

            return buscar;
        }

        [HttpGet("vencidos")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutoVencido()
        {
            if (_context.Produto == null)
            {
                return NotFound();
            }

            var buscar = await _context.Produto.Include(c => c.ColheitaOrigem).Where(p => p.Status == "Vencido").ToListAsync();

            if (buscar.Count == 0)
            {
                return NotFound("Nenhum produto encontrado");
            }

            return buscar;
        }

        // GET: api/Produtos/5
        [HttpGet("{lote}")]
        public async Task<ActionResult<Produto>> GetProduto(string lote)
        {
            if (_context.Produto == null)
            {
                return NotFound("Nenhum produto encontrado");
            }
            var produto = await _context.Produto.Include(c => c.ColheitaOrigem).Where(p => p.Lote == lote).SingleOrDefaultAsync(p => p.Lote == lote);

            if (produto == null)
            {
                return NotFound("Nenhum produto encontrado");
            }

            return produto;
        }

        // PUT: api/Produtos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{lote}")]
        public async Task<IActionResult> PutProduto(string lote, ProdutoPutDTO produto)
        {
            var produtoAtual = await _context.Produto.Include(c => c.ColheitaOrigem).Where(p => p.Lote == lote).SingleOrDefaultAsync(p => p.Lote == lote);

            if (produtoAtual.Status == "Vencido")
            {
                return Conflict("O produto está vencido e não pode ser alterado.");
            }



            produtoAtual.CategoriaProduto = produto.CategoriaProduto;
            produtoAtual.ValorUnitario = produto.ValorUnitario;
            produtoAtual.Quantidade = produto.Quantidade;
            produtoAtual.Status = produto.Status;

            if (produtoAtual.Quantidade <= 0)
            {
                return Conflict("A quantidade do produto não pode ser menor ou igual a zero.");
            }

            if (produtoAtual.Quantidade > produtoAtual.ColheitaOrigem.QuantidadeColhida)
            {
                return Conflict("A quantidade do produto não pode ser maior que a quantidade colhida na colheita de origem.");
            }

            if (produtoAtual.ValorUnitario <= 0)
            {
                return Conflict("O valor unitário do produto não pode ser menor ou igual a zero.");
            }

            if (DateTime.Now > produtoAtual.DataValidade)
            {
                produtoAtual.Status = "Vencido";
                _context.Entry(produtoAtual).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Conflict("A nova data informada indica que o produto está vencido e não pode ser modificado. O status do produto foi alterado para vencido.");
            }

            _context.Entry(produtoAtual).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(lote))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Produto atualizado com sucesso!");
        }

        // POST: api/Produtos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(ProdutoDTO produtoDTO)
        {
            if (_context.Produto == null)
            {
                return Problem("Entity set 'FazendaAPIContext.Produto'  is null.");
            }

            var colheita = await _context.Colheita.Include(p => p.Plantacao).Where(c => c.NumeroRegistro == produtoDTO.ColheitaOrigem).SingleOrDefaultAsync(c => c.NumeroRegistro == produtoDTO.ColheitaOrigem);


            if (colheita == null)
            {
                return NotFound("Colheita não encontrada");
            }

            if (colheita.Status == "Cancelada")
            {
                return BadRequest("A colheita informada foi cancelada e, portanto, não pode ser usada como lote de novos produtos.");
            }

            if (colheita.Status == "Expirada")
            {
                return BadRequest("A colheita informada está vencida e, portanto, não pode ser usada como lote de novos produtos.");
            }

            var produtoColheita = await _context.Produto.Include(c => c.ColheitaOrigem).Where(p => p.ColheitaOrigem.NumeroRegistro == colheita.NumeroRegistro).SingleOrDefaultAsync(p => p.ColheitaOrigem.NumeroRegistro == colheita.NumeroRegistro);


            if (produtoColheita != null)
            {
                return Conflict("Um produto já foi cadastrado com o lote da colheita informada.");
            }

            var produto = new Produto()
            {
                Lote = colheita.Plantacao.Id,
                NomeProduto = colheita.Plantacao.Nome,
                CategoriaProduto = produtoDTO.CategoriaProduto,
                ColheitaOrigem = colheita,
                Quantidade = colheita.QuantidadeColhida,
                ValorUnitario = produtoDTO.ValorUnitario,
                DataValidade = colheita.DataColheita.AddDays(10),
                Status = "Ativo"

            };

            if (DateTime.Now > produto.DataValidade)
            {
                colheita.Status = "Expirada";
                _context.Entry(colheita).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Conflict("O produto colhido já está vencido. O status da colheita foi alterado para expirada");
            }

            if (produto.Quantidade <= 0)
            {
                return Conflict("A quantidade do produto não pode ser menor ou igual a zero.");
            }

            if (produto.ValorUnitario <= 0)
            {
                return Conflict("O valor unitário do produto não pode ser menor ou igual a zero.");
            }

            _context.Produto.Add(produto);

            colheita.Status = "Consumida";

            _context.Entry(colheita).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProdutoExists(produto.Lote))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProdutoAtivo", new { id = produto.Lote }, produto);
        }

        // DELETE: api/Produtos/5
        [HttpDelete("{lote}")]
        public async Task<IActionResult> DeleteProduto(string lote)
        {
            if (_context.Produto == null)
            {
                return NotFound();
            }
            var produto = await _context.Produto.FindAsync(lote);

            if (produto == null)
            {
                return NotFound("Nenhum produto encontrado.");
            }

            produto.Status = "Inativo";
            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("Produto inativado com sucesso!");
        }

        private bool ProdutoExists(string id)
        {
            return (_context.Produto?.Any(e => e.Lote == id)).GetValueOrDefault();
        }
    }
}
