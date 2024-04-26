using System;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using TimetableWPF.Classes;
using TimetableWPF.Classes.Algorithmtest;

namespace TimetableWPF.TimetableWPF.Classes.ExcelExporter
{
    public class ExcelExporter
    {
        private ExcelWorksheet worksheet;

        public void ExportTimeTableToExcel()
        {
            Algorithm algoClass = new Algorithm();

            for (int a = 0; a < algoClass.TimeTableClassName.Length; a++)
            {
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    worksheet = excelPackage.Workbook.Worksheets.Add("Timetable_" + algoClass.TimeTableClassName[a]);

                    var table = worksheet.Tables.Add(worksheet.Cells["A1:C1"], "DataTable");
                    table.TableStyle = TableStyles.Light9; // Table Style
                    Template(); // Populate Template

                    for (int i = 1; i < 6; i++)
                    {
                        for (int j = 1; j < 11; j++)
                        {
                            worksheet.Cells[j, i].Value = algoClass.TimeTableSubject[a, i, j] + algoClass.TimeTableTeacher[a, i, j];
                        }
                    }

                    // Save the Excel file in the Downloads folder
                    string downloadsFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads\\";
                    string fileName = "Timetable_" + algoClass.TimeTableClassName[a] + ".xlsx";
                    string fullPath = downloadsFolder + fileName;
                    FileInfo file = new FileInfo(fullPath);

                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    excelPackage.SaveAs(file);
                }
            }
        }

        internal void Template()
        {
            string[] DayTemplate = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
            string[] TimeTemplate = { "8:00 - 8:45", "8:50 - 9:35", "9:55 - 10:40", "10:45 - 11:30", "12:30 - 13:15", "13:20 - 14:05", "14:10 - 14:55", "15:20 - 16:05", "16:10 - 16:55", "17:00 - 17:45" };

            // Create Days
            for (int i = 0; i < DayTemplate.Length; i++)
            {
                worksheet.Cells[1, i + 2].Value = DayTemplate[i];
            }

            // Create Times
            for (int j = 0; j < TimeTemplate.Length; j++)
            {
                worksheet.Cells[j + 2, 1].Value = TimeTemplate[j];
            }
        }
    }
}
