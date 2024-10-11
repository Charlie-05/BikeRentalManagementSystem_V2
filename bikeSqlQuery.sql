create database BikeRentalManagement;
use BikeRentalManagement ;

create table Bikes(
	BikeId nvarchar(50) primary key,
	Brand nvarchar(50) ,
	 Model nvarchar(50),
	RentalPrice decimal(18,2) 
);

insert into Bikes(BikeId ,Brand ,Model ,RentalPrice)
values('BIKE_001' , 'Honda' , 'CB-Shine' , 5.00)
