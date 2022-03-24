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
    public class UserListManager : IUserListManager
    {
        private UnitOfWork _unitOfWork;
        public UserListManager()
        {
            _unitOfWork = new UnitOfWork(new ManualActionsDBEntities());


        }

        public List<UserListDTO> GetAllManager()
        {
            List<UserList> managerList = _unitOfWork.UserListRepository.GetAll().ToList();
            List<UserListDTO> list = new List<UserListDTO>();
            if (managerList == null)
            {
                return null;
            }
            foreach (UserList recordValue in managerList)
            {
                UserListDTO returnValue = new UserListDTO()
                {
                    registerNo = recordValue.registerNo,
                    userType=recordValue.userType,
                    username = recordValue.username,
                    password = recordValue.password,
                    departmantType = recordValue.departmantType,
                };
                list.Add(returnValue);

            }

            return list;
        }

        public UserListDTO GetManagerById(String id)
        {
            if (id == null)
                return null;
            List<UserList> managerList = _unitOfWork.UserListRepository.GetAll().ToList();
            UserList recordValue = managerList.FirstOrDefault(x=>x.registerNo==id);
            if (recordValue == null)
                return null;
            UserListDTO returnValue = new UserListDTO()
            {
                registerNo = recordValue.registerNo,
                userType=recordValue.userType,
                username = recordValue.username,
                password = recordValue.password,
                departmantType = recordValue.departmantType,
            };
            return returnValue;
        }
    }
}
