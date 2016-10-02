using MusicTesting.Models.Constant;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MusicTesting.Models.FunctionalModel
{
    public class FileManager
    {

        public void DeleteImage(int advertisementId, int fileId)
        {
            using (var context = new MTDBEntities())
            {
                var item = (from a in context.OgNotice where a.Id == advertisementId select a).First();
                if (item!= null)
                {
                    string fileArr = item.Files;
                    new Logger().WriteLog(2,
                        string.Format(@"Author of advertisement Id = {0}. Delete file Id = {1}.
                            source string with file array look like this: {2}", advertisementId, fileId, fileArr));
                    var str = fileArr.TrimEnd('|').Split('|');

                    fileArr = string.Empty;
                    foreach (var elem in str)
                    {
                        if (elem != fileId.ToString())
                        {
                            fileArr = string.Format(@"{0}{1}{2}", fileArr, elem, "|");
                        }
                    }
                    context.Database.ExecuteSqlCommand(@"update OgNotice set Files = @p1 where Id = @p0 ", advertisementId, fileArr);
                }
            }
        }

        public string AddImage(int advertisementId, HttpPostedFileBase imageUploader)
        {
            var DB = new MTDBEntities();
            try
            {
                string result = string.Empty;

                        Files file = new Files { Data = '.' + imageUploader.FileName.Split('.')[1] };
                        
                        DB.Entry(file).State = System.Data.Entity.EntityState.Added;
                        DB.SaveChanges();

                        var adv = (from o in DB.OgNotice where o.Id == advertisementId select o).First();

                        if (adv != null)
                        {
                            string fileName = file.Id + file.Data;

                            var path = Path.Combine(HttpContext.Current.Request.MapPath(FileDirect.UploadPath), fileName);
                            string extension = Path.GetExtension(fileName);
                            imageUploader.SaveAs(path);

                            result = string.Format(@"{0}{1}{2}", adv.Files, file.Id.ToString(), "|");
                    new Logger().WriteLog(4, string.Format(@"Advertisement with Id = {0}, has been update. 
                            Files add new file id = {1}. Before advertisement Files look like this: {2} after them:{3}", advertisementId
                        ,file.Id, adv.Files,  result));

                         DB.Database.ExecuteSqlCommand(@"update OgNotice set [Files] = @p0 where Id = @p1", result, advertisementId);
                        }      
                return result;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}