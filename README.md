# VesselsApps
 Get Vessels Information

#### VesselsApps is composed of a Web API named VesselsApi and a console application named Refresher.
#### It was implemented using .NET 5.0 by Visual Stuido Community 2019, Version 16.10.3.
#### There are four projects.

## VesselsApi (ASP.NET Core Web API)
Four end-points are provided. The web api was tested by IIS Express host.

#### GET api/baisc: Retrieve a list of each vessel’s basic information (ID, Name and Status) from the local database VesselDb (table: Basic)

#### GET api/basic/location/{vesselID}: Retrieve the location (Latitude, Longitude, Speed and Heading) of a given vessel (vessel Id) from the cache

#### GET api/basic/refresh: Retrieve a list of each vessel’s basic information from the WSF server, and update the Basic table of the local database. The request will be called by the console application Refresher.

#### GET api/basic/cache: Retrieve a list of each vessel’s location information from the WSF server, and update the local cache. The cache life is set as 9 minutes. The request will be called by the console application Refresher.

- NOTICE: I hard code the Web API http port number as 42224. I am not sure if the port will be changed in other machine or not. 

## Refresher
#### This is a .NET Core console application.
#### It will get the basic information from WSF, and update the local database by calling api/basic/refresh every hour.
#### It will get the location information from WSF, and update the local cache by calling api/basic/cache every 5 minutes.


## ReaderApi
#### It is a library which is used to retrieve data from WSFS.


## Test
#### It is a unit test project using xUnit. Only one test method was implemented now.


## Database
SQL Server Express is being used.
The database name is VesselDb
Only one table is being used. The table name is Basic which is used to store basic information for vessels.

#### SQL file to create the table named Basic in database VesselDb
USE [VesselDb]  
GO  
SET ANSI_NULLS ON  
GO  
SET QUOTED_IDENTIFIER ON  
GO  
CREATE TABLE [dbo].[Basic](  
	[Id] [int] IDENTITY(1,1) NOT NULL,  
	[VesselId] [int] NOT NULL,  
	[VesselName] [nvarchar](255) NOT NULL,  
	[Status] [int] NOT NULL,  
 CONSTRAINT [PK_Basic] PRIMARY KEY CLUSTERED  
( 
	[Id] ASC 
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]  
) ON [PRIMARY]  
GO  
INSERT INTO [dbo].[Basic]  
           ([VesselId],[VesselName],[Status])  
     VALUES  
           (101,'Vessel01',1)  
GO  
INSERT INTO [dbo].[Basic]  
           ([VesselId],[VesselName],[Status])  
     VALUES  
           (102,'Vessel02',2)  
GO  
INSERT INTO [dbo].[Basic]  
           ([VesselId],[VesselName],[Status])  
     VALUES  
           (103,'Vessel03',1)  
GO  
