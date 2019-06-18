CREATE TABLE [dbo].[tblRecipeIngredients]
(
	[ri_Id] INT NOT NULL PRIMARY KEY, 
    [ri_Quantity] INT NOT NULL, 
    [ms_Id] INT NOT NULL, 
    [ri_Notes] VARCHAR(250) NOT NULL, 
    [in_Id] INT NOT NULL, 
    [rc_Id] INT NOT NULL 
)
