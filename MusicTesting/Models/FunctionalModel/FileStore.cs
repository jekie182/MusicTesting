using MusicTesting.Models.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using MusicTesting.Models.Constant;

namespace MusicTesting.Models.FunctionalModel
{
    public class FileStore
    {
        public FileStore() { }

        public void GetImgListByString(string fileDir, ref List<Img> imgDir) 
        {
            if (fileDir != string.Empty && fileDir != null)
            {
                using (var DB = new MTDBEntities())
                {
                    var str = fileDir.TrimEnd('|').Split('|');
                    var imglist = (from i in DB.Files where str.Contains(i.Id.ToString()) select i).ToList<Files>();
                    if (imglist != null && imglist.Count > 0)
                    {
                        var uploader = new Uploader();
                        for (int i = 0; i < imglist.Count; i++)
                        {
                            if (i == 3) break;

                            var item = imglist[i];
                            imgDir.Add(new Img()
                            {
                                Data = uploader.MakeImageSrcData(Path.Combine(HttpContext.Current.Request.MapPath(FileDirect.UploadPath), item.Id + item.Data)),
                                Id = string.Format(@"imgFileId_{0}", item.Id)
                            });
                        }
                    }
                }
            }

        }
    }
}