using FazendaAPI.Controllers;
using FazendaAPI.Data;
using FazendaAPI.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext para uso com o SQL Server
builder.Services.AddDbContext<FazendaAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FazendaAPIContext")
    ?? throw new InvalidOperationException("Connection string 'FazaendaAPIContext' not found.")));

// Adiciona serviços ao contêiner
builder.Services.AddControllers();

// Adiciona serviços de utilidades e validações como transientes
builder.Services.AddTransient<ServiceEndereco>();
builder.Services.AddTransient<ValidarCNPJ>();
builder.Services.AddTransient<ValidarEmail>();
builder.Services.AddTransient<ValidarTelefone>();

// Adiciona controladores como serviços escopados
builder.Services.AddScoped<InsumosController>();
builder.Services.AddScoped<RegistrosInfestacoesController>();
builder.Services.AddScoped<PlantacoesController>();
builder.Services.AddScoped<UsuariosController>();

// Configura o CORS para permitir requisições de qualquer origem
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Adiciona suporte ao Swagger para documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura o pipeline de requisições HTTP
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

// Aplica a política de CORS
app.UseCors("AllowAllOrigins");

// Mapeia os controladores para as rotas
app.MapControllers();

// Inicia o aplicativo
app.Run();
