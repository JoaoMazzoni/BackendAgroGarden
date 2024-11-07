
using FazendaAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Models.DTO;
using System.Globalization;

namespace FazendaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantacoesController : ControllerBase
    {
        private readonly FazendaAPIContext _context;

        public PlantacoesController(FazendaAPIContext context)
        {
            _context = context;
        }

        // GET: api/Plantacoes
        [HttpGet("Ativas")]
        public async Task<ActionResult<IEnumerable<Plantacao>>> GetPlantacaoAtivas()
        {
            if (_context.Plantacao == null)
            {
                return NotFound();
            }

            var buscar = await _context.Plantacao.Where(p => p.Status == "Ativo").ToListAsync();

            if (buscar.Count == 0)
            {
                return NotFound("Não há plantações ativas no momento.");
            }

            return buscar;
        }

        [HttpGet("Inativas")]
        public async Task<ActionResult<IEnumerable<Plantacao>>> GetPlantacaoInativas()
        {
            if (_context.Plantacao == null)
            {
                return NotFound();
            }

            var buscar = await _context.Plantacao.Where(p => p.Status == "Inativo").ToListAsync();

            if (buscar.Count == 0)
            {
                return NotFound("Não há plantações inativas no momento.");
            }

            return buscar;
        }

        [HttpGet("Colhidas")]
        public async Task<ActionResult<IEnumerable<Plantacao>>> GetPlantacaoColhidas()
        {
            if (_context.Plantacao == null)
            {
                return NotFound();
            }

            var buscar = await _context.Plantacao.Where(p => p.Colheita == "Colhida").ToListAsync();

            if (buscar.Count == 0)
            {
                return NotFound("Não há plantações colhidas no momento.");
            }

            return buscar;
        }

        [HttpGet("Cultivando")]
        public async Task<ActionResult<IEnumerable<Plantacao>>> GetPlantacaoCultivando()
        {
            if (_context.Plantacao == null)
            {
                return NotFound();
            }

            var buscar = await _context.Plantacao.Where(p => p.Colheita == "Cultivando").ToListAsync();

            if (buscar.Count == 0)
            {
                return NotFound("Não há plantações sendo cultivadas no momento.");
            }

            return buscar;
        }

        [HttpGet("Perdas")]
        public async Task<ActionResult<IEnumerable<Plantacao>>> GetPlantacaoPerdas()
        {
            if (_context.Plantacao == null)
            {
                return NotFound();
            }

            var buscar = await _context.Plantacao.Where(p => p.Colheita == "Perda").ToListAsync();

            if (buscar.Count == 0)
            {
                return NotFound("Não há plantações perdidas no momento.");
            }

            return buscar;
        }

        [HttpGet("Cultivadas")]
        public async Task<ActionResult<IEnumerable<Plantacao>>> GetPlantacaoCultivadas()
        {
            if (_context.Plantacao == null)
            {
                return NotFound();
            }

            var buscar = await _context.Plantacao.Where(p => p.Status == "Cultivado").ToListAsync();

            if (buscar.Count == 0)
            {
                return NotFound("Não há nenhuma plantação cultivada no momento.");
            }

            return buscar;
        }

        [HttpGet("nome")]
        public async Task<ActionResult<IEnumerable<Plantacao>>> GetPlantacaoByName([FromQuery] string nome)
        {

            if (_context.Plantacao == null)
            {
                return NotFound();
            }

            var buscar = await _context.Plantacao.Where(p => p.Nome.ToLower() == nome.ToLower()).ToListAsync();

            if (buscar.Count == 0)
            {
                return NotFound("Nenhuma plantação encontrada com o nome informado.");
            }

            return buscar;
        }

        [HttpGet("Ativas/Nome/{nome}")]
        public async Task<ActionResult<IEnumerable<Plantacao>>> GetPlantacaoAtivasByName([FromRoute] string nome)
        {
            if (_context.Plantacao == null)
            {
                return NotFound();
            }

            var buscar = await _context.Plantacao.Where(p => p.Nome.ToLower() == nome.ToLower() && p.Status == "Ativo").ToListAsync();

            if (buscar.Count == 0)
            {
                return NotFound("Nenhuma plantação ativa encontrada com o nome informado.");
            }

            return buscar;
        }

        [HttpGet("Ativas/Id/{id}")]
        public async Task<ActionResult<Plantacao>> GetPlantacaoAtivasById([FromRoute] string id)
        {
            id = id.ToUpper();

            if (_context.Plantacao == null)
            {
                return NotFound();
            }

            var buscar = await _context.Plantacao.Where(p => p.Id == id && p.Status == "Ativo").ToListAsync();

            if (buscar.Count == 0)
            {
                return NotFound("Nenhuma plantação ativa encontrada com o ID informado.");
            }

            return buscar[0];
        }

        [HttpGet("data")]
        public async Task<ActionResult<IEnumerable<Plantacao>>> GetPlantacaoByDate([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            if (_context.Plantacao == null || startDate == default || endDate == default)
            {
                return NotFound();
            }

            var buscar = await _context.Plantacao
                .Where(p => p.DataDePlantio >= startDate && p.DataDePlantio <= endDate)
                .ToListAsync();

            if (buscar.Count == 0)
            {
                return NotFound("Nenhuma plantação encontrada neste intervalo de datas.");
            }

            return buscar;
        }


        // GET: api/Plantacoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plantacao>> GetPlantacao(string id)
        {
            id = id.ToUpper();

            if (_context.Plantacao == null)
            {
                return NotFound();
            }
            var plantacao = await _context.Plantacao.FindAsync(id);

            if (plantacao == null)
            {
                return NotFound("Nenhuma plantação encontrada.");
            }

            return plantacao;
        }

        // PUT: api/Plantacoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Plantacao>> PutPlantacao(string id, PlantacaoDTO plantacao)
        {
            id = id.ToUpper();

            if (_context.Plantacao == null)
            {
                return NotFound();
            }

            var plantacaoAtual = await _context.Plantacao.FindAsync(id);

            if (plantacaoAtual == null)
            {
                return NotFound("Nenhuma plantação encontrada.");
            }

            if (plantacaoAtual.Status == "Inativo")
            {
                return Conflict("A plantação informada está inativa e portando suas informações não podem ser atualizadas.");
            }

            if (plantacaoAtual.Colheita == "Colhida")
            {
                return Conflict("A plantação informada já foi colhida e portanto suas informações não podem ser atualizadas.");
            }

            plantacaoAtual.Id = id;
            plantacaoAtual.Nome = plantacao.Nome;
            plantacaoAtual.DataDePlantio = DateTime.ParseExact(plantacao.DataDePlantio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            plantacaoAtual.Tipo = plantacao.Tipo;
            plantacaoAtual.LocalDePlantio = plantacao.LocalDePlantio;
            plantacaoAtual.Irrigacao = plantacao.Irrigacao;
            plantacaoAtual.LuzSolar = plantacao.LuzSolar;
            plantacaoAtual.CondicaoClimatica = plantacao.CondicaoClimatica;
            plantacaoAtual.Crescimento = plantacao.Crescimento;

            DateTime umaSemanaFrente = DateTime.Now.AddDays(7);

            if (plantacaoAtual.DataDePlantio > umaSemanaFrente)
            {
                return Conflict("A data de plantio não pode ser registrada em um período maior do que uma semana a partir da data atual. Por favor, escolha uma data dentro desse intervalo.");
            }

            _context.Entry(plantacaoAtual).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantacaoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return plantacaoAtual;
        }

        // POST: api/Plantacoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Plantacao>> PostPlantacao(PlantacaoDTO plantacao)
        {
            if (_context.Plantacao == null)
            {
                return Problem("Entity set 'FazendaAPIContext.Plantacao'  is null.");
            }

            var resultadoPlantacao = new Plantacao();

            resultadoPlantacao.Nome = plantacao.Nome;
            resultadoPlantacao.DataDePlantio = DateTime.ParseExact(plantacao.DataDePlantio, "dd/MM/yyyy", CultureInfo.GetCultureInfo("pt-BR"));
            resultadoPlantacao.Colheita = "Cultivando";
            resultadoPlantacao.Tipo = plantacao.Tipo;
            resultadoPlantacao.LocalDePlantio = plantacao.LocalDePlantio;
            resultadoPlantacao.Irrigacao = plantacao.Irrigacao;
            resultadoPlantacao.LuzSolar = plantacao.LuzSolar;
            resultadoPlantacao.CondicaoClimatica = plantacao.CondicaoClimatica;
            resultadoPlantacao.Crescimento = plantacao.Crescimento;
            resultadoPlantacao.Status = "Ativo";
            resultadoPlantacao.Id = Guid.NewGuid().ToString().Substring(0, 5).ToUpper();

            DateTime umaSemanaAtras = DateTime.Now.AddDays(-7);
            DateTime umaSemanaFrente = DateTime.Now.AddDays(7);

            if (resultadoPlantacao.DataDePlantio < umaSemanaAtras || resultadoPlantacao.DataDePlantio > umaSemanaFrente)
            {
                return Conflict("A data de plantio deve estar dentro de um período de uma semana a partir da data atual. Por favor, escolha uma data dentro desse intervalo.");
            }

            _context.Plantacao.Add(resultadoPlantacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlantacao", new { id = resultadoPlantacao.Id }, resultadoPlantacao);
        }

        // DELETE: api/Plantacoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlantacao(string id)
        {
            if (_context.Plantacao == null)
            {
                return NotFound();
            }

            var plantacao = await _context.Plantacao.FindAsync(id);

            if (plantacao == null)
            {
                return NotFound("Nenhuma plantação encontrada.");
            }

            if (plantacao.Status == "Inativo")
            {
                return Conflict("A plantação informada já está inativa.");
            }

            if (plantacao.Colheita == "Colhida")
            {
                return Conflict("A plantação informada já foi colhida, portanto, não pode ser deletada.");
            }

            plantacao.Status = "Inativo";
            plantacao.Colheita = "Perda";

            _context.Entry(plantacao).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok("A plantação foi inativada e atualizada como \"Perda\".");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchColheita(string id)
        {
            id = id.ToUpper();

            if (_context.Plantacao == null)
            {
                return NotFound();
            }
            var plantacao = await _context.Plantacao.FindAsync(id);

            if (plantacao == null)
            {
                return NotFound("Nenhuma plantação foi encontrada para colheita.");
            }
            if (plantacao.Colheita == "Colhida")
            {
                return Conflict("A plantação informada já foi colhida.");
            }

            plantacao.Colheita = "Colhida";
            plantacao.Status = "Cultivado";

            _context.Entry(plantacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantacaoExists(plantacao.Id))
                {
                    return NotFound("A plantação informada não existe.");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Registro de colheita realizado com sucesso!");
        }

        private bool PlantacaoExists(string id)
        {
            return (_context.Plantacao?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
