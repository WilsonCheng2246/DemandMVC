using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MvcDemand.Models;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;

namespace MvcDemand.Controllers
{
    public class SystemDataDetailController : Controller
    {

        /* 函數名稱         :   SystemDataDetailController.cs
         * 程式人員         :   Wilson Cheng
         * 函數更新紀錄     :
         *                      2018-11-20                  新建函數
         *                      Index                       進入頁面時導入第一個函數 
         *                      Detail                      系統參數明細從資料庫取得List
         *                      Index   Post                重新導入第一個函數
         *                      GetBindData                 取得系統參數清單取得List
         *                      Create  Post                新增系統參數基本資料
         *                      Update  Post                修改系統參數基本資料
         *                      Delete  Post                刪除系統參數基本資料
         *                      OutToExecl                  匯出系統參數基本資料
         *                      Upload  Post                上傳系統參數基本資料
         */

        /// <summary>
        /// 定義變數
        /// </summary>
        SystemDataDetailModels sddModel = new SystemDataDetailModels();
        ClassDataBase dbClass = new ClassDataBase();
        public List<string> aryColumnName = new List<string>() { "SystemClass", "SystemValue", "SystemTitle", "SystemNotation", "SystemRemark", "SystemStatus" };
        public List<string> aryDeclareName = new List<string>() { "@SystemClass", "@SystemValue", "@SystemTitle", "@SystemNotation", "@SystemRemark", "@SystemStatus" };

        /// <summary>
        /// 函數名稱    :   Index
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult Index(SystemDataDetailModels viewModel)
        {
            GetBindData("", "1", viewModel);
            return View(viewModel);
        }

        /// <summary>
        /// 函數名稱    :   Detail
        /// </summary>
        /// <param name="fSystemClass"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public PartialViewResult Detail(string fSystemClass, SystemDataDetailModels viewModel)
        {
            List<oSystemDataDetail> deList = new List<oSystemDataDetail>(); deList = null;
            deList = sddModel.listObjSystemDataDetail().Where(x => x.oSystemClass == fSystemClass && x.oSystemStatus != "D").ToList();
            viewModel.detailSystemDataDetail = deList;            
            return PartialView("Detail", viewModel);
        }

        /// <summary>
        /// 函數名稱    :   Index
        /// </summary>
        /// <param name="form"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(FormCollection form, SystemDataDetailModels viewModel)
        {
            GetBindData(form["searchValue"].ToString(), form["hidePageIndex"].ToString(), viewModel);
            return View(viewModel);
        }

