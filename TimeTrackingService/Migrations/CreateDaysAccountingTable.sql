CREATE TABLE DaysAccounting(
	Id uniqueidentifier NOT NULL,
	Hours int NOT NULL CHECK(Hours >= 1 AND Hours <= 24),
	Day int NOT NULL CHECK(Day >= 1 AND Day <= 31),
	Month int NOT NULL CHECK(Month >= 1 AND Month <= 12),
	Year int NOT NULL CHECK(Year >=2000 AND Year <= 3000),
	Date date NOT NULL,
	AccountingType int NOT NULL,
	IsConfirmed bit NOT NULL,
	UserId uniqueidentifier NOT NULL,
	CONSTRAINT PK_DaysAccounting PRIMARY KEY CLUSTERED 
	(
		Id ASC
	)
);
