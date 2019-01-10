using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDemand.Models;

namespace MvcDemand.Controllers
{
    public class DemandDetailController : Controller
    {

        /* 函數名稱         :   DemandDetailController.cs
         * 程式人員         :   Wilson Cheng
         * 函數更新紀錄     :
         *                      2018-11-20                  新建函數
         *                      Index                       進入頁面時導入第一個函數 
         *                      Create                      進行新增需求申請單基本資料
         *                      Create Post                 進行新增表單資料到資料庫
         *                      Upload                      上傳需求申請單所需檔案到資料庫及資料夾中
         *                      returnValueToAccIndex       取得需求申請單序號
         */

        /// <summary>
        /// 定義變數
        /// </summary>
        SystemDataDetailModels sdModel = new SystemDataDetailModels();
        DemandDetailModels ddModel = new DemandDetailModels();
        AccountDetailModels adModel = new AccountDetailModels();
        AccountRelationModels arModel = new AccountRelationModels();
        ClassDataBase dbClass = new ClassDataBase();

        /// <summary>
        /// 函數名稱    :   Index
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult Index(DemandDetailModels viewModel)
        {
            viewModel.objDemandDetail = ddModel.objDemandDetailData();
            ViewBag.objDemandDetail = ddModel.objDemandDetailData();
            ViewBag.valCountSum = viewModel.objDemandDetailData().Count().ToString();

            return View(viewModel);
        }

        /// <summary>
        /// 函數名稱    :   Create
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult Create(DemandDetailModels viewModel)
        {
            viewModel.selDemandClass = sdModel.selObjSystemDataDetail("DemandClass", "請選擇", "");
            ViewBag.selDemandClass = viewModel.selDemandClass;
            viewModel.selAccountDetail = ddModel.returnSelectAccountData("", "A");
            ViewBag.selAccountDetail = viewModel.selAccountDetail;
            viewModel.selAccountDetailAgent = sdModel.selObjSystemDataDetail("", "請選擇", "");
            ViewBag.selAccountDetailAgent = viewModel.selAccountDetailAgent;
            viewModel.selAccountDetailTop = sdModel.selObjSystemDataDetail("", "請選擇", "");
            ViewBag.selAccountDetailTop = viewModel.selAccountDetailTop;
            viewModel.selAccountDetailMan = sdModel.selObjSystemDataDetail("", "請選擇", "");
            ViewBag.selAccountDetailMan = viewModel.selAccountDetailMan;
            return View(viewModel);
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
            string funMaxDemandIndex = ddModel.returnDemandMaxIndex(); 
            List<string> listSchDeclare = new List<string>() { "@DemandIndex", "@DemandStep", "@SchAccIndex", "@SchNotation", "@SchDateTime", "@SchStatus" };
            List<string> aryDeclare = new List<string>() { "@DemandIndex","@DemandDate","@DemandTitle","@DemandClass","@DemandTest"
                        ,"@DemandUpload","@DemandStep","@DemandFrom","@DemandProject","@DemandDateS"
                        ,"@DemandDateE","@DemandDateH","@DemandNotation","@DemandRemark","@DemandStatus"
                        ,"@DemandAccIndex","@DemandAgentIndex","@DemandTopIndex","@DemandManIndex", "@Update_DateTime"
                        ,"@Create_DateTime" };
            List<object> aryValue = new List<object>(){ funMaxDemandIndex, dbClass.ReturnDetailToNowDateTime("VF"), form["textDemandTitle"].ToString(), form["hideDemandClass"], form["hideDemandTest"]
                , form["hideDemandUpload"], "A", form["textDemandFrom"], form["textDemandProject"], form["textDemandDateS"]
                , "", form["textDemandDateH"], HttpUtility.HtmlEncode(form["textDemandNotation"]), HttpUtility.HtmlEncode(form["textDemandRemark"]), "X"
                , form["hideDemandAccIndex"].Replace(',',' ').Trim(), form["hideDemandAgentIndex"], form["hideDemandTopIndex"], form["hideDemandManIndex"], dbClass.ReturnDetailToNowDateTime("VF"), ""};            
            
            string fExecuteValue = dbClass.msExecuteDataBase("N", "DemandDetail", 0, aryDeclare, aryValue);
            string DemandStepST = (form["hideDemandUpload"] == "O") ? "X" : "O";
            List<object> listSchValue = new List<object>(); string fExecSchValue = "";
            if (form["hideDemandAccIndex"] != "") {
                listSchValue = new List<object>() { funMaxDemandIndex, "A", form["hideDemandAccIndex"].Replace(',', ' ').Trim(), "", dbClass.ReturnDetailToNowDateTime("VF"), DemandStepST };
                fExecSchValue = dbClass.msExecuteDataBase("N", "DemandSchedule", 0, listSchDeclare, listSchValue);
            }            
            if (form["hideDemandAgentIndex"] != "")
            {
                listSchValue = new List<object>() { funMaxDemandIndex, "A", form["hideDemandAgentIndex"].Replace(',', ' ').Trim(), "", dbClass.ReturnDetailToNowDateTime("VF"), DemandStepST };
                fExecSchValue = dbClass.msExecuteDataBase("N", "DemandSchedule", 0, listSchDeclare, listSchValue);
            }
            if (form["hideDemandTopIndex"] != "")
            {
                listSchValue = new List<object>() { funMaxDemandIndex, "C", form["hideDemandTopIndex"].Replace(',', ' ').Trim(), "", dbClass.ReturnDetailToNowDateTime("VF"), "X" };
                fExecSchValue = dbClass.msExecuteDataBase("N", "DemandSchedule", 0, listSchDeclare, listSchValue);
            }
            if (form["hideDemandManIndex"] != "")
            {
                listSchValue = new List<object>() { funMaxDemandIndex, "D", form["hideDemandManIndex"].Replace(',', ' ').Trim(), "", dbClass.ReturnDetailToNowDateTime("VF"), "X" };
                fExecSchValue = dbClass.msExecuteDataBase("N", "DemandSchedule", 0, listSchDeclare, listSchValue);
            }
            if (form["hideDemandUpload"] == "O")
            {
                return Redirect("~/DemandDetail/Index");            
            } else {
                return Redirect("~/DemandDetail/Upload?fDemandIndex=" + funMaxDemandIndex);            
            }
            
        }

