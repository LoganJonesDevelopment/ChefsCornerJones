/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
DROP TABLE IF EXISTS dbo.tblIngredients
DROP TABLE IF EXISTS dbo.tblMeasures
DROP TABLE IF EXISTS dbo.tblRecipe
DROP TABLE IF EXISTS dbo.tblRecipeIngredients
DROP TABLE IF EXISTS dbo.tblUser
