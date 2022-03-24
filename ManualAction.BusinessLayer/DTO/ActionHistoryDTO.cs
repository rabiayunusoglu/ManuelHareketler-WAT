using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManualAction.BusinessLayer.DTO
{
    public class ActionHistoryDTO
    {
        public System.Guid historyID { get; set; }
        public string manualActionID { get; set; }
        public string senderUser { get; set; }
        public string senderTeam { get; set; }
        public string receiverUser { get; set; }
        public string receiverTeam { get; set; }
        public string redirectedText { get; set; }
        public string redirectedDate { get; set; }
    }
}
