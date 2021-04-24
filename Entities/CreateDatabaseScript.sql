
CREATE DATABASE ShoppingCart
GO
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
	CONSTRAINT PK__SalesOrd__3214EC0736C3FFEB PRIMARY KEY (Id),
	CONSTRAINT FK__SalesOrde__AppUs__65C116E7 FOREIGN KEY (AppUserId) REFERENCES Shoppingcart.dbo.AppUser(Id),
	CONSTRAINT FK__SalesOrde__Deliv__689D8392 FOREIGN KEY (DeliveryTypeId) REFERENCES Shoppingcart.dbo.DeliveryType(Id),
	CONSTRAINT FK__SalesOrde__Order__66B53B20 FOREIGN KEY (OrderStatusId) REFERENCES Shoppingcart.dbo.OrderStatus(Id),
	CONSTRAINT FK__SalesOrde__Payme__67A95F59 FOREIGN KEY (PaymentTypeId) REFERENCES Shoppingcart.dbo.PaymentType(Id),
	CONSTRAINT FK__SalesOrde__Produ__6991A7CB FOREIGN KEY (ProductId) REFERENCES Shoppingcart.dbo.Product(Id)
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


INSERT INTO AppRole
(Id, Name, Description)
VALUES(N'1733C8FE-5767-4F45-9072-1E0DFE9A01EC', N'Member', N'Member');
INSERT INTO AppRole
(Id, Name, Description)
VALUES(N'2437E5F5-5F34-4FC5-ACD7-6F657312DCC8', N'Staff', N'Staff');
INSERT INTO AppRole
(Id, Name, Description)
VALUES(N'65BB97E7-23C0-4FF6-816E-FDD233C05FB1', N'Admin', N'Administrator');

INSERT INTO AppUser
(Id, UserName, Email, DateOfBirth, Gender, PasswordHash, PasswordSalt, Status, LastActive, CreateDate, ModifyDate, Version, CreateBy, ModifyBy)
VALUES(N'3F943FD4-485A-430E-ABC4-9EC5A17BAC48', N'admin', N'admin@gmail.com', '1990-01-01', 2, 0x9D0D5303EEF7B277E545B43039DDADAC7E486F71D22445B68AEAED2FAE3709593B577F678D96035B3359ABA8FE13B891CAA882804FF6F713203472368197F8BF, 0xE3BC8BF7E4CFC71832CB55A86789860979B3AD6EBBC60396560707FB7B88BA3CD31FFEBB4EDC2DBB14D5FB4212349D2408761CC69989E797C136B59EE9B2E666C8E62BAD60BEC5E8B5B70BA1258B7AD0664CF1835A5559AB9262CD01B6D8B974F19B8F109EDCFED00732BE4D180ADEC92FBBFBD0674323BC6675BE9F9910FB58, 1, '2021-04-14 14:01:06.070', '2021-04-14 14:01:06.070', '2021-04-14 19:47:03.720', 1, NULL, NULL);

INSERT INTO AppUserRole
(UserId, RoleId)
VALUES(N'3F943FD4-485A-430E-ABC4-9EC5A17BAC48', N'1733C8FE-5767-4F45-9072-1E0DFE9A01EC');
INSERT INTO AppUserRole
(UserId, RoleId)
VALUES(N'3F943FD4-485A-430E-ABC4-9EC5A17BAC48', N'2437E5F5-5F34-4FC5-ACD7-6F657312DCC8');
INSERT INTO AppUserRole
(UserId, RoleId)
VALUES(N'3F943FD4-485A-430E-ABC4-9EC5A17BAC48', N'65BB97E7-23C0-4FF6-816E-FDD233C05FB1');

-- Auto-generated SQL script #202104231836
INSERT INTO Shoppingcart.dbo.Category (Id,Name,Description)
	VALUES (N'01',N'Bags & Purses',N'We have a lovely selection of purses, pouches, card holders, cosmetic bags, day and evening bags. You can never have too many, right?!');
INSERT INTO Shoppingcart.dbo.Category (Id,Name,Description)
	VALUES (N'02',N'Sentiment Gifts',N'Sometimes we want to send a gift with a special message. We have a lovely range of gifts with a bit of sentiment and meaning behind them.');
INSERT INTO Shoppingcart.dbo.Category (Id,Name,Description)
	VALUES (N'03',N'Candles',N'Candles make such a nice gift, or as a wee treat for yourself. Check out some of our most popular ones here.');
INSERT INTO Shoppingcart.dbo.Category (Id,Name,Description)
	VALUES (N'04',N'Clothing',N'Pretty scarves and patterned socks – what’s not to love in this selection of clothing?');
INSERT INTO Shoppingcart.dbo.Category (Id,Name,Description)
	VALUES (N'AA',N'Greetings Cards',N'We all know how much it can mean to receive a card, so we’re very excited to bring you some of our best-selling card ranges to our online shop! There’s a great choice of cards here, covering a wide variety of occasions.');
