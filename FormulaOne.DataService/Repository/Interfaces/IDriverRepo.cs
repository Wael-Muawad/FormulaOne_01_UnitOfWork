using FormulaOne.Entities.DbSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.DataService.Repository.Interfaces
{
    public interface IDriverRepo : IGenericRepo<Driver, Guid>
    {

        Task<Driver?> GetDriverById(Guid id);

    }
}
