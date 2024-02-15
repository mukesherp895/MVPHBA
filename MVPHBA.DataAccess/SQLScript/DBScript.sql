CREATE PROCEDURE [dbo].[Sp_PropertyInfoListGet]
(
	@displayStart INT,
	@displayLength INT,
	@sortDir NVARCHAR(10),
	@sortCol INT,
	@location NVARCHAR(255),
	@price DECIMAL(18,2),
	@propertyType NVARCHAR(255)
)
AS
BEGIN
	SET NOCOUNT ON;
		DECLARE @sql NVARCHAR(MAX)

		SET @sortCol=CASE WHEN @sortCol=0 THEN 1 ELSE @sortCol END
		SELECT @sql=N'
		SELECT
			ROW_NUMBER() OVER(ORDER BY PI.Id) AS [RowNum],
			COUNT(1) OVER() AS [RecCount],
			PI.Id,
			PI.PropertyType,
			PI.Description,
			PI.Location,
			''https://localhost:7177/api/imageget/''+CAST(PI.Id AS NVARCHAR) AS [ImageUrl],
			PI.Price,
			PI.Feature
		FROM PropertyInfos AS PI
		WHERE  ('''+@location+'''='''' OR PI.Location='''+@location+''') AND ('''+CAST(@price AS NVARCHAR)+'''=''0.00'' OR PI.Price<='''+CAST(@price AS NVARCHAR)+''') AND ('''+@propertyType+'''='''' OR PI.PropertyType='''+@propertyType+''') ORDER BY '+CAST(@sortCol AS NVARCHAR(10))+' '+ @sortDir +' OFFSET '+ CAST(@displayStart AS NVARCHAR(10))+' ROW FETCH NEXT ' + CAST(@displayLength AS NVARCHAR(10))+' ROWS ONLY
		' 
		PRINT @sql
		EXECUTE(@sql)
	SET NOCOUNT OFF;
END