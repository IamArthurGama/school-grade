using SistemaNotasEscolar.Models;

namespace SistemaNotasEscolar.Repositories;

public interface INotaRepository
{
    Task<IEnumerable<Nota>> GetAllAsync();

    Task<Nota?> GetByIdAsync(int id);

    Task<IEnumerable<Nota>> GetByAlunoAsync(int alunoId);

    Task<IEnumerable<Nota>> GetByDisciplinaAsync(int disciplinaId);

    Task AddAsync(Nota nota);

    void Update(Nota nota);

    void Delete(Nota nota);

    Task SaveChangesAsync();
}
