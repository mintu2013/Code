CREATE DATABASE AppDatabase;
PRINT 'Create databse AppDatabase'
GO
USE AppDatabase

CREATE TABLE [dbo].[Company](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](60) NOT NULL,
	[Exchange] [varchar](50) NOT NULL,
	[Ticker] [varchar](10) NOT NULL,
	[ISIN] [varchar](15) NOT NULL,
	[Website] [varchar](50) NULL,
	[Status] [BIT] NULL
 CONSTRAINT [PK_COMPANY] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
))

ALTER TABLE [dbo].[Company] add constraint U_ISIN unique([ISIN]);

EXEC('Create PROCEDURE [dbo].[Company.InsertCompany] 
	@Name varchar(60)='''',
	@Exchange varchar(50)='''',
	@Ticker varchar(10)='''',
	@ISIN varchar(15)='''',
	@Website varchar=''''
	
AS
	INSERT INTO [Company]
	      (Name, Exchange, Ticker, ISIN, Website,Status)
	VALUES     
	      (@Name, @Exchange, @Ticker, @ISIN, @Website,1);
	
	SELECT 
	ID, Name, Exchange, Ticker, ISIN, Website 
	     FROM [Company] 
	     WHERE (ID = SCOPE_IDENTITY())
	RETURN')


EXEC('CREATE PROCEDURE [dbo].[Company.UpdateCompany] 
    @ID int,
	@Name varchar(60)='''''''',
	@Exchange varchar(50)='''''''',
	@Ticker varchar(10)='''''''',
	@ISIN varchar(15)='''''''',
	@Website varchar=''''''''
AS
	UPDATE [dbo].[Company] SET 
	[Name] = @Name, 
	[Exchange] = @Exchange, 
	[Ticker] = @Ticker, 
	[ISIN] = @ISIN,
	[Website]=@Website
	WHERE [ID] = @ID
SELECT ID, Name, Exchange, Ticker,ISIN, Website FROM [dbo].[Company] WHERE ID = @ID
RETURN')


EXEC('CREATE PROCEDURE [dbo].[Company.GetAllCompanies]	
AS
BEGIN
	SELECT [ID]
		,[Name]
		,[Exchange]
		,[Ticker]
		,[ISIN]
		,[Website]
	FROM [Company] 
	WHERE Status <> 0
END')


EXEC('CREATE PROCEDURE [dbo].[Company.GetCompanyByISIN] 
	@ISIN varchar(15)=''''
AS
	
SELECT ID, Name, Exchange, Ticker,ISIN, Website FROM [dbo].[Company] WHERE ISIN = @ISIN
RETURN')


EXEC('CREATE PROCEDURE [dbo].[Company.GetCompanyByID] 
	@ID INT 
AS
	
SELECT ID, Name, Exchange, Ticker,ISIN, Website FROM [dbo].[Company] WHERE ID= @ID
RETURN')
