--USE ClotheOurKids
--USE [dbVelocity_ITL]
--GO

ALTER TABLE [Requests].[NeededItems] DROP CONSTRAINT [FK_Requests_NeededItems$IdentifiesItemsNeededFor$Requests_Request]
GO

ALTER TABLE [Reference].[SchoolDistrict] DROP CONSTRAINT [FK_Reference_SchoolDistrict$IsPartOfTheCounty$Reference_County]
GO
ALTER TABLE [Reference].[School] DROP CONSTRAINT [FK_Reference_SchoolType$IdentifiesTypeOf$Reference_School]
GO
ALTER TABLE [Reference].[School] DROP CONSTRAINT [FK_Reference_SchoolDistrict$IdentifiesSchoolDistrictOf$Reference_School]
GO
ALTER TABLE [Reference].[School] DROP CONSTRAINT [FK_Reference_School$DescribesSchoolDetailsFor$Reference_Office]
GO
ALTER TABLE [Reference].[PositionOfficeType] DROP CONSTRAINT [FK_Reference_PositionOfficeType$SpecifiesOfficeTypeOf$Reference_Position]
GO
ALTER TABLE [Reference].[PositionOfficeType] DROP CONSTRAINT [FK_Reference_OfficeType$DescribesTypeOfOfficeAssociatedWith$Reference_PositionOfficeType]
GO
ALTER TABLE [Reference].[Office] DROP CONSTRAINT [FK_Reference_State$DefinesStateOfResidenceFor$Reference_Office]
GO
ALTER TABLE [Reference].[Office] DROP CONSTRAINT [FK_Reference_OfficeType$DescribesOrganizationalTypeOf$Reference_Office]
GO
ALTER TABLE [dbo].[AspNetUsers] DROP CONSTRAINT [FK_Reference_ContactMethod$IsPreferredMethodOfContactFor$dbo_AspNetUsers]
GO
ALTER TABLE [dbo].[AspNetUsers] DROP CONSTRAINT [FK_Reference_Office$IsOfficeOf$dbo_AspNetUsers]
GO
ALTER TABLE [dbo].[AspNetUsers] DROP CONSTRAINT [FK_Reference_Position$IsPositionOf$dbo_AspNetUsers]
GO

DROP TABLE [Requests].[Request]
GO

DROP TABLE [Requests].[NeededItems]
GO



DROP TABLE [Requests].[Urgency]
GO

DROP TABLE [Requests].[RequestStatus]
GO

DROP TABLE [Reference].[ContactMethod]
GO

DROP TABLE [Reference].[States]
GO

--DROP TABLE [Reference].[Size]
--GO

DROP TABLE [Reference].[SchoolType]
GO

DROP TABLE [Reference].[SchoolDistrict]
GO

DROP TABLE [Reference].[School]
GO

DROP TABLE [Reference].[PositionOfficeType]
GO

DROP TABLE [Reference].[Position]
GO

DROP TABLE [Reference].[OfficeType]
GO

DROP TABLE [Reference].[Office]
GO

DROP TABLE [Reference].[Grade]
GO

DROP TABLE [Reference].[County]
GO

DROP TABLE [Reference].[PantSize]
GO

DROP TABLE [Reference].[ShirtSize]
GO

DROP TABLE [Reference].[Gender]
GO

--DROP TABLE [Reference].[ClothesType]
--GO

DROP TABLE [Reference].[AgeGroup]
GO

DROP SCHEMA [Requests]
GO

DROP SCHEMA [Reference]
GO


CREATE SCHEMA Reference;
GO

CREATE TABLE Reference.ContactMethod
(
	ContactMethodId		smallint NOT NULL CONSTRAINT PK_Reference_ContactMethod_ContactMethodId PRIMARY KEY,
	Name				varchar(20) NOT NULL
);
GO

INSERT INTO Reference.ContactMethod
VALUES (1, 'Phone'), (2, 'Email'), (3, 'Text');
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

-- Reference.States Table courtesy Reference.Statestable.com

CREATE TABLE Reference.States 
(
	StateId				smallint NOT NULL CONSTRAINT PK_Reference_State_StateId PRIMARY KEY, 
	Name				varchar(50), 
	Code				varchar(2)
);
GO

ALTER TABLE Reference.States
	ADD CONSTRAINT AK_Reference_State_Name UNIQUE (Name);
GO

ALTER TABLE Reference.States
	ADD CONSTRAINT AK_Reference_State_Code UNIQUE (Code);
GO

INSERT INTO Reference.States (StateId,Name,Code) VALUES ('1','Alabama','AL');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('2','Alaska','AK');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('3','Arizona','AZ');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('4','Arkansas','AR');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('5','California','CA');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('6','Colorado','CO');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('7','Connecticut','CT');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('8','Delaware','DE');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('9','Florida','FL');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('10','Georgia','GA');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('11','Hawaii','HI');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('12','Idaho','ID');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('13','Illinois', 'IL');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('14','Indiana', 'IN');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('15','Iowa', 'IA');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('16','Kansas', 'KS');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('17','Kentucky', 'KY');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('18','Louisiana', 'LA');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('19','Maine','ME');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('20','Maryland','MD');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('21','Massachusetts','MA');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('22','Michigan','MI');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('23','Minnesota','MN');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('24','Mississippi','MS');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('25','Missouri','MO');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('26','Montana','MT');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('27','Nebraska','NE');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('28','Nevada','NV');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('29','New Hampshire','NH');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('30','New Jersey','NJ');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('31','New Mexico','NM');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('32','New York','NY');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('33','North Carolina','NC');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('34','North Dakota','ND');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('35','Ohio','OH');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('36','Oklahoma','OK');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('37','Oregon','OR');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('38','Pennsylvania','PA');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('39','Rhode Island','RI');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('40','South Carolina','SC');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('41','South Dakota','SD');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('42','Tennessee','TN');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('43','Texas','TX');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('44','Utah','UT');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('45','Vermont','VT');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('46','Virginia','VA');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('47','Washington','WA');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('48','West Virginia','WV');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('49','Wisconsin','WI');
INSERT INTO Reference.States (StateId,Name,Code) VALUES ('50','Wyoming','WY');
GO


