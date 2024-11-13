using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoRotaOeste.Models;

public class AppDbContext : IdentityDbContext<Usuario>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Definindo as tabelas como propriedades DbSet
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Pergunta> Perguntas { get; set; }
    public DbSet<FinalidadePergunta> FinalidadesPergunta { get; set; }
    public DbSet<RespostaItem> RespostasItem { get; set; }
    //public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<RespostaItem> RespostaItems { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Definindo chaves primárias e relacionamentos
        modelBuilder.Entity<Cliente>()
            .HasKey(c => c.IdCliente);
  
        modelBuilder.Entity<Pergunta>()
            .HasKey(p => p.IdPergunta);
        
        modelBuilder.Entity<FinalidadePergunta>()
            .HasKey(fp => fp.IdFinalidade);
        
        modelBuilder.Entity<Pergunta>()
            .HasMany(p => p.UserClients)
            .WithMany()
            .UsingEntity(j => j.ToTable("PerguntaUsuarios"));

        modelBuilder.Entity<RespostaItem>()
            .HasKey(r => r.IdPergunta);

        modelBuilder.Entity<Pergunta>()
            .HasOne(p => p.RespostaItem)
            .WithOne(r => r.Pergunta)
            .HasForeignKey<RespostaItem>(r => r.IdPergunta);
    }
}
