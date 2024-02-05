CREATE TABLE Addresses (
	                Id uniqueidentifier NOT NULL,
	                Country nvarchar(30) NOT NULL,
	                City nvarchar(30) NOT NULL,
	                Street nvarchar(35) NOT NULL,
	                HouseNumber nvarchar(10) NOT NULL,
                    CONSTRAINT PK_Addresses PRIMARY KEY CLUSTERED 
                    (
	                    Id ASC
                    )
                    );

                CREATE TABLE Branches (
					Id uniqueidentifier NOT NULL,
					Name nvarchar(30) NOT NULL,
					AddressId uniqueidentifier NOT NULL,
					CONSTRAINT PK_Branches PRIMARY KEY CLUSTERED 
					(
						Id ASC
					),
					CONSTRAINT FK_Branches_Addresses_AddressId FOREIGN KEY(AddressId)
						REFERENCES Addresses (Id)
					);
				
				CREATE TABLE Departments(
					Id uniqueidentifier NOT NULL,
					Name nvarchar(30) NOT NULL,
					[BranchOfficeId] uniqueidentifier NOT NULL,
					CONSTRAINT PK_Departments PRIMARY KEY CLUSTERED 
					(
						Id ASC
					),
					CONSTRAINT FK_Departments_Branches_BranchOfficeId FOREIGN KEY(BranchOfficeId)
						REFERENCES Branches (Id) ON DELETE CASCADE
					);

				CREATE TABLE Positions (
					Id uniqueidentifier NOT NULL,
					Name nvarchar(30) NOT NULL,
					DepartmentId uniqueidentifier NOT NULL,
					CONSTRAINT PK_Positions PRIMARY KEY CLUSTERED 
					(
						Id ASC
					),
					CONSTRAINT FK_Positions_Departments_DepartmentId FOREIGN KEY(DepartmentId)
						REFERENCES Departments (Id) ON DELETE CASCADE
					);

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

				CREATE TABLE RefreshTokens (
					Id uniqueidentifier NOT NULL,
					Token nvarchar NOT NULL,
					UserId uniqueidentifier NOT NULL,
					CreatedDateTime datetime NOT NULL,
					CONSTRAINT PK_RefreshToken PRIMARY KEY CLUSTERED 
					(
						Id ASC
					),
					CONSTRAINT UQ_RefreshToken_UserId UNIQUE (UserId),
					CONSTRAINT FK_RefreshTokens_Users_UserId FOREIGN KEY(UserId)
						REFERENCES Users (Id)
					);