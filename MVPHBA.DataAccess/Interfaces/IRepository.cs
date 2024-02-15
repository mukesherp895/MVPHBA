using MVPHBA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVPHBA.DataAccess.Interfaces
{
    public interface IRepository<T>
    {
        EnumData.DBAttempt Create(T entity);
        EnumData.DBAttempt Update(T entity);
        EnumData.DBAttempt DeleteById(object id);
        EnumData.DBAttempt DeleteObject(T entity);
        IQueryable<T> GetAll();
        T GetById(object id);
        IQueryable<T> Search(Expression<Func<T, bool>> predicate);
    }
}