        /// <summary>
        /// 函數名稱    :   Upload
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="fDemandIndex"></param>
        /// <returns></returns>
        public ActionResult Upload(DemandDetailModels viewModel, string fDemandIndex)
        {
            viewModel.vDemandIndex = fDemandIndex;
            ViewBag.vDemandIndex = fDemandIndex;
            return View(viewModel);
        }
        
        /// <summary>
        /// 函數名稱    :   returnValueToAccIndex
        /// </summary>
        /// <param name="fAccIndex"></param>
        /// <returns></returns>
        public string returnValueToAccIndex(string fAccIndex)
        {
            string returnValue = ""; string oAccDeptNo = "";
            List<oAccountDetail> delList = adModel.listObjAccountDetail().Where(x => x.oAccIndex == fAccIndex).ToList();
            oAccDeptNo = (delList.Count > 0) ? delList[0].oAccDeptNo.ToString() : "";
            List<oAccountRelation> ardListB = arModel.listAccountRelation().Where(x => x.oAccDeptNo == oAccDeptNo && x.oRelationClass == "B").ToList();
            List<oAccountRelation> ardListC = arModel.listAccountRelation().Where(x => x.oAccDeptNo == oAccDeptNo && x.oRelationClass == "C").ToList();
            List<oAccountRelation> ardListD = arModel.listAccountRelation().Where(x => x.oAccDeptNo == oAccDeptNo && x.oRelationClass == "E").ToList();            
            returnValue = oAccDeptNo;
            if (ardListB.Count > 0) {
                returnValue += "^";
                foreach (oAccountRelation itemA in ardListB) {
                    returnValue += string.Format(@"{0}_{1}_{2}{3}", itemA.oAccIndex, itemA.oAccNo, itemA.oAccName, "|");
                }
            } else { returnValue += "^Zero"; }
            if (ardListC.Count > 0) {
                returnValue += "^";
                foreach (oAccountRelation itemB in ardListC) {
                    returnValue += string.Format(@"{0}_{1}_{2}{3}", itemB.oAccIndex, itemB.oAccNo, itemB.oAccName, "|");
                }
            } else { returnValue += "^Zero"; }
            if (ardListD.Count > 0) {
                returnValue += "^";
                foreach (oAccountRelation itemC in ardListD) {
                    returnValue += string.Format(@"{0}_{1}_{2}{3}", itemC.oAccIndex, itemC.oAccNo, itemC.oAccName, "|");
                }
            } else { returnValue += "^Zero"; }
            return returnValue;
        }

        public ActionResult Sign(string fDemandIndex, DemandDetailModels viewModel)
        {
            List<oDemandDetail> dDemandDetail = new List<oDemandDetail>();
            List<oDemandSchedule> dDemandSchedule = new List<oDemandSchedule>();
            dDemandDetail = ddModel.objDemandDetailData();
            dDemandDetail = dDemandDetail.Where(x => x.oDemandIndex == fDemandIndex).ToList();
            if (dDemandDetail.Count > 0)
            {
                viewModel.vDemandTitle = dDemandDetail[0].oDemandTitle.ToString();
                viewModel.vDemandNotation = HttpUtility.HtmlDecode(dDemandDetail[0].oDemandNotation);
                viewModel.vDemandDate = string.Format(@"{0}/{1}/{2}", dDemandDetail[0].oDemandDate.Substring(0, 4), dDemandDetail[0].oDemandDate.Substring(4, 2), dDemandDetail[0].oDemandDate.Substring(6, 2));                
            }
            dDemandSchedule = ddModel.objDemandSchedule();
            dDemandSchedule = dDemandSchedule.Where(x => x.oDemandIndex == fDemandIndex && x.oSchStatus == "X").OrderBy(x => x.oDemandStep).ToList();
            if (dDemandSchedule.Count > 0)
            {
                viewModel.vDemandStep = dDemandSchedule[0].oDemandStep.ToString();
                viewModel.vDemandStepTitle = sdModel.detailObjSystemDataDetail("DemandStep", viewModel.vDemandStep, "").ToList()[0].oSystemTitle;
                viewModel.vSchAccIndex = dDemandSchedule[0].oSchAccIndex.ToString();
            }
            viewModel.vDemandIndex = fDemandIndex;
            ViewBag.vDemandIndex = viewModel.vDemandIndex;
            ViewBag.vDemandStep = viewModel.vDemandStep;
            ViewBag.vDemandStepTitle = viewModel.vDemandStepTitle;
            viewModel.selDemandScheduleStatus = sdModel.selObjSystemDataDetail("SchStatus", "請選擇", "");
            ViewBag.selDemandScheduleStatus = viewModel.selDemandScheduleStatus;
            viewModel.vSchAccName = adModel.listObjAccountDetail().Where(x => x.oAccIndex == viewModel.vSchAccIndex).ToList()[0].oAccName;
            return View(viewModel);
        }

    }
}