INSERT INTO Shoppingcart.dbo.Category (Id,Name,Description)
	VALUES (N'A0',N'Homeware',N'Turning a house into a home is one of our favourite things to do, and we love sourcing special pieces to do just that! We hope you enjoy looking at this choice of homeware, which includes candles, diffusers, frames and coasters.');
INSERT INTO Shoppingcart.dbo.Category (Id,Name,Description)
	VALUES (N'A1',N'Jewellery',N'Looking to treat someone to some gorgeous new jewellery, or for that perfect piece to finish off your outfit? Look no further! Here at Spirito we have a beautiful range of sterling silver and costume jewellery, to suit all different styles and budgets. We stock well-known brands such as Joma Jewellery, Pilgrim and Ed Blad, as well as sourcing more unique pieces. We also have several ranges which have been designed and handmade in Scotland.');
INSERT INTO Shoppingcart.dbo.Category (Id,Name,Description)
	VALUES (N'A2',N'Celebrations & Gift Bags',N'Celebrate the good times with a little help from SPiRiTO! We have a lovely choice of gifts for special occasions such as anniversaries, birthdays, retirements, engagements and weddings. We also have some party decorations and cake candles, perfect for making a memorable day!');
INSERT INTO Shoppingcart.dbo.Category (Id,Name,Description)
	VALUES (N'A3',N'Accessories',N'Every girl needs some accessories in her life! We love this range of jewellery boxes, keyrings and bag charms, perfect for adding a little something extra to your day.');


INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0500001', N'Mini Pastel Vases – blue', N'https://spiritogifts.com/product/mini-pastel-vases-blue/', N'This gorgeous set of 4 stoneware vases comes in cool blue shades. They’re perfect for displaying a few simple stems of your favourite flowers. Each vase is individual and has a hand-made and hand-painted feel to them. The vases range in height, from 4.5 cm to 8 cm, and come boxed.', 87, 17.9900, N'00001', N'05', 0, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0500002', N'Cone Alarm Clock – Grey', N'https://spiritogifts.com/product/cone-alarm-clock-grey/', N'A sleek grey alarm clock in a modern clean design, so waking up can be slightly more luxurious! Perfect for a housewarming present. Dimensions are: 10 x 5 cm. It takes 1 AA battery, not included.', 29, 15.9900, N'00002', N'05', 0, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0500003', N'Globe Alarm Clock – Blue', N'https://spiritogifts.com/product/globe-alarm-clock-blue/', N'This gorgeous blue alarm clock would make the perfect addition to bedside tables. Gold hardware compliments the deep blue colour, creating a classy and timeless feel. Dimensions are: 9.5 x 10.5 x 7cm. It takes 1 AA battery, not included.', 41, 18.0000, N'00003', N'05', 0, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0500004', N'Lure Alarm Clock – Sand Brown', N'https://spiritogifts.com/product/lure-alarm-clock-sand-brown/', N'An on-trend neutral tone alarm clock with a simple design, bold gold hardware and white hands. The gorgeous earthy tones will compliment almost any interior. Dimensions are 11 x 3 cm. It takes 1 AA battery, not included.', 17, 18.9900, N'00004', N'05', 0, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0500005', N'Pink Alarm Clock – Tinge', N'https://spiritogifts.com/product/pink-alarm-clock-tinge/', N'A steel, lightweight alarm clock in a gorgeous soft pink colour – perfect for brightening up a work-from-home desk. Dimensions are: 9 x 3 cm. It takes 1 AA battery, not included.', 33, 14.9900, N'00005', N'05', 0, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0500006', N'Paper Fan', N'https://spiritogifts.com/product/paper-fan/', N'A paper fan to add a touch of summer to your home. Combining a cool lilac with a warm orange, this statement piece from Broste would look gorgeous hanging on a wall. D34cm, H45cm', 56, 14.0000, N'00006', N'05', 0, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0600007', N'Aralia leaf necklace', N'https://spiritogifts.com/product/aralia-leaf-necklace/', N'A pretty Aralia leaf necklace, inspired by the jungle paintings of Henri Rousseau. This necklace is perfect for everyday wear, with its simple yet elegant details. Necklace length 43 cm, leaf height 1.2 cm, made from sterling silver.', 99, 31.0000, N'00007', N'06', 0, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0600008', N'Rainbow necklace', N'https://spiritogifts.com/product/rainbow-necklace/', N'This pretty necklace features a rainbow peeking out from behind the clouds – a reminder of hope after the storm! Made from sterling silver and 18ct gold, the necklace is 43cm in length and the rainbow measures 20 x 9mm.', 68, 43.0000, N'00008', N'06', 0, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0600009', N'Sausage Dog Bracelet', N'https://spiritogifts.com/product/sausage-dog-bracelet/', N'Who doesn’t love a little sausage dog?! Made from sterling silver, this delicate bracelet features a sausage dog with a tiny 18ct gold collar and moonstone. The bracelet measures 17cm with a 2cm extension chain, and the dog is 1 x 1cm.', 90, 43.0000, N'00009', N'06', 0, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0600010', N'Butterfly necklace', N'https://spiritogifts.com/product/butterfly-necklace/', N'How pretty is this tiny little butterfly pendant? Paired with a coral flower, this delicate necklace is made from sterling silver and plated with 22ct gold. The necklace length is 43cm, the butterfly measures 6mm across and 8mm height.', 49, 45.0000, N'00010', N'06', 0, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0600011', N'Bee Stud Earrings', N'https://spiritogifts.com/product/bee-stud-earrings/', N'Beautifully detailed little bee earrings, this gorgeous pair are made from sterling silver and measure 10 x 6mm.', 46, 10.0000, N'00011', N'06', 0, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0600012', N'Wire Wrapped Necklace', N'https://spiritogifts.com/product/wire-wrapped-necklace/', N'We love this contemporary design with a ball of wrapped silver wire hanging beautifully from a silver chain. Designed and made in Edinburgh, this sterling silver necklace measures 46cm/ 18″ with a 12mm ball.', 51, 35.0000, N'00012', N'06', 0, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0600013', N'Mia Garnet Earrings', N'https://spiritogifts.com/product/mia-garnet-earrings/', N'These beautiful sterling silver earrings feature garnet stone chips that playfully sparkle in the light.  Handmade by Glasgow based jeweller RR Designs. Sterling Silver, Garnet, Length approx. 50mm', 64, 20.0000, N'00013', N'06', 0, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0600014', N'Moonstone Necklace', N'https://spiritogifts.com/product/moonstone-necklace/', N'This beautifully delicate necklace features a sterling silver diamond cut ring with a tiny semi-precious moonstone gem encased in 18ct gold. The dainty pendant can also be taken off the chain and worn as a ring! Made from sterling silver and 18ct gold plated sterling silver, the chain is adjustable from 41 – 46cm (16″ – 18″) and the ring is 18mm in diameter.', 51, 40.0000, N'00014', N'06', 0, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0600015', N'Silver infinity bangle', N'https://spiritogifts.com/product/silver-infinity-bangle/', N'A pretty sterling silver bangle with an infinity symbol. Perfect for saying how much someone means to you. The circumference of the bangle is approximately 18 cm.', 30, 30.0000, N'00015', N'06', 0, N'US-Texas', 180, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0600016', N'Silver chain bracelet with hearts', N'https://spiritogifts.com/product/silver-chain-bracelet-with-hearts/', N'We love this delicate sterling silver bracelet with silver and rose gold heart charms. The fine chain of the bracelet sits prettily on the wrist, with hearts dangling from it. The bracelet measures 17 cm, with a 2.5 cm extender chain.', 55, 32.0000, N'00016', N'06', 0, N'US-Texas', 180, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0600017', N'Rose Gold Circle Sparkle earrings', N'https://spiritogifts.com/product/rose-gold-circle-sparkle-earrings/', N'These beautiful open circle rose gold stud earrings are adorned with tiny cubic zirconia stones for an eye catching sparkle. Made from rose gold plated sterling silver, the earrings measure 1cm in diameter and have butterfly backings. Also available in silver.', 80, 20.0000, N'00017', N'06', 0, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0700018', N'Balloons medium gift bag', N'https://spiritogifts.com/product/balloons-medium-gift-bag/', N'A pretty gift bag with balloons and confetti, perfect for giving birthday gifts in! It has gold foil details, and comes with a confetti circle tag and pale pink grosgrain ribbon handles.

 

Measures 220 x 220 x 80 mm.', 21, 2.9900, N'00018', N'07', 1, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0700019', N'Flower medium gift bag', N'https://spiritogifts.com/product/flower-medium-gift-bag/', N'A pretty gift bag with a colourful floral design, perfect for giving gifts in! It has gold foil details, and comes with a patterned circle tag and pale pink grosgrain ribbon handles.

 

Measures 220 x 220 x 80 mm.', 59, 2.9900, N'00019', N'07', 1, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0700020', N'Rainbow Sprinkles medium gift bag', N'https://spiritogifts.com/product/rainbow-sprinkles-medium-gift-bag/', N'Today is all about you! We love this bright and cheerful gift bag, with a pretty rainbow tag. It comes with grey grosgrain ribbon handles.

 

Measures 220 x 220 x 80 mm.', 59, 2.9900, N'00020', N'07', 1, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0700021', N'Leopard Print medium gift bag', N'https://spiritogifts.com/product/leopard-print-medium-gift-bag/', N'Fabulous You! This is a bold and colourful gift bag, with contrasting bright pink and orange side and leopard print pattern. It has gold foil details, a circular gift tag and navy blue grosgrain ribbon handles.

 

