using MVPHBA.Common;
using MVPHBA.DataAccess;
using MVPHBA.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPHBA.DataAccess.Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MVPHBADBContext _dbContext;
        public UnitOfWork(MVPHBADBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<EnumData.DBAttempt> BeginTransaction()
        {
            await _dbContext.Database.BeginTransactionAsync();
            return EnumData.DBAttempt.Success;
        }
        public async Task<EnumData.DBAttempt> Commit()
        {
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return EnumData.DBAttempt.Success;
            }
            return EnumData.DBAttempt.Fail;
        }
        public async Task<EnumData.DBAttempt> CommitTransaction()
        {
            if (await Commit() == EnumData.DBAttempt.Success)
            {
                await _dbContext.Database.CurrentTransaction.CommitAsync();
                return EnumData.DBAttempt.Success;
            }
            return EnumData.DBAttempt.Fail;
        }
        public async Task<EnumData.DBAttempt> Rollback()
        {
            await _dbContext.Database.CurrentTransaction.RollbackAsync();
            return EnumData.DBAttempt.Success;
        }
    }
}
