using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTrackingService.Migrations
{
    public partial class DaysAccounting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
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

				CREATE TABLE DaysAccountingDocument(
					DayAccountingId uniqueidentifier NOT NULL,
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
				);");
        }
    }
}
