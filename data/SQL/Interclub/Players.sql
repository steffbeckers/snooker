SELECT YEAR(s.[StartDate]) + '-' + YEAR(s.[EndDate]) AS Season
      ,c.[Name] AS [ClubName]
      ,p.[FirstName]
      ,p.[LastName]
      ,p.[DateOfBirth]
      ,p.[Class]
      ,t.[Name] AS [TeamName]
      --,tp.[IsCaptain]
  FROM [dbo].[InterclubPlayers] p
  LEFT JOIN [dbo].[InterclubClubs] c ON p.[ClubId] = c.[Id]
  LEFT JOIN [dbo].[InterclubTeamPlayers] tp ON p.[Id] = tp.[PlayerId]
  LEFT JOIN [dbo].[InterclubTeams] t ON tp.[TeamId] = t.[Id]
  LEFT JOIN [dbo].[InterclubDivisions] d ON t.[DivisionId] = d.[Id]
  LEFT JOIN [dbo].[InterclubSeasons] s ON d.[SeasonId] = s.[Id]
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
