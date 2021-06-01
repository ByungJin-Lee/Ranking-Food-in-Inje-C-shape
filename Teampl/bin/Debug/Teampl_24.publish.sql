/*
TeamplDB의 배포 스크립트

이 코드는 도구를 사용하여 생성되었습니다.
파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
변경 내용이 손실됩니다.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "TeamplDB"
:setvar DefaultFilePrefix "TeamplDB"
:setvar DefaultDataPath "C:\Users\dlqud\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"
:setvar DefaultLogPath "C:\Users\dlqud\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"

GO
:on error exit
GO
/*
SQLCMD 모드가 지원되지 않으면 SQLCMD 모드를 검색하고 스크립트를 실행하지 않습니다.
SQLCMD 모드를 설정한 후에 이 스크립트를 다시 사용하려면 다음을 실행합니다.
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'이 스크립트를 실행하려면 SQLCMD 모드를 사용하도록 설정해야 합니다.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'[dbo].[foodData]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[foodData] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [parentId] INT            NOT NULL,
    [writer]   NVARCHAR (20)  NOT NULL,
    [foodStar] TINYINT        NOT NULL,
    [postData] SMALLDATETIME  NULL,
    [comment]  NVARCHAR (MAX) NULL,
    [gender]   BIT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'[dbo].[postData]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[postData] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [postKind]     TINYINT        NOT NULL,
    [postFood]     NVARCHAR (30)  NOT NULL,
    [postStar]     INT            NOT NULL,
    [postTime]     SMALLDATETIME  NULL,
    [postDes]      NVARCHAR (MAX) NULL,
    [imageURL]     NVARCHAR (MAX) NULL,
    [commentCount] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'[dbo].[userData]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[userData] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [userId]        NVARCHAR (20) NOT NULL,
    [userPw]        NVARCHAR (20) NOT NULL,
    [Email]         NVARCHAR (30) NOT NULL,
    [joinTime]      SMALLDATETIME NULL,
    [code]          NVARCHAR (5)  NOT NULL,
    [certification] BIT           NOT NULL,
    [gender]        BIT           NOT NULL,
    [userName]      NVARCHAR (15) NOT NULL,
    [class]         BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'[dbo].[userLog]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[userLog] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [userId]     NVARCHAR (15) NULL,
    [connection] BIT           NOT NULL,
    [time]       SMALLDATETIME NULL,
    [postKind]   NVARCHAR (15) NOT NULL,
    [postItem]   NVARCHAR (15) NOT NULL,
    [accessIP]   NVARCHAR (20) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'[dbo].[foodData]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[foodData]
    ADD DEFAULT 0 FOR [foodStar];


GO
PRINT N'[dbo].[foodData]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[foodData]
    ADD DEFAULT GetDate() FOR [postData];


GO
PRINT N'[dbo].[postData]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[postData]
    ADD DEFAULT 0 FOR [postStar];


GO
PRINT N'[dbo].[postData]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[postData]
    ADD DEFAULT GetDate() FOR [postTime];


GO
PRINT N'[dbo].[postData]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[postData]
    ADD DEFAULT 0 FOR [commentCount];


GO
PRINT N'[dbo].[userData]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[userData]
    ADD DEFAULT GetDate() FOR [joinTime];


GO
PRINT N'[dbo].[userData]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[userData]
    ADD DEFAULT 0 FOR [certification];


GO
PRINT N'[dbo].[userData]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[userData]
    ADD DEFAULT 0 FOR [class];


GO
PRINT N'[dbo].[userLog]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[userLog]
    ADD DEFAULT GetDate() FOR [time];


GO
PRINT N'업데이트가 완료되었습니다.';


GO