Measures 220 x 220 x 80 mm.', 100, 2.9900, N'00021', N'07', 1, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0700022', N'Stripe Blue Baby medium gift bag', N'https://spiritogifts.com/product/blue-stripe-baby-medium-gift-bag/', N'A gorgeous blue stripe gift bag, with the message ‘It’s a baby boy’ on the front. It comes with a patterned star shaped tag and pale blue grosgrain ribbon handles.

 

Measures 220 x 220 x 80 mm.', 25, 2.9900, N'00022', N'07', 1, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0800023', N'Large grey jewellery box – Be Happy Be Bright', N'https://spiritogifts.com/product/large-grey-jewellery-box-be-happy-be-bright/', N'This gorgeous grey large jewellery box from Katie Loxton has been designed to keep every little trinket and treasure safe and sound! The minimalist design features a sweet engraved gold sentiment ‘Be Happy Be Bright’ to the lid and a gold-tone hammered disc embellishment to create the most special and luxe finishing touch.

Made from PU vegan leather.

Measures 25 x 18 x 5 cm.', 32, 39.9900, N'00023', N'08', 0, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0800024', N'Metallic stars oval travel jewellery box', N'https://spiritogifts.com/product/metallic-stars-oval-travel-jewellery-box/', N'This pretty box is perfect for storing your most loved bits of jewellery! It comes in a gorgeous faux leather grey colour, with scattered gold stars. Open up the gold zip to reveal the pale purple velveteen interior with two handy compartments, a section for rings and studs, and a pocket in the lid. Gold lettering reads ‘take me with you’ inside. This is perfect for storing your jewellery at home, or for popping in your bags for a trip away.

Made from vegan friendly PU leather. Measures width 14.5 cm x height 5 cm x depth 9.5 cm.', 65, 20.0000, N'00024', N'08', 0, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0800025', N'Mini round stars travel jewellery case – pink', N'https://spiritogifts.com/product/mini-round-stars-travel-jewellery-case-pink/', N'This pretty and practical round case features 2 small sections inside, ring rolls and a pocket in the lid – perfect for storing your favourite pieces of jewellery! Made from vegan-friendly PU leather, the pretty pink outer with metallic stars opens up to reveal a beautiful inner with gold lettering ‘take me with you’ inside the lid. It closes securely with a gold zip and has a handy little strap for carrying.

It measures 8.7 cm in diameter, and is  3.8 cm in height.', 18, 16.0000, N'00025', N'08', 0, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0800026', N'Mum in a Million – boxed keyring', N'https://spiritogifts.com/product/mum-in-a-million-boxed-keyring/', N'Gorgeous new keyrings from Katie Loxton, these make a perfect gift in their beautiful boxes. This one is perfect for giving to a special mum, with its heart shape, featuring the sweet engraved gold sentiment ‘Mum in a million’ on the front and finished with the Katie Loxton signature gold-tone hardware for a luxe look.

Measures 6 x 7 cm. Made from PU vegan leather.', 80, 14.9900, N'00026', N'08', 0, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0800027', N'Wonderful Mum Heart Keyring', N'https://spiritogifts.com/product/wonderful-mum-heart-keyring/', N'This beautiful keyring from Katie Loxton is the perfect gift for your wonderful mum. The shimmering silver heart shaped design is made from 100% PU- vegan leather and embellished with delicate golden details.

Net Weight 30g
Size: W:7 cm x H: 6 cm
Care instructions: Wipe with damp cloth.', 85, 11.9900, N'00027', N'08', 0, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0800028', N'Pink and Orange Heart Keyring', N'https://spiritogifts.com/product/pink-and-orange-heart-keyring/', N'Add a little playful spirit to your day with this gorgeous heart keyring from Katie Loxton. Designed to add a chic touch to your keychain or handbag, this luxe design is finished with a large hot pink heart with a contrast colour pop mini orange heart on top! Created to capture the happiness of bright and beautiful days.

 

