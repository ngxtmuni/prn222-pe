USE [master]
GO
/****** Object:  Database [PE_PRN_25SprB5]    Script Date: 4/25/2025 2:39:17 PM ******/
CREATE DATABASE [PE_PRN_25SprB5]
GO
USE [PE_PRN_25SprB5]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 4/25/2025 2:39:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Instructor]    Script Date: 4/25/2025 2:39:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Instructor](
	[InstructorId] [int] IDENTITY(1,1) NOT NULL,
	[Fullname] [nvarchar](200) NULL,
	[ContractDate] [date] NULL,
	[IsFulltime] [bit] NULL,
	[Department] [int] NULL,
 CONSTRAINT [PK_Instructor] PRIMARY KEY CLUSTERED 
(
	[InstructorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Major]    Script Date: 4/25/2025 2:39:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Major](
	[MajorCode] [varchar](2) NOT NULL,
	[MajorName] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[MajorCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 4/25/2025 2:39:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[StudentId] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](200) NULL,
	[Male] [bit] NULL,
	[Dob] [date] NULL,
	[Major] [varchar](2) NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Department] ON 
GO
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName]) VALUES (1, N'Computing Fundamental')
GO
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName]) VALUES (2, N'Software Engineering')
GO
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName]) VALUES (3, N'Softskill')
GO
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName]) VALUES (4, N'English')
GO
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName]) VALUES (5, N'Japanese')
GO
SET IDENTITY_INSERT [dbo].[Department] OFF
GO
SET IDENTITY_INSERT [dbo].[Instructor] ON 
GO
INSERT [dbo].[Instructor] ([InstructorId], [Fullname], [ContractDate], [IsFulltime], [Department]) VALUES (1, N'Le Bao Trung', CAST(N'2007-10-12' AS Date), 1, 1)
GO
INSERT [dbo].[Instructor] ([InstructorId], [Fullname], [ContractDate], [IsFulltime], [Department]) VALUES (2, N'Nguyen Gia Hieu', CAST(N'2008-09-15' AS Date), 1, 1)
GO
INSERT [dbo].[Instructor] ([InstructorId], [Fullname], [ContractDate], [IsFulltime], [Department]) VALUES (3, N'Hoang Viet Cuong', CAST(N'2010-07-06' AS Date), 0, 1)
GO
INSERT [dbo].[Instructor] ([InstructorId], [Fullname], [ContractDate], [IsFulltime], [Department]) VALUES (4, N'Do Thien Long', CAST(N'2020-02-15' AS Date), 0, 2)
GO
INSERT [dbo].[Instructor] ([InstructorId], [Fullname], [ContractDate], [IsFulltime], [Department]) VALUES (5, N'Hoang Viet Long', CAST(N'2021-02-16' AS Date), 1, 2)
GO
INSERT [dbo].[Instructor] ([InstructorId], [Fullname], [ContractDate], [IsFulltime], [Department]) VALUES (6, N'Le Thi Thu Trang', CAST(N'2019-10-20' AS Date), 1, 3)
GO
INSERT [dbo].[Instructor] ([InstructorId], [Fullname], [ContractDate], [IsFulltime], [Department]) VALUES (7, N'Do Bao Ngoc', CAST(N'2019-12-16' AS Date), 0, 4)
GO
INSERT [dbo].[Instructor] ([InstructorId], [Fullname], [ContractDate], [IsFulltime], [Department]) VALUES (10, N'Dinh Gia Bao', CAST(N'2018-05-15' AS Date), 1, 5)
GO
INSERT [dbo].[Instructor] ([InstructorId], [Fullname], [ContractDate], [IsFulltime], [Department]) VALUES (11, N'Duong Thien Nga', CAST(N'2017-03-09' AS Date), 0, 5)
GO
INSERT [dbo].[Instructor] ([InstructorId], [Fullname], [ContractDate], [IsFulltime], [Department]) VALUES (12, N'Hoang Bao Chau', CAST(N'2020-12-16' AS Date), 1, 5)
GO
INSERT [dbo].[Instructor] ([InstructorId], [Fullname], [ContractDate], [IsFulltime], [Department]) VALUES (13, N'Vu Ngoc Binh', CAST(N'2021-01-01' AS Date), 0, 1)
GO
INSERT [dbo].[Instructor] ([InstructorId], [Fullname], [ContractDate], [IsFulltime], [Department]) VALUES (1002, N'Bui Dinh Chien', CAST(N'2019-09-01' AS Date), 0, 2)
GO
SET IDENTITY_INSERT [dbo].[Instructor] OFF
GO
INSERT [dbo].[Major] ([MajorCode], [MajorName]) VALUES (N'AI', N'Artificial Intelligence')
GO
INSERT [dbo].[Major] ([MajorCode], [MajorName]) VALUES (N'GD', N'Graphic Design')
GO
INSERT [dbo].[Major] ([MajorCode], [MajorName]) VALUES (N'IA', N'Information Assurance')
GO
INSERT [dbo].[Major] ([MajorCode], [MajorName]) VALUES (N'SE', N'Software Engineering')
GO
SET IDENTITY_INSERT [dbo].[Student] ON 
GO
INSERT [dbo].[Student] ([StudentId], [FullName], [Male], [Dob], [Major]) VALUES (1, N'Dinh Bao Ngoc', 0, CAST(N'2000-10-12' AS Date), N'SE')
GO
INSERT [dbo].[Student] ([StudentId], [FullName], [Male], [Dob], [Major]) VALUES (2, N'Do Thi Huong Giang', 0, CAST(N'2001-09-10' AS Date), N'GD')
GO
INSERT [dbo].[Student] ([StudentId], [FullName], [Male], [Dob], [Major]) VALUES (3, N'Duong Phuong Thao', 0, CAST(N'2001-10-06' AS Date), N'GD')
GO
INSERT [dbo].[Student] ([StudentId], [FullName], [Male], [Dob], [Major]) VALUES (4, N'Tran Duc Binh', 1, CAST(N'2000-01-12' AS Date), N'SE')
GO
INSERT [dbo].[Student] ([StudentId], [FullName], [Male], [Dob], [Major]) VALUES (5, N'Vu Hoang Long', 1, CAST(N'2002-07-07' AS Date), N'SE')
GO
INSERT [dbo].[Student] ([StudentId], [FullName], [Male], [Dob], [Major]) VALUES (6, N'Do Thi Thu Trang', 0, CAST(N'2000-10-01' AS Date), N'IA')
GO
INSERT [dbo].[Student] ([StudentId], [FullName], [Male], [Dob], [Major]) VALUES (7, N'Ha Khuc Khanh An', 0, CAST(N'2003-08-02' AS Date), N'AI')
GO
INSERT [dbo].[Student] ([StudentId], [FullName], [Male], [Dob], [Major]) VALUES (8, N'Tran Bao Chau', 1, CAST(N'2002-10-05' AS Date), N'IA')
GO
INSERT [dbo].[Student] ([StudentId], [FullName], [Male], [Dob], [Major]) VALUES (9, N'Duong Phuong Thao', 0, CAST(N'2003-08-07' AS Date), N'AI')
GO
INSERT [dbo].[Student] ([StudentId], [FullName], [Male], [Dob], [Major]) VALUES (10, N'Tran Binh Giang', 1, CAST(N'2003-08-05' AS Date), N'SE')
GO
INSERT [dbo].[Student] ([StudentId], [FullName], [Male], [Dob], [Major]) VALUES (11, N'Do Hoang Giang', 1, CAST(N'2000-12-06' AS Date), N'AI')
GO
SET IDENTITY_INSERT [dbo].[Student] OFF
GO
ALTER TABLE [dbo].[Instructor]  WITH CHECK ADD FOREIGN KEY([Department])
REFERENCES [dbo].[Department] ([DepartmentId])
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD FOREIGN KEY([Major])
REFERENCES [dbo].[Major] ([MajorCode])
GO
USE [master]
GO
ALTER DATABASE [PE_PRN_25SprB5] SET  READ_WRITE 
GO
