using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewProject.Data;
using NewProject.Models;

namespace NewProject.Reponsitories
{
    public class SportRepository : ISportRepository
    {
        
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SportRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> AddSportAsync(RequestSportModel model)
        {
            var newSport = _mapper.Map<Sport>(model);
            _context.Sports.Add(newSport);
            await _context.SaveChangesAsync();
            return newSport.SportID;
        }

        public async Task DeleteSportAsync(int sportId)
        {
            var deleteSport = _context.Sports.SingleOrDefault(s => s.SportID == sportId);
            if (deleteSport != null)
            {
                _context.Sports.Remove(deleteSport);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<SportModel>> GetAllSportsAsync()
        {
            var sports = await _context.Sports.ToListAsync();
            return _mapper.Map<List<SportModel>>(sports);
        }

        public async Task<SportModel> GetSportsAsync(int sportId)
        {
            var sport = await _context.Sports!.FindAsync(sportId);
            return _mapper.Map<SportModel>(sport);
        }

        public async Task UpdateSportAsync(int sportId, SportModel sport)
        {
            if (sportId == sport.SportID)
            {
                var updateSport = _mapper.Map<Sport>(sport);
                _context.Sports.Update(updateSport);
                await _context.SaveChangesAsync();
            }
        }

        public List<SportModel> GetAll(string search)
        {
            var allProducts = _context.Sports.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                allProducts = allProducts.Where(s => s.SportName.Contains(search));
            }

            var result = allProducts.Select(s => new SportModel
            {
                SportID = s.SportID,
                SportName = s.SportName
            });
            return result.ToList();

        }
         
    }
}
