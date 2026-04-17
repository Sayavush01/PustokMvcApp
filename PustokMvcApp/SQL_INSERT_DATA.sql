-- =====================================================
-- PUSTOK BOOK STORE - SQL SERVER INSERT QUERIES
-- Database: PustokMvcApp
-- =====================================================

-- Set database context
USE PustokMvcApp;
GO

-- Enable identity insert if needed
SET IDENTITY_INSERT Sliders ON;
GO

-- =====================================================
-- 1. DELETE EXISTING DATA (OPTIONAL - تنظيف مسبق)
-- =====================================================
/*
DELETE FROM BookTags;
DELETE FROM BookImages;
DELETE FROM Books;
DELETE FROM Tags;
DELETE FROM Authors;
DELETE FROM Sliders;
GO
*/

-- =====================================================
-- 2. INSERT AUTHORS (YAZARLAR)
-- =====================================================
PRINT '📝 Inserting Authors...'
INSERT INTO Authors (Id, FullName)
VALUES
    (CAST('A1B2C3D4-E5F6-7890-ABCD-EF1234567890' AS UNIQUEIDENTIFIER), 'J.K. Rowling'),
    (CAST('B2C3D4E5-F6A7-8901-BCDE-F12345678901' AS UNIQUEIDENTIFIER), 'George R.R. Martin'),
    (CAST('C3D4E5F6-A7B8-9012-CDEF-123456789012' AS UNIQUEIDENTIFIER), 'J.R.R. Tolkien'),
    (CAST('D4E5F6A7-B8C9-0123-DEF1-234567890123' AS UNIQUEIDENTIFIER), 'Agatha Christie'),
    (CAST('E5F6A7B8-C9D0-1234-EF12-345678901234' AS UNIQUEIDENTIFIER), 'Stephen King'),
    (CAST('F6A7B8C9-D0E1-2345-F123-456789012345' AS UNIQUEIDENTIFIER), 'Paulo Coelho'),
    (CAST('A7B8C9D0-E1F2-3456-A234-567890123456' AS UNIQUEIDENTIFIER), 'Dan Brown'),
    (CAST('B8C9D0E1-F2A3-4567-B345-678901234567' AS UNIQUEIDENTIFIER), 'Danielle Steel');
GO
PRINT '✅ Authors inserted successfully!'
GO

-- =====================================================
-- 3. INSERT TAGS (ETIKETLER)
-- =====================================================
PRINT '📝 Inserting Tags...'
INSERT INTO Tags (Id, Name)
VALUES
    (CAST('T1111111-1111-1111-1111-111111111111' AS UNIQUEIDENTIFIER), 'Fiction'),
    (CAST('T2222222-2222-2222-2222-222222222222' AS UNIQUEIDENTIFIER), 'Fantasy'),
    (CAST('T3333333-3333-3333-3333-333333333333' AS UNIQUEIDENTIFIER), 'Mystery'),
    (CAST('T4444444-4444-4444-4444-444444444444' AS UNIQUEIDENTIFIER), 'Adventure'),
    (CAST('T5555555-5555-5555-5555-555555555555' AS UNIQUEIDENTIFIER), 'Romance'),
    (CAST('T6666666-6666-6666-6666-666666666666' AS UNIQUEIDENTIFIER), 'Thriller'),
    (CAST('T7777777-7777-7777-7777-777777777777' AS UNIQUEIDENTIFIER), 'Self-Help'),
    (CAST('T8888888-8888-8888-8888-888888888888' AS UNIQUEIDENTIFIER), 'Science Fiction');
GO
PRINT '✅ Tags inserted successfully!'
GO

