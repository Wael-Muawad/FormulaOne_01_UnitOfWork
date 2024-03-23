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
    public class DriverRepo : GenericRepo<Driver, Guid>, IDriverRepo
    {
        public DriverRepo(AppDbContext context) : base(context)
        {
        }

        public override async Task<List<Driver>> GetAll()
        {
            var result = await _dbSet.AsNoTracking()
                               .Where(d => d.Status == 1)
                               .AsSplitQuery()
                               .OrderBy(d => d.AddedAt)
                               .ToListAsync();
            return result;
        }

        public override async Task<bool> Update(Driver entity)
        {
            var driver = await _dbSet.FindAsync(entity.Id);
            if (driver is null)
                return false;

            driver.UpdatedAt = DateTime.UtcNow;
            driver.DriverNumber = entity.DriverNumber;
            driver.FirstName = entity.FirstName;
            driver.LastName = entity.LastName;
            driver.DateOfBirth = entity.DateOfBirth;

            return true;
        }

        public async Task<bool> SoftDelete(Guid id)
        {
            var driver = await _dbSet.FindAsync(id);
            if (driver is null)
                return false;

            driver.Status = 0;
            driver.UpdatedAt = DateTime.UtcNow;

            return true;
        }

        public override async Task<bool> Delete(Guid id)
        {
            var driver = await _dbSet.FindAsync(id);
            if (driver is null)
                return false;

            _dbSet.Remove(driver);
            return true;
        }

        public async Task<Driver?> GetDriverById(Guid id)
        {
            var driver = await base.GetByCondition(d => d.Id == id && d.Status == 1);
            return driver.FirstOrDefault();
        }
    }
}
