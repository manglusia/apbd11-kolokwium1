using ExampleTest1.Models.DTOs;

namespace ExampleTest1.Repositories;

public interface IAnimalRepository
{
    Task<bool> doesAnimalExists(int id);
    Task<GetAnimalWithOwnerDTO> GetAnimalWithOwner(int id);
    Task<AnimalClassDTO> GetAnimalClass(int id);
    Task<List<OwnerDTO>> getOwners(int id);
    Task<GetAnimalWithOwnerDTO> createNewAnimal(CreateNewAnimalDTO createNewAnimal);
}