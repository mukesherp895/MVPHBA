using MVPHBA.Common;
using MVPHBA.DataAccess.Interfaces;
using MVPHBA.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVPHBA.Service
{
    public class Service<T> : IService<T>
    {
        private readonly IRepository<T> _repository;
        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }
        public EnumData.DBAttempt Create(T entity)
        {
            return _repository.Create(entity);
        }
        public EnumData.DBAttempt DeleteById(object id)
        {
            return _repository.DeleteById(id);
        }
        public EnumData.DBAttempt DeleteObject(T entity)
        {
            return _repository.DeleteObject(entity);
        }
        public IQueryable<T> GetAll()
        {
            return _repository.GetAll();
        }
        public T GetById(object id)
        {
            return _repository.GetById(id);
        }
        public IQueryable<T> Search(Expression<Func<T, bool>> predicate)
        {
            return _repository.Search(predicate);
        }
        public EnumData.DBAttempt Update(T entity)
        {
            return _repository.Update(entity);
        }
    }
}
