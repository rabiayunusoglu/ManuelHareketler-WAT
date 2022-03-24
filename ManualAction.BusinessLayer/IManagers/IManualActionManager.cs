﻿using ManualAction.BusinessLayer.DTO;
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
        int GetCount(int statusType);
        int GetCountByRegNo(string regNo,int statusType);
        bool RedirectManager(ManualActionDTO manager,int depNo,string username);
        bool GetDataFromExcelFile(string filename, Stream stream);
        ManualAction.DataAccessLayer.ManualAction AddUserInfo(ManualAction.DataAccessLayer.ManualAction list);
        byte[] ExporttoExcel();
        byte[] ExporttoExcelByDate(DateTime startDate, DateTime endDate);
        List<List<ReportDTO>> GetReport();
        List<ReportDTO> GetReason();
        List<ReportDTO> GetMonth();
        List<ReportDTO> GetReasonByMonth();

    }
}
