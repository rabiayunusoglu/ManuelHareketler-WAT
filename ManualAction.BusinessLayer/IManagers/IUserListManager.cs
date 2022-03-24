using ManualAction.BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManualAction.BusinessLayer.IManagers
{
    public interface IUserListManager
    {
        List<UserListDTO> GetAllManager();
        UserListDTO GetManagerById(String id);
    }
}
