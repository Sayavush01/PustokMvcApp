-- =====================================================
-- ALTERNATE: If you prefer to use .NET Core CLI
-- =====================================================
-- Run migrations first:
-- dotnet ef database update

-- Then use SQL Script in SSMS or Azure Data Studio
-- =====================================================

-- Quick Check - View all data
SELECT 'Authors' as [Table], COUNT(*) as [Count] FROM Authors
UNION ALL
SELECT 'Tags', COUNT(*) FROM Tags
UNION ALL
SELECT 'Books', COUNT(*) FROM Books
UNION ALL
SELECT 'BookImages', COUNT(*) FROM BookImages
UNION ALL
SELECT 'BookTags', COUNT(*) FROM BookTags
UNION ALL
SELECT 'Sliders', COUNT(*) FROM Sliders;

-- =====================================================
-- Test Queries
-- =====================================================

-- Test 1: All books with their authors
SELECT b.Name as [Book], b.Price, b.DiscountPercentage, a.FullName as [Author]
FROM Books b
INNER JOIN Authors a ON b.AuthorId = a.Id
ORDER BY b.Name;

-- Test 2: Books with tags
SELECT b.Name as [Book], STRING_AGG(t.Name, ', ') as [Tags]
FROM Books b
INNER JOIN BookTags bt ON b.Id = bt.BookId
INNER JOIN Tags t ON bt.TagId = t.Id
GROUP BY b.Id, b.Name
ORDER BY b.Name;

-- Test 3: Books with image count
SELECT b.Name as [Book], COUNT(bi.Id) as [Image Count]
FROM Books b
LEFT JOIN BookImages bi ON b.Id = bi.BookId
GROUP BY b.Id, b.Name
ORDER BY b.Name;

-- Test 4: Featured and New books
SELECT Name as [Book], 
       CASE WHEN IsFeatured = 1 THEN 'Featured' ELSE '' END as [Status],
       CASE WHEN IsNew = 1 THEN 'New' ELSE '' END as [New]
FROM Books
WHERE IsFeatured = 1 OR IsNew = 1;

-- Test 5: Discounted books
SELECT Name as [Book], 
       Price as [Original Price],
       DiscountPercentage as [Discount %],
       CAST(Price - (Price * DiscountPercentage / 100) AS DECIMAL(10,2)) as [Final Price]
FROM Books
WHERE DiscountPercentage > 0
ORDER BY DiscountPercentage DESC;

-- =====================================================
-- ÖNERİLƏN DÜZƏLTMƏ
-- =====================================================

-- Yanlış olan link düzəldilir
<a asp-controller="Books" asp-action="Details" href="product-details.html">

-- Düzgün istifadə 
<a asp-controller="Books" asp-action="Detail" asp-route-id="@book.Id">

-- Alternativ Javascript ilə açmaq üçün
<a href="#" class="book-modal-trigger" data-book-url="@Url.Action(...)">
