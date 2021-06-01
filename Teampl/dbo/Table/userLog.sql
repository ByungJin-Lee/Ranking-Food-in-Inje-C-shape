CREATE TABLE dbo.userLog
(
	Id INT Identity(1, 1) NOT NULL PRIMARY KEY,
	userId NVARCHAR(15) NULL,
	connection Bit NOT NULL,
	time NVARCHAR(30) NULL,
	postKind NVARCHAR(15) Not NULL,
	postItem NVARCHAR(15) NOT NULL,
	accessIP NVARCHAR(20) NULL
)
