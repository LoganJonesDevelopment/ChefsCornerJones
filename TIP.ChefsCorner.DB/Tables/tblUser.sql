CREATE TABLE [dbo].[tblUser]
(
	[us_Id] INT NOT NULL PRIMARY KEY, 
    [us_ScreenName] VARCHAR(64) NOT NULL, 
    [us_Email] VARCHAR(64) NOT NULL, 
    [us_Pass] VARCHAR(40) NOT NULL
)
