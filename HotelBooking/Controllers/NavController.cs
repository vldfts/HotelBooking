using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelBooking.Controllers
{
    public class NavController : Controller
    {
        // GET: Nav
        public ActionResult Menu()
        {

            return PartialView("Menu");
        }
        public ActionResult AdminMenu()
        {

            return PartialView("AdminMenu");
        }
    }
}