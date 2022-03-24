using ManualAction.BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManualAction.BusinessLayer.IManagers
{
   public interface IDepartmantManager
    {
        List<DepartmantDTO> GetAllManager();
        DepartmantDTO GetManagerById(int id);
       
    }
}
