using ManualAction.DataAccessLayer.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManualAction.DataAccessLayer
{
    public interface IUnitOfWork : IDisposable
    {
        IActionHistoryRepository ActionHistoryRepository { get; }
        IDepartmantRepository DepartmantRepository { get; }
        IManualActionReasonRepository ManualActionReasonRepository { get; }
        IManualActionRepository ManualActionRepository { get; }
        IStatusRepository StatusRepository { get; }
        IUserListRepository UserListRepository { get; }
        int Complete();
    }
}
