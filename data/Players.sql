SELECT p.[FirstName]
      ,p.[LastName]
      ,p.[DateOfBirth]
      ,p.[Class]
      ,c.[Name] AS [ClubName]
      ,t.[Name] AS [TeamName]
      ,tp.[IsCaptain]
  FROM [dbo].[AppPlayers] p
  LEFT JOIN [dbo].[AppClubs] c ON p.[ClubId] = c.[Id]
  LEFT JOIN [dbo].[AppTeamPlayers] tp ON p.[Id] = tp.[PlayerId]
  LEFT JOIN [dbo].[AppTeams] t ON tp.[TeamId] = t.[Id]
  --ORDER BY [FirstName], [LastName]
  --ORDER BY [DateOfBirth] DESC
  ORDER BY c.[Name], t.[Name], p.[FirstName], p.[LastName]

--SELECT [FirstName]
--      ,[LastName]
--	    ,[ClubId]
--	    ,COUNT(*) as [Count]
--  FROM [dbo].[AppPlayers]
--  GROUP BY [FirstName], [LastName], [ClubId]
--  ORDER BY COUNT(*) DESC, [FirstName]

--SELECT [FirstName]
--	    ,COUNT(*) as [Count]
--  FROM [dbo].[AppPlayers]
--  GROUP BY [FirstName]
--  ORDER BY COUNT(*) DESC

--SELECT [LastName]
--	    ,COUNT(*) as [Count]
--  FROM [dbo].[AppPlayers]
--  GROUP BY [LastName]
--  ORDER BY COUNT(*) DESC
