using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using MusicTesting.Models.FunctionalModel;
using MusicTesting.Models.DataModel;

namespace MusicTesting.Models.ViewModel
{
    /// <summary>
    /// Модель, що передаєтсья в Інлексне представлення
    /// </summary>
    public class Index : BaseView
    {
        private readonly string UploadPath = "~/Files/";
        private MTDBEntities DB = new MTDBEntities();

        /// <summary>
        /// Місце де слід розмістити оголошення(Будинок жек підхзд і атк далі)
        /// </summary>
        public List<OgLocation> LocationList { get; set; }
        /// <summary>
        /// Варіанти розміщення оголошень
        /// </summary>
        public List<Paragraph> ParagraphList { get; set; }
        /// <summary>
        /// Список оголошень. При першій загрузці сторінки буде топ 10 оголошень
        /// при пошуку - результат пошуку
        /// </summary>
        public List<Advertisement> ReciveModelList { get; set; }
        /// <summary>
        /// Список країн для вибору в полі адреси
        /// </summary>
        public List<Country> CountryList { get; set; }
        /// <summary>
        /// Список міст для виборупри заповненні адреси
        /// </summary>
        public List<City> CityList { get; set; }
        /// <summary>
        /// Спсок вулиць
        /// </summary>
        public List<Street> StreetList { get; set; }

        /// <summary>
        /// Констуркор екземпляра класу...Потрібно оптимізувати по можливості!!!
        /// </summary>
        public Index()
        {
            LocationList = new List<OgLocation>();
            ParagraphList = new List<Paragraph>();
            ReciveModelList = new List<Advertisement>();
            CountryList = new List<Country>();
            CityList = new List<City>();
            StreetList = new List<Street>();

            CountryList = (from country in DB.Country select country).ToList();
            CityList = (from city in DB.City select city).ToList();
            StreetList = (from street in DB.Street select street).ToList();
            LocationList = (from l in DB.OgLocation select l).ToList();
            ParagraphList = (from p in DB.Paragraph select p).ToList();


            var ReciveResult = (from r in DB.OgNotice where r.IsVoided == false
                                orderby r.Date descending
                                select r).Take(12);
            if (ReciveResult != null)
            {
                foreach (var elem in ReciveResult.ToList<OgNotice>())
                {
                    List<Img> imgDir = new List<Img>();
                    if (elem.Files != string.Empty && elem.Files != null)
                    {
                        var str = elem.Files.TrimEnd('|').Split('|');
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
                                    Data = uploader.MakeImageSrcData(Path.Combine(HttpContext.Current.Request.MapPath(UploadPath), item.Id + item.Data)),
                                    Id = string.Format(@"imgFileId_{0}", item.Id)
                                });                        
                            }
                        }
                    }
                    var contactInfo = (from c in DB.OgContact where c.Id.Equals(elem.OgContacts) select c).ToList<OgContact>();
                    ReciveModelList.Add(new Advertisement()
                    {
                        Header = elem.Header,
                        Id = elem.Id,
                        PostedDate = elem.Date,
                        Location = "tge adress is hide",
                        ImgList = imgDir,
                        PhoneNumber = (contactInfo != null && contactInfo.Count == 1) ? contactInfo[0].PhoneNumber : string.Empty,
                        Email = (contactInfo != null && contactInfo.Count == 1) ? contactInfo[0].Email : string.Empty,
                    });

                }
            }
        }
    }
}
