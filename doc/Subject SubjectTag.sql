/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 [SubjectTagSet].[SubjectID],[Subject].[Name]
      ,[SubjectTagID],[SubjectTag].[Name]

  FROM [dbo].[SubjectTagSet] join [dbo].[Subject] on [Subject].[ID]=[SubjectID] join [dbo]. [SubjectTag] on [SubjectTag].[ID]=[SubjectTagID]