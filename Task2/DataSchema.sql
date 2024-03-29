USE [Ticket]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 2022/3/2 下午 05:07:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Role] [int] NOT NULL,
	[LoginAccount] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Function]    Script Date: 2022/3/2 下午 05:07:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Function](
	[Code] [smallint] NOT NULL,
	[Type] [varchar](20) NOT NULL,
	[Action] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Function] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermissionMap]    Script Date: 2022/3/2 下午 05:07:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionMap](
	[RoleId] [int] NOT NULL,
	[Code] [smallint] NOT NULL,
 CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 2022/3/2 下午 05:07:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tickets]    Script Date: 2022/3/2 下午 05:07:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tickets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](20) NOT NULL,
	[Summary] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[Status] [tinyint] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[UpdateTime] [datetime] NULL,
	[UpdateUser] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tickets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Role] FOREIGN KEY([Role])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Role]
GO
ALTER TABLE [dbo].[PermissionMap]  WITH CHECK ADD  CONSTRAINT [FK_PermissionMap_Function] FOREIGN KEY([Code])
REFERENCES [dbo].[Function] ([Code])
GO
ALTER TABLE [dbo].[PermissionMap] CHECK CONSTRAINT [FK_PermissionMap_Function]
GO
ALTER TABLE [dbo].[PermissionMap]  WITH CHECK ADD  CONSTRAINT [FK_PermissionMap_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[PermissionMap] CHECK CONSTRAINT [FK_PermissionMap_Role]
GO




INSERT INTO [dbo].[Role] ([Id],[Name]) VALUES
(0,'Administrator')
,(1,'QA')
,(2,'RD')
,(3,'PM')
GO

INSERT [dbo].[Account] ([Id],[Name],[Role],[LoginAccount],[Password]) VALUES
 ('8C6E4361-22EA-49BA-8464-0006DD69B37C','QA',1,'QA','*')
,('386EC953-5946-4D9F-80E6-00092EEE154F','RD',2,'RD','*')
GO

INSERT INTO [dbo].[Function]
           ([Code]
           ,[Type]
           ,[Action])
     VALUES
           (1,'bug','Create')
		   ,(2,'bug','Edit')
		   ,(3,'bug','Delete')
		   ,(4,'bug','Query')
		   ,(5,'bug','Resolve')
GO


INSERT INTO [dbo].[PermissionMap]
           ([RoleId]
           ,[Code])
     VALUES
           (1,1)
		   ,(1,2)
		   ,(1,3)
		   ,(1,4)
		   ,(2,4)
		   ,(2,5)
GO