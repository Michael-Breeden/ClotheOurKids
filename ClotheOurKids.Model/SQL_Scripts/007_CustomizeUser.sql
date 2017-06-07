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
ALTER COLUMN PhoneNumber nvarchar(64);
GO