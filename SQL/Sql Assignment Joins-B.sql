-- E-commerce Platform Schema

CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY,
    CustomerName VARCHAR(100),
    Email VARCHAR(100),
    RegistrationDate DATE
);

CREATE TABLE Products (
    ProductID INT PRIMARY KEY,
    ProductName VARCHAR(200),
    Price DECIMAL(10, 2),
    CategoryID INT
);

CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY,
    CategoryName VARCHAR(100)
);

CREATE TABLE Orders (
    OrderID INT PRIMARY KEY,
    CustomerID INT,
    OrderDate DATE,
    TotalAmount DECIMAL(10, 2),
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);

CREATE TABLE OrderDetails (
    OrderDetailID INT PRIMARY KEY,
    OrderID INT,
    ProductID INT,
    Quantity INT,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

-- Questions

-- 1. List all products with their category names, including products without a category.
select * from [Products] as a Left Join [Categories] as b
on a.[CategoryID] = b.[CategoryID] where a.[CategoryID] is null
-- 2. Display all customers and their order history, including customers who haven't placed any orders.
Select * from  [Customers] as A left join [Orders] as B on A.[CustomerID]=B.[CustomerID] order by A.[CustomerID]
-- 3. Show all categories and the products in each category, including categories without any products.
select * from [Categories] as A left join [Products] as B on A.[CategoryID]=B.[CategoryID]
-- 4. List all possible customer-product combinations, regardless of whether a purchase has occurred.
select * from Customers as A cross join Products as B 
-- 5. Display all orders with customer and product information, including orders where either the customer or product information is missing.
select * from Orders as O left join Customers as C on O.[CustomerID]=C.[CustomerID] left join OrderDetails as Od on O.[OrderID]=Od.[OrderID] left join Products as P on Od.[ProductID]=P.[ProductID]
-- 6. Show all products that have never been ordered, along with their category information.
select * from Products as P left join Categories as Ct on   P.CategoryID =Ct.CategoryID  left join OrderDetails as Od on P.[ProductID]=Od.[ProductID]   where Od.[OrderDetailID] is null
-- 7. List all customers who have placed orders in the last week, along with the products they've purchased.
SELECT * 
FROM [dbo].[Customers] AS C
JOIN [dbo].[Orders] AS O ON C.CustomerID = O.CustomerID
join [dbo].[OrderDetails] as Od on O.[OrderID]=Od.[OrderID]
join [dbo].[Products] as P on  Od.[ProductID]=P.[ProductID]
WHERE DATEDIFF(day, O.OrderDate, GETDATE()) >=7
-- 8. Display all categories with products priced over $100, including categories without such products.
select * from [dbo].[Products] as P right join [dbo].[Categories] as  Ct on P.[CategoryID]=Ct.[CategoryID] and P.[Price]>100

-- 9. Show all orders placed before 2023 and any associated product information.
select    O.[OrderID],O.[CustomerID],O.[OrderDate],O.[OrderDate],P.[ProductID],P.[ProductName],P.[Price],P.[CategoryID]from [dbo].[Orders] as O join [dbo].[OrderDetails] as Od on O.[OrderID]=od.[OrderID] JOIN [dbo].[Products] AS P on  Od.[ProductID]=P.[ProductID] where year([OrderDate])<2023
-- 10. List all possible category-customer combinations, regardless of whether the customer has purchased a product from that category.
select Ct.[CategoryID],Ct.[CategoryName],C.[CustomerID],C.[CustomerName],C.[Email],C.[RegistrationDate] from [dbo].[Categories] as Ct cross Join [dbo].[Customers] as C
