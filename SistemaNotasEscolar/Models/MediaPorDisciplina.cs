namespace SistemaNotasEscolar.Models;

public class MediaPorDisciplina
{
    public string DisciplinaNome { get; set; } = string.Empty;

    public int QuantidadeNotas { get; set; }

    public decimal Media { get; set; }
}
