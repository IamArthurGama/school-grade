using Microsoft.EntityFrameworkCore;
using SistemaNotasEscolar.Data;
using SistemaNotasEscolar.Models;

namespace SistemaNotasEscolar.Repositories;

public class NotaRepository : INotaRepository
{
    private readonly AppDbContext _context;

    public NotaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Nota>> GetAllAsync()
    {
        return await _context.Notas
            .AsNoTracking()
            .Include(nota => nota.Aluno)
            .Include(nota => nota.Disciplina)
            .OrderBy(nota => nota.Aluno.Nome)
            .ThenBy(nota => nota.Disciplina.Nome)
            .ToListAsync();
    }

    public async Task<Nota?> GetByIdAsync(int id)
    {
        return await _context.Notas
            .Include(nota => nota.Aluno)
            .Include(nota => nota.Disciplina)
            .FirstOrDefaultAsync(nota => nota.Id == id);
    }

    public async Task<IEnumerable<Nota>> GetByAlunoAsync(int alunoId)
    {
        return await _context.Notas
            .AsNoTracking()
            .Include(nota => nota.Disciplina)
            .Where(nota => nota.AlunoId == alunoId)
            .OrderBy(nota => nota.Disciplina.Nome)
            .ToListAsync();
    }

    public async Task<IEnumerable<Nota>> GetByDisciplinaAsync(int disciplinaId)
    {
        return await _context.Notas
            .AsNoTracking()
            .Include(nota => nota.Aluno)
            .Where(nota => nota.DisciplinaId == disciplinaId)
            .OrderBy(nota => nota.Aluno.Nome)
            .ToListAsync();
    }

    public async Task AddAsync(Nota nota)
    {
        await _context.Notas.AddAsync(nota);
    }

    public void Update(Nota nota)
    {
        _context.Notas.Update(nota);
    }

    public void Delete(Nota nota)
    {
        _context.Notas.Remove(nota);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
