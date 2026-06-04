using System.ComponentModel.DataAnnotations;

namespace SistemaNotasEscolar.Models;

public class Disciplina
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    public ICollection<Nota> Notas { get; set; } = new List<Nota>();
}
