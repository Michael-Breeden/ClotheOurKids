USE ClotheOurKids
GO

CREATE SCHEMA Reference;
GO

CREATE TABLE Reference.OfficeType
(
	OfficeTypeId			tinyint NOT NULL CONSTRAINT PK_Reference_OfficeType_OfficeTypeId PRIMARY KEY,
	Name					varchar(20) NOT NULL,
	Description				varchar(500) NULL
)
GO

ALTER TABLE Reference.OfficeType
	ADD CONSTRAINT AK_Reference_OfficeType_Name UNIQUE (Name);
GO

INSERT INTO Reference.OfficeType (OfficeTypeId, Name)
VALUES (1, 'School'), (2, 'NonProfit'), (3, 'Government'), (4, 'Church');
GO

CREATE TABLE Reference.County
(
	CountyId			smallInt NOT NULL CONSTRAINT PK_Reference_County_CountyId PRIMARY KEY,
	Name				varchar(50) NOT NULL,
	Description			varchar(500) NULL,
	ServiceStartDate	date NULL,
	ServiceEndDate		date NULL
)
GO

ALTER TABLE Reference.County
	ADD CONSTRAINT AK_Reference_County_Name UNIQUE (Name);
GO

INSERT INTO Reference.County (CountyId, Name, ServiceStartDate)
VALUES (1, 'Morgan County', '20160801'), (2, 'Lawrence County', '20170801');
GO




CREATE TABLE Reference.SchoolSystem
(
	SchoolSystemId			smallint NOT NULL CONSTRAINT PK_Reference_SchoolSystem_SchoolSystemId PRIMARY KEY,
	Name					varchar(20) NOT NULL,
	Description				varchar(500) NULL,
	CountyId				smallInt NOT NULL
)
GO

ALTER TABLE Reference.SchoolSystem
	ADD CONSTRAINT AK_Reference_SchoolSystem_Name UNIQUE (Name);
GO

ALTER TABLE Reference.SchoolSystem
	ADD CONSTRAINT FK_Reference_SchoolSystem$IsPartOfTheCounty$Reference_County
	FOREIGN KEY (CountyId) REFERENCES Reference.County (CountyId)
ON UPDATE NO ACTION
ON DELETE NO ACTION;
GO

INSERT INTO Reference.SchoolSystem (SchoolSystemId, Name, CountyId)
VALUES	(1, 'Morgan County', 1),
		(2, 'Decatur City', 1),
		(3, 'Hartselle City', 1);
GO

CREATE TABLE Reference.SchoolType
(
	SchoolTypeId			tinyint NOT NULL CONSTRAINT PK_Reference_SchoolType_SchoolTypeId PRIMARY KEY,
	Name					varchar(20) NOT NULL,
	Description				varchar(500) NULL
);
GO

ALTER TABLE Reference.SchoolType
	ADD CONSTRAINT AK_Reference_SchoolType_Name UNIQUE (Name);
GO

INSERT INTO Reference.SchoolType (SchoolTypeId, Name)
VALUES	(1, 'Elementary'),
		(2, 'Middle School'),
		(3, 'High School');
GO

CREATE TABLE Reference.Office
(
	OfficeId				smallInt NOT NULL IDENTITY(1,1) CONSTRAINT PK_Reference_Office_OfficeId PRIMARY KEY,
	Name					varchar(100) NOT NULL,
	OfficeTypeId			tinyint NOT NULL,
	StreetAddress			varchar(250) NULL,
	City					varchar(50) NULL,
	State					char(2) NULL,
	PostalCode				char(6) NULL,
	PostalCodeExt			char(4) NULL,
	Phone					varchar(50) NULL
)
GO

ALTER TABLE Reference.Office
	ADD CONSTRAINT AK_Reference_Office_Name UNIQUE (Name);
GO

ALTER TABLE Reference.Office
	ADD CONSTRAINT FK_Reference_OfficeType$DescribesOrganizationalTypeOf$Reference_Office
	FOREIGN KEY (OfficeTypeId) REFERENCES Reference.OfficeType (OfficeTypeId)
