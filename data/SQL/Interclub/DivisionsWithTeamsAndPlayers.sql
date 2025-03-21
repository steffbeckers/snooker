SELECT CONCAT(YEAR(s.[StartDate]), '-', YEAR(s.[EndDate])) AS [Season]
      --,d.[Id]
      --,d.[SortOrder]
      ,d.[Name] AS [Division]
      --,d.[FrameCount]
      --,d.[MinPlayerClass]
	  ,c.[Name] AS [Club]
	  ,t.[Name] AS [Team]
	  ,p.[FirstName] AS [PlayerFirstName]
	  ,p.[LastName] AS [PlayerLastName]
  FROM [dbo].[InterclubDivisions] d
  LEFT JOIN [dbo].[InterclubSeasons] s ON d.[SeasonId] = s.[Id]
  LEFT JOIN [dbo].[InterclubTeams] t ON d.[Id] = t.[DivisionId]
  LEFT JOIN [dbo].[InterclubClubs] c ON t.[ClubId] = c.[Id]
  LEFT JOIN [dbo].[InterclubTeamPlayers] tp ON t.[Id] = tp.[TeamId]
  LEFT JOIN [dbo].[InterclubPlayers] p ON tp.[PlayerId] = p.[Id]
  --WHERE p.[FirstName] = 'Steff' AND p.[LastName] = 'Beckers'
  --WHERE c.[Name] = 'NRG' AND t.[Name] = 'C'
  --WHERE p.[FirstName] = 'Dimitri' AND p.[LastName] = 'Clauw'
  --WHERE p.[FirstName] = 'Ufuk' AND p.[LastName] = 'Baygunes'
  --WHERE p.[FirstName] = 'Vincent' AND p.[LastName] = 'Moureau'
  WHERE p.[FirstName] = 'Sybren' AND p.[LastName] = 'Sokolowski'
  ORDER BY d.[SortOrder], c.[Name], t.[Name], p.[FirstName]