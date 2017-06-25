ALTER TABLE dbo.AspNetUsers
ADD FirstName nvarchar(50) not null;
GO

ALTER TABLE dbo.AspNetUsers
ADD LastName nvarchar(50) not null;
GO

ALTER TABLE dbo.AspNetUsers
ADD PositionId smallInt not null;
GO

ALTER TABLE dbo.AspNetUsers
ADD OfficeId smallInt not null;
GO

ALTER TABLE dbo.AspNetUsers
ADD ContactMethodId smallInt not null;

ALTER TABLE dbo.AspNetUsers
ALTER COLUMN PhoneNumber varchar(50);
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

ALTER TABLE dbo.AspNetUsers
	ADD CONSTRAINT FK_Reference_ContactMethod$IsPreferredMethodOfContactFor$dbo_AspNetUsers
	FOREIGN KEY (ContactMethodId) REFERENCES Reference.ContactMethod (ContactMethodId);
GO