USE [master]
GO

CREATE DATABASE [Student]
GO

USE [Student]
GO

/****** Object:  Table [dbo].[Student]    Script Date: 2022/07/28 20:45:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Student](
	[StudentId] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[FirstName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[Grade] [nvarchar](100) NULL,
	[IsActive] [bit] NULL
) ON [PRIMARY]
GO
