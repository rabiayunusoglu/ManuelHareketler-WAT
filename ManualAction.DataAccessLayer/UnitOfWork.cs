
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManualAction.DataAccessLayer.Repositories.Abstract;
using ManualAction.DataAccessLayer.Repositories.Concrete;
namespace ManualAction.DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private ManualActionsDBEntities _context;
        public UnitOfWork(ManualActionsDBEntities context)
        {
            _context = context;
            ActionHistoryRepository = new ActionHistoryRepository(_context);
            DepartmantRepository = new DepartmantRepository(_context);
            ManualActionReasonRepository = new ManualActionReasonRepository(_context);
            ManualActionRepository = new ManualActionRepository(_context);
            StatusRepository = new StatusRepository(_context);
            UserListRepository = new UserListRepository(_context);
        }

        public IActionHistoryRepository ActionHistoryRepository { get; private set; }

        public IDepartmantRepository DepartmantRepository{ get; private set; }

        public IManualActionReasonRepository ManualActionReasonRepository{ get; private set; }

        public IManualActionRepository ManualActionRepository{ get; private set; }

        public IStatusRepository StatusRepository{ get; private set; }

        public IUserListRepository UserListRepository{ get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
