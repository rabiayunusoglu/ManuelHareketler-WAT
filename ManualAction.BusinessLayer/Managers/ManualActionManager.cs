﻿using System.Collections.Generic;
using ManualAction.BusinessLayer.DTO;
using System.Linq;

using ManualAction.BusinessLayer.IManagers;
using ManualAction.DataAccessLayer;
using System.Collections;
using System;
using System.IO;
using System.Data;
using ExcelDataReader;
using ClosedXML.Excel;

namespace ManualAction.BusinessLayer.Managers
{
    public class ManualActionManager : IManualActionManager
    {
        private UnitOfWork _unitOfWork;
        public ManualActionManager()
        {
            _unitOfWork = new UnitOfWork(new ManualActionsDBEntities());


        }
        public ManualActionDTO CreateManager(ManualActionDTO manager)
        {
            if (manager == null)
            {
                return null;
            }
            ManualAction.DataAccessLayer.ManualAction value = new ManualAction.DataAccessLayer.ManualAction();
            value.manualActionID = System.Guid.NewGuid();
            value.UY = manager.UY;
            value.HTU = manager.HTU;
            value.material = manager.material;
            value.materialText = manager.materialText;
            value.MT = manager.MT;
            value.amount = manager.amount;
            value.brm = manager.brm;
            value.priceBrm = manager.priceBrm;
            value.total = manager.total;
            value.DnmsMF = manager.DnmsMF;
            value.DnmsMFValue = manager.DnmsMFValue;
            value.orderValue = manager.orderValue;
            value.productCode = manager.productCode;
            value.productCodeInfo = manager.productCodeInfo;
            value.productYear = manager.productYear;
            value.registerDate = Convert.ToDateTime(manager.registerDate);
            value.loginDate = Convert.ToDateTime(manager.loginDate);
            value.loginDateTime = manager.loginDateTime;
            value.userRegisterNo = manager.userRegisterNo;
            value.username = manager.username;
            value.actionReason = manager.actionReason;
            value.reasonDetail = manager.reasonDetail;
            value.madeDate = (manager.madeDate);
            value.statusType = manager.statusType;
            value.statusName = manager.statusName;
            value.isRedirected = manager.isRedirected;
            value.historyID = manager.historyID;
            value.redirectedTeam = manager.redirectedTeam;
            value.redirectedUser = manager.redirectedUser;
            value.redirectedText = manager.redirectedText;
            value.redirectedDate = (manager.redirectedDate);

            ManualAction.DataAccessLayer.ManualAction recordValue = _unitOfWork.ManualActionRepository.Add(value);

            ManualActionDTO returnValue = new ManualActionDTO()
            {
                manualActionID = recordValue.manualActionID,
                UY = recordValue.UY,
                HTU = recordValue.HTU,
                material = recordValue.material,
                materialText = recordValue.materialText,
                MT = recordValue.MT,
                amount = recordValue.amount,
                brm = recordValue.brm,
                priceBrm = recordValue.priceBrm,
                total = recordValue.total,
                DnmsMF = recordValue.DnmsMF,
                DnmsMFValue = recordValue.DnmsMFValue,
                orderValue = recordValue.orderValue,
                productCode = recordValue.productCode,
                productCodeInfo = recordValue.productCodeInfo,
                productYear = recordValue.productYear,
                registerDate = recordValue.registerDate.ToString().Substring(0, 10),
                sortingDate = recordValue.registerDate.Value.Month < 10 ? recordValue.registerDate.Value.Year.ToString() + 0 + recordValue.registerDate.Value.Month.ToString() : recordValue.registerDate.Value.Year.ToString() + recordValue.registerDate.Value.Month.ToString(),
                loginDate = recordValue.loginDate.ToString().Substring(0, 10),
                loginDateTime = recordValue.loginDateTime,
                userRegisterNo = recordValue.userRegisterNo,
                username = recordValue.username,
                actionReason = recordValue.actionReason,
                reasonDetail = recordValue.reasonDetail,
                madeDate = recordValue.madeDate,
                statusType = recordValue.statusType,
                statusName = recordValue.statusName,
                isRedirected = recordValue.isRedirected,
                historyID = recordValue.historyID,
                redirectedTeam = recordValue.redirectedTeam,
                redirectedUser = recordValue.redirectedUser,
                redirectedText = recordValue.redirectedText,
                redirectedDate = recordValue.redirectedDate,
            };
            if (_unitOfWork.Complete() > 0)
                return returnValue;
            return null;
        }

