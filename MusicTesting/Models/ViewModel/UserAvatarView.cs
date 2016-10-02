using MusicTesting.Models.Constant;
using MusicTesting.Models.FunctionalModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MusicTesting.Models.ViewModel
{
    public class UserAvatarView
    {
        public string Avatar { get; set; }
        public int UserId { get; set; }

        public UserAvatarView() { }

        public UserAvatarView(int userId)
        {
            UserId = userId;
            using (var context = new MTDBEntities())
            {
                string fileName = context.Database.SqlQuery<string>(@"
select 
CONVERT(varchar(max), f.Id) +f.[Data] as [FileName]

from Person p
left join Files as f on f.Id = p.Avatar
where p.UserId = @p0", userId).First();
                if (fileName != null && fileName != string.Empty)
                {
                    Avatar = new Uploader().MakeImageSrcData(Path.Combine(HttpContext.Current.Request.MapPath(FileDirect.UploadPath), fileName));
                }
                else Avatar = @"~/Content/images/non-avatar-image.png";
            }
        }

        public void UpdateAvatar(ref HttpPostedFileBase uploadImages, int userId)
        {
            var result = new Uploader().UpdateAvatar(ref uploadImages);

            if (result != null)
            {
                new Logger().WriteLog(3,
                    string.Format(@"User with Id = {0}. Update his avatar on file with Id = {1}"
                    , userId, result.Object.ToString()));

                using (var context = new MTDBEntities())
                {
                    context.Database.ExecuteSqlCommand("update Person set Avatar = @p0 where Userid = @p1", result.Object, userId);
                }
            }
        }
    }
}