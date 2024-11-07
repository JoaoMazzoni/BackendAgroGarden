using FazendaAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using System.Globalization;

namespace FazendaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrosInfestacoesController : ControllerBase
    {
        private readonly FazendaAPIContext _context;
        private readonly PlantacoesController _plantacoesController;

        public RegistrosInfestacoesController(FazendaAPIContext context, PlantacoesController plantacoesController)
        {
            _context = context;
            _plantacoesController = plantacoesController;
        }

        [HttpGet("ativos")]
        public async Task<ActionResult<IEnumerable<RegistroInfestacao>>> GetRegistroInfestacaoAtivos()
        {
            if (_context.RegistroInfestacao == null)
            {
                return NotFound();
            }

            var buscar = await _context.RegistroInfestacao.Include(p => p.Plantacao).Include(i => i.Praga).Where(r => r.Status == "Ativo").ToListAsync();

            if (buscar.Count == 0)
            {
                return NotFound("Nenhum registro de infestação foi encontrado.");
            }

            return buscar;
        }

        [HttpGet("concluidos")]
        public async Task<ActionResult<IEnumerable<RegistroInfestacao>>> GetRegistroInfestacaoConcluidos()
        {
            if (_context.RegistroInfestacao == null)
            {
                return NotFound();
            }

            var buscar = await _context.RegistroInfestacao.Include(p => p.Plantacao).Include(i => i.Praga).Where(r => r.Status == "Concluido").ToListAsync();

            if (buscar.Count == 0)
            {
                return NotFound("Nenhum registro de infestação concluído foi encontrado.");
            }


            return buscar;
        }

        [HttpGet("perda")]
        public async Task<ActionResult<IEnumerable<RegistroInfestacao>>> GetRegistroInfestacaoPerda()
        {
            if (_context.RegistroInfestacao == null)
            {
                return NotFound();
            }

            var buscar = await _context.RegistroInfestacao.Include(p => p.Plantacao).Include(i => i.Praga).Where(r => r.Status == "Perda").ToListAsync();

            if (buscar.Count == 0)
            {
                return NotFound("Nenhum registro de infestação com perda de plantação foi encontrado.");
            }

            return buscar;
        }

        // GET: api/RegistrosInfestacoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RegistroInfestacao>> GetRegistroInfestacao(int id)
        {
            if (_context.RegistroInfestacao == null)
            {
                return NotFound();
            }
            var registroInfestacao = await _context.RegistroInfestacao.Include(p => p.Plantacao).Include(i => i.Praga).Where(r => r.Id == id).SingleOrDefaultAsync(r => r.Id == id);

            if (registroInfestacao == null)
            {
                return NotFound("Nenhum registro de infestação foi encontrado.");
            }

            return registroInfestacao;
        }

        [HttpGet("plantacao/{id}")]
        public async Task<ActionResult<IEnumerable<RegistroInfestacao>>> GetRegistroInfestacaoByPlantacao(string id)
        {
            if (_context.RegistroInfestacao == null)
            {
                return NotFound();
            }
            var registroInfestacao = await _context.RegistroInfestacao.Include(p => p.Plantacao).Include(i => i.Praga).Where(r => r.Plantacao.Id == id && r.Status == "Ativo").ToListAsync();

            if (registroInfestacao == null)
            {
                return NotFound("Não há registros de infestações para esta plantação.");
            }

            return registroInfestacao;
        }

        [HttpGet("pragas")]
        public async Task<ActionResult<IEnumerable<Praga>>> GetRegistroInfestacaoByPraga()
        {
            if (_context.Praga == null)
            {
                return NotFound();
            }
            var praga = await _context.Praga.ToListAsync();

            if (praga.Count == 0)
            {
                return NotFound("Não há registros de infestações para esta praga.");
            }

            return praga;
        }
        [HttpGet("pragas/{id}")]
        public async Task<ActionResult<IEnumerable<RegistroInfestacao>>> GetRegistroInfestacaoByPragaById(string id)
        {
            if (_context.RegistroInfestacao == null)
            {
                return NotFound();
            }
            var registroInfestacao = await _context.RegistroInfestacao.Include(p => p.Plantacao).Include(i => i.Praga).Where(r => r.Praga.PragaId == id && r.Status == "Ativo").ToListAsync();

            if (registroInfestacao == null)
            {
                return NotFound("Não há registros de infestações para esta praga.");
            }

            return registroInfestacao;
        }


        // POST: api/RegistrosInfestacoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RegistroInfestacao>> PostRegistroInfestacao(RegistroInfestacaoDTO registroInfestacaoDTO)
        {
            if (_context.RegistroInfestacao == null)
            {
                return Problem("Entity set 'FazendaAPIContext.RegistroInfestacao'  is null.");
            }

            var plantacao = await _context.Plantacao.FindAsync(registroInfestacaoDTO.PlantacaoId);
            var praga = await _context.Praga.FindAsync(registroInfestacaoDTO.PragaId);

            if (plantacao == null)
            {
                return NotFound("Plantação não encontrada.");
            }

            if (plantacao.Status != "Ativo")
            {
                return BadRequest("Plantação não está ativa.");
            }

            if (praga == null)
            {
                return NotFound("Praga não encontrada.");
            }

            var registroInfestacao = new RegistroInfestacao
            {
                Plantacao = plantacao,
                Praga = praga,
                DataRegistro = DateTime.ParseExact(registroInfestacaoDTO.DataRegistro, "dd/MM/yyyy", CultureInfo.GetCultureInfo("pt-BR")),
                DataConclusaoTratamento = null,
                Cauterizado = "Em processo",
                Status = "Ativo"
            };

            _context.RegistroInfestacao.Add(registroInfestacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegistroInfestacaoAtivos", new { id = registroInfestacao.Id }, registroInfestacao);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> RegistroInfestacaoCauterizar(int id, RegistroInfestacaoPutDTO registroInfestacaoPut)
        {
            if (_context.RegistroInfestacao == null)
            {
                return Problem("Entity set 'FazendaAPIContext.RegistroInfestacao'  is null.");
            }

            var registroInfestacao = await _context.RegistroInfestacao.Include(p => p.Plantacao).Where(r => r.Id == id).SingleOrDefaultAsync(r => r.Id == id);

            var data = DateTime.ParseExact(registroInfestacaoPut.DataConclusaoTratamento, "dd/MM/yyyy", CultureInfo.GetCultureInfo("pt-BR"));

            if (registroInfestacao == null)
            {
                return NotFound("Registro de infestação não encontrado.");
            }

            if (registroInfestacao.Status != "Ativo")
            {
                return BadRequest("Registro de infestação não está ativo.");
            }

            if (registroInfestacaoPut.Cauterizado == "Sim")
            {
                registroInfestacao.Cauterizado = "Sim";
                registroInfestacao.Status = "Concluido";
                registroInfestacao.DataConclusaoTratamento = data;
            }
            else if (registroInfestacaoPut.Cauterizado == "Não")
            {
                var buscar = registroInfestacao.Plantacao.Id;
                var plantacao = await _context.Plantacao.FindAsync(buscar);
                await _plantacoesController.DeletePlantacao(plantacao.Id);

                registroInfestacao.Cauterizado = "Não";
                registroInfestacao.Status = "Perda";
                registroInfestacao.DataConclusaoTratamento = data;
            }
            else if (registroInfestacaoPut.Cauterizado == "Em processo")
            {
                registroInfestacao.Cauterizado = "Em processo";
                registroInfestacao.Status = "Ativo";
                registroInfestacao.DataConclusaoTratamento = null;
            }
            else
            {
                return BadRequest("Valor inválido para o campo 'Cauterizado'.");
            }

            if (data < registroInfestacao.DataRegistro)
            {
                return BadRequest("Data de conclusão do tratamento não pode ser anterior à data de registro.");
            }

            _context.Entry(registroInfestacao).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("O registro de infestação foi atualizado com sucesso!");
        }


        // DELETE: api/RegistrosInfestacoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistroInfestacao(int id)
        {
            if (_context.RegistroInfestacao == null)
            {
                return NotFound();
            }
            var registroInfestacao = await _context.RegistroInfestacao.FindAsync(id);

            if (registroInfestacao == null)
            {
                return NotFound("Registro de infestação não encontrado.");
            }

            _context.RegistroInfestacao.Remove(registroInfestacao);
            await _context.SaveChangesAsync();

            return Ok("O registro de infestação foi removido com sucesso!");
        }


    }
}
