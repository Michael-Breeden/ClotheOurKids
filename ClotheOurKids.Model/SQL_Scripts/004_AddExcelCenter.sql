INSERT INTO Reference.Office (Name, OfficeTypeId, StreetAddress, City, StateId, PostalCode, Phone)
VALUES ('DCS EXCEL Center', 1, '1625 Danville Rd SW', 'Decatur', 1, '35601', '2565523060');
GO

INSERT INTO Reference.School (OfficeId, SchoolDistrictId, SchoolTypeId)
SELECT o.OfficeId, 1, 4
FROM Reference.Office o
WHERE o.Name = 'DCS EXCEL Center';
GO