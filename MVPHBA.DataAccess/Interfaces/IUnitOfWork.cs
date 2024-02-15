using MVPHBA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPHBA.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        Task<EnumData.DBAttempt> BeginTransaction();
        Task<EnumData.DBAttempt> Commit();
        Task<EnumData.DBAttempt> CommitTransaction();
        Task<EnumData.DBAttempt> Rollback();
    }
}