        /// <summary>
        /// 函數名稱    :   GetBindData
        /// </summary>
        /// <param name="fSearchValue"></param>
        /// <param name="fPageIndex"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public PartialViewResult GetBindData(string fSearchValue, string fPageIndex , SystemDataDetailModels viewModel) {
            List<oSystemDataDetail> dataList = new List<oSystemDataDetail>();
            dataList = sddModel.listObjSystemDataDetail().Where(x=>x.oSystemStatus == "D").ToList();            
            if (!(string.IsNullOrWhiteSpace(fSearchValue))) {
                dataList = dataList.Where(x => x.oSystemClass.Contains(fSearchValue) || x.oSystemValue.Contains(fSearchValue)
                    || x.oSystemTitle.Contains(fSearchValue) || x.oSystemNotation.Contains(fSearchValue) || x.oSystemRemark.Contains(fSearchValue)
                    ).ToList();
            }
            viewModel.viewSystemDataDetail = dataList;            
            viewModel.valCountSum = dataList.Count.ToString();
            viewModel.valCountPage = "30";
            viewModel.valPageCountSum = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(viewModel.valCountSum) / Convert.ToDecimal(viewModel.valCountPage))).ToString();
            viewModel.valPageIndex = fPageIndex;
            ViewBag.valCountSum = viewModel.valCountSum;
            ViewBag.valCountPage = viewModel.valCountPage;
            ViewBag.valPageCountSum = viewModel.valPageCountSum;
            ViewBag.valPageIndex = viewModel.valPageIndex;             
            return PartialView("List", viewModel);
        }
      
        /// <summary>
        /// 函數名稱    :   Create  Post
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public RedirectResult Create(FormCollection form)
        {
            List<object> aryDataCreate = new List<object>() { 
                form["hideSystemClass"].ToString(), form["hideSystemValue"].ToString(),
                form["hideSystemTitle"].ToString(), form["hideSystemNotation"].ToString(),
                form["hideSystemRemark"].ToString(), form["hideSystemStatus"].ToString()
            };
            String funExecuteValue = dbClass.msExecuteDataBase("N", "SystemDataDetail", 0 , aryDeclareName, aryDataCreate);                
            return Redirect("~/SystemDataDetail/Index");
        }

        /// <summary>
        /// 函數名稱    :   Update  Post
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public RedirectResult Update(FormCollection form)
        {
            List<object> aryDataUpdate = new List<object>()
            {
                form["hideSystemClass"].ToString(), form["hideSystemValue"].ToString(),
                form["hideSystemTitle"].ToString(), form["hideSystemNotation"].ToString(),
                form["hideSystemRemark"].ToString(), form["hideSystemStatus"].ToString()
            };
            string funExecuteValue = dbClass.msExecuteDataBase("U", "SystemDataDetail", 2, aryDeclareName, aryDataUpdate);
            return Redirect("~/SystemDataDetail/Index");
        }

        /// <summary>
        /// 函數名稱    :   Delete
        /// </summary>
        /// <param name="vClass"></param>
        /// <param name="vValue"></param>
        /// <returns></returns>
        public RedirectResult Delete(string vClass, string vValue)
        {
            List<string> delaryDeclareName = new List<string>() { "@SystemClass", "@SystemValue" };
            List<object> aryDataDelete = new List<object>() { vClass, vValue };
            string funExecuteValue = dbClass.msExecuteDataBase("D", "SystemDataDetail", 0, delaryDeclareName, aryDataDelete);
            return Redirect("~/SystemDataDetail/Index");
        }

        /// <summary>
        /// 函數名稱    :   OutToExcel  Post
        /// </summary>
        [HttpPost]
        public void OutToExecl()
        {
            List<string> listColumn = new List<string>() { "SystemClass", "systemValue", "SystemTitle", "SystemNotation", "SystemRemark", "SystemStatus" };
            List<oSystemDataDetail> systemDetailList = new List<oSystemDataDetail>(); systemDetailList = sddModel.listObjSystemDataDetail();
            string FileName = "SystemData.xlsx"; string SheetName = "Data"; Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            XSSFWorkbook NpoiWB = new XSSFWorkbook();

            XSSFCellStyle xCellStyle = (XSSFCellStyle)NpoiWB.CreateCellStyle();
            XSSFDataFormat NpoiFormat = (XSSFDataFormat)NpoiWB.CreateDataFormat();
            xCellStyle.SetDataFormat(NpoiFormat.GetFormat("[DbNum2][$-804]0"));
            XSSFCellStyle cellStyleFontColor = (XSSFCellStyle)NpoiWB.CreateCellStyle();
            XSSFFont font1 = (XSSFFont)NpoiWB.CreateFont(); font1.Color = (short)10; font1.IsBold = true; cellStyleFontColor.SetFont(font1);

            ISheet xSheet = NpoiWB.CreateSheet(SheetName);

            IRow xRowT = xSheet.CreateRow(0); xRowT.HeightInPoints = 40;
            for (int i = 0; i < listColumn.Count; i++) {
                ICell xCellT = xRowT.CreateCell(i); xCellT.SetCellValue(listColumn[i]);
            }
            int len = 0;
            foreach (oSystemDataDetail item in systemDetailList) {
                List<string> itemData = new List<string>();
                itemData.Add(item.oSystemClass.ToString()); itemData.Add(item.oSystemValue.ToString());
                itemData.Add(item.oSystemTitle.ToString()); itemData.Add(item.oSystemNotation.ToString());
                itemData.Add(item.oSystemRemark.ToString()); itemData.Add(item.oSystemStatus.ToString());
                IRow xRowD = xSheet.CreateRow(len + 1); xRowD.HeightInPoints = 40;
                for (int b = 0; b < itemData.Count; b++) {
                    ICell xCellData = xRowD.CreateCell(b); xCellData.SetCellValue(itemData[b]);
                }
                len++;
            }
            
            MemoryStream MS = new MemoryStream(); NpoiWB.Write(MS);
            Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + "");
            Response.BinaryWrite(MS.ToArray());
            NpoiWB = null; MS.Close(); MS.Dispose(); Response.Flush(); Response.End();
            

        }

        /// <summary>
        /// 函數名稱    :   Upload      Post
        /// </summary>
        /// <param name="fileUpload"></param>
        /// <returns></returns>
        [HttpPost]
        public RedirectResult Upload(HttpPostedFileBase fileUpload)
        {
            string showDetailData = ""; DataTable dtSystemDetail = new DataTable();
            if (fileUpload != null && fileUpload.ContentLength > 0)
            {
                string FilePath = System.Web.HttpContext.Current.Server.MapPath("../excel");
                string FileName = fileUpload.FileName;
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
                    for (int i = 0; i < 6; i++)
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
                            for (int a = 0; a < 6; a++)
                            {
                                tempRow[a] = (rowD.GetCell(a).ToString() != null) ? rowD.GetCell(a).ToString() : "";
                            }
                        }
                    }
                    List<oSystemDataDetail> SystemDetailList = sddModel.listObjSystemDataDetail();
                    string valSystemClass, valSystemValue, valSystemTitle, valSystemNotation, valSystemRemark, valSystemStatus;
                    if (tempDataTable.Rows.Count > 0)
                    {
                        foreach (DataRow drtemp in tempDataTable.Rows)
                        {
                            List<object> DataList = new List<object>();
                            DataList.Add(drtemp["SystemClass"].ToString());
                            DataList.Add(drtemp["SystemValue"].ToString());
                            DataList.Add(drtemp["SystemTitle"].ToString());
                            DataList.Add(drtemp["SystemNotation"].ToString());
                            DataList.Add(drtemp["SystemRemark"].ToString());
                            DataList.Add(drtemp["SystemStatus"].ToString());
                            valSystemClass = drtemp["SystemClass"].ToString();
                            valSystemValue = drtemp["SystemValue"].ToString();
                            if (SystemDetailList.Where(x => x.oSystemClass == valSystemClass && x.oSystemValue == valSystemValue).Count() > 0)
                            {
                                dbClass.msExecuteDataBase("N", "SystemDataDetail", 0, aryDeclareName, DataList);                                
                            }
                            else
                            {
                                dbClass.msExecuteDataBase("U", "SystemDataDetail", 2, aryDeclareName, DataList);                                
                            }
                        }
                    }
                    List<oSystemDataDetail> SysDetailList = new List<oSystemDataDetail>();
                    if (SysDetailList.Count > 0)
                    {
                        foreach (oSystemDataDetail item in SysDetailList)
                        {
                            showDetailData += string.Format(@"{0}~{1}~{2}~{3}~{4}~{5},br />"
                                , item.oSystemClass, item.oSystemValue, item.oSystemTitle, item.oSystemNotation, item.oSystemRemark, item.oSystemStatus);
                        }
                    }
                }                
            }
            return Redirect("~/SystemDataDetail/Index");
        }

    }
}
