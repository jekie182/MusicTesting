using MusicTesting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicTesting.Interface
{
    /// <summary>
    /// Інтерфейс від якого наслідуются класи яким потрібен доступ до бази даних
    /// </summary>
    interface IDataEntityModel
    {
        /// <summary>
        /// Поле для екземпляра яерез який работаємо з базою даних
        /// </summary>
       MTDBEntities DB { get; set; }
    }
}
