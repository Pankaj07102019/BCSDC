using BCSDC.Models;
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
    }
}