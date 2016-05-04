using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AI_proj.NeuralNetwork;
using FANNCSharp;
using FANNCSharp.Double;

namespace AI_proj.Controllers
{


    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            Console.WriteLine("XOR");
            NeuralNet xornet = XOR.get_xor_net();
            XOR.Test(xornet, 0, 0);
            XOR.Test(xornet, 0, 1);
            XOR.Test(xornet, 1, 0);
            XOR.Test(xornet, 1, 1);

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}