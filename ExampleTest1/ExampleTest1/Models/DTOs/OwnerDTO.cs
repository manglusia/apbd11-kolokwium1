using System.ComponentModel.DataAnnotations;

namespace ExampleTest1.Models.DTOs;

public class OwnerDTO
{
    [Required]
    public int ID { get; set; }
    [MaxLength(100)]
    public String FirstName { get; set; } = String.Empty;
    [MaxLength(100)]
    public String LastName { get; set; } = string.Empty;
    
}