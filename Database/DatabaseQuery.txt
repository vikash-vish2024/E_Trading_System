Create Database E_TradingDB
use E_TradingDB
drop database E_TradingDB
 
--Database

----------------------This is the table for the Hint Questions--------
Create table Hints 
(Hint_Id  int Primary Key Identity ,
Questions NVarchar (100))

----------------------This is the table for the Admin-----------------
Create table [Admin] 
(Admin_Id numeric(10) Primary key not null,
Admin_Email varchar(30) unique not null,
Admin_Name varchar(30) not null, 
[Password] varchar(20) unique not null,
Hint_Id int foreign key references Hints(Hint_Id),
[Hint_Answers] varchar (200) not null)

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