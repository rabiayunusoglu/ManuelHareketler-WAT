using ManualAction.BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManualAction.BusinessLayer.IManagers
{
    public interface IManualActionReasonManager
    {
        List<ManualActionReasonDTO> GetAllManager();
        ManualActionReasonDTO GetManagerById(System.Guid id);
        ManualActionReasonDTO CreateManager(ManualActionReasonDTO manager);
        ManualActionReasonDTO UpdateManager(ManualActionReasonDTO manager);
        bool DeleteManager(System.Guid id);
    }
}
