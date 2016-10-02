using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicTesting.Models.HelperModel;
using MusicTesting.Models.DataModel;
using MusicTesting.Models.ViewModel;

namespace MusicTesting.Models.FunctionalModel
{
    /// <summary>
    /// Клас, що інкапсулює в собі функціонал реєстрації користувача
    /// </summary>
    public class Registration : Authorithation
    {
        /// <summary>
        /// Метод для створення профіля користувача
        /// </summary>
        /// <param name="usernamesignup"></param>
        /// <param name="emailsignup"></param>
        /// <param name="passwordsignup"></param>
        /// <param name="passwordsignup_confirm"></param>
        /// <returns>
        /// В разі якщо реєстрація проходе вдало то результатом є 
        /// див Login().LogIn(...)
        /// </returns>
        public Result<User> CreateAccount(string usernamesignup, string emailsignup, string passwordsignup, string passwordsignup_confirm)
        {
            if (new HelperFunction().CheckUserExist(usernamesignup, emailsignup) && passwordsignup.Equals(passwordsignup_confirm))
            {

                Users us = new Users() { Email = emailsignup, UserName = usernamesignup, Password = passwordsignup };
                DB.Entry(us).State = System.Data.Entity.EntityState.Added;
                DB.SaveChanges();
                var result = from u in DB.Users where u.Password.Equals(passwordsignup) && u.Email.Equals(emailsignup) && u.UserName.Equals(usernamesignup) select u;
                Users user = result.ToList<Users>().FirstOrDefault<Users>();

                if (user != null)
                {
                    FeedBack feedBack = new FeedBack() { Email = user.Email };
                    DB.Entry(feedBack).State = System.Data.Entity.EntityState.Added;
                    DB.SaveChanges();
                    Adress adress = new Adress() { Build = 0, City = 1, Country = 1, Flat = 0, Street = 1, Text = "" };
                    DB.Entry(adress).State = System.Data.Entity.EntityState.Added;
                    DB.SaveChanges();

                    Person person = new Person() { UserId = user.Id, FeedBack = feedBack.Id, Adress = adress.Id, AboutMe = "Its me", Avatar = 1, DOB = DateTime.Now, FirstName = "Me", LastName = "Me" };
                    PersonBalance balance = new PersonBalance() { DateOfRegBalance = DateTime.Now, Balance = "0.00", PersonId = user.Id };

                    DB.Entry(person).State = System.Data.Entity.EntityState.Added;
                    DB.Entry(balance).State = System.Data.Entity.EntityState.Added;
                    DB.SaveChanges();
                    return new Result<User>()
                    {
                        IsSucces = true,
                        Object = new User(user.Id)
                    };
                }
            }
            return new Result<User>()
            {
                IsSucces = false,
                Object = null,
                Message = string.Format(@"Корисувач з іменем: {0} вже існує або пароль Ви допустили помилку при підтвердженні пароля! ", usernamesignup)
            };
        }
    }
}