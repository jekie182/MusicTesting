using MusicTesting.Models.DataModel;
using MusicTesting.Models.FunctionalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MusicTesting.Models.ViewModel
{
    public class MyAdvertisement:BaseView
    {
        
        public MyAdvertisement(){}

        public List<MyAdvertisementData>  GetMyArvertisementList(int userId, int pageN)
        {
            var myAdvertsementList = new List<MyAdvertisementData>();
            using (var context = new MTDBEntities())
            {
                var result = context.Database.SqlQuery<MyAdvertisementData>(@"  
select top 5
ogc.Email,
ogc.PhoneNumber,
ogl.Name as Location,
ogn.Id,
ogn.Header,
ogn.[Date]
from OgNotice ogn
right join OgContact as ogc on ogc.Id = ogn.OgContacts
right join OgLocation as  ogl on ogn.OgLocation = ogl.Id

where ogn.UserId  = @p0 and ogn.isvoided = 0
order by ogn.Date desc
", userId).ToList();
                if (result != null)
                {
                    int count = result.Count;

                    if (count <= 5)
                        {
                        AddClassName(ref result);
                            return result;
                        }
                    else
                    {
                        int startItem = (pageN - 1) * 5;
                        int finItem = (pageN * 5) - 1;

                        int finIndex = (count - 1 > finItem) ? finItem : count-1 ;

                        for (int index = startItem; index <= finItem; index++)
                        {
                            myAdvertsementList.Add(result[index]);
                        }
                    }
                }
                AddClassName(ref myAdvertsementList);
            }
            return myAdvertsementList;
        }

        public void AddClassName(ref List<MyAdvertisementData> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        list[i].ClassName = "choiceA";
                        break;
                    case 1:
                        list[i].ClassName = "choiceB";
                        break;
                    case 2:
                        list[i].ClassName = "choiceC";
                        break;
                    case 3:
                        list[i].ClassName = "choiceD";
                        break;
                    case 4:
                        list[i].ClassName = "choiceE";
                        break;
                } 
            }
        }

        public void DeleteMyAdvertisemnt(int advertsementId)
        {
            using (var context = new MTDBEntities())
            {
                var result = context.Database.ExecuteSqlCommand(@"  
update OgNotice 
set Isvoided = 1
where id = @p0", advertsementId);
            }
        }
     }
}