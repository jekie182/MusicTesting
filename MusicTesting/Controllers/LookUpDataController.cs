using MusicTesting.Models.FunctionalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicTesting.Controllers
{
    public class LookUpDataController : Controller
    {
        [HttpPost]
        public JsonResult GetParagraphLookUpData()
        {
            var list = new LookUpRepository().GetParagrapgList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCountryLookUpData()
        {
            var list = new LookUpRepository().GetCountryList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCityLookUpData()
        {
            var list = new LookUpRepository().GetCityList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetStreetLookUpData()
        {
            var list = new LookUpRepository().GetStreetList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}