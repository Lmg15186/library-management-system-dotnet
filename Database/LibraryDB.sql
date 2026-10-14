/* =============================================================
   Library Management System - database setup script
   Creates the LibraryDB database, its tables, the ViewBook view
   and sample data so the application can be run straight away.

   All data below is fictitious and for testing only.
   Passwords are stored as SHA-256 hashes, never as plain text.
   ============================================================= */

IF DB_ID('LibraryDB') IS NULL
    CREATE DATABASE LibraryDB;
GO

USE LibraryDB;
GO

/* ---------- Clean re-run: drop objects in dependency order ---------- */
IF OBJECT_ID('dbo.ViewBook', 'V') IS NOT NULL DROP VIEW dbo.ViewBook;
IF OBJECT_ID('dbo.TabReserved', 'U') IS NOT NULL DROP TABLE dbo.TabReserved;
IF OBJECT_ID('dbo.TabBorrow', 'U') IS NOT NULL DROP TABLE dbo.TabBorrow;
IF OBJECT_ID('dbo.TabBook', 'U') IS NOT NULL DROP TABLE dbo.TabBook;
IF OBJECT_ID('dbo.TabAuthor', 'U') IS NOT NULL DROP TABLE dbo.TabAuthor;
IF OBJECT_ID('dbo.TabCategory', 'U') IS NOT NULL DROP TABLE dbo.TabCategory;
IF OBJECT_ID('dbo.TabLanguage', 'U') IS NOT NULL DROP TABLE dbo.TabLanguage;
IF OBJECT_ID('dbo.TabUser', 'U') IS NOT NULL DROP TABLE dbo.TabUser;
GO

/* ---------- Tables ---------- */
CREATE TABLE dbo.TabUser (
    UID        INT IDENTITY(1,1) PRIMARY KEY,
    UserName   VARCHAR(8)  NOT NULL UNIQUE,
    Password   VARCHAR(64) NOT NULL,          -- SHA-256 hash (64 hex characters)
    UserLevel  INT         NOT NULL CHECK (UserLevel BETWEEN 1 AND 3)  -- 1 Student, 2 Staff, 3 Administrator
);

CREATE TABLE dbo.TabCategory (
    CID          INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName VARCHAR(50) NOT NULL
);

CREATE TABLE dbo.TabLanguage (
    LID          INT IDENTITY(1,1) PRIMARY KEY,
    LanguageName VARCHAR(50) NOT NULL
);

CREATE TABLE dbo.TabAuthor (
    AID        INT IDENTITY(1,1) PRIMARY KEY,
    AuthorName VARCHAR(100) NOT NULL
);

CREATE TABLE dbo.TabBook (
    ISBN        VARCHAR(13)  PRIMARY KEY,
    BookName    VARCHAR(100) NOT NULL,
    Author      INT          NOT NULL REFERENCES dbo.TabAuthor(AID),
    Category    INT          NOT NULL REFERENCES dbo.TabCategory(CID),
    Language    INT          NOT NULL REFERENCES dbo.TabLanguage(LID),
    PublishYear INT          NOT NULL,
    Pages       INT          NOT NULL,
    Publisher   VARCHAR(100) NOT NULL
);

-- ActualReturnDate uses 2000-01-01 to mean "not returned yet" (the application relies on this).
CREATE TABLE dbo.TabBorrow (
    BID              INT IDENTITY(1,1) PRIMARY KEY,
    UID              INT          NOT NULL REFERENCES dbo.TabUser(UID),
    ISBN             VARCHAR(13)  NOT NULL REFERENCES dbo.TabBook(ISBN),
    BorrowDate       DATE         NOT NULL,
    ReturnDate       DATE         NOT NULL,
    ActualReturnDate DATE         NOT NULL,
    LateFee          DECIMAL(5,2) NOT NULL DEFAULT 0
);

CREATE TABLE dbo.TabReserved (
    RID          INT IDENTITY(1,1) PRIMARY KEY,
    UID          INT         NOT NULL REFERENCES dbo.TabUser(UID),
    ISBN         VARCHAR(13) NOT NULL REFERENCES dbo.TabBook(ISBN),
    ReservedDate DATE        NOT NULL
);
GO

