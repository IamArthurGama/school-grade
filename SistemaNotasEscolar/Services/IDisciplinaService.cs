using SistemaNotasEscolar.Models;

namespace SistemaNotasEscolar.Services;

public interface IDisciplinaService
{
    Task<IEnumerable<Disciplina>> GetAllAsync();

    Task<Disciplina?> GetByIdAsync(int id);

    Task<ServiceResult> CreateAsync(Disciplina disciplina);

    Task<ServiceResult> UpdateAsync(Disciplina disciplina);

    Task<ServiceResult> DeleteAsync(int id);
}
