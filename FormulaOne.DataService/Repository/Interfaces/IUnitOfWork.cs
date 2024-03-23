using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.DataService.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IDriverRepo DriverRepo { get; }
        public IAchievementRepo AchievementRepo { get; }


        Task<bool> SaveChangesAsync();
    }
}
