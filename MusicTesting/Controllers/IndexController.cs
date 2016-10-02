using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using MusicTesting.Models.FunctionalModel;
using MusicTesting.Models.ViewModel;
using MusicTesting.Models.HelperModel;

namespace MusicTesting.Controllers
{
    public class IndexController : Controller {

        public ActionResult Index()
        {
 
            var model = new Index();
            if (Session["User"] != null)
            {
                model.CurrentUser = (User)Session["User"];
            }
            //Ці два рядки коа нада шоби тіпа симулювати то шо я типу увійшов до системи          
            model.CurrentUser = new User(32);
            Session["User"] = model.CurrentUser;

            return View("Index", model);
        }

        public ActionResult LogOut()
        {
            if (Session["User"] != null)
            {
                Session["User"] = null;
            }
            return View("Index", new Index());
        }


        public ActionResult Authorithation()
        {
            return View("Authorithation");
        }

        public ActionResult Ancet(int userId)
        {
            var model = new PersonAncet().GetAncet(userId);
            if (Session["User"] != null)
            {
                model.CurrentUser = (User)Session["User"];
            }      
            return View("Ancet", model);
        }

        public PartialViewResult GetAvatarById(int userId)
        {
            var model = new UserAvatarView(userId);
            return PartialView("UserAvatar", model);
        }

        [HttpPost]
        public ActionResult UpdateAvatarById(HttpPostedFileBase uploadImages)
        {
            int userId = ((User)Session["User"]).UserId;
            new UserAvatarView().UpdateAvatar(ref uploadImages, userId );
            var model = new PersonAncet().GetAncet(userId);
            if (Session["User"] != null)
            {
                model.CurrentUser = (User)Session["User"];
            }
            return View("Ancet", model);
        }
        //public ActionResult SaveAncet(HttpPostedFileBase uploadImages, string firstName, string lastName, string email,
        //    string phone, DateTime dob, string about, int country, int city, int street, string build)
        //{
        //    Result imageUploadResult = new Result();
        //    if (uploadImages != null)
        //    {
        //        imageUploadResult = new Uploader().UpdateAvatar(uploadImages);
        //    }
        //    int userid = ((User)(Session["User"])).UserId;
        //    var result = new Ancet().AncetSave(imageUploadResult.Object == null ? 1 : (int)imageUploadResult.Object, firstName, lastName, email, phone, dob, about, country, city, street, build, userid);
        //    if (result.IsSucces == false)
        //    {
        //        return RedirectToAction("Alert", routeValues: new
        //        {
        //            message = result.Message,
        //            viewName = "Home",
        //            viewDataName = "Message"
        //        });
        //    }
        //    else return View("Home", result.Object);
        //}
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string username, string password, string loginkeeping)
        {
            var model = new Login()
            {
                UserName = username,
                Password = password,
            }.LogIn();

            if (!model.IsSucces)
            {
                return RedirectToAction("Alert", routeValues: new
                {
                    message = model.Message,
                    viewName = "Authorithation",
                    viewDataName = "Message"
                });
            }
            else
            {
                Session["User"] = model.Object;
                var viewModel = new Index();
                viewModel.CurrentUser = ((User)model.Object);
                return View("Index", viewModel);
            }
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Registration(string usernamesignup, string emailsignup, string passwordsignup, string passwordsignup_confirm)
        {
            var model = new Registration().CreateAccount(usernamesignup, emailsignup, passwordsignup, passwordsignup_confirm);
            if (!model.IsSucces)
            {
                return RedirectToAction("Alert", routeValues: new
                {
                    message = model.Message,
                    viewName = "Authorithation",
                    viewDataName = "Message"
                });
            }
            else
            {
                Session["User"] = model.Object;
                var viewModel = new Index();
                viewModel.CurrentUser = ((User)model.Object);
                return View("Index", viewModel);
            }
        }

        #region Method for my account
        public ActionResult MyAdvertisement()
        {
            var model = new MyAdvertisement();
            model.CurrentUser = (User)Session["User"];
            return View("MyAdvertisement", model);
        }
        
        //public ActionResult Balance()
        //{
        //    return View("Balance");
        //}
        //method for returning some message for client
        #endregion
 


        public ActionResult Alert(string message, string viewName, string viewDataName)
        {
            ViewData[viewDataName] = message;
            return View(viewName);
        }

        public ActionResult ViewAdvertisment(int id)
        {
            var model = new AdvertisementView(id);
            if (Session["User"] != null)
            {
                model.CurrentUser = (User)Session["User"];
            }
            return View("Advertisement", model);
        }

        [HttpPost]
        public ActionResult UpdateCurrentMyAdvetrisement(string phoneNumber, string email,
            string header, int? paragraph, string advertisementBody, int? country, int? city, 
            int? street, string adressText, int advertisementId)
        {
            var result = new AdversistementLogin().UpdateAdvertisement(phoneNumber, email, header, paragraph, advertisementBody, country,
                city, street, adressText, advertisementId);
            return Json(true);
        }

        [HttpPost]
        public JsonResult DeleteImageFromMyAdvertisement(int advertisementId, int fileId)
        {
            new FileManager().DeleteImage(advertisementId, fileId);
            return Json(null, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// Метод на контроллері для подачі оголошень
        /// він працює і коли користувач авторизований і коли користувач не авторизований
        /// розподіл функціоналу опирається на змінну користувача в сесії Session["User"]
        /// якшо така змінна закеширована тоді можна вважати, що користувач ще авторизований
        /// </summary>
        /// <param name="header"></param>
        /// <param name="paragraph"></param>
        /// <param name="location"></param>
        /// <param name="uploadImages"></param>
        /// <param name="reciveText"></param>
        /// <param name="phonenumber"></param>
        /// <param name="email"></param>
        /// <param name="country"></param>
        /// <param name="city"></param>
        /// <param name="street"></param>
        /// <param name="adressText"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult AddAdvertisment(string header, string paragraph, string location, HttpPostedFileBase[] uploadImages, string reciveText,  string phonenumber, 
            string email, int? country, int? city, int? street, string adressText)
        {
            string uploadResult = string.Empty;
            if (uploadImages != null)
            {
                uploadResult = new Uploader().UploadReciveImage(uploadImages);
            }
            if (Session["User"] != null)
            {
                var result = new AdversistementLogin().AddAdversisment(header, location, paragraph, reciveText, uploadResult, ((User)Session["User"]).UserId);
            }
            else
            {
                var result = new AdversismentAnonymous().AddAdversisment( header,  paragraph,  location,  reciveText,  phonenumber,
             email,   country,   city,   street,  adressText, uploadResult);
            }

            var model = new Index();
            if (Session["User"] != null)
            {
                model.CurrentUser = (User)Session["User"];
            }
            return View("Index", model);
        }

    }

}