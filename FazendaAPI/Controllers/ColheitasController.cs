
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FazendaAPI.Data;
using Models;
using Models.DTO;
using System.Globalization;

namespace FazendaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColheitasController : ControllerBase
    {
        private readonly FazendaAPIContext _context;
        private readonly PlantacoesController _plantacoesController;

        public ColheitasController(FazendaAPIContext context, PlantacoesController plantacoesController)
        {
            _context = context;
            _plantacoesController = plantacoesController;
        }

        // GET: api/Colheitas
        [HttpGet("concluida")]
        public async Task<ActionResult<IEnumerable<Colheita>>> GetColheitaConcluidas()
        {
            if (_context.Colheita == null)
            {
                return NotFound();
            }

            var colheitas = await _context.Colheita.Include(p => p.Plantacao).Where(c => c.Status == "Concluída").ToListAsync();

            if (colheitas.Count == 0)
            {
                return NotFound("Nenhuma colheita ativa foi encontrada.");
            }

            return colheitas;
        }

        [HttpGet("consumida")]
        public async Task<ActionResult<IEnumerable<Colheita>>> GetColheitaConsumidas()
        {
            if (_context.Colheita == null)
            {
                return NotFound();
            }

            var colheitas = await _context.Colheita.Include(p => p.Plantacao).Where(c => c.Status == "Consumida").ToListAsync();

            if (colheitas.Count == 0)
            {
                return NotFound("Nenhuma colheita consumida foi encontrada.");
            }

            return colheitas;
        }

        [HttpGet("cancelada")]
        public async Task<ActionResult<IEnumerable<Colheita>>> GetColheitaCanceladas()
        {
            if (_context.Colheita == null)
            {
                return NotFound();
            }

            var colheitas = await _context.Colheita.Include(p => p.Plantacao).Where(c => c.Status == "Cancelada").ToListAsync();

            if (colheitas.Count == 0)
            {
                return NotFound("Nenhuma colheita cancelada foi encontrada.");
            }

            return colheitas;
        }


        // GET: api/Colheitas/5
        [HttpGet("{numeroRegistro}")]
        public async Task<ActionResult<Colheita>> GetColheita(int numeroRegistro)
        {
            if (_context.Colheita == null)
            {
                return NotFound();
            }
            var colheita = await _context.Colheita.Include(p => p.Plantacao).Where(c => c.NumeroRegistro == numeroRegistro).SingleOrDefaultAsync(c => c.NumeroRegistro == numeroRegistro);

            if (colheita == null)
            {
                return NotFound("Nenhuma colheita encontrada.");
            }

            return colheita;
        }

        [HttpGet("concluida/{numeroRegistro}")]
        public async Task<ActionResult<Colheita>> GetColheitaConcluida(int numeroRegistro)
        {
            if (_context.Colheita == null)
            {
                return NotFound();
            }
            var colheita = await _context.Colheita.Include(p => p.Plantacao).Where(c => c.NumeroRegistro == numeroRegistro && c.Status == "Concluída").SingleOrDefaultAsync(c => c.NumeroRegistro == numeroRegistro && c.Status == "Concluída");

            if (colheita == null)
            {
                return NotFound("Nenhuma colheita encontrada.");
            }

            return colheita;
        }

        


        [HttpGet("data")]
        public async Task<ActionResult<IEnumerable<Colheita>>> GetColheitaByDate([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            if (_context.Colheita == null || startDate == default || endDate == default)
            {
                return NotFound("Nenhuma colheita encontrada.");
            }

            var buscar = await _context.Colheita.Include(p => p.Plantacao)
                .Where(c => c.DataColheita >= startDate && c.DataColheita <= endDate)
                .ToListAsync();

            if (buscar.Count == 0)
            {
                return NotFound("Nenhuma colheita encontrada neste intervalo de datas.");
            }

            return buscar;
        }

        [HttpGet("concluida/Plantacao/{id}")]
        public async Task<ActionResult<Colheita>> GetColheitaByPlantacao(string id)
        {
            if (_context.Colheita == null)
            {
                return NotFound("Nenhuma colheita encontrada.");
            }

            var colheitas = await _context.Colheita.Include(p => p.Plantacao).Where(c => c.Plantacao.Id == id && c.Status == "Concluída")
                .SingleOrDefaultAsync(c => c.Plantacao.Id == id && c.Status == "Concluída");

            if (colheitas == null)
            {
                return NotFound("Nenhuma colheita encontrada para a plantação selecionada.");
            }

            return colheitas;
        }


        // PUT: api/Colheitas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{numeroRegistro}")]
        public async Task<IActionResult> PutColheita(int numeroRegistro, ColheitaPutDTO colheitaPutDTO)
        {
            var colheita = await _context.Colheita.Include(p => p.Plantacao).Where(c => c.NumeroRegistro == numeroRegistro).SingleOrDefaultAsync(c => c.NumeroRegistro == numeroRegistro); 

            if (colheita == null)
            {
                return NotFound("Nenhuma colheita encontrada.");
            }

            var existeProduto = await _context.Produto.Include(c => c.ColheitaOrigem).Where(p => p.ColheitaOrigem.NumeroRegistro == numeroRegistro).SingleOrDefaultAsync(p => p.ColheitaOrigem.NumeroRegistro == numeroRegistro);

            if (existeProduto != null)
            {
                return Conflict("Um produto já foi gerado utilizando o lote desta colheita. As informações da colheita não podem mais serem alteradas.");
            }

            colheita.DataColheita = DateTime.ParseExact(colheitaPutDTO.DataColheita, "dd/MM/yyyy", CultureInfo.GetCultureInfo("pt-BR"));
            colheita.QuantidadeColhida = colheitaPutDTO.QuantidadeColhida;


            if (colheita.DataColheita > DateTime.Now)
            {
                return Conflict("A data de colheita não pode ser registrada em um período maior do que o dia atual. Por favor, escolha uma data dentro desse intervalo.");
            }

            if (colheita.Plantacao.DataDePlantio > colheita.DataColheita)
            {
                return Conflict("A data de colheita deve ser registrada após a data de plantio da plantação. Por favor, escolha uma data posterior à data de plantio.");
            }


            if (colheita.QuantidadeColhida <= 0)
            {
                return Conflict("A quantidade colhida deve ser maior que zero. Caso o contrário, deve-se ser registrado \"perda\" na plantação ao invés de \"colheita\".");
            }

            _context.Entry(colheita).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColheitaExists(numeroRegistro))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Colheitas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Colheita>> PostColheita(ColheitaDTO colheitaDTO)
        {
            if (_context.Colheita == null)
            {
                return Problem("Entity set 'FazendaAPIContext.Colheita'  is null.");
            }

            var colheita = new Colheita()
            {
                Plantacao = await _context.Plantacao.FindAsync(colheitaDTO.Plantacao),
                DataColheita = DateTime.ParseExact(colheitaDTO.DataColheita, "dd/MM/yyyy", CultureInfo.GetCultureInfo("pt-BR")),
                QuantidadeColhida = colheitaDTO.QuantidadeColhida,
                Status = "Concluída"
            };


            if (colheita.Plantacao == null)
            {
                return NotFound("Nenhuma plantação encontrada.");
            }

            if (colheita.Plantacao.Status != "Ativo")
            {
                return Conflict("A plantação selecionada não está ativa. Por favor, selecione uma plantação ativa.");
            }

            
            if (colheita.DataColheita > DateTime.Now)
            {
                return Conflict("A data de colheita não pode ser registrada em um período maior do que o dia atual. Por favor, escolha uma data dentro desse intervalo.");
            }

            if (colheita.Plantacao.DataDePlantio > colheita.DataColheita)
            {
                return Conflict("A data de colheita deve ser registrada após a data de plantio da plantação. Por favor, escolha uma data posterior à data de plantio.");
            }


            if (colheita.QuantidadeColhida <= 0)
            {
                return Conflict("A quantidade colhida deve ser maior que zero. Caso o contrário, deve-se ser registrado \"perda\" na plantação ao invés de \"colheita\".");
            }

            if (colheita.QuantidadeColhida > 500)
            {
                return Conflict("A quantidade colhida deve ser menor ou igual a 500 unidades. Os canteiros e estufas de cultivo não permitem uma densidade de produção maior do que 500 unidades por lote de qualquer produto.");
            }

            _context.Colheita.Add(colheita);
            await _plantacoesController.PatchColheita(colheita.Plantacao.Id);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetColheitaConcluidas", new { id = colheita.NumeroRegistro }, colheita);
        }

        // DELETE: api/Colheitas/5
        [HttpDelete("{numeroRegistro}")]
        public async Task<IActionResult> DeleteColheita(int numeroRegistro)
        {
            if (_context.Colheita == null)
            {
                return NotFound();
            }

            var colheita = await _context.Colheita.Include(p => p.Plantacao).Where(c => c.NumeroRegistro == numeroRegistro).SingleOrDefaultAsync();
            var plantacaoId = colheita.Plantacao.Id;
            var plantacao = await _context.Plantacao.FindAsync(plantacaoId);

            if (colheita == null)
            {
                return NotFound("Nenhuma colheita encontrada.");
            }

            var produtoColheita = await _context.Produto.Include(c => c.ColheitaOrigem).Where(p => p.ColheitaOrigem.NumeroRegistro == numeroRegistro).SingleOrDefaultAsync(p => p.ColheitaOrigem.NumeroRegistro == numeroRegistro);

            if(produtoColheita != null && produtoColheita.Status == "Ativo")
            {
                return Conflict("A colheita não pode ser excluída. Um produto foi gerado utilizando o lote desta colheita, verifique-o e altere seu status antes de deletá-la.");
            }

            colheita.Status = "Cancelada";
            plantacao.Status = "Ativo";
            plantacao.Colheita = "Cultivando";

            _context.Entry(plantacao).State = EntityState.Modified;
            _context.Entry(colheita).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("Registro de colheita cancelado com sucesso! Produtos e vendas referentes ao lote dessa colheita foram inválidados.");
        }

        private bool ColheitaExists(int id)
        {
            return (_context.Colheita?.Any(e => e.NumeroRegistro == id)).GetValueOrDefault();
        }
    }
}


