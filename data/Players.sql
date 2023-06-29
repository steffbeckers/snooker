SELECT p.[Id]
      ,p.[FirstName]
      ,p.[LastName]
      ,p.[DateOfBirth]
      ,p.[Class]
	  ,c.[Id] as [ClubId]
	  ,c.[Name] as [ClubName]
  FROM [dbo].[AppPlayers] p
  LEFT JOIN [dbo].[AppClubs] c
  ON p.[ClubId] = c.[Id]
  ORDER BY [FirstName], [LastName]

--SELECT p.[Id]
--      ,p.[FirstName]
--      ,p.[LastName]
--      ,p.[DateOfBirth]
--      ,p.[Class]
--	  ,c.[Id] as [ClubId]
--	  ,c.[Name] as [ClubName]
--  FROM [dbo].[AppPlayers] p
--  LEFT JOIN [dbo].[AppClubs] c
--  ON p.[ClubId] = c.[Id]
--  ORDER BY [DateOfBirth] DESC

SELECT [FirstName]
      ,[LastName]
	  ,[ClubId]
	  ,COUNT(*) as [Count]
  FROM [dbo].[AppPlayers]
  GROUP BY [FirstName], [LastName], [ClubId]
  ORDER BY COUNT(*) DESC, [FirstName]

--SELECT [FirstName]
--	  ,COUNT(*) as [Count]
--  FROM [dbo].[AppPlayers]
--  GROUP BY [FirstName]
--  ORDER BY COUNT(*) DESC

--SELECT [LastName]
--	  ,COUNT(*) as [Count]
--  FROM [dbo].[AppPlayers]
--  GROUP BY [LastName]
--  ORDER BY COUNT(*) DESC
