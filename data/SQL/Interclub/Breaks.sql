SELECT p.[FirstName] + ' ' + p.[LastName] AS [PlayerName],
       b.[Value]
  FROM [dbo].[InterclubBreaks] b
  LEFT JOIN [dbo].[InterclubPlayers] p ON b.[PlayerId] = p.[Id]
  --WHERE p.[FirstName] = 'Steff' AND p.[LastName] = 'Beckers'
  --WHERE p.[FirstName] = 'Yoshi' AND p.[LastName] = 'Lecocq'
  --WHERE p.[FirstName] = 'Marco' AND p.[LastName] = 'Vitali'
  ORDER BY b.[Value] DESC