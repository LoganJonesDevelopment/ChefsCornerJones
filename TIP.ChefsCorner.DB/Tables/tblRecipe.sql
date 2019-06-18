CREATE TABLE [dbo].[tblRecipe]
(
	[rc_Id] INT NOT NULL PRIMARY KEY, 
    [rc_ImagePath] VARCHAR(100) NOT NULL, 
    [rc_Name] VARCHAR(64) NOT NULL, 
    [rc_Description] VARCHAR(MAX) NOT NULL, 
    [rc_Directions] VARCHAR(MAX) NOT NULL, 
    [us_Id] INT NOT NULL,
	[rc_Ingredients] VARCHAR(MAX) NOT NULL
)
