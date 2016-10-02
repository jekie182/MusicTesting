using MusicTesting.Models.Constant;
using MusicTesting.Models.DataModel;
using MusicTesting.Models.FunctionalModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MusicTesting.Models.ViewModel
{
    public class MyAdvertisementCurrentPVFileManagerModel
    {
        public int CurrentAdvertisemenId { get; set; }
        public List<Img> ImageList;

        public MyAdvertisementCurrentPVFileManagerModel() { }
        public MyAdvertisementCurrentPVFileManagerModel(string result, int id)
        {
            CurrentAdvertisemenId = id;
            if (result != string.Empty)
            {
                ImageList = new List<Img>();
                GetImagList(ref ImageList, result);
            }
        }

        private void GetImagList(ref List<Img> imgDir, string files)
        {
            var DB = new MTDBEntities();
            var str = files.TrimEnd('|').Split('|');
            var imglist = (from i in DB.Files where str.Contains(i.Id.ToString()) select i).ToList<Files>();
            if (imglist != null && imglist.Count > 0)
            {
                var uploader = new Uploader();
                for (int i = 0; i < imglist.Count; i++)
                {
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