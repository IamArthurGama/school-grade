using SistemaNotasEscolar.Models;
using SistemaNotasEscolar.Repositories;
using SistemaNotasEscolar.Services;
using SistemaNotasEscolar.Tests.Repositories;

namespace SistemaNotasEscolar.Tests.Services;

public class DisciplinaServiceTests
{
    [Fact]
    public async Task CreateAsync_ShouldRejectEmptyName()
    {
        await using var context = TestDbContextFactory.Create();
        var service = new DisciplinaService(new DisciplinaRepository(context));

        var result = await service.CreateAsync(new Disciplina { Nome = " " });

        Assert.False(result.Success);
        Assert.Equal("Nome é obrigatório.", result.ErrorMessage);
    }

    [Fact]
    public async Task CreateAsync_ShouldPersistValidDisciplina()
    {
        await using var context = TestDbContextFactory.Create();
        var service = new DisciplinaService(new DisciplinaRepository(context));

        var result = await service.CreateAsync(new Disciplina { Nome = "Matemática" });

        Assert.True(result.Success);
        Assert.Single(await service.GetAllAsync());
    }
}
