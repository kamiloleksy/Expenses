using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expenses.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return Ok("Index");
        }
        public IActionResult Article()
        {
            return Ok("Action of Blog controller");
        }
    }
}