CREATE TABLE Reference.County
(
	CountyId			smallInt NOT NULL CONSTRAINT PK_Reference_County_CountyId PRIMARY KEY,
	Name				varchar(50) NOT NULL,
	Description			varchar(500) NULL,
	ServiceStartDate	date NULL,
	ServiceEndDate		date NULL,
	StateId				smallint
)
GO

ALTER TABLE Reference.County
	ADD CONSTRAINT AK_Reference_County_Name UNIQUE (Name);
GO

INSERT INTO Reference.County (CountyId, Name, ServiceStartDate, StateId)
VALUES (1, 'Morgan County', '20160801', 1), (2, 'Lawrence County', '20170801', 1);
GO




CREATE TABLE Reference.SchoolDistrict
(
	SchoolDistrictId	    smallint NOT NULL CONSTRAINT PK_Reference_SchoolDistrict_SchoolDistrictId PRIMARY KEY,
	Name					varchar(20) NOT NULL,
	Description				varchar(500) NULL,
	CountyId				smallInt NOT NULL
)
GO

ALTER TABLE Reference.SchoolDistrict
	ADD CONSTRAINT AK_Reference_SchoolDistrict_Name UNIQUE (Name);
GO

ALTER TABLE Reference.SchoolDistrict
	ADD CONSTRAINT FK_Reference_SchoolDistrict$IsPartOfTheCounty$Reference_County
	FOREIGN KEY (CountyId) REFERENCES Reference.County (CountyId)
ON UPDATE NO ACTION
ON DELETE NO ACTION;
GO

INSERT INTO Reference.SchoolDistrict (SchoolDistrictId, Name, CountyId)
VALUES	(1, 'Morgan County', 1),
		(2, 'Decatur City', 1),
		(3, 'Hartselle City', 1),
		(4, 'Lawrence County', 2);
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
VALUES	(1, 'Elementary School'),
		(2, 'Middle School'),
		(3, 'High School'),
		(4, 'Other');
GO

