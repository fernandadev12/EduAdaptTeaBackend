using AdaptaEdu.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdaptaEdu.Infrastructure.Data;

// Infrastructure/Data/AppDbContext.cs
public class AppDbContext : IdentityDbContext<Usuario, IdentityRole<Guid>, Guid>
{
    public DbSet<Aluno> Alunos => Set<Aluno>();
    public DbSet<PerfilAdaptacao> PerfisAdaptacao => Set<PerfilAdaptacao>();
    public DbSet<TarefaOriginal> TarefasOriginais => Set<TarefaOriginal>();
    public DbSet<TarefaAdaptada> TarefasAdaptadas => Set<TarefaAdaptada>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}