﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MvcDemand.Models;

namespace MvcDemand.Models
{
    public class DemandDetailModels
    {
        ClassDataBase dbClass = new ClassDataBase();
        AccountRelationModels arModel = new AccountRelationModels();
        SystemDataDetailModels sdModel = new SystemDataDetailModels();
        AccountDetailModels adModel = new AccountDetailModels();

        public List<oDemandDetail> objDemandDetail = new List<oDemandDetail>();
        public List<SelectListItem> selAccountDetail = new List<SelectListItem>();
        public List<SelectListItem> selAccountDetailAgent = new List<SelectListItem>();
        public List<SelectListItem> selAccountDetailTop = new List<SelectListItem>();
        public List<SelectListItem> selAccountDetailMan = new List<SelectListItem>();
        public List<SelectListItem> selDemandClass = new List<SelectListItem>();
        public List<SelectListItem> selDemandScheduleStatus = new List<SelectListItem>();
        public List<oDemandSchedule> objDemandSchedule = new List<oDemandSchedule>();
        public List<SelectListItem> selAccountRelatClassH = new List<SelectListItem>();
        public List<SelectListItem> selAccountRelatClassI = new List<SelectListItem>();
        public List<SelectListItem> selAccountRelatClassJ = new List<SelectListItem>();

        
        public string vDemandIndex { get; set; }
        public string vDemandDate { get; set; }
        public string vDemandTitle { get; set; }
        public string vDemandClass { get; set; }
        public string vDemandTest { get; set; }
        public string vDemandUpload { get; set; }
        public string vDemandStep { get; set; }
        public string vDemandFrom { get; set; }
        public string vDemandProject { get; set; }
        public string vDemandDateS { get; set; }
        public string vDemandDateE { get; set; }
        public string vDemandDateH { get; set; }
        public string vDemandNotation { get; set; }
        public string vDemandRemark { get; set; }
        public string vDemandStatus { get; set; }
        public string vDemandAccIndex { get; set; }
        public string vDemandAgentIndex { get; set; }
        public string vDemandTopIndex { get; set; }
        public string vDemandManIndex { get; set; }
        public string vUpdate_DateTime { get; set; }
        public string vCreate_DateTime { get; set; }
        public string vDemandClassTitle { get; set; }
        public string vDemandStepTitle { get; set; }
        public string vDemandStatusTitle { get; set; }
        public string vDemandAccNumber { get; set; }
        public string vDemandAccNumberName { get; set; }
        public string vDemandAgentNumber { get; set; }
        public string vDemandAgentName { get; set; }
        public string vDemandTopNumber { get; set; }
        public string vDemandTopName { get; set; }
        public string vDemandManNumber { get; set; }
        public string vDemandManName { get; set; }
        public string vSchAccIndex { get; set; }
        public string vSchAccName { get; set; }
        public string vSchNotation { get; set; }
        public string vSchDateTime { get; set; }
        public string vSchStatus { get; set; }
        public string vSchStatusTitle { get; set; }
        public string valPageIndex { get; set; }
        public string valPageCountSum { get; set; }
        public string valCountSum { get; set; }
        public string valCountPage { get; set; }
        public string funQuerySQL { get; set; }
        public string funExecuteSQL { get; set; }
        public string funReturnValue { get; set; }
        public string funExecuteValue { get; set; }
        public string valShowDetail { get; set; }
        public string valDetailCount { get; set; }
        public string aryAccountDetail { get; set; }
        public string aryAccountDeptData { get; set; }
        public string sDemandDetail { get; set; }

