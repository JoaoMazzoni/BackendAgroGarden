
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FazendaAPI.Data;
using Models;
using System.Globalization;

namespace FazendaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AplicacaoInsumosController : ControllerBase
    {
        private readonly FazendaAPIContext _context;
        private readonly InsumosController _insumosController;

        public AplicacaoInsumosController(FazendaAPIContext context, InsumosController insumosController)
        {
            _context = context;
            _insumosController = insumosController;
        }

        // GET: api/AplicacaoInsumos
        [HttpGet("aplicado")]
        public async Task<ActionResult<IEnumerable<AplicacaoInsumo>>> GetAplicacaoInsumoAplicado()
        {
            if (_context.AplicacaoInsumo == null)
            {
                return NotFound();
            }

            var busca = await _context.AplicacaoInsumo.Include(i => i.Insumo).Include(p => p.Plantacao).Include(f => f.Insumo.Fornecedor).Where(a => a.Status == "Aplicado").ToListAsync();

            if (busca.Count == 0)
            {
                return NotFound("Nenhuma aplicação de insumo aplicada foi encontrada.");
            }

            return busca;
        }

        [HttpGet("cancelado")]
        public async Task<ActionResult<IEnumerable<AplicacaoInsumo>>> GetAplicacaoInsumoCancelado()
        {
            if (_context.AplicacaoInsumo == null)
            {
                return NotFound();
            }

            var busca = await _context.AplicacaoInsumo.Include(i => i.Insumo).Include(p => p.Plantacao).Include(f => f.Insumo.Fornecedor).Where(a => a.Status == "Cancelado").ToListAsync();

            if (busca.Count == 0)
            {
                return NotFound("Nenhuma aplicação de insumo cancelada foi encontrada.");
            }

            return busca;
        }


        // GET: api/AplicacaoInsumos/5
        [HttpGet("{registro}")]
        public async Task<ActionResult<AplicacaoInsumo>> GetAplicacaoInsumo(int registro)
        {
            if (_context.AplicacaoInsumo == null)
            {
                return NotFound();
            }
            var aplicacaoInsumo = await _context.AplicacaoInsumo.Include(i => i.Insumo).Include(p => p.Plantacao).Include(f => f.Insumo.Fornecedor).Where(a => a.Registro == registro).SingleOrDefaultAsync(a => a.Registro == registro);

            if (aplicacaoInsumo == null)
            {
                return NotFound("Nenhuma aplicação com este número de registro foi encontrada.");
            }

            return aplicacaoInsumo;
        }

        [HttpGet("plantacao/{id}")]
        public async Task<ActionResult<IEnumerable<AplicacaoInsumo>>> GetAplicacaoInsumoByPlantacao(string id)
        {
            if (_context.AplicacaoInsumo == null)
            {
                return NotFound();
            }
            var aplicacaoInsumo = await _context.AplicacaoInsumo.Include(i => i.Insumo).Include(p => p.Plantacao).Include(f => f.Insumo.Fornecedor).Where(a => a.Plantacao.Id == id).ToListAsync();

            if (aplicacaoInsumo.Count == 0)
            {
                return NotFound("Nenhuma aplicação foi encontrada para esta plantação.");
            }

            return aplicacaoInsumo;
        }

        [HttpGet("insumo/{id}")]
        public async Task<ActionResult<IEnumerable<AplicacaoInsumo>>> GetAplicacaoInsumoByInsumo(string id)
        {
            if (_context.AplicacaoInsumo == null)
            {
                return NotFound();
            }
            var aplicacaoInsumo = await _context.AplicacaoInsumo.Include(i => i.Insumo).Include(p => p.Plantacao).Include(f => f.Insumo.Fornecedor).Where(a => a.Insumo.CodigoLote == id).ToListAsync();

            if (aplicacaoInsumo.Count == 0)
            {
                return NotFound("Nenhuma aplicação utilizando este insumo foi encontrada.");
            }

            return aplicacaoInsumo;
        }

        // POST: api/AplicacaoInsumos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AplicacaoInsumo>> PostAplicacaoInsumo(AplicacaoInsumoDTO aplicacaoInsumoDTO)
        {
            if (_context.AplicacaoInsumo == null)
            {
                return Problem("Entity set 'FazendaAPIContext.AplicacaoInsumo'  is null.");
            }

            var aplicacaoInsumo = new AplicacaoInsumo();
            aplicacaoInsumo.Plantacao = _context.Plantacao.Find(aplicacaoInsumoDTO.PlantacaoId);
            aplicacaoInsumo.Insumo = _context.Insumo.Find(aplicacaoInsumoDTO.LoteInsumo);
            aplicacaoInsumo.Tipo = aplicacaoInsumoDTO.Tipo;
            aplicacaoInsumo.Quantidade = aplicacaoInsumoDTO.Quantidade;
            aplicacaoInsumo.DataAplicacao = DateTime.ParseExact(aplicacaoInsumoDTO.DataAplicacao, "dd/MM/yyyy", CultureInfo.GetCultureInfo("pt-BR"));
            aplicacaoInsumo.Status = "Aplicado";

            if (aplicacaoInsumo.Plantacao == null)
            {
                return NotFound("Plantacao não encontrada.");
            }

            if(aplicacaoInsumo.Quantidade > aplicacaoInsumo.Insumo.MililitrosAtual)
            {
                return BadRequest("Quantidade de insumo insuficiente.");
            }

            if(aplicacaoInsumo.Quantidade <= 0)
            {
                return BadRequest("Quantidade de insumo inválida.");
            }

            if (aplicacaoInsumo.Insumo == null)
            {
                return NotFound("Insumo não encontrado.");
            }

            if (aplicacaoInsumo.Plantacao.Status == "Inativo")
            {
                return BadRequest("Não é possível aplicar insumos em plantações inativas.");
            }

            if (aplicacaoInsumo.Plantacao.Status == "Cultivado")
            {
                return BadRequest("Não é possível aplicar insumos em plantações que já foram cultivadas.");
            }

            if (aplicacaoInsumo.Insumo.Status == "Inativo")
            {
                return BadRequest("Não é possível aplicar insumos inativos.");
            }

            if (aplicacaoInsumo.Tipo == "Fertilizante" && aplicacaoInsumo.Insumo.Funcao != "Fertilizante")
            {
                return BadRequest("O tipo de insumo não corresponde ao tipo de aplicação.");
            }

            if (aplicacaoInsumo.Tipo == "Adubo" && aplicacaoInsumo.Insumo.Funcao != "Adubo")
            {
                return BadRequest("O tipo de insumo não corresponde ao tipo de aplicação.");
            }

            if (aplicacaoInsumo.Tipo == "Agrotóxico" && aplicacaoInsumo.Insumo.Funcao == "Adubo")
            {
                return BadRequest("O tipo de insumo não corresponde ao tipo de aplicação.");
            }

            if (aplicacaoInsumo.Tipo == "Agrotóxico" && aplicacaoInsumo.Insumo.Funcao == "Fertilizante")
            {
                return BadRequest("O tipo de insumo não corresponde ao tipo de aplicação.");
            }

            DateTime umaSemanaAtras = DateTime.Now.AddDays(-7);


            if (aplicacaoInsumo.DataAplicacao < umaSemanaAtras)
            {
                return BadRequest("A data de aplicação deve ser no máximo uma semana atrás da data atual.");
            }

            if(aplicacaoInsumo.DataAplicacao > DateTime.Now)
            {
                return BadRequest("A data de aplicação não pode ser no futuro.");
            }

            await AtualizarQuantidadeControllerInsumo(aplicacaoInsumo.Insumo.CodigoLote, aplicacaoInsumo.Quantidade,
                false, null);

            _context.AplicacaoInsumo.Add(aplicacaoInsumo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAplicacaoInsumoAplicado", new { id = aplicacaoInsumo.Registro }, aplicacaoInsumo);
        }

        // DELETE: api/AplicacaoInsumos/5
        [HttpDelete("{registro}")]
        public async Task<IActionResult> DeleteAplicacaoInsumo(int registro)
        {
            if (_context.AplicacaoInsumo == null)
            {
                return NotFound();
            }
            var aplicacaoInsumo = await _context.AplicacaoInsumo.Include(i => i.Insumo).Include(p => p.Plantacao).Where(a => a.Registro == registro).SingleOrDefaultAsync(a => a.Registro == registro);

            if (aplicacaoInsumo == null)
            {
                return NotFound();
            }

            if(aplicacaoInsumo.Status == "Cancelado")
            {
                return BadRequest("Esta aplicação de insumo já foi cancelada.");
            }

            var loteInsumo = aplicacaoInsumo.Insumo.CodigoLote;
            var quantidade = aplicacaoInsumo.Quantidade;

            await AtualizarQuantidadeControllerInsumo(loteInsumo, quantidade,
                               true, aplicacaoInsumo);

            aplicacaoInsumo.Status = "Cancelado";
            _context.Entry(aplicacaoInsumo).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("O registro de aplicação de insumo foi cancelado com sucesso! O estoque de insumos foi atualizado!");
        }

        private async Task AtualizarQuantidadeControllerInsumo(string lote, int quantidade, bool operacao, AplicacaoInsumo auxObjeto)
        {
            await _insumosController.AlterarQuantidade(lote, quantidade, operacao, auxObjeto);
        }

        private bool AplicacaoInsumoExists(int id)
        {
            return (_context.AplicacaoInsumo?.Any(e => e.Registro == id)).GetValueOrDefault();
        }
    }
}
