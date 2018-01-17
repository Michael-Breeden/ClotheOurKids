UPDATE Reference.County
SET Name = 'Morgan'
WHERE Name = 'Morgan County';

UPDATE Reference.County
SET Name = 'Lawrence'
WHERE Name = 'Lawrence County';
GO

ALTER TABLE Reference.Office
ADD CountyId smallInt;
GO

UPDATE Reference.Office
SET CountyId = 1 --Morgan County
WHERE EXISTS (	SELECT 1 
				FROM Reference.School s
					INNER JOIN Reference.SchoolDistrict sd ON s.SchoolDistrictId = sd.SchoolDistrictId
					INNER JOIN Reference.County c ON sd.CountyId = c.CountyId
				WHERE s.OfficeId = office.OfficeId
					and c.Name = 'Morgan'
			);

UPDATE Reference.Office
SET CountyId = 2 --Lawrence County
WHERE EXISTS (	SELECT 1 
				FROM Reference.School s
					INNER JOIN Reference.SchoolDistrict sd ON s.SchoolDistrictId = sd.SchoolDistrictId
					INNER JOIN Reference.County c ON sd.CountyId = c.CountyId
				WHERE s.OfficeId = office.OfficeId
					and c.Name = 'Lawrence'
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

BEGIN TRANSACTION

BEGIN TRY
	

	IF OBJECT_ID('Reference.ChapterOffice', 'U') IS NOT NULL
		EXEC ('USE ClotheOurKids; exec sp_executesql N''DROP TABLE Reference.ChapterOffice;'' ');

	IF OBJECT_ID('Reference.ChapterCounty', 'U') IS NOT NULL
		EXEC ('USE ClotheOurKids; exec sp_executesql N''DROP TABLE Reference.ChapterCounty;'' ');

	IF OBJECT_ID('Reference.Chapter', 'U') IS NOT NULL
		EXEC ('USE ClotheOurKids; exec sp_executesql N''DROP TABLE Reference.Chapter;'' ');

	IF OBJECT_ID('Reference.Address', 'U') IS NOT NULL
		EXEC ('USE ClotheOurKids; exec sp_executesql N''DROP TABLE Reference.Address;'' ');

	CREATE TABLE Reference.Address
	(
		AddressId				int NOT NULL IDENTITY(1,1) CONSTRAINT PK_Reference_Address_AddressId PRIMARY KEY,
		StreetAddress			varchar(500) NULL,
		City					varchar(100) NULL,
		StateId					smallint NULL,
		PostalCode				char(6) NULL,
		PostalCodeExt			char(4) NULL
	)

	ALTER TABLE Reference.Address
	ADD CONSTRAINT AK_Reference_Address_StreetAddress_City_StateId UNIQUE (StreetAddress, City, StateId);


	CREATE TABLE Reference.Chapter
	(
		ChapterId				int NOT NULL IDENTITY(1,1) CONSTRAINT PK_Reference_Chapter_ChapterId PRIMARY KEY,
		OfficeId				smallint NOT NULL,
		MailingAddressId		int NOT NULL,
		IncorporatedDate		date NOT NULL,
		IncorporatedStateId		smallint NOT NULL,
		Email					nvarchar(256) NOT NULL
	);

	ALTER TABLE Reference.Chapter
	ADD CONSTRAINT AK_Reference_Chapter_OfficeId UNIQUE (OfficeId);

	ALTER TABLE Reference.Chapter
		ADD CONSTRAINT FK_Reference_Office$DefinesOfficeOfThis$Reference_Chapter
		FOREIGN KEY (OfficeId) REFERENCES Reference.Office (OfficeId)
	ON UPDATE NO ACTION
	ON DELETE NO ACTION; 

	ALTER TABLE Reference.Chapter	
		ADD CONSTRAINT FK_Reference_States$DefinesStateWhereChapterIsIncorporatedIn$Reference_Chapter
		FOREIGN KEY (IncorporatedStateId) REFERENCES Reference.States (StateId)
	ON UPDATE NO ACTION
	ON DELETE NO ACTION;

	--Used to allow bulk assignments of Chapters to Offices
	-- ChapterOffice table will the main table that is used to describe which offices are served by which chapter,
	--	because the ChapterCounty relationship may not always be true to describe each office.
	CREATE TABLE Reference.ChapterCounty
	(
		ChapterCountyId			int NOT NULL IDENTITY(1,1) CONSTRAINT PK_Reference_ChapterCounty_ChapterCountyId PRIMARY KEY,
		ChapterId				int NOT NULL,
		CountyId				smallint NOT NULL
	);

	ALTER TABLE Reference.ChapterCounty
	ADD CONSTRAINT AK_Reference_ChapterCounty_ChapterId_CountyId UNIQUE (ChapterId, CountyId);

	ALTER TABLE Reference.ChapterCounty
		ADD CONSTRAINT FK_Reference_Chapter$DefinesChapterFor$Reference_ChapterCounty
		FOREIGN KEY (ChapterId) REFERENCES Reference.Chapter (ChapterId)
	ON UPDATE NO ACTION
	ON DELETE NO ACTION;

	ALTER TABLE Reference.ChapterCounty
		ADD CONSTRAINT FK_Reference_County$DefinesCountyFor$Reference_ChapterCounty
		FOREIGN KEY (CountyId) REFERENCES Reference.County (CountyId)
	ON UPDATE NO ACTION
	ON DELETE NO ACTION;


	CREATE TABLE Reference.ChapterOffice
	(
		ChapterOfficeId			int NOT NULL IDENTITY(1,1) CONSTRAINT PK_Reference_ChapterOffice_ChapterOfficeId PRIMARY KEY,
		ChapterId				int NOT NULL,
		OfficeId				smallint NOT NULL
	);

	ALTER TABLE Reference.ChapterOffice
	ADD CONSTRAINT AK_Reference_ChapterOffice_ChapterId_OfficeId UNIQUE (ChapterId, OfficeId);

	ALTER TABLE Reference.ChapterOffice
		ADD CONSTRAINT FK_Reference_Chapter$DefinesChapterFor$Reference_ChapterOffice
		FOREIGN KEY (ChapterId) REFERENCES Reference.Chapter (ChapterId)
	ON UPDATE NO ACTION
	ON DELETE NO ACTION;

	ALTER TABLE Reference.ChapterOffice
		ADD CONSTRAINT FK_Reference_Office$DefinesServedByChapterIn$Reference_ChapterOffice
		FOREIGN KEY (OfficeId) REFERENCES Reference.Office (OfficeId)
	ON UPDATE NO ACTION
	ON DELETE NO ACTION;


	INSERT INTO Reference.Office (Name, OfficeTypeId, StreetAddress, City, StateId, PostalCode, Phone, CountyId)
	SELECT	'Clothe Our Kids of North Alabama' as Name,
			(
				SELECT ot.OfficeTypeId
				FROM Reference.OfficeType ot
				WHERE ot.Name = 'NonProfit'
			) as OfficeTypeId,
			'317 AL-67' as StreetAddress,
			'Decatur' as City,
			1 as StateId,
			35603 as PostalCode,
			'256-887-5437' as Phone,
			1 as CountyId

	INSERT INTO Reference.Address (StreetAddress, City, StateId, PostalCode)
	VALUES ('PO Box 565', 'Somerville', 1, '35670');

	INSERT INTO Reference.Chapter (OfficeId, MailingAddressId, IncorporatedDate, IncorporatedStateId, Email)
	SELECT	(
				SELECT OfficeId
				FROM Reference.Office o
				WHERE o.Name LIKE 'Clothe Our Kids of North Alabama'
			) as OfficeId,
			(	
				SELECT AddressId
				FROM Reference.Address a
				WHERE a.StateId = 1
					AND a.City = 'Somerville'
					AND a.StreetAddress = 'PO Box 565'
			) as MailingAddressId,
			'20160415' as IncorporatedDate,
			1 AS IncorporatedStateId,
			'givekidsclothes@gmail.com';

	INSERT INTO Reference.ChapterCounty (CountyId, ChapterId)
	SELECT co.CountyId,
			(
				SELECT c.ChapterId
				FROM Reference.Chapter c
				WHERE c.email = 'givekidsclothes@gmail.com'
			) as ChapterId
	FROM Reference.County co
		INNER JOIN Reference.States s ON co.StateId = s.StateId
	WHERE co.Name IN ('Morgan', 'Lawrence')
		AND s.Code = 'AL';
			

	INSERT INTO Reference.ChapterOffice (ChapterId, OfficeId)
	SELECT cc.ChapterId, o.OfficeId
	FROM Reference.Office o
		INNER JOIN Reference.ChapterCounty cc ON o.CountyId = cc.CountyId
		LEFT OUTER JOIN Reference.Chapter c ON o.OfficeId = c.OfficeId
	WHERE c.ChapterId IS NULL;

	COMMIT;

END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION;

	THROW;
END CATCH;
GO












