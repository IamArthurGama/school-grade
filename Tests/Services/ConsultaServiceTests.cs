using SistemaNotasEscolar.Models;
using SistemaNotasEscolar.Services;
using SistemaNotasEscolar.Tests.Repositories;

namespace SistemaNotasEscolar.Tests.Services;

public class ConsultaServiceTests
{
    [Fact]
    public async Task GetNotasPorAlunoDisciplinaAsync_ShouldReturnCombinedAlunoDisciplinaNota()
    {
        await using var context = TestDbContextFactory.Create();
        await SeedAsync(context);
        var service = new ConsultaService(context);

        var result = (await service.GetNotasPorAlunoDisciplinaAsync()).ToList();

        Assert.Contains(result, nota =>
            nota.AlunoNome == "Ana" &&
            nota.Matricula == "A001" &&
            nota.DisciplinaNome == "Matemática" &&
            nota.Valor == 8);
    }

    [Fact]
    public async Task GetMediasPorDisciplinaAsync_ShouldGroupByDisciplinaAndCalculateAverage()
    {
        await using var context = TestDbContextFactory.Create();
        await SeedAsync(context);
        var service = new ConsultaService(context);

        var result = (await service.GetMediasPorDisciplinaAsync()).ToList();
        var matematica = result.Single(media => media.DisciplinaNome == "Matemática");

        Assert.Equal(2, matematica.QuantidadeNotas);
        Assert.Equal(7, matematica.Media);
    }

    [Fact]
    public async Task GetNotasAprovadasAsync_ShouldFilterNotasGreaterOrEqualSix()
    {
        await using var context = TestDbContextFactory.Create();
        await SeedAsync(context);
        var service = new ConsultaService(context);

        var result = (await service.GetNotasAprovadasAsync()).ToList();

        Assert.All(result, nota => Assert.True(nota.Valor >= 6));
        Assert.DoesNotContain(result, nota => nota.Valor == 5);
    }

    [Fact]
    public async Task GetDisciplinasComMediaMinimaAsync_ShouldFilterGroupedAverageLikeHaving()
    {
        await using var context = TestDbContextFactory.Create();
        await SeedAsync(context);
        var service = new ConsultaService(context);

        var result = (await service.GetDisciplinasComMediaMinimaAsync()).ToList();

        Assert.Single(result);
        Assert.Equal("Matemática", result[0].DisciplinaNome);
        Assert.Equal(7, result[0].Media);
    }

    private static async Task SeedAsync(Data.AppDbContext context)
    {
        var ana = new Aluno { Nome = "Ana", Matricula = "A001" };
        var bruno = new Aluno { Nome = "Bruno", Matricula = "B001" };
        var matematica = new Disciplina { Nome = "Matemática" };
        var historia = new Disciplina { Nome = "História" };

        context.AddRange(ana, bruno, matematica, historia);
        await context.SaveChangesAsync();

        context.Notas.AddRange(
            new Nota { AlunoId = ana.Id, DisciplinaId = matematica.Id, Valor = 8 },
            new Nota { AlunoId = bruno.Id, DisciplinaId = matematica.Id, Valor = 6 },
            new Nota { AlunoId = ana.Id, DisciplinaId = historia.Id, Valor = 5 });

        await context.SaveChangesAsync();
    }
}
