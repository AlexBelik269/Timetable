using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TimetableWPF.Classes;
using TimetableWPF.TimetableWPF.Classes;
using TimetableWPF.TimetableWPF.Classes.ExcelExporter;

namespace TimetableWPF.Classes
{

    namespace Algorithmtest
    {
        public class Algorithm
        {

            List<SchoolSubject> schoolSubjects = new List<SchoolSubject>();
            List<Student> students = new List<Student>();
            List<Class> classes = new List<Class>();
            List<Education> educations = new List<Education>();
            List<Room> rooms = new List<Room>();
            List<Teacher> teachers = new List<Teacher>();
            List<TimePlanList> timePlanLists = new List<TimePlanList>();
            List<Lesson> lessons = new List<Lesson>();


            public string[] TeacherName = { }; //ListTranslator();
            //int[] TeacherTypeTemp = {/*TeacherId*/}; //Do it like unix for example french = 2 and german = 3 So teacher has 6
            int[] TeacherType = { };
            string[,] TeacherTypeUnCov = { { } };
            bool[,,] TeacherAvailability = {/*TeacherId*/ {/*Day*/ {/*Avail*/ } } };

            bool[,,] ClassAvailability = {/*ClassId*/ {/*Day*/ {/*Avail*/ } } };
            string[] ClassName = { };

            bool[,,] RoomAvailability = {/*RoomId*/ {/*Day*/ {/*Avail*/ } } };
            string[] RoomName = { };
            bool[] RoomType = { };

            //Lessons

            //unix guide: 2 IT, 3 German, 5 English, 7 French, 11 Math, 13 Economics, 17 Finance
            //int[,,] Lesson = { { { } } };

            //string[,,] LessonName = { { { } } };


            //int[,] SubjectGuidelines = {/*ClassId*/{ /*Unix for finding out what is needed ie: 3 french lessons so 3rd array space contains int 7 */ } };
            //string[]SubjectName = { };


            //Timetable Guide: 1: Class 2: Day 3: Time
            //bool[,,] TimetableAvailability = { { { } } };
            public string[,,] TimeTableTeacher = { { { } } };
            public string[,,] TimeTableSubject = { { { } } };
            public string[,,] TimeTableRoom = { { { } } };



            public string[] TimeTableClassName = { };


