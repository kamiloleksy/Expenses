using Expenses.Data;
using Expenses.Models;
using Expenses.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expenses.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ExpenseController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        { 
            var objList = _db.Expenses.Include(x => x.ExpenseType);
            var total = objList.Sum(x => x.Amount);
            ViewBag.Total = total;
            return View(objList);
        }

        //Get - Update
        public IActionResult Create()
        {
            //IEnumerable<SelectListItem> TypeDropDown = _db.ExpenseTypes.Select(i => new SelectListItem
            //{
            //    Text = i.Name,
            //    Value = i.Id.ToString(),
            //});

            //ViewBag.TypeDropDown = TypeDropDown;

            var expenseVM = new ExpenseVM()
            {
                Expense = new Expense(),
                TypeDropDown = _db.ExpenseTypes.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                })
            };

            return View(expenseVM);
        }
        public IActionResult Update(int? id)
        {
            var expenseVM = new ExpenseVM()
            {
                Expense = new Expense(),
                TypeDropDown = _db.ExpenseTypes.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                })
            };

            if (id == null || id == 0)
            {
                return NotFound();
            }
            expenseVM.Expense = _db.Expenses.Find(id);
            if (expenseVM.Expense == null)
            {
                return NotFound();
            }

            return View(expenseVM);
        }

        public IActionResult Remove(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var item = _db.Expenses.Find(id);
            if(item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        //Post - Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseVM model)
        {
            if (ModelState.IsValid)
            {
                _db.Expenses.Add(model.Expense);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
            
        }

        //Post - Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ExpenseVM model)
        {
            if (ModelState.IsValid)
            {
                _db.Expenses.Update(model.Expense);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? Id)
        {
            var item = _db.Expenses.Find(Id);
            if (item == null)
            {
                return NotFound();
            }

            _db.Expenses.Remove(item);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
