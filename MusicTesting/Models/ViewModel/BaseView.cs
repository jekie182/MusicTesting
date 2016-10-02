using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicTesting.Models.ViewModel
{
    /// <summary>
    /// Базовий кас для всих класів екземпляри якиз передаються в представлення
    /// </summary>
    public class BaseView
    {
        /// <summary>
        /// Поле з допомогою якого власне реалізовується Особистий кабінет(віджет)
        /// дістати його можна з моделі як CurrentUser, 
        /// в місці де необхідно викликати метод з ацді користувачем слід
        /// звертатись таким чином
        /// @Model.CurrentUser.UserId
        /// Для відображення особистихданих
        /// @Model.CurrentUser.UserInfo.Назва поля яке нас ікавить (див. клас UserInfo)
        /// </summary>
        public User CurrentUser { get; set; }
    }
}