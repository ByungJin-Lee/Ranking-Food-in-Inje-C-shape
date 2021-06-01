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
[dbo].[postData].[imageName] 열이 삭제되므로 데이터 손실이 발생할 수 있습니다.
*/

IF EXISTS (select top 1 1 from [dbo].[postData])
    RAISERROR (N'행이 발견되었습니다. 데이터가 손실될 수 있으므로 스키마 업데이트가 종료됩니다.', 16, 127) WITH NOWAIT

GO
PRINT N'[dbo].[postData]에 대한 명명되지 않은 제약 조건 삭제 중...';


GO
ALTER TABLE [dbo].[postData] DROP CONSTRAINT [DF__tmp_ms_xx__postT__2E1BDC42];


GO
PRINT N'[dbo].[postData] 테이블 다시 빌드 시작...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_postData] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [postKind]     TINYINT        NOT NULL,
    [postFood]     NVARCHAR (30)  NOT NULL,
    [postStar]     TINYINT        NOT NULL,
    [postTime]     SMALLDATETIME  DEFAULT GetDate() NULL,
    [postDes]      NVARCHAR (MAX) NULL,
    [imageURL]     NVARCHAR (MAX) NULL,
    [commandCount] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[postData])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_postData] ON;
        INSERT INTO [dbo].[tmp_ms_xx_postData] ([Id], [postKind], [postFood], [postStar], [postTime], [postDes], [commandCount])
        SELECT   [Id],
                 [postKind],
                 [postFood],
                 [postStar],
                 [postTime],
                 [postDes],
                 [commandCount]
        FROM     [dbo].[postData]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_postData] OFF;
    END

DROP TABLE [dbo].[postData];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_postData]', N'postData';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'업데이트가 완료되었습니다.';


GO
