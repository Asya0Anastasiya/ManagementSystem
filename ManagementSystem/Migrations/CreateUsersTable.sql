CREATE TABLE Users (
	Id uniqueidentifier NOT NULL,
	FirstName nvarchar(50) NOT NULL,
	LastName nvarchar(50) NOT NULL,
	Email nvarchar(50) NOT NULL,
	PositionId uniqueidentifier NOT NULL,
	Role int NOT NULL,
	PhoneNumber nvarchar(15) NULL,
	Password nvarchar(65) NOT NULL,
	UserImage varbinary(max) NULL,
	CONSTRAINT PK_Users PRIMARY KEY CLUSTERED 
	(
		Id ASC
	),
	CONSTRAINT FK_Users_Positions_PositionId FOREIGN KEY(PositionId)
		REFERENCES Positions (Id)
);
