using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManualAction.BusinessLayer.DTO
{
    public class UserListDTO
    {
        public string registerNo { get; set; }
        public string userType { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int departmantType { get; set; }
    }
}
