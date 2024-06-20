using System.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExampleTest1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        // private readonly IAnimalsRepository _animalsRepository;
        // public AnimalsController(IAnimalsRepository animalsRepository)
        // {
        //     _animalsRepository = animalsRepository;
        // }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSmth(int id)
        {
            return Ok();
        }
        
        // Version with implicit transaction
        [HttpPost]
        public async Task<IActionResult> AddSmth()
        {
            return Created();
        }
        
        // Version with transaction scope
        [HttpPost]
        [Route("with-scope")]
        public async Task<IActionResult> AddSmthV2()
        {
            return Created();
        }
    }
}
