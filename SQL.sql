/*

create an empty DB called GuardianshipDB before running this script.

*/

USE [GuardianshipDB]
GO

CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


/****** Object:  Table [dbo].[DefaultSettings]    Script Date: 10/18/2016 20:51:47 ******/
DROP TABLE [dbo].[DefaultSettings]
GO
DROP TABLE [dbo].[DefaultCategory]
GO
DROP TABLE [dbo].[ReportDetail]
GO
/****** Object:  Table [dbo].[Report]    Script Date: 10/18/2016 20:51:47 ******/
DROP TABLE [dbo].[Report]
GO
/****** Object:  Table [dbo].[ReportDetail]    Script Date: 10/18/2016 20:51:47 ******/
/****** Object:  Table [dbo].[UserCategory]    Script Date: 10/18/2016 20:51:47 ******/
DROP TABLE [dbo].[UserCategory]
GO
/****** Object:  Table [dbo].[UserSettings]    Script Date: 10/18/2016 20:51:47 ******/
DROP TABLE [dbo].[UserSettings]
GO
/****** Object:  Table [dbo].[Ward]    Script Date: 10/18/2016 20:51:47 ******/
DROP TABLE [dbo].[Ward]
GO



CREATE TABLE [dbo].[DefaultCategory] (
	[StateCode]		VARCHAR (2)	 NOT NULL,
    [CategoryType]  TINYINT      NOT NULL,
    [CategoryClass] TINYINT      NOT NULL,
    [CategoryName]  VARCHAR (50) NOT NULL,
    [Ordinal]       TINYINT      NOT NULL,
    CONSTRAINT [PK_DefaultCategory_1] PRIMARY KEY CLUSTERED ([CategoryType] ASC, [CategoryName] ASC)
);
GO
CREATE TABLE [dbo].[DefaultSettings] (
    [SystemGroup]     VARCHAR (50)  NOT NULL,
    [SystemParameter] VARCHAR (50)  NOT NULL,
    [SystemValue]     VARCHAR (MAX) NOT NULL,
    [SystemFriendlyName] VARCHAR (50)  NULL,
    [SystemDescription]  VARCHAR (255) NULL,
    CONSTRAINT [PK_DefaultSettings] PRIMARY KEY CLUSTERED ([SystemGroup] ASC, [SystemParameter] ASC)
);
GO
CREATE TABLE [dbo].[Ward] (
    [WardID]      INT            IDENTITY (1, 1) NOT NULL,
    [UserID]      NVARCHAR (128) NOT NULL,
    [FirstName]   VARCHAR (50)   NOT NULL,
    [MiddleName]  VARCHAR (10)   NOT NULL,
    [LastName]    VARCHAR (50)   NOT NULL,
    [Suffix]      VARCHAR (10)   NOT NULL,
    [Gender]      NVARCHAR (1)   NULL,
    [PeriodStartMonth] [tinyint] NOT NULL,
	[PeriodDuration] [tinyint] NOT NULL,
    [CreateDate]  DATETIME       NOT NULL,
    [LastUpdated] DATETIME       NULL,
    [DeletedDate] DATETIME       NULL,
    CONSTRAINT [PK_Ward] PRIMARY KEY CLUSTERED ([WardID] ASC),
    CONSTRAINT [FK_Ward_AspNetUsers] FOREIGN KEY ([UserID]) REFERENCES [dbo].[AspNetUsers] ([Id])
);
GO
    
CREATE TABLE [dbo].[UserSettings] (
    [UserID]  NVARCHAR (128) NOT NULL,
    [Group]   VARCHAR (50)   NOT NULL,
    [Setting] VARCHAR (50)   NOT NULL,
    [Value]   VARCHAR (MAX)  NOT NULL,
    [FriendlyName] VARCHAR (50)   NULL,
    [Description]  VARCHAR (255)  NULL,
    CONSTRAINT [PK_UserSettings] PRIMARY KEY CLUSTERED ([UserID] ASC, [Group] ASC, [Setting] ASC),
    CONSTRAINT [FK_UserSettings_AspNetUsers] FOREIGN KEY ([UserID]) REFERENCES [dbo].[AspNetUsers] ([Id])
);
Go

CREATE TABLE [dbo].[UserCategory] (
    [CategoryID]    INT            IDENTITY (1, 1) NOT NULL,
    [UserID]        NVARCHAR (128) NOT NULL,
    [StateCode]		VARCHAR (2)	   NOT NULL,
    [CategoryType]  TINYINT        NOT NULL,
    [CategoryClass] TINYINT        NOT NULL,
    [CategoryName]  VARCHAR (50)   NOT NULL,
    [Ordinal]       TINYINT        NOT NULL,
    [Hide]          BIT            NOT NULL,
    CONSTRAINT [PK_UserCategory] PRIMARY KEY CLUSTERED ([CategoryID] ASC),
    CONSTRAINT [FK_UserCategory_AspNetUsers] FOREIGN KEY ([UserID]) REFERENCES [dbo].[AspNetUsers] ([Id])
);
go

