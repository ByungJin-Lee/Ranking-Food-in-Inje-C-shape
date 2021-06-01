CREATE TABLE dbo.postData
(
	Id INT Identity(1, 1) NOT NULL PRIMARY KEY,
	postKind TinyInt NOT NULL,
	postFood NVARCHAR(30) NOT NULL,
	postStar INT Default 0 NOT NULL,
	postTime SmallDateTime Default GetDate(),	
	postDes NVARCHAR(Max) NULL,
	imageURL NVARCHAR(MAX) NULL,
	commentCount int DEFAULT 0 NOT NULL
)
