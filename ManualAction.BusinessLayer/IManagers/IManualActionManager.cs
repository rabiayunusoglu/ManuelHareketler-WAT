using ManualAction.BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManualAction.BusinessLayer.IManagers
{
    public interface IManualActionManager
    {
        ManualActionDTO CreateManager(ManualActionDTO manager);
        List<ManualActionDTO> GetAllManager();
        ManualActionDTO GetManagerById(System.Guid id);
        ManualActionDTO UpdateManager(ManualActionDTO manager);
        bool DeleteManager(System.Guid id);
        int GetCount(int statusType);

        bool DeleteManagerByDate(string sortDate);
        int GetCountByRegNo(string regNo, int statusType);
        List<ManualActionDTO> GetManagerByRegNo(string regNo);

        bool RedirectManager(ManualActionDTO manager, int depNo, string username);

        byte[] ExportDecision(int type, string regNo, DateTime startDate, DateTime endDate);
        byte[] Export(List<ManualActionDTO> list);
        bool GetDataFromExcelFile(string filename, Stream stream);
        ManualAction.DataAccessLayer.ManualAction AddUserInfo(ManualAction.DataAccessLayer.ManualAction list);

        List<List<ReportDTO>> GetReport();
        List<ReportDTO> GetReason();
        List<ReportDTO> GetYear();
        List<ReportDTO> GetMonth();
        List<ReportDTO> GetReasonByMonth();

        List<ReportDTO> GetReasonByDate(string sortDate);
        List<ReportDTO> GetMonthByDate(string sortDate);
        List<ReportDTO> GetReasonByMonthByDate(string sortDate);
        List<ReportDTO> GetReasonByMonthByDateYear(string sortDate);
        List<ReportDTO> GetMonthByYear(string year);
        List<List<ReportDTO>> GetReportByDate(string sortDate);

    }
}