-- =====================================================
-- 4. INSERT BOOKS (KITABLAR)
-- =====================================================
PRINT '📝 Inserting Books...'
INSERT INTO Books (Id, Name, Description, DiscountPercentage, Price, AuthorId, MainImageUrl, HoverImageUrl, IsNew, IsFeatured, InStock, Code)
VALUES
    (CAST('B1111111-1111-1111-1111-111111111111' AS UNIQUEIDENTIFIER), 
     'Harry Potter and the Sorcerer''s Stone', 
     'The first book in the Harry Potter series about a young wizard discovering his magical powers at Hogwarts School of Witchcraft and Wizardry.', 
     10, 15.99, CAST('A1B2C3D4-E5F6-7890-ABCD-EF1234567890' AS UNIQUEIDENTIFIER), 
     'harry-potter-1.jpg', 'harry-potter-1-hover.jpg', 1, 1, 1, 1001),

    (CAST('B2222222-2222-2222-2222-222222222222' AS UNIQUEIDENTIFIER), 
     'A Game of Thrones', 
     'The first book of A Song of Ice and Fire series featuring political intrigue in a fantasy world.', 
     15, 18.99, CAST('B2C3D4E5-F6A7-8901-BCDE-F12345678901' AS UNIQUEIDENTIFIER), 
     'game-of-thrones.jpg', 'game-of-thrones-hover.jpg', 1, 1, 1, 1002),

    (CAST('B3333333-3333-3333-3333-333333333333' AS UNIQUEIDENTIFIER), 
     'The Hobbit', 
     'A fantasy novel about a hobbit''s unexpected adventure in Middle-earth with dwarves and a wizard.', 
     5, 14.99, CAST('C3D4E5F6-A7B8-9012-CDEF-123456789012' AS UNIQUEIDENTIFIER), 
     'hobbit.jpg', 'hobbit-hover.jpg', 1, 1, 1, 1003),

    (CAST('B4444444-4444-4444-4444-444444444444' AS UNIQUEIDENTIFIER), 
     'Murder on the Orient Express', 
     'A detective mystery novel by Agatha Christie featuring Hercule Poirot solving a complex murder case.', 
     0, 12.99, CAST('D4E5F6A7-B8C9-0123-DEF1-234567890123' AS UNIQUEIDENTIFIER), 
     'murder-orient.jpg', 'murder-orient-hover.jpg', 0, 0, 1, 1004),

    (CAST('B5555555-5555-5555-5555-555555555555' AS UNIQUEIDENTIFIER), 
     'The Shining', 
     'A psychological horror novel about a family isolated in a haunted hotel during winter.', 
     20, 16.99, CAST('E5F6A7B8-C9D0-1234-EF12-345678901234' AS UNIQUEIDENTIFIER), 
     'shining.jpg', 'shining-hover.jpg', 1, 0, 1, 1005),

    (CAST('B6666666-6666-6666-6666-666666666666' AS UNIQUEIDENTIFIER), 
     'The Alchemist', 
     'A philosophical novel about pursuing personal dreams and finding one''s purpose in life.', 
     12, 13.99, CAST('F6A7B8C9-D0E1-2345-F123-456789012345' AS UNIQUEIDENTIFIER), 
     'alchemist.jpg', 'alchemist-hover.jpg', 0, 1, 1, 1006),

    (CAST('B7777777-7777-7777-7777-777777777777' AS UNIQUEIDENTIFIER), 
     'The Da Vinci Code', 
     'A mystery thriller involving art, history, and codes that must be solved to prevent disaster.', 
     8, 17.99, CAST('A7B8C9D0-E1F2-3456-A234-567890123456' AS UNIQUEIDENTIFIER), 
     'davinci.jpg', 'davinci-hover.jpg', 1, 1, 1, 1007),

    (CAST('B8888888-8888-8888-8888-888888888888' AS UNIQUEIDENTIFIER), 
     'The Notebook', 
     'A romantic novel about enduring love and commitment despite life''s challenges.', 
     25, 11.99, CAST('B8C9D0E1-F2A3-4567-B345-678901234567' AS UNIQUEIDENTIFIER), 
     'notebook.jpg', 'notebook-hover.jpg', 0, 0, 1, 1008);
GO
PRINT '✅ Books inserted successfully!'
GO

