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
    public class ActionHistoryManager : IActionHistoryManager
    {
        private UnitOfWork _unitOfWork;
        public ActionHistoryManager()
        {
            _unitOfWork = new UnitOfWork(new ManualActionsDBEntities());


        }
        public ActionHistoryDTO CreateManager(ActionHistoryDTO manager)
        {
            if (manager == null)
            {
                return null;
            }
            ActionHistory value = new ActionHistory();
            value.historyID = System.Guid.NewGuid();
            value.manualActionID = manager.manualActionID;
            value.senderUser = manager.senderUser;
            value.senderTeam = manager.senderTeam;
            value.receiverUser = manager.receiverUser;
            value.receiverTeam = manager.receiverTeam;
            value.redirectedText = manager.redirectedText;
            value.redirectedDate = Convert.ToDateTime(manager.redirectedDate);
            value.registerDate = manager.registerDate;
            ActionHistory recordValue = _unitOfWork.ActionHistoryRepository.Add(value);

            ActionHistoryDTO returnValue = new ActionHistoryDTO()
            {
                historyID = recordValue.historyID,
                manualActionID = recordValue.manualActionID,
                senderUser = recordValue.senderUser,
                senderTeam = recordValue.senderTeam,
                receiverUser = recordValue.receiverUser,
                receiverTeam = recordValue.receiverTeam,
                redirectedText = recordValue.redirectedText,
                redirectedDate = recordValue.redirectedDate.ToString(),
                registerDate = recordValue.registerDate,
            };
            if (_unitOfWork.Complete() > 0)
                return returnValue;
            return null;
        }

        public bool DeleteManager(Guid id)
        {
            if (id == null)
                return false;
            if (_unitOfWork.ActionHistoryRepository.Remove(id))
            {
                if (_unitOfWork.Complete() > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public List<ActionHistoryDTO> GetAllManager()
        {
            List<ActionHistory> managerList = _unitOfWork.ActionHistoryRepository.GetAll().ToList();
            List<ActionHistoryDTO> list = new List<ActionHistoryDTO>();
            if (managerList == null)
            {
                return null;
            }
            foreach (ActionHistory recordValue in managerList)
            {
                ActionHistoryDTO returnValue = new ActionHistoryDTO()
                {
                    historyID = recordValue.historyID,
                    manualActionID = recordValue.manualActionID,
                    senderUser = recordValue.senderUser,
                    senderTeam = recordValue.senderTeam,
                    receiverUser = recordValue.receiverUser,
                    receiverTeam = recordValue.receiverTeam,
                    redirectedText = recordValue.redirectedText,
                    redirectedDate = recordValue.redirectedDate.ToString(),
                    registerDate = recordValue.registerDate,
                };
                list.Add(returnValue);

            }

            return list;
        }
        public List<ActionHistoryDTO> GetAllManagerByManualID(string id)
        {
            List<ActionHistory> managerList = _unitOfWork.ActionHistoryRepository.GetAll().OrderBy(t => t.redirectedDate).ToList();
            List<ActionHistoryDTO> list = new List<ActionHistoryDTO>();
            if (managerList == null)
            {
                return null;
            }
            managerList = managerList.Where(x => x.manualActionID == id).ToList();
            foreach (ActionHistory recordValue in managerList)
            {
                ActionHistoryDTO returnValue = new ActionHistoryDTO()
                {
                    historyID = recordValue.historyID,
                    manualActionID = recordValue.manualActionID,
                    senderUser = recordValue.senderUser,
                    senderTeam = recordValue.senderTeam,
                    receiverUser = recordValue.receiverUser,
                    receiverTeam = recordValue.receiverTeam,
                    redirectedText = recordValue.redirectedText,
                    redirectedDate = recordValue.redirectedDate.ToString(),
                    registerDate = recordValue.registerDate,
                };
                list.Add(returnValue);

            }

            return list;
        }

        public ActionHistoryDTO GetManagerById(Guid id)
        {
            if (id == null)
                return null;
            ActionHistory recordValue = _unitOfWork.ActionHistoryRepository.GetById(id);
            if (recordValue == null)
                return null;
            ActionHistoryDTO returnValue = new ActionHistoryDTO()
            {
                historyID = recordValue.historyID,
                manualActionID = recordValue.manualActionID,
                senderUser = recordValue.senderUser,
                senderTeam = recordValue.senderTeam,
                receiverUser = recordValue.receiverUser,
                receiverTeam = recordValue.receiverTeam,
                redirectedText = recordValue.redirectedText,
                redirectedDate = recordValue.redirectedDate.ToString(),
                registerDate = recordValue.registerDate,
            };
            return returnValue;
        }

        public ActionHistoryDTO UpdateManager(ActionHistoryDTO manager)
        {
            if (manager == null)
            {
                return null;
            }
            ActionHistory value = new ActionHistory();
            value.historyID = manager.historyID;
            value.manualActionID = manager.manualActionID;
            value.senderUser = manager.senderUser;
            value.senderTeam = manager.senderTeam;
            value.receiverUser = manager.receiverUser;
            value.receiverTeam = manager.receiverTeam;
            value.redirectedText = manager.redirectedText;
            value.redirectedDate = Convert.ToDateTime(manager.redirectedDate);
            value.registerDate = manager.registerDate;
            ActionHistory recordValue = _unitOfWork.ActionHistoryRepository.Update(value);

            ActionHistoryDTO returnValue = new ActionHistoryDTO()
            {
                historyID = recordValue.historyID,
                manualActionID = recordValue.manualActionID,
                senderUser = recordValue.senderUser,
                senderTeam = recordValue.senderTeam,
                receiverUser = recordValue.receiverUser,
                receiverTeam = recordValue.receiverTeam,
                redirectedText = recordValue.redirectedText,
                redirectedDate = recordValue.redirectedDate.ToString(),
                registerDate = recordValue.registerDate,
            };
            if (_unitOfWork.Complete() > 0)
                return returnValue;
            return null;
        }
    }
}
