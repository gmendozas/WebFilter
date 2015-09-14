using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebFilter.Controllers
{
    public class DatePickerController : Controller
    {
        // GET: DatePicker
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MostrarPartialView(string tipo)
        {
            if(tipo.Equals("1"))
                return PartialView("_DatePickerPVUno");
            else if(tipo.Equals("2"))
                return PartialView("_DatePickerPVDos");
            else if (tipo.Equals("3"))
                return PartialView("_DatePickerPVTres");
            else
                return PartialView();
        }
    }
}