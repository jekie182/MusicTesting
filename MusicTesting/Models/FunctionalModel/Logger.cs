using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicTesting.Models.FunctionalModel
{
    public class Logger
    {
        public void WriteLog(int logType, string description)
        {
            using (var context = new MTDBEntities())
            {
                var item = new Log()
                {
                    Description = description,
                    LogType = logType,
                    LogDate = DateTime.Now
                };

                context.Entry(item).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
            }
        }
    }
}