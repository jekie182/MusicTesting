using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicTesting.Models.DataModel
{
    /// <summary>
    /// Клас - дата який інкапсулює інформацію про користувача який щойно 
    /// залогінивсь, і ця же інфа буде відображатись в особистому кабінеті
    /// та як анкетні дані, доступ до даних з представлення буде здійснюватись
    /// за допомогою екземпляра класа User та полем під назвою CurrentUser
    /// яке міститимуть сі моделі, що переаються у представлення
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// Імя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Прізвище
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Номер телефону
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Адреса яка буде використовуватись і для того,
        /// наприклад, якщо користувачподаватиме оголошення
        /// вже авторизованим тоді, логіка передбача таку можливість
        /// щоб користувач не заповнював зайві поля
        /// </summary>
        public string Adress { get; set; }
        /// <summary>
        /// Стан рахунку
        /// </summary>
        public string Balane { get; set; }
        /// <summary>
        /// Електронна адреса
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Ійдішнік зображення з аватаркой
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// Дані про себе які користувач вводе у  вільному форматі
        /// </summary>
        public string AboutMe { get; set; }
        /// <summary>
        /// Дата реєстрації
        /// </summary>
        public DateTime DateOfReg { get; set; }
        /// <summary>
        /// Назва країни користувача для текстового відображення
        /// </summary>
        public string CountryName { get; set; }
        /// <summary>
        /// Імя міста, для відображення як текст
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// Назва вулиці
        /// </summary>
        public string StreetName { get; set; }
        /// <summary>
        /// супровідний текст до адреси, наприклад 'Живу в другому підїзді'
        /// </summary>
        public string AdressText { get; set; }
    }
}