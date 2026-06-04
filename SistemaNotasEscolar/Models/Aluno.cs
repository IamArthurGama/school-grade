using System.ComponentModel.DataAnnotations;

namespace SistemaNotasEscolar.Models;

public class Aluno
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Matricula { get; set; } = string.Empty;

    public ICollection<Nota> Notas { get; set; } = new List<Nota>();
}
