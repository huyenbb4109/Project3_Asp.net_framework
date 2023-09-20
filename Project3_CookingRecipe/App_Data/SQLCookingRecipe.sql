create table CookingRecipe(
	IdRecipe int not null identity(1,1),
	NameFood nvarchar(60) not null,
	Img varchar(150) not null,
	Describe nvarchar(500),
	Ingredients nvarchar(500) not null,
	IdUser int not null,
	IdEvent int not null,
	DateSubmit datetime not null,
	Steps text not null,
	Status bit not null DEFAULT 1,
	CONSTRAINT PK_IdRecipe PRIMARY KEY (IdRecipe)
);

create table Event(
	IdEvent int not null identity(1,1),
	NameEvent nvarchar(100) not null,
	Img varchar(150) not null,
	NumberOfParticipants tinyint not null,
	StartTime datetime not null,
	EndTime datetime not null,
	Prize nvarchar(100),
	Status bit not null DEFAULT 1,
	CONSTRAINT PK_IdEvent PRIMARY KEY (IdEvent)
);

create table DetailsEvent(
	IdEvent int not null,
	IdUser int not null,
	IdRecipe int not null,
);

create table Comment(
	IdComment int not null identity(1,1),
	IdUser int not null,
	Text nvarchar(max),
	IdRecipe int not null,
	Status bit not null DEFAULT 1,
	CONSTRAINT PK_IdComment PRIMARY KEY (IdComment)
);

create table Users(
	IdUser int not null identity(1,1),
	FullName nvarchar(50) not null,
	Email varchar(60) not null,
	Phone varchar(15) not null,
	UserName varchar(50) not null,
	IdUserGroup int not null,
	Password varchar(70) not null,
	Status bit not null DEFAULT 1,
	CONSTRAINT PK_IdUser PRIMARY KEY (IdUser),
	CONSTRAINT Unique_Email Unique (Email),
	CONSTRAINT Unique_Phone Unique (Phone),
	CONSTRAINT Unique_UserName Unique (UserName),
);

create table UserGroup(
	IdUserGroup int not null identity(1,1),
	NameUserGroup nvarchar(30),
	CONSTRAINT PK_IdUserGroup PRIMARY KEY (IdUserGroup)
);

create table Achievements(
	IdAchievement int not null identity(1,1),
	IdUser int not null,
	IdEvent int not null,
	Describe nvarchar(120) not null,
	CONSTRAINT PK_IdAchievement PRIMARY KEY (IdAchievement)
);

create table Tips(
	IdTip int not null identity(1,1),
	NameTip nvarchar(60) not null,
	Img varchar(150) not null,
	Describe nvarchar(max),
	CONSTRAINT PK_IdTip PRIMARY KEY (IdTip)
);

create table Feedback(
	IdUser int not null,
	message nvarchar(max)
);

create table Follow(
	IdUser int not null,
	IdFollowing int not null,
	CONSTRAINT PK_Follow PRIMARY KEY (IdUser, IdFollowing)
);

ALTER TABLE CookingRecipe
ADD CONSTRAINT FK_CookingRecipeIdUser FOREIGN KEY (IdUser) REFERENCES Users(IdUser);
ALTER TABLE CookingRecipe
ADD CONSTRAINT FK_CookingRecipeIdEvent FOREIGN KEY (IdEvent) REFERENCES Event(IdEvent);

ALTER TABLE DetailsEvent
ADD CONSTRAINT FK_DetailsEventIdEvent FOREIGN KEY (IdEvent) REFERENCES Event(IdEvent);
ALTER TABLE DetailsEvent
ADD CONSTRAINT FK_DetailsEventIdUser FOREIGN KEY (IdUser) REFERENCES Users(IdUser);
ALTER TABLE DetailsEvent
ADD CONSTRAINT FK_DetailsEventIdRecipe FOREIGN KEY (IdRecipe) REFERENCES CookingRecipe(IdRecipe);

ALTER TABLE Comment
ADD CONSTRAINT FK_CommentIdUser FOREIGN KEY (IdUser) REFERENCES Users(IdUser);
ALTER TABLE Comment
ADD CONSTRAINT FK_CommentIdRecipe FOREIGN KEY (IdRecipe) REFERENCES CookingRecipe(IdRecipe);

ALTER TABLE Users
ADD CONSTRAINT FK_UsersIdUserGroup FOREIGN KEY (IdUserGroup) REFERENCES UserGroup(IdUserGroup);

ALTER TABLE Achievements
ADD CONSTRAINT FK_AchievementsIdUser FOREIGN KEY (IdUser) REFERENCES Users(IdUser);
ALTER TABLE Achievements
ADD CONSTRAINT FK_AchievementsIdEvent FOREIGN KEY (IdEvent) REFERENCES Event(IdEvent);

ALTER TABLE Feedback
ADD CONSTRAINT FK_FeedbackIdUser FOREIGN KEY (IdUser) REFERENCES Users(IdUser);

ALTER TABLE Follow
ADD CONSTRAINT FK_FollowIdUser FOREIGN KEY (IdUser) REFERENCES Users(IdUser);
ALTER TABLE Follow
ADD CONSTRAINT FK_Following FOREIGN KEY (IdFollowing) REFERENCES Users(IdUser);
