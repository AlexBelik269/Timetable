using Microsoft.Win32;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using TimetableWPF.Classes;

namespace TimetableWPF
{
    internal class SQLHandler
    {
        private static SqlConnection s_cnn = new SqlConnection();
        public static List<Student> _Students = new List<Student>();
        public static List<Teacher> _Teachers = new List<Teacher>();
        public static List<Class> _Classes = new List<Class>();
        public static List<Education> _Educations = new List<Education>();
        public static List<SchoolSubject> _SchoolSubjects = new List<SchoolSubject>();
        public static List<Lesson> _Lessons = new List<Lesson>();
        public static List<string> _Types = new List<string>();
        public static bool VerbindungHerstellen()
        {
            //s_cnn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0";  //altes Format mdb
            s_cnn.ConnectionString = "Database=Timetable";   //neues Format accdb
            s_cnn.ConnectionString += ";User ID=MyLogin";
            s_cnn.ConnectionString += ";PASSWORD=123";
            s_cnn.ConnectionString += ";Server=DESKTOP-4OLLUMT"; //Change Server Name to your Server Name


            try
            {
                s_cnn.Open();
                return true;
            }
            catch (SqlException err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                if (s_cnn.State == ConnectionState.Open)
                    s_cnn.Close();
            }
            return false; //Verbindung konnte nicht hergestellt werden
        }

        public static void AllesEinlesen()
        {
            LeseAlleStudentsEin();


            LeseAlleTeacherEin();
            LeseAlleTeachAvailabilityEin();
            LeseAlleClassEin();
            LeseAlleEducationEin();
            LeseAlleEducationLessonsEin();
            LeseAlleSchoolSubjectEin();
            LeseAlleRoomEin();
            LeseAlleLessonEin();
            LeseAlleTypeEin();
        }

        public static async void LeseAlleStudentsEin()
        {

            try
            {
                if (s_cnn.State != ConnectionState.Open)
                    s_cnn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Student";
                cmd.Connection = s_cnn;
                SqlDataReader myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    Student h = new Student();
                    h._Name = (Convert.ToString(myReader["Firstname"])) + " " + Convert.ToString((myReader["LastName"]));
                    _Students.Add(h);
                }
                myReader.Close();
            }
            catch (SqlException err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                if (s_cnn.State == ConnectionState.Open)
                    s_cnn.Close();
            }
        }

        public static async void LeseAlleTeacherEin()
        {

            try
            {
                if (s_cnn.State != ConnectionState.Open)
                    s_cnn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select FirstName + ' ' + LastName as 'Name', TeacherAvailability, GeneralType.TypeName as 'Type' from Teacher join GeneralType on fk_TypeId = TypeId";
                cmd.Connection = s_cnn;
                SqlDataReader myReader = cmd.ExecuteReader();

                while (myReader.Read())
                {
                    Teacher h = new Teacher();
                    h._Name = (Convert.ToString(myReader["Name"]));
                    h._Availability = (Convert.ToString(myReader["TeacherAvailability"]).Split(",").ToList<string>());
                    h._Type = (Convert.ToString(myReader["Type"]));
                    _Teachers.Add(h);   
                }
                myReader.Close();
            }
            catch (SqlException err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                if (s_cnn.State == ConnectionState.Open)
                    s_cnn.Close();
            }
        }

        public static async void LeseAlleTeachAvailabilityEin()
        {

            try
            {
                if (s_cnn.State != ConnectionState.Open)
                    s_cnn.Open();
                SqlCommand cmd1 = new SqlCommand();
                cmd1.CommandText = "select Teacher.FirstName + ' ' + Teacher.LastName as 'Teacher', SchoolSubject.SubjectName as 'Subject' from teaches join Teacher on fk_TeacherId = TeacherID join SchoolSubject on fk_SchoolSubjectID = SchoolSubjectID";
                cmd1.Connection = s_cnn;
                SqlDataReader myReader1 = cmd1.ExecuteReader();

                foreach(Teacher t in _Teachers)
                {
                    t._TeachesSubject = new List<string>();
                }

                while (myReader1.Read())
                {
                    _Teachers.Find(e => e._Name == (Convert.ToString(myReader1["Teacher"])))._TeachesSubject.Add(Convert.ToString(myReader1["Subject"]));
                }
                myReader1.Close();
            }
            catch (SqlException err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                if (s_cnn.State == ConnectionState.Open)
                    s_cnn.Close();
            }
        }