CREATE TABLE [dbo].[Report] (
    [ReportID]         INT            IDENTITY (1, 1) NOT NULL,
    [WardID]           INT            NOT NULL,
    [UserID]           NVARCHAR (128) NOT NULL,
    [StateCode]		   VARCHAR (2)	  NOT NULL,
    [PeriodStartMonth] TINYINT        NOT NULL,
    [PeriodStartYear]  SMALLINT       NOT NULL,
    [PeriodDuration]   TINYINT        NOT NULL,
    [CreateDate]       DATETIME       NOT NULL,
    [LastUpdated]      DATETIME       NOT NULL,
    [SubmittedDate]    DATETIME       NULL,
    [DeletedDate]      DATETIME       NULL,
    CONSTRAINT [PK_Report] PRIMARY KEY CLUSTERED ([ReportID] ASC),
    CONSTRAINT [FK_Report_Ward] FOREIGN KEY ([WardID]) REFERENCES [dbo].[Ward] ([WardID]),
    CONSTRAINT [FK_Report_AspNetUsers] FOREIGN KEY ([UserID]) REFERENCES [dbo].[AspNetUsers] ([Id])
);
GO
CREATE TABLE [dbo].[ReportDetail] (
    [ReportDetailID] INT            IDENTITY (1, 1) NOT NULL,
    [ReportID]       INT            NOT NULL,
    [UserID]         NVARCHAR (128) NOT NULL,
    [WardID]         INT            NOT NULL,
    [CategoryID]     INT            NOT NULL,
    [Worksheet]      TINYINT        NOT NULL,
    [Description]    VARCHAR (255)  NULL,
    [Month]          TINYINT        NOT NULL,
    [Year]           SMALLINT		NOT NULL,
    [Value]          MONEY          NOT NULL,
    [VOrdinal]       TINYINT        NOT NULL,
    [HOrdinal]       TINYINT        NOT NULL,
    [LastUpdated]    DATETIME       NOT NULL,
    [DeletedDate]    DATETIME       NULL,
    CONSTRAINT [PK_ReportDetail] PRIMARY KEY CLUSTERED ([ReportDetailID] ASC),
    CONSTRAINT [FK_ReportDetail_Report] FOREIGN KEY ([ReportID]) REFERENCES [dbo].[Report] ([ReportID]),
    CONSTRAINT [FK_ReportDetail_UserCategory] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[UserCategory] ([CategoryID]),
    CONSTRAINT [FK_ReportDetail_Ward] FOREIGN KEY ([WardID]) REFERENCES [dbo].[Ward] ([WardID]),
    CONSTRAINT [FK_ReportDetail_AspNetUsers] FOREIGN KEY ([UserID]) REFERENCES [dbo].[AspNetUsers] ([Id])
);
GO


/* required for Identity 2.0 */
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AspNetRoles] (
    [Id]   NVARCHAR (128) NOT NULL,
    [Name] NVARCHAR (256) NOT NULL
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [dbo].[AspNetRoles]([Name] ASC);


GO
ALTER TABLE [dbo].[AspNetRoles]
    ADD CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC);


CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [UserId]     NVARCHAR (128) NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserClaims]([UserId] ASC);


GO
ALTER TABLE [dbo].[AspNetUserClaims]
    ADD CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC);


GO
ALTER TABLE [dbo].[AspNetUserClaims]
    ADD CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE;

CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] NVARCHAR (128) NOT NULL,
    [ProviderKey]   NVARCHAR (128) NOT NULL,
    [UserId]        NVARCHAR (128) NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserLogins]([UserId] ASC);


GO
ALTER TABLE [dbo].[AspNetUserLogins]
    ADD CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC, [UserId] ASC);


GO
ALTER TABLE [dbo].[AspNetUserLogins]
    ADD CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE;

CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] NVARCHAR (128) NOT NULL,
    [RoleId] NVARCHAR (128) NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserRoles]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_RoleId]
    ON [dbo].[AspNetUserRoles]([RoleId] ASC);


GO
ALTER TABLE [dbo].[AspNetUserRoles]
    ADD CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC);


GO
ALTER TABLE [dbo].[AspNetUserRoles]
    ADD CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE;


GO
ALTER TABLE [dbo].[AspNetUserRoles]
    ADD CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE;
/* end Identity 2.0 requirements */





/* 
CategoryType 0 = income; 1=expense 
CategoryClass 0 = single entry; 1=heading only/multiple entry
*/

/* populate DefaultSettings */
insert into DefaultCategory select 'WA', 0, 0, 'Social Security', 5
insert into DefaultCategory select 'WA', 0, 0, 'SSI', 10
insert into DefaultCategory select 'WA', 0, 0, 'VA/Railroad/CSA Pension', 15
insert into DefaultCategory select 'WA', 0, 0, 'Retirement Pension', 20
insert into DefaultCategory select 'WA', 0, 0, 'Wages', 25
insert into DefaultCategory select 'WA', 0, 0, 'Interest and Dividends', 30
insert into DefaultCategory select 'WA', 0, 1, 'Other', 35

insert into DefaultCategory select 'WA', 1, 1, 'Room and Board', 5
insert into DefaultCategory select 'WA', 1, 1, 'Personal Funds', 10
insert into DefaultCategory select 'WA', 1, 1, 'Other: Entertainment and Travel', 15
insert into DefaultCategory select 'WA', 1, 1, 'Other: Transportation', 20
insert into DefaultCategory select 'WA', 1, 1, 'Medical and Dental', 25
insert into DefaultCategory select 'WA', 1, 1, 'Guardian Allowence', 30
insert into DefaultCategory select 'WA', 1, 1, 'Other: Attorney Fees', 35
insert into DefaultCategory select 'WA', 1, 1, 'Other:', 40

insert into DefaultSettings select 'REPORT', 'PeriodStartMonth', '1', 'Period Start Month','Starting month of the reporting period (1=Jan, 2=Feb, etc.)'
insert into DefaultSettings select 'REPORT', 'PeriodStartYear', '[CURRENT_YEAR]','Period Year','Starting year of the reporting period'
insert into DefaultSettings select 'REPORT', 'PeriodDuration', '36', 'Report Duration (mos.)','Number of months the report covers'

go



