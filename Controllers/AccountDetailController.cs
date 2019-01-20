using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDemand.Models;
using System.Data;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;


namespace MvcDemand.Controllers
{
    public class AccountDetailController : Controller
    {

        /* 函數名稱         :   AccountDetailController.cs
         * 程式人員         :   Wilson Cheng
         * 函數更新紀錄     :
         *                      2018-11-20      新建函數
         *                      Index           進入頁面時導入第一個函數   
         *                      getBindData     取得會員資料並且將新增資料的相關選單資料導入
         *                      checkAccNo      驗證會員帳號是否重複
         *                      Create          進行新增會員資料表單資料到資料庫    Post
         *                      FormUpdate      點選按鈕移到更新資料庫表單畫面
         *                      Update          進行更新會員資料表單資料到資料庫    Post
         *                      Upload          使用Excel檔案上傳新增會員資料到資料庫
         *                      OutToExcel      將會員資料匯出成為Excel檔案
         */

        /// <summary>
        /// 定義變數
        /// </summary>
        AccountDetailModels adModel = new AccountDetailModels();
        SystemDataDetailModels sddModel = new SystemDataDetailModels();
        ClassDataBase dbClass = new ClassDataBase();
        public List<string> aryDeclareName = new List<string>() { 
            "@AccIndex", "@AccNo", "@AccName", "@AccClass", "@AccDeptNo"
            , "@AccJobNo" , "@AccMobile", "@AccPhone", "@AccEmail",  "@AccPassword"
            , "@AccNotation", "@AccImage", "@AccDateS", "@AccDateE", "@AccStatus","@AccNotationS" };
        
        /// <summary>
        /// 函數名稱    :   Index
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult Index(AccountDetailModels viewModel)
        {
            getBindData("", "1", viewModel, "", "", "");
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(FormCollection form, AccountDetailModels viewModel)
        {
            string fSearchValue, fSearchClass, fSearchDept, fSearchJob; 
            fSearchValue = form["searchValue"].ToString();
            fSearchClass = form["hideSearchClass"].ToString();
            fSearchDept = form["hideSearchDept"].ToString();
            fSearchJob = form["hideSearchJob"].ToString();
            ViewBag.searchValue = fSearchValue;
            ViewBag.valSearchClass = fSearchClass;
            ViewBag.valSearchDept = fSearchDept;
            ViewBag.valSearchJob = fSearchJob;
            getBindData(fSearchValue, "1", viewModel, fSearchClass, fSearchDept, fSearchJob);
            return View(viewModel);
        }

        /// <summary>
        /// 函數名稱    :   getBindData
        /// </summary>
        /// <param name="fSearchValue"></param>
        /// <param name="fPageIndex"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public PartialViewResult getBindData(string fSearchValue, string fPageIndex , AccountDetailModels viewModel, string fSearchClass, string fSearchDept, string fSearchJob) {
            List<oAccountDetail> accList = new List<oAccountDetail>();
            accList = adModel.listObjAccountDetail();
            
            if (!(string.IsNullOrWhiteSpace(fSearchValue))) {
                accList = accList.Where(x => x.oAccIndex.Contains(fSearchValue) || x.oAccNo.Contains(fSearchValue) || x.oAccName.Contains(fSearchValue)
                    || x.oAccMobile.Contains(fSearchValue) || x.oAccPhone.Contains(fSearchValue) || x.oAccEmail.Contains(fSearchValue) || x.oAccNotation.Contains(fSearchValue)
                    || x.oAccDeptNo.Contains(fSearchValue) || x.oAccJobNo.Contains(fSearchValue)).ToList();
            }
            if (!(string.IsNullOrWhiteSpace(fSearchClass))) {
                if (fSearchClass.IndexOf(',') == -1) {
                    accList = accList.Where(x => x.oAccClass.Contains(fSearchClass)).ToList();
                } else {
                    string[] arySearchClass = fSearchClass.Split(',');
                    accList = accList.Where(x => arySearchClass.Contains(x.oAccClass)).ToList();                    
                }
            }
            if (!(string.IsNullOrWhiteSpace(fSearchDept))) {
                if (fSearchDept.IndexOf(',') == -1)
                {
                    accList = accList.Where(x => x.oAccDeptNo.Contains(fSearchDept)).ToList();
                } else {
                    string[] arySearchDept = fSearchDept.Split(',');
                    accList = accList.Where(x => arySearchDept.Contains(x.oAccDeptNo)).ToList();
                }
            }
            if (!(string.IsNullOrWhiteSpace(fSearchJob)))
            {
                if (fSearchJob.IndexOf(',') == -1)
                {
                    accList = accList.Where(x => x.oAccJobNo.Contains(fSearchJob)).ToList();
                } else {
                    string[] arySearchJob = fSearchJob.Split(',');
                    accList = accList.Where(x => arySearchJob.Contains(x.oAccJobNo)).ToList();
                }
            }
            viewModel.viewAccountDetail = accList;
            viewModel.valCountSum = accList.Count.ToString();
            viewModel.valCountPage = "30";
            viewModel.valPageCountSum = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(viewModel.valCountSum) / Convert.ToDecimal(viewModel.valCountPage))).ToString();
            viewModel.valPageIndex = fPageIndex;
            ViewBag.valCountSum = viewModel.valCountSum;
            ViewBag.valCountPage = viewModel.valCountPage;
            ViewBag.valPageCountSum = viewModel.valPageCountSum;
            ViewBag.valPageIndex = viewModel.valPageIndex;
            
