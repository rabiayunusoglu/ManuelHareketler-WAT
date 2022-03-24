using ManualAction.BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ManualAction.BusinessLayer.IManagers
{
    public interface IActionHistoryManager
    {
        List<ActionHistoryDTO> GetAllManager();
        ActionHistoryDTO GetManagerById(System.Guid id);
        ActionHistoryDTO CreateManager(ActionHistoryDTO manager);
        ActionHistoryDTO UpdateManager(ActionHistoryDTO manager);
        bool DeleteManager(System.Guid id);
    }
}
