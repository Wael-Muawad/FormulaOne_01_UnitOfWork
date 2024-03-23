using FormulaOne.DataService.Data;
using FormulaOne.DataService.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.DataService.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposedValue;
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        public IDriverRepo DriverRepo { get; private set; }
        public IAchievementRepo AchievementRepo { get; private set; }


        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            //_logger = logger;
            DriverRepo = new DriverRepo(_context);
            AchievementRepo = new AchievementRepo(_context);
        }



        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null

                _disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UnitOfWork()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
