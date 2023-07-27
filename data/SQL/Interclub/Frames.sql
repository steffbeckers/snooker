SELECT hp.[FirstName] + ' ' + hp.[LastName] AS [HomePlayerName]
      ,f.[HomePlayerScore]
      ,f.[AwayPlayerScore]
      ,ap.[FirstName] + ' ' + ap.[LastName] AS [AwayPlayerName]
  FROM [dbo].[InterclubFrames] f
  LEFT JOIN [dbo].[InterclubMatches] m ON f.[MatchId] = m.[Id]
  LEFT JOIN [dbo].[InterclubMatchTeamPlayers] mhtp ON f.[HomePlayerId] = mhtp.[Id]
  LEFT JOIN [dbo].[InterclubPlayers] hp ON mhtp.[PlayerId] = hp.[Id]
  LEFT JOIN [dbo].[InterclubMatchTeamPlayers] matp ON f.[AwayPlayerId] = matp.[Id]
  LEFT JOIN [dbo].[InterclubPlayers] ap ON matp.[PlayerId] = ap.[Id]
  ORDER BY f.[HomePlayerScore] DESC