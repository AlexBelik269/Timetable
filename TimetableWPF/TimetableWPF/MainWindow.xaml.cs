using OfficeOpenXml.ConditionalFormatting;
using System;
using System.Collections.Generic;
using System.Windows;
using TimetableWPF.Classes;
using TimetableWPF.Classes.Algorithmtest;
using TimeTableWPF;
namespace TimetableWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>			AuxButton.Visibility = Visibility.Visible;
    /// 
    public partial class MainWindow : Window
    {
        byte StateOfWindow;

        bool IsCorrect = true;
        public MainWindow()
        {
            InitializeComponent();
            MainUserInit();
            SQLHandler.VerbindungHerstellen();
            SQLHandler.AllesEinlesen();


            List<string> Educations = new List<string>();
            foreach (Education edu in SQLHandler._Educations)
            {
                Educations.Add(edu._Name);
            }
            EducationSelect.ItemsSource = Educations;

        }


        public void MainUserInit()
        {
            StateOfWindow = 1;

            Titlebar.Content = "Timetable";

            Label1.Visibility = Visibility.Hidden;
            TextBox1.Visibility = Visibility.Hidden;
            Label2.Visibility = Visibility.Hidden;
            TextBox2.Visibility = Visibility.Hidden;
            Label3.Visibility = Visibility.Hidden;
            TextBox3.Visibility = Visibility.Hidden;
            Label4.Visibility = Visibility.Hidden;
            TextBox4.Visibility = Visibility.Hidden;

            ReturnButton.Visibility = Visibility.Hidden;
			AuxButton.Visibility = Visibility.Hidden;
            GenerateRandButton.Visibility = Visibility.Hidden;
        }
        public void NewTeacherInit()
        {
            StateOfWindow = 2;

            Titlebar.Content = "New Teacher";

            //Labels
            Label1.Content = "Name:";
            Label2.Content = "Avail. (CSV):";
            Label3.Content = "Type:";
            Label4.Content = "Modules (CSV):";
            //Buttons
            AuxButton.Content = "Confirm choice";


            Label1.Visibility = Visibility.Visible;
            TextBox1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            TextBox2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Visible;
            TextBox3.Visibility = Visibility.Visible;
            Label4.Visibility = Visibility.Visible;
            TextBox4.Visibility = Visibility.Visible;

            ReturnButton.Visibility = Visibility.Visible;
			AuxButton.Visibility = Visibility.Visible;
            GenerateRandButton.Visibility = Visibility.Visible;
        }
        public void NewStudentInit()
        {
            StateOfWindow = 3;

            Titlebar.Content = "New Student";

            //Labels
            Label1.Content = "Name:";
            Label2.Content = "Education:";
            //Buttons
            AuxButton.Content = "Confirm choice";


            Label1.Visibility = Visibility.Visible;
            TextBox1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            TextBox2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Hidden;
            TextBox3.Visibility = Visibility.Hidden;
            Label4.Visibility = Visibility.Hidden;
            TextBox4.Visibility = Visibility.Hidden;

            ReturnButton.Visibility = Visibility.Visible;
			AuxButton.Visibility = Visibility.Visible;
            GenerateRandButton.Visibility = Visibility.Visible;
        }
        public void NewModuleInit()
        {
            StateOfWindow = 4;

            Titlebar.Content = "New Module / Subject";

            //Labels
            Label1.Content = "Name:";
            Label2.Content = "Type:";
            //Buttons
            AuxButton.Content = "Confirm choice";


            Label1.Visibility = Visibility.Visible;
            TextBox1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            TextBox2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Hidden;
            TextBox3.Visibility = Visibility.Hidden;
            Label4.Visibility = Visibility.Hidden;
            TextBox4.Visibility = Visibility.Hidden;

            ReturnButton.Visibility = Visibility.Visible;
			AuxButton.Visibility = Visibility.Visible;
            GenerateRandButton.Visibility = Visibility.Hidden;
        }

        public void NewRoomInit()
        {
            StateOfWindow = 5;

            Titlebar.Content = "New Room";

            //Labels
            Label1.Content = "Name:";
            Label2.Content = "Type:";
            //Buttons
            AuxButton.Content = "Confirm choice";


            Label1.Visibility = Visibility.Visible;
            TextBox1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            TextBox2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Hidden;
            TextBox3.Visibility = Visibility.Hidden;
            Label4.Visibility = Visibility.Hidden;
            TextBox4.Visibility = Visibility.Hidden;

            ReturnButton.Visibility = Visibility.Visible;
			AuxButton.Visibility = Visibility.Visible;
            GenerateRandButton.Visibility = Visibility.Visible;
        }
        public void NewEducationInit()
        {
            StateOfWindow = 6;

            Titlebar.Content = "New Education";

            //Labels
            Label1.Content = "Name:";
            Label2.Content = "Type:";
            Label3.Content = "Modules (CSV):";
            Label4.Content = "Amount (CSV):";
            //Buttons
            AuxButton.Content = "Confirm choice";


            Label1.Visibility = Visibility.Visible;
            TextBox1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            TextBox2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Visible;
            TextBox3.Visibility = Visibility.Visible;
            Label4.Visibility = Visibility.Visible;
            TextBox4.Visibility = Visibility.Visible;

            ReturnButton.Visibility = Visibility.Visible;
            AuxButton.Visibility = Visibility.Visible;
            GenerateRandButton.Visibility = Visibility.Hidden;
        }
        public void NewTypeInit()
        {
            StateOfWindow = 7;

            Titlebar.Content = "New Type";

            //Labels
            Label1.Content = "Name:";
            //Buttons
            AuxButton.Content = "Confirm choice";


            Label1.Visibility = Visibility.Visible;
            TextBox1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Hidden;
            TextBox2.Visibility = Visibility.Hidden;
            Label3.Visibility = Visibility.Hidden;
            TextBox3.Visibility = Visibility.Hidden;
            Label4.Visibility = Visibility.Hidden;
            TextBox4.Visibility = Visibility.Hidden;

            ReturnButton.Visibility = Visibility.Visible;
            AuxButton.Visibility = Visibility.Visible;
            GenerateRandButton.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// Events / triggers with buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void AuxButtonSwitch(object sender, RoutedEventArgs e)
        {
            switch (StateOfWindow)
            {
                case 2:
                    CreateTeacher();
                    break;
                case 3:
                    CreateStudent();
                    break;
                case 4:
                    CreateSchoolSubject();
                    break;
                case 5:
                    CreateRoom();
                    break;
                case 6:
                    CreateEducation();
                    break;
                case 7:
                    CreateType();
                    break;
            }

        }
        private void ReturnEvent(object sender, RoutedEventArgs e)
        {
            ResetAndGoDefault();
            MainUserInit();
        }

        /// <summary>
        /// General Functionality
        /// </summary>
        private void ClearBoxes()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
             TextBox4.Text = "";
    

            Label1.Content = "";
            Label2.Content = "";
            Label3.Content = "";
        }
        private void ResetAndGoDefault()
        {
            ClearBoxes();
            MainUserInit();
        }

        private void AddModule(object sender, RoutedEventArgs e)
        {
            ClearBoxes();
            NewModuleInit();
        }

        private void AddStudent(object sender, RoutedEventArgs e)
        {
            ClearBoxes();
            NewStudentInit();
        }

        private void AddTeacher(object sender, RoutedEventArgs e)
        {
            ClearBoxes();
            NewTeacherInit();
        }

        private void AddRoom(object sender, RoutedEventArgs e)
        {
            ClearBoxes();
            NewRoomInit();
        }

        private void AddEducation(object sender, RoutedEventArgs e)
        {
            ClearBoxes();
            NewEducationInit();
        }
        private void AddType(object sender, RoutedEventArgs e)
        {
            ClearBoxes();
            NewTypeInit();
        }
        private void CreateTeacher()
        {
            string name = TextBox1.Text;
            string type = TextBox2.Text;
			string availability = TextBox3.Text;
            string[] modules = TextBox4.Text.Split(',');
            ErrorReturnHandler(SQLHandler.InsertForTeacher(name, availability, type).Result);

            foreach (string module in modules)
            {
                ErrorReturnHandler(SQLHandler.InsertTeaches(name, module).Result);
            }
        }

        private void CreateStudent()
		{
			string name = TextBox1.Text;
            string className = TextBox2.Text;
            ErrorReturnHandler(SQLHandler.InsertForStudent(name, className).Result);
		}
		private void CreateSchoolSubject()
		{
			string name = TextBox1.Text;
			string type = TextBox2.Text;
            ErrorReturnHandler(SQLHandler.InsertForSchoolSubject(name, type).Result);
        }
		private void CreateRoom()
        {
			string name = TextBox1.Text;
			string type = TextBox2.Text;
            ErrorReturnHandler(SQLHandler.InsertForRoom(name, type).Result);
		}
        private void CreateEducation()
        {
            string name = TextBox1.Text;
            string type = TextBox2.Text;
            string[] Subjects = TextBox3.Text.Split(',');
            string[] Amounts = TextBox4.Text.Split(',');
            ErrorReturnHandler(SQLHandler.InsertForEducation(name, type).Result);

            List<int> amountsInInt = new List<int>();

            int Temp = 0;

            foreach (string amount in Amounts)
            {

                if(!int.TryParse(amount, out int result))
                {
                    Temp = 0;
                }
                else
                {
                    Temp = Convert.ToInt32(amount);
                }

                amountsInInt.Add(Convert.ToInt32(Temp));

            }

            if (Subjects.Length != Amounts.Length)
            {
                ErrorReturnHandler("Subject amount not equal to \"Amount\" value");
                return;
            }

            for(int i = 0; i < Subjects.Length; i++) {
                ErrorReturnHandler(SQLHandler.InsertEducationHasSubject(name, Subjects[i], amountsInInt[i]).Result);
            }
        }

        private void CreateType()
        {
            string name = TextBox1.Text;
            ErrorReturnHandler(SQLHandler.InsertForType(name).Result);
        }
        private void ErrorReturnHandler(string value)
        {
            if(value != "")
            {
                Titlebar.Content = value;
            }
        }

        private void GenerateTimetable(object sender, RoutedEventArgs e)
        {
            if (IsCorrect)
            {
                ResultWindow asd = new ResultWindow();

                string selection = Convert.ToString(EducationSelect.SelectedItem);

                asd.Show();

                Algorithm algo = new Algorithm();

                algo.TimeTableMaker();
            } 
            else
            {
                MainUserInit();
                Titlebar.Content = "Error with creating Table";
            }
        }

        private void GenerateButtonTexts(object sender, RoutedEventArgs e)
        {
            switch (StateOfWindow)
            {
                case 2:
                    var temp = ProgramGenerator.GenerateRandomTeacher();

                    TextBox1.Text = temp.FirstName + " " + temp.LastName;
                    TextBox4.Text = temp.Subjects[0];
                    int index = 0;
                    foreach (string t in temp.Subjects)
                    {
                        if (index != 0)
                        {
                            TextBox4.Text += "," + t;
                        }
                    }
                    break;
                case 3:
                    var temp1 = ProgramGenerator.GenerateRandomStudent();
                    TextBox1.Text = temp1.FirstName + " " + temp1.LastName;
                    break;
                case 5:
                    var temp2 = ProgramGenerator.GenerateRandomRoom();
                    TextBox1.Text = Convert.ToString(temp2.Name);
                    TextBox4.Text = temp2.PossibleSubjects[0];

                    int index1 = 0;
                    foreach (string t in temp2.PossibleSubjects)
                    {
                        if (index1 != 0)
                        {
                            TextBox4.Text += "," + t;
                        }
                    }
                    break;
            }
        }
    }
}
