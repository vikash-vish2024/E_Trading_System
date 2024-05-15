Create Database E_TradingDB
use E_TradingDB
drop database E_TradingDB
 
--Database

----------------------This is the table for the Hint Questions--------
Create table Hints 
(Hint_Id  int Primary Key Identity ,
Questions NVarchar (100))
INSERT INTO Hints (Questions) VALUES 
('What is your Favourite Sport?'),
('In what city were you born?'),
('What is the name of your first pet?'),
('What is your favorite movie?'),
('What street did you grow up on?');
select * from Hints
----------------------This is the table for the Admin-----------------
Create table [Admin] 
(Admin_Id numeric(10) Primary key not null,
Admin_Email varchar(30) unique not null,
Admin_Name varchar(30) not null, 
[Password] varchar(20) unique not null,
Hint_Id int foreign key references Hints(Hint_Id),
[Hint_Answers] varchar (200) not null)
select * from Admin
INSERT INTO Admin (Admin_Id, Admin_Email, Admin_Name, [Password], Hint_Id, Hint_Answers)
VALUES (1111, 'vikashv@infinite.com', 'Vikash', 'Vikash@123', 1, 'Volleyball');
----------------------This is the table for the Customer ----------------
Create Table Customer 
([Customer_Id] numeric(10) Primary key,
[Customer_Name] varchar(30) not null,
[Customer_Email] varchar(30) unique not null,
Date_Of_Birth Date not null,
[Address] varchar(50) not null,
Balance float default 0,
Mobile_Number numeric(10),
[Password] varchar(20) not null,
[Hint_Id] int foreign key references Hints(Hint_Id),
[Hint_Answer] varchar(200) not null,
[Status] varchar(20))
 INSERT INTO Customer (Customer_Id, Customer_Name, Customer_Email, Date_Of_Birth, Address, Balance, Mobile_Number, [Password], Hint_Id, Hint_Answer, [Status])
VALUES 
(1, 'John Doe', 'john@example.com', '1990-01-01', '123 Main St', 100.50, 1234567890, 'password123', 1, 'Hint answer 1', 'Active'),
(2, 'Jane Smith', 'jane@example.com', '1985-05-15', '456 Oak Ave', 75.25, 9876543210, 'securepwd', 2, 'Hint answer 2', 'Active'),
(3, 'Alice Johnson', 'alice@example.com', '1998-11-30', '789 Elm St', 0, 5554443333, 'p@ssw0rd', 3, 'Hint answer 3', 'Active'),
(4, 'Bob Brown', 'bob@example.com', '1982-08-20', '321 Pine St', 50, 1112223333, '123456', 4, 'Hint answer 4', 'Inactive'),
(5, 'Emily Davis', 'emily@example.com', '1995-03-10', '654 Cedar Ave', 25.75, 9998887777, 'qwerty', 5, 'Hint answer 5', 'Active');
select * from Customer
----------------------This is the table for the User_Wallet ----------------
Create Table Wallet (
wallet_Id int primary key identity,
[Customer_Id] numeric(10) foreign key references Customer(Customer_Id),
[Date_of_Top_Up] date,
[Last_Top_Up] float,
[Total_Top_Up] float
)
 
----------------------This is the table for the Vendors ----------------
Create Table Vendors 
(Vendor_Id Numeric(10) Primary Key,
Vendor_Name varchar(20) not null,
Vendor_Email varchar(30) unique not null,
Mobile_Number Numeric(10) not null,
[Address] varchar(50) not  null,
Category varchar(40) not null,
Vendor_Age int not null,
[Passowrd] varchar(20) not null,
[Hint_Id] int foreign key references Hints(Hint_Id),
[Hint_Answer] nvarchar(50),
[Status] varchar(20))
 INSERT INTO Vendors (Vendor_Id, Vendor_Name, Vendor_Email, Mobile_Number, [Address], Category, Vendor_Age, [Passowrd], Hint_Id, Hint_Answer, [Status])
VALUES 
(1, 'Vendor1', 'vendor1@example.com', 1234567890, '123 Main St', 'Electronics', 35, 'password123', 1, 'Hint answer 1', 'Active'),
(2, 'Vendor2', 'vendor2@example.com', 9876543210, '456 Oak Ave', 'Clothing', 28, 'securepwd', 2, 'Hint answer 2', 'Active'),
(3, 'Vendor3', 'vendor3@example.com', 5554443333, '789 Elm St', 'Furniture', 42, 'p@ssw0rd', 3, 'Hint answer 3', 'Inactive'),
(4, 'Vendor4', 'vendor4@example.com', 1112223333, '321 Pine St', 'Grocery', 30, '123456', 4, 'Hint answer 4', 'Active'),
(5, 'Vendor5', 'vendor5@example.com', 9998887777, '654 Cedar Ave', 'Health & Beauty', 37, 'qwerty', 5, 'Hint answer 5', 'Active');
----------------------This is the table for the Orders ----------------
Create Table Orders
(Purchase_Id numeric(5) Primary key,
[Customer_Id] numeric(10) foreign key references Customer(Customer_Id),
Delivery_Date date,
[Order_Amount] float,
[Payment_Mode] varchar(20),
[Address] varchar(50),
[Status] varchar(30))
 

----------------------This is the table for the Order Details ----------------
Create Table Order_Details
(Order_Id numeric(3) primary key,
[Purchase_Id] numeric(5) foreign key references Orders(Purchase_Id),
Product_Name varchar(30),
Product_Price float)

----------------------This is the table for the Products ----------------
Create Table Products
(Product_Id numeric(10) primary key,
Vendor_Id numeric(10) foreign key references Vendors(Vendor_Id),
Product_Name varchar(40),
Brand varchar(30),
Color varchar(20),
Price float,
Available_Stock int,
[Status] varchar(25))
ALTER TABLE Products
ADD ImageFileName varchar(100);
alter table products add isdeleted varchar(20)
----------------------This is the Table for Bucket------------------
create Table BucketList(
Serial_Number int  identity,
Product_Id numeric(10) foreign key references Products(Product_Id),
Customer_Id numeric(10) foreign key references Customer(Customer_Id))

  
----------------------This is the table for the Order_Cancellation----------------
Create table Order_Cancellation
(Cancellation_Id numeric(5) Primary key,
Order_Id numeric(3) foreign key references Order_Details(Order_Id),
Cancellation_Date DateTime not null,
Refund_Amount numeric(8))
 
 
select * from Admin
Select * from Customer
select * from Vendors
select * from Products
select * from Order_Details
select * from Order_Cancellation
select * from Orders
select * from Wallet
select * from BucketList
select * from Hints
select * from Orders

CREATE PROCEDURE GetProductsByVendorId
    @VendorId DECIMAL
AS
BEGIN
    SELECT p.Product_Id,
           p.Product_Name,
           p.Brand,
           p.Color,
           p.Price,
           p.Available_Stock,
           p.Status,
           p.ImageFileName,
           p.isdeleted,
           v.Vendor_Name
    FROM Products p
    INNER JOIN Vendors v ON p.Vendor_Id = v.Vendor_Id
    WHERE p.Vendor_Id = @VendorId;
END;