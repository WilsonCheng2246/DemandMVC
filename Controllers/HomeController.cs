using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDemand.Models;
using System.Data;

namespace MvcDemand.Controllers
{
    public class HomeController : Controller
    {

        /* 函數名稱         :   HomeController.cs
         * 程式人員         :   Wilson Cheng
         * 函數更新紀錄     :
         *                      2018-11-20                  新建函數
         *                      Index                       進入頁面時導入第一個函數 
         *                      Login                       登入會員帳號密碼頁面
         *                      LogOut                      登出系統回到登入首頁
         *                      LoginSessionValue           會員帳號密碼正確後建立Session值
         *                      returnCheckLoginData        驗證會員帳號密碼是否正確
         *                      funExecutePassword          修改會員帳號密碼
         */

        /// <summary>
        /// 定義變數
        /// </summary>
        ClassDataBase dbClass = new ClassDataBase();
        AccountDetailModels adModel = new AccountDetailModels();
        AccountRelationModels arModel = new AccountRelationModels();
        DemandDetailModels ddModel = new DemandDetailModels();

        /// <summary>
        /// 函數名稱    :   Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Session["AccIndex"] == null)
            {
                return Redirect("~/Home/Login");
            } else {
                List<oDemandDetail> objDemandDetail = new List<oDemandDetail>();
                objDemandDetail = ddModel.objDemandDetailData();

                return View();
            }
            
        }

        /// <summary>
        /// 函數名稱    :   Login
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 函數名稱    :   LogOut
        /// </summary>
        /// <returns></returns>
        public RedirectResult LogOut()
        {
            Session.Abandon();
            return Redirect("~/Home/Login");
        }

        /// <summary>
        /// 函數名稱    :   LoginSessionValue
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        public RedirectResult LoginSessionValue(FormCollection post)
        {
            Session["AccIndex"] = post["hideAccIndex"].ToString();
            Session["AccNo"] = post["hideAccNo"].ToString();
            Session.Timeout = 240;            
            return Redirect("~/Home/Index");
        }

        /// <summary>
        /// 函數名稱    :   returnCheckLoginData
        /// </summary>
        /// <param name="fLoginNo"></param>
        /// <param name="fLoginPass"></param>
        /// <returns></returns>
        public string returnCheckLoginData(string fLoginNo, string fLoginPass)
        {
            string funReturnValue = ""; string fPassValue = "";
            List<oAccountDetail> oAccountDetail = new List<oAccountDetail>();
            oAccountDetail = adModel.listObjAccountDetail();
            if (oAccountDetail.Where(x => x.oAccNo == fLoginNo).ToList() == null) {
                funReturnValue = "X_帳號不正確";                
            } else {
                oAccountDetail = oAccountDetail.Where(x => x.oAccNo == fLoginNo).ToList();
                if (oAccountDetail.Count() > 0) {
                    fPassValue = oAccountDetail[0].oAccPassword.ToString();
                    funReturnValue = fPassValue == fLoginPass ? string.Format(@"O_{0}", oAccountDetail[0].oAccIndex.ToString()) : "X_密碼不正確";                    
                } else {
                    funReturnValue = "X_帳號不正確";                
                }                              
            }            
            return funReturnValue;
        }

        /// <summary>
        /// 函數名稱    :   funExecutePassword
        /// </summary>
        /// <param name="execClass"></param>
        /// <param name="fAccIndex"></param>
        /// <param name="fPassword"></param>
        /// <param name="nPassword"></param>
        /// <returns></returns>
        public string funExecutePassword(string execClass, string fAccIndex, string fPassword , string nPassword)
        {
            string funReturnValue = ""; string cPassValue = "";
            List<oAccountDetail> oAccountDetail = new List<oAccountDetail>();
            oAccountDetail = adModel.listObjAccountDetail();
            if (oAccountDetail.Where(x => x.oAccIndex == fAccIndex).ToList() == null) {
                funReturnValue = "X_帳號不存在";
            } else {
                oAccountDetail = oAccountDetail.Where(x => x.oAccIndex == fAccIndex).ToList();
                if (oAccountDetail.Count() > 0) {
                    if (execClass == "C") {
                        cPassValue = oAccountDetail[0].oAccPassword.ToString();
                        funReturnValue = cPassValue == fPassword ? string.Format(@"O_{0}", oAccountDetail[0].oAccIndex.ToString()) : "X_密碼不正確";
                    } else {
                        List<string> PassModDeclare = new List<string>(); List<object> PassModValue = new List<object>();
                        PassModDeclare = new List<string>() { "@AccIndex", "@AccPassword" };
                        PassModValue = new List<object>() { fAccIndex, nPassword };
                        funReturnValue = dbClass.msExecuteDataBase("U", "AccountDetail", 1, PassModDeclare, PassModValue);
                    }
                } else {
                    funReturnValue = "X_帳號不存在";
                }
            }
            return funReturnValue;
        }

    }
}
