CREATE TABLE Documents (
                    Id uniqueidentifier NOT NULL,
                    Name nvarchar(60) NOT NULL,
                    UserId uniqueidentifier NOT NULL,
                    UploadDate date NOT NULL,
	                ContentType nvarchar(20) NOT NULL,
	                Size float NOT NULL CHECK(Size > 0 AND Size <= 20000),
	                Type int NOT NULL CHECK(Type >=1 AND Type <= 5),
	                CONSTRAINT PK_Documents PRIMARY KEY CLUSTERED 
	                (
		                Id ASC
	                )
                );