using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicTesting.Models.DataModel
{
    /// <summary>
    /// Клас одного оголошення, яке буде відображатися
    /// коли ми переходимо на індексну сторінку, і після пошуку.
    /// Тобто, результатом пошуку чи тому буде список відповідних екземплярів цього класу.
    /// </summary>
    public class Advertisement
    {
            public int Id { get; set; }
            public string Header { get; set; }
            public string Location { get; set; }
            public DateTime? PostedDate { get; set; }
            public List<Img> ImgList { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
    }
}