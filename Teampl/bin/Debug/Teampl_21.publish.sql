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
[dbo].[postData].[commandCount] 열이 삭제되므로 데이터 손실이 발생할 수 있습니다.
*/

IF EXISTS (select top 1 1 from [dbo].[postData])
    RAISERROR (N'행이 발견되었습니다. 데이터가 손실될 수 있으므로 스키마 업데이트가 종료됩니다.', 16, 127) WITH NOWAIT

GO
PRINT N'[dbo].[postData]에 대한 명명되지 않은 제약 조건 삭제 중...';


GO
ALTER TABLE [dbo].[postData] DROP CONSTRAINT [DF__postData__postSt__276EDEB3];


GO
PRINT N'[dbo].[postData]에 대한 명명되지 않은 제약 조건 삭제 중...';


GO
ALTER TABLE [dbo].[postData] DROP CONSTRAINT [DF__postData__comman__29572725];


GO
PRINT N'[dbo].[postData] 변경 중...';


GO
ALTER TABLE [dbo].[postData] DROP COLUMN [commandCount];


GO
ALTER TABLE [dbo].[postData] ALTER COLUMN [postStar] INT NOT NULL;


GO
ALTER TABLE [dbo].[postData]
    ADD [commentCount] INT DEFAULT 0 NOT NULL;


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
PRINT N'[dbo].[postData]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[postData]
    ADD DEFAULT 0 FOR [postStar];


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
PRINT N'업데이트가 완료되었습니다.';


GO
