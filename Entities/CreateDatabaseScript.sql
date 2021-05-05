-- DROP SCHEMA dbo;

CREATE SCHEMA dbo;
-- Shoppingcart.dbo.AppRole definition

-- Drop table

-- DROP TABLE Shoppingcart.dbo.AppRole;

CREATE TABLE Shoppingcart.dbo.AppRole (
	Id uniqueidentifier NOT NULL,
	Name varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Description nvarchar(250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK__AppRole__3214EC071AA92E64 PRIMARY KEY (Id)
);


-- Shoppingcart.dbo.AppUser definition

-- Drop table

-- DROP TABLE Shoppingcart.dbo.AppUser;

CREATE TABLE Shoppingcart.dbo.AppUser (
	Id uniqueidentifier NOT NULL,
	UserName varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Email varchar(250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	DateOfBirth date NULL,
	Gender int NULL,
	PasswordHash varbinary(MAX) NULL,
	PasswordSalt varbinary(MAX) NULL,
	Status bit DEFAULT 1 NULL,
	LastActive datetime NULL,
	CreateDate datetime NULL,
	ModifyDate datetime NULL,
	Version bigint NULL,
	CreateBy varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ModifyBy varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK__AppUser__3214EC07AE23C4D3 PRIMARY KEY (Id)
);


-- Shoppingcart.dbo.Category definition

-- Drop table

-- DROP TABLE Shoppingcart.dbo.Category;

CREATE TABLE Shoppingcart.dbo.Category (
	Id varchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Name varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Description varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Status bit DEFAULT 1 NULL,
	CreateBy varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreateDate datetime NULL,
	ModifyBy varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ModifyDate datetime NULL,
	CONSTRAINT PK__Category__3214EC0734C7970B PRIMARY KEY (Id)
);


-- Shoppingcart.dbo.ContactUs definition

-- Drop table

-- DROP TABLE Shoppingcart.dbo.ContactUs;

CREATE TABLE Shoppingcart.dbo.ContactUs (
	Id uniqueidentifier NOT NULL,
	Name nvarchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Email varchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	PhoneNumber varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Subject varchar(250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Confirm int NULL,
	Description varchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreateDate datetime NULL,
	ModifyBy varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ModifyDate datetime NULL,
	ConfirmDescription varchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK__ContactU__3214EC07F91308BA PRIMARY KEY (Id)
);


-- Shoppingcart.dbo.DeliveryType definition

-- Drop table

-- DROP TABLE Shoppingcart.dbo.DeliveryType;

CREATE TABLE Shoppingcart.dbo.DeliveryType (
	Id char(1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Name nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Description nvarchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Status bit DEFAULT 1 NULL,
	Fee decimal(18,2) NOT NULL,
	CONSTRAINT PK__Delivery__3214EC0767B85B76 PRIMARY KEY (Id)
);


-- Shoppingcart.dbo.OrderStatus definition

-- Drop table

-- DROP TABLE Shoppingcart.dbo.OrderStatus;

CREATE TABLE Shoppingcart.dbo.OrderStatus (
	Id int IDENTITY(1,1) NOT NULL,
	OrderStatusName nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Description nvarchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Status bit DEFAULT 1 NULL,
	CreateBy varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreateDate datetime NULL,
	ModifyBy varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ModifyDate datetime NULL,
	CONSTRAINT PK__OrderSta__3214EC07B38A4923 PRIMARY KEY (Id)
);


-- Shoppingcart.dbo.PaymentType definition

-- Drop table

-- DROP TABLE Shoppingcart.dbo.PaymentType;

CREATE TABLE Shoppingcart.dbo.PaymentType (
	Id int IDENTITY(1,1) NOT NULL,
	PaymentTypeName nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Description nvarchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Status bit DEFAULT 1 NULL,
	CreateBy varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreateDate datetime NULL,
	ModifyBy varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ModifyDate datetime NULL,
	IsPaid bit DEFAULT 0 NOT NULL,
	CONSTRAINT PK__PaymentT__3214EC073D55DE46 PRIMARY KEY (Id)
);


-- Shoppingcart.dbo.AppUserRole definition

-- Drop table

-- DROP TABLE Shoppingcart.dbo.AppUserRole;

CREATE TABLE Shoppingcart.dbo.AppUserRole (
	UserId uniqueidentifier NOT NULL,
	RoleId uniqueidentifier NOT NULL,
	CONSTRAINT PK_User_Role PRIMARY KEY (UserId,RoleId),
	CONSTRAINT FK_UserRole_Role FOREIGN KEY (RoleId) REFERENCES Shoppingcart.dbo.AppRole(Id) ON DELETE CASCADE,
	CONSTRAINT FK_UserRole_User FOREIGN KEY (UserId) REFERENCES Shoppingcart.dbo.AppUser(Id) ON DELETE CASCADE
);


-- Shoppingcart.dbo.Product definition

-- Drop table

-- DROP TABLE Shoppingcart.dbo.Product;

CREATE TABLE Shoppingcart.dbo.Product (
	Id varchar(7) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	ProductName nvarchar(250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	ShortDescription nvarchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	LongDescription nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Stock int NULL,
	Price decimal(18,4) NOT NULL,
	Origin varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CategoryId varchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Unlimited bit DEFAULT 0 NULL,
	Location varchar(250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	WarrantyPeriod int NULL,
	Status bit DEFAULT 1 NULL,
	CreateBy varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreateDate datetime NULL,
	ModifyBy varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ModifyDate datetime NULL,
	CONSTRAINT PK__Product__3214EC0768224804 PRIMARY KEY (Id),
	CONSTRAINT FK__Product__Categor__2116E6DF FOREIGN KEY (CategoryId) REFERENCES Shoppingcart.dbo.Category(Id)
);
ALTER TABLE Shoppingcart.dbo.Product WITH NOCHECK ADD CONSTRAINT Check_instock CHECK ([Unlimited]=(0) AND [Stock]>=(0) OR [Unlimited]=(1));


-- Shoppingcart.dbo.ProductPhoto definition

-- Drop table

-- DROP TABLE Shoppingcart.dbo.ProductPhoto;

CREATE TABLE Shoppingcart.dbo.ProductPhoto (
	Id uniqueidentifier NOT NULL,
	ProductId varchar(7) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Url varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	PublicId varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsMain bit NOT NULL,
	CreateBy varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreateDate datetime NULL,
	CONSTRAINT PK__ProductP__3214EC07494162F2 PRIMARY KEY (Id),
	CONSTRAINT FK__ProductPh__Produ__39E294A9 FOREIGN KEY (ProductId) REFERENCES Shoppingcart.dbo.Product(Id)
);


-- Shoppingcart.dbo.SalesOrder definition

-- Drop table

-- DROP TABLE Shoppingcart.dbo.SalesOrder;

CREATE TABLE Shoppingcart.dbo.SalesOrder (
	Id varchar(16) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	OrderDate datetime NOT NULL,
	AppUserId uniqueidentifier NULL,
	OrderStatusId int NULL,
	PaymentTypeId int NULL,
	DeliveryTypeId char(1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ProductId varchar(7) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Quantity int NOT NULL,
	Price decimal(18,4) NOT NULL,
	FirstName nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	LastName nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CompanyName nvarchar(250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Contry nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	StreetAddress nvarchar(250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	PostCode nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	City nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PhoneNumber nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	OrderNote nvarchar(250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ReceivedDate datetime NULL,
	Status bit DEFAULT 1 NULL,
	CreateBy varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreateDate datetime NULL,
	ModifyBy varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ModifyDate datetime NULL,
	OrderNumber int NULL,
	CONSTRAINT PK__SalesOrd__3214EC0736C3FFEB PRIMARY KEY (Id),
	CONSTRAINT FK__SalesOrde__AppUs__65C116E7 FOREIGN KEY (AppUserId) REFERENCES Shoppingcart.dbo.AppUser(Id),
	CONSTRAINT FK__SalesOrde__Deliv__689D8392 FOREIGN KEY (DeliveryTypeId) REFERENCES Shoppingcart.dbo.DeliveryType(Id),
	CONSTRAINT FK__SalesOrde__Order__66B53B20 FOREIGN KEY (OrderStatusId) REFERENCES Shoppingcart.dbo.OrderStatus(Id),
	CONSTRAINT FK__SalesOrde__Payme__67A95F59 FOREIGN KEY (PaymentTypeId) REFERENCES Shoppingcart.dbo.PaymentType(Id),
	CONSTRAINT FK__SalesOrde__Produ__6991A7CB FOREIGN KEY (ProductId) REFERENCES Shoppingcart.dbo.Product(Id)
);


-- Shoppingcart.dbo.TradeReturnRequest definition

-- Drop table

-- DROP TABLE Shoppingcart.dbo.TradeReturnRequest;

CREATE TABLE Shoppingcart.dbo.TradeReturnRequest (
	Id uniqueidentifier NOT NULL,
	AppUserId uniqueidentifier NOT NULL,
	SalesOrderId varchar(16) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	ProductId varchar(7) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Quantity int NOT NULL,
	Description varchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Type] int NOT NULL,
	RequestStatus int NOT NULL,
	Status bit DEFAULT 1 NULL,
	CreateBy varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreateDate datetime NULL,
	ModifyBy varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ModifyDate datetime NULL,
	CONSTRAINT PK__TradeRet__3214EC07294995D4 PRIMARY KEY (Id),
	CONSTRAINT FK__TradeRetu__AppUs__0AF29B96 FOREIGN KEY (AppUserId) REFERENCES Shoppingcart.dbo.AppUser(Id),
	CONSTRAINT FK__TradeRetu__Produ__0CDAE408 FOREIGN KEY (ProductId) REFERENCES Shoppingcart.dbo.Product(Id),
	CONSTRAINT FK__TradeRetu__Sales__0BE6BFCF FOREIGN KEY (SalesOrderId) REFERENCES Shoppingcart.dbo.SalesOrder(Id)
);


-- Shoppingcart.dbo.UserAddress definition

-- Drop table

-- DROP TABLE Shoppingcart.dbo.UserAddress;

CREATE TABLE Shoppingcart.dbo.UserAddress (
	Id uniqueidentifier NOT NULL,
	AppUserId uniqueidentifier NULL,
	FirstName nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	LastName nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CompanyName nvarchar(250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Contry nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	StreetAddress nvarchar(250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	PostCode nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	City nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PhoneNumber nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Status bit DEFAULT 1 NULL,
	CreateBy varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreateDate datetime NULL,
	ModifyBy varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ModifyDate datetime NULL,
	CONSTRAINT PK__UserAddr__3214EC0773693E08 PRIMARY KEY (Id),
	CONSTRAINT FK__UserAddre__AppUs__361203C5 FOREIGN KEY (AppUserId) REFERENCES Shoppingcart.dbo.AppUser(Id)
);


-- Shoppingcart.dbo.Delivery definition

-- Drop table

-- DROP TABLE Shoppingcart.dbo.Delivery;

CREATE TABLE Shoppingcart.dbo.Delivery (
	Id uniqueidentifier NOT NULL,
	SalesOrderId varchar(16) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	FullName nvarchar(250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CompanyName nvarchar(550) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Phone varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	SalesOrderAmount decimal(18,4) NULL,
	FeeAmount decimal(18,4) NULL,
	TotalAmount decimal(18,4) NULL,
	DeliveryDate datetime NOT NULL,
	DeliveryStatus int NOT NULL,
	Status bit DEFAULT 1 NULL,
	CreateBy varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreateDate datetime NULL,
	ModifyBy varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ModifyDate datetime NULL,
	CONSTRAINT PK__Delivery__3214EC075F8A8730 PRIMARY KEY (Id),
	CONSTRAINT FK__Delivery__SalesO__10AB74EC FOREIGN KEY (SalesOrderId) REFERENCES Shoppingcart.dbo.SalesOrder(Id)
);


--data
