select  
ogn.Id as Id,
ogn.Header as Header,
loc.Name as [Location],
ogn.[Message] as AdvertisementBody,
ogn.[Files] as FileDir,
ogn.[Date] as PostedDate,
par.Name as Paragraph,
adr.[Text] as Adresstext,
coun.Name as CountryName,
cit.Name as CityName,
stre.Name as StreetName,
ogcon.Email as Email,
ogcon.PhoneNumber as PhoneNumber,
per.FirstName as AuthorName,
per.LastName as AuthorLastName,
per.UserId as AuthorId
from OgNotice ogn

join Paragraph as par on par.Id = ogn.Paragraph 
join Adress as adr on adr.Id = ogn.AdressId
join Country as coun on adr.Country = coun.Id
join City as cit on adr.City = cit.Id
join Street as stre on adr.Street = stre.Id
join OgContact as ogcon on ogn.OgContacts = ogcon.Id
join OgLocation as loc on loc.Id = ogn.OgLocation
join Person as per on per.UserId = ogn.UserId

where ogn.Id = 37