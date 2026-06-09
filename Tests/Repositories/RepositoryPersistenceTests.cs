using SistemaNotasEscolar.Models;
using SistemaNotasEscolar.Repositories;

namespace SistemaNotasEscolar.Tests.Repositories;

public class RepositoryPersistenceTests
{
    [Fact]
    public async Task Repositories_ShouldPersistAlunoDisciplinaAndNota()
    {
        await using var context = TestDbContextFactory.Create();
        var alunoRepository = new AlunoRepository(context);
        var disciplinaRepository = new DisciplinaRepository(context);
        var notaRepository = new NotaRepository(context);

        await alunoRepository.AddAsync(new Aluno { Nome = "Ana", Matricula = "A001" });
        await disciplinaRepository.AddAsync(new Disciplina { Nome = "Matemática" });
        await context.SaveChangesAsync();

        var aluno = (await alunoRepository.GetAllAsync()).Single();
        var disciplina = (await disciplinaRepository.GetAllAsync()).Single();
        await notaRepository.AddAsync(new Nota { AlunoId = aluno.Id, DisciplinaId = disciplina.Id, Valor = 9 });
        await notaRepository.SaveChangesAsync();

        var notas = (await notaRepository.GetAllAsync()).ToList();

        Assert.Single(notas);
        Assert.Equal("Ana", notas[0].Aluno.Nome);
        Assert.Equal("Matemática", notas[0].Disciplina.Nome);
    }
}
