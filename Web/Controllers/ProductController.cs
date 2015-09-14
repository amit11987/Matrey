
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace SimbaCapital.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/
        IProductService ProductService;
        public ProductController(IProductService ProductService)   
        {
            this.ProductService = ProductService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Person/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (ProductService.FindBy(p => p.ProductName.Trim() == product.ProductName.Trim()).Count() > 0)
                    {
                        ModelState.AddModelError("", "Product alreary exists with same name");
                        return View();
                    }
                    ProductService.Create(product);
                    ResetModel(product);
                    ViewBag.ResultMessage = "Record inserted successfully !";
                }
            }
            catch
            {
                 ViewBag.ResultMessage = "Error occured";
            }
            return View();
        }

        private void ResetModel(Product product)
        {
            product.ProductName = string.Empty;
            product.PID = string.Empty;
            product.ProductDescription = string.Empty;
            this.ModelState.Clear();
        }
    }
}
