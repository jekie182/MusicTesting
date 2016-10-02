using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicTesting.Models.DataModel
{
    public class AdversismentLoginSave
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Country { get; set; }
        public int City { get; set; }
        public int Street { get; set; }
        public string AdressText { get; set; }
    }
}