ON UPDATE NO ACTION
ON DELETE NO ACTION;
GO

INSERT INTO Reference.Office (Name, OfficeTypeId, StreetAddress, City, State, PostalCode, Phone)
VALUES 
	('Brewer High School', 1, '59 Eva Rd', 'Somerville', 'AL', '35670', '2567788634'),
	('Cotaco Junior High School', 1, '100 Cotaco School Rd', 'Somerville', 'AL', '35670', '2567788154'),
	('Danville High School', 1, '9235 Danville Rd', 'Danville', 'AL', '35619', '2567739909'),
	('Danville Middle School', 1, '5933 AL-36', 'Danville', 'AL', '35619', '2567737723'),
	('Danville-Neel Elementary School', 1, '8688 Danville Rd', 'Danville', 'AL', '35619', '2567737182'),
	('Eva Junior High School', 1, '20 School Rd', 'Eva', 'AL', '35621', '2567965141'),
	('Falkville Elementary School', 1, '72 Clark Dr', 'Falkville', 'AL', '35622', '2567845249'),
	('Falkville High School', 1, '43 Clark Dr', 'Falkville', 'AL', '35622', '2567845248'),
	('Laceys Spring Junior High School', 1, '48 School Rd', 'Laceys Spring', 'AL', '35754', '2568814460'),
	('Priceville Elementary School', 1, '438 Cave Spring Rd', 'Priceville', 'AL', '35603', '2563419202'),
	('Priceville High School', 1, '2650 N Bethel Rd', 'Decatur', 'AL', '35603', '2563531950'),
	('Priceville Junior High School', 1, '317 AL-67', 'Decatur', 'AL', '35603', '2563555104'),
	('Sparkman Junior High School', 1, '72 Plainview Street', 'Hartselle', 'AL', '35640', '2567736458'),
	('Union Hill Junior High School', 1, '2221 Union Hill Rd', 'Somerville', 'AL', '35670', '2564982431'),
	('West Morgan Elementary School', 1, '571 Old Hwy 24', 'Trinity', 'AL', '35673', '2563508818'),
	('West Morgan High School', 1, '261 S Greenway Dr', 'Trinity', 'AL', '35673', '2563535214'),
	('West Morgan Middle School', 1, '261 S Greenway Dr', 'Trinity', 'AL', '35673', '2563535214'),
	('Austin High School', 1, 2, 3),
	('Decatur High School', 1, 2, 3),
	('Brookhaven Middle School', 1, 2, 2),
	('Cedar Ridge Middle School', 1, 2, 2),
	('Oak Park Middle School', 1, 2, 2),
	('Austinville Elementary School', 1, 2, 1),
	('Banks-Caddell Elementary School', 1, 2, 1),
	('Benjamin Davis Elementary School', 1, 2, 1),
	('Chestnut Grove Elementary School', 1, 2, 1),
	('Eastwood Elementary School', 1, 2, 1),
	('Frances Nungester Elementary School', 1, 2, 1),
	('Julian Harris Elementary School', 1, 2, 1),
	('Leon Sheffield Magnet Elementary School', 1, 2, 1),
	('Somerville Road Elementary School', 1, 2, 1),
	('Walter Jackson Elementary School', 1, 2, 1),
	('West Decatur Elementary School', 1, 2, 1),
	('Woodmeade Elementary School', 1, 2, 1),
	('Barkley Bridge Elementary', 1, 3, 1),
	('Crestline Elementary', 1, 3, 1),
	('F.E. Burleson Elementary', 1, 3, 1),
	('Hartselle Intermediate', 1, 3, 2),
	('Hartselle Jr High', 1, 3, 2),
	('Hartselle High School', 1, 3, 3),
	('P.A.C.T Office', 2, NULL, NULL);
GO

CREATE TABLE Reference.School
(
	SchoolId				smallint NOT NULL IDENTITY(1,1) CONSTRAINT PK_Reference_School_SchoolId PRIMARY KEY,
	OfficeId				smallint NOT NULL,
	SchoolSystemId			smallint NOT NULL,
	SchoolTypeId			tinyint NOT NULL
)

