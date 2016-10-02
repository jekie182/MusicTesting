using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicTesting.Models.DataModel
{
    /// <summary>
    /// Клас для зобрадень і роботи з ними при передачі на клієнт
    /// </summary>
    public class Img
    {
        /// <summary>
        /// Поле ідентифікатор зображення
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Поле в якому буде міститись кодоване зображення,
        /// що передаватиметься в <img scr = "Data"/>
        /// </summary>
        public string Data { get; set; }
    }
}