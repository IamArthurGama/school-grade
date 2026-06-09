using SistemaNotasEscolar.Models;
using SistemaNotasEscolar.Repositories;
using SistemaNotasEscolar.Services;
using SistemaNotasEscolar.Tests.Repositories;

namespace SistemaNotasEscolar.Tests.Services;

public class AlunoServiceTests
{
    [Fact]
    public async Task CreateAsync_ShouldRejectEmptyName()
    {
        await using var context = TestDbContextFactory.Create();
        var service = CreateService(context);

        var result = await service.CreateAsync(new Aluno { Nome = " ", Matricula = "A001" });

        Assert.False(result.Success);
        Assert.Equal("Nome é obrigatório.", result.ErrorMessage);
    }

    [Fact]
    public async Task CreateAsync_ShouldRejectDuplicatedMatricula()
    {
        await using var context = TestDbContextFactory.Create();
        var service = CreateService(context);
        await service.CreateAsync(new Aluno { Nome = "Ana", Matricula = "A001" });

        var result = await service.CreateAsync(new Aluno { Nome = "Bruno", Matricula = "A001" });

        Assert.False(result.Success);
        Assert.Equal("Já existe aluno com esta matrícula.", result.ErrorMessage);
    }

    [Fact]
    public async Task CreateAsync_ShouldPersistValidAluno()
    {
        await using var context = TestDbContextFactory.Create();
        var service = CreateService(context);

        var result = await service.CreateAsync(new Aluno { Nome = "Ana", Matricula = "A001" });

        Assert.True(result.Success);
        Assert.Single(await service.GetAllAsync());
    }

    private static AlunoService CreateService(Data.AppDbContext context)
    {
        return new AlunoService(new AlunoRepository(context));
    }
}
