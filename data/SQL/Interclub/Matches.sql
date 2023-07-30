SELECT d.[Name] AS [DivisionName]
      ,m.[Date]
      ,htc.[Name] + ' ' + ht.[Name] AS [HomeTeamName]
      ,m.[HomeTeamScore]
      ,m.[AwayTeamScore]
	  ,atc.[Name] + ' ' + at.[Name] AS [AwayTeamName]
  FROM [dbo].[InterclubMatches] m
  LEFT JOIN [dbo].[InterclubDivisions] d ON m.[DivisionId] = d.[Id]
  LEFT JOIN [dbo].[InterclubTeams] ht ON m.[HomeTeamId] = ht.[Id]
  LEFT JOIN [dbo].[InterclubClubs] htc ON ht.[ClubId] = htc.[Id]
  LEFT JOIN [dbo].[InterclubTeams] at ON m.[AwayTeamId] = at.[Id]
  LEFT JOIN [dbo].[InterclubClubs] atc ON at.[ClubId] = atc.[Id]
  WHERE htc.[Name] = 'NRG' AND ht.[Name] = 'C' OR atc.[Name] = 'NRG' AND at.[Name] = 'C'
  ORDER BY d.[SortOrder], m.[Date]