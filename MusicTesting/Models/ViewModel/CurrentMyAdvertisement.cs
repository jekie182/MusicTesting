using MusicTesting.Models.Constant;
using MusicTesting.Models.DataModel;
using MusicTesting.Models.FunctionalModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MusicTesting.Models.ViewModel
{
    public class CurrentMyAdvertisement
    {
       
        public int Id { get; set; }
        public string Header { get; set; }
        public int ParagraphId { get; set; }
        public string AdvertisementBody { get; set; }
        public string Files { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public int StreetId { get; set; }
        public string AdressBody { get; set; }

        public CurrentMyAdvertisement() { }

        public CurrentMyAdvertisement GetCurrentMyAdvertisement(int Id)
        {
            using( MTDBEntities context = new MTDBEntities())
            {
                var model = context.Database.SqlQuery<CurrentMyAdvertisement>(@"
select 
ogn.Id as Id,
ogn.Header,
ogn.Paragraph as ParagraphId,
ogn.[Message] as AdvertisementBody,
ogn.[Files], 
ogc.Email,
ogc.PhoneNumber,
ISNULL(adr.Country, 1 )  as CountryId,
ISNULL(adr.City, 1 )  as CityId,
ISNULL(adr.Street, 1 )  as StreetId,
adr.[Text] as AdressBody

 from OgNotice as ogn

left join OgContact as ogc on ogn.OgContacts = ogc.Id
left join Adress as adr on ogn.AdressId = adr.Id

where ogn.Id = @p0", Id).FirstOrDefault();
                return model;
            }
        }
    }
}