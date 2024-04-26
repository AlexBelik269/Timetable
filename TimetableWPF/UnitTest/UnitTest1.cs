using TimeTableWPF;
namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestClass]
        public class ProgramGeneratorTests
        {
            [TestMethod]
            public void GenerateRandomName_ShouldReturnValidName()
            {
                var result = ProgramGenerator.GenerateRandomName();
                string[] firstNames = { "Max", "Sophie", "Alex", "Emma", "Liam", "Olivia", "Noah", "Ava", "Ethan", "Isabella" };
                string[] lastNames = { "Smith", "Johnson", "Williams", "Jones", "Brown", "Miller", "Davis", "García", "Rodriguez", "Martinez" };

                Assert.IsTrue(firstNames.Contains(result.Item1), "FirstName not in expected range.");
                Assert.IsTrue(lastNames.Contains(result.Item2), "LastName not in expected range.");
            }

            [TestMethod]
            public void GenerateRandomStudent_ShouldReturnValidStudent()
            {
                var student = ProgramGenerator.GenerateRandomStudent();
                string[] classes = { "A", "B", "C", "D" };

                Assert.IsNotNull(student.FirstName);
                Assert.IsNotNull(student.LastName);
                Assert.IsTrue(classes.Contains(student.Class), "Class not in expected range.");
            }
            
            [TestMethod]
            public void GenerateRandomTeacher_ShouldReturnValidTeacher()
            {
                var teacher = ProgramGenerator.GenerateRandomTeacher();
                string[] subjects = { "Math", "Science", "English", "History", "Art" };

                Assert.IsNotNull(teacher.FirstName);
                Assert.IsNotNull(teacher.LastName);
                Assert.IsTrue(teacher.Subjects.All(sub => subjects.Contains(sub)), "One or more subjects are not valid.");
            }

            [TestMethod]
            public void GenerateRandomRoom_ShouldReturnValidRoom()
            {
                var room = ProgramGenerator.GenerateRandomRoom();
                string[] possibleSubjects = { "Math", "Science", "English", "History", "Art" };

                Assert.IsTrue(room.Name >= 1.100f && room.Name <= 4.120f, "Room name not in expected range.");
                Assert.AreEqual(possibleSubjects.Length, room.PossibleSubjects.Length);
            }



        }




    }
}