/* ---------- View used by the book search screen ---------- */
CREATE VIEW dbo.ViewBook AS
SELECT b.ISBN, b.BookName, b.Publisher, b.PublishYear, b.Pages,
       a.AuthorName, c.CategoryName, l.LanguageName,
       b.Author, b.Category, b.Language
FROM dbo.TabBook b
JOIN dbo.TabAuthor   a ON a.AID = b.Author
JOIN dbo.TabCategory c ON c.CID = b.Category
JOIN dbo.TabLanguage l ON l.LID = b.Language;
GO

/* ---------- Sample data ---------- */

-- Test accounts (fictitious). Passwords: Admin@2026, Staff@2026, Tester@2026
INSERT INTO dbo.TabUser (UserName, Password, UserLevel) VALUES
('admin',  'a36aef5a11c4073fbe60314fc9df530a9d5f986533594d1f5190742ff9e0e408', 3),
('staff',  '59b2a257b53ecdd7b19fec7b5050ac33ddda40e5ac5c9fe21de5bc32d96abb3a', 2),
('tester', '3bc9cc16b85c8c8b172720c7378dd048ce6a12a9ca6abf1181dc3f8caf8e1837', 1);

INSERT INTO dbo.TabCategory (CategoryName) VALUES
('Fiction'), ('Technology'), ('Science'), ('History'), ('Business');

INSERT INTO dbo.TabLanguage (LanguageName) VALUES
('English'), ('Portuguese'), ('Spanish');

INSERT INTO dbo.TabAuthor (AuthorName) VALUES
('Sample Author One'), ('Sample Author Two'), ('Sample Author Three'),
('Sample Author Four'), ('Sample Author Five');

-- Sample books with test ISBNs
INSERT INTO dbo.TabBook (ISBN, BookName, Author, Category, Language, PublishYear, Pages, Publisher) VALUES
('9780000000001', 'Introduction to Networking',      1, 2, 1, 2021, 320, 'Test Publishing'),
('9780000000002', 'Databases Made Simple',           2, 2, 1, 2019, 280, 'Test Publishing'),
('9780000000003', 'The Night Library',               3, 1, 1, 2018, 410, 'Sample Press'),
('9780000000004', 'A Short History of Computing',    4, 4, 1, 2020, 256, 'Sample Press'),
('9780000000005', 'Ciência para Todos',              5, 3, 2, 2017, 198, 'Editora Teste'),
('9780000000006', 'Historias del Mar',               3, 1, 3, 2016, 230, 'Editorial Prueba'),
('9780000000007', 'Small Business Basics',           1, 5, 1, 2022, 210, 'Test Publishing'),
('9780000000008', 'Programming with C#',             2, 2, 1, 2023, 450, 'Test Publishing');

-- Loans relative to today, so the three reports always show data:
-- one active loan, one overdue loan and one returned loan.
INSERT INTO dbo.TabBorrow (UID, ISBN, BorrowDate, ReturnDate, ActualReturnDate, LateFee) VALUES
(3, '9780000000001', DATEADD(DAY, -3,  CAST(GETDATE() AS DATE)), DATEADD(DAY, 11,  CAST(GETDATE() AS DATE)), '2000-01-01', 0),
(3, '9780000000003', DATEADD(DAY, -30, CAST(GETDATE() AS DATE)), DATEADD(DAY, -16, CAST(GETDATE() AS DATE)), '2000-01-01', 0),
(2, '9780000000004', DATEADD(DAY, -20, CAST(GETDATE() AS DATE)), DATEADD(DAY, -6,  CAST(GETDATE() AS DATE)), DATEADD(DAY, -8, CAST(GETDATE() AS DATE)), 0);

-- One reservation on a book that is currently on loan
INSERT INTO dbo.TabReserved (UID, ISBN, ReservedDate) VALUES
(2, '9780000000001', DATEADD(DAY, -1, CAST(GETDATE() AS DATE)));
GO
