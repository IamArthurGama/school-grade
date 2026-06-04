using SistemaNotasEscolar.Models;
using SistemaNotasEscolar.Repositories;

namespace SistemaNotasEscolar.Services;

public class NotaService : INotaService
{
    private readonly IAlunoRepository _alunoRepository;
    private readonly IDisciplinaRepository _disciplinaRepository;
    private readonly INotaRepository _notaRepository;

    public NotaService(
        IAlunoRepository alunoRepository,
        IDisciplinaRepository disciplinaRepository,
        INotaRepository notaRepository)
    {
        _alunoRepository = alunoRepository;
        _disciplinaRepository = disciplinaRepository;
        _notaRepository = notaRepository;
    }

    public Task<IEnumerable<Nota>> GetAllAsync()
    {
        return _notaRepository.GetAllAsync();
    }

    public Task<Nota?> GetByIdAsync(int id)
    {
        return _notaRepository.GetByIdAsync(id);
    }

    public async Task<ServiceResult> CreateAsync(Nota nota)
    {
        var validation = await ValidateAsync(nota);
        if (!validation.Success)
        {
            return validation;
        }

        await _notaRepository.AddAsync(nota);
        await _notaRepository.SaveChangesAsync();
        return ServiceResult.Ok();
    }

    public async Task<ServiceResult> UpdateAsync(Nota nota)
    {
        var existing = await _notaRepository.GetByIdAsync(nota.Id);
        if (existing is null)
        {
            return ServiceResult.Fail("Nota não encontrada.");
        }

        var validation = await ValidateAsync(nota);
        if (!validation.Success)
        {
            return validation;
        }

        existing.Valor = nota.Valor;
        existing.AlunoId = nota.AlunoId;
        existing.DisciplinaId = nota.DisciplinaId;

        _notaRepository.Update(existing);
        await _notaRepository.SaveChangesAsync();
        return ServiceResult.Ok();
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var nota = await _notaRepository.GetByIdAsync(id);
        if (nota is null)
        {
            return ServiceResult.Fail("Nota não encontrada.");
        }

        _notaRepository.Delete(nota);
        await _notaRepository.SaveChangesAsync();
        return ServiceResult.Ok();
    }

    private async Task<ServiceResult> ValidateAsync(Nota nota)
    {
        if (nota.Valor < 0 || nota.Valor > 10)
        {
            return ServiceResult.Fail("Nota deve estar entre 0 e 10.");
        }

        if (nota.AlunoId <= 0 || await _alunoRepository.GetByIdAsync(nota.AlunoId) is null)
        {
            return ServiceResult.Fail("Aluno válido é obrigatório.");
        }

        if (nota.DisciplinaId <= 0 || await _disciplinaRepository.GetByIdAsync(nota.DisciplinaId) is null)
        {
            return ServiceResult.Fail("Disciplina válida é obrigatória.");
        }

        return ServiceResult.Ok();
    }
}
