use Timetable
go


insert into GeneralType (TypeName)
values
('Sample1'),
('Sample2')

go

INSERT INTO Student (FirstName, LastName)
VALUES 
('Amanda' , 'Haaloli'),
('Bobre' , 'Hohebrückli'),
('Fruki' , 'Kolikol'),
('Ruslet' , 'Stande'),
('Komat' , 'Lopito'),
('Nutim' , 'Xavern'),
('Derla' , 'Haaloli'),
('Rick' , 'Stansfildes'),
('Ruslina' , 'Standitok'),
('Arelenia' , 'Frutzlicham'),
('Josisif' , 'Fernasifon'),
('Hulinberg' , 'Guligum'),
('Naziedno' , 'Danielisimo'),
('Wedruno' , 'Quatvares')
---

SELECT '/'

INSERT INTO Teacher (FirstName, LastName, TeacherAvailability, fk_TypeId)
VALUES 
('Amfqwdqanda' , 'mnbvcds', 'A1,A2', 1),
('qfwfwqf' , 'jhgfrewggfd', 'A1,A2', 1),
('u5u4h4w' , 'lkjuztred', 'A1,A2', 1),
('ergrgew' , 'poiuztgbvd', 'A1,A2', 1),
('thtrhwt' , 'qwsdcxvf', 'A1,A2', 1),
('erhwerer' , 'nbfrfg', 'A1,A2', 1),
('erhw' , 'mnbgj', 'A1,A2', 1),
('rwz4b' , 'plkmjujh', 'A1,A2', 1),
('Ruslmeoeina' , 'sqssmn', 'A1,A2', 1),
('srtnu' , 'bdkegjf', 'A1,A2', 1),
('erace' , 'ihfjwe', 'A1,A2', 1),
('pimnberg' , 'oqjfljwn', 'A1,A2', 1),
('qewtv' , 'wojorj', 'A1,A2', 1),
('n6bg54' , 'jidjqwk', 'A1,A2', 1)


insert into Education (EducationName, fk_TypeId)
values
('IMS', 1)

INSERT INTO Class (ClassName, fk_EducationId)
VALUES 
('A21a', 1),
('A21b', 1),
('A21c', 1),
('B22a', 1),
('B22b', 1),
('B22c', 1),
('B23a', 1),
('B23b', 1),
('C21a', 1),
('C22a', 1),
('C23a', 1),
('C23b', 1),
('D21a', 1),
('D22a', 1),
('D23a', 1),
('D24a', 1)


INSERT INTO SchoolSubject (SubjectName, fk_TypeId)
VALUES 
('Mathematics', 1),
('German', 1),
('English', 1),
('French', 1),
('Spanish', 1),
('Biology', 1),
('Geography', 1),
('Science', 1),
('Social Studies', 1),
('Art', 1),
('Music', 1),
('Sport', 1),
('Literature', 1),
('Psychology', 1),
('Chemistry ', 1),
('Information Technology', 1)


INSERT INTO Room (RoomName, fk_TypeId)
VALUES 
('S-0.15', 1),
('S-0.16', 1),
('S-0.17', 1),
('S-0.18', 1),
('S-0.19', 1),
('S-0.20', 1),
('S-1.31', 1),
('S-1.32', 1),
('S-1.33', 1),
('S-1.34', 1),
('S-1.35', 1),
('S-2.50', 1),
('S-2.51', 1),
('S-2.52', 1),
('S-2.53', 1),
('S-2.56', 1),
('S-3.60', 1),
('S-3.65', 1),
('S-3.68', 1),
('S-3.62', 1),
('S-3.69', 1),
('S-4.3', 1),
('S-4.2', 1),
('S-4.1', 1)

INSERT INTO SchoolSubject(SubjectName, fk_TypeId)
values 
('Test', 1),
('Test2', 1)

INSERT INTO Lesson (LessonName, LessonDay, LessonStartTime, LessonEndTime, fk_SchoolSubjectID, fk_TeacherID, fk_ClassID, fk_RoomID) 
VALUES 
('A1',  'Monday',    '8:00', '8:00', 1, 1, 1, 1),
('A2',  'Monday',    '8:00', '8:00', 1, 1, 1, 1),
('A3',  'Monday',    '8:00', '8:00', 1, 1, 1, 1),
('A4',  'Monday',    '8:00', '8:00', 1, 1, 1, 1),
('A5',  'Monday',    '8:00', '8:00', 1, 1, 1, 1),
('A6',  'Monday',    '8:00', '8:00', 1, 1, 1, 1),
('A7',  'Monday',    '8:00', '8:00', 1, 1, 1, 1),
('A8',  'Monday',    '8:00', '8:00', 1, 1, 1, 1),
('A9',  'Monday',    '8:00', '8:00', 1, 1, 1, 1),
('A10', 'Monday',    '8:00', '8:00', 1, 1, 1, 1),
('A11', 'Monday',    '8:00', '8:00', 1, 1, 1, 1),

