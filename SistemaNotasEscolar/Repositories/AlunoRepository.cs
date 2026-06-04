using Microsoft.EntityFrameworkCore;
using SistemaNotasEscolar.Data;
using SistemaNotasEscolar.Models;

namespace SistemaNotasEscolar.Repositories;

public class AlunoRepository : IAlunoRepository
{
    private readonly AppDbContext _context;

    public AlunoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Aluno>> GetAllAsync()
    {
        return await _context.Alunos
            .AsNoTracking()
            .OrderBy(aluno => aluno.Nome)
            .ToListAsync();
    }

    public async Task<Aluno?> GetByIdAsync(int id)
    {
        return await _context.Alunos
            .Include(aluno => aluno.Notas)
            .FirstOrDefaultAsync(aluno => aluno.Id == id);
    }

    public async Task<Aluno?> GetByMatriculaAsync(string matricula)
    {
        return await _context.Alunos
            .AsNoTracking()
            .FirstOrDefaultAsync(aluno => aluno.Matricula == matricula);
    }

    public async Task AddAsync(Aluno aluno)
    {
        await _context.Alunos.AddAsync(aluno);
    }

    public void Update(Aluno aluno)
    {
        _context.Alunos.Update(aluno);
    }

    public void Delete(Aluno aluno)
    {
        _context.Alunos.Remove(aluno);
    }

    public async Task<bool> HasNotasAsync(int alunoId)
    {
        return await _context.Notas.AnyAsync(nota => nota.AlunoId == alunoId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