CREATE TABLE Reference.Office
(
	OfficeId				smallInt NOT NULL IDENTITY(1,1) CONSTRAINT PK_Reference_Office_OfficeId PRIMARY KEY,
	Name					varchar(100) NOT NULL,
	OfficeTypeId			tinyint NOT NULL,
	StreetAddress			varchar(250) NULL,
	City					varchar(50) NULL,
	StateId					smallInt NULL,
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

ALTER TABLE Reference.Office
	ADD CONSTRAINT FK_Reference_State$DefinesStateOfResidenceFor$Reference_Office
	FOREIGN KEY (StateId) REFERENCES Reference.States (StateId)
ON UPDATE NO ACTION
ON DELETE NO ACTION;
GO

INSERT INTO Reference.Office (Name, OfficeTypeId, StreetAddress, City, StateId, PostalCode, Phone)
VALUES 
	--Morgan County Schools
	('Brewer High School', 1, '59 Eva Rd', 'Somerville', 1, '35670', '2567788634'),
	('Cotaco Junior High School', 1, '100 Cotaco School Rd', 'Somerville', 1, '35670', '2567788154'),
	('Danville High School', 1, '9235 Danville Rd', 'Danville', 1, '35619', '2567739909'),
	('Danville Middle School', 1, '5933 AL-36', 'Danville', 1, '35619', '2567737723'),
	('Danville-Neel Elementary School', 1, '8688 Danville Rd', 'Danville', 1, '35619', '2567737182'),
	('Eva Junior High School', 1, '20 School Rd', 'Eva', 1, '35621', '2567965141'),
	('Falkville Elementary School', 1, '72 Clark Dr', 'Falkville', 1, '35622', '2567845249'),
	('Falkville High School', 1, '43 Clark Dr', 'Falkville', 1, '35622', '2567845248'),
	('Laceys Spring Junior High School', 1, '48 School Rd', 'Laceys Spring', 1, '35754', '2568814460'),
	('Priceville Elementary School', 1, '438 Cave Spring Rd', 'Priceville', 1, '35603', '2563419202'),
	('Priceville High School', 1, '2650 N Bethel Rd', 'Decatur', 1, '35603', '2563531950'),
	('Priceville Junior High School', 1, '317 AL-67', 'Decatur', 1, '35603', '2563555104'),
	('Sparkman Junior High School', 1, '72 Plainview Street', 'Hartselle', 1, '35640', '2567736458'),
	('Union Hill Junior High School', 1, '2221 Union Hill Rd', 'Somerville', 1, '35670', '2564982431'),
	('West Morgan Elementary School', 1, '571 Old Hwy 24', 'Trinity', 1, '35673', '2563508818'),
	('West Morgan High School', 1, '261 S Greenway Dr', 'Trinity', 1, '35673', '2563535214'),
	('West Morgan Middle School', 1, '261 S Greenway Dr', 'Trinity', 1, '35673', '2563535214'),
	('Austin High School', 1, '1625 Danville Rd SW', 'Decatur', 1, '35601', '2565523060'),
	('Decatur High School', 1, '1101 Prospect Dr SE', 'Decatur', 1, '35601', '2565523011'),
	('Brookhaven Middle School', 1, '1302 5th Ave SW', 'Decatur', 1, '35601', '2565523045'),
	('Cedar Ridge Middle School', 1, '2715 Danville Rd SW', 'Decatur', 1, '35603', '2565524622'),
	('Oak Park Middle School', 1, '1218 16th Ave SE', 'Decatur', 1, '35601', '2565523035'),
	('Austinville Elementary School', 1, '2320 Clara Ave SW', 'Decatur', 1, '35601', '2565523050'),
	('Banks-Caddell Elementary School', 1, '211 Gordon Dr SE', 'Decatur', 1, '35601', '2565523040'),
	('Benjamin Davis Elementary School', 1, '417 Monroe Dr NW', 'Decatur', 1, '35601', '2565523025'),
	('Chestnut Grove Elementary School', 1, '3205 Cedarhurst Dr', 'Decatur', 1, '35603', '2565523092'),
	('Eastwood Elementary School', 1, '1802 26th Ave SE', 'Decatur', 1, '35601', '2565523043'),
	('Frances Nungester Elementary School', 1, '726 Tammy St SW', 'Decatur', 1, '35603', '2565523079'),
	('Julian Harris Elementary School', 1, '1922 McAuliffe Dr SW', 'Decatur', 1, '35603', '2565523096'),
	('Leon Sheffield Magnet Elementary School', 1, '801 Wilson St NW', 'Decatur', 1, '35601', '2565523056'),
	('Somerville Road Elementary School', 1, '910 Somerville Rd SE', 'Decatur', 1, '35601', '2565523033'),
	('Walter Jackson Elementary School', 1, '1950 Park St SE', 'Decatur', 1, '35601', '2565523031'),
	('West Decatur Elementary School', 1, '708 Memorial Dr SW', 'Decatur', 1, '35601', '2565523027'),
	('Woodmeade Elementary School', 1, '1400 19th Ave SW', 'Decatur', 1, '35601', '2565523023'),
	('Barkley Bridge Elementary', 1, '2333 Barkley Bridge Rd SW', 'Hartselle', 1, '35640', '2567731931'),
	('Crestline Elementary', 1, '600 Crestline Dr SW', 'Hartselle', 1, '35640', '2567739967'),
	('F.E. Burleson Elementary', 1, '1100 Bethel Rd NE', 'Hartselle', 1, '35640', '2567732411'),
	('Hartselle Intermediate', 1, '130 Petain St SW', 'Hartselle', 1, '35640', '2567736094'),
	('Hartselle Jr High', 1, '904 Sparkman St SW', 'Hartselle', 1, '35640', '2567735426'),
	('Hartselle High School', 1, '1000 Bethel Rd NE', 'Hartselle', 1, '35640', '2567515615'),
	

	--Lawrence County Schools
	('East Lawrence Elementary', 1, '263 County Road 370', 'Trinity', 1, '35673', '2569052514'),
	('East Lawrence High', 1, '55 County Road 370', 'Trinity', 1, '35673', '2569052430'),
	('East Lawrence Middle', 1, '99 County Road 370', 'Trinity', 1, '35673', '2569052420'),
	('Hatton Elementary', 1, '6536 County Road 236', 'Town Creek', 1, '35672', '2566854000'),
	('Hatton High', 1, '6909 Alabama Highway 101', 'Town Creek', 1, '35672', '2566854010'),
	('Hazlewood Elementary', 1, '334 Hazlewood Street', 'Town Creek', 1, '35672', '2566854020'),
	('Lawrence County Center of Technology', 1, '179 College Street', 'Moulton', 1, '35650', '2569052425'),
	('Lawrence County High School', 1, '102 College Street', 'Moulton', 1, '35650', '2569052440'),
	('Moulton Elementary', 1, '412 Main Street', 'Moulton', 1, '35650', '2569052450'),
	('Moulton Middle', 1, '660 College Street', 'Moulton', 1, '35650', '2569052460'),
	('Mount Hope School', 1, '8455 County Road 23', 'Mt. Hope', 1, '35651', '2569052470'),
	('R. A. Hubbard', 1, '12905 Jesse Jackson Parkway', 'Courtland', 1, '35618', '2566373010'),
	('Speake School', 1, '7323 Al Hwy 36', 'Danville', 1, '35619', '2569749201'),

	--School Boards
	('Morgan County Board of Education', 3, '235 Highway 67 South', 'Decatur', 1, '35603', '2563536442'),
	('Decatur City Board of Education', 3, '302 Fourth Avenue', 'Decatur', 1, '35601', '2565523000'),
	('Hartselle City Board of Education', 3, '305 College St NE', 'Hartselle', 1, '35640', '2567735419'),
	('Lawrence County Board of Education', 3, '14131 Market St', 'Moulton', 1, '35650', '2569052400'),

	--Other Organizations
	('P.A.C.T', 2, '245 Jackson St SE', 'Decatur', 1, '35601', '2563557252'),
	('CASA of Noth Alabama', 2, '302 Lee Street NE', 'Decatur', 1, '35602', '2565606102');

GO

CREATE TABLE Reference.School
(
	SchoolId				smallint NOT NULL IDENTITY(1,1) CONSTRAINT PK_Reference_School_SchoolId PRIMARY KEY,
	OfficeId				smallint NOT NULL,
	SchoolDistrictId		smallint NOT NULL,
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
	ADD CONSTRAINT FK_Reference_SchoolDistrict$IdentifiesSchoolDistrictOf$Reference_School
	FOREIGN KEY (SchoolDistrictId) REFERENCES Reference.SchoolDistrict (SchoolDistrictId)
ON UPDATE NO ACTION
ON DELETE NO ACTION;
GO

ALTER TABLE Reference.School
	ADD CONSTRAINT FK_Reference_SchoolType$IdentifiesTypeOf$Reference_School
	FOREIGN KEY (SchoolTypeId) REFERENCES Reference.SchoolType (SchoolTypeId)
ON UPDATE NO ACTION
ON DELETE NO ACTION;
GO

INSERT INTO Reference.School (OfficeId, SchoolDistrictId, SchoolTypeId)
SELECT	o.OfficeId,
		CASE o.Name
			WHEN 'Brewer High School' THEN 1 --Morgan County
			WHEN 'Cotaco Junior High School' THEN 1 --Morgan County
			WHEN 'Danville High School' THEN 1 --Morgan County
			WHEN 'Danville Middle School' THEN 1 --Morgan County
			WHEN 'Danville-Neel Elementary School' THEN 1 --Morgan County
			WHEN 'Eva Junior High School' THEN 1 --Morgan County
			WHEN 'Falkville Elementary School' THEN 1 --Morgan County
			WHEN 'Falkville High School' THEN 1 --Morgan County
			WHEN 'Laceys Spring Junior High School' THEN 1 --Morgan County
			WHEN 'Priceville Elementary School' THEN 1 --Morgan County
			WHEN 'Priceville High School' THEN 1 --Morgan County
			WHEN 'Priceville Junior High School' THEN 1 --Morgan County
			WHEN 'Sparkman Junior High School' THEN 1 --Morgan County
			WHEN 'Union Hill Junior High School' THEN 1 --Morgan County
			WHEN 'West Morgan Elementary School' THEN 1 --Morgan County
			WHEN 'West Morgan High School' THEN 1 --Morgan County
			WHEN 'West Morgan Middle School' THEN 1 --Morgan County
			WHEN 'Austin High School' THEN 2 --Decatur City
			WHEN 'Decatur High School' THEN 2 --Decatur City
			WHEN 'Brookhaven Middle School' THEN 2 --Decatur City
			WHEN 'Cedar Ridge Middle School' THEN 2 --Decatur City
			WHEN 'Oak Park Middle School' THEN 2 --Decatur City
			WHEN 'Austinville Elementary School' THEN 2 --Decatur City
			WHEN 'Banks-Caddell Elementary School' THEN 2 --Decatur City
			WHEN 'Benjamin Davis Elementary School' THEN 2 --Decatur City
			WHEN 'Chestnut Grove Elementary School' THEN 2 --Decatur City
			WHEN 'Eastwood Elementary School' THEN 2 --Decatur City
			WHEN 'Frances Nungester Elementary School' THEN 2 --Decatur City
			WHEN 'Julian Harris Elementary School' THEN 2 --Decatur City
			WHEN 'Leon Sheffield Magnet Elementary School' THEN 2 --Decatur City
			WHEN 'Somerville Road Elementary School' THEN 2 --Decatur City
			WHEN 'Walter Jackson Elementary School' THEN 2 --Decatur City
			WHEN 'West Decatur Elementary School' THEN 2 --Decatur City
			WHEN 'Woodmeade Elementary School' THEN 2 --Decatur City
			WHEN 'Barkley Bridge Elementary' THEN 3 --Hartselle City
			WHEN 'Crestline Elementary' THEN 3 --Hartselle City
			WHEN 'F.E. Burleson Elementary' THEN 3 --Hartselle City
			WHEN 'Hartselle Intermediate' THEN 3 --Hartselle City
			WHEN 'Hartselle Jr High' THEN 3 --Hartselle City
			WHEN 'Hartselle High School' THEN 3 --Hartselle City
			WHEN 'East Lawrence Elementary' THEN 4 --Lawrence County
			WHEN 'East Lawrence High' THEN 4 --Lawrence County
			WHEN 'East Lawrence Middle' THEN 4 --Lawrence County
			WHEN 'Hatton Elementary' THEN 4 --Lawrence County
			WHEN 'Hatton High' THEN 4 --Lawrence County
			WHEN 'Hazlewood Elementary' THEN 4 --Lawrence County
			WHEN 'Lawrence County Center of Technology' THEN 4 --Lawrence County
			WHEN 'Lawrence County High School' THEN 4 --Lawrence County
			WHEN 'Moulton Elementary' THEN 4 --Lawrence County
			WHEN 'Moulton Middle' THEN 4 --Lawrence County	
			WHEN 'Mount Hope School' THEN 4 --Lawrence County
			WHEN 'R. A. Hubbard' THEN 4 --Lawrence County
			WHEN 'Speake School' THEN 4 --Lawrence County
			Else 0
		END as SchoolDistrict,
		CASE o.Name
			WHEN 'Brewer High School' THEN 3 --High school
			WHEN 'Cotaco Junior High School' THEN 2 --Middle school
			WHEN 'Danville High School' THEN 3 --High school
			WHEN 'Danville Middle School' THEN 2 --Middle school
			WHEN 'Danville-Neel Elementary School' THEN 1 --Elementary school
			WHEN 'Eva Junior High School' THEN 2 --Middle school
			WHEN 'Falkville Elementary School' THEN 1 --Elementary school
			WHEN 'Falkville High School' THEN 3 --High school
			WHEN 'Laceys Spring Junior High School' THEN 2 --Middle school
			WHEN 'Priceville Elementary School' THEN 1 --Elementary school
			WHEN 'Priceville High School' THEN 3 --High school
			WHEN 'Priceville Junior High School' THEN 2 --Middle school
			WHEN 'Sparkman Junior High School' THEN 2 --Middle school
			WHEN 'Union Hill Junior High School' THEN 2 --Middle school
			WHEN 'West Morgan Elementary School' THEN 1 --Elementary school
			WHEN 'West Morgan High School' THEN 3 --High school
			WHEN 'West Morgan Middle School' THEN 2 --Middle school
			WHEN 'Austin High School' THEN 3 --High school
			WHEN 'Decatur High School' THEN 3 --High school
			WHEN 'Brookhaven Middle School' THEN 2 --Middle school
			WHEN 'Cedar Ridge Middle School' THEN 2 --Middle school
			WHEN 'Oak Park Middle School' THEN 2 --Middle school
			WHEN 'Austinville Elementary School' THEN 1 --Elementary school
			WHEN 'Banks-Caddell Elementary School' THEN 1 --Elementary school
			WHEN 'Benjamin Davis Elementary School' THEN 1 --Elementary school
			WHEN 'Chestnut Grove Elementary School' THEN 1 --Elementary school
			WHEN 'Eastwood Elementary School' THEN 1 --Elementary school
			WHEN 'Frances Nungester Elementary School' THEN 1 --Elementary school
			WHEN 'Julian Harris Elementary School' THEN 1 --Elementary school
			WHEN 'Leon Sheffield Magnet Elementary School' THEN 1 --Elementary school
			WHEN 'Somerville Road Elementary School' THEN 1 --Elementary school
			WHEN 'Walter Jackson Elementary School' THEN 1 --Elementary school
			WHEN 'West Decatur Elementary School' THEN 1 --Elementary school
			WHEN 'Woodmeade Elementary School' THEN 1 --Elementary school
			WHEN 'Barkley Bridge Elementary' THEN 1 --Elementary school
			WHEN 'Crestline Elementary' THEN 1 --Elementary school
			WHEN 'F.E. Burleson Elementary' THEN 1 --Elementary school
			WHEN 'Hartselle Intermediate' THEN 2 --Middle school
			WHEN 'Hartselle Jr High' THEN 2 --Middle school
			WHEN 'Hartselle High School' THEN 3 --High school
			WHEN 'East Lawrence Elementary' THEN 1 --Elementary school
			WHEN 'East Lawrence High' THEN 3 --High school
			WHEN 'East Lawrence Middle' THEN 2 --Middle school
			WHEN 'Hatton Elementary' THEN 1 --Elementary school
			WHEN 'Hatton High' THEN 3 --High school
			WHEN 'Hazlewood Elementary' THEN 1 --Elementary school
			WHEN 'Lawrence County Center of Technology' THEN 3 --High school
			WHEN 'Lawrence County High School' THEN 3 --High school
			WHEN 'Moulton Elementary' THEN 1 --Elementary school
			WHEN 'Moulton Middle' THEN 2 --Middle school		
			WHEN 'Mount Hope School' THEN 1 --Elementary school
			WHEN 'R. A. Hubbard' THEN 3 --High school
			WHEN 'Speake School' THEN 1 --Elementary school
			Else 0
		END as SchoolType
FROM Reference.Office o
	INNER JOIN Reference.OfficeType ot ON o.OfficeTypeId = ot.OfficeTypeId
WHERE ot.Name = 'School'


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
		(3, 'Principal');
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
		(1, 2);
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


CREATE TABLE Reference.AgeGroup
(
	AgeGroupId		tinyint NOT NULL CONSTRAINT PK_Reference_AgeGroup_AgeGroupId PRIMARY KEY,
	Name			varchar(50) NOT NULL,
	Description		varchar(500) NULL
)

ALTER TABLE Reference.AgeGroup
	ADD CONSTRAINT AK_Reference_AgeGroup_Name UNIQUE (NAME);
GO


INSERT INTO Reference.AgeGroup (AgeGroupId, Name)
VALUES (0, 'None'), (1, 'Boys'), (2, 'Girls'), (3, 'Juniors'), (4, 'Mens'), (5, 'Womens');
GO


--CREATE TABLE Reference.ClothesType
--(
--	ClothesTypeId	tinyint NOT NULL CONSTRAINT PK_Reference_ClothesType_ClothesTypeId PRIMARY KEY,
--	Name			varchar(50) NOT NULL
--)

--ALTER TABLE Reference.ClothesType
--	ADD CONSTRAINT AK_Reference_ClothesType_Name UNIQUE (NAME)
--GO

--INSERT INTO Reference.ClothesType (ClothesTypeId, Name)
--VALUES (1, 'Shirt'), (2, 'Pant');
--GO

CREATE TABLE Reference.Gender
(
	GenderId		varchar(10) NOT NULL CONSTRAINT PK_Reference_Gender_GenderId PRIMARY KEY,
	Abbreviation	char(1) NOT NULL
);
GO

ALTER TABLE Reference.Gender
	ADD CONSTRAINT AK_Reference_Gender_Abbreviation UNIQUE (Abbreviation);
GO

INSERT INTO Reference.Gender (GenderId, Abbreviation)
VALUES ('Male', 'M'), ('Female', 'F');
GO


CREATE TABLE Reference.ShirtSize
(
	ShirtSizeId		int NOT NULL IDENTITY(1,1) CONSTRAINT PK_Reference_ShirtSize_ShirtSizeId PRIMARY KEY,
	Name			varchar(50) NOT NULL,
	AgeGroupId		tinyint NOT NULL,
	GenderId		varchar(10) NOT NULL,
	SortOrder		int NOT NULL
);
GO

ALTER TABLE Reference.ShirtSize
	ADD CONSTRAINT AK_Reference_ShirtSize_Name_AgeGroupId_GenderId UNIQUE (Name, AgeGroupId, GenderId);
GO

CREATE UNIQUE INDEX UQ_Reference_ShirtSize_SortOrder ON Reference.ShirtSize (SortOrder);
GO


ALTER TABLE Reference.ShirtSize
	ADD CONSTRAINT FK_Reference_AgeGroup$DescribesAgeGroupAssociatedWith$Reference_ShirtSize
	FOREIGN KEY (AgeGroupId) REFERENCES Reference.AgeGroup (AgeGroupId)
ON UPDATE NO ACTION
ON DELETE NO ACTION;
GO


INSERT INTO Reference.ShirtSize (Name, AgeGroupId, GenderId, SortOrder)
VALUES ('Newborn', 1, 'Male', 1010),
		('0-3 Months', 1, 'Male', 1020),
		('3-6 Months', 1, 'Male', 1030),
		('6-9 Months', 1, 'Male', 1040),
		('9-12 Months', 1, 'Male', 1050),
		('12-18 Months', 1, 'Male', 1060),
		('2T', 1, 'Male', 1070),
		('3T', 1, 'Male', 1080),
		('4T', 1, 'Male', 1090),
		('5', 1, 'Male', 1100),
		('6', 1, 'Male', 1110),
		('7', 1, 'Male', 1120),
		('8', 1, 'Male', 1130),
		('10', 1, 'Male', 1140),
		('12', 1, 'Male', 1150),
		('14', 1, 'Male', 1160),
		('16', 1, 'Male', 1170),
			--Girls
		('Newborn', 2, 'Female', 2010),
		('0-3 Months', 2, 'Female', 2020),
		('3-6 Months', 2, 'Female', 2030),
		('6-9 Months', 2, 'Female', 2040),
		('9-12 Months', 2, 'Female', 2050),
		('12-18 Months', 2, 'Female', 2060),
		('2T', 2, 'Female', 2070),
		('3T', 2, 'Female', 2080),
		('4T', 2, 'Female', 2090),
		('5', 2, 'Female', 2100),
		('6', 2, 'Female', 2110),
		('7', 2, 'Female', 2120),
		('8', 2, 'Female', 2130),
		('10', 2, 'Female', 2140),
		('12', 2, 'Female', 2150),
		('14', 2, 'Female', 2160),
		('16', 2, 'Female', 2170),
			--Juniors
		('XS', 3, 'Female', 3010),
		('Small', 3, 'Female', 3020),
		('Medium', 3, 'Female', 3030),
		('Large', 3, 'Female', 3040),
		('XL', 3, 'Female', 3050),
			--Mens
		('XS', 4, 'Male', 4010),
		('Small', 4, 'Male', 4020),
		('Medium', 4, 'Male', 4030),
		('Large', 4, 'Male', 4040),
		('XL', 4, 'Male', 4050),
		('2XL', 4, 'Male', 4060),
		('3XL', 4, 'Male', 4070),
		('4XL', 4, 'Male', 4080),
			--Womens
		('XS', 5, 'Female', 5010),
		('Small', 5, 'Female', 5020),
		('Medium', 5, 'Female', 5030),
		('Large', 5, 'Female', 5040),
		('XL', 5, 'Female', 5050),
		('2XL', 5, 'Female', 5060),
		('3XL', 5, 'Female', 5070),
		('4XL', 5, 'Female', 5080);
GO


CREATE TABLE Reference.PantSize
(
	PantSizeId		int NOT NULL IDENTITY(1,1) CONSTRAINT PK_Reference_PantSize_PantSizeId PRIMARY KEY,
	Name			varchar(50) NOT NULL,
	AgeGroupId		tinyint NOT NULL,
	GenderId		varchar(10) NOT NULL,
	SortOrder		int NOT NULL
);
GO

ALTER TABLE Reference.PantSize
	ADD CONSTRAINT AK_Reference_PantSize_Name_AgeGroupId_GenderId UNIQUE (Name, AgeGroupId, GenderId);
GO

CREATE UNIQUE INDEX UQ_Reference_PantSize_SortOrder ON Reference.PantSize (SortOrder);
GO

ALTER TABLE Reference.PantSize
	ADD CONSTRAINT FK_Reference_AgeGroup$DescribesAgeGroupAssociatedWith$Reference_PantSize
	FOREIGN KEY (AgeGroupId) REFERENCES Reference.AgeGroup (AgeGroupId)
ON UPDATE NO ACTION
ON DELETE NO ACTION;
GO


INSERT INTO Reference.PantSize (Name, AgeGroupId, GenderId, SortOrder)
VALUES  ('Newborn', 1, 'Male', 1010),
		('0-3 Months', 1, 'Male', 1020),
		('3-6 Months', 1, 'Male', 1030),
		('6-9 Months', 1, 'Male', 1040),
		('9-12 Months', 1, 'Male', 1050),
		('12-18 Months', 1, 'Male', 1060),
		('2T', 1, 'Male', 1070),
		('3T', 1, 'Male', 1080),
		('4T', 1, 'Male', 1090),
		('5', 1, 'Male', 1100),
		('6', 1, 'Male', 1110),
		('7', 1, 'Male', 1120),
		('8', 1, 'Male', 1130),
		('10', 1, 'Male', 1140),
		('12', 1, 'Male', 1150),
		('14', 1, 'Male', 1160),
		('16', 1, 'Male', 1170),
		('18', 1, 'Male', 1180),
			--Girls
		('Newborn', 2, 'Female', 2010),
		('0-3 Months', 2, 'Female', 2020),
		('3-6 Months', 2, 'Female', 2030),
		('6-9 Months', 2, 'Female', 2040),
		('9-12 Months', 2, 'Female', 2050),
		('12-18 Months', 2, 'Female', 2060),
		('2T', 2, 'Female', 2070),
		('3T', 2, 'Female', 2080),
		('4T', 2, 'Female', 2090),
		('5', 2, 'Female', 2100),
		('6', 2, 'Female', 2110),
		('7', 2, 'Female', 2120),
		('8', 2, 'Female', 2130),
		('10', 2, 'Female', 2140),
		('12', 2, 'Female', 2150),
		('14', 2, 'Female', 2160),
		('16', 2, 'Female', 2170),
		('18', 2, 'Female', 2180),
			--Juniors
		('00', 3, 'Female', 3010),
		('0', 3, 'Female', 3020),
		('1', 3, 'Female', 3030),
		('3', 3, 'Female', 3040),
		('5', 3, 'Female', 3050),
		('7', 3, 'Female', 3060),
		('9', 3, 'Female', 3070),
		('11', 3, 'Female', 3080),
		('13', 3, 'Female', 3090),
		('15', 3, 'Female', 3110),
		('17', 3, 'Female', 3120),
			--Mens
		('26', 4, 'Male', 4010),
		('27', 4, 'Male', 4020),
		('28', 4, 'Male', 4030),
		('29', 4, 'Male', 4040),
		('30-31', 4, 'Male', 4050),
		('32-33', 4, 'Male', 4060),
		('34-35', 4, 'Male', 4070),
		('36-37', 4, 'Male', 4080),
		('38-39', 4, 'Male', 4090),
		('40-41', 4, 'Male', 4100),
		('42-43', 4, 'Male', 4110),
		('44-45', 4, 'Male', 4120),
		('46-47', 4, 'Male', 4130),
		('48-49', 4, 'Male', 4140),
			--Womens
		('0', 5, 'Female', 5010),
		('2', 5, 'Female', 5020),
		('4', 5, 'Female', 5030),
		('6', 5, 'Female', 5040),
		('8', 5, 'Female', 5050),
		('10', 5, 'Female', 5060),
		('12', 5, 'Female', 5070),
		('14', 5, 'Female', 5080),
		('16', 5, 'Female', 5090),
		('18', 5, 'Female', 5100),
		('20', 5, 'Female', 5110);
GO

CREATE SCHEMA Requests;
GO

CREATE TABLE Requests.Urgency
(
	UrgencyId			tinyint	NOT NULL CONSTRAINT PK_Requests_Urgency_UrgecyId PRIMARY KEY,
	Name				varchar(50) NOT NULL,
	Description			varchar(250) NULL,
	DaysForDelivery		tinyint NOT NULL
)
GO

ALTER TABLE Requests.Urgency
	ADD CONSTRAINT AK_Requets_Urgency_Name UNIQUE (Name);
GO

INSERT INTO Requests.Urgency (UrgencyId, Name, DaysForDelivery)
VALUES	(1, 'Normal', 7),
		(3, 'Immediate Need', 1);
GO

CREATE TABLE Requests.RequestStatus
(
	RequestStatusId		tinyint NOT NULL CONSTRAINT PK_Requests_RequestStatus_RequestStatusId PRIMARY KEY,
	Name				varchar(20) NOT NULL
);
GO

ALTER TABLE Requests.RequestStatus
	ADD CONSTRAINT AK_Requests_RequestStatus_RequestStatusId UNIQUE (RequestStatusId);
GO

INSERT INTO Requests.RequestStatus (RequestStatusId, Name)
VALUES (1, 'Requested'), (2, 'Delivered')

CREATE TABLE Requests.Request 
(
	RequestId			int NOT NULL IDENTITY(1,1) CONSTRAINT PK_Requests_Request_RequestId PRIMARY KEY,
	DateRequested		datetime2 NOT NULL,
	DateEstimatedDelivery date NOT NULL,
	DateDelivered		date NULL,
	SubmittedByUserId	nvarchar(128) NOT NULL,
	RequestStatusId		tinyint NOT NULL,
	SchoolId			smallint NULL,
	GenderId			varchar(10) NOT NULL,
	GradeId				smallint NOT NULL,
	UrgencyId			tinyint NOT NULL,
	ShirtSizeId			int NULL,
	PantSizeId			int NULL,
	PantLengthSizeId	int NULL,
	UnderwearSize		varchar(20) NOT NULL,
	ShoeSize			varchar(20) NOT NULL,
	Comments			varchar(2000) NOT NULL DEFAULT(''),
	NeededItemsId		int NOT NULL
);
GO

ALTER TABLE Requests.Request
	ADD CONSTRAINT FK_Requests_Request$WasSubmittedBy$dbo_AspNetUsers
	FOREIGN KEY (SubmittedByUserId) REFERENCES dbo.AspNetUsers (Id)
ON UPDATE NO ACTION
ON DELETE NO ACTION;
GO

ALTER TABLE Requests.Request
	ADD CONSTRAINT FK_Requests_RequestStatus$DescribesStatusOf$Requests_Request
	FOREIGN KEY (RequestStatusId) REFERENCES Requests.RequestStatus (RequestStatusId)
ON UPDATE NO ACTION
ON DELETE NO ACTION;
GO

ALTER TABLE Requests.Request
	ADD CONSTRAINT FK_Requests_Request$IsForChildAttending$Reference_School
	FOREIGN KEY (SchoolId) REFERENCES Reference.School (SchoolId)
ON UPDATE NO ACTION
ON DELETE NO ACTION;
GO

ALTER TABLE Requests.Request
	ADD CONSTRAINT FK_Reference_Gender$DescribesGenderOfChildFor$Requests_Request
	FOREIGN KEY (GenderId) REFERENCES Reference.Gender (GenderId)
ON UPDATE NO ACTION
ON DELETE NO ACTION;
GO

ALTER TABLE Requests.Request
	ADD CONSTRAINT FK_Requests_Request$IsForChildInGrade$Reference_Grade
	FOREIGN KEY (GradeId) REFERENCES Reference.Grade (GradeId)
ON UPDATE NO ACTION
ON DELETE NO ACTION;
GO

ALTER TABLE Requests.Request
	ADD CONSTRAINT FK_Requests_Request$HasAnUrgencyOf$Requests_Urgency
	FOREIGN KEY (UrgencyId) REFERENCES Requests.Urgency (UrgencyId)
ON UPDATE NO ACTION
ON DELETE NO ACTION;
GO

ALTER TABLE Requests.Request
	ADD CONSTRAINT FK_Requests_ShirtSize$DescribesShirtSizeFor$Requests_Request
	FOREIGN KEY (ShirtSizeId) REFERENCES Reference.ShirtSize (ShirtSizeId)
ON UPDATE NO ACTION
ON DELETE NO ACTION;
GO

ALTER TABLE Requests.Request
	ADD CONSTRAINT FK_Requests_PantSize$DescribesPantSizeFor$Requests_Request
	FOREIGN KEY (PantSizeId) REFERENCES Reference.PantSize (PantSizeId)
ON UPDATE NO ACTION
ON DELETE NO ACTION;
GO

ALTER TABLE Requests.Request
	ADD CONSTRAINT FK_Requests_PantSize$DescribesPantLengthSizeFor$Requests_Request
	FOREIGN KEY (PantLengthSizeId) REFERENCES Reference.PantSize (PantSizeId)
ON UPDATE NO ACTION
ON DELETE NO ACTION;
GO



ALTER TABLE Requests.Request
	ADD CONSTRAINT CHK_Requests_Request_DateEstimatedDelivery
		CHECK (DateEstimatedDelivery >= DateRequested);
GO

ALTER TABLE Requests.Request
	ADD CONSTRAINT CHK_Requests_Request_DateDelivered
		CHECK (DateDelivered >= DateRequested OR DateDelivered IS NULL);
GO



CREATE TABLE Requests.NeededItems
(
	NeededItemsId		int NOT NULL IDENTITY(1,1) CONSTRAINT PK_Requests_NeededItems PRIMARY KEY,
	ShirtFlag			bit NOT NULL DEFAULT(0),
	PantFlag			bit NOT NULL DEFAULT(0),
	UnderwearFlag		bit NOT NULL DEFAULT(0),
	SockFlag			bit NOT NULL DEFAULT(0),
	ShoeFlag			bit NOT NULL DEFAULT(0),
	CoatFlag			bit NOT NULL DEFAULT(0),
	HygieneFlag			bit NOT NULL DEFAULT(0)
);
GO

ALTER TABLE Requests.Request
	ADD CONSTRAINT FK_Requests_NeededItems$IdentifiesItemsNeededFor$Requests_Request
	FOREIGN KEY (NeededItemsId) REFERENCES Requests.NeededItems (NeededItemsId);
GO



ALTER TABLE dbo.AspNetUsers
	ADD CONSTRAINT FK_Reference_ContactMethod$IsPreferredMethodOfContactFor$dbo_AspNetUsers
	FOREIGN KEY (ContactMethodId) REFERENCES Reference.ContactMethod (ContactMethodId);
GO

ALTER TABLE dbo.AspNetUsers
	ADD CONSTRAINT FK_Reference_Position$IsPositionOf$dbo_AspNetUsers
	FOREIGN KEY (PositionId) REFERENCES Reference.Position (PositionId);
GO

ALTER TABLE dbo.AspNetUsers
	ADD CONSTRAINT FK_Reference_Office$IsOfficeOf$dbo_AspNetUsers
	FOREIGN KEY (OfficeId) REFERENCES Reference.Office (OfficeId);
GO