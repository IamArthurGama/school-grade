using SistemaNotasEscolar.Models;

namespace SistemaNotasEscolar.Services;

public interface INotaService
{
    Task<IEnumerable<Nota>> GetAllAsync();

    Task<Nota?> GetByIdAsync(int id);

    Task<ServiceResult> CreateAsync(Nota nota);

    Task<ServiceResult> UpdateAsync(Nota nota);

    Task<ServiceResult> DeleteAsync(int id);
}
