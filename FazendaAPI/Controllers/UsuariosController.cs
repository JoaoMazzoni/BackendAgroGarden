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
    public class UsuariosController : ControllerBase
    {
        private readonly FazendaAPIContext _context;
        private readonly ServiceEndereco _serviceEndereco;

        public UsuariosController(FazendaAPIContext context, ServiceEndereco serviceEndereco)
        {
            _context = context;
            _serviceEndereco = serviceEndereco;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario()
        {
            if (_context.Usuario == null)
                return NotFound();

            var usuarios = await _context.Usuario.Include(e => e.Endereco).ToListAsync();

            return usuarios;
        }

        // GET: api/Usuarios/5
        [HttpGet("{cpf}")]
        public async Task<ActionResult<Usuario>> GetUsuario(string cpf)
        {
            cpf = ValidarCPF.Desformatar(cpf);
            var usuario = await _context.Usuario.Include(e => e.Endereco).Where(u => u.CPF == cpf).FirstOrDefaultAsync();

            if (usuario == null)
                return NotFound("Usuario não encontrado");

            usuario.CPF = ValidarCPF.Formatar(usuario.CPF);
            return usuario;
        }

        [HttpGet("nome/{nomeCompleto}")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarioNome(string nomeCompleto)
        {
            var usuarios = await _context.Usuario.Include(e => e.Endereco).Where(u => u.NomeCompleto.Contains(nomeCompleto)).ToListAsync();

            if (usuarios.Count == 0)
                return NotFound("Nenhum usuário encontrado");

            return usuarios;
        }

        [HttpGet("inativos")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuariosInativos()
        {
            if (_context.Usuario == null)
                return NotFound();

            var usuarios = await _context.Usuario.Include(e => e.Endereco).Where(u => u.Ativo == false).ToListAsync();

            if (usuarios.Count == 0)
                return NotFound("Nenhum usuário inativo encontrado");

            return usuarios;
        }

        [HttpGet("ativos")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuariosAtivos()
        {
            if (_context.Usuario == null)
                return NotFound();

            var usuarios = await _context.Usuario.Include(e => e.Endereco).Where(u => u.Ativo == true).ToListAsync();

            if (usuarios.Count == 0)
                return NotFound("Nenhum usuário ativo encontrado");

            return usuarios;
        }

        [HttpGet("Administrador")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAdministradores()
        {
            if (_context.Usuario == null)
                return NotFound();

            var usuarios = await _context.Usuario.Include(e => e.Endereco).Where(u => u.Tipo == "Administrador").ToListAsync();

            if (usuarios.Count == 0)
                return NotFound("Nenhum administrador encontrado");

            return usuarios;
        }

        [HttpGet("Agricultor")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAgricultores()
        {
            if (_context.Usuario == null)
                return NotFound();

            var usuarios = await _context.Usuario.Include(e => e.Endereco).Where(u => u.Tipo == "Agricultor").ToListAsync();

            if (usuarios.Count == 0)
                return NotFound("Nenhum agricultor encontrado");

            return usuarios;
        }

        [HttpGet("Comercial")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetComerciais()
        {
            if (_context.Usuario == null)
                return NotFound();

            var usuarios = await _context.Usuario.Include(e => e.Endereco).Where(u => u.Tipo == "Comercial").ToListAsync();

            if (usuarios.Count == 0)
                return NotFound("Nenhum comercial encontrado");

            return usuarios;
        }

        [HttpGet("Gerente")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetGerentes()
        {
            if (_context.Usuario == null)
                return NotFound();

            var usuarios = await _context.Usuario.Include(e => e.Endereco).Where(u => u.Tipo == "Gerente").ToListAsync();

            if (usuarios.Count == 0)
                return NotFound("Nenhum gerente encontrado");

            return usuarios;
        }

        [HttpPost("auth")]
        public async Task<ActionResult> Authenticate([FromBody] Login login)
        {
            login.CPF = ValidarCPF.Desformatar(login.CPF);

            var usuario = await _context.Usuario
                                        .Where(u => u.CPF == login.CPF && u.Senha == login.Senha)
                                        .FirstOrDefaultAsync();

            if (usuario == null || !usuario.Senha.Equals(login.Senha, StringComparison.Ordinal) || usuario.Ativo == false)
                return Unauthorized("Acesso negado: CPF ou senha inválidos.");


            return Ok(usuario);
        }


        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{cpf}")]
        public async Task<IActionResult> PutUsuario(string cpf, UsuarioPutDTO usuario)
        {
            cpf = ValidarCPF.Desformatar(cpf);
            var usuarioPut = await GetUsuario(cpf);
            Usuario usuarioFinal = usuarioPut.Value;

            if (usuarioFinal == null) return NotFound("Usuário não encontrado.");

            usuarioFinal.CPF = cpf;

            if (usuarioPut.Value == null)
            {
                if (usuarioPut.Value.Ativo == false)
                    return BadRequest("Usuário inativo, para modifica-lo, mude seu status");

                return NotFound("Usuario não encontrado");
            }

            if (usuario.NomeCompleto != null && usuario.NomeCompleto != "" && usuario.NomeCompleto != usuarioFinal.NomeCompleto)
                usuarioFinal.NomeCompleto = usuario.NomeCompleto;

            if (usuario.DataNascimento != usuarioFinal.DataNascimento)
                usuarioFinal.DataNascimento = usuario.DataNascimento;

            if (usuario.Email != null && usuario.Email != "" && usuario.Email != usuarioFinal.Email)
            {
                if (new ValidarEmail().EmailValidar(usuario.Email) == false)
                    return BadRequest("Email inválido");

                usuarioFinal.Email = usuario.Email;
            }

            if (usuario.Telefone != null && usuario.Telefone == "" && usuario.Telefone != usuarioFinal.NomeCompleto)
            {
                usuario.Telefone = new ValidarTelefone().FormatarTelefone(usuario.Telefone);

                if (new ValidarTelefone().TelefoneValidar(usuario.Telefone))
                    return BadRequest("Telefone inválido");

                usuarioFinal.Telefone = usuario.Telefone;
            }

            if (usuario.Tipo != "Administrador" && usuario.Tipo != "Agricultor" && usuario.Tipo != "Comercial" && usuario.Tipo != "Gerente")
                return BadRequest("Tipo de usuário inválido");

            usuarioFinal.Tipo = usuario.Tipo;

            usuarioFinal.Ativo = usuario.Ativo;

            if (usuario.Endereco.CEP != null && usuario.Endereco.CEP != "" && usuario.Endereco.CEP != usuarioFinal.Endereco.CEP)
            {
                var enderecoExistente = await _context.Endereco
                    .FirstOrDefaultAsync(e => e.Id == usuario.Endereco.CEP + usuario.Endereco.Numero);

                if (enderecoExistente != null)
                {
                    usuarioFinal.Endereco = enderecoExistente;
                }
                else
                {
                    usuarioFinal.Endereco = await _serviceEndereco.GetEnderecoCorreio(usuario.Endereco);
                }

            }
            if (usuario.Endereco.Numero != null && usuario.Endereco.Numero != "" && usuario.Endereco.Numero != usuarioFinal.Endereco.Numero)
                usuarioFinal.Endereco.Numero = usuario.Endereco.Numero;

            if (usuario.Endereco.Complemento != null && usuario.Endereco.Complemento != "" && usuario.Endereco.Complemento != usuarioFinal.Endereco.Complemento)
                usuarioFinal.Endereco.Complemento = usuario.Endereco.Complemento;

            if (usuario.Senha != null && usuario.Senha != "" && usuario.Senha != usuarioFinal.Senha)
                usuarioFinal.Senha = usuario.Senha;

            _context.Entry(usuarioFinal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest("Houve um erro, tente novamente mais tarde" + e);
            }

            return Ok(usuarioFinal);
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(UsuarioDTO usuario)
        {
            if (!ValidarCPF.Validar(usuario.CPF))
                return BadRequest("CPF inválido.");

            var getUsuario = await GetUsuario(usuario.CPF);

            if (getUsuario.Value != null)
            {
                if (getUsuario.Value.Ativo == false)
                    return BadRequest("CPF já cadastrado, porem inativo");

                return BadRequest("CPF já cadastrado");
            }

            if (usuario.CPF == null || usuario.CPF == "")
                return BadRequest("CPF inválido");

            usuario.Telefone = new ValidarTelefone().FormatarTelefone(usuario.Telefone);
            if (usuario.Telefone == null || usuario.Telefone == "" || new ValidarTelefone().TelefoneValidar(usuario.Telefone))
                return BadRequest("Telefone inválido");

            if (usuario.Endereco.CEP == null || usuario.Endereco.CEP == "")
                return BadRequest("CEP inválido");

            if (usuario.NomeCompleto == null || usuario.NomeCompleto == "")
                return BadRequest("Nome completo inválido");

            if (usuario.DataNascimento == null || usuario.DataNascimento == DateTime.MinValue)
                return BadRequest("Data de nascimento inválida");

            if (usuario.Email == null || usuario.Email == "" || new ValidarEmail().EmailValidar(usuario.Email) == false)
                return BadRequest("Email inválido");

            if (usuario.Tipo != "Administrador" && usuario.Tipo != "Agricultor" && usuario.Tipo != "Comercial" && usuario.Tipo != "Gerente")
                return BadRequest("Tipo de usuário inválido");

            if (usuario.Senha == null || usuario.Senha == "")
                return BadRequest("Senha inválida");

            Endereco endereco = await _serviceEndereco.GetEnderecoCorreio(usuario.Endereco);

            if (endereco == null)
                return BadRequest("Endereço inválido");

            Usuario usuarioFinal = new Usuario
            {
                CPF = ValidarCPF.Desformatar(usuario.CPF),
                NomeCompleto = usuario.NomeCompleto,
                DataNascimento = usuario.DataNascimento,
                Email = usuario.Email,
                Telefone = usuario.Telefone,
                Tipo = usuario.Tipo.ToString(),
                Endereco = endereco,
                Senha = usuario.Senha,
                Ativo = true
            };

            _context.Usuario.Add(usuarioFinal);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return BadRequest("Houve um erro, tente novamente mais tarde" + e);
            }

            usuarioFinal.CPF = ValidarCPF.Formatar(usuarioFinal.CPF);
            return Ok(usuarioFinal);
        }

        // DELETE: api/Usuarios/5
        [HttpPatch("/desativarOuAtivar/{cpf}")]
        public async Task<IActionResult> DesativarUsuario(string cpf)
        {
            var usuario = await _context.Usuario.FindAsync(cpf);
            if (usuario == null)
                return NotFound();

            usuario.Ativo = !usuario.Ativo;

            _context.Usuario.Update(usuario);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return BadRequest("Houve um erro, tente novamente mais tarde" + e);
            }

            if (usuario.Ativo)
                return Ok("Usuário ativado");
            return Ok("Usuário desativado");
        }
    }
}
