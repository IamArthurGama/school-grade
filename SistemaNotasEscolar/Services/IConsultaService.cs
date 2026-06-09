using SistemaNotasEscolar.Models;

namespace SistemaNotasEscolar.Services;

public interface IConsultaService
{
    Task<IEnumerable<NotaDetalhada>> GetNotasPorAlunoDisciplinaAsync();

    Task<IEnumerable<MediaPorDisciplina>> GetMediasPorDisciplinaAsync();

    Task<IEnumerable<NotaDetalhada>> GetNotasAprovadasAsync(decimal notaMinima = 6);

    Task<IEnumerable<MediaPorDisciplina>> GetDisciplinasComMediaMinimaAsync(decimal mediaMinima = 7);
}
