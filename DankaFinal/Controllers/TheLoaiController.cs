using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DankaFinal.Models;

namespace DankaFinal.Controllers
{
    public class TheloaiController : Controller
    {
        DanKaEDMEntities db = new DanKaEDMEntities();
        // GET: Theloai

        public ActionResult TheloaiPartial()
        {
            var theloai = db.theloais.ToList();
            return PartialView(theloai);
        }
    }
}

