using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.OpenApi.Models;
using ProjetoRotaOeste.Repositories;
using ProjetoRotaOeste.Hubs;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Configuração do SignalR
builder.Services.AddSignalR();


// Configuração de serviços do controlador
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjetoRotaOeste API", Version = "v1" });
});

// Configuração de identidade
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Configurações do DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuração de autenticação JWT
var key = "sua_chave_secreta_aqui"; // Use uma chave secreta forte e mantenha em segredo
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});

// Repositório do usuário
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

//Serviço de email
builder.Services.AddScoped<IEmailSender, EmailSender>();

// Serviço de mensagem
builder.Services.AddScoped<IMensagemService, MensagemService>();

// Repositório de perguntas
builder.Services.AddScoped<IPerguntaRepository, PerguntaRepository>();

// Repositório de relatórios
builder.Services.AddScoped<IRelatorioRepository, RelatorioRepository>();


builder.WebHost.UseUrls("http://localhost:5183");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjetoRotaOeste API V1");
    c.RoutePrefix = "swagger"; // Set this to "" if you want to serve the UI at the app's root
});



// Usar autenticação
app.UseAuthentication();
app.UseAuthorization();

// Configuração do pipeline de solicitação...
app.MapControllers();

app.Run();
