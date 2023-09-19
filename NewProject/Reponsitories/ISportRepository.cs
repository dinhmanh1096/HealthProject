using NewProject.Models;

namespace NewProject.Reponsitories
{
    public interface ISportRepository
    {
        public Task<List<SportModel>> GetAllSportsAsync();
        public Task<SportModel> GetSportsAsync(int sportId);
        public Task<int> AddSportAsync(RequestSportModel model);
        public Task DeleteSportAsync(int sportId);
        public Task UpdateSportAsync(int sportId, SportModel model);
        
    }
}
