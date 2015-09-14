using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
namespace Web.Controllers
{
    public class TestController : Controller
    {

        //
        // GET: /Test/
        ITestService TestService;
        IFieldTypeService FieldTypeService;
        public TestController(ITestService TestService, IFieldTypeService FieldTypeService)
        {
            this.TestService = TestService;
            this.FieldTypeService = FieldTypeService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.NumberOfParametersRequired = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultSelectedNoOfTestParameter"]);
            fillDropdown();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Test test)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (TestService.FindBy(p => p.TestName.Trim() == test.TestName.Trim()).Count() > 0)
                    {
                        ViewBag.NumberOfParametersRequired = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultSelectedNoOfTestParameter"]);
                        fillDropdown();
                        ModelState.AddModelError("", "Test alreary exists with same name");
                        return View();
                    }
                    TestService.Create(test);
                }
            }
            catch
            {
            }
            ViewBag.NumberOfParametersRequired = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultSelectedNoOfTestParameter"]);
            fillDropdown();
            return View();
        }

        [HttpGet]
        public ActionResult Renderview(int? id)
        {
            ViewBag.NumberOfParametersRequired = id.HasValue ? id.Value : Convert.ToInt32(ConfigurationManager.AppSettings["DefaultSelectedNoOfTestParameter"]);
            fillDropdown();
            return PartialView("~/Views/PartialView/testParameter.cshtml");
        }

        private void fillDropdown()
        {
            ViewBag.MaxNoOfTestParameter = Convert.ToInt32(ConfigurationManager.AppSettings["MaxNoOfTestParameter"]);
            ViewBag.FieldTypes = new SelectList(FieldTypeService.GetAllByIsActive(true), "FieldTypeID", "FieldTypeName");
        }
    }
}
