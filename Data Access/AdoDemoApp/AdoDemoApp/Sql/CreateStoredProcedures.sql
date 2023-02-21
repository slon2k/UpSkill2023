CREATE PROCEDURE dbo.sp_GetStudents
AS
	SELECT Id, HouseId, FirstName, LastName FROM dbo.Student
GO

CREATE PROCEDURE dbo.sp_GetStudent(@id INT)
AS
	SELECT Id, HouseId, FirstName, LastName FROM dbo.Student
	WHERE Student.Id = @id
GO

CREATE PROCEDURE dbo.sp_DeleteStudent(@id INT)
AS
	DELETE FROM dbo.Student
	WHERE Student.Id = @id
GO

CREATE PROCEDURE dbo.sp_UpdateStudent(
	@id INT, @firstName NVARCHAR(100), @lastName NVARCHAR(100), @houseId INT
)
AS
	UPDATE dbo.Student
	SET FirstName = @firstName, LastName = @lastName, HouseId = @houseId
	WHERE Student.Id = @id
GO

CREATE PROCEDURE dbo.sp_CreateStudent(
	@firstName NVARCHAR(100), @lastName NVARCHAR(100), @houseId INT
)
AS
	INSERT INTO dbo.Student(FirstName, LastName, HouseId)
	VALUES (@firstName, @lastName, @houseId)
GO

CREATE PROCEDURE dbo.sp_FindStudentByName(@name NVARCHAR(100))
AS
	SELECT Id, HouseId, FirstName, LastName FROM dbo.Student
	WHERE FirstName LIKE '%'+@name+'%' OR LastName LIKE '%'+@name+'%'
GO