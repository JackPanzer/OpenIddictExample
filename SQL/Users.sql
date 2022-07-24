CREATE TABLE [dbo].[USERS] (
    [USER_ID]         INT            NOT NULL,
    [GUID]            NVARCHAR (MAX) NOT NULL,
    [EMAIL]           NVARCHAR (70)  NULL,
    [NAME]            NVARCHAR (MAX) NOT NULL,
    [LAST_NAME]       NVARCHAR (MAX) NOT NULL,
    [REGISTERED_DATE] DATETIME       NOT NULL,
    [BO_CLIENT_ID]    NVARCHAR (70)  NULL,
    [BO_USER_LOGIN]   NVARCHAR (MAX) NULL,
    [DELETED]         BIT            DEFAULT ((0)) NOT NULL,
    [ENABLED]         BIT            DEFAULT ((0)) NOT NULL,
    [PASSWORD]        VARCHAR (255)  NULL,
    [SALT]            VARCHAR (255)  NULL,
    PRIMARY KEY CLUSTERED ([USER_ID] ASC)
);

