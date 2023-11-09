using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelDataReader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            // Specify the file path
            FileInfo fileInfo = new FileInfo("C:\\Users\\umudi\\OneDrive\\Desktop\\Startup\\Startup.xlsx");
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                // Get the first worksheet in the workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                int rowCount = worksheet.Dimension.Rows; // Number of rows in the worksheet
                int columnCount = worksheet.Dimension.Columns;

                string universityName = "";
                string groupName = "";

                List<Data> datas = new List<Data>();

                for (int row = 2; row <= rowCount; row++)
                {
                    if (worksheet.Cells[row, 2].Value == null)
                    {
                        if (worksheet.Cells[row, 1].Value.ToString().Contains("qrup"))
                        {
                            groupName = worksheet.Cells[row, 1].Value.ToString();
                        }
                        else
                        {
                            universityName = worksheet.Cells[row, 1].Value.ToString();
                        }
                    }
                    else
                    {
                        Data data = new Data();

                        data.UniversityName = universityName;
                        data.GroupName = groupName;
                        data.No = Convert.ToInt32(worksheet.Cells[row, 1].Value);
                        data.SpecialtyName = worksheet.Cells[row, 2].Value.ToString();
                        data.KindOfLesson = Convert.ToChar(worksheet.Cells[row, 3].Value);
                        data.Score = worksheet.Cells[row, 4].Value.ToString();

                        datas.Add(data);
                    }
                }

                foreach (var data in datas)
                {
                    Console.WriteLine($"{data.No} {data.UniversityName} {data.GroupName} {data.SpecialtyName} {data.KindOfLesson} {data.Score}");
                }
            }

        }
    }
}
