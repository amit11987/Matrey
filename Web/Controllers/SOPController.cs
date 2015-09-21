using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class SOPController : Controller
    {
        //
        // GET: /SOP/
        #region Properties
        public string FileName { get; set; }
        #endregion

        #region Constroctor
        ISOPService SOPService;
        ITestService TestService;
        public SOPController(ISOPService SOPService, ITestService TestService)
        {
            this.SOPService = SOPService;
            this.TestService = TestService;
        }
        #endregion

        #region Action Method
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.TestList = new SelectList(TestService.GetAll(), "TestID", "TestName");
            return View();
        }


        // POST: /Person/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(SOP sop)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.TestList = new SelectList(TestService.GetAll(), "TestID", "TestName");
                    sop.Formula = Convert.ToString(TempData["formula"]);
                    if (string.IsNullOrEmpty(sop.Formula))
                    {
                        ModelState.AddModelError("", "Please insert formula");
                        return View(sop);
                    }
                    if (SOPService.FindBy(s => s.SOPName.Trim() == sop.SOPName.Trim()).Count() > 0)
                    {
                        ModelState.AddModelError("", "SOP alreary exists with same name");
                        return View(sop);
                    }
                    sop.FileName = this.FileName;
                    sop.Formula = Convert.ToString(TempData["formula"]);
                    SOPService.Create(sop);
                    ViewBag.ResultMessage = "Record inserted successfully !";
                }
            }
            catch
            {
                ViewBag.ResultMessage = "Error occured";
            }

            ResetModel(sop);
            return View();
        }



        [HttpPost]
        public ActionResult ShowDialog(int testId)
        {
            ViewBag.NumberOfParametersRequired = TestService.GetById(testId).NumberOfParametersRequired;
            return PartialView("InsertFormula");
        }

        [HttpPost]
        public void Upload(HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength > 0)
            {
                string directoryPath = ConfigurationManager.AppSettings["SOPFileDirectroy"].ToString();
                if (!Directory.Exists(Server.MapPath(directoryPath)))
                {
                    Directory.CreateDirectory(Server.MapPath(directoryPath));
                }

                this.FileName = upload.FileName;
                string extension = System.IO.Path.GetExtension(this.FileName);

                this.FileName = this.FileName.Substring(0, this.FileName.Length - extension.Length) + "-" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + extension;
                var path = Path.Combine(Server.MapPath(directoryPath), this.FileName);
                upload.SaveAs(path);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formula"></param>
        [HttpPost]
        public void InsertFormula(string formula)
        {
            TempData["formula"] = formula;
        }
        #endregion
      
        #region Private Method
          /// <summary>
        /// Reset Model Data
        /// </summary>
        /// <param name="product"></param>
        private void ResetModel(SOP sop)
        {
            sop.SID = string.Empty;
            sop.SOPDescription = string.Empty;
            sop.SOPHtml = string.Empty;
            sop.SOPName = string.Empty;
            sop.TestID = 0;
            sop.FileName = string.Empty;
            sop.Formula = string.Empty;
            this.ModelState.Clear();
        }

        #endregion
    }
}
