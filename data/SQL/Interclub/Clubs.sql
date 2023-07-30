SELECT --c.[Id]
      c.[Number]
      ,c.[Name]
      ,c.[Email]
      ,c.[PhoneNumber]
      ,c.[Website]
      ,a.[Street]
      ,a.[Number]
      ,a.[PostalCode]
      ,a.[City]
      ,c.[NumberOfTables]
  FROM [dbo].[InterclubClubs] c
  LEFT JOIN [dbo].[InterclubClubAddresses] a ON c.Id = a.ClubId
  ORDER BY c.[Name]