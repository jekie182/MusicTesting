using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicTesting.Interface;
using MusicTesting.Models.HelperModel;

namespace MusicTesting.Models.FunctionalModel
{
    /// <summary>
    /// Базовний клас для функціональних класі, які працюватимуть з подачею оголошення
    /// </summary>
    public class Advertisment : IDataEntityModel
    {
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

        public Advertisment()
        {
        }

        /// <summary>
        /// Метод орзрахований для реалізації при доданні анонімного оголошення
        /// </summary>
        /// <param name="header"></param>
        /// <param name="paragraph"></param>
        /// <param name="reciveText"></param>
        /// <param name="phonenumber"></param>
        /// <param name="email"></param>
        /// <param name="location"></param>
        /// <param name="fileDir"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual Result AddAdversisment(string header, string paragraph, string location, string reciveText, string phonenumber,
            string email, int? country, int? city, int? street, string adressText, string fileDir)
        {
            return (new object()) as Result;
        }
        /// <summary>
        /// Метод орієвнтований на реалізацію додавання оголошеь авторизованими користувачами
        /// </summary>
        /// <param name="header"></param>
        /// <param name="paragraph"></param>
        /// <param name="reciveText"></param>
        /// <param name="fileDir"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual Result AddAdversisment(string header, string location, string paragraph, string reciveText, string fileDir, int userId)
        {
            return (new object()) as Result;
        }

        /// <summary>
        /// Видалення оголошення, доступний лише авторизованим користувачам та в разі якшо оголошення є їхнім. 
        /// </summary>
        /// <param name="idAdversiment"></param>
        /// <returns></returns>
        public virtual Result DeleteAdversiment(int idAdversiment)
        {
            return (new object()) as Result;
        }


    }
}