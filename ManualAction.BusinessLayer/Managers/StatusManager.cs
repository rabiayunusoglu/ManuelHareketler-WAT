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
    public class StatusManager : IStatusManager
    {
        private UnitOfWork _unitOfWork;
        public StatusManager()
        {
            _unitOfWork = new UnitOfWork(new ManualActionsDBEntities());


        }
        public List<StatusDTO> GetAllManager()
        {
            List<Status> managerList = _unitOfWork.StatusRepository.GetAll().ToList();
            List<StatusDTO> list = new List<StatusDTO>();
            if (managerList == null)
            {
                return null;
            }
            foreach (Status recordValue in managerList)
            {
                StatusDTO returnValue = new StatusDTO()
                {
                    statusType = recordValue.statusType,
                    statusName = recordValue.statusName,

                };
                list.Add(returnValue);

            }

            return list;
        }

        public StatusDTO GetManagerById(int id)
        {
            if (id == null)
                return null;
            Status recordValue = _unitOfWork.StatusRepository.GetByIdSpecific(id);
            if (recordValue == null)
                return null;
            StatusDTO returnValue = new StatusDTO()
            {
                statusType = recordValue.statusType,
                statusName = recordValue.statusName,
            };
            return returnValue;
        }
    }
}
