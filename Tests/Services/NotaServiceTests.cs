using SistemaNotasEscolar.Models;
using SistemaNotasEscolar.Repositories;
using SistemaNotasEscolar.Services;
using SistemaNotasEscolar.Tests.Repositories;

namespace SistemaNotasEscolar.Tests.Services;

public class NotaServiceTests
{
    [Fact]
    public async Task CreateAsync_ShouldRejectGradeOutsideRange()
    {
        await using var context = TestDbContextFactory.Create();
        var service = CreateService(context);

        var result = await service.CreateAsync(new Nota { AlunoId = 1, DisciplinaId = 1, Valor = 11 });

        Assert.False(result.Success);
        Assert.Equal("Nota deve estar entre 0 e 10.", result.ErrorMessage);
    }

    [Fact]
    public async Task CreateAsync_ShouldRejectMissingAluno()
    {
        await using var context = TestDbContextFactory.Create();
        var service = CreateService(context);
        context.Disciplinas.Add(new Disciplina { Nome = "História" });
        await context.SaveChangesAsync();

        var result = await service.CreateAsync(new Nota { AlunoId = 999, DisciplinaId = 1, Valor = 8 });

        Assert.False(result.Success);
        Assert.Equal("Aluno válido é obrigatório.", result.ErrorMessage);
    }

    [Fact]
    public async Task CreateAsync_ShouldPersistValidNota()
    {
        await using var context = TestDbContextFactory.Create();
        var aluno = new Aluno { Nome = "Ana", Matricula = "A001" };
        var disciplina = new Disciplina { Nome = "História" };
        context.Alunos.Add(aluno);
        context.Disciplinas.Add(disciplina);
        await context.SaveChangesAsync();
        var service = CreateService(context);

        var result = await service.CreateAsync(new Nota { AlunoId = aluno.Id, DisciplinaId = disciplina.Id, Valor = 8 });

        Assert.True(result.Success);
        Assert.Single(await service.GetAllAsync());
    }

    private static NotaService CreateService(Data.AppDbContext context)
    {
        return new NotaService(
            new AlunoRepository(context),
            new DisciplinaRepository(context),
            new NotaRepository(context));
    }
}
