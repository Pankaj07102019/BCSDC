using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BCSDC.DAL;
using System.Web.Mvc;

namespace BCSDC.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Search()
        {
            return View();
        }
        public JsonResult SearchFormsGrid(string Form_Name, string Field_Name, string Field_Type, string Field_Value)
        {
            try
            {
                var ReturnData = SearchDAL.SearchForms(Form_Name, Field_Name, Field_Type, Field_Value);
                return Json(ReturnData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = "SUCCESS", Msg = ex }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}