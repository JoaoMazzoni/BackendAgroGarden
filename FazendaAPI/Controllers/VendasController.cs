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
    public class VendasController : ControllerBase
    {
        private readonly FazendaAPIContext _context;

        public VendasController(FazendaAPIContext context)
        {
            _context = context;
        }

        // GET: api/Vendas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venda>>> GetVendas()
        {
            var vendas = GetAllVendas();

            if (vendas != null)
            {
                return Ok(vendas);
            }

            return NotFound("Nenhuma venda encontrada");

        }

        // GET: api/Vendas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Venda>> GetVenda(int id)
        {
            var venda = await GetOneVenda(id);

            if(venda == null)
            {
               venda = await GetVendaFalse(id);
            }

            if (venda == null)
                return NotFound("Venda não encontrada");

            return Ok(venda);
        }

        [HttpGet("cliente/{cnpj}")]
        public async Task<ActionResult<IEnumerable<Venda>>> GetVendaCliente(string cnpj)
        {
            cnpj = ValidarCNPJ.FormatarCNPJ(cnpj);
            var cliente = await _context.Cliente.Where(c => c.CNPJ == cnpj).FirstOrDefaultAsync();

            if (cliente == null)
                return NotFound("Cliente não encontrado");

            var vendas = await _context.Venda.Include(f => f.Funcionario).Include(f => f.Funcionario.Endereco).Include(c => c.Cliente).Include(c => c.Cliente.Endereco).Where(v => v.Cliente.CNPJ == cliente.CNPJ).ToListAsync();

            if (vendas.Count == 0)
                return NotFound("Nenhuma venda encontrada");

            var vendasList = new List<Venda>();
            foreach (var venda in vendas)
            {
                if (venda.Ativa)
                    vendasList.Add(await GetOneVenda(venda.Id));
            }

            return Ok(vendasList);
        }

        [HttpGet("funcionario/{cpf}")]
        public async Task<ActionResult<IEnumerable<Venda>>> GetVendaFuncionario(string cpf)
        {
            cpf = ValidarCPF.Desformatar(cpf);
            var funcionario = await _context.Usuario.Where(f => f.CPF == cpf).FirstOrDefaultAsync();

            if (funcionario == null)
                return NotFound("Funcionário não encontrado");

            var vendas = await _context.Venda.Include(f => f.Funcionario).Include(c => c.Cliente).Where(v => v.Funcionario.CPF == funcionario.CPF).ToListAsync();

            if (vendas.Count == 0)
                return NotFound("Nenhuma venda encontrada");

            var vendasList = new List<Venda>();
            foreach (var venda in vendas)
            {
                if (venda.Ativa)
                    vendasList.Add(await GetOneVenda(venda.Id));
            }

            return Ok(vendasList);
        }

        [HttpGet("concluida")]
        public async Task<ActionResult<IEnumerable<Venda>>> GetVendaConcluida()
        {
            var vendas = await _context.Venda.ToListAsync();

            var vendasList = new List<Venda>();
            if (vendas != null)
            {
                foreach (var venda in vendas)
                {
                    if (vendas.Count == 1 && !venda.Ativa)
                        return NotFound("Nenhuma venda encontrada");
                    else
                    {
                        if (venda.Ativa && venda.Status == "Concluida")
                            vendasList.Add(await GetOneVenda(venda.Id));
                    }
                }
            }

            else
                return NotFound("Nenhuma venda encontrada");

            return Ok(vendasList);
        }

        [HttpGet("cancelada")]
        public async Task<ActionResult<IEnumerable<Venda>>> GetVendaCancelada()
        {
            var vendas = await _context.Venda.ToListAsync();

            var vendasList = new List<Venda>();
            if (vendas != null)
            {
                foreach (var venda in vendas)
                {
                    if (!venda.Ativa && venda.Status == "Cancelada")
                        vendasList.Add(await GetVendaFalse(venda.Id));
                }
            }

            else
                return NotFound("Nenhuma venda encontrada");

            return Ok(vendasList);
        }


        // POST: api/Vendas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Venda>> PostVenda(VendaDTO vendaDTO)
        {
            vendaDTO.DocumentoCliente = ValidarCNPJ.FormatarCNPJ(vendaDTO.DocumentoCliente);
            var cliente = await _context.Cliente.Include(c => c.Endereco).Where(c => c.CNPJ == vendaDTO.DocumentoCliente).FirstOrDefaultAsync();
            if (cliente == null || cliente.Status == "Inativo")
                return BadRequest("Cliente não encontrado");

            vendaDTO.DocumentoFuncionario = ValidarCPF.Desformatar(vendaDTO.DocumentoFuncionario);
            var funcionario = await _context.Usuario.Include(f => f.Endereco).Where(f => f.CPF == vendaDTO.DocumentoFuncionario).FirstOrDefaultAsync();
            if (funcionario == null || funcionario.Ativo == false)
                return BadRequest("Funcionário não encontrado.");

            var produtosVenda = new List<ProdutoVenda>();
            foreach (var item in vendaDTO.ProdutoVenda)
            {
                if (item.Quantidade < 1)
                    return BadRequest("Quantidade de produto inválida. A venda de cada lote de um produto deve conter pelo menos 1 unidade");

                var produto = await _context.Produto.FindAsync(item.ProdutoVenda);

                if (produto == null)
                    return BadRequest("Produto não encontrado.");

                if (produto.Quantidade < item.Quantidade)
                    return BadRequest("Quantidade de produto insuficiente, quantidade de produto em estoque:" + produto.Quantidade);

                if (produto.Status == "Inativo")
                    return BadRequest("O produto está inativo.");

                if (produto.Status == "Vencido")
                    return BadRequest("O produto está vencido.");

                if(produto.DataValidade < DateTime.Now)
                {
                    produto.Status = "Vencido";
                    _context.Produto.Update(produto);
                    await _context.SaveChangesAsync();
                    return BadRequest($"O produto {produto.NomeProduto} está vencido e portanto foi inativado e sua venda cancelada.");
                }

                produtosVenda.Add(new ProdutoVenda() { Produto = produto, Quantidade = item.Quantidade });

                produto.Quantidade -= item.Quantidade;

                if (produto.Quantidade == 0)
                    produto.Status = "Inativo";

                _context.Produto.Update(produto);
            }


            var venda = new Venda(cliente, funcionario, produtosVenda, vendaDTO.FormaPagamento);
            _context.ProdutoVenda.AddRange(produtosVenda);

            _context.Venda.Add(venda);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest("Houve um erro, tente novamente mais tarde" + e);
            }

            funcionario.Senha = null;

            return Ok(venda);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Venda>> DeleteVenda(int id)
        {
            var venda = await _context.Venda.Include(f => f.Funcionario).Include(f => f.Funcionario.Endereco).Include(c => c.Cliente).Include(c => c.Cliente.Endereco).Where(v => v.Id == id).FirstOrDefaultAsync();
            if (venda == null)
                return NotFound("Venda não encontrada");

            venda.Ativa = false;
            venda.Status = "Cancelada";
            var produtos = await _context.ProdutoVenda.Include(p => p.Produto).Where(p => p.VendaId == id).ToListAsync();

            _context.Venda.Update(venda);

            foreach (var produtoVenda in produtos)
            {
                var produto = produtoVenda.Produto;
                produto.Quantidade += produtoVenda.Quantidade;

                _context.Produto.Update(produto);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest("Houve um erro, tente novamente mais tarde" + e);
            }

            return Ok(venda);
        }

        private async Task<Venda> GetOneVenda(int id)
        {
            var venda = await _context.Venda.Include(f => f.Funcionario).Include(f => f.Funcionario.Endereco).Include(c => c.Cliente).Include(c => c.Cliente.Endereco).Where(v => v.Id == id).FirstOrDefaultAsync();

            if (venda != null && venda.Ativa)
            {
                venda.Produtos = await _context.ProdutoVenda.Include(p => p.Produto).Where(p => p.VendaId == id).ToListAsync();
                venda.Cliente.CNPJ = ValidarCNPJ.Desformatar(venda.Cliente.CNPJ);
                venda.Cliente.CNPJ = ValidarCNPJ.FormatarCNPJ(venda.Cliente.CNPJ);
                venda.Funcionario.CPF = ValidarCPF.Desformatar(venda.Funcionario.CPF);
                venda.Funcionario.CPF = ValidarCPF.Formatar(venda.Funcionario.CPF);
                venda.Funcionario.Senha = null;

                return venda;
            }

            return null;
        }

        private async Task<Venda> GetVendaFalse(int id)
        {
            var venda = await _context.Venda.Include(f => f.Funcionario).Include(f => f.Funcionario.Endereco).Include(c => c.Cliente).Include(c => c.Cliente.Endereco).Where(v => v.Id == id).FirstOrDefaultAsync();

            if (venda != null && !venda.Ativa)
            {
                venda.Produtos = await _context.ProdutoVenda.Include(p => p.Produto).Where(p => p.VendaId == id).ToListAsync();
                venda.Cliente.CNPJ = ValidarCNPJ.Desformatar(venda.Cliente.CNPJ);
                venda.Cliente.CNPJ = ValidarCNPJ.FormatarCNPJ(venda.Cliente.CNPJ);
                venda.Funcionario.CPF = ValidarCPF.Desformatar(venda.Funcionario.CPF);
                venda.Funcionario.CPF = ValidarCPF.Formatar(venda.Funcionario.CPF);
                venda.Funcionario.Senha = null;

                return venda;
            }

            return null;
        }

        private List<Venda> GetAllVendas()
        {
            var venda = _context.Venda.Include(f => f.Funcionario).Include(f => f.Funcionario.Endereco).Include(c => c.Cliente).Include(c => c.Cliente.Endereco).ToList();
            var vendas = new List<Venda>();

            foreach (var v in venda)
            {
                v.Produtos = _context.ProdutoVenda.Include(p => p.Produto).Where(p => p.VendaId == v.Id).ToList();
                v.Cliente.CNPJ = ValidarCNPJ.Desformatar(v.Cliente.CNPJ);
                v.Cliente.CNPJ = ValidarCNPJ.FormatarCNPJ(v.Cliente.CNPJ);
                v.Funcionario.CPF = ValidarCPF.Desformatar(v.Funcionario.CPF);
                v.Funcionario.CPF = ValidarCPF.Formatar(v.Funcionario.CPF);
                v.Funcionario.Senha = null;
                
                vendas.Add(v);
            }

            if (venda.Count > 0)
            {
                return vendas;
            }

            return null;
        }

    }
}