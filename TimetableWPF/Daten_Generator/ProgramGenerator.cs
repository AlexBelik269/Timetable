
namespace TimeTableWPF
{
    public class ProgramGenerator
    {
        public static void Main()
        {
            //var student = GenerateRandomStudent();
            //Console.WriteLine("Student:");
            //Console.WriteLine("FirstName: " + student.FirstName);
            //Console.WriteLine("LastName: " + student.LastName);
            //Console.WriteLine("Class: " + student.Class);
            //Console.WriteLine();

            //var teacher = GenerateRandomTeacher();
            //Console.WriteLine("Teacher:");
            //Console.WriteLine("FirstName: " + teacher.FirstName);
            //Console.WriteLine("LastName: " + teacher.LastName);
            //Console.WriteLine("Subjects: " + string.Join(", ", teacher.Subjects));
            //Console.WriteLine();

            //// Generate and display multiple random rooms
            //Console.WriteLine("Rooms:");
            //for (int i = 0; i < 5; i++)
            //{
            //    var room = GenerateRandomRoom();
            //    Console.WriteLine("Name: " + room.Name);
            //    Console.WriteLine("Possible Subjects: " + string.Join(", ", room.PossibleSubjects));
            //    Console.WriteLine();
            //}
        }

        public static (string, string) GenerateRandomName()
        {
            string[] firstNames = { "Max", "Sophie", "Alex", "Emma", "Liam", "Olivia", "Noah", "Ava", "Ethan", "Isabella" };
            string[] lastNames = { "Smith", "Johnson", "Williams", "Jones", "Brown", "Miller", "Davis", "García", "Rodriguez", "Martinez" };

            Random random = new Random();
            string firstName = firstNames[random.Next(firstNames.Length)];
            string lastName = lastNames[random.Next(lastNames.Length)];

            return (firstName, lastName);
        }

        public class Student
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Class { get; set; }
        }

        public class Teacher
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string[] Subjects { get; set; }
        }

        public class Room
        {
            public float Name { get; set; }
            public string[] PossibleSubjects { get; set; }
        }

        public static Student GenerateRandomStudent()
        {
            var (firstName, lastName) = GenerateRandomName();
            string[] classes = { "A", "B", "C", "D" };

            Random random = new Random();
            string studentClass = classes[random.Next(classes.Length)];

            return new Student { FirstName = firstName, LastName = lastName, Class = studentClass };
        }

        public static Teacher GenerateRandomTeacher()
        {
            var (firstName, lastName) = GenerateRandomName();
            string[] subjects = { "Math", "Science", "English", "History", "Art" };

            Random random = new Random();
            int numberOfSubjects = random.Next(1, 4);
            string[] teacherSubjects = subjects.OrderBy(x => random.Next()).Take(numberOfSubjects).ToArray();

            return new Teacher { FirstName = firstName, LastName = lastName, Subjects = teacherSubjects };
        }

        public static Room GenerateRandomRoom()
        {
            Random random = new Random();
            int floor = random.Next(1, 5);
            int roomNumber = random.Next(100, 121); // Assuming you want room numbers between 100 and 120
            float roomName = float.Parse($"{floor}.{roomNumber}");

            string[] possibleSubjects = { "Math", "Science", "English", "History", "Art" };
            return new Room { Name = roomName, PossibleSubjects = possibleSubjects };
        }
    }
}