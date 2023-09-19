using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewProject.Models;
using NewProject.Reponsitories;

namespace NewProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutRepository _WorkoutRepo;

        public WorkoutController(IWorkoutRepository repo)
        {
            _WorkoutRepo = repo;
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<IActionResult> GetAllWorkout()
        { 
           return Ok(await _WorkoutRepo.GetAllWorkoutAsync());

        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("{workoutID}")]
        public async Task<IActionResult> GetWorkoutByID(int workoutID)
        {
            var workout = await _WorkoutRepo.GetWorkoutAsync(workoutID);
            return workout == null ? NotFound() : Ok(workout);
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        public async Task<IActionResult> AddNewWorkout(RequestWorkoutModel model)
        {
            var newWorkoutID = await _WorkoutRepo.AddWorkoutAsync(model);
            var workout = await _WorkoutRepo.GetWorkoutAsync(newWorkoutID);
            return workout == null ? BadRequest() : Ok(workout);

        }

        [Authorize(Roles = "Admin,User")]
        [HttpPut("{workoutID}")]
        public async Task<IActionResult> UpdateWorkout(int workoutID, [FromBody] WorkoutModel model)
        {
            if (workoutID != model.WorkoutID)
            {
                return NotFound();
            }
            await _WorkoutRepo.UpdateWorkoutAsync(workoutID, model);
            return Ok();
        }

        [Authorize(Roles = "Admin,User")]
        [HttpDelete("{workoutID}")]
        public async Task<IActionResult> DeleteWorkout(int workoutID)
        {
            var workout = await _WorkoutRepo.GetWorkoutAsync(workoutID);
            if (workout == null)
                return NotFound();
            await _WorkoutRepo.DeleteWorkoutAsync(workoutID);
            return Ok();
        }
    }
}
