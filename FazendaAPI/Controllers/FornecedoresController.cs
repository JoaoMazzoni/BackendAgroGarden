
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FazendaAPI.Data;
using FazendaAPI.Utils;
using Models.DTO;
using System.Text.RegularExpressions;
using System.Text;

namespace FazendaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedoresController : ControllerBase
    {
        private readonly FazendaAPIContext _context;
        private readonly ServiceEndereco _serviceEndereco;
        private readonly ValidarCNPJ ValidarCNPJ;
        private readonly ValidarEmail _validarEmail;
        private readonly ValidarTelefone _validarTelefone;


        public FornecedoresController(FazendaAPIContext context, ServiceEndereco serviceEndereco, ValidarCNPJ validar, ValidarEmail validarEmail, ValidarTelefone validarTelefone)
        {
            _context = context;
            _serviceEndereco = serviceEndereco;
            ValidarCNPJ = validar;
            _validarEmail = validarEmail;
            _validarTelefone = validarTelefone;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fornecedor>>> GetFornecedor()
        {
            if (_context.Fornecedor == null)
            {
                return NotFound();
            }

            return await _context.Fornecedor.Include(e => e.Endereco).ToListAsync();
        }

        // GET: api/Fornecedores
        [HttpGet("Ativos")]
        public async Task<ActionResult<IEnumerable<Fornecedor>>> GetFornecedorAtivos()
        {

            if (_context.Fornecedor == null)
            {
                return NotFound("Não há fornecedores ativos no momento.");
            }

            var buscar = await _context.Fornecedor.Include(e => e.Endereco).Where(f => f.Status == "Ativo").ToListAsync();

            if (buscar.Count == 0)
            {
                return NotFound("Não há fornecedores ativos no momento.");
            }

            return buscar;

        }

        [HttpGet("Ativos/nome/{nome}")]
        public async Task<ActionResult<IEnumerable<Fornecedor>>> GetFornecedorAtivosPorNome(string nome)
        {
            if (_context.Fornecedor == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedor.Include(e => e.Endereco).Where(f => f.NomeDoFornecedor.Contains(nome) && f.Status == "Ativo").ToListAsync();

            if (fornecedor.Count == 0)
            {
                return NotFound("Não há fornecedores ativos com o nome informado.");
            }

            return fornecedor;
        }

        [HttpGet("Ativos/tipo/{tipo}")]
        public async Task<ActionResult<IEnumerable<Fornecedor>>> GetFornecedorAtivosPorTipo(string tipo)
        {
            if (_context.Fornecedor == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedor.Include(e => e.Endereco).Where(f => f.TipoDeFornecimento.Contains(tipo) && f.Status == "Ativo").ToListAsync();

            if (fornecedor.Count == 0)
            {
                return NotFound("Não há fornecedores ativos com o tipo de fornecimento informado.");
            }

            return fornecedor;
        }

        [HttpGet("Ativos/cnpj/{CNPJ}")] 
        public async Task<ActionResult<IEnumerable<Fornecedor>>> GetFornecedorAtivoPorCNPJ(string CNPJ)
        {
            CNPJ = CNPJ.Replace(".", "").Replace("-", "").Replace("/", "");

            CNPJ = ValidarCNPJ.FormatarCNPJ(CNPJ);

            if (_context.Fornecedor == null)
            {
                return NotFound();
            }
            var fornecedor = await _context.Fornecedor.Include(e => e.Endereco).Where(f => f.CNPJ == CNPJ && f.Status == "Ativo").ToListAsync();

            if (fornecedor.Count == 0)
            {
                return NotFound("O fornecedor informado não possui registro em nosso sistema.");
            }

            return fornecedor;
        }

        [HttpGet("Inativos")]
        public async Task<ActionResult<IEnumerable<Fornecedor>>> GetFornecedorInativos()
        {

            if (_context.Fornecedor == null)
            {
                return NotFound("Não há nenhum fornecedor cadastrado no momento.");
            }

            var buscar = await _context.Fornecedor.Include(e => e.Endereco).Where(f => f.Status == "Inativo").ToListAsync();

            if (buscar.Count == 0)
            { 
                return NotFound("Não há fornecedores inativos no momento.");
            }

            return buscar;

        }

        // GET: api/Fornecedores/5
        [HttpGet("{CNPJ}")]
        public async Task<ActionResult<Fornecedor>> GetFornecedor(string CNPJ)
        {

            CNPJ = CNPJ.Replace(".", "").Replace("-", "").Replace("/", "");

            CNPJ = ValidarCNPJ.FormatarCNPJ(CNPJ);

            if (_context.Fornecedor == null)
            {
                return NotFound();
            }
            var fornecedor = await _context.Fornecedor.Include(e => e.Endereco).Where(f => f.CNPJ == CNPJ).SingleOrDefaultAsync(f => f.CNPJ == CNPJ); 

            if (fornecedor == null)
            {
                return NotFound("O fornecedor informado não possui registro em nosso sistema.");
            }

            return fornecedor;
        }

        [HttpGet("nome/{nome}")]
        public async Task<ActionResult<IEnumerable<Fornecedor>>> GetFornecedorPorNome(string nome)
        {
            if (_context.Fornecedor == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedor.Include(e => e.Endereco).Where(f => f.NomeDoFornecedor.Contains(nome)).ToListAsync();

            if (fornecedor.Count == 0)
            {
                return NotFound("Não há fornecedores com o nome informado.");
            }

            return fornecedor;
        }

        // PUT: api/Fornecedores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{CNPJ}")]
        public async Task<IActionResult> PutFornecedor(string CNPJ, FornecedorPutDTO fornecedorPutDTO)
        {
            CNPJ = CNPJ.Replace(".", "").Replace("-", "").Replace("/", "");

            CNPJ =  ValidarCNPJ.FormatarCNPJ(CNPJ);

            if (!_validarEmail.EmailValidar(fornecedorPutDTO.Email))
            {
                return BadRequest("O formato de email informado é inválido.");
            }
            
            fornecedorPutDTO.Telefone = _validarTelefone.FormatarTelefone(fornecedorPutDTO.Telefone);

            if(!_validarTelefone.TelefoneValidar(fornecedorPutDTO.Telefone))
            {
                return BadRequest("O formato de telefone informado é inválido.");
            }

            var fornecedor1 = new Fornecedor();
            fornecedor1.CNPJ = CNPJ;
            fornecedor1.NomeDoFornecedor = fornecedorPutDTO.NomeDoFornecedor;
            fornecedor1.Telefone = fornecedorPutDTO.Telefone;
            fornecedor1.Email = fornecedorPutDTO.Email;
            fornecedor1.TipoDeFornecimento = fornecedorPutDTO.TipoDeFornecimento;
            fornecedor1.Status = fornecedorPutDTO.Status;


            var enderecoExistente = await _context.Endereco
       .FirstOrDefaultAsync(e => e.Id == fornecedorPutDTO.Endereco.CEP + fornecedorPutDTO.Endereco.Numero);

            if (enderecoExistente != null)
            {
                fornecedor1.Endereco = enderecoExistente;
            }
            else
            {
                fornecedor1.Endereco = await _serviceEndereco.GetEnderecoCorreio(fornecedorPutDTO.Endereco);
            }

            _context.Entry(fornecedor1).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FornecedorExists(CNPJ))
                {
                    return NotFound("Nenhum fornecedor foi encontrado.");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Fornecedor alterado com sucesso!");
        }

        // POST: api/Fornecedores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Fornecedor>> PostFornecedor(FornecedorDTO fornecedor)
        {

            var cnpj = fornecedor.CNPJ;
            cnpj = ValidarCNPJ.FormatarCNPJ(cnpj);

            if (_context.Fornecedor == null)
            {
                return Problem("Entity set 'FazendaAPIContext.Fornecedor'  is null.");
            }

           var fornecedorExiste = await _context.Fornecedor.Include(e => e.Endereco).Where(f => f.CNPJ == cnpj).SingleOrDefaultAsync(f => f.CNPJ == cnpj);

            if(fornecedorExiste != null)
            {
                return Conflict("O CNPJ informado já está cadastrado no sistema.");
            }

            Fornecedor fornecedor1 = new Fornecedor();
            fornecedor1.CNPJ = fornecedor.CNPJ;
            fornecedor1.NomeDoFornecedor = fornecedor.NomeDoFornecedor;
            fornecedor1.Telefone = fornecedor.Telefone;
            fornecedor1.Email = fornecedor.Email;
            fornecedor1.TipoDeFornecimento = fornecedor.TipoDeFornecimento;
            fornecedor1.Status = "Ativo";


            if (!_validarEmail.EmailValidar(fornecedor1.Email))
            {
                return BadRequest("O formato de email informado é inválido.");
            }


            fornecedor1.CNPJ = ValidarCNPJ.FormatarCNPJ(fornecedor1.CNPJ);


            fornecedor1.Telefone = _validarTelefone.FormatarTelefone(fornecedor1.Telefone);

            if(!_validarTelefone.TelefoneValidar(fornecedor1.Telefone))
            {
                return BadRequest("O formato de telefone informado é inválido.");
            }


            var enderecoExistente = await _context.Endereco
            .FirstOrDefaultAsync(e => e.Id == fornecedor.Endereco.CEP + fornecedor.Endereco.Numero);

            if (enderecoExistente != null)
            {
                fornecedor1.Endereco = enderecoExistente;
            }
            else
            {
                fornecedor1.Endereco = await _serviceEndereco.GetEnderecoCorreio(fornecedor.Endereco);
            }

            var existe = await _context.Fornecedor.Include(e => e.Endereco).Where(f => f.CNPJ == fornecedor1.CNPJ && f.Status == "Inativo")
                .SingleOrDefaultAsync(f => f.CNPJ == fornecedor1.CNPJ && f.Status == "Inativo");

            if (existe != null && existe.Status == "Inativo")
            {
                existe.Status = "Ativo";
                _context.Entry(existe).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("O fornecedor estava inativo e teve seu status alterado para ativo novamente.");
            }

            _context.Fornecedor.Add(fornecedor1);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FornecedorExists(fornecedor.CNPJ))
                {
                    return Conflict("O fornecedor informado já está cadastrado no sistema.");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Fornecedor cadastrado com sucesso!");
        }

        // DELETE: api/Fornecedores/5
        [HttpDelete("{CNPJ}")]
        public async Task<IActionResult> DeleteFornecedor(string CNPJ)
        {
            CNPJ = CNPJ.Replace(".", "").Replace("-", "").Replace("/", "");

            CNPJ = ValidarCNPJ.FormatarCNPJ(CNPJ);

            if (_context.Fornecedor == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedor.FindAsync(CNPJ);
            if (fornecedor == null)
            {
                return NotFound("Fornecedor não encontrado.");
            }

            if(fornecedor.Status == "Inativo")
            {
                return Conflict("O fornecedor informado já está inativo.");
            }

            fornecedor.Status = "Inativo";
            await _context.SaveChangesAsync();

            return Ok("Fornecedor desativado com sucesso!");
        }

        private bool FornecedorExists(string id)
        {
            return (_context.Fornecedor?.Any(e => e.CNPJ == id)).GetValueOrDefault();
        }

        
    }
}
