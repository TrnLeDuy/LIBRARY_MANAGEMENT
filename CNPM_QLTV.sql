USE MASTER
GO
IF EXISTS(SELECT * FROM sysdatabases WHERE NAME='CNPM_QLTV') 
DROP DATABASE CNPM_QLTV 
CREATE DATABASE CNPM_QLTV
GO
use CNPM_QLTV
GO

------------------------------------------------------
--------------Tạo bảng cho Cơ sở dữ liệu--------------
------------------------------------------------------
CREATE TABLE BOOK(
	[BookID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[BookName] [nvarchar](Max) NOT NULL,
	[Author] [nvarchar](35) NULL,
	[Describe] [nvarchar](Max) NULL,
	[Publish] [date] NULL,
	[Publisher] [nvarchar](30) NULL,
	[BookImage] [varchar](50) NULL,
	[BookStatus] [varchar](1) NULL, ---Yes,No,Lost,Broken
	PRIMARY KEY (BookID, CategoryID)
);
GO

CREATE TABLE BOOKRETURN(
	[ReturnID] [int] IDENTITY(1,1) NOT NULL,
	[BorrowID] [int] NOT NULL,
	[ReturnDate] [datetime] NULL,
	PRIMARY KEY (ReturnID)
);
GO

CREATE TABLE BORROW(
	[BorrowID] [int] IDENTITY(1,1) NOT NULL,
	[CardID] [varchar](7) NOT NULL,
	[BookID] [int] NULL,
	[TakenDate] [datetime] NULL,
	[DueDate] [datetime] NULL,
	PRIMARY KEY (BorrowID)
);
GO

CREATE TABLE CATEGORY(
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](30) NOT NULL,
	Note nvarchar(50),
	[Status] [varchar](1) NULL,
	PRIMARY KEY (CategoryID)
);
GO

CREATE TABLE FINE(
	[FineID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [varchar](5),
	[ReturnID] [int] NULL,
	CreateDate datetime,
	[FineAmount] [varchar](1) NULL,
	PRIMARY KEY (FineID)
);
GO

CREATE TABLE LIBRARYCARD(
	[CardID] [varchar](7) NOT NULL,
	StudentID varchar(10) NOT NULL,
	[DateExpires] [datetime] NULL,
	[Status] [nvarchar](25) NULL,
	PRIMARY KEY (CardID)
);
GO

CREATE TABLE STUDENT(
	StudentID varchar(10) NOT NULL,
	[StudentName] [nvarchar](25) NOT NULL,
	[Birth] [date] NULL,
	[Email] [nvarchar](30) NULL,
	[Phone] [varchar](10) NULL,
	[Addr] [nvarchar](100) NULL,
	[Gender] [char](10) NOT NULL,
	PRIMARY KEY (StudentID)
);
GO

CREATE TABLE USERS(
	[UserID] [varchar](5),
	[Fullname] [nvarchar](50) NOT NULL,
	[Username] [nvarchar](15) NULL,
	[Password] [nvarchar](30) NULL,
	[Roles] [char](2) NULL,
	[Birth] [date] NULL,
	[Gender] [char](10) NULL,
	[Email] [varchar](50) NULL,
	[Phone] [varchar](10) NULL,
	[Address] [nvarchar](100) NULL,
	PRIMARY KEY (UserID),
);
GO


-------------------------------------------------------
--Tạo khóa ngoại và ràng buộc cho các thuộc tính trên--
-------------------------------------------------------
ALTER TABLE [dbo].[BOOK]  WITH CHECK ADD  CONSTRAINT [FK_BOOK_BORROW] FOREIGN KEY([BookID])
REFERENCES [dbo].[BORROW] ([BorrowID])
GO
ALTER TABLE [dbo].[BOOK] NOCHECK CONSTRAINT [FK_BOOK_BORROW]
GO
ALTER TABLE [dbo].[BOOK]  WITH CHECK ADD  CONSTRAINT [FK_BOOK_CATEGORY] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[CATEGORY] ([CategoryID])
GO
ALTER TABLE [dbo].[BOOK] CHECK CONSTRAINT [FK_BOOK_CATEGORY]
GO
ALTER TABLE [dbo].[BOOKRETURN]  WITH CHECK ADD  CONSTRAINT [FK_BOOKRETURN_BORROW] FOREIGN KEY([BorrowID])
REFERENCES [dbo].[BORROW] ([BorrowID])
GO
ALTER TABLE [dbo].[BOOKRETURN] CHECK CONSTRAINT [FK_BOOKRETURN_BORROW]
GO
ALTER TABLE [dbo].[BORROW]  WITH CHECK ADD  CONSTRAINT [FK_BORROW_LIBRARYCARD] FOREIGN KEY([CardID])
REFERENCES [dbo].[LIBRARYCARD] ([CardID])
GO
ALTER TABLE [dbo].[BORROW] CHECK CONSTRAINT [FK_BORROW_LIBRARYCARD]
GO
ALTER TABLE [dbo].[FINE]  WITH CHECK ADD  CONSTRAINT [FK_FINE_BOOKRETURN] FOREIGN KEY([ReturnID])
REFERENCES [dbo].[BOOKRETURN] ([ReturnID])
GO
ALTER TABLE [dbo].[FINE] CHECK CONSTRAINT [FK_FINE_BOOKRETURN]
GO
ALTER TABLE [dbo].[FINE]  WITH CHECK ADD  CONSTRAINT [FK_FINE_USERS] FOREIGN KEY([UserID])
REFERENCES [dbo].[USERS] ([UserID])
GO
ALTER TABLE [dbo].[FINE] CHECK CONSTRAINT [FK_FINE_USERS]
GO
ALTER TABLE [dbo].[LIBRARYCARD]  WITH CHECK ADD  CONSTRAINT [FK_LIBRARYCARD_STUDENT] FOREIGN KEY([StudentID])
REFERENCES [dbo].[STUDENT] ([StudentID])
GO
ALTER TABLE [dbo].[LIBRARYCARD] CHECK CONSTRAINT [FK_LIBRARYCARD_STUDENT]
GO
ALTER TABLE [dbo].[LIBRARYCARD]  WITH CHECK ADD  CONSTRAINT [chk_CardID] CHECK  (([CardID] like 'TV[0-9][0-9][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[LIBRARYCARD] CHECK CONSTRAINT [chk_CardID]
GO
ALTER TABLE [dbo].[STUDENT]  WITH CHECK ADD  CONSTRAINT [chk_StudentID] CHECK  (([StudentID] like '[0-9][0-9]DH[0-9][0-9][0-9][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[STUDENT] CHECK CONSTRAINT [chk_StudentID]
GO
ALTER TABLE [dbo].[USERS]  WITH CHECK ADD  CONSTRAINT [chk_UserID] CHECK  (([UserID] like '[0-9][0-9][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[USERS] CHECK CONSTRAINT [chk_UserID]
GO