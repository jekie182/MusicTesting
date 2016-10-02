using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicTesting.Models.HelperModel;
using MusicTesting.Models.ViewModel;

namespace MusicTesting.Models.FunctionalModel
{
    /// <summary>
    /// Клас в якому інкапсульовапна реалзація входу до сервісу
    /// </summary>
    public class Login : Authorithation
    {
        /// <summary>
        /// Мето який виконує авторизацію на сайті
        /// </summary>
        /// <returns>
        /// Значення яке повертаю функція це екземпяр класу Result 
        /// з обєктом класа User, що записується в сесію і як поле будь-якого класа, що предаєтьмя в представлення
        /// в якому дістати його можна з допомогою CurrentUser а для 
        /// детальнішої інфи через поле UserInfo
        /// </returns>
        public Result<User> LogIn()
        {
            Users user;
            if (UserName.Contains('@'))
            {
                var result = from u in DB.Users where u.Password.Equals(Password) && u.Email.Equals(UserName) select u;
                user = result.ToList<Users>().FirstOrDefault<Users>();
            }
            else
            {
                var result = from u in DB.Users where u.Password.Equals(Password) && u.UserName.Equals(UserName) select u;
                user = result.ToList<Users>().FirstOrDefault<Users>();
            }

            if (user != null)
            {
                return new Result<User>()
                {
                    IsSucces = true,
                    Message = string.Empty,
                    Object = new User(user.Id)
                };
            }
            else
                return new Result<User>()
                {
                    IsSucces = false,
                    Object = null,
                    Message = string.Format(@"Користувач {0} не існує чи ,можливо, невірний пароль!", UserName)
                };
        }
    }
}