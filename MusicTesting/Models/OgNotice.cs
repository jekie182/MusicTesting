//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MusicTesting.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OgNotice
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public Nullable<int> Paragraph { get; set; }
        public string Message { get; set; }
        public string Files { get; set; }
        public int OgContacts { get; set; }
        public int OgLocation { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<bool> IsVoided { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> AdressId { get; set; }
    }
}