DROP DATABASE Wizard;
CREATE DATABASE Wizard;
USE Wizard;


CREATE TABLE [dbo].[Users] (
    [Id]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [NAME]     NVARCHAR (50) NOT NULL,
    [Email]    NVARCHAR (50) NOT NULL,
    [Password] NVARCHAR (50) NOT NULL,
    [Type]     NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[Wizards] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [Titel]       NVARCHAR (50)  NULL,
    [Description] NVARCHAR (MAX) NULL,
    [UserId]      BIGINT         NOT NULL,
    [Hashnum]     BIGINT         NOT NULL,
    CONSTRAINT [PK_Wizards] PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);


CREATE TABLE [dbo].[Pages] (
    [Id]         BIGINT        IDENTITY (1, 1) NOT NULL,
    [WizardType] NVARCHAR (50) NULL,
    [NumPages]   INT           NOT NULL,
    [WizardID]   BIGINT        NOT NULL,
    CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([WizardID]) REFERENCES [dbo].[Wizards] ([Id])
);


CREATE TABLE [dbo].[WizardData] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [WizardIndex] NVARCHAR (MAX) NULL,
    [PageID]      BIGINT         NOT NULL,
    CONSTRAINT [PK_WizardData] PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([PageID]) REFERENCES [dbo].[Pages] ([Id])
);


CREATE TABLE [dbo].[Answers] (
    [Id]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [answer]       TEXT          NULL,
    [UserName]     NVARCHAR (50) NOT NULL,
    [UserEmail]    NVARCHAR (50) NOT NULL,
    [WizaedDataID] BIGINT        NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([WizaedDataID]) REFERENCES [dbo].[WizardData] ([Id])
);

INSERT INTO [dbo].[Users] values('Admin','Admin@gmail.com','admin','ADMIN');
