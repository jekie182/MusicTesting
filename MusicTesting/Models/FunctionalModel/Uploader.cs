using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using MusicTesting.Models.HelperModel;
using MusicTesting.Interface;

namespace MusicTesting.Models.FunctionalModel
{
    /// <summary>
    /// Клас,що інкапсулює в собі функціонал для маніпуляції з файлами
    /// </summary>
    public class Uploader : IDataEntityModel
    {
        private readonly string UploadPath = "~/Files";
        private MTDBEntities db = new MTDBEntities();
        public MTDBEntities DB
        {
            get
            {
                return db;
            }

            set
            {
             
            }
        }

        public Uploader()
        {
        }

        /// <summary>
        /// Метод для загрузки на сервер зображень, що
        /// кріпляться до оголошення
        /// </summary>
        /// <param name="uploadImages"></param>
        /// <returns>
        /// повертаю рядок через делфмітер в якому сховано всі ідентифікатори вигружених зображень
        /// </returns>
        public string UploadReciveImage(HttpPostedFileBase[] uploadImages)
        {
            try
            {
                string result = "";
                if (uploadImages.Count() == 0)
                {
                    return result;
                }
                foreach (var image in uploadImages)
                {
                    if (image.ContentLength > 0)
                    {

                        Files file = new Files { Data = '.' + image.FileName.Split('.')[1] };
                        DB.Entry(file).State = System.Data.Entity.EntityState.Added;
                        DB.SaveChanges();

                        string fileName = file.Id + file.Data;
                        var path = Path.Combine(HttpContext.Current.Request.MapPath(UploadPath), fileName);
                        string extension = Path.GetExtension(fileName);
                        image.SaveAs(path);
                        result = string.Format(@"{0}{1}{2}", result, file.Id.ToString(), "|");
                    }
                }
                return result;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Метод для створення або редагування зображення з користувацького кабінету
        /// та анкети
        /// </summary>
        /// <param name="uploadImages"></param>
        /// <returns>
        /// Повертає ідентифікатор зоюраження яке було вигружено
        /// </returns>
        public Result UpdateAvatar(ref HttpPostedFileBase uploadImages)
        {
            try
            {
                Files file = new Files { Data = '.' + uploadImages.FileName.Split('.')[1] };
                DB.Entry(file).State = System.Data.Entity.EntityState.Added;
                DB.SaveChanges();

                string fileName = file.Id + file.Data;
                var path = Path.Combine(HttpContext.Current.Request.MapPath(UploadPath), fileName);
                string extension = Path.GetExtension(fileName);
                uploadImages.SaveAs(path);
                return new Result() { IsSucces = true, Object = file.Id };
            }
            catch
            {
                new Result() { IsSucces = false, Message = "Помилка при заугрзці зображення", Object = 1 };
            }
            return new Result() { IsSucces = false, Message = "Помилка при заугрзці зображення", Object = 1 };
        }

        /// <summary>
        /// Метод, що конверткє бінарний файл в формат приходних лдя
        /// використання на стороні клієнта
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public string MakeImageSrcData(string filename)
        {
            if (filename == null || filename == "")
            {
                return string.Empty;
            }
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            byte[] filebytes = new byte[fs.Length];
            fs.Read(filebytes, 0, Convert.ToInt32(fs.Length));
            return "data:image/png;base64," +
              Convert.ToBase64String(filebytes, Base64FormattingOptions.None);
        }
    }
}