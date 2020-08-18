CREATE DATABASE HotelService;
GO

use HotelService;

GO

DROP TABLE IF EXISTS tblUser;
DROP TABLE IF EXISTS tblManager;
DROP TABLE IF EXISTS tblEmployee;

 CREATE TABLE tblUser (
    UserID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    FullName varchar(50),
	DateOfBirth date,
	Email varchar(50),
	UserName varchar(50),
	Password varchar(50)	
);

 CREATE TABLE tblManager (
    ManagerID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    FloorNumber int,
	WorkExperience int,
    LevelOfEducation varchar(50),   
	UserID int FOREIGN KEY REFERENCES tblUser(UserID)
);


 CREATE TABLE tblEmployee (
    EmployeeID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	FloorNumber int,
	Gender varchar(50),
	Citizenship varchar(50),
	WorkType varchar(50),
	Salary decimal,
	UserID int FOREIGN KEY REFERENCES tblUser(UserID)
);

GO

CREATE VIEW vwManager AS
	SELECT	tblUser.*, 
			tblManager.ManagerID, tblManager.FloorNumber, tblManager.WorkExperience, tblManager.LevelOfEducation
	FROM	tblUser, tblManager
	WHERE	tblUser.UserID = tblManager.UserID
	
GO

CREATE VIEW vwEmployee AS
	SELECT tblUser.*,
		   tblEmployee.EmployeeID, tblEmployee.FloorNumber, tblEmployee.Gender, tblEmployee.Citizenship, tblEmployee.WorkType,
		   tblEmployee.Salary
	FROM   tblUser, tblEmployee
	WHERE  tblUser.UserID = tblEmployee.UserID