('B1',  'Tuesday',   '8:00', '8:00', 1, 1, 1, 1),
('B2',  'Tuesday',   '8:00', '8:00', 1, 1, 1, 1),
('B3',  'Tuesday',   '8:00', '8:00', 1, 1, 1, 1),
('B5',  'Tuesday',   '8:00', '8:00', 1, 1, 1, 1),
('B6',  'Tuesday',   '8:00', '8:00', 1, 1, 1, 1),
('B7',  'Tuesday',   '8:00', '8:00', 1, 1, 1, 1),
('B8',  'Tuesday',   '8:00', '8:00', 1, 1, 1, 1),
('B9',  'Tuesday',   '8:00', '8:00', 1, 1, 1, 1),
('B10', 'Tuesday',   '8:00', '8:00', 1, 1, 1, 1),
('B11', 'Tuesday',   '8:00', '8:00', 1, 1, 1, 1),

('C1',  'Wednesday', '8:00', '8:00', 1, 1, 1, 1),
('C2',  'Wednesday', '8:00', '8:00', 1, 1, 1, 1),
('C3',  'Wednesday', '8:00', '8:00', 1, 1, 1, 1),
('C4',  'Wednesday', '8:00', '8:00', 1, 1, 1, 1),
('C5',  'Wednesday', '8:00', '8:00', 1, 1, 1, 1),
('C6',  'Wednesday', '8:00', '8:00', 1, 1, 1, 1),
('C7',  'Wednesday', '8:00', '8:00', 1, 1, 1, 1),
('C8',  'Wednesday', '8:00', '8:00', 1, 1, 1, 1),
('C9',  'Wednesday', '8:00', '8:00', 1, 1, 1, 1),
('C10', 'Wednesday', '8:00', '8:00', 1, 1, 1, 1),
('C11', 'Wednesday', '8:00', '8:00', 1, 1, 1, 1),

('D1',  'Thursday', '8:00', '8:00', 1, 1, 1, 1),
('D2',  'Thursday', '8:00', '8:00', 1, 1, 1, 1),
('D3',  'Thursday', '8:00', '8:00', 1, 1, 1, 1),
('D4',  'Thursday', '8:00', '8:00', 1, 1, 1, 1),
('D5',  'Thursday', '8:00', '8:00', 1, 1, 1, 1),
('D6',  'Thursday', '8:00', '8:00', 1, 1, 1, 1),
('D7',  'Thursday', '8:00', '8:00', 1, 1, 1, 1),
('D8',  'Thursday', '8:00', '8:00', 1, 1, 1, 1),
('D9',  'Thursday', '8:00', '8:00', 1, 1, 1, 1),
('D10', 'Thursday', '8:00', '8:00', 1, 1, 1, 1),
('D11', 'Thursday', '8:00', '8:00', 1, 1, 1, 1),

('E1',  'Friday',   '8:00', '8:00', 1, 1, 1, 1),
('E2',  'Friday',   '8:00', '8:00', 1, 1, 1, 1),
('E3',  'Friday',   '8:00', '8:00', 1, 1, 1, 1),
('E4',  'Friday',   '8:00', '8:00', 1, 1, 1, 1),
('E5',  'Friday',   '8:00', '8:00', 1, 1, 1, 1),
('E6',  'Friday',   '8:00', '8:00', 1, 1, 1, 1),
('E7',  'Friday',   '8:00', '8:00', 1, 1, 1, 1),
('E8',  'Friday',   '8:00', '8:00', 1, 1, 1, 1),
('E9',  'Friday',   '8:00', '8:00', 1, 1, 1, 1),
('E10', 'Friday',   '8:00', '8:00', 1, 1, 1, 1),
('E11', 'Friday',   '8:00', '8:00', 1, 1, 1, 1)

INSERT INTO teaches (fk_TeacherID, fk_SchoolSubjectID)
VALUES (1, 1)

select * from Student
select * from Teacher
select * from teaches
select * from Lesson
select * from Class
select * from SchoolSubject
select * from Room