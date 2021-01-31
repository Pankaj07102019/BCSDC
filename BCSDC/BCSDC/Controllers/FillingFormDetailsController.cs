using BCSDC.DAL;
using BCSDC.Models;
using System;
using System.Collections.Generic;
using BM = BCSDC.Model;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BCSDC.Controllers
{
    public class FillingFormDetailsController : Controller
    {
        // GET: FillingFormDetails
        public ActionResult FillingFormDetails(string Form_Name)
        {
            FormPreview FormPreviewModel = new FormPreview();
            //string FName = (string)Session["FormName"];
            var ReturnData = SearchDAL.SearchForms(Form_Name, "", "", "");
            List<FormControlsList> lstcnt = new List<FormControlsList>();
            foreach (var Items in ReturnData)
            {
                FormPreviewModel.FormName = Items.Form_Name;
                lstcnt.Add(new FormControlsList
                {
                    FieldName = Items.Field_Name.ToString(),
                    FieldType = Items.Field_Type.ToString(),
                    FieldValue = Items.Field_Value.ToString()
                });
            }
            FormPreviewModel.lstControls = lstcnt;
            return View("~/Views/FillingFormDetails/FillingFormDetails.cshtml", FormPreviewModel);
        }
        
        [HttpPost]
        public ActionResult DeleteForms(string Form_Name)
        {
            return View("FillingFormDetails");
        }

        public ActionResult FillFormDetails()
        {
            FormPreview FormPreviewModel = new FormPreview();
            string FName = (string) Session["FormName"];
            var ReturnData = SearchDAL.SearchForms(FName, "", "", "");
            List<FormControlsList> lstcnt = new List<FormControlsList>();
            foreach (var Items in ReturnData)
            {
                FormPreviewModel.FormName = Items.Form_Name;
                lstcnt.Add(new FormControlsList
                {
                    FieldName = Items.Field_Name.ToString(),
                    FieldType = Items.Field_Type.ToString(),
                    FieldValue = Items.Field_Value.ToString()
                });
            }
            FormPreviewModel.lstControls = lstcnt;
            return View("~/Views/FillingFormDetails/FillingFormDetails.cshtml", FormPreviewModel);
        }
        public void RedirectToFormsDetails(string Form_Name)
        {
            Session["FormName"] = Form_Name;
            RedirectToAction("FillFormDetails");
        }

        public ActionResult SaveDetails(BM.FormPreview FromDetails)
        {
            try
            {
               // Session["lstControls"] = FromDetails;
                //var retValue = FormsDAL.SaveForm(FromDetails, "INSERT");
                //var retValue = FormsDAL.SaveFormControlList(FromDetails);
                return Json(new { Status = "SUCCESS", StatusText = "Form Saved Successfully!!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = "FAILURE", StatusText = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}