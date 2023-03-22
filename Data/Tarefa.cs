using System.ComponentModel.DataAnnotations;

public class Tarefa
{     
    public int Id { get; set; }
    [Required]
    public string? Titulo { get; set; }
    public bool IsCompleta { get; set; }
}
