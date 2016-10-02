using MusicTesting.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicTesting.Models.ViewModel
{
    public class MessagesView:BaseView
    {
        public List<Message> MessageList { get; set; }
        public MessagesView() {
            MessageList = new List<Message>();
        }

        public MessagesView(int userId)
        {
            MessageList = new List<Message>();
            using (var DB  = new MTDBEntities())
            {
                var result = DB.Database.SqlQuery<Message>(@"
                    select
m.Id as MessageId,
m.FromUser as FromUserId,
m.ToUser as ToUserId,
m.Header, m.Body, m.[Date], m.IsRead,
p.FirstName + ' ' + p.LastName as AuthorName
from Messages as m
left join Person as p on m.FromUser = p.UserId
where m.ToUser = @p0 or m.FromUser = @p0
order by [Date] desc
                    ", userId);
                if (result!= null)
                    {
                        MessageList = result.ToList<Message>();
                    }
            }
        }
        public void SendMessage(int fromUserId, int toUserId, string hader = "", string body = "", string fileDire = "")
        {
            var mes = new Messages()
            {
                Body = body,
                Header = hader,
                IsRead = false,
                Date = DateTime.Now,
                FromUser = fromUserId,
                ToUser = toUserId,
                FileDir = fileDire
            };
            using (var DB = new MTDBEntities())
            {
                DB.Entry(mes).State = System.Data.Entity.EntityState.Added;
                DB.SaveChanges();
            }
        }

        public void ReadMessage(int messageId)
        {
            using (var DB = new MTDBEntities())
            {
                DB.Database.
                    ExecuteSqlCommand(@"update Messages set IsRead = 1 where Id = @p0", messageId);
            }
        }
    }
}