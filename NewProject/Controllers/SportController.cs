using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewProject.Models;
using NewProject.Reponsitories;

namespace NewProject.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class SportController : ControllerBase
    {
        private readonly ISportRepository _sportRepo;

        public SportController(ISportRepository repo)
        {
            _sportRepo = repo;
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<IActionResult> GetAllSport()
        {
            return Ok(await _sportRepo.GetAllSportsAsync());

        }

        [HttpGet("{sportId}")]
        public async Task<IActionResult> GetSportByID(int sportId)
        {
            var sport = await _sportRepo.GetSportsAsync(sportId);
            return sport == null ? NotFound() : Ok(sport);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddNewSport([FromBody] RequestSportModel model)
        {

            var newSportId = await _sportRepo.AddSportAsync(model);
            var sport = await _sportRepo.GetSportsAsync(newSportId);
            return sport == null ? BadRequest() : Ok(sport);

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{sportId}")]
        public async Task<IActionResult> UpdateSport(int sportId, [FromBody] SportModel model)
        {
            if (sportId != model.SportID)
            {
                return NotFound();
            }
            await _sportRepo.UpdateSportAsync(sportId, model);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{sportId}")]
        public async Task<IActionResult> DeleteSport([FromRoute] int sportId)
        {
            var sport = await _sportRepo.GetSportsAsync(sportId);
            if (sport == null)
                return NotFound();
            await _sportRepo.DeleteSportAsync(sportId);
            return Ok();
        }
    }
}
