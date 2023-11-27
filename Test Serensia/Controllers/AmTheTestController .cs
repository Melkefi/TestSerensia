using Microsoft.AspNetCore.Mvc;
using Test_Serensia.Dto;
using Test_Serensia.Interfaces;

namespace Test_Serensia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmTheTestController : ControllerBase
    {
        private readonly IAmTheTestServices _amTheTestService;

        public AmTheTestController(IAmTheTestServices amTheTestService)
        {
            _amTheTestService = amTheTestService;
        }

        [HttpPost]
        public async Task<IActionResult> Get([FromBody] TestRequest testRequest)
        {
            if (!testRequest.Choices.Any())
            {
                return BadRequest($"Parameter {nameof(testRequest.Choices)} is empty.");
            }

            try
            {
                var result = await _amTheTestService.GetSuggestionAsync(testRequest);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}