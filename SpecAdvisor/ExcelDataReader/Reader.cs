using OfficeOpenXml;
using SpecAdvisor.Entities;
using SpecAdvisor.Enums;
using System;
using System.Collections.Generic;
using System.IO;

namespace SpecAdvisor.ExcelDataReader
{
    public static class Reader
    {
        public static List<Specialty> Read()
        {
            // Specify the file path
            FileInfo fileInfo = new FileInfo("C:\\Users\\umudi\\Downloads\\2023-2024copy.xlsx");

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            List<Specialty> specialties = new List<Specialty>();

            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                // Get the first worksheet in the workbook
                int sheetCount = package.Workbook.Worksheets.Count;
                int id = 1;
                string universityName = "";
                string groupName = "";
                for (int i = 0; i < sheetCount; i++)
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[i];

                    int rowCount = worksheet.Dimension.Rows; // Number of rows in the worksheet
                    int columnCount = worksheet.Dimension.Columns;

                    for (int row = 1; row <= rowCount; row++)
                    {
                        if (worksheet.Cells[row, 2].Value == null)
                        {
                            if (worksheet.Cells[row, 1].Value == null) continue;
                            string cell = worksheet.Cells[row, 1].Value.ToString();

                            if (cell.Contains("qrup"))
                            {
                                groupName = cell;
                            }
                            else
                            {
                                universityName = cell;
                            }
                        }
                        else
                        {
                            if (worksheet.Cells[row, 4].Value == null) continue;
                            if (worksheet.Cells[row, 1].Value == null || worksheet.Cells[row, 3].Value == null || worksheet.Cells[row, 4].Value.ToString().Contains("/")) continue;

                            int index = groupName.IndexOf(" ");
                            string group = groupName.Substring(0, index);
                            if (group == "V")
                            {
                                continue;
                            }
                            Specialty specialty = new Specialty();

                            specialty.University = new University()
                            {
                                Name = universityName,
                            };
                            
                            specialty.Group = group == "I" ? Group.I : group == "II" ? Group.II : group == "III" ? Group.III : group == "IV" ? Group.IV : Group.V;
                            specialty.Name = worksheet.Cells[row, 2].Value.ToString();
                            specialty.IsVisual = worksheet.Cells[row, 3].Value.ToString()[0] == 'Ə' ? true : false;

                            string[] scores = worksheet.Cells[row, 4].GetValue<string>().Replace(" )", "").Replace("( ", "").Replace("(", "").Replace(")", "").Trim().Split();
                         
                            if (scores[0] != "-")
                            {
                                specialty.Id = id++;
                                specialty.IsPaid = true;
                                specialty.AccessScore = Convert.ToDouble(scores[0]);
                                specialties.Add(specialty);
                            }
                            if (scores.Length > 1 && scores[1] != "-")
                            {
                                specialty.Id = id++;
                                specialty.IsPaid = false;
                                specialty.AccessScore = Convert.ToDouble(scores[1]);
                                specialties.Add(specialty);
                            }
                        }
                    }
                }
            }
            foreach (var specialty in specialties)
            {
                Console.WriteLine($"{specialty.Id} {specialty.University.Name} {specialty.Group} {specialty.Name} {specialty.IsVisual} {specialty.AccessScore} {specialty.IsPaid}");
            }

            return specialties;
        }
    }
}
