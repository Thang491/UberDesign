USE [master]
GO
/****** Object:  Database [UberSystem]    Script Date: 9/13/2024 8:49:07 AM ******/
CREATE DATABASE [UberSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'UberSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\UberSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'UberSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\UberSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [UberSystem] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [UberSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [UberSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [UberSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [UberSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [UberSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [UberSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [UberSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [UberSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [UberSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [UberSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [UberSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [UberSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [UberSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [UberSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [UberSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [UberSystem] SET  ENABLE_BROKER 
GO
ALTER DATABASE [UberSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [UberSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [UberSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [UberSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [UberSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [UberSystem] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [UberSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [UberSystem] SET RECOVERY FULL 
GO
ALTER DATABASE [UberSystem] SET  MULTI_USER 
GO
ALTER DATABASE [UberSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [UberSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [UberSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [UberSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [UberSystem] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'UberSystem', N'ON'
GO
ALTER DATABASE [UberSystem] SET QUERY_STORE = OFF
GO
USE [UberSystem]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [UberSystem]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 9/13/2024 8:49:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cabs]    Script Date: 9/13/2024 8:49:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cabs](
	[id] [bigint] NOT NULL,
	[driverId] [bigint] NULL,
	[type] [char](1) NULL,
	[regNo] [varchar](50) NULL,
 CONSTRAINT [PK_Cabs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[customers]    Script Date: 9/13/2024 8:49:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customers](
	[id] [bigint] NOT NULL,
	[createAt] [timestamp] NOT NULL,
	[userId] [bigint] NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[drivers]    Script Date: 9/13/2024 8:49:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[drivers](
	[id] [bigint] NOT NULL,
	[cabId] [bigint] NULL,
	[dob] [date] NULL,
	[locationLatitude] [float] NULL,
	[locationLongitude] [float] NULL,
	[createAt] [timestamp] NOT NULL,
	[userId ] [bigint] NULL,
 CONSTRAINT [PK_Drivers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[payments]    Script Date: 9/13/2024 8:49:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[payments](
	[id] [bigint] NOT NULL,
	[tripId] [bigint] NULL,
	[method] [char](1) NULL,
	[amount] [float] NULL,
	[createAt] [timestamp] NOT NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ratings]    Script Date: 9/13/2024 8:49:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ratings](
	[id] [bigint] NOT NULL,
	[customerId] [bigint] NULL,
	[driverId] [bigint] NULL,
	[tripId] [bigint] NULL,
	[rating] [int] NULL,
	[feedback] [nvarchar](1) NULL,
 CONSTRAINT [PK_Ratings] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[trips]    Script Date: 9/13/2024 8:49:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[trips](
	[id] [bigint] NOT NULL,
	[customerId] [bigint] NULL,
	[driverId] [bigint] NULL,
	[paymentId] [bigint] NULL,
	[status] [char](1) NULL,
	[sourceLatitude] [float] NULL,
	[sourceLongitude] [float] NULL,
	[destinationLatitude] [float] NULL,
	[destinationLongitude] [float] NULL,
	[createAt] [timestamp] NOT NULL,
 CONSTRAINT [PK_Trips] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[users]    Script Date: 9/13/2024 8:49:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [bigint] NOT NULL,
	[role] [int] NULL,
	[userName] [nvarchar](50) NULL,
	[email] [varchar](50) NOT NULL,
	[password] [varchar](500) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Index [IX_cabs_driverId]    Script Date: 9/13/2024 8:49:11 AM ******/
CREATE NONCLUSTERED INDEX [IX_cabs_driverId] ON [dbo].[cabs]
(
	[driverId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_customers_userId]    Script Date: 9/13/2024 8:49:11 AM ******/
CREATE NONCLUSTERED INDEX [IX_customers_userId] ON [dbo].[customers]
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_drivers_cabId]    Script Date: 9/13/2024 8:49:11 AM ******/
CREATE NONCLUSTERED INDEX [IX_drivers_cabId] ON [dbo].[drivers]
(
	[cabId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_drivers_userId ]    Script Date: 9/13/2024 8:49:11 AM ******/
CREATE NONCLUSTERED INDEX [IX_drivers_userId ] ON [dbo].[drivers]
(
	[userId ] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_payments_tripId]    Script Date: 9/13/2024 8:49:11 AM ******/
CREATE NONCLUSTERED INDEX [IX_payments_tripId] ON [dbo].[payments]
(
	[tripId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ratings_customerId]    Script Date: 9/13/2024 8:49:11 AM ******/
CREATE NONCLUSTERED INDEX [IX_ratings_customerId] ON [dbo].[ratings]
(
	[customerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ratings_driverId]    Script Date: 9/13/2024 8:49:11 AM ******/
CREATE NONCLUSTERED INDEX [IX_ratings_driverId] ON [dbo].[ratings]
(
	[driverId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ratings_tripId]    Script Date: 9/13/2024 8:49:11 AM ******/
CREATE NONCLUSTERED INDEX [IX_ratings_tripId] ON [dbo].[ratings]
(
	[tripId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_trips_customerId]    Script Date: 9/13/2024 8:49:11 AM ******/
CREATE NONCLUSTERED INDEX [IX_trips_customerId] ON [dbo].[trips]
(
	[customerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_trips_driverId]    Script Date: 9/13/2024 8:49:11 AM ******/
CREATE NONCLUSTERED INDEX [IX_trips_driverId] ON [dbo].[trips]
(
	[driverId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_trips_paymentId]    Script Date: 9/13/2024 8:49:11 AM ******/
CREATE NONCLUSTERED INDEX [IX_trips_paymentId] ON [dbo].[trips]
(
	[paymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cabs]  WITH CHECK ADD  CONSTRAINT [FK_Cab_Driver] FOREIGN KEY([driverId])
REFERENCES [dbo].[drivers] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[cabs] CHECK CONSTRAINT [FK_Cab_Driver]
GO
ALTER TABLE [dbo].[customers]  WITH CHECK ADD  CONSTRAINT [FK_Customer_User] FOREIGN KEY([userId])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[customers] CHECK CONSTRAINT [FK_Customer_User]
GO
ALTER TABLE [dbo].[drivers]  WITH CHECK ADD  CONSTRAINT [FK_Driver_Cab] FOREIGN KEY([cabId])
REFERENCES [dbo].[cabs] ([id])
GO
ALTER TABLE [dbo].[drivers] CHECK CONSTRAINT [FK_Driver_Cab]
GO
ALTER TABLE [dbo].[drivers]  WITH CHECK ADD  CONSTRAINT [FK_Driver_User] FOREIGN KEY([userId ])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[drivers] CHECK CONSTRAINT [FK_Driver_User]
GO
ALTER TABLE [dbo].[payments]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Trip] FOREIGN KEY([tripId])
REFERENCES [dbo].[trips] ([id])
GO
ALTER TABLE [dbo].[payments] CHECK CONSTRAINT [FK_Payment_Trip]
GO
ALTER TABLE [dbo].[ratings]  WITH CHECK ADD  CONSTRAINT [FK_Rating_Customer] FOREIGN KEY([customerId])
REFERENCES [dbo].[customers] ([id])
GO
ALTER TABLE [dbo].[ratings] CHECK CONSTRAINT [FK_Rating_Customer]
GO
ALTER TABLE [dbo].[ratings]  WITH CHECK ADD  CONSTRAINT [FK_Rating_Driver] FOREIGN KEY([driverId])
REFERENCES [dbo].[drivers] ([id])
GO
ALTER TABLE [dbo].[ratings] CHECK CONSTRAINT [FK_Rating_Driver]
GO
ALTER TABLE [dbo].[ratings]  WITH CHECK ADD  CONSTRAINT [FK_Rating_Trip] FOREIGN KEY([tripId])
REFERENCES [dbo].[trips] ([id])
GO
ALTER TABLE [dbo].[ratings] CHECK CONSTRAINT [FK_Rating_Trip]
GO
ALTER TABLE [dbo].[trips]  WITH CHECK ADD  CONSTRAINT [FK_Trip_Customer] FOREIGN KEY([customerId])
REFERENCES [dbo].[customers] ([id])
GO
ALTER TABLE [dbo].[trips] CHECK CONSTRAINT [FK_Trip_Customer]
GO
ALTER TABLE [dbo].[trips]  WITH CHECK ADD  CONSTRAINT [FK_Trip_Driver] FOREIGN KEY([driverId])
REFERENCES [dbo].[drivers] ([id])
GO
ALTER TABLE [dbo].[trips] CHECK CONSTRAINT [FK_Trip_Driver]
GO
ALTER TABLE [dbo].[trips]  WITH CHECK ADD  CONSTRAINT [FK_Trip_Payment] FOREIGN KEY([paymentId])
REFERENCES [dbo].[payments] ([id])
GO
ALTER TABLE [dbo].[trips] CHECK CONSTRAINT [FK_Trip_Payment]
GO
USE [master]
GO
ALTER DATABASE [UberSystem] SET  READ_WRITE 
GO
