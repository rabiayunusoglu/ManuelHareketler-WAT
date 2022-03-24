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
    public class ManualActionReasonManager : IManualActionReasonManager
    {
        private UnitOfWork _unitOfWork;
        public ManualActionReasonManager()
        {
            _unitOfWork = new UnitOfWork(new ManualActionsDBEntities());


        }
        public ManualActionReasonDTO CreateManager(ManualActionReasonDTO manager)
        {
            if (manager == null)
            {
                return null;
            }
            ManualActionReason value = new ManualActionReason();
            value.reasonID = System.Guid.NewGuid();
            value.reasonName = manager.reasonName;
            
            ManualActionReason recordValue = _unitOfWork.ManualActionReasonRepository.Add(value);

            ManualActionReasonDTO returnValue = new ManualActionReasonDTO()
            {
                reasonID = recordValue.reasonID,
                reasonName = recordValue.reasonName,

            };
            if (_unitOfWork.Complete() > 0)
                return returnValue;
            return null;
        }

        public bool DeleteManager(Guid id)
        {
            if (id == null)
                return false;
            if (_unitOfWork.ManualActionReasonRepository.Remove(id))
            {
                if (_unitOfWork.Complete() > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public List<ManualActionReasonDTO> GetAllManager()
        {
            List<ManualActionReason> managerList = _unitOfWork.ManualActionReasonRepository.GetAll().OrderBy(x=>x.reasonName).ToList();
            List<ManualActionReasonDTO> list = new List<ManualActionReasonDTO>();
            if (managerList == null)
            {
                return null;
            }
            foreach (ManualActionReason recordValue in managerList)
            {
                ManualActionReasonDTO returnValue = new ManualActionReasonDTO()
                {
                    reasonID = recordValue.reasonID,
                    reasonName = recordValue.reasonName,

                };
                list.Add(returnValue);

            }

            return list;
        }

        public ManualActionReasonDTO GetManagerById(Guid id)
        {
            if (id == null)
                return null;
            ManualActionReason recordValue = _unitOfWork.ManualActionReasonRepository.GetById(id);
            if (recordValue == null)
                return null;
            ManualActionReasonDTO returnValue = new ManualActionReasonDTO()
            {
                reasonID = recordValue.reasonID,
                reasonName = recordValue.reasonName,
            };
            return returnValue;
        }

        public ManualActionReasonDTO UpdateManager(ManualActionReasonDTO manager)
        {
            if (manager == null)
            {
                return null;
            }
            ManualActionReason value = new ManualActionReason();
            value.reasonID = manager.reasonID;
            value.reasonName = manager.reasonName;

            ManualActionReason recordValue = _unitOfWork.ManualActionReasonRepository.Update(value);

            ManualActionReasonDTO returnValue = new ManualActionReasonDTO()
            {
                reasonID = recordValue.reasonID,
                reasonName = recordValue.reasonName,

            };
            if (_unitOfWork.Complete() > 0)
                return returnValue;
            return null;
        }
    }
}