        public static async void LeseAlleClassEin()
        {

            try
            {
                if (s_cnn.State != ConnectionState.Open)
                    s_cnn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select ClassName, EducationYear, Mandatory, ClassAvailability, Education.EducationName as 'Education' from Class join Education on fk_EducationId = EducationId";
                cmd.Connection = s_cnn;
                SqlDataReader myReader = cmd.ExecuteReader();

                while (myReader.Read())
                {
                    Class h = new Class();
                    if(!myReader.IsDBNull("ClassName")) h._Name = (Convert.ToString(myReader["ClassName"]));
                    if (!myReader.IsDBNull("EducationYear")) h._EducationYear = (Convert.ToInt32(myReader["EducationYear"]));
                    if (!myReader.IsDBNull("Mandatory")) h._IsMandatory = (Convert.ToBoolean(myReader["Mandatory"]));
                    if (!myReader.IsDBNull("ClassAvailability")) h._Availability = (Convert.ToString(myReader["ClassAvailability"]).Split(",").ToList<string>());
                    if (!myReader.IsDBNull("Education")) h._Education = (Convert.ToString(myReader["Education"]));
                    _Classes.Add(h);
                }
                myReader.Close();
            }
            catch (SqlException err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                if (s_cnn.State == ConnectionState.Open)
                    s_cnn.Close();
            }
        }

        public static async void LeseAlleEducationEin()
        {

            try
            {
                if (s_cnn.State != ConnectionState.Open)
                    s_cnn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select EducationName, GeneralType.TypeName as 'Type' from Education join GeneralType on fk_TypeId = TypeId";
                cmd.Connection = s_cnn;
                SqlDataReader myReader = cmd.ExecuteReader();

                while (myReader.Read())
                {
                    Education h = new Education();
                    if (!myReader.IsDBNull("EducationName")) h._Name = (Convert.ToString(myReader["EducationName"]));
                    if (!myReader.IsDBNull("Type")) h._Type = (Convert.ToString(myReader["Type"]));

                    _Educations.Add(h);
                }
                myReader.Close();
            }
            catch (SqlException err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                if (s_cnn.State == ConnectionState.Open)
                    s_cnn.Close();
            }
        }

        public static async void LeseAlleEducationLessonsEin()
        {

            try
            {
                if (s_cnn.State != ConnectionState.Open)
                    s_cnn.Open();
                SqlCommand cmd1 = new SqlCommand();
                cmd1.CommandText = "\r\nselect Education.EducationName as 'Education', SchoolSubject.SubjectName as 'Subject' from EducationHasLesson \r\njoin SchoolSubject on fk_SchoolSubjectId = SchoolSubjectID\r\njoin Education on fk_EducationId = EducationId";
                cmd1.Connection = s_cnn;
                SqlDataReader myReader1 = cmd1.ExecuteReader();

                foreach (Education t in _Educations)
                {
                    t._Subjects = new List<string>();
                }

                while (myReader1.Read())
                {
                    _Educations.Find(e => e._Name == (Convert.ToString(myReader1["Education"])))._Subjects.Add(Convert.ToString(myReader1["Subject"]));
                }
                myReader1.Close();
            }
            catch (SqlException err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                if (s_cnn.State == ConnectionState.Open)
                    s_cnn.Close();
            }
        }

        public static async void LeseAlleSchoolSubjectEin()
        {

            try
            {
                if (s_cnn.State != ConnectionState.Open)
                    s_cnn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select EducationName, GeneralType.TypeName as 'Type' from Education join GeneralType on fk_TypeId = TypeId";
                cmd.Connection = s_cnn;
                SqlDataReader myReader = cmd.ExecuteReader();

                while (myReader.Read())
                {
                    SchoolSubject h = new SchoolSubject();
                    if (!myReader.IsDBNull("EducationName")) h._Name = (Convert.ToString(myReader["EducationName"]));
                    if (!myReader.IsDBNull("Type")) h._Type = (Convert.ToString(myReader["Type"]));

                    _SchoolSubjects.Add(h);
                }
                myReader.Close();
            }
            catch (SqlException err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                if (s_cnn.State == ConnectionState.Open)
                    s_cnn.Close();
            }
        }

