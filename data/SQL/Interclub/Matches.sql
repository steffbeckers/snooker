/****** Script for SelectTopNRows command from SSMS  ******/
SELECT [DivisionId]
      ,[Date]
      ,[HomeTeamId]
      ,[HomeTeamScore]
      ,[AwayTeamScore]
      ,[AwayTeamId]
  FROM [dbo].[InterclubMatches]
  ORDER BY [Date]