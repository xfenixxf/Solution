SELECT * FROM Books
WHERE Price > 700
ORDER BY Price DESC;

UPDATE Books
SET Price = Price * 1.05
WHERE Price > 1000;

DELETE FROM Books WHERE Price < 10000;

SELECT Author, MAX(Price) AS Max_price
FROM Books
GROUP BY Author;

SELECT b.Title, ob.Quantity
FROM Books b
LEFT JOIN OrderBooks ob ON b.ID_books = ob.ID_book;

SELECT ob.Quantity, b.Title
FROM Books b
RIGHT JOIN OrderBooks ob ON b.ID_books = ob.ID_book;

SELECT DISTINCT u.Full_name
FROM Users u
INNER JOIN Orders o ON u.ID_user = o.ID_user;