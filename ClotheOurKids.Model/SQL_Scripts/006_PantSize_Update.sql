UPDATE Reference.PantSize
SET Name = '30'
WHERE GenderId = 'Male' AND Name = '30-31';

UPDATE Reference.PantSize
SET Name = '32'
WHERE GenderId = 'Male' AND Name = '32-33';

UPDATE Reference.PantSize
SET Name = '34'
WHERE GenderId = 'Male' AND Name = '34-35';

UPDATE Reference.PantSize
SET Name = '36'
WHERE GenderId = 'Male' AND Name = '36-37';

UPDATE Reference.PantSize
SET Name = '38'
WHERE GenderId = 'Male' AND Name = '38-39';

UPDATE Reference.PantSize
SET Name = '40'
WHERE GenderId = 'Male' AND Name = '40-41';

UPDATE Reference.PantSize
SET Name = '42'
WHERE GenderId = 'Male' AND Name = '42-43';

UPDATE Reference.PantSize
SET Name = '44'
WHERE GenderId = 'Male' AND Name = '44-45';

UPDATE Reference.PantSize
SET Name = '46'
WHERE GenderId = 'Male' AND Name = '46-47';

UPDATE Reference.PantSize
SET Name = '48'
WHERE GenderId = 'Male' AND Name = '48-49';

INSERT INTO Reference.PantSize (Name, AgeGroupId, GenderId, SortOrder)
VALUES	('31', 4, 'Male', 4055), 
		('33', 4, 'Male', 4065),
		('35', 4, 'Male', 4075),
		('37', 4, 'Male', 4085),
		('39', 4, 'Male', 4095),
		('41', 4, 'Male', 4105),
		('43', 4, 'Male', 4115),
		('45', 4, 'Male', 4125),
		('47', 4, 'Male', 4135),
		('49', 4, 'Male', 4145);


