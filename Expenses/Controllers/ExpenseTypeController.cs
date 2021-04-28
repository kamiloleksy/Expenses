using Expenses.Data;
using Expenses.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expenses.Controllers
{
    public class ExpenseTypeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ExpenseTypeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        { 
            var objList = _db.ExpenseTypes;
            return View(objList);
        }

        //Get - Update
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var item = _db.ExpenseTypes.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        public IActionResult Remove(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var item = _db.ExpenseTypes.Find(id);
            if(item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        //Post - Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseType model)
        {
            if (ModelState.IsValid)
            {
                _db.ExpenseTypes.Add(model);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
            
        }

        //Post - Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ExpenseType model)
        {
            if (ModelState.IsValid)
            {
                _db.ExpenseTypes.Update(model);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? Id)
        {
            var item = _db.ExpenseTypes.Find(Id);
            if (item == null)
            {
                return NotFound();
            }

            _db.ExpenseTypes.Remove(item);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
