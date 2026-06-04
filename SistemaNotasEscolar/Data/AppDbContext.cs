using Microsoft.EntityFrameworkCore;
using SistemaNotasEscolar.Models;

namespace SistemaNotasEscolar.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Aluno> Alunos => Set<Aluno>();

    public DbSet<Disciplina> Disciplinas => Set<Disciplina>();

    public DbSet<Nota> Notas => Set<Nota>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Aluno>(entity =>
        {
            entity.HasIndex(aluno => aluno.Matricula).IsUnique();
            entity.Property(aluno => aluno.Nome).HasMaxLength(100).IsRequired();
            entity.Property(aluno => aluno.Matricula).HasMaxLength(20).IsRequired();
        });

        modelBuilder.Entity<Disciplina>(entity =>
        {
            entity.Property(disciplina => disciplina.Nome).HasMaxLength(100).IsRequired();
        });

        modelBuilder.Entity<Nota>(entity =>
        {
            entity.Property(nota => nota.Valor).HasColumnType("decimal(4,2)").IsRequired();

            entity.HasOne(nota => nota.Aluno)
                .WithMany(aluno => aluno.Notas)
                .HasForeignKey(nota => nota.AlunoId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(nota => nota.Disciplina)
                .WithMany(disciplina => disciplina.Notas)
                .HasForeignKey(nota => nota.DisciplinaId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
