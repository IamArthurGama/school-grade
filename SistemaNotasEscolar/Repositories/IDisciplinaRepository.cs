using SistemaNotasEscolar.Models;

namespace SistemaNotasEscolar.Repositories;

public interface IDisciplinaRepository
{
    Task<IEnumerable<Disciplina>> GetAllAsync();

    Task<Disciplina?> GetByIdAsync(int id);

    Task AddAsync(Disciplina disciplina);

    void Update(Disciplina disciplina);

    void Delete(Disciplina disciplina);

    Task<bool> HasNotasAsync(int disciplinaId);

    Task SaveChangesAsync();
}
