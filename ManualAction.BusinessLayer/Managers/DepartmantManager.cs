using System.Collections.Generic;
using ManualAction.BusinessLayer.DTO;
using System.Linq;

using ManualAction.BusinessLayer.IManagers;
using ManualAction.DataAccessLayer;
using System.Collections;
using System;
using System.IO;

namespace ManualAction.BusinessLayer.Managers
{
    public class DepartmantManager : IDepartmantManager
    {
        private UnitOfWork _unitOfWork;
        public DepartmantManager()
        {
            _unitOfWork = new UnitOfWork(new ManualActionsDBEntities());


        }
        public List<DepartmantDTO> GetAllManager()
        {
            List<Department> managerList = _unitOfWork.DepartmantRepository.GetAll().ToList();
            List<DepartmantDTO> list = new List<DepartmantDTO>();
            if (managerList == null)
            {
                return null;
            }
            foreach (Department recordValue in managerList)
            {
                DepartmantDTO returnValue = new DepartmantDTO()
                {
                    departmentType = recordValue.departmentType,
                    departmentName = recordValue.departmentName,
                    
                };
                list.Add(returnValue);

            }

            return list;
        }

        public DepartmantDTO GetManagerById(int id)
        {
            if (id == null)
                return null;
            DepartmantDTO recordValue = GetAllManager().Where(x => x.departmentType == id).FirstOrDefault();
            if (recordValue == null)
                return null;
            return recordValue;
        }
    }
}