-- =====================================================
-- 5. INSERT BOOK IMAGES (KITAB RESIMLERI)
-- =====================================================
PRINT '📝 Inserting Book Images...'
INSERT INTO BookImages (Id, Image, BookId)
VALUES
    (CAST('I1111111-1111-1111-1111-111111111111' AS UNIQUEIDENTIFIER), 
     'https://via.placeholder.com/400x600?text=Harry+Potter+Cover+1', 
     CAST('B1111111-1111-1111-1111-111111111111' AS UNIQUEIDENTIFIER)),

    (CAST('I2222222-2222-2222-2222-222222222222' AS UNIQUEIDENTIFIER), 
     'https://via.placeholder.com/400x600?text=Harry+Potter+Cover+2', 
     CAST('B1111111-1111-1111-1111-111111111111' AS UNIQUEIDENTIFIER)),

    (CAST('I3333333-3333-3333-3333-333333333333' AS UNIQUEIDENTIFIER), 
     'https://via.placeholder.com/400x600?text=Game+of+Thrones+Cover+1', 
     CAST('B2222222-2222-2222-2222-222222222222' AS UNIQUEIDENTIFIER)),

    (CAST('I4444444-4444-4444-4444-444444444444' AS UNIQUEIDENTIFIER), 
     'https://via.placeholder.com/400x600?text=Game+of+Thrones+Cover+2', 
     CAST('B2222222-2222-2222-2222-222222222222' AS UNIQUEIDENTIFIER)),

    (CAST('I5555555-5555-5555-5555-555555555555' AS UNIQUEIDENTIFIER), 
     'https://via.placeholder.com/400x600?text=The+Hobbit+Cover+1', 
     CAST('B3333333-3333-3333-3333-333333333333' AS UNIQUEIDENTIFIER)),

    (CAST('I6666666-6666-6666-6666-666666666666' AS UNIQUEIDENTIFIER), 
     'https://via.placeholder.com/400x600?text=The+Hobbit+Cover+2', 
     CAST('B3333333-3333-3333-3333-333333333333' AS UNIQUEIDENTIFIER)),

    (CAST('I7777777-7777-7777-7777-777777777777' AS UNIQUEIDENTIFIER), 
     'https://via.placeholder.com/400x600?text=Murder+Orient+Cover', 
     CAST('B4444444-4444-4444-4444-444444444444' AS UNIQUEIDENTIFIER)),

    (CAST('I8888888-8888-8888-8888-888888888888' AS UNIQUEIDENTIFIER), 
     'https://via.placeholder.com/400x600?text=The+Shining+Cover', 
     CAST('B5555555-5555-5555-5555-555555555555' AS UNIQUEIDENTIFIER)),

    (CAST('I9999999-9999-9999-9999-999999999999' AS UNIQUEIDENTIFIER), 
     'https://via.placeholder.com/400x600?text=The+Alchemist+Cover', 
     CAST('B6666666-6666-6666-6666-666666666666' AS UNIQUEIDENTIFIER)),

    (CAST('IA000000-A000-A000-A000-A00000000000' AS UNIQUEIDENTIFIER), 
     'https://via.placeholder.com/400x600?text=Da+Vinci+Code+Cover', 
     CAST('B7777777-7777-7777-7777-777777777777' AS UNIQUEIDENTIFIER)),

    (CAST('IB000000-B000-B000-B000-B00000000000' AS UNIQUEIDENTIFIER), 
     'https://via.placeholder.com/400x600?text=The+Notebook+Cover', 
     CAST('B8888888-8888-8888-8888-888888888888' AS UNIQUEIDENTIFIER));
GO
PRINT '✅ Book Images inserted successfully!'
GO

