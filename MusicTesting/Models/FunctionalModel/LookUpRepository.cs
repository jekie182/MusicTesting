using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicTesting.Models.FunctionalModel
{
    public class LookUpRepository
    {
        public List<Paragraph> GetParagrapgList()
        {
            using (var context = new MTDBEntities())
            {
               return  (from p in context.Paragraph select p).ToList();
            }
        }

        public List<Country> GetCountryList()
        {
            using (var context = new MTDBEntities())
            {
                return (from p in context.Country select p).ToList();
            }
        }

        public List<City> GetCityList()
        {
            using (var context = new MTDBEntities())
            {
                return (from p in context.City select p).ToList();
            }
        }

        public List<Street> GetStreetList()
        {
            using (var context = new MTDBEntities())
            {
                return (from p in context.Street select p).ToList();
            }
        }
    }
}