ALTER TABLE Reference.School
	ADD CONSTRAINT AK_Reference_School_OfficeId UNIQUE (OfficeId);
GO

ALTER TABLE Reference.School
	ADD CONSTRAINT FK_Reference_School$DescribesSchoolDetailsFor$Reference_Office
	FOREIGN KEY (OfficeId) REFERENCES Reference.Office (OfficeId)
ON UPDATE NO ACTION
ON DELETE NO ACTION;
GO

ALTER TABLE Reference.School
	ADD CONSTRAINT FK_Reference_SchoolSystem$IdentifiesSchoolSystemOf$Reference_School
	FOREIGN KEY (SchoolSystemId) REFERENCES Reference.SchoolSystem (SchoolSystemId)
ON UPDATE NO ACTION
ON DELETE NO ACTION;
GO

ALTER TABLE Reference.School
	ADD CONSTRAINT FK_Reference_SchoolType$IdentifiesTypeOf$Reference_School
	FOREIGN KEY (SchoolTypeId) REFERENCES Reference.SchoolType (SchoolTypeId)
ON UPDATE NO ACTION
ON DELETE NO ACTION;
GO

CREATE TABLE Reference.Position
(
	PositionId		smallint NOT NULL CONSTRAINT PK_Reference_Position_PositionId PRIMARY KEY,
	Name			varchar(50) NOT NULL,
	Description		varchar(500) NULL
);
GO

ALTER TABLE Reference.Position
	ADD CONSTRAINT AK_Reference_Position_Name UNIQUE (Name);
GO

INSERT INTO Reference.Position (PositionId, Name)
VALUES	(1, 'Counselor'),
		(2, 'Teacher'),
		(3, 'Principal'),
		(4, 'F.A.C.T Resource Counselor');
GO

CREATE TABLE Reference.PositionOfficeType
(
	PositionOfficeTypeId	int NOT NULL IDENTITY(1,1) CONSTRAINT PK_Reference_PositionOfficeType_PositionOfficeTypeId PRIMARY KEY,
	PositionId				smallint NOT NULL,
	OfficeTypeId			tinyint NOT NULL
)

ALTER TABLE Reference.PositionOfficeType
	ADD CONSTRAINT AK_Reference_PositionOfficeType_PositionId_OfficeTypeId UNIQUE (PositionId, OfficeTypeId);
GO

ALTER TABLE Reference.PositionOfficeType
	ADD CONSTRAINT FK_Reference_PositionOfficeType$SpecifiesOfficeTypeOf$Reference_Position
	FOREIGN KEY (PositionId) REFERENCES Reference.Position (PositionId)
ON UPDATE NO ACTION
ON DELETE NO ACTION;
GO

ALTER TABLE Reference.PositionOfficeType
	ADD CONSTRAINT FK_Reference_OfficeType$DescribesTypeOfOfficeAssociatedWith$Reference_PositionOfficeType
	FOREIGN KEY (OfficeTypeId) REFERENCES Reference.OfficeType (OfficeTypeId)
ON UPDATE NO ACTION
ON DELETE NO ACTION;
GO

INSERT INTO Reference.PositionOfficeType (PositionId, OfficeTypeId)
VALUES	(1, 1),
		(2, 1),
		(3, 1),
		(4, 2);
GO



ALTER TABLE dbo.AspNetUsers
ADD FirstName nvarchar(50) not null;
GO

ALTER TABLE dbo.AspNetUsers
ADD LastName nvarchar(50) not null;
GO

ALTER TABLE dbo.AspNetUsers
ADD PositionId smallint not null;
GO

ALTER TABLE dbo.AspNetUsers
ADD OfficeId smallint not null;
GO


CREATE TABLE Reference.Grade
(
	GradeId			smallint NOT NULL IDENTITY(1,1) CONSTRAINT PK_Reference_Grade_GradeId PRIMARY KEY,
	Name			varchar(20) NOT NULL,
	Description		varchar(500) NULL
);
GO

