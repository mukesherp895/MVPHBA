using Microsoft.EntityFrameworkCore;
using MVPHBA.Common;
using MVPHBA.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVPHBA.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MVPHBADBContext _dbContext;
        public Repository(IDBFactory dBFactory)
        {
            _dbContext = dBFactory.GetDbContext();
        }
        public EnumData.DBAttempt Create(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return EnumData.DBAttempt.Success;
        }
        public EnumData.DBAttempt Update(T entity)
        {
            if (_dbContext.Entry(entity).State != EntityState.Detached)
            {
                _dbContext.Entry(entity).State = EntityState.Detached;
            }
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return EnumData.DBAttempt.Success;
        }
        public EnumData.DBAttempt DeleteById(object id)
        {
            _dbContext.Set<T>().Remove(_dbContext.Set<T>().Find(id));
            return EnumData.DBAttempt.Success;
        }
        public EnumData.DBAttempt DeleteObject(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return EnumData.DBAttempt.Success;
        }
        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>().AsQueryable();
        }
        public T GetById(object id)
        {
            return _dbContext.Set<T>().Find(id);
        }
        public IQueryable<T> Search(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate).AsQueryable();
        }
    }
}