            List<SelectListItem> listselAccClass = new List<SelectListItem>();
            listselAccClass = sddModel.selObjSystemDataDetail("AccClass", "請選擇帳號類別", "");
            ViewBag.selAccClass = listselAccClass;
            List<SelectListItem> listselDeptNo = new List<SelectListItem>();
            listselDeptNo = sddModel.selObjSystemDataDetail("AccDeptNo", "請選擇所屬單位", "S"); 
            ViewBag.selAccDeptNo = listselDeptNo;
            List<SelectListItem> listselJobNo = new List<SelectListItem>();
            listselJobNo = sddModel.selObjSystemDataDetail("AccJobNo", "請選擇帳號職稱", "D");
            ViewBag.selAccJobNo = listselJobNo;
            ViewBag.chkAccClass = sddModel.listObjSystemDataDetail().Where(x=>x.oSystemClass == "AccClass" && x.oSystemStatus != "D").ToList();
            ViewBag.chkAccDeptNo = sddModel.listObjSystemDataDetail().Where(x => x.oSystemClass == "AccDeptNo" && x.oSystemStatus != "D").ToList();
            ViewBag.chkAccJobNo = sddModel.listObjSystemDataDetail().Where(x => x.oSystemClass == "AccJobNo" && x.oSystemStatus != "D").ToList();
            ViewBag.valArrayAccClass = sddModel.rtnArraySystemDataClass("AccClass", "_");
            ViewBag.valArrayAccDeptNo = sddModel.rtnArraySystemDataClass("AccDeptNo", "_");
            ViewBag.valArrayAccJobNo = sddModel.rtnArraySystemDataClass("AccJobNo", "_");

