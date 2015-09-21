using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class SampleReceivingController : Controller
    {
        //
        // GET: /SampleReceiving/
        ISampleReceivingService SampleReceivingService;
        IUOMService UOMService;
        IProductService ProductService;
        ITestService TestService;
        public SampleReceivingController(ISampleReceivingService SampleReceivingService, IUOMService UOMService, IProductService ProductService, ITestService TestService)
        {
            this.SampleReceivingService = SampleReceivingService;
            this.UOMService = UOMService;
            this.ProductService = ProductService;
            this.TestService = TestService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.NoOfProductReceived = Convert.ToInt32(ConfigurationManager.AppSettings["NoOfProductReceived"]);
            ViewBag.NoofTestRequired = Convert.ToInt32(ConfigurationManager.AppSettings["NoofTestRequired"]);
            fillDropdown();
            return View();
        }

        [HttpPost]
        public ActionResult Create(SampleReceive test)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SampleReceivingService.Create(test);
                }
                ViewBag.NoOfProductReceived = Convert.ToInt32(ConfigurationManager.AppSettings["NoOfProductReceived"]);
                ViewBag.NoofTestRequired = Convert.ToInt32(ConfigurationManager.AppSettings["NoofTestRequired"]);
                fillDropdown();
            }
            catch
            {
            }
            return View();
        }

        [HttpGet]
        public ActionResult RenderProductReceivedView(int? id)
        {
            ViewBag.NoOfProductReceived = id.HasValue ? id.Value : Convert.ToInt32(ConfigurationManager.AppSettings["NoOfProductReceived"]);
            fillDropdown();
            return PartialView("~/Views/PartialView/ProductReceived.cshtml");
        }

        [HttpGet]
        public ActionResult RenderSampleTestView(int? id)
        {
            ViewBag.NoofTestRequired = id.HasValue ? id.Value : Convert.ToInt32(ConfigurationManager.AppSettings["NoofTestRequired"]);
            fillDropdown();
            return PartialView("~/Views/PartialView/SampleTest.cshtml");
        }

        private void fillDropdown()
        {
            ViewBag.UOMS = new SelectList(UOMService.GetAll(), "UOMID", "UOMName");
            ViewBag.Products = new SelectList(ProductService.GetAll(), "ProductID", "ProductName");
            ViewBag.Tests = new SelectList(TestService.GetAll(), "TestID", "TestName");
        }


    }
}
