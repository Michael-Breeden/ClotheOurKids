ALTER TABLE Reference.Office
ADD PhysicalAddressId int NULL;
GO

INSERT INTO Reference.Address (StreetAddress, City, StateId, PostalCode, PostalCodeExt)
SELECT DISTINCT o.StreetAddress, o.City, o.StateId, o.PostalCode, o.PostalCodeExt
FROM Reference.Office o;

UPDATE o
SET PhysicalAddressId = AddressId
FROM Reference.Office o
	INNER JOIN Reference.Address a 
		ON o.StreetAddress = a.StreetAddress
			AND o.City = a.City
			AND o.StateId = a.StateId
			AND o.PostalCode = o.PostalCode;

ALTER TABLE Reference.Office
		ADD CONSTRAINT FK_Reference_Address$DefinesPhysicalAddressOf$Reference_Office
		FOREIGN KEY (PhysicalAddressId) REFERENCES Reference.Address (AddressId)
	ON UPDATE NO ACTION
	ON DELETE NO ACTION;

ALTER TABLE Reference.Office
DROP CONSTRAINT FK_Reference_State$DefinesStateOfResidenceFor$Reference_Office;

ALTER TABLE Reference.Office
DROP COLUMN StreetAddress, City, StateId, PostalCode, PostalCodeExt;
GO



UPDATE a
SET CountyId = o.CountyId
FROM Reference.Address a
	INNER JOIN Reference.Office o ON a.AddressId = o.PhysicalAddressId;

GO

ALTER TABLE Reference.Office
DROP CONSTRAINT FK_Reference_County$DefinesCountyOfResidenceFor$Reference_Office;

ALTER TABLE Reference.Office
DROP COLUMN CountyId;
GO