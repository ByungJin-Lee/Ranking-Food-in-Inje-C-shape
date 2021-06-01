CREATE TABLE dbo.userData
(
	Id INT Identity(1, 1) NOT NULL PRIMARY KEY,
	userId NVARCHAR(20) NOT NULL,	
	userPw NVARCHAR(20) NOT NULL,
	Email NVARCHAR(30) NOT NULL,
	joinTime SmallDateTime Default GetDate(),	
	code NVARCHAR(5) NOT NULL,
	certification bit DEFAULT 0 NOT NULL,
	gender Bit NOT NULL,
	userName NVARCHAR(15) NOT NULL,
	class bit Default 0 NOT NULL
)

