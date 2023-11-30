CREATE TABLE Addresses (
	Id uniqueidentifier NOT NULL,
	Country nvarchar(30) NOT NULL,
	City nvarchar(30) NOT NULL,
	Strit nvarchar(35) NOT NULL,
	HouseNumber nvarchar(10) NOT NULL,
 CONSTRAINT PK_Adresses PRIMARY KEY CLUSTERED 
(
	Id ASC
)
);
