DROP TABLE IF EXISTS Books;
CREATE TABLE Books (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(255) NOT NULL,
    Author NVARCHAR(255) NOT NULL,
    YearPublished INT,
    Content XML
);

-- Вставка новой книги
DROP PROCEDURE IF EXISTS InsertBook;
CREATE PROCEDURE InsertBook
    @Title NVARCHAR(255),
    @Author NVARCHAR(255),
    @YearPublished INT,
    @Content XML
AS
BEGIN
    INSERT INTO Books (Title, Author, YearPublished, Content)
    VALUES (@Title, @Author, @YearPublished, @Content);
END;
go;

-- Обновление существующей книги
DROP PROCEDURE IF EXISTS UpdateBook;
CREATE PROCEDURE UpdateBook
    @Id INT,
    @Title NVARCHAR(255),
    @Author NVARCHAR(255),
    @YearPublished INT,
    @Content XML
AS
BEGIN
    UPDATE Books
    SET Title = @Title, Author = @Author, YearPublished = @YearPublished, Content = @Content
    WHERE Id = @Id;
END;
go;

-- Удаление книги по ID
DROP PROCEDURE IF EXISTS DeleteBook;
CREATE PROCEDURE DeleteBook
    @Id INT
AS
BEGIN
    DELETE FROM Books WHERE Id = @Id;
END;
go;

-- Получение всех книг
DROP PROCEDURE IF EXISTS GetAllBooks;
CREATE PROCEDURE GetAllBooks
AS
BEGIN
    SELECT * FROM Books;
END;
go;