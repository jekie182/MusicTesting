using MusicTesting.Models.FunctionalModel;
using MusicTesting.Models.ViewModel;
using System;
using System.Web;
using System.Web.Mvc;

namespace MusicTesting.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        #region Stores
        public PartialViewResult GetMyAdvertisementPage(int userId, int page = 1)
        {
            var result = new MyAdvertisement().GetMyArvertisementList(userId, page);
            return PartialView("MyAdvertisementTablePV", result);
        }

        [HttpPost]
        public PartialViewResult DeleteMyAdvertisement(int advertisementId)
        {
            var model = new MyAdvertisement();
            model.DeleteMyAdvertisemnt(advertisementId);
            var result = model.GetMyArvertisementList(((User)Session["User"]).UserId, advertisementId);
            return PartialView("MyAdvertisementTablePV", result);
        }


        [HttpPost]
        public PartialViewResult GetCurrentMyAdvertisement(int advertisementId)
        {
            var model = new CurrentMyAdvertisement().GetCurrentMyAdvertisement(advertisementId);
            return PartialView("MyAdvertisementCurrentPV", model);
        }

        [HttpPost]
        public PartialViewResult GetCurrentMyAdvertisementImage(string fileDir, int id)
        {
            var model = new MyAdvertisementCurrentPVFileManagerModel(fileDir, id);
            return PartialView("MyAdvertisementCurrentPVFileManager", model);
        }

        [HttpPost]
        public JsonResult AddImageCurrentMyAdvertisement(int advertisementId, HttpPostedFileBase fileUpload)
        {
            string result = new FileManager().AddImage(advertisementId, fileUpload);
            return Json(new { Success = true, Result = result });
        }

        public PartialViewResult GetMyAdvertisementEmptyRecord()
        {
            return PartialView("MyAdvertisementCurrentPV", new CurrentMyAdvertisement() {
                AdressBody = string.Empty,
                AdvertisementBody = string.Empty,
                CityId = 1,
                CountryId = 1,
                Email = string.Empty,
                Files = string.Empty,
                Header = string.Empty,
                Id = 0,
                StreetId = 1,
                ParagraphId = 1,
                PhoneNumber = string.Empty
            });
        }

        [HttpPost]
        public PartialViewResult SaveCurrentMaAdvertisement()
        {
            return PartialView("sdfgsf");
        }


        public ActionResult GetImageDataByPath(string path)
        {
            var fs = System.IO.File.OpenRead(path);
            try
            {
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                return File(bytes, "image/png");
            }
            finally
            {
                fs.Close();
            }
        }
        #endregion
    }
}