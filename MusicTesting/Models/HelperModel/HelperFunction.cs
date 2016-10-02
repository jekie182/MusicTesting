using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicTesting.Models.HelperModel
{
    /// <summary>
    /// Клас для споміжного функціоналу який ніяким чином не узагальнюєтсья
    /// </summary>
    public class HelperFunction
    {
        private MTDBEntities DB = new MTDBEntities();

        /// <summary>
        /// Метод який при реєстрації превіряє на наявність користувача з вказаною електронною адресою чи імям користувача
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// <returns>
        /// Булівський результат в залежності від того чи маємо вказане мило чи імя в базі даних користувачів
        /// </returns>
        public bool CheckUserExist(string userName, string email)
        {
            var result = from u in DB.Users where u.Email.Equals(email) || u.UserName.Equals(userName) select u;
            Users user = result.ToList<Users>().FirstOrDefault<Users>();
            if (user != null)
                return false;
            return true;
        }
    }
}