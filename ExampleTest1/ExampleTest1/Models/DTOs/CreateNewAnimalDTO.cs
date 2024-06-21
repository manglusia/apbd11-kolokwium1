using System.ComponentModel.DataAnnotations;

namespace ExampleTest1.Models.DTOs;

public class CreateNewAnimalDTO
{
    [Required]
    public int ID { get; set; }
    [MaxLength(100)]
    public String Name { get; set; } = String.Empty;
    public DateTime AdmissionDate { get; set; }
    public int OwnerID { get; set; }
    public string animalClassName { get; set; } = string.Empty;
    
    public IEnumerable<Procedure> Procedures { get; set; } = new List<Procedure>();
}