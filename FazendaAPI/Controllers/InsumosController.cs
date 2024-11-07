
using FazendaAPI.Data;
using FazendaAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using System.Globalization;

namespace FazendaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsumosController : ControllerBase
    {
        private readonly FazendaAPIContext _context;
        private readonly ValidarCNPJ _validarCNPJ;

        public InsumosController(FazendaAPIContext context, ValidarCNPJ validarCNPJ)
        {
            _context = context;
            _validarCNPJ = validarCNPJ;
        }

        // GET: api/Agrotoxicos
        [HttpGet("Ativos")]
        public async Task<ActionResult<IEnumerable<Insumo>>> GetInsumoAtivo()
        {
            if (_context.Insumo == null)
            {
                return NotFound("Não há nenhum insumo ativo no momento.");
            }

            var busca = await _context.Insumo.Include(f => f.Fornecedor).Where(a => a.Status == "Ativo").ToListAsync();

            if (busca.Count == 0)
            {
                return NotFound("Não há nenhum insumo ativo no momento.");
            }

            return busca;
        }

        [HttpGet("Inativos")]
        public async Task<ActionResult<IEnumerable<Insumo>>> GetInsumoInativo()
        {
            if (_context.Insumo == null)
            {
                return NotFound("Não há nenhum insumo inativo no momento.");
            }

            var busca = await _context.Insumo.Include(f => f.Fornecedor).Where(a => a.Status == "Inativo").ToListAsync();

            if (busca.Count == 0)
            {
                return NotFound("Não há nenhum insumo inativo no momento.");
            }

            return busca;
        }



        // GET: api/Agrotoxicos/5
        [HttpGet("{codigoLote}")]
        public async Task<ActionResult<Insumo>> GetInsumo(string codigoLote)
        {
            if (_context.Insumo == null)
            {
                return NotFound();
            }
            var insumo = await _context.Insumo.Include(f => f.Fornecedor).Where(a => a.CodigoLote == codigoLote).SingleOrDefaultAsync(a => a.CodigoLote == codigoLote);

            if (insumo == null)
            {
                return NotFound("Insumo não encontrado.");
            }

            return insumo;
        }

        // PUT: api/Agrotoxicos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{codigoLote}")]
        public async Task<ActionResult<Insumo>> PutInsumo(string codigoLote, InsumoPutDTO insumo)
        {
            codigoLote = codigoLote.ToUpper();

            var insumoAtual = await _context.Insumo.FindAsync(codigoLote);

            if (insumoAtual == null)
            {
                return NotFound("O insumo informado não está cadastrado no sistema.");
            }

            insumoAtual.NomeDoInsumo = insumo.NomeDoInsumo;
            insumoAtual.Funcao = insumo.Funcao;
            insumoAtual.Status = insumo.Status;
            insumoAtual.MililitrosAtual = insumo.MililitrosAtual;
            insumoAtual.DataValidade = DateTime.ParseExact(insumo.DataValidade, "dd/MM/yyyy", CultureInfo.GetCultureInfo("pt-BR"));

            if (insumoAtual.DataValidade <= insumoAtual.DataEntrada)
            {
                return BadRequest("A data de validade do insumo não pode ser anterior ou igual à data de entrada.");
            }

            if (insumoAtual.MililitrosAtual > insumoAtual.QuantidadeEntrada)
            {
                return BadRequest("A quantidade atual do insumo não pode ser maior que a quantidade de entrada.");
            }

            if (insumoAtual.MililitrosAtual < 0)
            {
                return BadRequest("A quantidade atual do insumo não pode ser menor que zero.");
            }

            _context.Entry(insumoAtual).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InsumoExists(codigoLote))
                {
                    return NotFound("O insumo informado não está cadastrado no sistema.");
                }
                else
                {
                    throw;
                }
            }

            return await _context.Insumo.Include(f => f.Fornecedor).Include(e => e.Fornecedor.Endereco).Where(a => a.CodigoLote == insumoAtual.CodigoLote).SingleOrDefaultAsync(a => a.CodigoLote == insumoAtual.CodigoLote);
        }

        // POST: api/Agrotoxicos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Insumo>> PostInsumo(Models.DTO.InsumoDTO insumo)
        {
            if (_context.Insumo == null)
            {
                return Problem("Entity set 'FazendaAPIContext.Insumo'  is null.");
            }


            Insumo resultadoInsumo = new Insumo();
            resultadoInsumo.CodigoLote = Guid.NewGuid().ToString().Substring(0, 8).ToUpper().Replace("-", "");
            resultadoInsumo.NomeDoInsumo = insumo.NomeDoInsumo;
            resultadoInsumo.Funcao = insumo.Funcao;
            resultadoInsumo.Status = "Ativo";
            resultadoInsumo.QuantidadeEntrada = insumo.QuantidadeEntrada;
            resultadoInsumo.MililitrosAtual = insumo.QuantidadeEntrada;
            resultadoInsumo.DataValidade = DateTime.ParseExact(insumo.DataValidade, "dd/MM/yyyy", CultureInfo.GetCultureInfo("pt-BR"));
            resultadoInsumo.DataEntrada = DateTime.Now;

            insumo.FornecedorCNPJ = ValidarCNPJ.FormatarCNPJ(insumo.FornecedorCNPJ.ToString());

            resultadoInsumo.Fornecedor = await _context.Fornecedor.FindAsync(insumo.FornecedorCNPJ);

            if (resultadoInsumo.QuantidadeEntrada <= 0)
            {
                return BadRequest("A quantidade de insumo informada deve ser maior que zero.");
            }

            if (resultadoInsumo.Fornecedor == null)
            {
                return NotFound("O fornecedor informado não está cadastrado no sistema.");
            }
            if (resultadoInsumo.Fornecedor.Status == "Inativo")
            {
                return Conflict("O fornecedor informado está inativo.");
            }

            if (resultadoInsumo.DataValidade <= resultadoInsumo.DataEntrada)
            {
                return BadRequest("A data de validade do insumo não pode ser anterior ou igual à data de entrada. Considere a data de entrada como a data atual");
            }

            var auxNome = insumo.NomeDoInsumo;
            auxNome = auxNome.ToLower().Replace(" ", "").Replace("-", "");
            var buscarInsumo = await _context.Insumo.Where(a => a.NomeDoInsumo == auxNome).ToListAsync();

            foreach (var insumoCadastrado in buscarInsumo)
            {

                if (insumoCadastrado.NomeDoInsumo.ToLower().Replace(" ", "").Replace("-", "") == auxNome && insumoCadastrado.Fornecedor == resultadoInsumo.Fornecedor && insumoCadastrado.Funcao == resultadoInsumo.Funcao && insumoCadastrado.DataEntrada == resultadoInsumo.DataEntrada && insumoCadastrado.DataValidade == resultadoInsumo.DataValidade)
                {
                    return BadRequest("Um insumo com as mesmas informações já está cadastrado. Não é possível cadastrar insumos com informações duplicadas.");
                }

            }

            _context.Insumo.Add(resultadoInsumo);
            await _context.SaveChangesAsync();

            return await _context.Insumo.Include(f => f.Fornecedor).Include(e => e.Fornecedor.Endereco).Where(a => a.CodigoLote == resultadoInsumo.CodigoLote).SingleOrDefaultAsync(a => a.CodigoLote == resultadoInsumo.CodigoLote);
        }

        [HttpPatch("AlterarQuantidade/{codigoLote}")]
        public async Task<ActionResult> AlterarQuantidade(string codigoLote, int quantidade, bool operacao, AplicacaoInsumo aplicacaoInsumo)
        {
            codigoLote = codigoLote.ToUpper();

            if (_context.Insumo == null)
            {
                return NotFound();
            }

            var insumo = await _context.Insumo.FindAsync(codigoLote);

            if (insumo == null)
            {
                return NotFound("Insumo não encontrado.");
            }

            if (operacao == false && aplicacaoInsumo == null)
            {
                if (insumo.MililitrosAtual == 0)
                {
                    return BadRequest("O insumo informado está esgotado.");
                }

                if (insumo.Status == "Inativo")
                {
                    return BadRequest("O insumo informado está inativo.");
                }

                if (insumo.MililitrosAtual < quantidade)
                {
                    return BadRequest("A quantidade informada é maior que a quantidade atual do insumo.");
                }

                insumo.MililitrosAtual = insumo.MililitrosAtual - quantidade;
                _context.Entry(insumo).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Quantidade do insumo alterada com sucesso!");
            }

            if (operacao == true)
            {
                if (insumo.MililitrosAtual == 0 && insumo.Status == "Inativo" && insumo.DataValidade > aplicacaoInsumo.DataAplicacao)
                {
                    insumo.Status = "Ativo";
                    insumo.MililitrosAtual = insumo.MililitrosAtual + quantidade;
                    _context.Entry(insumo).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok("Quantidade do insumo alterada com sucesso!");
                }

                if (insumo.MililitrosAtual == 0 && insumo.Status == "Inativo" && insumo.DataValidade < aplicacaoInsumo.DataAplicacao)
                {
                    return BadRequest("O insumo informado está inativo e vencido. Não é possível aplicar insumos vencidos.");
                }

                insumo.MililitrosAtual = insumo.MililitrosAtual + quantidade;
                _context.Entry(insumo).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Quantidade do insumo alterada com sucesso!");
            }


            return Ok("Quantidade do insumo alterada com sucesso!");
        }

        [HttpPatch("ReativarInsumo/{codigoLote}")]
        public async Task<ActionResult> ReativarInsumo(string codigoLote)
        {
            codigoLote = codigoLote.ToUpper();

            if (_context.Insumo == null)
            {
                return NotFound();
            }
            var insumo = await _context.Insumo.FindAsync(codigoLote);

            if (insumo == null)
            {
                return NotFound("Insumo não encontrado.");
            }

            if (insumo.Status == "Ativo")
            {
                return Conflict("O insumo informado já está ativo.");
            }

            if (insumo.MililitrosAtual == 0)
            {
                return BadRequest("O insumo informado está esgotado.");
            }

            if (insumo.DataValidade < DateTime.Now)
            {
                return BadRequest("O insumo informado está vencido. Não é possível reativar insumos vencidos.");
            }

            insumo.Status = "Ativo";
            await _context.SaveChangesAsync();

            return Ok("Insumo reativado com sucesso!");
        }

        // DELETE: api/Agrotoxicos/5
        [HttpDelete("{codigoLote}")]
        public async Task<IActionResult> DeleteInsumo(string codigoLote)
        {
            codigoLote = codigoLote.ToUpper();

            if (_context.Insumo == null)
            {
                return NotFound();
            }
            var insumo = await _context.Insumo.FindAsync(codigoLote);

            if (insumo == null)
            {
                return NotFound("Insumo não encontrado.");
            }

            if (insumo.Status == "Inativo")
            {
                return Conflict("O insumo informado já está inativo.");
            }

            insumo.Status = "Inativo";
            await _context.SaveChangesAsync();

            return Ok("Insumo inativado com sucesso!");
        }

        private bool InsumoExists(string codigoLote)
        {
            return (_context.Insumo?.Any(e => e.CodigoLote == codigoLote)).GetValueOrDefault();
        }
    }
}
