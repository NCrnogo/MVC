CREATE PROC createUser
@name varchar(50),
@roll int,
@salt varchar(36),
@date char(10),
@pwd varchar(50),
@id int output
as
INSERT INTO Users 
VALUES (HASHBYTES('SHA2_512', @pwd),@salt,@name,@date)
SET @id=SCOPE_IDENTITY()
INSERT INTO UserRollMappings VALUES (@id,@roll)
GO

Exec createUser @name='Ivo',@roll=1,@date='2022-02-03',@pwd='12345',@id=1

SELECT * FROM Users

CREATE PROC getUsers
AS
	SELECT Users.*,UR.Roll AS 'Roll' FROM Users 
	LEFT JOIN UserRollMappings as URM ON URM.UserFK = Users.IDUser
	LEFT JOIN UserRoles as UR ON UR.IDUserRole = URM.UserRoleFK
GO

CREATE PROC getUser
@id int
AS
	SELECT Users.*,UR.Roll AS 'Roll' FROM Users 
	LEFT JOIN UserRollMappings as URM ON URM.UserFK = Users.IDUser
	LEFT JOIN UserRoles as UR ON UR.IDUserRole = URM.UserRoleFK
	WHERE Users.IDUser = @id
GO

EXEC getUser @id=2


CREATE PROC getTeams
AS
	SELECT * FROM Teams
GO


CREATE PROC getTeam
@id int
AS
	SELECT * FROM Teams
	WHERE Teams.IDTeam = @id
GO

CREATE PROC CheckLogin
@name varchar(50),
@pwd varchar(50)
AS
SELECT * FROM Users as s Where s.LoginName=@name AND HASHBYTES('SHA2_512', @pwd) = s.PasswordHash
GO

CREATE PROC updateUser
@name varchar(50),
@id int
AS
UPDATE Users
SET LoginName=@name
WHERE IDUser =@id
GO

CREATE PROC getTeamsByUser
	@id int
AS
	SELECT * FROM Teams as t
	RIGHT JOIN TeamMembers as tm ON tm.TeamID = t.IDTeam
	WHERE t.OwnerID = @id OR tm.UserID = @id
GO
--User sends request to join to a team
CREATE PROC joinRequestUser
	@teamName nvarchar(50),
	@userId int
AS
	INSERT INTO TeamInvites VALUES ((Select IDTeam from Teams where Team like @teamName),@userId,0)
GO
--Team sends invite to user
CREATE PROC joinRequestTeam
	@teamid int,
	@userName nvarchar(100)
AS
	INSERT INTO TeamInvites VALUES (@teamid,(Select IDUser from Users where LoginName like @userName),1)
GO

CREATE PROC createTeam
	@idUser int,
	@name varchar(50),
	@created varchar(50)
AS
INSERT INTO Teams VALUES(@name,@created,@idUser,NULL)
GO

CREATE PROC getTeamInvites
	@idUser int
AS
SELECT DISTINCT TeamID, UserID, Invited, t.Team FROM TeamInvites 
INNER JOIN Teams as t on TeamID =t.IDTeam
WHERE UserID=@idUser AND Invited=1 
GO

CREATE PROC joinTeam
	@idUser int,
	@teamName varchar(50)
	AS
	INSERT INTO TeamMembers VALUES((SELECT IDTeam FROM Teams WHERE Team=@teamName),@idUser)
	DELETE FROM TeamInvites WHERE TeamID = (SELECT IDTeam FROM Teams WHERE Team=@teamName) AND UserID=@idUser
GO

CREATE PROC dismissJoinTeam
	@idUser int,
	@teamName varchar(50)
	AS
	DELETE FROM TeamInvites WHERE TeamID = (SELECT IDTeam FROM Teams WHERE Team=@teamName) AND UserID=@idUser
GO