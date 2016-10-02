using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicTesting.Models.DataModel
{
    public class Message
    {
        public int MessageId { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public string FileDir { get; set; }
        public DateTime Date{ get; set; }
        public bool IsRead { get; set; }
        public string AuthorName { get; set; }
    }
}