        public static async void LeseAlleRoomEin()
        {

            try
            {
                if (s_cnn.State != ConnectionState.Open)
                    s_cnn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select RoomName, GeneralType.TypeName as 'Type' from Room join GeneralType on fk_TypeId = TypeId";
                cmd.Connection = s_cnn;
                SqlDataReader myReader = cmd.ExecuteReader();

                while (myReader.Read())
                {
                    SchoolSubject h = new SchoolSubject();
                    if (!myReader.IsDBNull("RoomName")) h._Name = (Convert.ToString(myReader["RoomName"]));
                    if (!myReader.IsDBNull("Type")) h._Type = (Convert.ToString(myReader["Type"]));

                    _SchoolSubjects.Add(h);
                }
                myReader.Close();
            }
            catch (SqlException err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                if (s_cnn.State == ConnectionState.Open)
                    s_cnn.Close();
            }
        }

        public static async void LeseAlleLessonEin()
        {

            try
            {
                if (s_cnn.State != ConnectionState.Open)
                    s_cnn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select LessonName, LessonDay, LessonStartTime, LessonEndTime, SchoolSubject.SubjectName as 'Subject', Teacher.FirstName + ' ' + Teacher.LastName as 'Teacher', Class.ClassName as 'Class', Room.RoomName as 'Room' from Lesson\r\njoin Room on fk_RoomID = RoomID\r\njoin SchoolSubject on fk_SchoolSubjectID = SchoolSubjectID\r\njoin Teacher on fk_TeacherID = TeacherID\r\njoin Class on fk_ClassID = ClassID";
                cmd.Connection = s_cnn;
                SqlDataReader myReader = cmd.ExecuteReader();

                while (myReader.Read())
                {
                    Lesson h = new Lesson();
                    if (!myReader.IsDBNull("LessonName")) h._Name = (Convert.ToString(myReader["LessonName"]));
                    if (!myReader.IsDBNull("LessonDay")) h._LessonDay = (Convert.ToString(myReader["LessonDay"]));
                    if (!myReader.IsDBNull("LessonStartTime")) h._LessonStartTime = (Convert.ToString(myReader["LessonStartTime"]));
                    if (!myReader.IsDBNull("LessonEndTime")) h._LessonEndTime = (Convert.ToString(myReader["LessonEndTime"]));
                    if (!myReader.IsDBNull("Subject")) h._SchoolSubject = (Convert.ToString(myReader["Subject"]));
                    if (!myReader.IsDBNull("Teacher")) h._Lehrer = (Convert.ToString(myReader["Teacher"]));
                    if (!myReader.IsDBNull("Class")) h._Class = (Convert.ToString(myReader["Class"]));
                    if (!myReader.IsDBNull("Room")) h._Room = (Convert.ToString(myReader["Room"]));


                    _Lessons.Add(h);
                }
                myReader.Close();
            }
            catch (SqlException err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                if (s_cnn.State == ConnectionState.Open)
                    s_cnn.Close();
            }
        }

