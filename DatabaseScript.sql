USE [MATER]
GO

CREATE DATABASE [EProcurement]

/****** Object:  Table [dbo].[Tender]    Script Date: 12/04/2021 17:06:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tender](
	[Id] [varchar](50) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[RefNumber] [varchar](50) NOT NULL,
	[ReleaseDate] [datetime] NOT NULL,
	[ClosingDate] [datetime] NOT NULL,
	[Details] [varchar](max) NOT NULL,
	[CreatorId] [varchar](50) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[LastUpdatedBy] [varchar](50) NULL,
	[LastUpdatedTime] [datetime] NULL,
 CONSTRAINT [PK_Tender] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 12/04/2021 17:06:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [varchar](50) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](max) NOT NULL,
	[PasswordSalt] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Role] [varchar](20) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Active]  DEFAULT ((0)) FOR [Active]
GO
ALTER TABLE [dbo].[Tender]  WITH CHECK ADD  CONSTRAINT [FK_Tender_User] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Tender] CHECK CONSTRAINT [FK_Tender_User]
GO
ALTER TABLE [dbo].[Tender]  WITH CHECK ADD  CONSTRAINT [FK_Tender_User1] FOREIGN KEY([LastUpdatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Tender] CHECK CONSTRAINT [FK_Tender_User1]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_User]
GO
