using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPHBA.DataAccess.Interfaces
{
    public interface IDBFactory
    {
        MVPHBADBContext GetDbContext();
    }
}
