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
        List<ManualActionDTO> GetAllManager();
        ManualActionDTO GetManagerById(System.Guid id);
        List<ManualActionDTO> GetManagerByRegNo(string regNo);
        ManualActionDTO CreateManager(ManualActionDTO manager);
        ManualActionDTO UpdateManager(ManualActionDTO manager);
        bool DeleteManager(System.Guid id);
        bool DeleteManagerByDate(string sortDate);
        int GetCount(int statusType);
        int GetCountByRegNo(string regNo,int statusType);
        bool RedirectManager(ManualActionDTO manager,int depNo,string username);
        bool GetDataFromExcelFile(string filename, Stream stream);
        ManualAction.DataAccessLayer.ManualAction AddUserInfo(ManualAction.DataAccessLayer.ManualAction list);
        byte[] ExporttoExcel();
        byte[] ExporttoExcelByDate(DateTime startDate, DateTime endDate);
        List<List<ReportDTO>> GetReport();
        List<ReportDTO> GetReason();
        List<ReportDTO> GetYear();
        List<ReportDTO> GetMonth();
        List<ReportDTO> GetReasonByMonth();

        List<List<ReportDTO>> GetReportByDate(string sortDate);
        List<ReportDTO> GetReasonByDate(string sortDate);
        List<ReportDTO> GetReasonByMonthByDateYear(string sortDate);
        List<ReportDTO> GetMonthByDate(string sortDate);
        List<ReportDTO> GetReasonByMonthByDate(string sortDate);
        List<ReportDTO> GetReasonByYear(string year);
        List<ReportDTO> GetMonthByYear(string year);
        List<ReportDTO> GetReasonByMonthByYear(string year);
    }
}
