using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicTesting.Models.DataModel;
using System.IO;
using MusicTesting.Models.FunctionalModel;
using MusicTesting.Interface;

namespace MusicTesting.Models.ViewModel
{
    /// <summary>
    /// Клас який інкапсулює в собі дані про користувача
    /// і екземпляр якого є в кожному представленні,
    /// в разі якшо користуач анонім то значення його нал, на основі цього відбуваєтсяь ренерінг 
    /// деяких контролів на стороні клієнта.
    /// </summary>
    public class User : IDataEntityModel
    {
        private readonly string UploadPath = "~/Files/";

        public int UserId { get; set; }
        public UserSetting UserSetting { get; set; }
        public UserInfo UserInfoData { get; set; }

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

        public User() {
          
        }

        public User(int userId)
        {
            UserId = userId;

            var result = (from person in DB.Person
                          join feedback in DB.FeedBack on person.FeedBack equals feedback.Id
                          join balance in DB.PersonBalance on person.PersonId equals balance.PersonId
                          join adress in DB.Adress on person.Adress equals adress.Id
                          join files in DB.Files on person.Avatar equals files.Id
                          //join for Adress definishion
                          join country in DB.Country on adress.Country equals country.Id
                          join city in DB.City on adress.City equals city.Id
                          join stret in DB.Street on adress.Street equals stret.Id

                          where person.UserId == UserId
                          select new UserInfo
                          {
                              Email = feedback.Email,
                              Balane = balance.Balance,
                              DateOfReg = balance.DateOfRegBalance,
                              FirstName = person.FirstName,
                              LastName = person.LastName,
                              PhoneNumber = feedback.PhoneNumber,
                              AboutMe = person.AboutMe,
                              Avatar = files.Id.ToString() + files.Data.ToString(),
                              CountryName = country.Name,
                              CityName = city.Name,
                              StreetName = stret.Name,
                              AdressText = adress.Text

                          }).ToList().FirstOrDefault();
            if (result != null)
            {
                result.Adress = result.CountryName + " " + result.CityName + "  " + result.StreetName + "  " + result.AdressText;
                UserInfoData = result;
                UserInfoData.Avatar = new Uploader().MakeImageSrcData(Path.Combine(HttpContext.Current.Request.MapPath(UploadPath), UserInfoData.Avatar));
            }
        }
    }
}