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