using FormulaOne.DataService.Data;
using FormulaOne.DataService.Repository.Interfaces;
using FormulaOne.Entities.DbSet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.DataService.Repository.Implementations
{
    public class AchievementRepo : GenericRepo<Achievement, Guid>, IAchievementRepo
    {
        public AchievementRepo(AppDbContext context) : base(context)
        {
        }

        public async Task<Achievement?> GetDriverAchievementsAsync(Guid driverId)
        {
            var result = await base.GetByCondition(a => a.DriverId == driverId);
            return result.FirstOrDefault();
        }

        public override async Task<List<Achievement>> GetAll()
        {
            var result = await _dbSet.AsNoTracking()
                               .Where(a => a.Status == 1)
                               .AsSplitQuery()
                               .OrderBy(a => a.AddedAt)
                               .ToListAsync();
            return result;
        }

        public async Task<Achievement?> GetById(Guid id)
        {
            var result = await _dbSet.AsNoTracking()
                               .Where(a => a.Id == id && a.Status == 1)
                               .FirstOrDefaultAsync();
            return result;
        }

        public override async Task<bool> Update(Achievement entity)
        {
            var achievement = await _dbSet.FindAsync(entity.Id);
            if (achievement is null)
                return false;

            achievement.UpdatedAt = DateTime.UtcNow;
            achievement.PolePosition = entity.PolePosition;
            achievement.FastestLap = entity.FastestLap;
            achievement.RaceWin = entity.RaceWin;
            achievement.WorldChampionship = entity.WorldChampionship;


            return true;
        }

        public async Task<bool> SoftDelete(Guid id)
        {
            var achievement = await _dbSet.FindAsync(id);
            if (achievement is null)
                return false;

            achievement.Status = 0;
            achievement.UpdatedAt = DateTime.UtcNow;

            return true;
        }

        public override async Task<bool> Delete(Guid id)
        {
            var achievement = await _dbSet.FindAsync(id);
            if (achievement is null)
                return false;

            _dbSet.Remove(achievement);
            return true;
        }

        public async Task<Achievement?> GetAchievementById(Guid id)
        {
            var achievement = await base.GetByCondition(d => d.Id == id);
            return achievement.FirstOrDefault();
        }
    }
}
