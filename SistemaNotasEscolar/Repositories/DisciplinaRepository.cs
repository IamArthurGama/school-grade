using Microsoft.EntityFrameworkCore;
using SistemaNotasEscolar.Data;
using SistemaNotasEscolar.Models;

namespace SistemaNotasEscolar.Repositories;

public class DisciplinaRepository : IDisciplinaRepository
{
    private readonly AppDbContext _context;

    public DisciplinaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Disciplina>> GetAllAsync()
    {
        return await _context.Disciplinas
            .AsNoTracking()
            .OrderBy(disciplina => disciplina.Nome)
            .ToListAsync();
    }

    public async Task<Disciplina?> GetByIdAsync(int id)
    {
        return await _context.Disciplinas
            .Include(disciplina => disciplina.Notas)
            .FirstOrDefaultAsync(disciplina => disciplina.Id == id);
    }

    public async Task AddAsync(Disciplina disciplina)
    {
        await _context.Disciplinas.AddAsync(disciplina);
    }

    public void Update(Disciplina disciplina)
    {
        _context.Disciplinas.Update(disciplina);
    }

    public void Delete(Disciplina disciplina)
    {
        _context.Disciplinas.Remove(disciplina);
    }

    public async Task<bool> HasNotasAsync(int disciplinaId)
    {
        return await _context.Notas.AnyAsync(nota => nota.DisciplinaId == disciplinaId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