		public static async void LeseAlleTypeEin()
		{

			try
			{
				if (s_cnn.State != ConnectionState.Open)
					s_cnn.Open();
				SqlCommand cmd = new SqlCommand();
				cmd.CommandText = "select TypeName from GeneralType";
				cmd.Connection = s_cnn;
				SqlDataReader myReader = cmd.ExecuteReader();

				while (myReader.Read())
				{
                    string h = "";
					if (!myReader.IsDBNull("TypeName")) h = (Convert.ToString(myReader["TypeName"]));
					_Types.Add(h);
				}
				myReader.Close();
			}
			catch (SqlException err)
			{
				MessageBox.Show(err.Message);
			}
			finally
			{
				if (s_cnn.State == ConnectionState.Open)
					s_cnn.Close();
			}
		}
        //Done + Test
		public static async Task<string> InsertForRoom(string name, string type)
        {

            try
            {
                if (s_cnn.State != ConnectionState.Open)
                    s_cnn.Open();
                SqlCommand cmd = new SqlCommand();
				cmd.Connection = s_cnn;

				cmd.CommandText = "select TypeId from GeneralType where TypeName = @TypeName";
				cmd.Parameters.AddWithValue("@TypeName", type);
				SqlDataReader myReader = cmd.ExecuteReader();

                int SelectedTypeId = 0;

				while (myReader.Read())
				{
					if (!myReader.IsDBNull("TypeId")) SelectedTypeId = (Convert.ToInt32(myReader["TypeId"]));
				}
                myReader.Close();

                if(SelectedTypeId == 0){ return "No type found."; }

                cmd.CommandText = "insert into Room (RoomName, fk_TypeId) values ((@Name), (@Type))";
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Type", SelectedTypeId);
                cmd.ExecuteNonQuery();

                return "";
            }
            catch (SqlException err)
            {
                MessageBox.Show(err.Message);

                return "Error: " + err.Message;
            }
            finally
            {
                if (s_cnn.State == ConnectionState.Open)
                    s_cnn.Close();

            }
        }
        //Done
        public static async Task<string> InsertForSchoolSubject(string name, string type)
        {

            try
            {
                if (s_cnn.State != ConnectionState.Open)
                    s_cnn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = s_cnn;

                cmd.CommandText = "select TypeId from GeneralType where TypeName = @TypeName";
                cmd.Parameters.AddWithValue("@TypeName", type);
                SqlDataReader myReader = cmd.ExecuteReader();

                int SelectedTypeId = 0;

                while (myReader.Read())
                {
                    if (!myReader.IsDBNull("TypeId")) SelectedTypeId = (Convert.ToInt32(myReader["TypeId"]));
                }
                myReader.Close();
                
                if (SelectedTypeId == 0) { return "No type found."; }

                cmd.CommandText = "insert into SchoolSubject (SubjectName, fk_TypeId) values ((@Name), (@Type))";
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Type", SelectedTypeId);
                cmd.ExecuteNonQuery();

                return "";
            }
            catch (SqlException err)
            {
                MessageBox.Show(err.Message);

                return "Error: " + err.Message;
            }
            finally
            {
                if (s_cnn.State == ConnectionState.Open)
                    s_cnn.Close();
            }
        }
        //Works
        public static async Task<string> InsertForTeacher(string name, string availability, string type)
        {

            try
            {

                if (s_cnn.State != ConnectionState.Open)
                    s_cnn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = s_cnn;

                cmd.CommandText = "select TypeId from GeneralType where TypeName = @TypeName";
                cmd.Parameters.AddWithValue("@TypeName", type);
                SqlDataReader myReader = cmd.ExecuteReader();

                int SelectedTypeId = 0;

                while (myReader.Read())
                {
                    if (!myReader.IsDBNull("TypeId")) SelectedTypeId = (Convert.ToInt32(myReader["TypeId"]));
                }
                myReader.Close();

                if (SelectedTypeId == 0) { return "No type found."; }

                cmd.CommandText = "INSERT INTO Teacher (FirstName, LastName, TeacherAvailability, fk_TypeId) VALUES ((@FirstName), (@LastName), (@Availability), (@Type))";
                cmd.Parameters.AddWithValue("@FirstName", (name.Split(" "))[0]);
                cmd.Parameters.AddWithValue("@LastName", (name.Split(" "))[1]);
                cmd.Parameters.AddWithValue("@Availability", availability);
                cmd.Parameters.AddWithValue("@Type", SelectedTypeId);
                cmd.ExecuteNonQuery();

                return "";
            }
            catch (SqlException err)
            {
                MessageBox.Show(err.Message);

                return "Error: " + err.Message;
            }
            finally
            {
                if (s_cnn.State == ConnectionState.Open)
                    s_cnn.Close();
            }
        }
        //Done
        public static async Task<string> InsertForStudent(string name, string className)
        {

            try
            {
                if (s_cnn.State != ConnectionState.Open)
                    s_cnn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = s_cnn;

                cmd.CommandText = "select ClassID from Class where ClassName = @ClassName";
                cmd.Parameters.AddWithValue("@ClassName", className);
                SqlDataReader myReader = cmd.ExecuteReader();
                
                int SelectedClassId = 0;

                while (myReader.Read())
                {
                    if (!myReader.IsDBNull("ClassID")) SelectedClassId = (Convert.ToInt32(myReader["ClassID"]));
                }
                myReader.Close();

                if (SelectedClassId == 0) { return "No Class found."; }

                cmd.CommandText = "insert into Student (FirstName, LastName, fk_ClassID) values ((@FirstName), (@LastName), (@Class))";
                cmd.Parameters.AddWithValue("@FirstName", (name.Split(" "))[0]);
                cmd.Parameters.AddWithValue("@LastName", (name.Split(" "))[1]);
                cmd.Parameters.AddWithValue("@Class", SelectedClassId);
                cmd.ExecuteNonQuery();

                return "";
            }
            catch (SqlException err)
            {
                MessageBox.Show(err.Message);

                return "Error: " + err.Message;
            }
            finally
            {
                if (s_cnn.State == ConnectionState.Open)
                    s_cnn.Close();
            }
        }
        //Works
        public static async Task<string> InsertForClass(string name, int educationYear, bool mandatory, string classAvailability, string education)
        {

            try
            {
                if (s_cnn.State != ConnectionState.Open)
                    s_cnn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = s_cnn;

                cmd.CommandText = "select EducationId from Education where EducationName = (@Education)";
                cmd.Parameters.AddWithValue("@Education", education);
                SqlDataReader myReader = cmd.ExecuteReader();

                int SelectedEducationId = 0;

                while (myReader.Read())
                {
                    if (!myReader.IsDBNull("EducationId")) SelectedEducationId = (Convert.ToInt32(myReader["EducationId"]));
                }
                myReader.Close();

                if (SelectedEducationId == 0) { return "No education found."; }

                cmd.CommandText = "insert into Class (ClassName, EducationYear, Mandatory, ClassAvailability, fk_EducationId) values ((@ClassName), (@EducationYear), (@Mandatory), (@ClassAvailability), (@fk_EducationId))";
                cmd.Parameters.AddWithValue("@ClassName", name);
                cmd.Parameters.AddWithValue("@EducationYear", educationYear);
                cmd.Parameters.AddWithValue("@Mandatory", mandatory);
                cmd.Parameters.AddWithValue("@ClassAvailability", classAvailability);
                cmd.Parameters.AddWithValue("@fk_EducationId", SelectedEducationId);
                cmd.ExecuteNonQuery();

                return "";
            }
            catch (SqlException err)
            {
                MessageBox.Show(err.Message);

                return "Error: " + err.Message;
            }
            finally
            {
                if (s_cnn.State == ConnectionState.Open)
                    s_cnn.Close();
            }
        }
        //Works
        public static async Task<string> InsertForEducation(string name, string type)
        {

            try
            {
                if (s_cnn.State != ConnectionState.Open)
                    s_cnn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = s_cnn;

                cmd.CommandText = "select TypeId from GeneralType where TypeName = @TypeName";
                cmd.Parameters.AddWithValue("@TypeName", type);
                SqlDataReader myReader = cmd.ExecuteReader();

                int SelectedTypeId = 0;

                while (myReader.Read())
                {
                    if (!myReader.IsDBNull("TypeId")) SelectedTypeId = (Convert.ToInt32(myReader["TypeId"]));
                }
                myReader.Close();

                if (SelectedTypeId == 0) { return "No type found."; }

                cmd.CommandText = "insert into Education (EducationName, fk_TypeId) values ((@Name), (@Type))";
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Type", SelectedTypeId);
                cmd.ExecuteNonQuery();

                return "";
            }
            catch (SqlException err)
            {
                MessageBox.Show(err.Message);

                return "Error: " + err.Message;
            }
            finally
            {
                if (s_cnn.State == ConnectionState.Open)
                    s_cnn.Close();
            }
        }
        //Works
        public static async Task<string> InsertForLesson(string name, string day, string starttime, string endtime, string subject, string teacher, string className, string room)
        {

            try
            {
                if (s_cnn.State != ConnectionState.Open)
                    s_cnn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = s_cnn;

                cmd.CommandText = "select SchoolSubjectID from SchoolSubject where SubjectName = @SchoolSubject";
                cmd.Parameters.AddWithValue("@SchoolSubject", subject);
                SqlDataReader myReader = cmd.ExecuteReader();

                int SelectedSubjectId = 0;

                while (myReader.Read())
                {
                    if (!myReader.IsDBNull("SchoolSubjectID")) SelectedSubjectId = (Convert.ToInt32(myReader["SchoolSubjectID"]));
                }
                myReader.Close();

                if (SelectedSubjectId == 0) { return "No subject found."; }

                cmd.CommandText = "select TeacherID from Teacher where FirstName + ' ' + LastName = @Teacher";
                cmd.Parameters.AddWithValue("@Teacher", teacher);
                myReader = cmd.ExecuteReader();

                int SelectedTeacherId = 0;

                while (myReader.Read())
                {
                    if (!myReader.IsDBNull("TeacherID")) SelectedTeacherId = (Convert.ToInt32(myReader["TeacherID"]));
                }
                myReader.Close();

                if (SelectedTeacherId == 0) { return "No teacher found."; }

                cmd.CommandText = "select ClassID from Class where ClassName = @Class";
                cmd.Parameters.AddWithValue("@Class", className);
                myReader = cmd.ExecuteReader();

                int SelectedClassId = 0;

                while (myReader.Read())
                {
                    if (!myReader.IsDBNull("ClassID")) SelectedClassId = (Convert.ToInt32(myReader["ClassID"]));
                }
                myReader.Close();

                if (SelectedClassId == 0) { return "No class found."; }

                cmd.CommandText = "select RoomID from Room where RoomName = @Room";
                cmd.Parameters.AddWithValue("@Room", room);
                myReader = cmd.ExecuteReader();

                int SelectedRoomId = 0;

                while (myReader.Read())
                {
                    if (!myReader.IsDBNull("RoomID")) SelectedRoomId = (Convert.ToInt32(myReader["RoomID"]));
                }
                myReader.Close();

                if (SelectedRoomId == 0) { return "No room found."; }

                cmd.CommandText = "insert into Lesson (LessonName, LessonDay, LessonStartTime, LessonEndTime, fk_SchoolSubjectID, fk_TeacherID, fk_ClassID, fk_RoomID) values ((@Name), (@Day), (@Start), (@End), (@SubjectId), (@TeacherId), (@ClassId), (@RoomId))";
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Day", day);
                cmd.Parameters.AddWithValue("@Start", starttime);
                cmd.Parameters.AddWithValue("@End", endtime);
                cmd.Parameters.AddWithValue("@SubjectId", SelectedSubjectId);
                cmd.Parameters.AddWithValue("@TeacherId", SelectedTeacherId);
                cmd.Parameters.AddWithValue("@ClassId", SelectedClassId);
                cmd.Parameters.AddWithValue("@RoomId", SelectedRoomId);
                cmd.ExecuteNonQuery();

                return "";
            }
            catch (SqlException err)
            {
                MessageBox.Show(err.Message);

                return "Error: " + err.Message;
            }
            finally
            {
                if (s_cnn.State == ConnectionState.Open)
                    s_cnn.Close();
            }
        }
        //Works
        public static async Task<string> InsertEducationHasSubject(string education, string subject, int amount)
        {

            try
            {
                if (s_cnn.State != ConnectionState.Open)
                    s_cnn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = s_cnn;

                cmd.CommandText = "select EducationId from Education where EducationName = @Education";
                cmd.Parameters.AddWithValue("@Education", education);
                SqlDataReader myReader = cmd.ExecuteReader();

                int SelectedEducationId = 0;

                while (myReader.Read())
                {
                    if (!myReader.IsDBNull("EducationId")) SelectedEducationId = (Convert.ToInt32(myReader["EducationId"]));
                }
                myReader.Close();

                if (SelectedEducationId == 0) { return "No education found."; }

                cmd.CommandText = "select SchoolSubjectID from SchoolSubject where SubjectName = @Subject";
                cmd.Parameters.AddWithValue("@Subject", subject);
                myReader = cmd.ExecuteReader();

                int SelectedSubjectId = 0;

                while (myReader.Read())
                {
                    if (!myReader.IsDBNull("SchoolSubjectID")) SelectedSubjectId = (Convert.ToInt32(myReader["SchoolSubjectID"]));
                }
                myReader.Close();

                if (SelectedEducationId == 0) { return "No subject found."; }

                cmd.CommandText = "insert into EducationHasLesson (fk_EducationId, fk_SchoolSubjectId, Amount) values ((@EducationId), (@SubjectId), (@Amount))";
                cmd.Parameters.AddWithValue("@EducationId", SelectedEducationId);
                cmd.Parameters.AddWithValue("@SubjectId", SelectedSubjectId);
                cmd.Parameters.AddWithValue("@Amount", amount);

                cmd.ExecuteNonQuery();

                return "";
            }
            catch (SqlException err)
            {
                MessageBox.Show(err.Message);

                return "Error: " + err.Message;
            }
            finally
            {
                if (s_cnn.State == ConnectionState.Open)
                    s_cnn.Close();
            }
        }
        //Works
        public static async Task<string> InsertTeaches(string teacher, string subject)
        {

            try
            {
                if (s_cnn.State != ConnectionState.Open)
                    s_cnn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = s_cnn;

                cmd.CommandText = "select TeacherID from Teacher where FirstName + ' ' + LastName = @Teacher";
                cmd.Parameters.AddWithValue("@Teacher", teacher);
                SqlDataReader myReader = cmd.ExecuteReader();

                int SelectedTeacherId = 0;

                while (myReader.Read())
                {
                    if (!myReader.IsDBNull("TeacherID")) SelectedTeacherId = (Convert.ToInt32(myReader["TeacherID"]));
                }

                if (SelectedTeacherId == 0) { return "No teacher found."; }

                cmd.CommandText = "select SchoolSubjectID from SchoolSubject where SubjectName = @Lesson";
                cmd.Parameters.AddWithValue("@Lesson", subject);
                myReader.Close();
                myReader = cmd.ExecuteReader();

                int SelectedSubjectId = 0;

                while (myReader.Read())
                {
                    if (!myReader.IsDBNull("SchoolSubjectID")) SelectedSubjectId = (Convert.ToInt32(myReader["SchoolSubjectID"]));
                }
                myReader.Close();

                if (SelectedSubjectId == 0) { return "No subject found."; }

                cmd.CommandText = "insert into teaches (fk_TeacherID, fk_SchoolSubjectID) values ((@TeacherId), (@SubjectId))";
                cmd.Parameters.AddWithValue("@TeacherId", SelectedTeacherId);
                cmd.Parameters.AddWithValue("@SubjectId", SelectedSubjectId);

                cmd.ExecuteNonQuery();

                return "";
            }
            catch (SqlException err)
            {
                MessageBox.Show(err.Message);

                return "Error: " + err.Message;
            }
            finally
            {
                if (s_cnn.State == ConnectionState.Open)
                    s_cnn.Close();
            }
        }
        //Works
        public static async Task<string> InsertForType(string name)
        {

            try
            {
                if (s_cnn.State != ConnectionState.Open)
                    s_cnn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = s_cnn;

                cmd.CommandText = "Insert into GeneralType (TypeName) values (@Name)";
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.ExecuteNonQuery();
                return "";
            }
            catch (SqlException err)
            {
                MessageBox.Show(err.Message);

                return "Error: " + err.Message;
            }
            finally
            {
                if (s_cnn.State == ConnectionState.Open)
                    s_cnn.Close();
            }
        }
    }
}
