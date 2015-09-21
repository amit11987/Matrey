﻿using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.ViewModel;

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

        [HttpGet]
        public ActionResult Details()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Details(UOMViewModel uomDetails)
        {
            IEnumerable<UOM> lstUOM = null;
            if (!string.IsNullOrEmpty(uomDetails.UOMName))
            {
                lstUOM = UOMService.FindBy(p => p.UOMName == uomDetails.UOMName);
            }
            else
            {
                uomDetails.ToDate = new DateTime(uomDetails.ToDate.Year, uomDetails.ToDate.Month, uomDetails.ToDate.Day, 23, 59, 59);
                if (uomDetails.ToDate.Year >= 1900 && uomDetails.FromDate.Year >= 1900)
                {
                    lstUOM = UOMService.FindBy(p => p.CreatedDate >= uomDetails.FromDate && p.CreatedDate <= uomDetails.ToDate);
                }
                else if (uomDetails.ToDate.Year >= 1900 && uomDetails.FromDate.Year <= 1900)
                {
                    lstUOM = UOMService.FindBy(p => p.CreatedDate >= DateTime.MinValue && p.CreatedDate <= uomDetails.ToDate);
                }
                else if (uomDetails.ToDate.Year <= 1900 && uomDetails.FromDate.Year >= 1900)
                {
                    lstUOM = UOMService.FindBy(p => p.CreatedDate >= uomDetails.FromDate && p.CreatedDate <= DateTime.MaxValue);
                }
                else
                {
                    lstUOM = UOMService.FindBy(p => p.CreatedDate >= DateTime.MinValue && p.CreatedDate <= DateTime.MaxValue);
                }
            }
            uomDetails.lstUOM = lstUOM;
            return View(uomDetails);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            ViewBag.UOMNames = new SelectList(UOMService.GetAllByIsStandardUOM(true), "UOMName", "UOMName");
            var UOM = new UOM();
            UOM = UOMService.GetById(id);
            return View(UOM);
        }

        [HttpPost]
        public ActionResult Edit(UOM uom)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UOMService.Update(uom);  
                }
            }
            catch
            {
            }
            return View(uom);
        }


        public ActionResult LoadUOM()
        {
            List<UOM> lst = UOMService.GetAllByIsStandardUOM(true).ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

    }
}