        public bool DeleteManager(Guid id)
        {
            if (id == null)
                return false;
            if (_unitOfWork.ManualActionRepository.Remove(id))
            {
                if (_unitOfWork.Complete() > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public List<ManualActionDTO> GetAllManager()
        {
            List<ManualAction.DataAccessLayer.ManualAction> managerList = _unitOfWork.ManualActionRepository.GetAll().OrderBy(x => x.registerDate).ToList();
            List<ManualActionDTO> list = new List<ManualActionDTO>();
            if (managerList == null)
            {
                return null;
            }
            foreach (ManualAction.DataAccessLayer.ManualAction recordValue in managerList)
            {
                ManualActionDTO returnValue = new ManualActionDTO()
                {
                    manualActionID = recordValue.manualActionID,
                    UY = recordValue.UY,
                    HTU = recordValue.HTU,
                    material = recordValue.material,
                    materialText = recordValue.materialText,
                    MT = recordValue.MT,
                    amount = recordValue.amount,
                    brm = recordValue.brm,
                    priceBrm = recordValue.priceBrm,
                    total = recordValue.total,
                    DnmsMF = recordValue.DnmsMF,
                    DnmsMFValue = recordValue.DnmsMFValue,
                    orderValue = recordValue.orderValue,
                    productCode = recordValue.productCode,
                    productCodeInfo = recordValue.productCodeInfo,
                    productYear = recordValue.productYear,
                    registerDate = recordValue.registerDate.ToString().Substring(0, 10),
                    sortingDate = recordValue.registerDate.Value.Month < 10 ? recordValue.registerDate.Value.Year.ToString() + 0 + recordValue.registerDate.Value.Month.ToString() : recordValue.registerDate.Value.Year.ToString() + recordValue.registerDate.Value.Month.ToString(),
                    loginDate = recordValue.loginDate.ToString().Substring(0, 10),
                    loginDateTime = recordValue.loginDateTime.Substring(recordValue.loginDateTime.Length - 8),
                    userRegisterNo = recordValue.userRegisterNo,
                    username = recordValue.username,
                    actionReason = recordValue.actionReason,
                    reasonDetail = recordValue.reasonDetail,
                    madeDate = recordValue.madeDate,
                    statusType = recordValue.statusType,
                    statusName = recordValue.statusName,
                    isRedirected = recordValue.isRedirected,
                    historyID = recordValue.historyID,
                    redirectedTeam = recordValue.redirectedTeam,
                    redirectedUser = recordValue.redirectedUser,
                    redirectedText = recordValue.redirectedText,
                    redirectedDate = recordValue.redirectedDate,
                };
                list.Add(returnValue);

            }

            return list;
        }

        public ManualActionDTO GetManagerById(Guid id)
        {
            if (id == null)
                return null;
            ManualAction.DataAccessLayer.ManualAction recordValue = _unitOfWork.ManualActionRepository.GetById(id);
            if (recordValue == null)
            {
                return null;
            }
            ManualActionDTO returnValue = new ManualActionDTO()
            {
                manualActionID = recordValue.manualActionID,
                UY = recordValue.UY,
                HTU = recordValue.HTU,
                material = recordValue.material,
                materialText = recordValue.materialText,
                MT = recordValue.MT,
                amount = recordValue.amount,
                brm = recordValue.brm,
                priceBrm = recordValue.priceBrm,
                total = recordValue.total,
                DnmsMF = recordValue.DnmsMF,
                DnmsMFValue = recordValue.DnmsMFValue,
                orderValue = recordValue.orderValue,
                productCode = recordValue.productCode,
                productCodeInfo = recordValue.productCodeInfo,
                productYear = recordValue.productYear,
                registerDate = recordValue.registerDate.ToString().Substring(0, 10),
                sortingDate = recordValue.registerDate.Value.Month < 10 ? recordValue.registerDate.Value.Year.ToString() + 0 + recordValue.registerDate.Value.Month.ToString() : recordValue.registerDate.Value.Year.ToString() + recordValue.registerDate.Value.Month.ToString(),
                loginDate = recordValue.loginDate.ToString().Substring(0, 10),
                loginDateTime = recordValue.loginDateTime,
                userRegisterNo = recordValue.userRegisterNo,
                username = recordValue.username,
                actionReason = recordValue.actionReason,
                reasonDetail = recordValue.reasonDetail,
                madeDate = recordValue.madeDate,
                statusType = recordValue.statusType,
                statusName = recordValue.statusName,
                isRedirected = recordValue.isRedirected,
                historyID = recordValue.historyID,
                redirectedTeam = recordValue.redirectedTeam,
                redirectedUser = recordValue.redirectedUser,
                redirectedText = recordValue.redirectedText,
                redirectedDate = recordValue.redirectedDate,
            };
            return returnValue;
        }

        public ManualActionDTO UpdateManager(ManualActionDTO manager)
        {
            if (manager == null)
            {
                return null;
            }
            ManualAction.DataAccessLayer.ManualAction value = new ManualAction.DataAccessLayer.ManualAction();
            value.manualActionID = manager.manualActionID;
            value.UY = manager.UY;
            value.HTU = manager.HTU;
            value.material = manager.material;
            value.materialText = manager.materialText;
            value.MT = manager.MT;
            value.amount = manager.amount;
            value.brm = manager.brm;
            value.priceBrm = manager.priceBrm;
            value.total = manager.total;
            value.DnmsMF = manager.DnmsMF;
            value.DnmsMFValue = manager.DnmsMFValue;
            value.orderValue = manager.orderValue;
            value.productCode = manager.productCode;
            value.productCodeInfo = manager.productCodeInfo;
            value.productYear = manager.productYear;
            value.registerDate = Convert.ToDateTime(manager.registerDate);
            value.loginDate = Convert.ToDateTime(manager.loginDate);
            value.loginDateTime = manager.loginDateTime;
            value.userRegisterNo = manager.userRegisterNo;
            value.username = manager.username;
            value.actionReason = manager.actionReason;
            value.reasonDetail = manager.reasonDetail;
            value.madeDate = (manager.madeDate);
            value.statusType = manager.statusType;
            value.statusName = manager.statusName;
            value.isRedirected = manager.isRedirected;
            value.historyID = manager.historyID;
            value.redirectedTeam = manager.redirectedTeam;
            value.redirectedUser = manager.redirectedUser;
            value.redirectedText = manager.redirectedText;
            value.redirectedDate = (manager.redirectedDate);

            ManualAction.DataAccessLayer.ManualAction recordValue = _unitOfWork.ManualActionRepository.Update(value);

            ManualActionDTO returnValue = new ManualActionDTO()
            {
                manualActionID = recordValue.manualActionID,
                UY = recordValue.UY,
                HTU = recordValue.HTU,
                material = recordValue.material,
                materialText = recordValue.materialText,
                MT = recordValue.MT,
                amount = recordValue.amount,
                brm = recordValue.brm,
                priceBrm = recordValue.priceBrm,
                total = recordValue.total,
                DnmsMF = recordValue.DnmsMF,
                DnmsMFValue = recordValue.DnmsMFValue,
                orderValue = recordValue.orderValue,
                productCode = recordValue.productCode,
                productCodeInfo = recordValue.productCodeInfo,
                productYear = recordValue.productYear,
                registerDate = recordValue.registerDate.ToString().Substring(0, 10),
                sortingDate = recordValue.registerDate.Value.Month < 10 ? recordValue.registerDate.Value.Year.ToString() + 0 + recordValue.registerDate.Value.Month.ToString() : recordValue.registerDate.Value.Year.ToString() + recordValue.registerDate.Value.Month.ToString(),
                loginDate = recordValue.loginDate.ToString().Substring(0, 10),
                loginDateTime = recordValue.loginDateTime,
                userRegisterNo = recordValue.userRegisterNo,
                username = recordValue.username,
                actionReason = recordValue.actionReason,
                reasonDetail = recordValue.reasonDetail,
                madeDate = recordValue.madeDate,
                statusType = recordValue.statusType,
                statusName = recordValue.statusName,
                isRedirected = recordValue.isRedirected,
                historyID = recordValue.historyID,
                redirectedTeam = recordValue.redirectedTeam,
                redirectedUser = recordValue.redirectedUser,
                redirectedText = recordValue.redirectedText,
                redirectedDate = recordValue.redirectedDate,
            };
            if (_unitOfWork.Complete() > 0)
                return returnValue;
            return null;
        }
        public bool GetDataFromExcelFile(string filename, Stream stream)
        {
            DataSet dsexcelRecords = new DataSet();
            IExcelDataReader reader = null;

            if (stream != null)
            {
                try
                {
                    if (filename.EndsWith(".xls"))
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    else if (filename.EndsWith(".xlsx"))
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    else
                    {
                        Console.WriteLine("The file format is not supported.");
                        return false;
                    }
                    dsexcelRecords = reader.AsDataSet();
                    reader.Close();
                    if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
                    {

                        DataTable dtMontageRecords = dsexcelRecords.Tables[0];
                        DateTime today = DateTime.Now;
                        for (int i = 1; i < dtMontageRecords.Rows.Count; i++)
                        {
                            ManualAction.DataAccessLayer.ManualAction objStudent = new ManualAction.DataAccessLayer.ManualAction();

                            objStudent.manualActionID = System.Guid.NewGuid();
                            objStudent.UY = (dtMontageRecords.Rows[i][0].ToString());
                            objStudent.HTU = Convert.ToString(dtMontageRecords.Rows[i][1]);
                            objStudent.material = Convert.ToString(dtMontageRecords.Rows[i][2].ToString());
                            objStudent.materialText = Convert.ToString(dtMontageRecords.Rows[i][3]);
                            objStudent.MT = Convert.ToString(dtMontageRecords.Rows[i][4]);
                            objStudent.amount = Convert.ToString(dtMontageRecords.Rows[i][5]);
                            objStudent.brm = Convert.ToString(dtMontageRecords.Rows[i][6]);
                            objStudent.priceBrm = Convert.ToString(dtMontageRecords.Rows[i][7]);
                            objStudent.total = Convert.ToString(dtMontageRecords.Rows[i][8]);
                            objStudent.DnmsMF = Convert.ToString(dtMontageRecords.Rows[i][9]);
                            objStudent.DnmsMFValue = Convert.ToDecimal(dtMontageRecords.Rows[i][10]);
                            objStudent.orderValue = Convert.ToString(dtMontageRecords.Rows[i][11]);
                            objStudent.productCode = Convert.ToString(dtMontageRecords.Rows[i][12]);
                            objStudent.productCodeInfo = Convert.ToString(dtMontageRecords.Rows[i][13]);
                            objStudent.productYear = Convert.ToString(dtMontageRecords.Rows[i][14]);
                            objStudent.registerDate = Convert.ToDateTime(dtMontageRecords.Rows[i][15]);
                            objStudent.loginDate = Convert.ToDateTime(dtMontageRecords.Rows[i][16]);
                            objStudent.loginDateTime = Convert.ToString(dtMontageRecords.Rows[i][17]);
                            objStudent.userRegisterNo = Convert.ToString(dtMontageRecords.Rows[i][18]);
                            objStudent.statusType = 1;
                            objStudent.statusName = "Başlamadı";
                            objStudent.isRedirected = false;
                            objStudent.actionReason = "-";
                            ManualAction.DataAccessLayer.ManualAction recordValue = _unitOfWork.ManualActionRepository.Add(AddUserInfo(objStudent));

                        }
                        if (_unitOfWork.Complete() > 0)
                        {

                            return true;

                        }


                    }

                }
                catch (Exception)
                {
                    throw;
                }
            }

            return false;
        }
        public ManualAction.DataAccessLayer.ManualAction AddUserInfo(ManualAction.DataAccessLayer.ManualAction list)
        {
            UserListManager user = new UserListManager();
            UserListDTO userr = user.GetManagerById((list.userRegisterNo));
            if (userr == null)
                return list;
            list.username = userr.username;
            return list;
        }
        public int GetCount(int statusType)
        {
            List<ManualAction.DataAccessLayer.ManualAction> managerList = _unitOfWork.ManualActionRepository.GetAll().ToList();
            if (managerList == null)
            {
                return 0;
            }
            int managerCount = managerList.Where(x => x.statusType == statusType).Count();
            return managerCount;
        }

        public List<ManualActionDTO> GetManagerByRegNo(string regNo)
        {
            List<ManualAction.DataAccessLayer.ManualAction> managerList = _unitOfWork.ManualActionRepository.GetAll().Where(t => t.userRegisterNo == regNo).OrderBy(x => x.registerDate).ToList();
            List<ManualActionDTO> list = new List<ManualActionDTO>();
            if (managerList == null)
            {
                return null;
            }
            foreach (ManualAction.DataAccessLayer.ManualAction recordValue in managerList)
            {
                ManualActionDTO returnValue = new ManualActionDTO()
                {
                    manualActionID = recordValue.manualActionID,
                    UY = recordValue.UY,
                    HTU = recordValue.HTU,
                    material = recordValue.material,
                    materialText = recordValue.materialText,
                    MT = recordValue.MT,
                    amount = recordValue.amount,
                    brm = recordValue.brm,
                    priceBrm = recordValue.priceBrm,
                    total = recordValue.total,
                    DnmsMF = recordValue.DnmsMF,
                    DnmsMFValue = recordValue.DnmsMFValue,
                    orderValue = recordValue.orderValue,
                    productCode = recordValue.productCode,
                    productCodeInfo = recordValue.productCodeInfo,
                    productYear = recordValue.productYear,
                    registerDate = recordValue.registerDate.ToString().Substring(0, 10),
                    sortingDate = recordValue.registerDate.Value.Month < 10 ? recordValue.registerDate.Value.Year.ToString() + 0 + recordValue.registerDate.Value.Month.ToString() : recordValue.registerDate.Value.Year.ToString() + recordValue.registerDate.Value.Month.ToString(),
                    loginDate = recordValue.loginDate.ToString().Substring(0, 10),
                    loginDateTime = recordValue.loginDateTime.Substring(recordValue.loginDateTime.Length - 8),
                    userRegisterNo = recordValue.userRegisterNo,
                    username = recordValue.username,
                    actionReason = recordValue.actionReason,
                    reasonDetail = recordValue.reasonDetail,
                    madeDate = recordValue.madeDate,
                    statusType = recordValue.statusType,
                    statusName = recordValue.statusName,
                    isRedirected = recordValue.isRedirected,
                    historyID = recordValue.historyID,
                    redirectedTeam = recordValue.redirectedTeam,
                    redirectedUser = recordValue.redirectedUser,
                    redirectedText = recordValue.redirectedText,
                    redirectedDate = recordValue.redirectedDate,
                };
                list.Add(returnValue);

            }

            return list;
        }

        public int GetCountByRegNo(string regNo, int statusType)
        {
            List<ManualAction.DataAccessLayer.ManualAction> managerList = _unitOfWork.ManualActionRepository.GetAll().Where(t => t.userRegisterNo == regNo).ToList();
            if (managerList == null)
            {
                return 0;
            }
            int managerCount = managerList.Where(x => x.statusType == statusType).Count();
            return managerCount;
        }

        public bool RedirectManager(ManualActionDTO manager, int depNo, string username)
        {
            if (manager == null || depNo == null || username == null)
                return false;
            string depName = "";
            DepartmantManager dep = new DepartmantManager();
            DepartmantDTO depValue = dep.GetManagerById(depNo);
            if (depValue.departmentName == null || depValue.departmentName.Length == 0)
                depName = "*";
            depName = depValue.departmentName;
            ActionHistoryDTO actionHistory = new ActionHistoryDTO();
            actionHistory.manualActionID = manager.manualActionID.ToString();
            actionHistory.senderUser = username;
            actionHistory.senderTeam = depName;
            actionHistory.receiverTeam = manager.redirectedTeam;
            actionHistory.receiverUser = manager.redirectedUser;
            actionHistory.redirectedText = manager.redirectedText;
            actionHistory.redirectedDate = DateTime.Now.ToString();
            ActionHistoryManager actionHistoryManager = new ActionHistoryManager();
            ActionHistoryDTO historyDTO = actionHistoryManager.CreateManager(actionHistory);
            if (historyDTO == null)
                return false;
            manager.statusType = 2;
            manager.statusName = "Devam Ediyor";
            manager.isRedirected = true;
            manager.historyID = historyDTO.historyID.ToString();
            manager.redirectedTeam = "";
            manager.redirectedText = "";
            manager.redirectedUser = "";
            manager.redirectedDate = DateTime.Now;
            ManualActionManager manualAction = new ManualActionManager();
            ManualActionDTO returnValue = manualAction.UpdateManager(manager);
            if (returnValue == null)
                return false;
            return true;
        }

        public byte[] ExporttoExcel()
        {

            using (var workbook = new XLWorkbook())
            {
                var ws = workbook.Worksheets.Add("WAT-Manuel Hareketler");
                ws.ColumnWidth = 10;

                //header
                ws.Cell(1, 1).Value = "ÜY";
                ws.Cell(1, 2).Value = "HTü";
                ws.Cell(1, 3).Value = "Malzeme No";
                ws.Cell(1, 4).Value = "Malzeme Kısa Metni";
                ws.Cell(1, 5).Value = "MT";
                ws.Cell(1, 6).Value = "Miktar";
                ws.Cell(1, 7).Value = "Birim";
                ws.Cell(1, 8).Value = "Fiyat Birim";
                ws.Cell(1, 9).Value = "Tutar";
                ws.Cell(1, 10).Value = "Dnms.MF";
                ws.Cell(1, 11).Value = "Dnms.MF Değeri";
                ws.Cell(1, 12).Value = "Sipariş";
                ws.Cell(1, 13).Value = "Malzeme";
                ws.Cell(1, 14).Value = "Malzeme Bilgisi";
                ws.Cell(1, 15).Value = "M.yıl";
                ws.Cell(1, 16).Value = "Kayıt Tarihi";
                ws.Cell(1, 17).Value = "Giriş Tarihi";
                ws.Cell(1, 18).Value = "Saat";
                ws.Cell(1, 19).Value = "Sorumlu Sicili";
                ws.Cell(1, 20).Value = "Sorumlu Birey";
                ws.Cell(1, 21).Value = "Hareket Sebebi";
                ws.Cell(1, 22).Value = "Açıklama";
                ws.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 2).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 3).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 4).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 5).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 6).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 7).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 8).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 9).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 10).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 11).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 12).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 13).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 14).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 15).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 16).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 17).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 18).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 19).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 20).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 21).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 22).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;

                //connect to sql here
                System.Data.DataTable dt = new System.Data.DataTable();
                List<ManualActionDTO> list = GetAllManager();
                if (list.Count() == 0)
                    return null;
                int i = 2;
                foreach (ManualActionDTO item in list)
                {
                    ws.Cell(i, 1).Value = Convert.ToString(item.UY);
                    ws.Cell(i, 2).Value = Convert.ToString(item.HTU);
                    ws.Cell(i, 3).Value = Convert.ToString(item.material).TrimEnd();
                    ws.Cell(i, 4).Value = Convert.ToString(item.materialText);
                    ws.Cell(i, 5).Value = Convert.ToString(item.MT);
                    ws.Cell(i, 6).Value = Convert.ToString(item.amount);
                    ws.Cell(i, 7).Value = Convert.ToString(item.brm);
                    ws.Cell(i, 8).Value = Convert.ToString(item.priceBrm);
                    ws.Cell(i, 9).Value = Convert.ToString(item.total);
                    ws.Cell(i, 10).Value = Convert.ToString(item.DnmsMF);
                    ws.Cell(i, 11).Value = Convert.ToString(item.DnmsMFValue);
                    ws.Cell(i, 12).Value = Convert.ToString(item.orderValue).TrimEnd();
                    ws.Cell(i, 13).Value = Convert.ToString(item.productCode).TrimEnd();
                    ws.Cell(i, 14).Value = Convert.ToString(item.productCodeInfo);
                    ws.Cell(i, 15).Value = Convert.ToString(item.productYear);
                    ws.Cell(i, 16).Value = Convert.ToString(item.registerDate);
                    ws.Cell(i, 17).Value = Convert.ToString(item.loginDate);
                    ws.Cell(i, 18).Value = Convert.ToString(item.loginDateTime);
                    ws.Cell(i, 19).Value = Convert.ToString(item.userRegisterNo);
                    ws.Cell(i, 20).Value = Convert.ToString(item.username);
                    ws.Cell(i, 21).Value = Convert.ToString(item.actionReason);
                    ws.Cell(i, 22).Value = Convert.ToString(item.reasonDetail);
                    i++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    byte[] content = stream.ToArray();
                    return content;
                }
                return null;
            }

        }
        public byte[] ExporttoExcelByDate(DateTime startDate, DateTime endDate)
        {
            using (var workbook = new XLWorkbook())
            {
                var ws = workbook.Worksheets.Add("WAT-Manuel Hareketler");
                ws.ColumnWidth = 10;

                //header
                ws.Cell(1, 1).Value = "ÜY";
                ws.Cell(1, 2).Value = "HTü";
                ws.Cell(1, 3).Value = "Malzeme No";
                ws.Cell(1, 4).Value = "Malzeme Kısa Metni";
                ws.Cell(1, 5).Value = "MT";
                ws.Cell(1, 6).Value = "Miktar";
                ws.Cell(1, 7).Value = "Birim";
                ws.Cell(1, 8).Value = "Fiyat Birim";
                ws.Cell(1, 9).Value = "Tutar";
                ws.Cell(1, 10).Value = "Dnms.MF";
                ws.Cell(1, 11).Value = "Dnms.MF Değeri";
                ws.Cell(1, 12).Value = "Sipariş";
                ws.Cell(1, 13).Value = "Malzeme";
                ws.Cell(1, 14).Value = "Malzeme Bilgisi";
                ws.Cell(1, 15).Value = "M.yıl";
                ws.Cell(1, 16).Value = "Kayıt Tarihi";
                ws.Cell(1, 17).Value = "Giriş Tarihi";
                ws.Cell(1, 18).Value = "Saat";
                ws.Cell(1, 19).Value = "Sorumlu Sicili";
                ws.Cell(1, 20).Value = "Sorumlu Birey";
                ws.Cell(1, 21).Value = "Hareket Sebebi";
                ws.Cell(1, 22).Value = "Açıklama";
                ws.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 2).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 3).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 4).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 5).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 6).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 7).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 8).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 9).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 10).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 11).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 12).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 13).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 14).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 15).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 16).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 17).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 18).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 19).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 20).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 21).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                ws.Cell(1, 22).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;

                //connect to sql here
                System.Data.DataTable dt = new System.Data.DataTable();
                List<ManualActionDTO> list = GetAllManager();
                list = list.Where(x => Convert.ToDateTime(x.registerDate) <= endDate && Convert.ToDateTime(x.registerDate) >= startDate).ToList();
                if (list.Count() == 0)
                    return null;
                int i = 2;
                foreach (ManualActionDTO item in list)
                {
                    ws.Cell(i, 1).Value = Convert.ToString(item.UY);
                    ws.Cell(i, 2).Value = Convert.ToString(item.HTU);
                    ws.Cell(i, 3).Value = Convert.ToString(item.material).TrimEnd();
                    ws.Cell(i, 4).Value = Convert.ToString(item.materialText);
                    ws.Cell(i, 5).Value = Convert.ToString(item.MT);
                    ws.Cell(i, 6).Value = Convert.ToString(item.amount);
                    ws.Cell(i, 7).Value = Convert.ToString(item.brm);
                    ws.Cell(i, 8).Value = Convert.ToString(item.priceBrm);
                    ws.Cell(i, 9).Value = Convert.ToString(item.total);
                    ws.Cell(i, 10).Value = Convert.ToString(item.DnmsMF);
                    ws.Cell(i, 11).Value = Convert.ToString(item.DnmsMFValue);
                    ws.Cell(i, 12).Value = Convert.ToString(item.orderValue).TrimEnd();
                    ws.Cell(i, 13).Value = Convert.ToString(item.productCode).TrimEnd();
                    ws.Cell(i, 14).Value = Convert.ToString(item.productCodeInfo);
                    ws.Cell(i, 15).Value = Convert.ToString(item.productYear);
                    ws.Cell(i, 16).Value = Convert.ToString(item.registerDate);
                    ws.Cell(i, 17).Value = Convert.ToString(item.loginDate);
                    ws.Cell(i, 18).Value = Convert.ToString(item.loginDateTime);
                    ws.Cell(i, 19).Value = Convert.ToString(item.userRegisterNo);
                    ws.Cell(i, 20).Value = Convert.ToString(item.username);
                    ws.Cell(i, 21).Value = Convert.ToString(item.actionReason);
                    ws.Cell(i, 22).Value = Convert.ToString(item.reasonDetail);
                    i++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    byte[] content = stream.ToArray();
                    return content;
                }
                return null;
            }

        }

        public List<ReportDTO> GetReason()
        {
            List<ReportDTO> list = GetAllManager().GroupBy(t => new { t.actionReason }).OrderBy(y => y.Key.actionReason).Select(s => new ReportDTO { actionReason = s.Key.actionReason, sumAmount = s.Sum(i => (i.DnmsMFValue)) }).ToList();
            if (list == null)
                return null;
            return list;
        }
        public List<ReportDTO> GetMonth()
        {
            List<ReportDTO> list = GetAllManager().GroupBy(t => new { t.sortingDate }).OrderBy(y => y.Key.sortingDate).Select(s => new ReportDTO { sortingDate = s.Key.sortingDate, sumAmount = s.Sum(i => (i.DnmsMFValue)) }).ToList();
            if (list == null)
                return null;
            return list;
        }
        public List<ReportDTO> GetReasonByMonth()
        {
            List<ReportDTO> list = GetAllManager().GroupBy(t => new { t.actionReason, t.sortingDate }).OrderBy(y => y.Key.sortingDate).Select(s => new ReportDTO { actionReason = s.Key.actionReason, sortingDate = s.Key.sortingDate, sumAmount = s.Sum(i => (i.DnmsMFValue)) }).ToList();
            if (list == null)
                return null;
            return list;
        }

        public List<List<ReportDTO>> GetReport()
        {
            List<List<ReportDTO>> report = new List<List<ReportDTO>>();
            List<ReportDTO> column = GetMonth();
            List<ReportDTO> line = GetReason();
            List<ReportDTO> detail = GetReasonByMonth();
            if (column.Count == 0 || line.Count == 0)
                return null;
            for (var i = 0; i < line.Count; i++)
            {
                List<ReportDTO> b = new List<ReportDTO>();
                for (var j = 0; j < column.Count; j++)
                {

                    ReportDTO temp = new ReportDTO();
                    List<ReportDTO> value = detail.Where(t => t.actionReason == line[i].actionReason && t.sortingDate == column[j].sortingDate).ToList();
                    if (value != null && value.Count() != 0) { 
                        temp.sumAmount = value.FirstOrDefault().sumAmount;
                        temp.sortingDate = value.FirstOrDefault().sortingDate;
                    }
                    b.Add(temp);
                }
                line[i].detail = b;
            }
            report.Add(column);
            report.Add(line);
            List<ReportDTO> total = new List<ReportDTO>();
            ReportDTO reportItem = new ReportDTO();
            reportItem.sumAmount = GetAllManager().Sum(z=>z.DnmsMFValue);
            total.Add(reportItem);
            report.Add(total);
            return report;
        }
    }
}