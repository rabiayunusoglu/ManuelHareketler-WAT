using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManualAction.DataAccessLayer.Repositories.Abstract
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(System.Guid id);
        TEntity GetByIdSpecific(int id);
        IEnumerable<TEntity> GetAll();
        TEntity Add(TEntity entity);
        bool Remove(System.Guid id);
        TEntity Update(TEntity entity);
    }
}
