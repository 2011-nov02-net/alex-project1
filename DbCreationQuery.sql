
DROP TABLE IF EXISTS CustomerTable
DROP TABLE IF EXISTS ProductTable
DROP TABLE IF EXISTS StoreTable
DROP TABLE IF EXISTS Inventory
DROP TABLE IF EXISTS OrderDetails
DROP TABLE IF EXISTS OrderProduct

CREATE TABLE StoreTable (
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(99) NOT NULL,
)


CREATE TABLE CustomerTable (
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(99) NOT NULL,
	LastName NVARCHAR(99) NOT NULL,
	Phone NVARCHAR(99)
)


CREATE TABLE ProductTable (
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(99) NOT NULL,
	Price MONEY NOT NULL CHECK (Price > 0),
)


CREATE TABLE Inventory (
	LocationId INT NOT NULL FOREIGN KEY REFERENCES StoreTable (Id),
	ProductId INT NOT NULL FOREIGN KEY REFERENCES ProductTable (Id),
	Stock INT NOT NULL CHECK (Stock >= 0),
	PRIMARY KEY (LocationId, ProductId)
)


CREATE TABLE OrderDetails (
	Id INT PRIMARY KEY IDENTITY,
	CustomerId INT NOT NULL FOREIGN KEY REFERENCES CustomerTable (Id),
	LocationId INT NOT NULL FOREIGN KEY REFERENCES StoreTable (Id),
	[Date] DATETIME NOT NULL,
	Total MONEY NOT NULL CHECK (Total > 0),

)

CREATE TABLE OrderProduct (
	OrderId INT NOT NULL FOREIGN KEY REFERENCES OrderDetails (Id),
	ProductId INT NOT NULL FOREIGN KEY REFERENCES ProductTable (Id),
	Quantity INT NOT NULL CHECK (Quantity > 0),
	PRIMARY KEY (OrderId, ProductId)
)


INSERT INTO CustomerTable(FirstName, LastName, Phone) VALUES
	('Alex', 'Soto', '(619)730-8241'),
	('Yesenia', 'Cisneros', '(619)482-7104'),
	('Armando', 'Soto', '(619)547-2327');
	



INSERT INTO ProductTable(Name, Price) VALUES
	('Toilet Paper', 3.00),
	('Apple', 1.00),
	('Play Station 5', 500.00),
	('Cheese Burger', 2.00),
	('Picture Frame', 4.00),
	('Makeup', 25.00),
	('Drink', 2.00),
	('Fries', 3.00),
	('Salad', 7.00),
	('Book', 6.00),
	('Computer', 600.00),
	('Desk Chair', 100.00),
	('Mirror', 50.00),
	('Pizza', 13.00);


INSERT INTO StoreTable (Name) VALUES
	('Walmart'),
	('Staples'),
	('Fast Food Shop');



INSERT INTO Inventory (LocationId, ProductId, Stock) VALUES
	(1, 10, 30),
	(1, 11, 10),
	(1, 1, 100),
	(1, 13,20),
	(1, 2, 150),
	(1, 6, 40),
	(1, 5, 50),
	(2, 11, 50),
	(2, 12, 70),
	(2, 10, 200),
	(3, 4, 200),
	(3, 7, 300),
	(3, 8, 200),
	(3, 14, 100),
	(3, 9, 50)

INSERT INTO OrderDetails (CustomerId, LocationId, Date, Total) VALUES
	(1,1, CURRENT_TIMESTAMP, 627.00),
	(1,3, CURRENT_TIMESTAMP, 14.00);

INSERT INTO OrderProduct (OrderId, ProductId, Quantity) VALUES
	(1, 10, 2),
	(1, 11, 1),
	(1, 1, 5),
	(2, 4, 2),
	(2, 8, 2),
	(2, 7, 2);
	
SELECT * FROM CustomerTable
SELECT * FROM StoreTable
SELECT * FROM ProductTable
SELECT * FROM Inventory
SELECT * FROM OrderDetails
SELECT * FROM OrderProduct

