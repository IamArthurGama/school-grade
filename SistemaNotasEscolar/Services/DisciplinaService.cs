using SistemaNotasEscolar.Models;
using SistemaNotasEscolar.Repositories;

namespace SistemaNotasEscolar.Services;

public class DisciplinaService : IDisciplinaService
{
    private readonly IDisciplinaRepository _disciplinaRepository;

    public DisciplinaService(IDisciplinaRepository disciplinaRepository)
    {
        _disciplinaRepository = disciplinaRepository;
    }

    public Task<IEnumerable<Disciplina>> GetAllAsync()
    {
        return _disciplinaRepository.GetAllAsync();
    }

    public Task<Disciplina?> GetByIdAsync(int id)
    {
        return _disciplinaRepository.GetByIdAsync(id);
    }

    public async Task<ServiceResult> CreateAsync(Disciplina disciplina)
    {
        var validation = Validate(disciplina);
        if (!validation.Success)
        {
            return validation;
        }

        await _disciplinaRepository.AddAsync(disciplina);
        await _disciplinaRepository.SaveChangesAsync();
        return ServiceResult.Ok();
    }

    public async Task<ServiceResult> UpdateAsync(Disciplina disciplina)
    {
        var existing = await _disciplinaRepository.GetByIdAsync(disciplina.Id);
        if (existing is null)
        {
            return ServiceResult.Fail("Disciplina não encontrada.");
        }

        var validation = Validate(disciplina);
        if (!validation.Success)
        {
            return validation;
        }

        existing.Nome = disciplina.Nome.Trim();

        _disciplinaRepository.Update(existing);
        await _disciplinaRepository.SaveChangesAsync();
        return ServiceResult.Ok();
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var disciplina = await _disciplinaRepository.GetByIdAsync(id);
        if (disciplina is null)
        {
            return ServiceResult.Fail("Disciplina não encontrada.");
        }

        if (await _disciplinaRepository.HasNotasAsync(id))
        {
            return ServiceResult.Fail("Não é possível excluir disciplina com notas cadastradas.");
        }

        _disciplinaRepository.Delete(disciplina);
        await _disciplinaRepository.SaveChangesAsync();
        return ServiceResult.Ok();
    }

    private static ServiceResult Validate(Disciplina disciplina)
    {
        disciplina.Nome = disciplina.Nome.Trim();

        return string.IsNullOrWhiteSpace(disciplina.Nome)
            ? ServiceResult.Fail("Nome é obrigatório.")
            : ServiceResult.Ok();
    }
}
