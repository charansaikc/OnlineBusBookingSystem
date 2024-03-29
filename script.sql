USE [master]
GO
/****** Object:  Database [BusDB]    Script Date: 29-12-2023 10:22:22 PM ******/
CREATE DATABASE [BusDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BusDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\BusDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BusDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\BusDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [BusDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BusDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BusDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BusDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BusDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BusDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BusDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [BusDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BusDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BusDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BusDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BusDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BusDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BusDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BusDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BusDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BusDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BusDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BusDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BusDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BusDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BusDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BusDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BusDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BusDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BusDB] SET  MULTI_USER 
GO
ALTER DATABASE [BusDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BusDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BusDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BusDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BusDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BusDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BusDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [BusDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [BusDB]
GO
/****** Object:  User [bikram]    Script Date: 29-12-2023 10:22:22 PM ******/
CREATE USER [bikram] FOR LOGIN [bikram] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[BookedList]    Script Date: 29-12-2023 10:22:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookedList](
	[BusId] [int] NOT NULL,
	[ReferenceNo] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ScheduleId] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Qty] [int] NOT NULL,
	[Amount] [int] NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[IsCancelled] [bit] NOT NULL,
 CONSTRAINT [PK_BookedList_1] PRIMARY KEY CLUSTERED 
(
	[ReferenceNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusList]    Script Date: 29-12-2023 10:22:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusList](
	[BusNo] [int] NOT NULL,
	[BusName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_BusList] PRIMARY KEY CLUSTERED 
(
	[BusNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LocationList]    Script Date: 29-12-2023 10:22:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocationList](
	[LocationId] [int] IDENTITY(1,1) NOT NULL,
	[Terminal] [varchar](100) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[State] [varchar](50) NOT NULL,
 CONSTRAINT [PK_LocationList] PRIMARY KEY CLUSTERED 
(
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 29-12-2023 10:22:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedule](
	[BusId] [int] NOT NULL,
	[ScheduleId] [int] IDENTITY(1,1) NOT NULL,
	[BusName] [varchar](50) NOT NULL,
	[FromLocationId] [int] NOT NULL,
	[ToLocationId] [int] NOT NULL,
	[Departure] [datetime] NOT NULL,
	[Arrival] [datetime] NOT NULL,
	[Availability] [int] NOT NULL,
	[Price] [int] NOT NULL,
 CONSTRAINT [PK_Schedule_1] PRIMARY KEY CLUSTERED 
(
	[ScheduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 29-12-2023 10:22:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[UserName] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Role] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Passenger] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BookedList] ON 

INSERT [dbo].[BookedList] ([BusId], [ReferenceNo], [UserId], [ScheduleId], [Name], [Qty], [Amount], [Status], [IsCancelled]) VALUES (5000, 1, 2, 1, N'Bikram', 30, 10000, N'Paid', 0)
INSERT [dbo].[BookedList] ([BusId], [ReferenceNo], [UserId], [ScheduleId], [Name], [Qty], [Amount], [Status], [IsCancelled]) VALUES (5000, 2, 2, 3, N'Bikram', 3, 1500, N'Unpaid', 0)
INSERT [dbo].[BookedList] ([BusId], [ReferenceNo], [UserId], [ScheduleId], [Name], [Qty], [Amount], [Status], [IsCancelled]) VALUES (5002, 1002, 2, 1001, N'Bikram', 1, 400, N'Unpaid', 1)
SET IDENTITY_INSERT [dbo].[BookedList] OFF
GO
INSERT [dbo].[BusList] ([BusNo], [BusName]) VALUES (5000, N'Economy')
INSERT [dbo].[BusList] ([BusNo], [BusName]) VALUES (5001, N'Point to point')
INSERT [dbo].[BusList] ([BusNo], [BusName]) VALUES (5002, N'Luxury')
INSERT [dbo].[BusList] ([BusNo], [BusName]) VALUES (5003, N'Semi Luxury')
GO
SET IDENTITY_INSERT [dbo].[LocationList] ON 

INSERT [dbo].[LocationList] ([LocationId], [Terminal], [City], [State]) VALUES (1, N'North Terminal', N'North City', N'Test Only')
INSERT [dbo].[LocationList] ([LocationId], [Terminal], [City], [State]) VALUES (2, N'South Terminal', N'South City', N'Test Only')
SET IDENTITY_INSERT [dbo].[LocationList] OFF
GO
SET IDENTITY_INSERT [dbo].[Schedule] ON 

INSERT [dbo].[Schedule] ([BusId], [ScheduleId], [BusName], [FromLocationId], [ToLocationId], [Departure], [Arrival], [Availability], [Price]) VALUES (5000, 1, N'Economy', 1, 2, CAST(N'2023-12-27T14:42:00.000' AS DateTime), CAST(N'2023-12-28T20:48:00.000' AS DateTime), 30, 1000)
INSERT [dbo].[Schedule] ([BusId], [ScheduleId], [BusName], [FromLocationId], [ToLocationId], [Departure], [Arrival], [Availability], [Price]) VALUES (5001, 2, N'Point to point', 2, 1, CAST(N'2023-12-28T20:00:00.000' AS DateTime), CAST(N'2023-12-29T19:00:00.000' AS DateTime), 30, 200)
INSERT [dbo].[Schedule] ([BusId], [ScheduleId], [BusName], [FromLocationId], [ToLocationId], [Departure], [Arrival], [Availability], [Price]) VALUES (5000, 3, N'Economy', 1, 2, CAST(N'2023-12-30T19:26:00.000' AS DateTime), CAST(N'2023-12-31T18:27:00.000' AS DateTime), 30, 500)
INSERT [dbo].[Schedule] ([BusId], [ScheduleId], [BusName], [FromLocationId], [ToLocationId], [Departure], [Arrival], [Availability], [Price]) VALUES (5002, 1001, N'Luxury', 1, 2, CAST(N'2023-12-30T19:42:00.000' AS DateTime), CAST(N'2023-12-31T19:42:00.000' AS DateTime), 30, 400)
SET IDENTITY_INSERT [dbo].[Schedule] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Name], [UserName], [Password], [Role]) VALUES (1, N'Administrator', N'admin', N'admin', N'admin')
INSERT [dbo].[User] ([Id], [Name], [UserName], [Password], [Role]) VALUES (2, N'Bikram', N'Bikram123', N'123', N'user')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[BookedList]  WITH CHECK ADD  CONSTRAINT [FK_BookedList_BusList] FOREIGN KEY([BusId])
REFERENCES [dbo].[BusList] ([BusNo])
GO
ALTER TABLE [dbo].[BookedList] CHECK CONSTRAINT [FK_BookedList_BusList]
GO
ALTER TABLE [dbo].[BookedList]  WITH CHECK ADD  CONSTRAINT [FK_BookedList_Schedule] FOREIGN KEY([ScheduleId])
REFERENCES [dbo].[Schedule] ([ScheduleId])
GO
ALTER TABLE [dbo].[BookedList] CHECK CONSTRAINT [FK_BookedList_Schedule]
GO
ALTER TABLE [dbo].[BookedList]  WITH CHECK ADD  CONSTRAINT [FK_BookedList_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[BookedList] CHECK CONSTRAINT [FK_BookedList_User]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Schedule_LocationList] FOREIGN KEY([FromLocationId])
REFERENCES [dbo].[LocationList] ([LocationId])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [FK_Schedule_LocationList]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Schedule_LocationList1] FOREIGN KEY([ToLocationId])
REFERENCES [dbo].[LocationList] ([LocationId])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [FK_Schedule_LocationList1]
GO
USE [master]
GO
ALTER DATABASE [BusDB] SET  READ_WRITE 
GO
