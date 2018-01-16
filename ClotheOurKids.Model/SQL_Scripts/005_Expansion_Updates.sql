ALTER TABLE Reference.Office
ADD CountyId smallInt;

UPDATE Reference.Office
SET CountyId = 1 --Morgan County
WHERE EXISTS (	SELECT 1 
				FROM Reference.School s
					INNER JOIN Reference.SchoolDistrict sd ON s.SchoolDistrictId = sd.SchoolDistrictId
					INNER JOIN Reference.County c ON sd.CountyId = c.CountyId
				WHERE s.OfficeId = office.OfficeId
					and c.Name = 'Morgan County'
			);

UPDATE Reference.Office
SET CountyId = 2 --Lawrence County
WHERE EXISTS (	SELECT 1 
				FROM Reference.School s
					INNER JOIN Reference.SchoolDistrict sd ON s.SchoolDistrictId = sd.SchoolDistrictId
					INNER JOIN Reference.County c ON sd.CountyId = c.CountyId
				WHERE s.OfficeId = office.OfficeId
					and c.Name = 'Lawrence County'
			);

UPDATE Reference.Office
SET CountyId = 1 --Morgan County
WHERE EXISTS ( SELECT 1
				FROM Reference.Office o
					LEFT OUTER JOIN Reference.School s ON o.OfficeId = s.OfficeId
				WHERE s.OfficeId IS NULL
					AND o.OfficeId = office.OfficeId
			)
	AND City IN ('Decatur', 'Hartselle');

UPDATE Reference.Office
SET CountyId = 2 --Lawrence County
WHERE EXISTS ( SELECT 1
				FROM Reference.Office o
					LEFT OUTER JOIN Reference.School s ON o.OfficeId = s.OfficeId
				WHERE s.OfficeId IS NULL
					AND o.OfficeId = office.OfficeId
			)
	AND City IN ('Moulton');


ALTER TABLE Reference.Office
ALTER COLUMN CountyId smallint NOT NULL;

ALTER TABLE Reference.Office
	ADD CONSTRAINT FK_Reference_County$DefinesCountyOfResidenceFor$Reference_Office
	FOREIGN KEY (CountyId) REFERENCES Reference.County (CountyId)
ON UPDATE NO ACTION
ON DELETE NO ACTION;
GO


CREATE TABLE Reference.Address
(
	AddressId				int NOT NULL IDENTITY(1,1) CONSTRAINT PK_Reference_Address_AddressId PRIMARY KEY,
	StreetAddress			varchar(250) NULL,
	City					varchar(50) NULL,
	StateId					smallint NULL,
	PostalCode				char(6) NULL,
	PostalCodeExt			char(4) NULL
)

ALTER TABLE Reference.Address
ADD CONSTRAINT AK_Reference_Address_StreetAddress_City_StateId UNIQUE (StreetAddress, City, StateId);


CREATE TABLE Reference.Chapter
(
	ChapterId				int NOT NULL IDENTITY(1,1) CONSTRAINT PK_Reference_Chapter_CapterId PRIMARY KEY,
	Name					varchar(100) NOT NULL,
	MailingAddressId		int NOT NULL,
	PhysicalAddressId		int NOT NULL,
	FoundedDate				date NOT NULL,
	FoundingStateId			smallint NOT NULL,
	Phone					varchar(50) NULL,
	Email					nvarchar(256) NULL
);


