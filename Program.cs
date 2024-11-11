using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.OpenApi.Models;
using ProjetoRotaOeste.Repositories;
using ProjetoRotaOeste.Hubs;
using Microsoft.AspNetCore.Identity;

using ProjetoRotaOeste.Models;

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
builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Configura o Email como UserName no Identity
builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
});

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
//builder.Services.AddScoped<IRelatorioRepository, RelatorioRepository>();

// Register IClienteRepository with ClienteRepository
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

// Add CORS policy to allow requests from your Flutter app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

// Initialize roles
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = { "Admin", "Client" };

    foreach (var role in roles)
    {
        if (!roleManager.RoleExistsAsync(role).Result)
        {
            roleManager.CreateAsync(new IdentityRole(role)).Wait();
        }
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowAll");

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

// Listen on all network interfaces
app.Urls.Add("http://0.0.0.0:5183");

app.Run();
