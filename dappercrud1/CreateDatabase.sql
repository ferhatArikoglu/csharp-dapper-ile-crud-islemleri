IF DB_ID('OKUL') IS NOT NULL
BEGIN
	ALTER DATABASE OKUL SET SINGLE_USER
	WITH ROLLBACK IMMEDIATE 
	USE master
	DROP DATABASE OKUL	 
END 
CREATE DATABASE OKUL
GO
USE OKUL
CREATE TABLE [Ogrencis](
	[Id] [int]  NOT NULL identity(1,1),
	[OgrAd] [nvarchar](50) NULL,
	[OgrSoyad] [nvarchar](50) NULL,
	[OgrBolum] [nvarchar](50) NULL,
)
INSERT INTO Ogrencis( OgrAd, OgrSoyad, OgrBolum)
VALUES ('value1', 'value2', 'value3');

