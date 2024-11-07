using FazendaAPI.Data;
using FazendaAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;

namespace FazendaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly FazendaAPIContext _context;
        private readonly ValidarCNPJ _validarCNPJ;
        private readonly ValidarEmail _validarEmail;
        private readonly ValidarTelefone _validarTelefone;
        private readonly ServiceEndereco _serviceEndereco;

        public ClientesController(FazendaAPIContext context, ValidarCNPJ validarCNPJ, ValidarEmail validarEmail, ValidarTelefone validarTelefone, ServiceEndereco serviceEndereco)
        {
            _context = context;
            _validarCNPJ = validarCNPJ;
            _validarEmail = validarEmail;
            _validarTelefone = validarTelefone;
            _serviceEndereco = serviceEndereco;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetCliente()
        {
            if (_context.Cliente == null)
            {
                return NotFound();
            }

            var clientes = await _context.Cliente.Include(e => e.Endereco).ToListAsync();

            if (clientes.Count == 0)
            {
                return NotFound("Não há clientes cadastrados.");
            }

            return clientes;
        }

        // GET: api/Clientes/5
        [HttpGet("{CNPJ}")]
        public async Task<ActionResult<Cliente>> GetCliente(string CNPJ)
        {
            if (_context.Cliente == null)
            {
                return NotFound();
            }
            var cliente = await _context.Cliente.Include(e => e.Endereco).ToListAsync();

            if (cliente == null)
            {
                return NotFound("Nenhum cliente encontrado.");
            }

            return cliente.Where(c => c.CNPJ.Replace(".", "").Replace("-", "").Replace("/", "") == CNPJ).SingleOrDefault();
        }

        [HttpGet("Ativos")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientesAtivos()
        {
            if (_context.Cliente == null)
            {
                return NotFound();
            }
            var clientes = await _context.Cliente.Include(e => e.Endereco).Where(c => c.Status == "Ativo").ToListAsync();

            if (clientes.Count == 0)
            {
                return NotFound("Nenhum cliente ativo encontrado.");
            }

            return clientes;
        }

        [HttpGet("Inativos")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientesInativos()
        {
            if (_context.Cliente == null)
            {
                return NotFound();
            }
            var clientes = await _context.Cliente.Include(e => e.Endereco).Where(c => c.Status == "Inativo").ToListAsync();

            if (clientes.Count == 0)
            {
                return NotFound("Nenhum cliente inativo encontrado.");
            }

            return clientes;
        }


        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{CNPJ}")]
        public async Task<IActionResult> PutCliente(string CNPJ, ClientePutDTO clienteDTO)
        {

            CNPJ = CNPJ.Replace(".", "").Replace("-", "").Replace("/", "");

            CNPJ = ValidarCNPJ.FormatarCNPJ(CNPJ);

            if (!_validarEmail.EmailValidar(clienteDTO.Email))
            {
                return BadRequest("O formato de email informado é inválido.");
            }

            clienteDTO.Telefone = _validarTelefone.FormatarTelefone(clienteDTO.Telefone);

            if (!_validarTelefone.TelefoneValidar(clienteDTO.Telefone))
            {
                return BadRequest("O formato de telefone informado é inválido.");
            }

            var cliente = await _context.Cliente.FindAsync(CNPJ);

            cliente.RazaoSocial = clienteDTO.RazaoSocial;
            cliente.Telefone = clienteDTO.Telefone;
            cliente.Email = clienteDTO.Email;
            cliente.Status = clienteDTO.Status;

            var enderecoExistente = await _context.Endereco
           .FirstOrDefaultAsync(e => e.Id == clienteDTO.Endereco.CEP + clienteDTO.Endereco.Numero);

            if (enderecoExistente != null)
            {
                cliente.Endereco = enderecoExistente;
            }
            else
            {
                cliente.Endereco = await _serviceEndereco.GetEnderecoCorreio(clienteDTO.Endereco);
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(CNPJ))
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

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(ClienteDTO clienteDTO)
        {
            if (_context.Cliente == null)
            {
                return Problem("Entity set 'FazendaAPIContext.Cliente'  is null.");
            }

            var cliente = new Cliente
            {
                CNPJ = clienteDTO.CNPJ,
                RazaoSocial = clienteDTO.RazaoSocial,
                Telefone = _validarTelefone.FormatarTelefone(clienteDTO.Telefone),
                Email = clienteDTO.Email,
                Endereco =
                    new Endereco()
                    {
                        CEP = clienteDTO.Endereco.CEP,
                        Numero = clienteDTO.Endereco.Numero,
                        Complemento = clienteDTO.Endereco.Complemento
                    },
                Status = "Ativo"
            };

            if (!_validarEmail.EmailValidar(cliente.Email))
            {
                return BadRequest("O formato de email informado é inválido.");
            }

            cliente.CNPJ = ValidarCNPJ.FormatarCNPJ(cliente.CNPJ);


            cliente.Telefone = _validarTelefone.FormatarTelefone(cliente.Telefone);

            if (!_validarTelefone.TelefoneValidar(cliente.Telefone))
            {
                return BadRequest("O formato de telefone informado é inválido.");
            }


            var enderecoExistente = await _context.Endereco
            .FirstOrDefaultAsync(e => e.Id == cliente.Endereco.CEP + cliente.Endereco.Numero);

            if (enderecoExistente != null)
            {
                cliente.Endereco = enderecoExistente;
            }
            else
            {
                cliente.Endereco = await _serviceEndereco.GetEnderecoCorreio(clienteDTO.Endereco);
            }

            var existe = await _context.Cliente.Include(e => e.Endereco).Where(c => c.CNPJ == cliente.CNPJ && c.Status == "Inativo")
                .SingleOrDefaultAsync(c => c.CNPJ == cliente.CNPJ && c.Status == "Inativo");

            if (existe != null && existe.Status == "Inativo")
            {
                existe.Status = "Ativo";
                _context.Entry(existe).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("O cliente estava inativo e teve seu status alterado para ativo novamente");
            }

            _context.Cliente.Add(cliente);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClienteExists(clienteDTO.CNPJ))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCliente", new { id = clienteDTO.CNPJ }, clienteDTO);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{CNPJ}")]
        public async Task<IActionResult> DeleteCliente(string CNPJ)
        {
            CNPJ = ValidarCNPJ.FormatarCNPJ(CNPJ);

            if (_context.Cliente == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(CNPJ);
            if (cliente == null)
            {
                return NotFound("Nenhum cliente encontrado.");
            }

            if (cliente.Status == "Inativo")
            {
                return BadRequest("O cliente já está inativo.");
            }

            cliente.Status = "Inativo";
            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("Cliente inativado com sucesso!");
        }

        private bool ClienteExists(string id)
        {
            return (_context.Cliente?.Any(e => e.CNPJ == id)).GetValueOrDefault();
        }
    }
}
