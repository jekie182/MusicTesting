
--This query for the all advertisement of the author
select 

ogc.Email,
ogc.PhoneNumber,
ogl.Name,
ogn.Id,
ogn.Header,
ogn.Files,
ogn.[Date]
from OgNotice ogn

right join OgContact as ogc on ogc.Id = ogn.OgContacts  
right join OgLocation as  ogl on ogn.OgLocation = ogl.Id

where ogn.UserId = 31
