using MusicTesting.Models.HelperModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicTesting.Models.ViewModel
{
    /// <summary>
    /// Клас обєкт якого передажтся
    /// в представлення де відображатимуться особисті дані
    /// користувача, наприклад анкетні даніБ які в цій сторінці можна редагувати
    /// </summary>
    public class PersonAncet : BaseView
    {
        public string AboutMe { get; set; }
        public string AdressText { get; set; }
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public int StreetId { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public PersonAncet()
            {}


            public PersonAncet GetAncet(int userId)
            {
                using (var context  = new MTDBEntities())
                {
                var result = context.Database.SqlQuery<PersonAncet>(@"
                        select 
                        p.AboutMe,
                        p.DOB ,
                        p.FirstName,
                        p.LastName,
                        a.[Text] as AdressText,
                        a.City as CityId,
                        a.Country as CountryId,
                        a.Street as StreetId,
                        c.Email,
                        c.PhoneNumber

                        from Person p

                        join  Adress a on a.Id = p.Adress
                        join OgContact c on c.Id = p.FeedBack
                        where p.UserId = @p0", userId).First();
                    if (result != null)
                        return result;
                }
            return new PersonAncet();
            }
        /// <summary>
        /// Метод для збереження анкетних даних користувача
        /// </summary>
        /// <param name="avatarId"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="dob"></param>
        /// <param name="about"></param>
        /// <param name="country"></param>
        /// <param name="city"></param>
        /// <param name="street"></param>
        /// <param name="build"></param>
        /// <param name="userId"></param>
        /// <returns>
        /// В результаті повертаєтсья новий екземпляр класу для того аби оновити представлення з даними
        /// </returns>
            //public Result<Ancet> AncetSave(int? avatarId, string firstName, string lastName, string email,
            //    string phone, DateTime dob, string about, int country, int city, int street, string build, int userId)
            //{
            //    try
            //    {
            //        Person person = (from p in DB.Person where p.UserId == userId select p).ToList().FirstOrDefault();

            //        //person update
            //        if (avatarId != null && avatarId != 1)
            //            person.Avatar = avatarId;
            //        person.FirstName = firstName;
            //        person.LastName = lastName;
            //        person.AboutMe = about;
            //        person.DOB = dob;
            //        //feedback update

            //        var feedBack = (from f in DB.FeedBack where f.Id == person.FeedBack select f).ToList().FirstOrDefault();
            //        feedBack.PhoneNumber = phone;
            //        feedBack.Email = email;

            //        var adress = (from a in DB.Adress where a.Id == person.Adress select a).ToList().FirstOrDefault();
            //        adress.Country = country;
            //        adress.City = city;
            //        adress.Street = street;
            //        adress.Text = build;

            //        DB.Entry(adress).State = System.Data.Entity.EntityState.Modified;
            //        DB.Entry(feedBack).State = System.Data.Entity.EntityState.Modified;
            //        DB.Entry(person).State = System.Data.Entity.EntityState.Modified;

            //        DB.SaveChanges();
            //        var homeModel = new Ancet(userId);
            //        homeModel.CurrentUser = new User(userId);
            //        return new Result<Ancet>() { IsSucces = true, Object = homeModel };
            //    }
            //    catch
            //    {
            //        return new Result<Ancet> { IsSucces = false, Message = "Помилка при збиреженні даних. Спробуйте ще раз!" };
            //    }
            //}
        }
    }
