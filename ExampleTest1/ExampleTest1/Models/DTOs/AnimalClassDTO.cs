using System.ComponentModel.DataAnnotations;

namespace ExampleTest1.Models.DTOs;

public class AnimalClassDTO
{
    [Required]
    public int ID { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = String.Empty;
}