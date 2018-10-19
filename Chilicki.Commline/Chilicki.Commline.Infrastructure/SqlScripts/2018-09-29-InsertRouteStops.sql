USE [Commline]
GO

INSERT INTO [dbo].[RouteStops]
           ([StopIndex]
           ,[Line_Id]
           ,[Stop_Id])
     VALUES
           (1
           ,1
           ,8),
		   (2
           ,1
           ,9),
		   (3
           ,1
           ,10),
		   (1
           ,2
           ,10),
		   (2
           ,2
           ,11),
		   (3
           ,2
           ,12)
GO


