create database ArtistDB;
go
use ArtistDB
go

create table Artists(
ID int not null primary key identity(1,1),
ArtistName nvarchar(200) not null check(ArtistName!='')
);
