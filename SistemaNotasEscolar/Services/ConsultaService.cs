using Microsoft.EntityFrameworkCore;
using SistemaNotasEscolar.Data;
using SistemaNotasEscolar.Models;

namespace SistemaNotasEscolar.Services;

public class ConsultaService : IConsultaService
{
    private readonly AppDbContext _context;

    public ConsultaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<NotaDetalhada>> GetNotasPorAlunoDisciplinaAsync()
    {
        return await _context.Notas
            .AsNoTracking()
            .Select(nota => new NotaDetalhada
            {
                AlunoNome = nota.Aluno.Nome,
                Matricula = nota.Aluno.Matricula,
                DisciplinaNome = nota.Disciplina.Nome,
                Valor = nota.Valor
            })
            .OrderBy(nota => nota.AlunoNome)
            .ThenBy(nota => nota.DisciplinaNome)
            .ToListAsync();
    }

    public async Task<IEnumerable<MediaPorDisciplina>> GetMediasPorDisciplinaAsync()
    {
        return await _context.Notas
            .AsNoTracking()
            .GroupBy(nota => nota.Disciplina.Nome)
            .Select(group => new MediaPorDisciplina
            {
                DisciplinaNome = group.Key,
                QuantidadeNotas = group.Count(),
                Media = group.Average(nota => nota.Valor)
            })
            .OrderBy(media => media.DisciplinaNome)
            .ToListAsync();
    }

    public async Task<IEnumerable<NotaDetalhada>> GetNotasAprovadasAsync(decimal notaMinima = 6)
    {
        return await _context.Notas
            .AsNoTracking()
            .Where(nota => nota.Valor >= notaMinima)
            .Select(nota => new NotaDetalhada
            {
                AlunoNome = nota.Aluno.Nome,
                Matricula = nota.Aluno.Matricula,
                DisciplinaNome = nota.Disciplina.Nome,
                Valor = nota.Valor
            })
            .OrderBy(nota => nota.AlunoNome)
            .ThenBy(nota => nota.DisciplinaNome)
            .ToListAsync();
    }

    public async Task<IEnumerable<MediaPorDisciplina>> GetDisciplinasComMediaMinimaAsync(decimal mediaMinima = 7)
    {
        return await _context.Notas
            .AsNoTracking()
            .GroupBy(nota => nota.Disciplina.Nome)
            .Select(group => new MediaPorDisciplina
            {
                DisciplinaNome = group.Key,
                QuantidadeNotas = group.Count(),
                Media = group.Average(nota => nota.Valor)
            })
            .Where(media => media.Media >= mediaMinima)
            .OrderBy(media => media.DisciplinaNome)
            .ToListAsync();
    }
}
