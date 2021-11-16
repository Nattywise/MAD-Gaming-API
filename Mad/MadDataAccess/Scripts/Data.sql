USE MAD
GO
IF EXISTS( SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'CompetitionQuestion')
BEGIN
	DROP TABLE [dbo].[CompetitionQuestion]
END
GO


IF EXISTS( SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Competition')
BEGIN
	DROP TABLE [dbo].[Competition]
END
GO

IF EXISTS( SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Question')
BEGIN
DROP TABLE [dbo].[Question]
END
GO

IF EXISTS( SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Genre')
BEGIN
DROP TABLE [dbo].[Genre]
END
GO

CREATE TABLE [dbo].[Genre](
	[GenreId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](255) NULL,
    CONSTRAINT [PK_Genre] PRIMARY KEY (GenreId)
 )
 
GO

INSERT INTO GENRE ([Description]) VALUES ('Movies') 
GO
INSERT INTO GENRE ([Description]) VALUES ('Cars') 
GO
INSERT INTO GENRE ([Description]) VALUES ('Science') 
GO

 CREATE TABLE [dbo].[Question]
(
[QuestionId] [int] IDENTITY(1,1) NOT NULL,
	[QuestionPhrase] [nvarchar](255) NULL,
	[GenreId] [int] NULL,
	[Option1] [nvarchar](50) NULL,
	[Option1IsCorrect] [bit] NULL,
	[Option2] [nvarchar](50) NULL,
	[Option2IsCorrect] [bit] NULL,
	[Option3] [nvarchar](50) NULL,
	[Option3IsCorrect] [bit] NULL,
	[Option4] [nvarchar](50) NULL,
	[Option4IsCorrect] [bit] NULL,
	[TimeAllowance] [INT] NULL,
 CONSTRAINT PK_Question PRIMARY KEY (QuestionId)
, CONSTRAINT [FK_Question_To_Genre] FOREIGN KEY ([GenreId]) REFERENCES [Genre]([GenreId])

)

GO

INSERT INTO QUESTION (QuestionPhrase,GenreId,Option1,Option1IsCorrect,Option2,Option2IsCorrect,Option3,Option3IsCorrect,Option4,Option4IsCorrect,TimeAllowance) VALUES ('What is the highest grossing movie of all time',(SELECT TOP 1 GenreId FROM GENRE WHERE Description = 'Movies'),'Avatar',0,'Titanic',0,'Avengers End Game',1,'Need For Speed',0,10) 
GO

INSERT INTO QUESTION (QuestionPhrase,GenreId,Option1,Option1IsCorrect,Option2,Option2IsCorrect,Option3,Option3IsCorrect,Option4,Option4IsCorrect,TimeAllowance) VALUES ('Which of these is NOT a real job title that appears in movie credits?',(SELECT TOP 1 GenreId FROM GENRE WHERE Description = 'Movies'),'Key grip',1,'Gaffer',0,'Splicer',0,'Best boy',0,10) 
GO

INSERT INTO QUESTION (QuestionPhrase,GenreId,Option1,Option1IsCorrect,Option2,Option2IsCorrect,Option3,Option3IsCorrect,Option4,Option4IsCorrect,TimeAllowance) VALUES ('What was the first movie in the Marvel Cinematic Universe?',(SELECT TOP 1 GenreId FROM GENRE WHERE Description = 'Movies'),'The Avengers',0,'Spider-Man',0,'Iron Man',0,'Batman',1,10) 
GO

INSERT INTO QUESTION (QuestionPhrase,GenreId,Option1,Option1IsCorrect,Option2,Option2IsCorrect,Option3,Option3IsCorrect,Option4,Option4IsCorrect,TimeAllowance) VALUES ('Which of these actors didn''t appear in ''Pulp Fiction''?',(SELECT TOP 1 GenreId FROM GENRE WHERE Description = 'Movies'),'John Turturro',1,'Samuel L Jaction',0,'Uma Thurman',0,'Bruce Willis',0,10) 
GO

INSERT INTO QUESTION (QuestionPhrase,GenreId,Option1,Option1IsCorrect,Option2,Option2IsCorrect,Option3,Option3IsCorrect,Option4,Option4IsCorrect,TimeAllowance) VALUES ('Which of these movies was NOT directed by M. Night Shyamalan?',(SELECT TOP 1 GenreId FROM GENRE WHERE Description = 'Movies'),'The Rings',0,'Signs',0,'The Sixth Sense',1,'Glass',0,10) 
GO

INSERT INTO QUESTION (QuestionPhrase,GenreId,Option1,Option1IsCorrect,Option2,Option2IsCorrect,Option3,Option3IsCorrect,Option4,Option4IsCorrect,TimeAllowance) VALUES ('Which toyota car model is mostly used for Uber',(SELECT TOP 1 GenreId FROM GENRE WHERE Description = 'Cars'),'Yaris',0,'Auris',0,'Etios',1,'Corrola',0,10) 
GO

INSERT INTO QUESTION (QuestionPhrase,GenreId,Option1,Option1IsCorrect,Option2,Option2IsCorrect,Option3,Option3IsCorrect,Option4,Option4IsCorrect,TimeAllowance) VALUES ('Name the American Formula One racing team that entered the sport for the first time in 2016.',(SELECT TOP 1 GenreId FROM GENRE WHERE Description = 'Cars'),'Ferrari',1,'Haas',0,'Face India',0,'Red Bull',0,10) 
GO

INSERT INTO QUESTION (QuestionPhrase,GenreId,Option1,Option1IsCorrect,Option2,Option2IsCorrect,Option3,Option3IsCorrect,Option4,Option4IsCorrect,TimeAllowance) VALUES ('Name the Alfa Romeo mechanic who started the ​Italian sports car manufacturer, Ferrari',(SELECT TOP 1 GenreId FROM GENRE WHERE Description = 'Cars'),'William Ferrari',0,'Piero Ferrari',0,'Petra Ferrari',0,'Enzo Ferrari',1,10) 
GO

INSERT INTO QUESTION (QuestionPhrase,GenreId,Option1,Option1IsCorrect,Option2,Option2IsCorrect,Option3,Option3IsCorrect,Option4,Option4IsCorrect,TimeAllowance) VALUES ('The Ford Mustang is a true American classic. But it was almost called something else. Pick the right answer below.',(SELECT TOP 1 GenreId FROM GENRE WHERE Description = 'Cars'),'Allegro',0,'Corvette',1,'Ka',0,'Teardrop',0,10) 
GO

INSERT INTO QUESTION (QuestionPhrase,GenreId,Option1,Option1IsCorrect,Option2,Option2IsCorrect,Option3,Option3IsCorrect,Option4,Option4IsCorrect,TimeAllowance) VALUES ('The Model T is Ford''s best selling model. Just how many were manufactured?',(SELECT TOP 1 GenreId FROM GENRE WHERE Description = 'Cars'),'1 Million',0,'3 Million',1,'15 Million',0,'200000',0,10) 
GO

INSERT INTO QUESTION (QuestionPhrase,GenreId,Option1,Option1IsCorrect,Option2,Option2IsCorrect,Option3,Option3IsCorrect,Option4,Option4IsCorrect,TimeAllowance) VALUES ('Which of these is a virus?',(SELECT TOP 1 GenreId FROM GENRE WHERE Description = 'Science'),'Staphylococcus',0,'Leukemia',1,'Scoliosis',0,'Chicken pox',0,10) 
GO

INSERT INTO QUESTION (QuestionPhrase,GenreId,Option1,Option1IsCorrect,Option2,Option2IsCorrect,Option3,Option3IsCorrect,Option4,Option4IsCorrect,TimeAllowance) VALUES ('What do herpetologists study?',(SELECT TOP 1 GenreId FROM GENRE WHERE Description = 'Science'),'Herpes',0,'Blood',0,'Insects',1,'Reptiles',0,10) 
GO

INSERT INTO QUESTION (QuestionPhrase,GenreId,Option1,Option1IsCorrect,Option2,Option2IsCorrect,Option3,Option3IsCorrect,Option4,Option4IsCorrect,TimeAllowance) VALUES ('Which one of these is NOT a mineral?',(SELECT TOP 1 GenreId FROM GENRE WHERE Description = 'Science'),'Quartz',1,'Lithium',0,'Diamond',0,'Calcite',0,10) 
GO

INSERT INTO QUESTION (QuestionPhrase,GenreId,Option1,Option1IsCorrect,Option2,Option2IsCorrect,Option3,Option3IsCorrect,Option4,Option4IsCorrect,TimeAllowance) VALUES ('Which of these has the longest wave length?',(SELECT TOP 1 GenreId FROM GENRE WHERE Description = 'Science'),'Bluetooth',0,'Radio',1,'Visible light',0,'X-rays',0,10) 
GO

INSERT INTO QUESTION (QuestionPhrase,GenreId,Option1,Option1IsCorrect,Option2,Option2IsCorrect,Option3,Option3IsCorrect,Option4,Option4IsCorrect,TimeAllowance) VALUES ('What is the pH of pure water?',(SELECT TOP 1 GenreId FROM GENRE WHERE Description = 'Science'),'Seven',1,'Four',0,'Six',0,'Eight',0,10) 
GO

CREATE TABLE [dbo].[Competition]
(
 [CompetitionId] INT IDENTITY (1, 1) NOT NULL,
 [GenreId] INT NOT NULL
  
 CONSTRAINT PK_Competition PRIMARY KEY (CompetitionId)
, CONSTRAINT [FK_Competition_To_Genre] FOREIGN KEY ([GenreId]) REFERENCES [Genre]([GenreId])
)
GO


CREATE TABLE [dbo].[CompetitionQuestion]
(
 [CompetitionQuestionId] INT IDENTITY (1, 1) NOT NULL,
 [CompetitionId] INT NOT NULL,
 [QuestionId] INT NOT NULL,
  
 [AnswerProvided] INT NULL, 
 [AnswerIsCorrect] BIT NULL, 

 [TimeTaken] INT NULL, 

 [Score] INT NULL
 
 CONSTRAINT PK_CompetitionQuestion PRIMARY KEY (CompetitionQuestionId)
, CONSTRAINT [FK_CompetitionQuestion_To_Competition] FOREIGN KEY ([CompetitionId]) REFERENCES [Competition]([CompetitionId])
, CONSTRAINT [FK_CompetitionQuestion_To_Question] FOREIGN KEY ([QuestionId]) REFERENCES [Question]([QuestionId])
)

GO
