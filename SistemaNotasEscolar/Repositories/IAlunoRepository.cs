using SistemaNotasEscolar.Models;

namespace SistemaNotasEscolar.Repositories;

public interface IAlunoRepository
{
    Task<IEnumerable<Aluno>> GetAllAsync();

    Task<Aluno?> GetByIdAsync(int id);

    Task<Aluno?> GetByMatriculaAsync(string matricula);

    Task AddAsync(Aluno aluno);

    void Update(Aluno aluno);

    void Delete(Aluno aluno);

    Task<bool> HasNotasAsync(int alunoId);

    Task SaveChangesAsync();
}
