using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing.IndexedProperties;
using System.Text;
using System.Threading.Tasks;

namespace TimetableWPF.Classes.Algorithm
{
//    public class algorithm_Main
//    {using OfficeOpenXml.FormulaParsing.Excel.Functions;
//using System;
//using System.Reflection;
//using System.Security.Cryptography.X509Certificates;

namespace Algorithmtest
    {
        class Program
        {
            private bool[] TypeFinder(int ID)
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

                return types;
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
            private static bool SqlAvailTranslator(int Teacher, int DayId, int LessonId) //will work on later
            { //A, B, C, D, E
              //Splits the given string
                string tempSql = "b1, a2, c12";
                string[] tempArray = tempSql.Split(',');
                string tempTimeHalf = "";
                for (int i = 0; i < tempArray.Length; i++)
                {

                    if (tempArray[i].Contains("1"))
                    {
                        tempTimeHalf = "a";
                    }
                    if (tempArray[i].Contains("2"))
                    {
                        tempTimeHalf += "b";
                    }
                    if (tempArray[i] == "") return true;




                }
                return false;
            }

            static void Main(string[] args)
            {
                //Temporary Values
                //WARNING DON'T FORGET TO REPLACE WITH STUFF FOR 

                //unix guide: 2 IT, 3 German, 5 English, 7 French, 11 Math, 13 Economics, 17 Finance
                string[] TeacherName = { };
                int[] TeacherTypeTemp = {/*TeacherId*/}; //Do it like unix for example french = 2 and german = 3 So teacher has 6
                int[,] TeacherType = { { } };
                bool[,,] teacherAvailibility = {/*TeacherId*/ {/*Day*/ {/*Avail*/ } } };

                bool[,,] ClassAvail = {/*ClassId*/ {/*Day*/ {/*Avail*/ } } };
                string[] ClassName = { };

                bool[,,] RoomAvail = {/*RoomId*/ {/*Day*/ {/*Avail*/ } } };
                string[] RoomName = { };
                bool[] RoomType = { };

                //Lessons

                //unix guide: 2 IT, 3 German, 5 English, 7 French, 11 Math, 13 Economics, 17 Finance
                int[,,] Lesson = { { { } } };

                string[,,] LessonName = { { { } } };


                int[,] SubjectGuidelines = {/*ClassId*/{ /*Unix for finding out what is needed ie: 3 french lessons so 3rd array space contains int 7
                                                      
                                                      */ } };

                bool[,,] TimeTableAvail = { { { } } };
                //Timetable Guide: 1: Class 2: Day 3: Time

                Console.WriteLine("Setting Timetable...");
                int teacherNum = teacherAvailibility.GetLength(0);
                for (int x = 0; x >= teacherNum; x++)
                {

                    for (int y = 0; y < 5; y++)
                    {
                        for (int z = 0; z < 5; z++)
                        {
                            //Will put it all to true for the time being
                            if (SqlAvailTranslator(x, y, z) == true) //Checks if teacher is available
                            {
                                teacherAvailibility[x, y, z] = true; //might have to use append here
                            }
                            else //Teacher not available
                            {
                                teacherAvailibility[x, y, z] = false; //might have to use append here
                            }

                        }
                    }
                }
                int classNum = Lesson.GetLength(0);
                for (int x = 0; x >= teacherNum; x++)
                {

                    for (int y = 0; y < 5; y++) //day
                    {
                        for (int z = 0; z < 10; z++) //time
                        {
                            //Will put it all to true for the time being
                            if (SqlAvailTranslator(x, y, z)) //Checks if teacher is available in database
                            {
                                ClassAvail[x, y, z] = true; //might have to use append here
                            }
                            else
                            {
                                ClassAvail[x, y, z] = false; //might have to use append here
                            }
                        }
                    }
                }
                int roomNum = RoomType.GetLength(0);
                for (int x = 0; x >= teacherNum; x++)
                {

                    for (int y = 0; y < 5; y++)
                    {
                        for (int z = 0; z < 10; z++)
                        {
                            //Will put it all to true fot the time being
                            if (true) //Checks if Roomn is available in Database
                            {
                                RoomAvail[x, y, z] = true; //might have to use append here
                            }
                            else
                            {
                                RoomAvail[x, y, z] = false; //might have to use append here
                            }

                        }
                    }
                }


                //Empty value fixer here



                int ClassId = 0;
                int TeacherId = 0;
                int RoomId = 0;
                int DayID = 0;
                int LessonId = 0;
                int ClassNum = ClassName.Length;
                int SubjectTypeSaved = 0;
                int SubjectType;

                for (int a = 0; a < ClassNum; a++) //Switches Class
                {
                    for (int b = 0; b < 5; b++) //Switches Day
                    {
                        for (int c = 0; c < 10; c++)
                        {


                            //Will search for the class 

                            if (TimeTableAvail[ClassId, DayID, LessonId]) // Checks if Current Time is Available
                            {
                                int[] TriedSubjects = new int[24];//Will create an Array for remaining Subjects here
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
                                    }

                                }
                                //First find a valid room
                                for (int d = 0; d < roomNum; d++)
                                {

                                    if (RoomType[d])
                                    {
                                        for (int e = 0; e < teacherNum; e++)
                                        {

                                            if (TypeFinder(TeacherType[e]) == SubjectType && teacherAvailibility[a, b, c])
                                            {
                                                Lesson[a, b, c] = SubjectType;
                                            }

                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
}
