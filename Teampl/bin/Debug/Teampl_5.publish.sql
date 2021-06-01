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
/*
[dbo].[userData].[userName] 열이 삭제되므로 데이터 손실이 발생할 수 있습니다.
*/

IF EXISTS (select top 1 1 from [dbo].[userData])
    RAISERROR (N'행이 발견되었습니다. 데이터가 손실될 수 있으므로 스키마 업데이트가 종료됩니다.', 16, 127) WITH NOWAIT

GO
/*
[dbo].[userLog].[Ip] 열이 삭제되므로 데이터 손실이 발생할 수 있습니다.

[dbo].[userLog] 테이블의 postItem 열 형식이  NVARCHAR (30) NOT NULL에서  NVARCHAR (15) NOT NULL(으)로 변경됩니다. 데이터가 손실될 수 있습니다.
*/

IF EXISTS (select top 1 1 from [dbo].[userLog])
    RAISERROR (N'행이 발견되었습니다. 데이터가 손실될 수 있으므로 스키마 업데이트가 종료됩니다.', 16, 127) WITH NOWAIT

GO
PRINT N'[dbo].[DF__userData__certif__267ABA7A] 삭제 중...';


GO
ALTER TABLE [dbo].[userData] DROP CONSTRAINT [DF__userData__certif__267ABA7A];


GO
PRINT N'[dbo].[DF__userData__joinTi__25869641] 삭제 중...';


GO
ALTER TABLE [dbo].[userData] DROP CONSTRAINT [DF__userData__joinTi__25869641];


GO
PRINT N'[dbo].[DF__userLog__time__276EDEB3] 삭제 중...';


GO
ALTER TABLE [dbo].[userLog] DROP CONSTRAINT [DF__userLog__time__276EDEB3];


GO
PRINT N'[dbo].[userData] 테이블 다시 빌드 시작...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_userData] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [userId]        NVARCHAR (20) NOT NULL,
    [userPw]        NVARCHAR (20) NOT NULL,
    [Email]         NVARCHAR (30) NOT NULL,
    [joinTime]      SMALLDATETIME DEFAULT GetDate() NULL,
    [code]          NVARCHAR (5)  NOT NULL,
    [certification] BIT           DEFAULT 0 NOT NULL,
    [gender]        BIT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[userData])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_userData] ON;
        INSERT INTO [dbo].[tmp_ms_xx_userData] ([Id], [userId], [userPw], [Email], [joinTime], [code], [certification], [gender])
        SELECT   [Id],
                 [userId],
                 [userPw],
                 [Email],
                 [joinTime],
                 [code],
                 [certification],
                 [gender]
        FROM     [dbo].[userData]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_userData] OFF;
    END

DROP TABLE [dbo].[userData];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_userData]', N'userData';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'[dbo].[userLog] 테이블 다시 빌드 시작...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_userLog] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [userId]     NVARCHAR (15) NULL,
    [connection] BIT           NOT NULL,
    [time]       SMALLDATETIME DEFAULT GetDate() NULL,
    [postKind]   NVARCHAR (15) NOT NULL,
    [postItem]   NVARCHAR (15) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[userLog])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_userLog] ON;
        INSERT INTO [dbo].[tmp_ms_xx_userLog] ([Id], [userId], [connection], [time], [postKind], [postItem])
        SELECT   [Id],
                 [userId],
                 [connection],
                 [time],
                 [postKind],
                 [postItem]
        FROM     [dbo].[userLog]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_userLog] OFF;
    END

DROP TABLE [dbo].[userLog];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_userLog]', N'userLog';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'업데이트가 완료되었습니다.';


GO