ALTER TABLE Reference.Grade
	ADD CONSTRAINT AK_Reference_Grade_Name UNIQUE (Name);
GO

INSERT INTO Reference.Grade (Name)
VALUES	('Under Pre-K'),
		('Pre-K'),
		('Kindergarten'),
		('1st'),
		('2nd'),
		('3rd'),
		('4th'),
		('5th'),
		('6th'),
		('7th'),
		('8th'),
		('9th'),
		('10th'),
		('11th'),
		('12th');
GO


--CREATE TABLE Reference.Size
--(
--	SizeId				int NOT NULL CONSTRAINT PK_Reference_Size_SizeId PRIMARY KEY,
--	Name				varchar(50) NOT NULL,
--	Description			varchar(500) NULL
--);

--ALTER TABLE Reference.Size
--	ADD CONSTRAINT AK_Reference_Size_Name UNIQUE (NAME);
--GO

--INSERT INTO Reference.Size (SizeId, Name)
--VALUES	(1, '4T'),
--		(2, '5'),
--		(3, '6'),
--		(4, '7'),
--		(5, '8'),
--		(6, '10'),
--		(7, '12'),
--		(8, '14'),
--		(9, '16'),
--		(10, 'XS'),
--		(11, 'Small'),
--		(12, 'Medium'),
--		(13, 'Large'),
--		(14, 'XL'),
--		(15, '2XL'),
--		(16, '3XL'),
--		(17, '26'),
--		(18, '27'),
--		(19, '28'),
--		(20, '29'),
--		(21, '30-31'),
--		(22, '32-33'),
--		(23, '34-35'),
--		(25, '36-37'),
--		(26, '38-39'),
--		(27, '40-41'),
--		(28, '42-43'),
--		(29, '44-46'),
--		(30, '46-47'),
--		(31, '48-49'),
--		(32, '00'),
--		(33, '0'),
--		(34, '1'),
--		(35, '3'),
--		(38, '9'),
--		(39, '11'),
--		(40, '13'),
--		(41, '15'),
--		(42, '17'),
--		(43, '2'),
--		(44, '4'),
--		(45, '18'),
--		(46, '20');



--CREATE TABLE Reference.AgeGroup
--(
--	AgeGroupId		int NOT NULL CONSTRAINT PK_Reference_AgeGroup_AgeGroupId PRIMARY KEY,
--	Name			varchar(50) NOT NULL,
--	Description		varchar(500) NULL
--)

--ALTER TABLE Reference.AgeGroup
--	ADD CONSTRAINT AK_Reference_AgeGroup_Name UNIQUE (NAME);
--GO


--INSERT INTO Reference.AgeGroup (AgeGroupId, Name)
--VALUES (1, 'Child'), (2, 'Junior'), (3, 'Adult');
--GO


--CREATE TABLE Reference.ClothesType
--(
--	ClothesTypeId	int NOT NULL CONSTRAINT PK_Reference_ClothesType_ClothesTypeId PRIMARY KEY,
--	Name			varchar(50) NOT NULL
--)

--ALTER TABLE Reference.ClothesType
--	ADD CONSTRAINT AK_Reference_ClothesType_Name UNIQUE (NAME)
--GO

--INSERT INTO Reference.ClothesType (ClothesTypeId, Name)
--VALUES (1, 'Shirt'), (2, 'Pant');
--GO


--CREATE TABLE Reference.SizeChart
--(
--	SizeChartId		int NOT NULL IDENTITY(1,1) CONSTRAINT PK_Reference_SizeChart_SizeChartId PRIMARY KEY,
--	SizeId			int NOT NULL,
--	AgeGroupId		int NOT NULL,
--	Gender			varchar(10) NOT NULL,
--	ClothesTypeId	int NOT NULL,
--	SortOrder		int NULL
--)

--ALTER TABLE Reference.SizeChart
--	ADD CONSTRAINT AK_Reference_SizeChart_SizeId_AgeGroupId_Gender_ClothesTypeId UNIQUE (SizeId, AgeGroupId, Gender, ClothesTypeId);
--GO

