USE [master]
GO
/****** Object:  Database [MCQDatabase]    Script Date: 2019/11/18 9:43:40 pm ******/
CREATE DATABASE [MCQDatabase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MCQDatabase', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\MCQDatabase.mdf' , SIZE = 3136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MCQDatabase_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\MCQDatabase_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MCQDatabase] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MCQDatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MCQDatabase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MCQDatabase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MCQDatabase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MCQDatabase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MCQDatabase] SET ARITHABORT OFF 
GO
ALTER DATABASE [MCQDatabase] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [MCQDatabase] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [MCQDatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MCQDatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MCQDatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MCQDatabase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MCQDatabase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MCQDatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MCQDatabase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MCQDatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MCQDatabase] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MCQDatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MCQDatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MCQDatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MCQDatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MCQDatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MCQDatabase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MCQDatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MCQDatabase] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MCQDatabase] SET  MULTI_USER 
GO
ALTER DATABASE [MCQDatabase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MCQDatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MCQDatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MCQDatabase] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [MCQDatabase]
GO
/****** Object:  Table [dbo].[Lecturer]    Script Date: 2019/11/18 9:43:41 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Lecturer](
	[LecturerID] [varchar](5) NOT NULL,
	[Firstname] [varchar](15) NOT NULL,
	[LastName] [varchar](20) NOT NULL,
	[L_Password] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[LecturerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Question_Answer]    Script Date: 2019/11/18 9:43:41 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Question_Answer](
	[QuestionID] [varchar](5) NOT NULL,
	[TestID] [varchar](5) NOT NULL,
	[Question] [varchar](50) NOT NULL,
	[Answer1] [varchar](100) NULL,
	[Answer2] [varchar](100) NULL,
	[Answer3] [varchar](100) NULL,
	[Answer4] [varchar](100) NULL,
	[CorrectAnswer] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Student]    Script Date: 2019/11/18 9:43:41 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Student](
	[StudentNumber] [int] NOT NULL,
	[Age] [int] NOT NULL,
	[Firstname] [varchar](15) NOT NULL,
	[LastName] [varchar](20) NOT NULL,
	[S_Password] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[StudentNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Student_Test]    Script Date: 2019/11/18 9:43:41 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Student_Test](
	[StudentTestID] [varchar](5) NOT NULL,
	[TestName] [varchar](20) NOT NULL,
	[StudentNumber] [int] NOT NULL,
	[TestMark] [decimal](5, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[StudentTestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StudentAnswer]    Script Date: 2019/11/18 9:43:41 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StudentAnswer](
	[StudentAnswerID] [int] IDENTITY(1,1) NOT NULL,
	[StudentNumber] [int] NOT NULL,
	[QuestionID] [varchar](5) NOT NULL,
	[Answer] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[StudentAnswerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Test]    Script Date: 2019/11/18 9:43:41 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Test](
	[TestID] [varchar](5) NOT NULL,
	[LecturerID] [varchar](5) NOT NULL,
	[TestName] [varchar](20) NOT NULL,
	[Publish] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[TestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Lecturer] ([LecturerID], [Firstname], [LastName], [L_Password]) VALUES (N'1', N'Test', N'Test', N'L8LWbNl3/y4=')
INSERT [dbo].[Lecturer] ([LecturerID], [Firstname], [LastName], [L_Password]) VALUES (N'LE001', N'Matthew', N'Talbot', N'A0ScURKrBvA=')
INSERT [dbo].[Question_Answer] ([QuestionID], [TestID], [Question], [Answer1], [Answer2], [Answer3], [Answer4], [CorrectAnswer]) VALUES (N'Q0001', N'TE001', N'3 + 0', N'3', N'4', N'5', N'6', N'3')
INSERT [dbo].[Question_Answer] ([QuestionID], [TestID], [Question], [Answer1], [Answer2], [Answer3], [Answer4], [CorrectAnswer]) VALUES (N'Q0002', N'TE001', N'9 + 9', N'81', N'18', N'19', N'21', N'18')
INSERT [dbo].[Question_Answer] ([QuestionID], [TestID], [Question], [Answer1], [Answer2], [Answer3], [Answer4], [CorrectAnswer]) VALUES (N'Q0003', N'TE001', N'2 + 3', N'3', N'4', N'5', N'6', N'5')
INSERT [dbo].[Question_Answer] ([QuestionID], [TestID], [Question], [Answer1], [Answer2], [Answer3], [Answer4], [CorrectAnswer]) VALUES (N'Q0004', N'TE001', N'10 + 5', N'13', N'14', N'15', N'16', N'15')
INSERT [dbo].[Question_Answer] ([QuestionID], [TestID], [Question], [Answer1], [Answer2], [Answer3], [Answer4], [CorrectAnswer]) VALUES (N'Q0005', N'TE002', N'5 + 5', N'5', N'10', N'25', N'15', N'10')
INSERT [dbo].[Question_Answer] ([QuestionID], [TestID], [Question], [Answer1], [Answer2], [Answer3], [Answer4], [CorrectAnswer]) VALUES (N'Q0006', N'TE002', N'10 x 10', N'100', N'20', N'1000', N'21', N'100')
INSERT [dbo].[Question_Answer] ([QuestionID], [TestID], [Question], [Answer1], [Answer2], [Answer3], [Answer4], [CorrectAnswer]) VALUES (N'Q0007', N'TE002', N'2 + 3', N'5', N'6', N'4', N'7', N'5')
INSERT [dbo].[Question_Answer] ([QuestionID], [TestID], [Question], [Answer1], [Answer2], [Answer3], [Answer4], [CorrectAnswer]) VALUES (N'Q0008', N'TE003', N'test?', N'yes', N'No', N'Maybe', N'Maybe Not', N'Yes')
INSERT [dbo].[Question_Answer] ([QuestionID], [TestID], [Question], [Answer1], [Answer2], [Answer3], [Answer4], [CorrectAnswer]) VALUES (N'Q0009', N'TE004', N'test?', N'Yes', N'No', N'Maybe', N'Maybe Not', N'Yes')
INSERT [dbo].[Question_Answer] ([QuestionID], [TestID], [Question], [Answer1], [Answer2], [Answer3], [Answer4], [CorrectAnswer]) VALUES (N'Q0010', N'TE004', N'1 +1', N'1', N'2', N'3', N'4', N'1')
INSERT [dbo].[Question_Answer] ([QuestionID], [TestID], [Question], [Answer1], [Answer2], [Answer3], [Answer4], [CorrectAnswer]) VALUES (N'Q0011', N'TE005', N'5+5', N'10', N'5', N'25', N'55', N'10')
INSERT [dbo].[Student] ([StudentNumber], [Age], [Firstname], [LastName], [S_Password]) VALUES (18003819, 21, N'Matthew', N'Talbot', N'L8LWbNl3/y4=')
INSERT [dbo].[Student_Test] ([StudentTestID], [TestName], [StudentNumber], [TestMark]) VALUES (N'ST001', N'FinalTest', 18003819, CAST(100.00 AS Decimal(5, 2)))
INSERT [dbo].[Student_Test] ([StudentTestID], [TestName], [StudentNumber], [TestMark]) VALUES (N'ST002', N'Math2', 18003819, CAST(100.00 AS Decimal(5, 2)))
INSERT [dbo].[Student_Test] ([StudentTestID], [TestName], [StudentNumber], [TestMark]) VALUES (N'ST003', N'test', 18003819, CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[Student_Test] ([StudentTestID], [TestName], [StudentNumber], [TestMark]) VALUES (N'ST004', N'Math', 18003819, CAST(100.00 AS Decimal(5, 2)))
SET IDENTITY_INSERT [dbo].[StudentAnswer] ON 

INSERT [dbo].[StudentAnswer] ([StudentAnswerID], [StudentNumber], [QuestionID], [Answer]) VALUES (12, 18003819, N'Q0005', N'10')
INSERT [dbo].[StudentAnswer] ([StudentAnswerID], [StudentNumber], [QuestionID], [Answer]) VALUES (13, 18003819, N'Q0006', N'100')
INSERT [dbo].[StudentAnswer] ([StudentAnswerID], [StudentNumber], [QuestionID], [Answer]) VALUES (14, 18003819, N'Q0007', N'5')
INSERT [dbo].[StudentAnswer] ([StudentAnswerID], [StudentNumber], [QuestionID], [Answer]) VALUES (15, 18003819, N'Q0001', N'3')
INSERT [dbo].[StudentAnswer] ([StudentAnswerID], [StudentNumber], [QuestionID], [Answer]) VALUES (16, 18003819, N'Q0002', N'18')
INSERT [dbo].[StudentAnswer] ([StudentAnswerID], [StudentNumber], [QuestionID], [Answer]) VALUES (17, 18003819, N'Q0003', N'5')
INSERT [dbo].[StudentAnswer] ([StudentAnswerID], [StudentNumber], [QuestionID], [Answer]) VALUES (18, 18003819, N'Q0004', N'15')
INSERT [dbo].[StudentAnswer] ([StudentAnswerID], [StudentNumber], [QuestionID], [Answer]) VALUES (19, 18003819, N'Q0008', N'yes')
INSERT [dbo].[StudentAnswer] ([StudentAnswerID], [StudentNumber], [QuestionID], [Answer]) VALUES (20, 18003819, N'Q0011', N'10')
SET IDENTITY_INSERT [dbo].[StudentAnswer] OFF
INSERT [dbo].[Test] ([TestID], [LecturerID], [TestName], [Publish]) VALUES (N'TE001', N'LE001', N'Math2', 1)
INSERT [dbo].[Test] ([TestID], [LecturerID], [TestName], [Publish]) VALUES (N'TE002', N'LE001', N'FinalTest', 1)
INSERT [dbo].[Test] ([TestID], [LecturerID], [TestName], [Publish]) VALUES (N'TE003', N'LE001', N'test', 1)
INSERT [dbo].[Test] ([TestID], [LecturerID], [TestName], [Publish]) VALUES (N'TE004', N'LE001', N'test', 1)
INSERT [dbo].[Test] ([TestID], [LecturerID], [TestName], [Publish]) VALUES (N'TE005', N'LE001', N'Math', 1)
INSERT [dbo].[Test] ([TestID], [LecturerID], [TestName], [Publish]) VALUES (N'TE006', N'LE001', N'English', 1)
INSERT [dbo].[Test] ([TestID], [LecturerID], [TestName], [Publish]) VALUES (N'TE007', N'LE001', N'test2', 1)
INSERT [dbo].[Test] ([TestID], [LecturerID], [TestName], [Publish]) VALUES (N'TE008', N'LE001', N'test3', 1)
ALTER TABLE [dbo].[Question_Answer]  WITH CHECK ADD FOREIGN KEY([TestID])
REFERENCES [dbo].[Test] ([TestID])
GO
ALTER TABLE [dbo].[Student_Test]  WITH CHECK ADD FOREIGN KEY([StudentNumber])
REFERENCES [dbo].[Student] ([StudentNumber])
GO
ALTER TABLE [dbo].[StudentAnswer]  WITH CHECK ADD FOREIGN KEY([StudentNumber])
REFERENCES [dbo].[Student] ([StudentNumber])
GO
ALTER TABLE [dbo].[StudentAnswer]  WITH CHECK ADD FOREIGN KEY([QuestionID])
REFERENCES [dbo].[Question_Answer] ([QuestionID])
GO
ALTER TABLE [dbo].[Test]  WITH CHECK ADD FOREIGN KEY([LecturerID])
REFERENCES [dbo].[Lecturer] ([LecturerID])
GO
USE [master]
GO
ALTER DATABASE [MCQDatabase] SET  READ_WRITE 
GO
