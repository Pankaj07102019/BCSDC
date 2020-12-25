using BCSDC.DAL;
using BCSDC.Models;
using BM = BCSDC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BCSDC.Controllers
{
    public class CreateFormsController : Controller
    {
        // GET: CreateForms
        public ActionResult CreateForms()
        {
            return View();
        }
        public ActionResult Signature()
        {
            return View("~/Views/CreateForms/Signature.cshtml");
        }
        public ActionResult FormPreview()
        {              
            FormPreview FormPreviewModel = new FormPreview();
            FormPreviewModel = (FormPreview) Session["lstControls"];
            return View("~/Views/CreateForms/FormPreview.cshtml", FormPreviewModel);
        }
        public void RedirectToPreview(FormPreview FromDetails)
        {
            Session["lstControls"] = FromDetails;
            RedirectToAction("FormPreview");         
        }
        public ActionResult SaveForm(BM.FormPreview FromDetails)
        {
            try
            {

                Session["lstControls"] = FromDetails;
                var retValue = FormsDAL.SaveForm(FromDetails, "INSERT");

                return Json(new {Status = "SUCCESS", StatusText = "Form Saved Successfully!!" }, JsonRequestBehavior.AllowGet);               
            }
            catch (Exception ex)
            {
                return Json(new { Status = "FAILURE", StatusText = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}