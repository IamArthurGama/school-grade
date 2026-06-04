using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaNotasEscolar.Models;

public class Nota
{
    public int Id { get; set; }

    [Range(0, 10)]
    [Column(TypeName = "decimal(4,2)")]
    public decimal Valor { get; set; }

    public int AlunoId { get; set; }

    public Aluno Aluno { get; set; } = null!;

    public int DisciplinaId { get; set; }

    public Disciplina Disciplina { get; set; } = null!;
}
