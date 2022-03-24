using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using ManualAction.DataAccessLayer.Repositories.Abstract;
namespace ManualAction.DataAccessLayer.Repositories.Concrete
{
    public class Repository<E> : IRepository<E> where E : class
    {
        protected DbContext _context;
        private DbSet<E> _dbSet;
        public Repository(DbContext dbContext)
        {
            _context = dbContext;
            _dbSet = _context.Set<E>();
        }

        public E Add(E entity) => _dbSet.Add(entity);

        public IEnumerable<E> GetAll()
        {
            return _dbSet.ToList();
        }

        public E GetById(System.Guid id)
        {
            E value = _dbSet.Find(id);
            if (value == null)
            {
                return null;
            }
            return value;
        }

        public E GetByIdSpecific(int id)
        {
            E value = _dbSet.Find(id);
            if (value == null)
            {
                return null;
            }
            return value;
        }

        public bool Remove(System.Guid id)
        {
            E deleted = GetById(id);
            if (deleted != null)
            {
                _dbSet.Remove(deleted);
                return true;
            }
            return false;
        }
        public E Update(E entity)
        {
            if (entity == null)
            {
                return null;
            }
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            return entity;
        }
    }
}
