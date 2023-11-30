CREATE TABLE DaysAccountingDocument(
	DaysAccountingId uniqueidentifier NOT NULL,
	DocumentsId uniqueidentifier NOT NULL,
	CONSTRAINT PK_DayAccountingDocument PRIMARY KEY CLUSTERED 
(
	DaysAccountingId ASC,
	DocumentsId ASC
),
	CONSTRAINT FK_DaysAccountingDocument_DaysAccounting_DaysAccountingId FOREIGN KEY(DaysAccountingId)
		REFERENCES DaysAccounting (Id) ON DELETE CASCADE,
	CONSTRAINT FK_DaysAccountingDocument_Document_DocumentsId FOREIGN KEY(DocumentsId)
		REFERENCES Documents (Id) ON DELETE CASCADE
);
