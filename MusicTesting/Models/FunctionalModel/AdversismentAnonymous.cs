using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicTesting.Models.HelperModel;

namespace MusicTesting.Models.FunctionalModel
{
    /// <summary>
    /// Клас з допомогою якого можна подати оголошення
    /// без авторизації на сайті.
    /// </summary>
    public class AdversismentAnonymous: Advertisment
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
            public override Result AddAdversisment(string header, string paragraph, string location,  string reciveText, string phonenumber,
            string email, int? country, int? city, int? street, string adressText, string fileDir)
        {
                try
                {

                var reciveAdress = new Adress()
                {
                    Country = country == null ? 0 : (int)country,
                    City = city,
                    Street = street,
                    Text = adressText
                };
                DB.Entry(reciveAdress).State = System.Data.Entity.EntityState.Added;
                DB.SaveChanges();
                //Сторюємо обєкт для запису оголошеня
                var recive = new OgNotice()
                    {
                        Header = header,
                        Paragraph = Convert.ToInt32(paragraph),
                        UserId = 0,
                        Message = reciveText,
                        OgLocation = Convert.ToInt32(location),
                        IsVoided = false,
                        Files = fileDir,
                        Date = DateTime.Now,
                        AdressId = reciveAdress.Id
                    };
                    //Створюємо обєкт Контантна інформація і записуємо такий в базу 
                    //після витягуємого його ідентифікатор для того щоб
                    //прикрутити його до оголошення
                    if (email != string.Empty || phonenumber != string.Empty)
                    {
                        var contact = new OgContact() { Email = email, PhoneNumber = phonenumber };
                        DB.Entry(contact).State = System.Data.Entity.EntityState.Added;
                        DB.SaveChanges();
                        recive.OgContacts = contact.Id;
                    }
                    //Записуємо оголошення в базу
                    DB.Entry(recive).State = System.Data.Entity.EntityState.Added;
                    DB.SaveChanges();
                }
                catch
                {
                    return new Result() { IsSucces = false, Message = "Insert error" };
                }
                return new Result() { IsSucces = true, Message = "" };
            }
        }
    }


