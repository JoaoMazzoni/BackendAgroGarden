using FazendaAPI.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace FazendaAPI.Utils
{
    public class ServiceEndereco
    {
        private readonly FazendaAPIContext _context;

        public ServiceEndereco(FazendaAPIContext context)
        {
            _context = context;
        }


        public async Task<Endereco> GetEnderecoCorreio(EnderecoDTO endereco)
        {
            if (endereco == null || string.IsNullOrWhiteSpace(endereco.CEP))
            {
                throw new ArgumentException("CEP não fornecido.");
            }

            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://viacep.com.br/");
                    var response = await client.GetAsync($"ws/{endereco.CEP}/json/");

                    if (response.IsSuccessStatusCode)
                    {
                        var stringResult = await response.Content.ReadAsStringAsync();
                        var end = Newtonsoft.Json.JsonConvert.DeserializeObject<Endereco>(stringResult);

                        if (end == null)
                        {
                            throw new Exception("Não foi possível encontrar o endereço para o CEP fornecido.");
                        }


                        if (string.IsNullOrWhiteSpace(end.Cidade) || string.IsNullOrWhiteSpace(end.Estado) || string.IsNullOrWhiteSpace(end.Rua))
                        {
                            throw new Exception("CEP Inválido. Erro ao obter endereço do serviço ViaCEP.");
                        }

                        var cep = end.CEP.Replace("-", "");

                        end.Complemento = endereco.Complemento;
                        end.Numero = endereco.Numero;
                        end.Id = $"{cep}{end.Numero}";

                        if (!EnderecoExistsDataBase(end.Id))
                        {
                            _context.Endereco.Add(end);
                            await _context.SaveChangesAsync();
                            return end;
                        }

                        _context.Entry(end).State = EntityState.Modified;
                        return end;
                    }
                    else
                    {
                        throw new Exception($"Erro ao acessar o serviço ViaCEP. Código de status: {response.StatusCode}");
                    }
                }
                catch (HttpRequestException e)
                {
                    throw new Exception($"Erro de comunicação com o serviço ViaCEP: {e.Message}", e);
                }
                catch (Exception e)
                {
                    throw new Exception($"Erro inesperado: {e.Message}", e);
                }
            }
        }

        private bool EnderecoExistsDataBase(string id)
        {
            return _context.Endereco.Any(e => e.Id == id);
        }
    }
}
