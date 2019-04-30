CREATE VIEW List_of_orders
AS 
SELECT p.OrderId, c.Name
FROM PlaceOrder p JOIN OrderDetail od ON(od.OrderId = p.OrderId)
JOIN Customer c ON(c.CustomerId = od.CustomerId);



CREATE VIEW List_of_Chefs
AS 
SELECT OrderId, Name
FROM PassOrder p JOIN Chef c ON(c.ChefId = p.ChefId)
where p.Status = 'Done';


CREATE VIEW not_done_orders
AS 
SELECT o.OrderId
FROM OrderDetail o 
JOIN PassOrder p ON(o.OrderId = p.OrderId)
WHERE p.Status = 'Inprogress'


CREATE VIEW Admin_buy_products
AS 
SELECT c.Name as [Category Name],i.Name as [Item Name],[Description],[Price]
FROM SupplierItems i JOIN SupplierCategory c ON(i.CatId = c.CatId);

CREATE VIEW list_of_Admin_food_items
AS 
SELECT [CategoryName],[Name] as [Item Name],[Description],[Price]
FROM ItemsDetail i JOIN Category c ON(i.CategoryId = c.CategoryId);


CREATE VIEW max_order_customers
AS 
SELECT top(1) WITH TIES c.CustomerID, Name, count(*) AS [total orders]
FROM PlaceOrder p JOIN OrderDetail od ON(od.OrderId = p.OrderId)
JOIN Customer c ON(c.CustomerId = od.CustomerId)
JOIN PassOrder po ON(po.OrderId = p.OrderId)
where  po.Status= 'Done'
Group By c.CustomerID, c.Name 
Order by Count(p.OrderID) DESC;

/****8 nhi krna******/
/*** report9 is given below ***/
CREATE VIEW most_demanding_food_item
AS 
SELECT top(1) WITH TIES i.ItemId, i.Name As [Item Name], count(*) AS [total orders]
FROM ItemsDetail i JOIN PlaceOrder o ON (i.ItemId = o.ItemId)
Group By i.ItemId, i.Name 
Order by Count(o.OrderID) DESC;

CREATE VIEW Orders_in_a_day
AS 
SELECT i.Name as [Food_Item Name], OrderDate, count(*) As [Orders]
FROM PlaceOrder p JOIN OrderDetail o ON(o.OrderId = p.OrderId)
JOIN ItemsDetail i ON(i.ItemId = p.ItemId)
Group by o.OrderDate, i.Name;


CREATE VIEW Delivered_orders
AS 
SELECT top(1) WITH TIES Name, pl.Status As [delivery_status], count(*) As [Number_of_orders]
FROM PassOrder p JOIN Chef c ON(c.ChefId = p.ChefId)
JOIN AssignOrder pl ON(pl.OrderId = p.OrderId)
WHERE p.Status = 'Done' AND pl.Status = 'Delivered'
Group By p.OrderId, c.Name, pl.Status
Order by Count(p.OrderId) DESC;



CREATE VIEW Suppliers
AS 
SELECT Name
FROM Supplier;

