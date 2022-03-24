using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManualAction.BusinessLayer.DTO
{
    public class ReportDTO
    {

        public Nullable<decimal> sumAmount { get; set; }
        public string actionReason { get; set; }
        public string sortingDate { get; set; }
        public List<ReportDTO> detail { get; set; }
    }
}
