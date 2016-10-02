using MusicTesting.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MusicTesting.Controllers
{
    public class ComunicationController : Controller
    {
        public ActionResult GetMessages(int userId)
        {
            var model = new MessagesView(userId);
            if (Session["User"] != null)
            {
                model.CurrentUser = (User)Session["User"];
            }        
            Session["User"] = model.CurrentUser;
            return View("Messages", model);
        }

        public ActionResult SendReplyMessage(int fromUserId, int toUserId, string header, string body, string fileDire)
        {
           new MessagesView().SendMessage(fromUserId, toUserId, header, body, fileDire);
            var model = new MessagesView(fromUserId);
            if (Session["User"] != null)
            {
                model.CurrentUser = (User)Session["User"];
            }
            Session["User"] = model.CurrentUser;
            return View("Messages", model);
           // return Json(null, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult SendMessage(int fromUserId, int toUserId, string header, string body)
        {
            new MessagesView().SendMessage(fromUserId, toUserId, header, body, null);
            return Json(null, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult ReadMessage(int messagId)
        {
            new MessagesView().ReadMessage(messagId);
            return Json(null, JsonRequestBehavior.DenyGet);
        }
    }
}