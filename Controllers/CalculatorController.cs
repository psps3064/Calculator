using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calc.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Calc.Controllers
{
    public class CalculatorController : Controller
    {
        
        List<History> hisries = new List<History>();
        public IActionResult Index()
        {
            return View(new Calculator());
        }

        [HttpPost]
        public IActionResult Index(Calculator c, string symbol)
        {
            double result = 0;
            string operate = "";
            string his = "";
            if(symbol == "plus")
            {
                c.sum = c.num1 + c.num2;
                operate = "+";
                result = c.sum;
                his = "" + c.num1 + " " + operate + " " + c.num2 + " = " + result;
            }
            else if(symbol == "minus")
            {
                c.sum = c.num1 - c.num2;
                operate = "-";
                result = c.sum;
                his = "" + c.num1 + " " + operate + " " + c.num2 + " = " + result;
            }
            else if (symbol == "times")
            {
                c.sum = c.num1 * c.num2;
                operate = "x";
                result = c.sum;
                his = "" + c.num1 + " " + operate + " " + c.num2 + " = " + result;
            }
            else if (symbol == "divided")
            {
                c.sum = c.num1 / c.num2;
                operate = "÷";
                result = c.sum;
                his = "" + c.num1 + " " + operate + " " + c.num2 + " = " + result;
            }
            var jsonstring = HttpContext.Session.GetString("history");
            var gethis = !string.IsNullOrEmpty(jsonstring)?JsonConvert.DeserializeObject<List<History>>(jsonstring):new List<History>();
            gethis.Add(new History { His = his });
            
            var show = JsonConvert.SerializeObject(gethis);
            HttpContext.Session.SetString("history", show);
            ViewBag.history = gethis;
            //setLastResult(c, result, operate);
            return View(c);
        }

    }
}