        /// <summary>
        /// 函數名稱    :   returnSelectAccountData
        /// </summary>
        /// <param name="fAccDeptNo"></param>
        /// <param name="fRelClass"></param>
        /// <returns></returns>
        public List<SelectListItem> returnSelectAccountData(string fAccDeptNo, string fRelClass)
        {
            List<oAccountRelation> listAcc = new List<oAccountRelation>();
            listAcc = arModel.listAccountRelation();            
            List<SelectListItem> list = new List<SelectListItem>();
            if (fAccDeptNo != "") { listAcc = listAcc.Where(x => x.oAccDeptNo == fAccDeptNo ).ToList(); }
            if (fRelClass != "") { listAcc = listAcc.Where(x => x.oRelationClass == fRelClass).ToList(); }
            if (listAcc.Count > 0)
            {
                list.Add(new SelectListItem() { Value = "", Text= "請選擇" });
                foreach (oAccountRelation item in listAcc) {
                    list.Add(new SelectListItem() { Value = item.oAccIndex.ToString(), Text = string.Format(@"({2})({0}){1}", item.oAccNo.ToString(), item.oAccName.ToString(), item.oAccIndex.ToString()) });
                }
            }
            return list;
        }

        /// <summary>
        /// 函數名稱    :   returnDataTableDemandDetail
        /// </summary>
        /// <returns></returns>
        public DataTable returnDataTableDemandDetail(string fDemandIndex)
        {
            Dictionary<string, object> funDicParas = new Dictionary<string,object>(); 
            DataTable rtnDT = new DataTable(); funQuerySQL = ""; funDicParas = null;
            try {
                funQuerySQL = string.Format(@"Select distinct dd.*, sda.systemtitle as DemandClassTitle 
                                , sdb.SystemTitle as DemandStepTitle, sdc.systemtitle as DemandStatusTitle
                                , isnull(ada.AccNo,'') as AccNumber, isnull(ada.AccName,'') as AccName
                                , isnull(adb.AccNo,'') as AgentNumber, isnull(adb.accname,'') as AgentName
                                , isnull(adc.AccNo,'') as TopNumber, isnull(adc.AccName,'') as TopName
                                , isnull(ade.AccNo,'') as ManNumber, isnull(ade.AccName,'') as ManName
                                from DemandDetail dd 
                                inner join DemandSchedule ds on ds.DemandIndex = dd.DemandIndex
	                            inner join SystemDataDetail sda on sda.SystemClass='DemandClass' and sda.SystemValue = dd.DemandClass
	                            inner join SystemDataDetail sdb on sdb.SystemClass='DemandStep' and sdb.SystemValue= dd.DemandStep
	                            inner join SystemDataDetail sdc on sdc.SystemClass='DemandStatus' and sdc.SystemValue=dd.DemandStatus
	                            left join AccountDetail ada on ada.AccIndex=dd.DemandAccIndex 
	                            left join AccountDetail adb on adb.AccIndex = dd.DemandAgentIndex
	                            left join AccountDetail adc on adc.AccIndex = dd.DemandTopIndex
	                            left join AccountDetail ade on ade.AccIndex = dd.DemandManIndex
                                where 1=1");
                if (fDemandIndex != "") {
                    if (fDemandIndex != "00019") {
                        funQuerySQL += string.Format(@" and dd.DemandIndex in (select DemandIndex from DemandSchedule where SchAccIndex='{0}') ", fDemandIndex);
                    }
                }
                rtnDT = dbClass.msDataTableToDataBase(funQuerySQL, funDicParas);
            } catch (Exception ex) {
                rtnDT.Clear();
            }
            return rtnDT;
        }

        /// <summary>
        /// 函數名稱    :   objDemandDetailData
        /// </summary>
        /// <returns></returns>
        public List<oDemandDetail> objDemandDetailData()
        {
            List<oDemandDetail> list = new List<oDemandDetail>();
            try {
                list = (from dt in returnDataTableDemandDetail("").AsEnumerable()
                        select new oDemandDetail
                        {
                            oDemandIndex = dt.Field<string>("DemandIndex"), oDemandDate = dt.Field<string>("DemandDate"),
                            oDemandTitle = dt.Field<string>("DemandTitle"), oDemandClass = dt.Field<string>("DemandClass"),
                            oDemandTest = dt.Field<string>("DemandTest"),  oDemandUpload = dt.Field<string>("DemandUpload"),
                            oDemandStep = dt.Field<string>("DemandStep"),  oDemandFrom = dt.Field<string>("DemandFrom"),
                            oDemandProject = dt.Field<string>("DemandProject"), oDemandDateS = dt.Field<string>("DemandDateS"),
                            oDemandDateE = dt.Field<string>("DemandDateE"), oDemandDateH = dt.Field<string>("DemandDateH"),
                            oDemandNotation = dt.Field<string>("DemandNotation"), oDemandRemark = dt.Field<string>("DemandRemark"),
                            oDemandStatus = dt.Field<string>("DemandStatus"), oDemandAccIndex = dt.Field<string>("DemandAccIndex"),
                            oDemandAgentIndex = dt.Field<string>("DemandAgentIndex"), oDemandTopIndex = dt.Field<string>("DemandTopIndex"),
                            oDemandManIndex = dt.Field<string>("DemandManIndex"), oUpdate_DateTime = dt.Field<string>("Update_DateTime"),
                            oCreate_DateTime = dt.Field<string>("Create_DateTime"),
                            oDemandClassTitle = dt.Field<string>("DemandClassTitle"),
                            oDemandStepTitle = dt.Field<string>("DemandStepTitle"),
                            oDemandStatusTitle = dt.Field<string>("DemandStatusTitle"),
                            oDemandAccNumber = dt.Field<string>("AccNumber"),
                            oDemandAccNumberName = dt.Field<string>("AccName"),
                            oDemandAgentNumber = dt.Field<string>("AgentNumber"),
                            oDemandAgentName = dt.Field<string>("AgentName"),
                            oDemandTopNumber = dt.Field<string>("TopNumber"),
                            oDemandTopName = dt.Field<string>("TopName"),
                            oDemandManNumber = dt.Field<string>("ManNumber"),
                            oDemandManName = dt.Field<string>("ManName")
                        }).ToList();
            } catch (Exception ex) {
                list = null;
            }
            return list;
        }

        public List<oDemandDetail> objDemandDetailDataSchedule(string fAccIndex)
        {
            List<oDemandDetail> list = new List<oDemandDetail>();
            DataTable rtnDT = new DataTable();
            rtnDT = returnDataTableDemandDetail(fAccIndex);
            list = (from dt in rtnDT.AsEnumerable()
                    select new oDemandDetail
                    {
                        oDemandIndex = dt.Field<string>("DemandIndex"),
                        oDemandDate = dt.Field<string>("DemandDate"),
                        oDemandTitle = dt.Field<string>("DemandTitle"),
                        oDemandClass = dt.Field<string>("DemandClass"),
                        oDemandTest = dt.Field<string>("DemandTest"),
                        oDemandUpload = dt.Field<string>("DemandUpload"),
                        oDemandStep = dt.Field<string>("DemandStep"),
                        oDemandFrom = dt.Field<string>("DemandFrom"),
                        oDemandProject = dt.Field<string>("DemandProject"),
                        oDemandDateS = dt.Field<string>("DemandDateS"),
                        oDemandDateE = dt.Field<string>("DemandDateE"),
                        oDemandDateH = dt.Field<string>("DemandDateH"),
                        oDemandNotation = dt.Field<string>("DemandNotation"),
                        oDemandRemark = dt.Field<string>("DemandRemark"),
                        oDemandStatus = dt.Field<string>("DemandStatus"),
                        oDemandAccIndex = dt.Field<string>("DemandAccIndex"),
                        oDemandAgentIndex = dt.Field<string>("DemandAgentIndex"),
                        oDemandTopIndex = dt.Field<string>("DemandTopIndex"),
                        oDemandManIndex = dt.Field<string>("DemandManIndex"),
                        oUpdate_DateTime = dt.Field<string>("Update_DateTime"),
                        oCreate_DateTime = dt.Field<string>("Create_DateTime"),
                        oDemandClassTitle = dt.Field<string>("DemandClassTitle"),
                        oDemandStepTitle = dt.Field<string>("DemandStepTitle"),
                        oDemandStatusTitle = dt.Field<string>("DemandStatusTitle"),
                        oDemandAccNumber = dt.Field<string>("AccNumber"),
                        oDemandAccNumberName = dt.Field<string>("AccName"),
                        oDemandAgentNumber = dt.Field<string>("AgentNumber"),
                        oDemandAgentName = dt.Field<string>("AgentName"),
                        oDemandTopNumber = dt.Field<string>("TopNumber"),
                        oDemandTopName = dt.Field<string>("TopName"),
                        oDemandManNumber = dt.Field<string>("ManNumber"),
                        oDemandManName = dt.Field<string>("ManName")
                    }).ToList();
            return list;
        }

        /// <summary>
        /// 函數名稱    :   returnDataTableToDemandSchedule
        /// </summary>
        /// <returns></returns>
        public DataTable returnDataTableToDemandSchedule()
        {
            Dictionary<string, object> funDicPara = new Dictionary<string, object>();
            DataTable rtnDT = new DataTable(); funQuerySQL = ""; funDicPara = null;
            funQuerySQL = string.Format(@"select ds.*, sd.SystemTitle as DemandStepTitle, ad.AccNo, ad.AccName 
                            from DemandSchedule ds
                                inner join SystemDataDetail sd on sd.SystemClass='DemandStep' and sd.SystemValue = ds.DemandStep
                                inner join AccountDetail ad on ad.AccIndex = ds.SchAccIndex  ");
            rtnDT = dbClass.msDataTableToDataBase(funQuerySQL, funDicPara);
            return rtnDT;
        }

        /// <summary>
        /// 函數名稱    :   listDemandSchedule
        /// </summary>
        /// <returns></returns>
        public List<oDemandSchedule> listDemandSchedule()
        {
            List<oDemandSchedule> list = new List<oDemandSchedule>();
            DataTable dt = returnDataTableToDemandSchedule();
            list = (from d in dt.AsEnumerable()
                    select new oDemandSchedule
                    {
                        oDemandIndex = d.Field<string>("DemandIndex"),
                        oDemandStep = d.Field<string>("DemandStep"),
                        oDemandStepTitle = d.Field<string>("DemandStepTitle"),
                        oSchAccIndex = d.Field<string>("SchAccIndex"),
                        oSchAccNo = d.Field<string>("AccNo"),
                        oSchAccName = d.Field<string>("AccName"),
                        oSchDateTime = d.Field<string>("SchDateTime"),
                        oSchNotation = d.Field<string>("SchNotation"),
                        oSchStatus = d.Field<string>("SchStatus")
                    }).ToList();
            return list;
        }

        /// <summary>
        /// 函數名稱    :   rtnDataTableToDemandSchedule
        /// </summary>
        /// <returns></returns>
        public DataTable rtnDataTableToDemandSchedule()
        {
            Dictionary<string, object> funDicPara = new Dictionary<string, object>();
            DataTable rtnDT = new DataTable(); funQuerySQL = ""; funDicPara = null;
            funQuerySQL = string.Format(@"
                    Select distinct ds.*, dd.*, sda.systemtitle as DemandClassTitle 
                                , sdb.SystemTitle as DemandStepTitle, sdc.systemtitle as DemandStatusTitle
                                , isnull(ada.AccNo,'') as AccNumber, isnull(ada.AccName,'') as AccName
                                , isnull(adb.AccNo,'') as AgentNumber, isnull(adb.accname,'') as AgentName
                                , isnull(adc.AccNo,'') as TopNumber, isnull(adc.AccName,'') as TopName
                                , isnull(ade.AccNo,'') as ManNumber, isnull(ade.AccName,'') as ManName
                                , isnull(dsch.AccNo, '') as SchAccNo, ISNULL(dsch.AccName, '') as SchAccName 
                                from DemandSchedule ds
                                left join DemandDetail dd on ds.DemandIndex = dd.DemandIndex
	                            inner join SystemDataDetail sda on sda.SystemClass='DemandClass' and sda.SystemValue = dd.DemandClass
	                            inner join SystemDataDetail sdb on sdb.SystemClass='DemandStep' and sdb.SystemValue= dd.DemandStep
	                            inner join SystemDataDetail sdc on sdc.SystemClass='DemandStatus' and sdc.SystemValue=dd.DemandStatus
	                            left join AccountDetail ada on ada.AccIndex=dd.DemandAccIndex 
	                            left join AccountDetail adb on adb.AccIndex = dd.DemandAgentIndex
	                            left join AccountDetail adc on adc.AccIndex = dd.DemandTopIndex
	                            left join AccountDetail ade on ade.AccIndex = dd.DemandManIndex
                                left join AccountDetail dsch on dsch.AccIndex = ds.SchAccIndex 
                    ");
            rtnDT = dbClass.msDataTableToDataBase(funQuerySQL, funDicPara);
            return rtnDT;
        }

        /// <summary>
        /// 函數名稱    :   listDemandDetailAndSchedule
        /// </summary>
        /// <returns></returns>
        public List<oDemandDetailAndSchedule> listDemandDetailAndSchedule()
        {
            List<oDemandDetailAndSchedule> list = new List<oDemandDetailAndSchedule>();
            DataTable rtnDT = new DataTable();
            rtnDT = rtnDataTableToDemandSchedule();
            list = (from dt in rtnDT.AsEnumerable()
                    select new oDemandDetailAndSchedule
                    {
                        oDemandIndex = dt.Field<string>("DemandIndex"),
                        oDemandDate = dt.Field<string>("DemandDate"),
                        oDemandTitle = dt.Field<string>("DemandTitle"),
                        oDemandClass = dt.Field<string>("DemandClass"),
                        oDemandTest = dt.Field<string>("DemandTest"),
                        oDemandUpload = dt.Field<string>("DemandUpload"),
                        oDemandStep = dt.Field<string>("DemandStep"),
                        oDemandFrom = dt.Field<string>("DemandFrom"),
                        oDemandProject = dt.Field<string>("DemandProject"),
                        oDemandDateS = dt.Field<string>("DemandDateS"),
                        oDemandDateE = dt.Field<string>("DemandDateE"),
                        oDemandDateH = dt.Field<string>("DemandDateH"),
                        oDemandNotation = dt.Field<string>("DemandNotation"),
                        oDemandRemark = dt.Field<string>("DemandRemark"),
                        oDemandStatus = dt.Field<string>("DemandStatus"),
                        oDemandAccIndex = dt.Field<string>("DemandAccIndex"),
                        oDemandAgentIndex = dt.Field<string>("DemandAgentIndex"),
                        oDemandTopIndex = dt.Field<string>("DemandTopIndex"),
                        oDemandManIndex = dt.Field<string>("DemandManIndex"),
                        oDemandClassTitle = dt.Field<string>("DemandClassTitle"),
                        oDemandStepTitle = dt.Field<string>("DemandStepTitle"),
                        oDemandStatusTitle = dt.Field<string>("DemandStatusTitle"),
                        oDemandAccNumber = dt.Field<string>("AccNumber"),
                        oDemandAccNumberName = dt.Field<string>("AccName"),
                        oDemandAgentNumber = dt.Field<string>("AgentNumber"),
                        oDemandAgentName = dt.Field<string>("AgentName"),
                        oDemandTopNumber = dt.Field<string>("TopNumber"),
                        oDemandTopName = dt.Field<string>("TopName"),
                        oDemandManNumber = dt.Field<string>("ManNumber"),
                        oDemandManName = dt.Field<string>("ManName"),
                        oSchAccIndex = dt.Field<string>("SchAccIndex"),
                        oSchAccNo = dt.Field<string>("SchAccNo"),
                        oSchAccName = dt.Field<string>("SchAccName"),
                        oSchNotation = dt.Field<string>("SchNotation"),
                        oSchDateTime = dt.Field<string>("SchDateTime"),
                        oSchStatus = dt.Field<string>("SchStatus")
                    }).ToList();
            return list;
        }

        /// <summary>
        /// 函數名稱    :   returnDemandMaxIndex
        /// </summary>
        /// <returns></returns>
        public string returnDemandMaxIndex()
        {
            funReturnValue = ""; funQuerySQL = ""; DataTable rtnDT = new DataTable(); string funMaxIndex = "";
            List<string> fDeclare = new List<string>(); List<object> fValue = new List<object>();
            Dictionary<string,object> dicPara = new Dictionary<string,object>(); dicPara = null;
            try {
                string nowMonth = dbClass.ReturnDetailToNowDateTime("VM");
                funQuerySQL = string.Format(@"select isnull(max(DemandIndex),'') as MaxIndex from DemandDetail 
                             where substring(DemandDate,1,6)='{0}' ", nowMonth);
                rtnDT = dbClass.msDataTableToDataBase(funQuerySQL, dicPara);
                if (rtnDT.Rows.Count > 0)
                {
                    if (rtnDT.Rows[0]["MaxIndex"].ToString() != "")
                    {
                        funMaxIndex = rtnDT.Rows[0]["MaxIndex"].ToString();
                        funMaxIndex = nowMonth + Convert.ToInt32(Convert.ToInt32(funMaxIndex.Substring(6, 4)) + 1).ToString().PadLeft(4, '0').ToString();
                    } else
                    {
                        funMaxIndex = nowMonth + "0001";
                    }                    
                } else {
                    funMaxIndex = nowMonth+ "0001";
                }
                funReturnValue = funMaxIndex;
            }
            catch (Exception ex)
            {
                funReturnValue = ex.Message;
            }
            return funReturnValue;
        }

        public string showDetailDemandDetail(string fDemandIndex)
        {
            funReturnValue = ""; 
            List<oDemandDetail> listD = new List<oDemandDetail>();
            List<oDemandSchedule> listS = new List<oDemandSchedule>();
            listD = objDemandDetailData(); listS = listDemandSchedule();
            listD = listD.Where(x => x.oDemandIndex == fDemandIndex).ToList();            
            listS = listS.Where(x => x.oDemandIndex == fDemandIndex).ToList();
            int SchIndex = 1;
            string funScheduleDetail = string.Format(@"<table style='width:100%' border='0'>");            
            foreach (oDemandSchedule item in listS)
            {
                funScheduleDetail += string.Format(@"
                    <tr style='height:30px'>
                        <td style='width:50px; text-align:center'>{0}</td>
                        <td style='width:50px; text-align:center'>{1}</td>
                        <td style='width:150px; text-align:left'>{2}</td>
                        <td style='width:200px; text-align:left'>({3}) {4}</td>
                        <td style='width:50px; text-align:center'>{5}</td>
                        <td style='width:250px; text-align:left'>{6}</td>
                    </tr><tr><td colspan='6'><hr></td></tr>"
                , SchIndex.ToString(), item.oDemandStep, item.oDemandStepTitle
                , item.oSchAccNo, item.oSchAccName, item.oSchStatus, HttpUtility.HtmlDecode(item.oSchNotation));
                SchIndex++;
            }
            funScheduleDetail += string.Format(@"</table>");

            funReturnValue = string.Format(@"
                <table style='width:900px; font-size:15px; font-weight:bold' border='1' cellpadding='0' cellspacing='0'>
                    <tr style='height:45px; font-weight:20px; font-weight:bold; color:blue'>
                        <td style='width:100%; text-align:center' colspan='4'>系統需求申請單</td>
                    </tr>
                    <tr style='height:35px'>
                        <td style='width:150px; text-align:center; color:blue'>申請單號</td>
                        <td style='width:300px; text-align:left; color:black'>{0}</td>
                        <td style='width:150px; text-align:center; color:blue'>申請日期</td>
                        <td style='width:300px; text-align:left; color:black'>{1}</td>  
                    </tr>
                    <tr style='height:35px;'>
                        <td style='width:150px; text-align:center; color:blue'>申請帳號</td>
                        <td style='width:300px; text-align:left; color:black'>({2}) {3}</td>
                        <td style='width:150px; text-align:center; color:blue'>申請單位</td>
                        <td style='width:300px; text-align:left; color:black'>{4} {5}</td>  
                    </tr>
                    <tr style='height:35px;'>
                        <td style='width:150px; text-align:center; color:blue'>需求類別</td>
                        <td style='width:300px; text-align:left; color:black'>{6}</td>
                        <td style='width:150px; text-align:center; color:blue'>代理人</td>
                        <td style='width:300px; text-align:left; color:black'>({7}) {8}</td>  
                    </tr>
                    <tr style='height:35px;'>
                        <td style='width:150px; text-align:center; color:blue'>上級主管</td>
                        <td style='width:300px; text-align:left; color:black'>({9}) {10}</td>
                        <td style='width:150px; text-align:center; color:blue'>二級主管</td>
                        <td style='width:300px; text-align:left; color:black'>({11}) {12}</td>  
                    </tr>
                    <tr style='height:35px;'>
                        <td style='width:150px; text-align:center;color:blue'>到期日期</td>
                        <td style='width:300px; text-align:left; color:black'>{13}</td>
                        <td style='width:150px; text-align:center;color:blue'>開發後測試</td>
                        <td style='width:300px; text-align:left; color:black'>{14}</td>  
                    </tr>
                    <tr style='height:35px;'>
                        <td style='width:150px; text-align:center; color:blue'>需求單狀態</td>
                        <td style='width:300px; text-align:left; color:black'>{15}</td>
                        <td style='width:150px; text-align:center; color:blue'>申請進度</td>
                        <td style='width:300px; text-align:left; color:black'>({16}) {17}</td>  
                    </tr>
                    <tr style='height:35px;'>
                        <td style='width:150px; text-align:center; color:blue'>需求單標題</td>
                        <td style='750px; text-align:left' colspan='3'>{18}</td>
                    </tr>
                    <tr style='height:35px;'>
                        <td style='width:150px; text-align:center; color:blue'>需求單來源</td>
                        <td style='750px; text-align:left' colspan='3'>{19}</td>
                    </tr>
                    <tr style='height:35px;'>
                        <td style='width:150px; text-align:center; color:blue'>所屬專案系統</td>
                        <td style='750px; text-align:left' colspan='3'>{20}</td>
                    </tr>
                    <tr style='height:35px; vertical-align:top'>
                        <td style='width:150px; text-align:center; color:blue'>需求單說明內容</td>
                        <td style='750px; text-align:left' colspan='3'>{21}</td>
                    </tr>
                    <tr style='height:35px; vertical-align:top'>
                        <td style='width:150px; text-align:center; color:blue'>需求單備註內容</td>
                        <td style='750px; text-align:left' colspan='3'>{22}</td>
                    </tr>
                    <tr style='height:35px; vertical-align:top'>
                        <td style='width:150px; text-align:center; color:blue'>需求單簽核進度</td>
                        <td style='750px; text-align:left' colspan='3'>{23}</td>
                    </tr>
                </table>
            "
            , fDemandIndex, listD[0].oDemandDateS.ToString(), listD[0].oDemandAccNumber, listD[0].oDemandAccNumberName
            , sdModel.detailObjSystemDataDetail("AccDeptNo", adModel.returnAccountDetailValue(listD[0].oDemandAccIndex, listD[0].oDemandAccNumber, "AccDeptNo"), "").ToList()[0].oSystemTitle.ToString()
            , sdModel.detailObjSystemDataDetail("AccJobNo", adModel.returnAccountDetailValue(listD[0].oDemandAccIndex, listD[0].oDemandAccNumber, "AccJobNo"), "").ToList()[0].oSystemTitle.ToString()
            , listD[0].oDemandClassTitle, listD[0].oDemandAccNumber, listD[0].oDemandAgentName, listD[0].oDemandTopNumber, listD[0].oDemandTopName
            , listD[0].oDemandManNumber, listD[0].oDemandManName, listD[0].oDemandDateH, listD[0].oDemandTest, listD[0].oDemandStatusTitle
            , listD[0].oDemandStep, listD[0].oDemandStepTitle, listD[0].oDemandTitle, listD[0].oDemandFrom , listD[0].oDemandProject
            , HttpUtility.HtmlDecode(listD[0].oDemandNotation), HttpUtility.HtmlDecode(listD[0].oDemandRemark), funScheduleDetail.ToString()
            );
            
            return funReturnValue;
        }

    }
    
    public class oDemandDetail
    {
        public string oDemandIndex { get; set; }
        public string oDemandDate { get; set; }
        public string oDemandTitle { get; set; }
        public string oDemandClass { get; set; }
        public string oDemandTest { get; set; }
        public string oDemandUpload { get; set; }
        public string oDemandStep { get; set; }
        public string oDemandFrom { get; set; }
        public string oDemandProject { get; set; }
        public string oDemandDateS { get; set; }
        public string oDemandDateE { get; set; }
        public string oDemandDateH { get; set; }
        public string oDemandNotation { get; set; }
        public string oDemandRemark { get; set; }
        public string oDemandStatus { get; set; }
        public string oDemandAccIndex { get; set; }
        public string oDemandAgentIndex { get; set; }
        public string oDemandTopIndex { get; set; }
        public string oDemandManIndex { get; set; }
        public string oUpdate_DateTime { get; set; }
        public string oCreate_DateTime { get; set; }
        public string oDemandClassTitle { get; set; }
        public string oDemandStepTitle { get; set; }
        public string oDemandStatusTitle { get; set; }
        public string oDemandAccNumber { get; set; }
        public string oDemandAccNumberName { get; set; }
        public string oDemandAgentNumber { get; set; }
        public string oDemandAgentName { get; set; }
        public string oDemandTopNumber { get; set; }
        public string oDemandTopName { get; set; }
        public string oDemandManNumber { get; set; }
        public string oDemandManName { get; set; }
    }

    public class oDemandSchedule
    {
        public string oDemandIndex { get; set; }
        public string oDemandStep { get; set; }
        public string oDemandStepTitle { get; set; }
        public string oSchAccIndex { get; set; }
        public string oSchAccNo { get; set; }
        public string oSchAccName { get; set; }
        public string oSchNotation { get; set; }
        public string oSchDateTime { get; set; }
        public string oSchStatus { get; set; }
    }

    public class oDemandDetailAndSchedule
    {
        public string oDemandIndex { get; set; }
        public string oDemandDate { get; set; }
        public string oDemandTitle { get; set; }
        public string oDemandClass { get; set; }
        public string oDemandTest { get; set; }
        public string oDemandUpload { get; set; }
        public string oDemandStep { get; set; }
        public string oDemandFrom { get; set; }
        public string oDemandProject { get; set; }
        public string oDemandDateS { get; set; }
        public string oDemandDateE { get; set; }
        public string oDemandDateH { get; set; }
        public string oDemandNotation { get; set; }
        public string oDemandRemark { get; set; }
        public string oDemandStatus { get; set; }
        public string oDemandAccIndex { get; set; }
        public string oDemandAgentIndex { get; set; }
        public string oDemandTopIndex { get; set; }
        public string oDemandManIndex { get; set; }
        public string oDemandClassTitle { get; set; }
        public string oDemandStepTitle { get; set; }
        public string oDemandStatusTitle { get; set; }
        public string oDemandAccNumber { get; set; }
        public string oDemandAccNumberName { get; set; }
        public string oDemandAgentNumber { get; set; }
        public string oDemandAgentName { get; set; }
        public string oDemandTopNumber { get; set; }
        public string oDemandTopName { get; set; }
        public string oDemandManNumber { get; set; }
        public string oDemandManName { get; set; }
        public string oSchAccIndex { get; set; }
        public string oSchAccNo { get; set; }
        public string oSchAccName { get; set; }
        public string oSchNotation { get; set; }
        public string oSchDateTime { get; set; }
        public string oSchStatus { get; set; }
    }

}