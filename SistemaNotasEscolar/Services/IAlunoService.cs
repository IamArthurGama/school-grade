using SistemaNotasEscolar.Models;

namespace SistemaNotasEscolar.Services;

public interface IAlunoService
{
    Task<IEnumerable<Aluno>> GetAllAsync();

    Task<Aluno?> GetByIdAsync(int id);

    Task<ServiceResult> CreateAsync(Aluno aluno);

    Task<ServiceResult> UpdateAsync(Aluno aluno);

    Task<ServiceResult> DeleteAsync(int id);
}
