CREATE TABLE Branches (
	Id uniqueidentifier NOT NULL,
	Name nvarchar(30) NOT NULL,
	AdressId uniqueidentifier NOT NULL,
	CONSTRAINT PK_Branches PRIMARY KEY CLUSTERED 
	(
		Id ASC
	),
	CONSTRAINT FK_Branches_Adresses_AdressId FOREIGN KEY(AdressId)
		REFERENCES Addresses (Id)
);
