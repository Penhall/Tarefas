using System.ComponentModel.DataAnnotations;
namespace Tarefas.Data;

public class Tarefa
{     
    public int Id { get; set; }
    [Required]
    public string? Titulo { get; set; }
    public bool IsCompleta { get; set; }
}
