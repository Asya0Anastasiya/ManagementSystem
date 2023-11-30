CREATE TABLE Documents (
    Id uniqueidentifier NOT NULL,
    Name nvarchar(60) NOT NULL,
    Type int NOT NULL CHECK(Type >=1 AND Type <= 5),
	SourceId uniqueidentifier NOT NULL,
	UserId uniqueidentifier NOT NULL,
	CONSTRAINT PK_Documents PRIMARY KEY CLUSTERED 
	(
		Id ASC
	)
);