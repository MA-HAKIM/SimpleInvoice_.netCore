using InvoiceManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManagement.Controllers
{
    
    public class InvoiceController : Controller
    {
        private readonly InvoiceDbContext db;
        public InvoiceController(InvoiceDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Production Details
        public IActionResult ProductDetailsList()
        {
            return View(db.ProductDetails.ToList());
        }
        public IActionResult CreateProductDetails()
        {
            ViewBag.Title = "Create";
            return View();
        }
        [HttpPost]
        public IActionResult CreateProductDetails(ProductDetails pd)
        {
            if (ModelState.IsValid)
            {
                if (pd.Id > 0)
                {

                    db.Entry(pd).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Message"] = "Data Update Successfully";
                    TempData["Status"] = "2";
                    
                }
                else
                {
                    db.Entry(pd).State = EntityState.Added;
                    db.SaveChanges();
                    TempData["Message"] = "Data Save Successfully";
                    TempData["Status"] = "1";
                    

                }
                return RedirectToAction("ProductDetailsList","Invoice");
            }
            return View(pd);
        }

        public IActionResult EditProductDetails(int id)
        {
            ViewBag.Title = "Edit";
            var data = db.ProductDetails.Where(x => x.Id == id).FirstOrDefault();
            
            return View("CreateProductDetails",data);
        }
        public IActionResult DeleteProductDetails(int id)
        {
            ViewBag.Title = "Delete";
            var data = db.ProductDetails.Where(x => x.Id == id).FirstOrDefault();

            return View("CreateProductDetails", data);
        }
        [HttpPost]
        public IActionResult DeleteProductDetails(ProductDetails pd)
        {
            
            var data = db.ProductDetails.Where(x => x.Id == pd.Id).FirstOrDefault();
            db.Entry(data).State = EntityState.Deleted;
            db.SaveChanges();
            return Json(new { Success = 1, Id = pd.Id});
        }

        #endregion

        #region Product Purchase
        public IActionResult PurchaseList()
        {
            var data = db.Customers.ToList();
            return View(data);
        }
        public IActionResult Report(int id)
        {
            
            var data = db.Customers.Include(x=>x.Products).Where(x => x.CustomerId == id).FirstOrDefault();
            
            return View(data);
        }
        public IActionResult CreatePurchase()
        {
            ViewBag.Title = "Create";
            
            var plist = db.ProductDetails.ToList();
            ViewBag.ProductList = new SelectList(plist, "ProductName", "ProductName");
            return View();
        }
        [HttpPost]
        public IActionResult CreatePurchase(Customer c)
        {
            
            var result = "";
            if (c != null)
            {
                
                Customer model = new Customer();
                model.CustomerId = c.CustomerId;
                model.InvoiceNo = c.InvoiceNo;
                model.CustomerName = c.CustomerName;
                model.Address = c.Address;
                model.Phone = c.Phone;
                db.Customers.Add(model);
                db.SaveChanges();
                foreach (var item in c.Products)
                {
                    //var orderId = Guid.NewGuid();
                    Product P = new Product();
                    P.ProductName = item.ProductName;
                    P.PurchaseDate = DateTime.Parse( DateTime.Now.ToString("dd-MMM-yyyy"));
                    P.Quantity = item.Quantity;
                    P.Rate = item.Rate;
                    P.TotalAmount = item.Quantity * item.Rate;
                    P.CustomerId = model.CustomerId;
                    db.Products.Add(P);
                    db.SaveChanges();
                }
                db.SaveChanges();
                
            }
            return Json(new { Success=1});
            //return View();
        }

        public IActionResult DeletePurchase(int id)
        {
           
            var data = db.Customers.Include(x => x.Products).Where(x => x.CustomerId == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public IActionResult DeletePurchase(Customer c)
        {
            var data = db.Customers.Include(x=>x.Products).Where(x => x.CustomerId == c.CustomerId).FirstOrDefault();
            db.Customers.Remove(data);
            db.Products.RemoveRange(data.Products);
            db.SaveChanges();
            return RedirectToAction("PurchaseList");
        }
        
        #endregion


    }
}
