CREATE TABLE dbo.foodData
(
	Id INT Identity(1, 1) NOT NULL PRIMARY KEY,
	parentId INT NOT NULL,
	writer NVARCHAR(20) NOT NULL,
	foodStar TinyInt Default 0 NOT NULL,
	postData SmallDateTime Default GetDate(),	
	comment NVARCHAR(Max) NULL,
	gender bit NULL, 
    [writerId] NVARCHAR(25) NOT NULL
)
