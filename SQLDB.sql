use master
go

--YYYY-MM-DD date format
--hh:mm:ss time 
create database TeamyDB
go

use TeamyDB
go

create table Users(
	IDUser int primary key identity(1,1),
	[PasswordHash] [binary](64) NOT NULL,
	[Salt] [nvarchar](36) NOT NULL,
	LoginName nvarchar(50) unique not null, --email
	DateCreated char(10) not null, --YYYY-MM-DD date format
)
go

CREATE table Teams
(
	IDTeam int primary key identity(1,1),
	Team nvarchar(50) not null,
	Created char(10) not null,
	OwnerID int FOREIGN KEY REFERENCES Users(IDUser) not null,
	TeacherID int FOREIGN KEY REFERENCES Users(IDUser)
)
go

CREATE TABLE TeamMembers
(
	IDTeamMembers int not null identity(1,1) primary key,
	TeamID int FOREIGN KEY REFERENCES Teams(IDTeam) not null,
	UserID int FOREIGN KEY REFERENCES Users(IDUser) not null
);
go

CREATE TABLE TeamInvites(
	IDTeamInvites int not null identity(1,1) primary key,
	TeamID int FOREIGN KEY REFERENCES Teams(IDTeam) not null,
	UserID int FOREIGN KEY REFERENCES Users(IDUser) not null,
	Invited int
)
go

create table UserRoles(
	IDUserRole int primary key identity(1,1),
	Roll nvarchar(50) not null
)
go

create table UserRollMappings(
	IDUserRoleMapping int primary key identity(1,1),
	UserFK int foreign key references Users(IDUser) not null,
	UserRoleFK int foreign key references UserRoles(IDUserRole) not null
)
go

create table Dailys(
	IDDaily int primary key identity(1,1),
	DateCreated char(10) not null, --YYYY-MM-DD date format
	Details nvarchar(500),
	UserFK int foreign key references Users(IDUser)
)
go

create table Activities(
	IDActivities int primary key identity(1,1),
	"Start" char(8) not null, --hh:mm:ss
	"End" char(8),
	DailyFK int foreign key references Dailys(IDDaily)
)
go

create table Projects(
	IDProject int primary key identity(1,1),
	"Project" nvarchar(50) not null,
	Created char(10) not null,
	TeamLeadFK int foreign key references Users(IDUser) 
)
go

create table ProjectUserMappings(
	IDProjectUserMapping int primary key identity(1,1),
	UserFK int foreign key references Users(IDUser),
	ProjectFK int foreign key references Projects(IDProject)
)
go
