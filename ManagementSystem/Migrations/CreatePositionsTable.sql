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
