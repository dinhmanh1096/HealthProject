using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewProject.Data;
using NewProject.Models;

namespace NewProject.Reponsitories
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public WorkoutRepository(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }  
        public async Task<int> AddWorkoutAsync(RequestWorkoutModel model)
        {
            var newWorkout = _mapper.Map<Workout>(model);
            _context.Workouts.Add(newWorkout);
            await _context.SaveChangesAsync();
            return newWorkout.WorkoutID;
        }
        public async Task DeleteWorkoutAsync(int workoutID)
        {
            var deleteWorkout = _context.Workouts.SingleOrDefault(r => r.WorkoutID == workoutID);
            if (deleteWorkout != null)
            {
                _context.Workouts.Remove(deleteWorkout);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<WorkoutModel>> GetAllWorkoutAsync()
        {
            var workouts = await _context.Workouts.ToListAsync();
            return _mapper.Map<List<WorkoutModel>>(workouts);
        }

        public async Task<WorkoutModel> GetWorkoutAsync(int workoutID)
        {
            var workout = await _context.Workouts.FindAsync(workoutID);
            return _mapper.Map<WorkoutModel>(workout);
        }

        public async Task UpdateWorkoutAsync(int workoutID, WorkoutModel model)
        {
            if (workoutID == model.WorkoutID)
            {
                var updateWorkout = _mapper.Map<Workout>(model);
                _context.Workouts!.Update(updateWorkout);
                await _context.SaveChangesAsync();
            }
        }
    }
}
