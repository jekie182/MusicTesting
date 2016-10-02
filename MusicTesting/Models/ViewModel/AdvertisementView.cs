using MusicTesting.Models.DataModel;
using MusicTesting.Models.FunctionalModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MusicTesting.Models.ViewModel
{
    public class AdvertisementView:BaseView
    {
        private readonly string UploadPath = "~/Files/";
        public int Id { get; set; }
        public string Header { get; set; }
        public string Location { get; set; }
        public DateTime? PostedDate { get; set; }
        public List<Img> ImgList { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AdvertisementBody { get; set; }
        public string AuthorName { get; set; }
        public String AuthorLastName { get; set; }
        public int? AuthorId { get; set; }
        public int ImgCount { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public string StreetName { get; set; }
        public string Adresstext { get; set; }
        public string Paragraph { get; set; }
        
        public string FileDir { get; set; }

        public AdvertisementView() { }

        public AdvertisementView(int id)
        {
            AdvertisementView result;
            using (var context = new MTDBEntities() )
            {
                result = context.Database.SqlQuery<AdvertisementView>(@"select  
                    ogn.Id as Id,
                    ogn.Header as Header,
                    loc.Name as [Location],
                    ogn.[Message] as AdvertisementBody,
                    ogn.[Files] as FileDir,
                    ogn.[Date] as PostedDate,
                    par.Name as Paragraph,
                    adr.[Text] as Adresstext,
                    coun.Name as CountryName,
                    cit.Name as CityName,
                    stre.Name as StreetName,
                    ogcon.Email as Email,
                    ogcon.PhoneNumber as PhoneNumber,
                    per.FirstName as AuthorName,
                    per.LastName as AuthorLastName,
                    per.UserId as AuthorId
                    from OgNotice ogn

                    join Paragraph as par on par.Id = ogn.Paragraph 
                    join Adress as adr on adr.Id = ogn.AdressId
                    join Country as coun on adr.Country = coun.Id
                    join City as cit on adr.City = cit.Id
                    join Street as stre on adr.Street = stre.Id
                    join OgContact as ogcon on ogn.OgContacts = ogcon.Id
                    join OgLocation as loc on loc.Id = ogn.OgLocation
                    join Person as per on per.UserId = ogn.UserId
                    where ogn.Id = @p0
                    ", id).ToList().FirstOrDefault();
            }
            Header = result.Header;
            Id = result.Id;
            Location = result.Location;
            PostedDate = result.PostedDate;
            Email = result.Email;
            PhoneNumber = result.PhoneNumber;
            AdvertisementBody = result.AdvertisementBody;
            AuthorName = result.AuthorName;
            AuthorLastName = result.AuthorLastName;
            AuthorId = result.AuthorId;
            CountryName = result.CountryName;
            CityName = result.CityName;
            StreetName = result.StreetName;
            Adresstext = result.Adresstext;
            Paragraph = result.Paragraph;
            FileDir = result.FileDir;
            ImgList = new List<Img>();
            #region Entity Sample
            //var result = (from ogNotice in DB.OgNotice
            //              join contact in DB.OgContact on ogNotice.OgContacts equals contact.Id
            //              join adress in DB.Adress on ogNotice.AdressId equals adress.Id
            //              join location in DB.OgLocation on ogNotice.OgLocation equals location.Id
            //              join person in DB.Person on ogNotice.UserId equals person.UserId

            //              //join for Adress definishion
            //              join country in DB.Country on adress.Country equals country.Id
            //              join city in DB.City on adress.City equals city.Id
            //              join stret in DB.Street on adress.Street equals stret.Id

            //              where ogNotice.Id == Id
            //              select new AdvertisementView
            //              {
            //                Id = ogNotice.Id,
            //                Header = ogNotice.Header,
            //                  Location = location.Name,
            //                  PostedDate = ogNotice.Date,
            //                  Email = contact.Email,
            //                  PhoneNumber = contact.PhoneNumber,
            //                  AdvertisementBody = ogNotice.Message,
            //                  AuthorName = person.FirstName,
            //                  AuthorLastName = person.LastName,
            //                  AuthorId = person.UserId,
            //                  CountryName = country.Name,
            //                  CityName = city.Name,
            //                  StreetName = stret.Name,
            //                  FileDir = ogNotice.Files,
            //                  Adresstext = adress.Text,
            //              }).ToList().FirstOrDefault();
            #endregion
            if (result!= null && !FileDir.Equals(string.Empty) )
            {
                var DB = new MTDBEntities();
                    List<Img> Imgl = new List<Img>();
                        var str = FileDir.TrimEnd('|').Split('|');
                        var imglist = (from i in DB.Files where str.Contains(i.Id.ToString()) select i).ToList<Files>();
                        if (imglist != null && imglist.Count > 0)
                        {
                            var uploader = new Uploader();
                            for (int i = 0; i < imglist.Count; i++)
                            {
                                var item = imglist[i];
                                Imgl.Add(new Img()
                                {
                                    Data = uploader.MakeImageSrcData(Path.Combine(HttpContext.Current.Request.MapPath(UploadPath), item.Id + item.Data)),
                                    Id = string.Format(@"imgFileId_{0}", item.Id)
                                });
                            }
                        }
                this.ImgList = Imgl;
             }
            ImgCount = !ImgList.Equals(null) ? ImgList.Count : 0;
        }
    }
}