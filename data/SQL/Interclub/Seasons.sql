SELECT [Id]
      ,CONCAT(YEAR([StartDate]), '-', YEAR([EndDate])) AS [Season]
      ,[StartDate]
      ,[EndDate]
  FROM [dbo].[InterclubSeasons]
  ORDER BY [StartDate]

--DELETE FROM [dbo].[InterclubSeasons] WHERE [Id] = '938ACD6E-8C21-B166-6051-3A0CB8D9B392'
