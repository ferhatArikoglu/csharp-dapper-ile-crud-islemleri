GO
USE OKUL
go
CREATE PROCEDURE [dbo].[Procedure3]
	@OgrAd varchar(50),
	@OgrSoyad varchar(50),
	@OgrBolum varchar(50)
AS
begin
insert into Ogrencis(OgrAd,OgrSoyad,OgrBolum)values(@OgrAd,@OgrSoyad,@OgrBolum)
end