-- =====================================================
-- 6. INSERT BOOK TAGS (MANY-TO-MANY RELATIONSHIP)
-- =====================================================
PRINT '📝 Inserting Book Tags...'
INSERT INTO BookTags (BookId, TagId)
VALUES
    -- Harry Potter: Fiction, Fantasy, Adventure
    (CAST('B1111111-1111-1111-1111-111111111111' AS UNIQUEIDENTIFIER), CAST('T1111111-1111-1111-1111-111111111111' AS UNIQUEIDENTIFIER)),
    (CAST('B1111111-1111-1111-1111-111111111111' AS UNIQUEIDENTIFIER), CAST('T2222222-2222-2222-2222-222222222222' AS UNIQUEIDENTIFIER)),
    (CAST('B1111111-1111-1111-1111-111111111111' AS UNIQUEIDENTIFIER), CAST('T4444444-4444-4444-4444-444444444444' AS UNIQUEIDENTIFIER)),

    -- Game of Thrones: Fiction, Fantasy, Adventure
    (CAST('B2222222-2222-2222-2222-222222222222' AS UNIQUEIDENTIFIER), CAST('T1111111-1111-1111-1111-111111111111' AS UNIQUEIDENTIFIER)),
    (CAST('B2222222-2222-2222-2222-222222222222' AS UNIQUEIDENTIFIER), CAST('T2222222-2222-2222-2222-222222222222' AS UNIQUEIDENTIFIER)),
    (CAST('B2222222-2222-2222-2222-222222222222' AS UNIQUEIDENTIFIER), CAST('T4444444-4444-4444-4444-444444444444' AS UNIQUEIDENTIFIER)),

    -- The Hobbit: Fantasy, Adventure
    (CAST('B3333333-3333-3333-3333-333333333333' AS UNIQUEIDENTIFIER), CAST('T2222222-2222-2222-2222-222222222222' AS UNIQUEIDENTIFIER)),
    (CAST('B3333333-3333-3333-3333-333333333333' AS UNIQUEIDENTIFIER), CAST('T4444444-4444-4444-4444-444444444444' AS UNIQUEIDENTIFIER)),

    -- Murder on the Orient Express: Mystery, Thriller
    (CAST('B4444444-4444-4444-4444-444444444444' AS UNIQUEIDENTIFIER), CAST('T3333333-3333-3333-3333-333333333333' AS UNIQUEIDENTIFIER)),
    (CAST('B4444444-4444-4444-4444-444444444444' AS UNIQUEIDENTIFIER), CAST('T6666666-6666-6666-6666-666666666666' AS UNIQUEIDENTIFIER)),

    -- The Shining: Thriller, Fiction
    (CAST('B5555555-5555-5555-5555-555555555555' AS UNIQUEIDENTIFIER), CAST('T6666666-6666-6666-6666-666666666666' AS UNIQUEIDENTIFIER)),
    (CAST('B5555555-5555-5555-5555-555555555555' AS UNIQUEIDENTIFIER), CAST('T1111111-1111-1111-1111-111111111111' AS UNIQUEIDENTIFIER)),

    -- The Alchemist: Self-Help, Fiction
    (CAST('B6666666-6666-6666-6666-666666666666' AS UNIQUEIDENTIFIER), CAST('T7777777-7777-7777-7777-777777777777' AS UNIQUEIDENTIFIER)),
    (CAST('B6666666-6666-6666-6666-666666666666' AS UNIQUEIDENTIFIER), CAST('T1111111-1111-1111-1111-111111111111' AS UNIQUEIDENTIFIER)),

    -- The Da Vinci Code: Mystery, Thriller
    (CAST('B7777777-7777-7777-7777-777777777777' AS UNIQUEIDENTIFIER), CAST('T3333333-3333-3333-3333-333333333333' AS UNIQUEIDENTIFIER)),
    (CAST('B7777777-7777-7777-7777-777777777777' AS UNIQUEIDENTIFIER), CAST('T6666666-6666-6666-6666-666666666666' AS UNIQUEIDENTIFIER)),

    -- The Notebook: Romance, Fiction
    (CAST('B8888888-8888-8888-8888-888888888888' AS UNIQUEIDENTIFIER), CAST('T5555555-5555-5555-5555-555555555555' AS UNIQUEIDENTIFIER)),
    (CAST('B8888888-8888-8888-8888-888888888888' AS UNIQUEIDENTIFIER), CAST('T1111111-1111-1111-1111-111111111111' AS UNIQUEIDENTIFIER));
