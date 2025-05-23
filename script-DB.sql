USE [master]
GO
/****** Object:  Database [DB_Investigaciones]    Script Date: 21/05/2025 01:57:56 ******/
CREATE DATABASE [DB_Investigaciones]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DB_Investigaciones', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\DB_Investigaciones.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DB_Investigaciones_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\DB_Investigaciones_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DB_Investigaciones] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DB_Investigaciones].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DB_Investigaciones] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DB_Investigaciones] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DB_Investigaciones] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DB_Investigaciones] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DB_Investigaciones] SET ARITHABORT OFF 
GO
ALTER DATABASE [DB_Investigaciones] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [DB_Investigaciones] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DB_Investigaciones] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DB_Investigaciones] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DB_Investigaciones] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DB_Investigaciones] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DB_Investigaciones] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DB_Investigaciones] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DB_Investigaciones] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DB_Investigaciones] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DB_Investigaciones] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DB_Investigaciones] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DB_Investigaciones] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DB_Investigaciones] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DB_Investigaciones] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DB_Investigaciones] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DB_Investigaciones] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DB_Investigaciones] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DB_Investigaciones] SET  MULTI_USER 
GO
ALTER DATABASE [DB_Investigaciones] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DB_Investigaciones] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DB_Investigaciones] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DB_Investigaciones] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DB_Investigaciones] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DB_Investigaciones] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [DB_Investigaciones] SET QUERY_STORE = ON
GO
ALTER DATABASE [DB_Investigaciones] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DB_Investigaciones]
GO
/****** Object:  Table [dbo].[Investigaciones]    Script Date: 21/05/2025 01:57:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Investigaciones](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Prompt] [nvarchar](max) NOT NULL,
	[Resultado] [nvarchar](max) NOT NULL,
	[Fecha] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Investigaciones] ADD  DEFAULT (getdate()) FOR [Fecha]
GO
USE [master]
GO
ALTER DATABASE [DB_Investigaciones] SET  READ_WRITE 
GO
