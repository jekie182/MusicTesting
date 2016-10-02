using System;
using MusicTesting.Interface;


namespace MusicTesting.Models.FunctionalModel
{
    /// <summary>
    /// Базовний Клс, що міститие в дочірніх клаах реалізації входу та реєстрації
    /// </summary>
    public class Authorithation : IDataEntityModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        private MTDBEntities db = new MTDBEntities();
        public Authorithation()
        {
            DB = new MTDBEntities();
        }

        public MTDBEntities DB
        {
            get
            {
                return db;
            }
            set
            {}
        }
    }
}