Made from 100% PU – Vegan Leather. Measures 8 x 9 cm.', 97, 12.9900, N'00028', N'08', 0, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0800029', N'Love Heart Keyring', N'https://spiritogifts.com/product/love-heart-keyring/', N'Handmade by Lancaster & Gibbings, this cast pewter keyring features a small raised heart on a rectangle shape. It comes boxed, and the keyring (excluding fittings) measures 40 x 29 x 3mm.', 10, 14.0000, N'00029', N'08', 1, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0900030', N'Happy Birthday may all your dreams come true', N'https://spiritogifts.com/product/happy-birthday-may-all-your-dreams-come-true/', N'We love this beautiful birthday card from Five Dollar Shake! Hand embellished with French sequins and genuine Swarovski crystals. 16×16 cm. Blank inside.', 26, 4.9900, N'00030', N'09', 1, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0900031', N'Happy Birthday some girls just sparkle', N'https://spiritogifts.com/product/happy-birthday-some-girls-just-sparkle/', N'We love this beautiful birthday card from Five Dollar Shake! Hand embellished with French sequins and genuine Swarovski crystals. 16×16 cm. Blank inside.', 100, 4.9900, N'00031', N'09', 1, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0900032', N'With Love on Your Birthday – Some Girls Just Sparkle', N'https://spiritogifts.com/product/with-love-on-your-birthday-some-girls-just-sparkle/', N'A stylish large card from Five Dollar Shake, for the sparkling birthday girl! Hand embellished with French sequins and genuine Swarovski crystals. 21 x 21 cm. Blank inside.', 4, 6.9900, N'00032', N'09', 1, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0900033', N'Happy Birthday – Dalmatian', N'https://spiritogifts.com/product/happy-birthday-dalmatian/', N'A beautiful card with hand-drawn illustrations combined with photographic details and silver foiled text. Hand finished with gorgeous sparkly gems. Measures 13 x 13 cm, blank inside for your own message.', 77, 3.0000, N'00033', N'09', 1, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0900034', N'Amazing Birthday – cake', N'https://spiritogifts.com/product/amazing-birthday-cake/', N'A bright and cheerful cake to wish someone a happy birthday! Finished with gold foil and embossing details. Blank inside for your own message. H 13.5 x W 12.5 cm.', 27, 3.0000, N'00034', N'09', 1, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0900035', N'Sending hugs', N'https://spiritogifts.com/product/sending-hugs-2/', N'A lovely card with a pretty rainbow to send someone some hugs in a time when we can’t hug in person! Blank inside for your own message. Measures 140 x 152mm.', 1, 2.9900, N'00035', N'09', 1, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);
INSERT INTO Product
(Id, ProductName, ShortDescription, LongDescription, Stock, Price, Origin, CategoryId, Unlimited, Location, WarrantyPeriod, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(N'0900036', N'70 and wonderful!', N'https://spiritogifts.com/product/70-and-wonderful/', N'Such a pretty design on this card for a special 70th birthday. Hand embellished with French sequins and genuine Swarovski crystals. 16 x 16 cm. Blank inside.', 10, 4.9900, N'00036', N'09', 1, N'US-Texas', NULL, 1, N'admin', '2021-04-24 00:00:00.000', NULL, NULL);


--photo
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'83C55635-68FE-4C2C-8C94-06A850EBE1EA', N'0600015', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251803/kbwcez0kqk19omkxfrye.jpg', N'kbwcez0kqk19omkxfrye', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'84B43900-6718-4D40-B18B-12BC9752AD43', N'0900031', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619252647/wiga7w0dzjvolr6jbvoe.jpg', N'wiga7w0dzjvolr6jbvoe', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'978FABB3-B427-4A92-9B49-14FB876EB495', N'0500004', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619250887/natsl0q5marbzvntdi5r.jpg', N'natsl0q5marbzvntdi5r', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'E09A211B-4B7C-412A-9206-1A14DD0F04C8', N'0800027', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619252469/it5ezmdxn1cd3e8azsyr.jpg', N'it5ezmdxn1cd3e8azsyr', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'96D0C43D-9D09-4D53-BB29-1CC5BDF06F81', N'0500004', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619250888/jbvxpcuaxkfufbi6pe29.jpg', N'jbvxpcuaxkfufbi6pe29', 0, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'B0F7DB4F-DBF2-497D-B597-1F21EFBDCD38', N'0600010', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251530/cdkvxog3i6qbziiobgoq.jpg', N'cdkvxog3i6qbziiobgoq', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'75D8ED2F-38FA-494C-B339-204BA42267FF', N'0900036', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619252799/g5krdji7ma2jnofvgigq.jpg', N'g5krdji7ma2jnofvgigq', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'AA1C6DA1-16A3-45F4-BD7B-2103CA5A950F', N'0700021', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619252026/kernodsppijcbrlsh44k.jpg', N'kernodsppijcbrlsh44k', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'8DB4A442-7E21-4275-B984-2740F57D524C', N'0500003', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619250795/ycuqfoqu3ufvxggqpwji.jpg', N'ycuqfoqu3ufvxggqpwji', 0, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'9DCC0305-04DE-4E65-B28F-2750569A73F8', N'0600013', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251717/qwxqihlmifbnfpubzr2v.jpg', N'qwxqihlmifbnfpubzr2v', 0, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'9EE3E83D-6391-479E-B0CE-2A248788098A', N'0800027', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619252470/u68ppuivmiimqp2vzj6t.jpg', N'u68ppuivmiimqp2vzj6t', 0, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'AA4668E4-8F21-4B57-9F2E-32E9FA872253', N'0600013', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251716/vcoguj8dave396buvfur.jpg', N'vcoguj8dave396buvfur', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'DCDBAB43-BECE-4188-9ECB-346080B4E1BB', N'0600017', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251888/tvjzi5krdlv7c9wffyje.jpg', N'tvjzi5krdlv7c9wffyje', 0, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'E27B9A2B-8E83-4C95-A327-3709DF53F797', N'0600017', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251886/tjuq7ifiagq8sih2ncs7.jpg', N'tjuq7ifiagq8sih2ncs7', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'BFF6EA22-4B4A-4D66-A3A3-3E19D4650D1E', N'0600014', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251764/ma7ucv9fsrskq5durwk4.jpg', N'ma7ucv9fsrskq5durwk4', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'98F0D568-11F2-4021-814A-429B26093766', N'0600010', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251531/qht3cf5mpgqvq5ln0fuh.jpg', N'qht3cf5mpgqvq5ln0fuh', 0, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'67236DBC-419C-47D7-B09D-442FCCE0F0E6', N'0600011', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251564/ph75tps2b1nizufsggyq.jpg', N'ph75tps2b1nizufsggyq', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'3E25660B-F27C-4503-AE77-457D2A02CC85', N'0600007', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251268/dbur8f81lzch1jkgxqet.jpg', N'dbur8f81lzch1jkgxqet', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'54C77BE3-4DBB-465D-B7EC-4637DA1540F9', N'0800029', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619252537/dj91fkhqgafy6rybxiux.jpg', N'dj91fkhqgafy6rybxiux', 0, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'07FF9547-7116-4102-92F5-46CC34ACC8B5', N'0600009', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251421/lkjc8ijftcmz6d19szom.jpg', N'lkjc8ijftcmz6d19szom', 0, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'84B7D823-9215-4F28-B6B1-48B58A5AFE2C', N'0600012', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251625/vxceip136rmpbvsmqwjn.jpg', N'vxceip136rmpbvsmqwjn', 0, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'EB752CD4-19E4-4779-B075-515FC64E13EA', N'0600008', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251350/k3dbnj0snmivky39k93l.jpg', N'k3dbnj0snmivky39k93l', 0, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'518DD5E2-8EB4-49D9-BD48-531C42561579', N'0800029', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619252537/pgkfja4voahzurkmv7gz.jpg', N'pgkfja4voahzurkmv7gz', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'16A8B606-F16A-4D01-AF42-5619103BBC9C', N'0600017', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251890/brtoeymqydng8k5vsnd3.jpg', N'brtoeymqydng8k5vsnd3', 0, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'73B86B22-EB94-4FEE-9509-5C503E08AA4F', N'0500003', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619250793/ym0q9c2myh5tlv7s8qf8.jpg', N'ym0q9c2myh5tlv7s8qf8', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'6B0CF0EB-EF69-4E2B-930C-5F1A43FB2FC6', N'0600012', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251624/iyozzqxckwmxorsqcnvl.jpg', N'iyozzqxckwmxorsqcnvl', 0, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'68DE64D1-0630-441B-B151-60645C1FA3A8', N'0900030', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619252611/r7bji4qjoysrtdmbf0jd.jpg', N'r7bji4qjoysrtdmbf0jd', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'172C614A-4F2C-43C7-9CAC-61562D469F19', N'0600012', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251624/snfqrdyfqmkarmxwh5my.jpg', N'snfqrdyfqmkarmxwh5my', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'30E3C3AC-2A36-4687-BDFF-63547385FE14', N'0500002', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619250735/j1gk5ondux5e45be68k5.jpg', N'j1gk5ondux5e45be68k5', 0, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'7CFF83EA-787B-4A88-9409-69E367CE8DE2', N'0700020', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251995/zqpo6hnhcg9asasnvwxs.jpg', N'zqpo6hnhcg9asasnvwxs', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'F8F760F0-BD41-460F-B614-6A80ADED68EF', N'0800023', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619252222/j27javz9y3dom5fmj8zu.jpg', N'j27javz9y3dom5fmj8zu', 0, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'F08AE4C6-41A9-46DA-AF4A-6E55AB16EF7E', N'0800025', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619252316/sj4gnfpxiso3x6jl9sxp.jpg', N'sj4gnfpxiso3x6jl9sxp', 0, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'575EC9A5-B405-4FD6-8B39-6FD2829ECE69', N'0800025', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619252315/xzmddshqbsxc7j1dzicz.jpg', N'xzmddshqbsxc7j1dzicz', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'6F6CE407-1DFC-48E0-B60F-7029FABB674D', N'0600008', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251349/auowa1gtfnafy0pd9jyc.jpg', N'auowa1gtfnafy0pd9jyc', 0, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'B9D0E692-4E9C-4DB9-B0EB-7172DFE10F08', N'0500006', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251005/kku1zexjdnrra26btx4a.jpg', N'kku1zexjdnrra26btx4a', 0, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'80B9E53D-A051-428B-A327-74D4B8469366', N'0800024', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619252270/wtam2g1dfv8r0buioafu.jpg', N'wtam2g1dfv8r0buioafu', 0, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'2EF9A4DE-7D77-4825-95A0-74F63718D8E3', N'0900035', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619252772/ga7mgvn67etc5vsemu05.jpg', N'ga7mgvn67etc5vsemu05', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'74623FB7-5A41-48C7-BE27-776D4DD2B583', N'0800028', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619252501/ytxbcnnfst3pgribjzeb.jpg', N'ytxbcnnfst3pgribjzeb', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'3F75191F-8D6E-4374-87A2-7FA5797E902C', N'0600009', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251419/yhnlawsuholy03cju7as.jpg', N'yhnlawsuholy03cju7as', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'98DB6F97-778E-4706-A6EB-84075DED4605', N'0600016', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251828/yxobnigvlye2mxw2eqm2.jpg', N'yxobnigvlye2mxw2eqm2', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'81FFFBED-935F-48C1-983D-8DA7EB241B93', N'0800026', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619252372/kfvokprc0g0t3mukg8hg.jpg', N'kfvokprc0g0t3mukg8hg', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'A3BBFE1C-3260-4B2C-89D2-9AB633FB35A8', N'0900033', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619252698/mf4ss5gmknbfsi2gjtsr.jpg', N'mf4ss5gmknbfsi2gjtsr', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'4F17D392-D16E-4C8C-B0B1-9EF472DC9600', N'0800023', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619252221/kokylf21wcstoxibvetl.jpg', N'kokylf21wcstoxibvetl', 0, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'974546E1-D280-4282-B61C-ABDE827DFE9F', N'0700022', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619252113/v1phr1vvsm2ay7rls1gn.jpg', N'v1phr1vvsm2ay7rls1gn', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'C60A5D87-369F-425F-9CC7-B2FE9A867F44', N'0600010', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251532/ahjini5vmjexgkyhjray.jpg', N'ahjini5vmjexgkyhjray', 0, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'4E6F4FE4-B084-41F0-905A-B5B84666DC0C', N'0500001', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619250362/wlkncjre2oagwgoezbsq.jpg', N'wlkncjre2oagwgoezbsq', 0, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'F138D27B-223A-42F2-AE5E-C788A7B9C18D', N'0900032', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619252676/fq0wf6z7mcmwzt9oysch.jpg', N'fq0wf6z7mcmwzt9oysch', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'295F1393-1658-46CF-80B1-CBE806997A91', N'0700018', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251929/s2dpdtonpey2u8gbocve.jpg', N's2dpdtonpey2u8gbocve', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'3CDF46C4-F38C-4BB8-8D95-D56840E24F39', N'0600009', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251420/txeavidtj6ovzfgbsfw7.jpg', N'txeavidtj6ovzfgbsfw7', 0, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'A7C45A5D-F9A0-4C63-BD4E-D89547E489BE', N'0500005', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619250941/trfatprvaosstvevtddc.jpg', N'trfatprvaosstvevtddc', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'DE191137-8D37-44B4-A3E9-D8B2CC9B92E7', N'0500005', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619250943/czhs3icykkipfolt4mop.jpg', N'czhs3icykkipfolt4mop', 0, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'471CE78A-E19A-491C-9915-DC3AC15F9CDE', N'0800024', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619252268/mdj9revujsaohiiwbpjt.jpg', N'mdj9revujsaohiiwbpjt', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'875235A4-46D4-4087-89BD-E401817BDC10', N'0600013', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251721/ts3jdfvzq6uqqupmcge1.jpg', N'ts3jdfvzq6uqqupmcge1', 0, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'A40C504E-BCDA-41F7-B35C-E5FD5D8D9777', N'0600013', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251722/eqynl5xf6hxxgqyyiofh.jpg', N'eqynl5xf6hxxgqyyiofh', 0, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'B107BE83-BF8A-4A74-ABA9-E65930EED0FE', N'0600013', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251719/efevrdwmyyi5lll5o8ro.jpg', N'efevrdwmyyi5lll5o8ro', 0, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'28C48857-81DD-427C-B798-E99C7ED4C65F', N'0500006', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251003/qxjslx5shrks0gvxsktk.jpg', N'qxjslx5shrks0gvxsktk', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'8CF8E7CC-E816-4AD7-A425-EB92AA9E945E', N'0600014', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251765/bx931gufdpxvawcjloon.jpg', N'bx931gufdpxvawcjloon', 0, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'F5D79097-A8C4-4F8C-8A8C-ECE1728553BB', N'0700019', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251952/d13lyptrduitewqviwel.jpg', N'd13lyptrduitewqviwel', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'641CD4F9-22ED-4BD5-9E04-ED54334621C3', N'0800023', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619252220/sli5jne1ssjn50u89jue.jpg', N'sli5jne1ssjn50u89jue', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'1FB92F52-578F-4B0C-845F-F0E99BC663AA', N'0500001', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619250360/gchljosfo01w8bndqscy.jpg', N'gchljosfo01w8bndqscy', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'3DC201F0-E21B-4415-9CA5-F1B92AF49946', N'0600008', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619251348/np7m8b7zpt6w9dhwsmfq.jpg', N'np7m8b7zpt6w9dhwsmfq', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'0685554D-67D5-4B60-8230-F5F5BA3A0839', N'0900034', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619252745/yz9qizselyyzitwstw59.jpg', N'yz9qizselyyzitwstw59', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'E61219E9-AD40-4BFB-BE86-F60CB2BA6A52', N'0500002', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619250733/x6gnxzdexflzssvd8esa.jpg', N'x6gnxzdexflzssvd8esa', 1, N'admin', '2021-04-24 00:00:00.000');
INSERT INTO ProductPhoto
(Id, ProductId, Url, PublicId, IsMain, CreateBy, CreateDate)
VALUES(N'A5E76BAF-9664-47FD-889F-FC52C0B7CDDB', N'0800026', N'https://res.cloudinary.com/dzq5xdqac/image/upload/v1619252373/tdwrsm4ajppniyst3cc6.jpg', N'tdwrsm4ajppniyst3cc6', 0, N'admin', '2021-04-24 00:00:00.000');

--PaymentType
INSERT INTO PaymentType
(Id, PaymentTypeName, Description, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(1, N'Paypal', NULL, 1, NULL, NULL, NULL, NULL);
INSERT INTO PaymentType
(Id, PaymentTypeName, Description, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(2, N'Credit Cards', NULL, 1, NULL, NULL, NULL, NULL);
INSERT INTO PaymentType
(Id, PaymentTypeName, Description, Status, CreateBy, CreateDate, ModifyBy, ModifyDate)
VALUES(3, N'Cash', NULL, 1, NULL, NULL, NULL, NULL);

--delivery type
INSERT INTO DeliveryType
(Id, Name, Description, Status, Fee)
VALUES(N'1', N'First class delivery', N'Fast delivery in 1 - 2 days', 1, 6.95);
INSERT INTO DeliveryType
(Id, Name, Description, Status, Fee)
VALUES(N'2', N'Second class delivery', N'Delivery nomal in about 1 week', 1, 3.95);
INSERT INTO DeliveryType
(Id, Name, Description, Status, Fee)
VALUES(N'3', N'Self-Payment', N'Self-payment with shipper', 1, 0.00);


-- CREATE TABLE Shoppingcart.dbo.PaidDetail (
-- 	Id uniqueidentifier NOT NULL,
-- 	SalesOrderId uniqueidentifier NULL,
-- 	TotalAmount decimal(18,4) NOT NULL,
-- 	PaidAmount decimal(18,4) NOT NULL,
-- 	PaidDate datetime NULL,
-- 	FullName varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
-- 	SerialNumber varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
-- 	Description varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
-- 	CONSTRAINT PK__PaidDeta__3214EC07391A6408 PRIMARY KEY (Id),
-- 	CONSTRAINT FK__PaidDetai__Sales__2B947552 FOREIGN KEY (SalesOrderId) REFERENCES Shoppingcart.dbo.SalesOrder(Id)
-- );

-- CREATE TABLE Shoppingcart.dbo.ProductReturnRequest (
-- 	Id uniqueidentifier NOT NULL,
-- 	AppUserId uniqueidentifier NULL,
-- 	SalesOrderId uniqueidentifier NULL,
-- 	Description varchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
-- 	[Type] int NOT NULL,
-- 	RequestStatus int NOT NULL,
-- 	Status bit DEFAULT 1 NULL,
-- 	CreateBy varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
-- 	CreateDate datetime NULL,
-- 	ModifyBy varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
-- 	ModifyDate datetime NULL,
-- 	CONSTRAINT PK__ProductR__3214EC0799405751 PRIMARY KEY (Id),
-- 	CONSTRAINT FK__ProductRe__AppUs__3CBF0154 FOREIGN KEY (AppUserId) REFERENCES Shoppingcart.dbo.AppUser(Id),
-- 	CONSTRAINT FK__ProductRe__Sales__3DB3258D FOREIGN KEY (SalesOrderId) REFERENCES Shoppingcart.dbo.SalesOrder(Id)
-- );


-- CREATE TABLE Shoppingcart.dbo.Delivery (
-- 	Id uniqueidentifier NOT NULL,
-- 	SalesOrderId uniqueidentifier NULL,
-- 	FullName nvarchar(250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
-- 	CompanyName nvarchar(550) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
-- 	Phone varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
-- 	SalesOrderAmount decimal(18,4) NULL,
-- 	FeeAmount decimal(18,4) NULL,
-- 	TotalAmount decimal(18,4) NULL,
-- 	DeliveryDate datetime NOT NULL,
-- 	DeliveryStatus int NOT NULL,
-- 	Status bit DEFAULT 1 NULL,
-- 	CreateBy varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
-- 	CreateDate datetime NULL,
-- 	ModifyBy varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
-- 	ModifyDate datetime NULL,
-- 	CONSTRAINT PK__Delivery__3214EC074CD27A84 PRIMARY KEY (Id),
-- 	CONSTRAINT FK__Delivery__SalesO__324172E1 FOREIGN KEY (SalesOrderId) REFERENCES Shoppingcart.dbo.SalesOrder(Id)
-- );
