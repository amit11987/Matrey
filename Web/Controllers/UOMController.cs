using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class UOMController : Controller
    {
        //
        // GET: /Test/
        IUOMService UOMService;
        public UOMController(IUOMService UOMService)
        {
            this.UOMService = UOMService;
        }

        public ActionResult Create()
        {
            ViewBag.UOMNames = new SelectList(UOMService.GetAllByIsStandardUOM(true), "UOMName", "UOMName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(UOM uom)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (UOMService.FindBy(p => p.UOMName.Trim() == uom.UOMName.Trim()).Count() > 0)
                    {
                        ModelState.AddModelError("", "UOM alreary exists with same name");
                        return View();
                    }

                    bool isModelValid = true;
                    if (!uom.ISStandardUOM)
                    {
                        if (string.IsNullOrEmpty(uom.StandardUOM))
                        {
                            ModelState.AddModelError("", "Please Select Standard UOM");
                            isModelValid = false;
                        }
                        else if (uom.UOMMapping == 0)
                        {
                            ModelState.AddModelError("", "Please enter mapping Details");
                            isModelValid = false;
                        }
                    }
                    if (isModelValid)
                    {
                        UOMService.Create(uom);
                    }
                }
            }
            catch { }

            return View();
        }

        public ActionResult LoadUOM()
        {
            List<UOM> lst = UOMService.GetAllByIsStandardUOM(true).ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

    }
}
