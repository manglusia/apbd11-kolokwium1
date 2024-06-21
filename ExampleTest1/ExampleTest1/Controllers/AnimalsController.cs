using System.Transactions;
using ExampleTest1.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ExampleTest1.Models.DTOs;

namespace ExampleTest1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalRepository _animalsRepository;
        public AnimalsController(IAnimalRepository animalsRepository)
        {
            _animalsRepository = animalsRepository;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSmth(int id)
        {
            if (!await _animalsRepository.doesAnimalExists(id))
            {
                return NotFound();
            }

            var animals = await _animalsRepository.GetAnimalWithOwner(id);
            return Ok(animals);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddSmth()
        {
            
            return Created();
        }
        
        
    }
}