--CREATE NONCLUSTERED INDEX UQ_Reference_SizeChart_AgeGroupId_Gender_ClothesTypeId_SortOrder ON Reference.SizeChart (AgeGroupId, Gender, ClothesTypeId, SortOrder)
--WHERE SortOrder IS NOT NULL;
--GO

--ALTER TABLE Reference.SizeChart
--	ADD CONSTRAINT CHK_Reference_SizeChart_Gender
--		CHECK (Gender IN ('Male', 'Female'));
--GO


--INSERT INTO Reference.SizeChart (SizeId, AgeGroupId, Gender, ClothesTypeId)
--SELECT s.SizeId, 1, 'Male', 1
--FROM Reference.Size s
--WHERE s.Name IN ('4T', '5', '6', '7', '8', '10', '12', '14', '16') 
--UNION ALL
--SELECT s.SizeId, 1, 'Female', 1
--FROM Reference.Size s
--WHERE s.Name IN ('4T', '5', '6', '7', '8', '10', '12', '14', '16') 
--UNION ALL
--SELECT s.SizeId, 2, 'Female', 1
--FROM Reference.Size s
--WHERE s.Name IN ('XS', 'Small', 'Medium', 'Large', 'XL', '2XL', '3XL')
--UNION ALL
--SELECT s.SizeId, 3, 'Male', 1
--FROM Reference.Size s
--WHERE s.Name IN ('XS', 'Small', 'Medium', 'Large', 'XL', '2XL', '3XL')
--UNION ALL
--SELECT s.SizeId, 3, 'Female', 1
--FROM Reference.Size s
--WHERE s.Name IN ('XS', 'Small', 'Medium', 'Large', 'XL', '2XL', '3XL')
--UNION ALL
--SELECT s.SizeId, 1, 'Male', 2
--FROM Reference.Size s
--WHERE s.Name IN ('4T', '5', '6', '7', '8', '10', '12', '14', '16')
--UNION ALL
--SELECT s.SizeId, 1, 'Female', 2
--FROM Reference.Size s
--WHERE s.Name IN ('4T', '5', '6', '7', '8', '10', '12', '14', '16')
--UNION ALL
--SELECT s.SizeId, 2, 'Female', 2
--FROM Reference.Size s
--WHERE s.Name IN ('00', '0', '1', '3', '5', '7', '9', '11', '13', '15', '17')
--UNION ALL
--SELECT s.SizeId, 3, 'Male', 2
--FROM Reference.Size s
--WHERE s.Name IN ('26', '27', '28', '29', '30-31', '32-33', '34-35', '36-37', '38-39', '40-41', '42-43', '44-45', '46-47', '48-49')
--UNION ALL
--SELECT s.SizeId, 3, 'Female', 2
--FROM Reference.Size s
--WHERE s.Name IN ('0', '2', '4', '6', '8', '10', '12', '14', '16', '18', '20');
--GO

--ALTER TABLE Reference.SizeChart
--	ADD CONSTRAINT FK_Reference_Size$ProvidesClothingSizeDetailsFor$Reference_SizeChart
--	FOREIGN KEY (SizeId) REFERENCES Reference.Size (SizeId)
--ON UPDATE NO ACTION
--ON DELETE NO ACTION;
--GO

--ALTER TABLE Reference.SizeChart
--	ADD CONSTRAINT FK_Reference_AgeGroup$ProvidesAgeGroupDetailsFor$Reference_SizeChart
--	FOREIGN KEY (AgeGroupId) REFERENCES Reference.AgeGroup (AgeGroupId)
--ON UPDATE NO ACTION
--ON DELETE NO ACTION;
--GO

--ALTER TABLE Reference.SizeChart
--	ADD CONSTRAINT FK_Reference_ClothesType$ProvidesDetailsAboutTypeOfClothingFor$Reference_SizeChart
--	FOREIGN KEY (ClothesTypeId) REFERENCES Reference.ClothesType (ClothesTypeId)
--ON UPDATE NO ACTION
--ON DELETE NO ACTION;
--GO