            return PartialView("List", viewModel);
        }

        /// <summary>
        /// 函數名稱    :   checkAccNo 
        /// </summary>
        /// <param name="funAccNo"></param>
        /// <returns></returns>
        public string checkAccNo(string funAccNo)
        {
            string rtnValue = ""; int chkCount = 0;
            chkCount = adModel.listObjAccountDetail().Where(x => x.oAccNo == funAccNo).Count();
            rtnValue = (chkCount > 0) ? "O" : "X";                
            return rtnValue;
        }

        /// <summary>
        /// 函數名稱    :   Create  HttpPost
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public RedirectResult Create(FormCollection form)
        {
            List<object> listDataCreate = new List<object>()
            {
                adModel.getNewAccIndex().ToString(), form["textAccNo"].ToString(),
                form["textAccName"].ToString(), form["hideAccClass"].ToString(),
                form["hideAccDeptNo"].ToString(), form["hideAccJobNo"].ToString(),
                form["textAccMobile"].ToString(), form["textAccPhone"].ToString(),
                form["textAccEmail"].ToString(), form["textAccMobile"].ToString(),
                HttpUtility.HtmlEncode(form["textAccNotation"].ToString()), "",
                form["textAccDateS"].ToString(), "", "O", form["hideAccNotationS"].ToString() };
            string fExecuteValue = dbClass.msExecuteDataBase("N", "AccountDetail", 0, aryDeclareName, listDataCreate);            
            return Redirect("~/AccountDetail/Index");
        }

        /// <summary>
        /// 函數名稱    :   FormUpdate
        /// </summary>
        /// <param name="fAccIndex"></param>
        /// <param name="fAccNo"></param>
        /// <returns></returns>
        public  JsonResult FormUpdate(string fAccIndex, string fAccNo) {
            List<oAccountDetail> deList = new List<oAccountDetail>();
            deList = adModel.detailObjAccountDetail(fAccIndex, fAccNo);
            var rtnData = new { 
                deList[0].oAccIndex, deList[0].oAccNo, deList[0].oAccName, deList[0].oAccClass,
                deList[0].oAccDeptNo, deList[0].oAccJobNo, deList[0].oAccMobile, deList[0].oAccPhone,
                deList[0].oAccEmail, deList[0].oAccNotationS, deList[0].oAccDateS, deList[0].oAccDateE,
                deList[0].oAccStatus, deList[0].oAccPassword };
            return Json(rtnData);
        }

        /// <summary>
        /// 函數名稱    :   Update
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public RedirectResult Update(FormCollection form)
        {
            List<object> listDataUpdate = new List<object>();
            listDataUpdate.Add(form["hideAccIndex"].ToString());
            listDataUpdate.Add(form["hideAccNo"].ToString());
            listDataUpdate.Add(form["textAccNameU"].ToString());
            listDataUpdate.Add(form["hideAccClass"].ToString());
            listDataUpdate.Add(form["hideAccDeptNo"].ToString());
            listDataUpdate.Add(form["hideAccJobNo"].ToString());
            listDataUpdate.Add(form["textAccMobileU"].ToString());
            listDataUpdate.Add(form["textAccPhoneU"].ToString());
            listDataUpdate.Add(form["textAccEmailU"].ToString());
            listDataUpdate.Add(form["hideAccPassword"].ToString());
            listDataUpdate.Add(HttpUtility.HtmlEncode(form["textAccNotationU"].ToString()));
            listDataUpdate.Add("");
            listDataUpdate.Add(form["hideAccDateS"].ToString());
            listDataUpdate.Add(form["textAccDateE"].ToString());
            listDataUpdate.Add(form["hideAccStatus"].ToString());
            listDataUpdate.Add(form["hideAccNotationS"].ToString());
            string fExecuteValue = dbClass.msExecuteDataBase("U", "AccountDetail", 2, aryDeclareName, listDataUpdate);            
            return Redirect("~/AccountDetail/Index");    
        }

        /// <summary>
        /// 函數名稱    :   Upload
        /// </summary>
        /// <param name="fileUpload"></param>
        /// <returns></returns>
        [HttpPost]
        public RedirectResult Upload(HttpPostedFileBase fileUpload)
        {
            string showDetailData = ""; DataTable dtSystemDetail = new DataTable();
            string AccIndex = ""; string AccNo = "";
            if (fileUpload != null && fileUpload.ContentLength > 0)
            {
                string FilePath = System.Web.HttpContext.Current.Server.MapPath("../excel");
                string FileName = fileUpload.FileName; string fExecuteValue = "";
                fileUpload.SaveAs(string.Format(@"{0}\{1}", FilePath, FileName));
                dynamic hssfweb; DataTable tempDataTable = new DataTable(); tempDataTable.Clear();
                using (MemoryStream ms = new MemoryStream(dbClass.rtnByteReadFromFile(string.Format(@"{0}\{1}", FilePath, FileName))))
                {
                    hssfweb = WorkbookFactory.Create(ms);
                }
                ISheet sheet = (ISheet)hssfweb.GetSheetAt(0);
                if (sheet.LastRowNum > 0)
                {
                    IRow rowT = (IRow)sheet.GetRow(0);
                    for (int i = 0; i < 9; i++)
                    {
                        DataColumn col = new DataColumn(rowT.GetCell(i).ToString(), typeof(string));
                        tempDataTable.Columns.Add(col);
                    }
                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        IRow rowD = (IRow)sheet.GetRow(i);
                        if (rowD != null)
                        {
                            DataRow tempRow = tempDataTable.NewRow();
                            for (int a = 0; a < 9; a++)
                            {
                                tempRow[a] = (rowD.GetCell(a).ToString() != null) ? rowD.GetCell(a).ToString() : "";
                            }
                        }
                    }                    
                    if (tempDataTable.Rows.Count > 0)
                    {
                        foreach (DataRow drtemp in tempDataTable.Rows)
                        {
                            List<object> DataList = new List<object>();
                            AccNo = drtemp["AccNo"].ToString();
                            if (adModel.listObjAccountDetail().Where(x => x.oAccIndex == AccNo).Count() > 0)
                            {
                                List<oAccountDetail> deList = new List<oAccountDetail>();
                                deList = adModel.listObjAccountDetail().Where(x => x.oAccIndex == AccNo).ToList();
                                DataList.Add(deList[0].oAccIndex); DataList.Add(drtemp["AccNo"]);
                                DataList.Add(drtemp["AccName"]); DataList.Add(drtemp["AccClass"]);
                                DataList.Add(drtemp["AccDeptNo"]); DataList.Add(drtemp["AccJobNo"]);
                                DataList.Add(drtemp["AccMobile"]); DataList.Add(drtemp["AccPhone"]);
                                DataList.Add(drtemp["AccEmail"]); DataList.Add(drtemp["AccMobile"]);
                                DataList.Add(deList[0].oAccNotation); DataList.Add("");
                                DataList.Add(deList[0].oAccDateS); DataList.Add(deList[0].oAccDateE);
                                DataList.Add(deList[0].oAccStatus); DataList.Add(deList[0].oAccNotationS);
                                fExecuteValue = dbClass.msExecuteDataBase("U", "AccountDetail", 2, aryDeclareName, DataList);
                            } else { 
                                DataList.Add(adModel.getNewAccIndex().ToString());
                                DataList.Add(drtemp["AccNo"]); DataList.Add(drtemp["AccName"]);
                                DataList.Add(drtemp["AccClass"]); DataList.Add(drtemp["AccDeptNo"]);
                                DataList.Add(drtemp["AccJobNo"]); DataList.Add(drtemp["AccMobile"]);
                                DataList.Add(drtemp["AccPhone"]); DataList.Add(drtemp["AccEmail"]);
                                DataList.Add(drtemp["AccMobile"]); DataList.Add("");
                                DataList.Add(""); DataList.Add(dbClass.ReturnDetailToNowDateTime("SD"));
                                DataList.Add(""); DataList.Add("O"); DataList.Add("");
                                fExecuteValue = dbClass.msExecuteDataBase("N", "AccountDetail", 0, aryDeclareName, DataList);
                            }
                        }
                    }                    
                }                
            }
            return Redirect("~/AccountDetail/Index");
        }

        /// <summary>
        /// 函數名稱    :   OutToExcel
        /// </summary>
        public void OutToExcel() {
            List<string> listExcelTitle = new List<string>() { "AccNo", "AccName", "AccClass", "AccDeptNo", "AccJobNo", "AccMobile", "AccPhone", "AccEmail" };
            List<oAccountDetail> acList = new List<oAccountDetail>();
            acList = adModel.listObjAccountDetail();
            string FileName = "AccountData.xlsx"; string SheetName = "Data"; Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            XSSFWorkbook NpoiWB = new XSSFWorkbook();

            XSSFCellStyle xCellStyle = (XSSFCellStyle)NpoiWB.CreateCellStyle();
            XSSFDataFormat NpoiFormat = (XSSFDataFormat)NpoiWB.CreateDataFormat();
            xCellStyle.SetDataFormat(NpoiFormat.GetFormat("[DbNum2][$-804]0"));
            XSSFCellStyle cellStyleFontColor = (XSSFCellStyle)NpoiWB.CreateCellStyle();
            XSSFFont font1 = (XSSFFont)NpoiWB.CreateFont(); font1.Color = (short)10; font1.IsBold = true; cellStyleFontColor.SetFont(font1);

            ISheet xSheet = NpoiWB.CreateSheet(SheetName);

            IRow xRowT = xSheet.CreateRow(0); xRowT.HeightInPoints = 40;
            for (int i = 0; i < listExcelTitle.Count; i++)
            {
                ICell xCellT = xRowT.CreateCell(i); xCellT.SetCellValue(listExcelTitle[i]);
            }

            int len = 0;
            foreach (oAccountDetail item in acList)
            {
                List<string> listExcelData = new List<string>() { 
                    item.oAccNo.ToString(), item.oAccName.ToString(), item.oAccClass.ToString(), 
                    item.oAccDeptNo.ToString(), item.oAccJobNo.ToString(), item.oAccMobile.ToString(),
                    item.oAccPhone.ToString(), item.oAccEmail.ToString() };
                IRow xRowD = xSheet.CreateRow(len + 1); xRowD.HeightInPoints = 40;
                for (int b = 0; b < listExcelData.Count; b++)
                {
                    ICell xCellData = xRowD.CreateCell(b); xCellData.SetCellValue(listExcelData[b]);
                }
                len++;
            }

            MemoryStream MS = new MemoryStream(); NpoiWB.Write(MS);
            Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + "");
            Response.BinaryWrite(MS.ToArray());
            NpoiWB = null; MS.Close(); MS.Dispose(); Response.Flush(); Response.End();

        }
        
    }
}
