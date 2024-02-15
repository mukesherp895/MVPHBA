using MVPHBA.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPHBA.DataAccess.Infrastructures
{
    public class DBFactory : IDBFactory
    {
        private readonly MVPHBADBContext _dbContext;
        public DBFactory(MVPHBADBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public MVPHBADBContext GetDbContext()
        {
            return _dbContext;
        }
    }
}
