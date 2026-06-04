namespace SistemaNotasEscolar.Models;

public class NotaDetalhada
{
    public string AlunoNome { get; set; } = string.Empty;

    public string Matricula { get; set; } = string.Empty;

    public string DisciplinaNome { get; set; } = string.Empty;

    public decimal Valor { get; set; }
}
