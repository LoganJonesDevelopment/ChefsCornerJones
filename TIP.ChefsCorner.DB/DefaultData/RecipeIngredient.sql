BEGIN
	INSERT INTO tblRecipeIngredients (ri_Id, rc_Id, ri_Notes, ri_Quantity, ms_Id, in_Id)
	VALUES
	(1, 1, '', 1, 2, 1),
	(2, 1, '', 2, 2, 3),
	(3, 1, '', 4, 1, 4),
	(4, 1, '', 2, 3, 5),
	(5, 1, '', .75, 2, 2),
	(6, 1, '', 1, 2, 7),
	(7, 1, '', .5, 3, 8),
	(8, 1, '', .25, 3, 6),
	(9, 1, '', 1, 2, 9)
END