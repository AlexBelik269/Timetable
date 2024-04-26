USE master
GO

DROP DATABASE IF EXISTS Timetable
GO

CREATE DATABASE Timetable
GO

USE Timetable
GO


CREATE TABLE GeneralType(
  TypeId int identity,
  TypeName varchar(50),
  primary key (TypeId)
);

go

CREATE TABLE SchoolSubject (
	SchoolSubjectID INT IDENTITY,
	SubjectName VARCHAR(30),
	fk_TypeId int,
	PRIMARY KEY (SchoolSubjectID),
  foreign key (fk_TypeId) references GeneralType(TypeId)
);

CREATE TABLE Room (
	RoomID INT IDENTITY,
	RoomName VARCHAR(30),
	fk_TypeId int,
	PRIMARY KEY (RoomID),
  foreign key (fk_TypeId) references GeneralType(TypeId)
);


go

CREATE TABLE Education (
  EducationId int identity, 
  EducationName varchar(50),
  fk_TypeId int,
  primary key (EducationId),
  foreign key (fk_TypeId) references GeneralType(TypeId)
);

go

CREATE TABLE EducationHasLesson(
  EducationHasLessonId int identity,
  fk_EducationId int,
  fk_SchoolSubjectId int,
  Amount int,
  primary key (EducationHasLessonId),
  FOREIGN KEY (fk_SchoolSubjectID) REFERENCES SchoolSubject(SchoolSubjectID),
  FOREIGN KEY (fk_EducationId) REFERENCES Education(EducationId)
);

go

CREATE TABLE Class (
	ClassID INT IDENTITY,
	ClassName VARCHAR(30),
	EducationYear int,
	Mandatory int,
	ClassAvailability varchar(4),
  fk_EducationId int,
	PRIMARY KEY (ClassID),
  foreign key (fk_EducationId) references Education(EducationId)
);
go

CREATE TABLE Student (
    StudentID INT IDENTITY,
    FirstName VARCHAR(30),
    LastName VARCHAR(30),
	  fk_ClassID INT,
    PRIMARY KEY (StudentID),
    FOREIGN KEY (fk_ClassID) REFERENCES Class(ClassID)
);

CREATE TABLE Teacher (
	TeacherID INT IDENTITY,
	FirstName VARCHAR(30),
  LastName VARCHAR(30),
	TeacherAvailability varchar(400),
  fk_TypeId int,
	PRIMARY KEY (TeacherID),
  foreign key (fk_TypeId) references GeneralType(TypeId)
);
go

CREATE TABLE teaches (
    teachesID INT IDENTITY,
    fk_TeacherID INT,
    fk_SchoolSubjectID INT,
    PRIMARY KEY (teachesID),
    FOREIGN KEY (fk_SchoolSubjectID) REFERENCES SchoolSubject(SchoolSubjectID),
	  FOREIGN KEY (fk_TeacherID) REFERENCES Teacher(TeacherID)
);
go

CREATE TABLE Lesson(
	LessonID INT IDENTITY,
	LessonName varchar(30),
	LessonDay varchar(20),
	LessonStartTime VARCHAR(20),
	LessonEndTime VARCHAR(20),
	fk_SchoolSubjectID INT,
	fk_TeacherID INT,
	fk_ClassID INT,
	fk_RoomID INT,
	PRIMARY KEY (LessonID),
  FOREIGN KEY (fk_SchoolSubjectID) REFERENCES SchoolSubject(SchoolSubjectID),
	FOREIGN KEY (fk_TeacherID) REFERENCES Teacher(TeacherID),
	FOREIGN KEY (fk_ClassID) REFERENCES Class(ClassID),
	FOREIGN KEY (fk_RoomID) REFERENCES Room(RoomID)
);

go
