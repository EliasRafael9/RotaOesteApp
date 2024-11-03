using Microsoft.EntityFrameworkCore;
using ProjetoRotaOeste.Models;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Definindo as tabelas como propriedades DbSet
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Conversa> Conversas { get; set; }
    public DbSet<Pergunta> Perguntas { get; set; }
    public DbSet<FinalidadePergunta> FinalidadesPergunta { get; set; }
    public DbSet<RespostaItem> RespostasItem { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Definindo chaves primárias e relacionamentos, se necessário
        modelBuilder.Entity<Cliente>()
            .HasKey(c => c.IdCliente);

        modelBuilder.Entity<Conversa>()
            .HasKey(c => c.IdConversa);
        
        modelBuilder.Entity<Pergunta>()
            .HasKey(p => p.IdPergunta);
        
        modelBuilder.Entity<FinalidadePergunta>()
            .HasKey(fp => fp.IdFinalidade);
        
        modelBuilder.Entity<RespostaItem>()
            .HasKey(ri => ri.IdRespostaItem);

        modelBuilder.Entity<Usuario>()
            .HasKey(u => u.Id);    
                    
                    // Configurações adicionais, se necessário
    }
}