            private void TypeConverter()
            {
                for (int i = 0; i < TeacherTypeUnCov.GetLength(0); i++)
                {
                    for (int j = 0; j < TeacherTypeUnCov.GetLength(1); j++)
                    {

                        if (TeacherTypeUnCov[i, j].Contains("M"))
                        {
                            TeacherType[i] = 2;
                        }
                        else
                        {
                            TeacherType[i] = 1;
                        }
                        switch (TeacherTypeUnCov[i, j])
                        {

                            case ("English"):
                                TeacherType[i] *= 3;
                                break;
                            case ("German"):
                                TeacherType[i] *= 5;
                                break;
                            case ("French"):
                                TeacherType[i] *= 7;
                                break;
                            case ("Mathematics"):
                                TeacherType[i] *= 11;
                                break;
                            case ("Sports"):
                                TeacherType[i] *= 13;
                                break;
                            case ("Economics"):
                                TeacherType[i] *= 17;
                                break;
                            case ("Finance"):
                                TeacherType[i] *= 19;
                                break;
                            case ("Physics"):
                                TeacherType[i] *= 23;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }





            private bool TypeFinder(int ID, int Subject)
            {
                bool[] types = new bool[24]; ;
                types[2] = ID % 2 == 0; // ICT
                types[3] = ID % 3 == 0; // English
                types[5] = ID % 5 == 0; // German
                types[7] = ID % 7 == 0; // French
                types[11] = ID % 11 == 0; // Math
                types[13] = ID % 13 == 0; // Sports
                types[17] = ID % 17 == 0; // Economics
                types[19] = ID % 19 == 0; // Finance
                types[23] = ID % 23 == 0; // Physics

                return types[Subject];
            }
            static int GenerateUnusedRandomPrime(int min, int max, int[] usedNumbers)
            {
                if (AllPrimesUsed(usedNumbers))
                {
                    return 0; // All primes are used, return 0 or any other value to indicate unavailability
                }

                Random random = new Random();
                int randomPrime;

                do
                {
                    randomPrime = GenerateRandomPrime(min, max);
                } while (usedNumbers[randomPrime - 1] == 1);

                usedNumbers[randomPrime - 1] = 1;
                return randomPrime;
            }

            static bool AllPrimesUsed(int[] usedNumbers)
            {
                foreach (var number in usedNumbers)
                {
                    if (number == 0)
                        return false; // At least one prime is not used
                }
                return true; // All primes are used
            }

            static int GenerateRandomPrime(int min, int max)
            {
                Random random = new Random();
                int randomNumber;

                do
                {
                    randomNumber = random.Next(min, max + 1);
                } while (!IsPrime(randomNumber));

                return randomNumber;
            }

            static bool IsPrime(int number)
            {
                if (number < 2)
                    return false;

                for (int i = 2; i <= Math.Sqrt(number); i++)
                {
                    if (number % i == 0)
                        return false;
                }

                return true;
            }




            public void TimeTableMaker()
            {




                TypeConverter();
                //Timetable Guide: 1: Class 2: Day 3: Time
                bool[,,] TimetableAvailability = { { { } } };
                string[] TimeTableClassName = { };


                Console.WriteLine("Setting Timetable...");
                int teacherNum = TeacherAvailability.GetLength(0);
                for (int x = 0; x >= teacherNum; x++)
                {

                    for (int y = 0; y < 5; y++)
                    {
                        for (int z = 0; z < 5; z++)
                        {
                            ClassAvailability[x, y, z] = true;
                        }

                        SqlAvailTranslator(x, true);
                    }
                }
                int classNum = ClassAvailability.GetLength(0);
                for (int x = 0; x >= teacherNum; x++)
                {

                    for (int y = 0; y < 5; y++)
                    {
                        for (int z = 0; z < 5; z++)
                        {
                            ClassAvailability[x, y, z] = true;
                        }
                    }
                    SqlAvailTranslator(x, false);
                }

                int roomNum = RoomType.GetLength(0);
                for (int x = 0; x >= teacherNum; x++)
                {

                    for (int y = 0; y < 5; y++)
                    {
                        for (int z = 0; z < 10; z++)
                        {
                            RoomAvailability[x, y, z] = true;
                        }
                    }
                }







                int ClassNum = ClassName.Length;
                int SubjectTypeSaved = 0;
                int SubjectType = 0;

                for (int a = 0; a < ClassNum; a++) //Switches Class
                {
                    for (int b = 0; b < 5; b++) //Switches Day
                    {

                        SubjectTypeSaved = 0;
                        for (int c = 0; c < 10; c++) //Switches lesson
                        {

                            if (TimetableAvailability[a, b, c]) // Checks if Current Time is Available
                            {
                                int[] TriedSubjects = { };//Will create an Array for remaining Subjects here
                                bool IsFindingSubject = true;
                                while (IsFindingSubject)
                                {
                                    if (SubjectTypeSaved != 0) //if the subject has at least one left then it will save it 
                                    {
                                        SubjectType = SubjectTypeSaved;
                                    }
                                    else //Current Subject was invalid, change subject
                                    {
                                        SubjectType = GenerateUnusedRandomPrime(1, 24, TriedSubjects);
                                        if (SubjectType == 0) break;
                                        else if (SubjectType == 2)
                                        {

                                        }
                                    }

                                }
                                //First find a valid room
                                for (int d = 0; d < roomNum; d++)
                                {

                                    if (RoomType[d])
                                    {
                                        for (int e = 0; e < teacherNum; e++)
                                        {

                                            if (TypeFinder(TeacherType[e], SubjectType) && TeacherAvailability[a, b, c])
                                            {
                                                switch (SubjectType)
                                                {
                                                    case (2):
                                                        TimeTableSubject[a, b, c] = "M";
                                                        break;

                                                    case (3):
                                                        TimeTableSubject[a, b, c] = "English";
                                                        break;
                                                    case (5):
                                                        TimeTableSubject[a, b, c] = "German";
                                                        break;
                                                    case (7):
                                                        TimeTableSubject[a, b, c] = "French";
                                                        break;
                                                    case (11):
                                                        TimeTableSubject[a, b, c] = "Mathematics";
                                                        break;
                                                    case (13):
                                                        TimeTableSubject[a, b, c] = "Sports";
                                                        break;
                                                    case (17):
                                                        TimeTableSubject[a, b, c] = "Economics";
                                                        break;
                                                    case (19):
                                                        TimeTableSubject[a, b, c] = "Sports";
                                                        break;
                                                    case (23):
                                                        TimeTableSubject[a, b, c] = "Physics";
                                                        break;
                                                    default:
                                                        break;
                                                }
                                                TimeTableRoom[a, b, c] = RoomName[d];
                                                TimeTableTeacher[a, b, c] = TeacherName[e];
                                                SubjectTypeSaved = SubjectType;
                                                break;
                                            }

                                        }
                                    }

                                }
                            }
                        }

                    }
                    Console.WriteLine("Algorithm Finished");
                    ExcelExporter exporter = new ExcelExporter();
                    exporter.ExportTimeTableToExcel();
                    Console.WriteLine("Exported to Downloads Folder");

                }
            }

            public void SqlAvailTranslator(int Id, bool mode) //Fully done
            {

                //Splits the given string
                string tempSql = "b1, a2, c12";
                string[] tempArray = tempSql.Split(',');
                for (int i = 0; i < tempArray.Length; i++)
                {
                    if (tempArray[i] == "") break;
                    string Day = tempArray[i].Replace("1", "").Replace("2", "").Replace(" ", "");
                    switch (Day)
                    {
                        case "a":
                            if (tempArray[i].Contains("1"))
                            {
                                for (int j = 0; j < 4; j++)
                                {
                                    if (mode)
                                    {
                                        TeacherAvailability[Id, 0, j] = false;
                                    }
                                    else
                                    {
                                        ClassAvailability[Id, 0, j] = false;
                                    }
                                }
                            }
                            if (tempArray[i].Contains("2"))
                            {
                                for (int j = 4; j < 10; i++)
                                {
                                    if (mode)
                                    {
                                        TeacherAvailability[Id, 0, j] = false;
                                    }
                                    else
                                    {
                                        ClassAvailability[Id, 0, j] = false;
                                    }

                                }
                            }

                            break;
                        case "b":
                            if (tempArray[i].Contains("1"))
                            {
                                for (int j = 0; j < 4; j++)
                                {
                                    if (mode)
                                    {
                                        TeacherAvailability[Id, 1, j] = false;
                                    }
                                    else
                                    {
                                        ClassAvailability[Id, 1, j] = false;
                                    }

                                }
                            }
                            if (tempArray[i].Contains("2"))
                            {
                                for (int j = 4; j < 10; j++)
                                {
                                    if (mode)
                                    {
                                        TeacherAvailability[Id, 1, j] = false;
                                    }
                                    else
                                    {
                                        ClassAvailability[Id, 1, j] = false;
                                    }

                                }
                            }

                            break;
                        case "c":
                            if (tempArray[i].Contains("1"))
                            {
                                for (int j = 0; j < 4; j++)
                                {
                                    if (mode)
                                    {
                                        TeacherAvailability[Id, 2, j] = false;
                                    }
                                    else
                                    {
                                        ClassAvailability[Id, 2, j] = false;
                                    }

                                }
                            }
                            if (tempArray[i].Contains("2"))
                            {
                                for (int j = 4; j < 10; j++)
                                {
                                    if (mode)
                                    {
                                        TeacherAvailability[Id, 2, j] = false;
                                    }
                                    else
                                    {
                                        ClassAvailability[Id, 2, j] = false;
                                    }
                                }
                            }
                            break;
                        case "d":
                            if (tempArray[i].Contains("1"))
                            {
                                for (int j = 0; j < 4; j++)
                                {
                                    if (mode)
                                    {
                                        TeacherAvailability[Id, 3, j] = false;
                                    }
                                    else
                                    {
                                        ClassAvailability[Id, 3, j] = false;
                                    }

                                }
                            }
                            if (tempArray[i].Contains("2"))
                            {
                                for (int j = 4; j < 10; j++)
                                {
                                    if (mode)
                                    {
                                        TeacherAvailability[Id, 3, j] = false;
                                    }
                                    else
                                    {
                                        ClassAvailability[Id, 3, j] = false;
                                    }
                                }
                            }
                            break;
                        case "e":
                            if (tempArray[i].Contains("1"))
                            {
                                for (int j = 0; j < 4; j++)
                                {
                                    if (mode)
                                    {
                                        TeacherAvailability[Id, 4, j] = false;
                                    }
                                    else
                                    {
                                        ClassAvailability[Id, 4, j] = false;
                                    }
                                }
                            }
                            if (tempArray[i].Contains("2"))
                            {
                                for (int j = 4; j < 10; j++)
                                {
                                    if (mode)
                                    {
                                        TeacherAvailability[Id, 4, j] = false;
                                    }
                                    else
                                    {
                                        ClassAvailability[Id, 4, j] = false;
                                    }
                                }
                            }
                            break;

                        default:
                            break;

                    }
                }
            }

        }
    }
}
