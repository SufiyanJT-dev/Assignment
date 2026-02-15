-- Library Management Schema

CREATE TABLE Authors (
    AuthorID INT PRIMARY KEY,
    AuthorName VARCHAR(100),
    BirthYear INT
);

CREATE TABLE Books (
    BookID INT PRIMARY KEY,
    Title VARCHAR(200),
    AuthorID INT,
    PublicationYear INT,
    FOREIGN KEY (AuthorID) REFERENCES Authors(AuthorID)
);

CREATE TABLE Patrons (
    PatronID INT PRIMARY KEY,
    PatronName VARCHAR(100),
    MembershipDate DATE
);

CREATE TABLE Loans (
    LoanID INT PRIMARY KEY,
    BookID INT,
    PatronID INT,
    LoanDate DATE,
    ReturnDate DATE,
    FOREIGN KEY (BookID) REFERENCES Books(BookID),
    FOREIGN KEY (PatronID) REFERENCES Patrons(PatronID)
);

-- Questions

-- 1. List all books along with their authors, including books without assigned authors.
select * from Books as B Left Join Authors as A on B.AuthorID=A.AuthorID
-- 2. Display all patrons and their loan history, including patrons who have never borrowed a book.
select * from Patrons as P Left Join Loans as L on P.PatronID=L.PatronID 
-- 3. Show all authors and the books they've written, including authors who haven't written any books in our collection.
select * from  Authors as A Left Join Books as B on B.AuthorID=A.AuthorID
-- 4. List all possible book-patron combinations, regardless of whether a loan has occurred.
select * from Books as B cross Join Patrons 
-- 5. Display all loans with book and patron information, including loans where either the book or patron information is missing.
select * from Loans as L Left join Books as B  on L.BookID=B.BookID left join Patrons as P on L.PatronID=P.PatronID
-- 6. Show all books that have never been loaned, along with their author information.
select B.[BookID],B.[Title],A.[AuthorName],A.[BirthYear] from [dbo].[Books] as B left join [dbo].[Loans] as L on B.BookID=L.BookID left join Authors as A on B.AuthorID=A.AuthorID where  L.LoanID is Null 
-- 7. List all patrons who have borrowed books in the last month, along with the books they've borrowed.
SELECT 
    P.PatronID,
    P.PatronName,
    B.BookID,
    B.Title,
    L.LoanDate,
    L.ReturnDate
FROM Patrons AS P
JOIN Loans AS L ON P.PatronID = L.PatronID
JOIN Books AS B ON L.BookID = B.BookID
WHERE L.LoanDate >= date(MONTH, -1, GETDATE())
ORDER BY L.LoanDate;
-- 8. Display all authors born after 1970 and their books, including those without any books in our collection.
select * from Authors As A left Join Books as B on A.AuthorID=B.AuthorID where A.BirthYear >1970-
- 9. Show all books published before 2000 and any associated loan information.
select B.BookID, B.Title, B.PublicationYear, L.LoanID, L.PatronID, L.LoanDate, L.ReturnDate
from Books B
left join Loans L on B.BookID = L.BookID
where B.PublicationYear < 2000;

-- 10. List all possible author-patron combinations, regardless of whether the patron has borrowed a book by that author.
select * from Authors cross join Patrons
