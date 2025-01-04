SELECT hp.[FirstName] + ' ' + hp.[LastName] AS [HomePlayerName]
      ,f.[HomePlayerScore]
      ,f.[AwayPlayerScore]
      ,ap.[FirstName] + ' ' + ap.[LastName] AS [AwayPlayerName]
  FROM [dbo].[InterclubFrames] f
  LEFT JOIN [dbo].[InterclubPlayers] hp ON f.[HomePlayerId] = hp.[Id]
  LEFT JOIN [dbo].[InterclubPlayers] ap ON f.[AwayPlayerId] = ap.[Id]
  ORDER BY f.[HomePlayerScore] DESC
  --ORDER BY f.[AwayPlayerScore] DESC