using FormulaOne.Entities.DbSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.DataService.Repository.Interfaces
{
    public interface IAchievementRepo : IGenericRepo<Achievement, Guid>
    {
        Task<Achievement?> GetDriverAchievementsAsync(Guid driverId);

        Task<Achievement?> GetById(Guid id);
    }
}
