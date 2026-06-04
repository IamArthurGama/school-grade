using SistemaNotasEscolar.Models;
using SistemaNotasEscolar.Repositories;

namespace SistemaNotasEscolar.Services;

public class AlunoService : IAlunoService
{
    private readonly IAlunoRepository _alunoRepository;

    public AlunoService(IAlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;
    }

    public Task<IEnumerable<Aluno>> GetAllAsync()
    {
        return _alunoRepository.GetAllAsync();
    }

    public Task<Aluno?> GetByIdAsync(int id)
    {
        return _alunoRepository.GetByIdAsync(id);
    }

    public async Task<ServiceResult> CreateAsync(Aluno aluno)
    {
        var validation = await ValidateAsync(aluno);
        if (!validation.Success)
        {
            return validation;
        }

        await _alunoRepository.AddAsync(aluno);
        await _alunoRepository.SaveChangesAsync();
        return ServiceResult.Ok();
    }

    public async Task<ServiceResult> UpdateAsync(Aluno aluno)
    {
        var existing = await _alunoRepository.GetByIdAsync(aluno.Id);
        if (existing is null)
        {
            return ServiceResult.Fail("Aluno não encontrado.");
        }

        var validation = await ValidateAsync(aluno);
        if (!validation.Success)
        {
            return validation;
        }

        existing.Nome = aluno.Nome.Trim();
        existing.Matricula = aluno.Matricula.Trim();

        _alunoRepository.Update(existing);
        await _alunoRepository.SaveChangesAsync();
        return ServiceResult.Ok();
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var aluno = await _alunoRepository.GetByIdAsync(id);
        if (aluno is null)
        {
            return ServiceResult.Fail("Aluno não encontrado.");
        }

        if (await _alunoRepository.HasNotasAsync(id))
        {
            return ServiceResult.Fail("Não é possível excluir aluno com notas cadastradas.");
        }

        _alunoRepository.Delete(aluno);
        await _alunoRepository.SaveChangesAsync();
        return ServiceResult.Ok();
    }

    private async Task<ServiceResult> ValidateAsync(Aluno aluno)
    {
        aluno.Nome = aluno.Nome.Trim();
        aluno.Matricula = aluno.Matricula.Trim();

        if (string.IsNullOrWhiteSpace(aluno.Nome))
        {
            return ServiceResult.Fail("Nome é obrigatório.");
        }

        if (string.IsNullOrWhiteSpace(aluno.Matricula))
        {
            return ServiceResult.Fail("Matrícula é obrigatória.");
        }

        var sameMatricula = await _alunoRepository.GetByMatriculaAsync(aluno.Matricula);
        if (sameMatricula is not null && sameMatricula.Id != aluno.Id)
        {
            return ServiceResult.Fail("Já existe aluno com esta matrícula.");
        }

        return ServiceResult.Ok();
    }
}
