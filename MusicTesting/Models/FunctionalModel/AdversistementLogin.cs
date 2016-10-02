using MusicTesting.Models.DataModel;
using MusicTesting.Models.HelperModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicTesting.Models.FunctionalModel
{
    public class AdversistementLogin: Advertisment
    {
            /// <summary>
            /// Метод для створення оголошення анонімно
            /// </summary>
            /// <param name="header"> Заголовок оголошення</param>
            /// <param name="paragraph"> Розділ, типу продажа ЖКХ, чи щось інше</param>
            /// <param name="reciveText">Опис,або більш детальна інформація про оголошення</param>
            /// <param name="phonenumber">Контактний номер</param>
            /// <param name="email">Контактна електронна адреса</param>
            /// <param name="location">Місцезнаходження</param>
            /// <param name="fileDir">Рядок в якому зашивровані айдішніки прикріплених файлів</param>
            /// <param name="userId">тільки тоді коли користувач авторизований</param>
            /// <returns></returns>
            public override Result AddAdversisment(string header, string location,  string paragraph, string reciveText, string fileDir, int userId)
            {
                try
                {
                var result = (from person in DB.Person
                              join feedback in DB.FeedBack on person.FeedBack equals feedback.Id
                              join adress in DB.Adress on person.Adress equals adress.Id
                              join country in DB.Country on adress.Country equals country.Id
                              join city in DB.City on adress.City equals city.Id
                              join stret in DB.Street on adress.Street equals stret.Id

                              where person.UserId == userId
                              select new AdversismentLoginSave
                              {
                                  Email = feedback.Email,
                                  PhoneNumber = feedback.PhoneNumber,

                                  Country = country.Id,
                                  City = city.Id,
                                  Street = stret.Id,
                                  AdressText = adress.Text
                              }).ToList().FirstOrDefault();

                var con = new OgContact() {Email = result.Email, PhoneNumber = result.PhoneNumber };
                var loc = new Adress() { City = result.City, Country = result.Country, Street = result.Street, Text = result.AdressText };
                DB.Entry(con).State = System.Data.Entity.EntityState.Added;
                DB.Entry(loc).State = System.Data.Entity.EntityState.Added;
                DB.SaveChanges();
                //Сторюємо обєкт для запису оголошеня
                var recive = new OgNotice()
                    {
                        OgContacts = con.Id,
                        Header = header,
                        Paragraph = Convert.ToInt32(paragraph),
                        UserId = userId,
                        Message = reciveText,
                        OgLocation = Convert.ToInt32(location),
                        IsVoided = false,
                        Files = fileDir,
                        Date = DateTime.Now,
                        AdressId = loc.Id
                    };
                    DB.Entry(recive).State = System.Data.Entity.EntityState.Added;
                    DB.SaveChanges();
                }
                catch
                {
                    return new Result() { IsSucces = false, Message = "Insert error" };
                }
                return new Result() { IsSucces = true, Message = "" };
            }

            public Result UpdateAdvertisement(
                string phoneNumber, string email,
                string header, int? paragraph, string advertisementBody, int? country, int? city,
                int? street, string adressText, int advertisementId )
            {
            var result = new Result() { IsSucces = true, Message = string.Empty, Object = null };
            try{
                using (var DB = new MTDBEntities())
                {
                    #region
                    //var advert = (from a in DB.OgNotice where a.Id == advertisementId select a).First();
                    //var contact = (from c in DB.OgContact where c.Id == advert.OgContacts select c).First();
                    //var adress = (from adr in DB.Adress where adr.Id == advert.AdressId select adr).First();

                    //contact.Email = email;
                    //contact.PhoneNumber = phoneNumber;

                    //adress.Country = (country== null)? 1 : (int)country;
                    //adress.City = city;
                    //adress.Street = street;
                    //adress.Text = adressText;

                    //advert.Header = header;
                    //advert.Paragraph = paragraph;
                    //advert.Message = advertisementBody;
                    #endregion
                    DB.Database.ExecuteSqlCommand(@"
declare @contactId int = (select ogn.OgContacts from OgNotice as ogn where ogn.Id = @p0) 
declare @adressId int = (select ogn.AdressId from OgNotice as ogn where ogn.Id = @p0) 

update OgContact
set email = @p1, phonenumber = @p2  where id = @contactId

update Adress
set City = @p3, Country = @p4, Street = @p5, [Text] = @p6 where Id = @adressId

update OgNotice 
set Header = @p7, Paragraph = @p8, [Message] = @p9 where id = @p0",

advertisementId, email, phoneNumber, city, country, street, adressText, header, paragraph, advertisementBody);
                }
            }
            catch {
                result.IsSucces = false;
                result.Message = "We got some trouble while updating the record!";
            }
                
                return result;
            }
        }
    }