GO
PRINT '✅ Book Tags inserted successfully!'
GO

-- =====================================================
-- 7. INSERT SLIDERS (HOMEPAGE SLIDERLERİ)
-- =====================================================
PRINT '📝 Inserting Sliders...'
INSERT INTO Sliders (Id, ImageUrl, Title, Description, ButtonText, ButtonUrl)
VALUES
    (1, 'slider-1.jpg', 'Welcome to Pustok Books', 
     'Discover thousands of books from the best authors around the world', 
     'Shop Now', '/home/index'),

    (2, 'slider-2.jpg', 'Fantasy Worlds Await', 
     'Explore magical adventures, epic quests, and breathtaking fantasy stories', 
     'Browse Fantasy', '/books/index?tag=fantasy'),

    (3, 'slider-3.jpg', 'Mystery & Thriller Collection', 
     'Unravel compelling mysteries and thrilling tales that will keep you on the edge of your seat', 
     'Explore Mysteries', '/books/index?tag=mystery'),

    (4, 'slider-4.jpg', 'New Arrivals This Month', 
     'Check out the latest bestsellers and exciting new releases from your favorite authors', 
     'View New Books', '/books/index?new=1');
GO
SET IDENTITY_INSERT Sliders OFF;
GO
PRINT '✅ Sliders inserted successfully!'
GO

-- =====================================================
-- 8. VERIFICATION QUERIES - VERİLERİ KONTROL ET
-- =====================================================
PRINT '
========================================
VERIFICATION SUMMARY
========================================
'
GO

SELECT 
    (SELECT COUNT(*) FROM Authors) as [Authors Count],
    (SELECT COUNT(*) FROM Tags) as [Tags Count],
    (SELECT COUNT(*) FROM Books) as [Books Count],
    (SELECT COUNT(*) FROM BookImages) as [Book Images Count],
    (SELECT COUNT(*) FROM BookTags) as [Book Tags Count],
    (SELECT COUNT(*) FROM Sliders) as [Sliders Count];
GO

-- Detailed view
PRINT '
📚 BOOKS WITH AUTHORS:'
GO
SELECT 
    b.Name as [Book Title],
    b.Price as [Price],
    b.DiscountPercentage as [Discount %],
    CAST(b.Price - (b.Price * b.DiscountPercentage / 100.0) AS DECIMAL(10,2)) as [Final Price],
    a.FullName as [Author],
    b.IsFeatured as [Featured],
    b.IsNew as [New]
FROM Books b
INNER JOIN Authors a ON b.AuthorId = a.Id
ORDER BY b.Name;
GO

PRINT '
🏷️  BOOKS WITH TAGS:'
GO
SELECT 
    b.Name as [Book],
    STRING_AGG(t.Name, ', ') as [Tags]
FROM Books b
LEFT JOIN BookTags bt ON b.Id = bt.BookId
LEFT JOIN Tags t ON bt.TagId = t.Id
GROUP BY b.Id, b.Name
ORDER BY b.Name;
GO

PRINT '
🖼️  BOOKS WITH IMAGE COUNT:'
GO
SELECT 
    b.Name as [Book],
    COUNT(DISTINCT bi.Id) as [Image Count]
FROM Books b
LEFT JOIN BookImages bi ON b.Id = bi.BookId
GROUP BY b.Id, b.Name
ORDER BY b.Name;
GO

-- =====================================================
-- COMPLETION MESSAGE
-- =====================================================
PRINT '
✅ ✅ ✅ ALL DATA INSERTED SUCCESSFULLY! ✅ ✅ ✅
========================================
📊 Total Records Inserted:
   • 8 Authors
   • 8 Tags
   • 8 Books
   • 11 Book Images
   • 18 Book Tags (Many-to-Many)
   • 4 Sliders
========================================
🎉 Database is ready to use!
========================================
'
GO
