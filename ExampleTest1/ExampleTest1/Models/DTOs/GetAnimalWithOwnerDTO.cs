using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace ExampleTest1.Models.DTOs;

public class GetAnimalWithOwnerDTO
{
    [Required]
    public int ID { get; set; }
    [MaxLength(100)]
    public String Name { get; set; } = String.Empty;
    public DateTime AdmissionDate { get; set; }
    public int OwnerID { get; set; }
    public int AnimalClassID { get; set; }
    public List<OwnerDTO> owners { get; set; } = null!;
    public string animalClass { get; set; } = string.Empty;
}