using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManualAction.BusinessLayer.DTO
{
    public class ManualActionDTO
    {
        public System.Guid manualActionID { get; set; }
        public string UY { get; set; }
        public string HTU { get; set; }
        public string material { get; set; }
        public string materialText { get; set; }
        public string MT { get; set; }
        public string amount { get; set; }
        public string brm { get; set; }
        public string priceBrm { get; set; }
        public string total { get; set; }
        public string DnmsMF { get; set; }
        public Nullable<decimal> DnmsMFValue { get; set; }
        public string orderValue { get; set; }
        public string productCode { get; set; }
        public string productCodeInfo { get; set; }
        public string productYear { get; set; }
        public string registerDate { get; set; }
        public string sortingDate { get; set; }
        public string loginDate { get; set; }
        public string loginDateTime { get; set; }
        public string userRegisterNo { get; set; }
        public string username { get; set; }
        public string actionReason { get; set; }
        public string reasonDetail { get; set; }
        public Nullable<System.DateTime> madeDate { get; set; }
        public string madeDateText { get; set; }
        public Nullable<int> statusType { get; set; }
        public string statusName { get; set; }
        public Nullable<bool> isRedirected { get; set; }
        public string historyID { get; set; }
        public string redirectedTeam { get; set; }
        public string redirectedUser { get; set; }
        public string redirectedText { get; set; }
        public Nullable<System.DateTime> redirectedDate { get; set; }
    }
}
