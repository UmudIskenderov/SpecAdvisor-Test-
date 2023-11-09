using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace AnalysisLogic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // input: universitet, ixtisas, bal, E/Q, O/DS

            // 1) universitetlere gore sec
            // 2) ixtisaslara gore sec
            // 3) E/Q ya gore sec
            // 4) O/DS ya gore sec
            // 5) 
            //   5.1) ilk 2 ixtisas baldan yuxari sec, 2 ili muqayise et yerlerin sayina gore (3 ilin yer ve bal araligi datasi lazimdi)
            //       5.1.1) eger inisil ve ya kecen il null dursa birinin balini gotur
            //       5.1.2) eger yerlerin sayi azalibsa demek bal artacaq + 2*azalan yer sayi
            //       5.1.2) eger yerlerin sayi artibsa demek bal azalacaq + 1*artan yerlerin sayi
            //             5.1. .1) 
            //   5.2) novbeti 3 ixtisas oz balina beraber olar sec, yene muqayise et



            // elimizde 2022 2023 ixtisas ballari ve 2021 2022 2023 yerler ve 2022 2023 bal araligi cedvelleri var
            // 1) 2024 jurnal hazirlanmalidi
            // 2) 2022 bal araliginda 600-610 50 nefer idi, 2023 de 40 neferdi, cox olani gotur ki ballar qalxmama ehtimali daha coxdu ona gore bali gotur 2022 e gore
            // 3) 2021 de 40 yer var idi 2022 de 35 yer, 2023 de 30 yer var, 2022 bali 2021 balindan coxdusa 2022 den 2021 cix bol 40 cix 35 e, eger 2023 2022 den azdisa 2023 ucun baldan 5 vur evvelki netice toplayiriq


            // 1) ilk iki ixtisas sec baldan 5-20 bala kimi yuxari
            // 2) novbeti 3 ixtisas sec balina beraber +-5
            // 3) novbeti 3 ixtisas sec balindan 5-20 bal asagi
            // 4) novbeti 3 ixtisas sec balindan 21-40 bal asagi
            // 5) novbeti 4 ixtisas sec balindan 41-100 bal asagi


            Console.OutputEncoding = Encoding.UTF8;

            List<SpecialtyData> faculties = new List<SpecialtyData>();

            string excelFilePath = @"C:\Users\umudi\OneDrive\Desktop\Startup\Startup.xlsx";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileInfo excelFile = new FileInfo(excelFilePath);

            using (ExcelPackage package = new ExcelPackage(excelFile))
            {
                int id = 1;
                string universityName = "";
                string groupName = "";
                for (int i = 0; i < package.Workbook.Worksheets.Count; i++)
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[i]; // Change the index if needed

                    int rowCount = worksheet.Dimension.Rows;
                    int columnCount = worksheet.Dimension.Columns;


                    for (int row = 2; row <= rowCount; row++)
                    {
                        if (worksheet.Cells[row, 2].Value == null)
                        {
                            if (worksheet.Cells[row, 1].Value.ToString().Contains("qrup"))
                            {
                                if (worksheet.Cells[row, 1].Value.ToString().Length <= 8)
                                {
                                    int index = worksheet.Cells[row, 1].Value.ToString().IndexOf("qrup");
                                    groupName = worksheet.Cells[row, 1].Value.ToString().Substring(0, index - 1);
                                }
                                else
                                {
                                    universityName = "";
                                    string[] str = worksheet.Cells[row, 1].Value.ToString().Split();
                                    groupName = str[str.Length - 2];
                                    for (int j = 0; j < str.Length - 2; j++)
                                    {
                                        universityName += str[j];
                                        if (j == str.Length - 3)
                                            continue;
                                        universityName += " ";
                                    }
                                }
                            }
                            else
                            {
                                universityName = worksheet.Cells[row, 1].Value.ToString();
                            }
                        }
                        else
                        {
                            if (worksheet.Cells[row, 1].Value == null)
                            {
                                faculties[id - 2].Name += " " + worksheet.Cells[row, 2].GetValue<string>();
                                continue;
                            }
                            SpecialtyData faculty = new SpecialtyData();
                            faculty.Id = id++;
                            faculty.Name = worksheet.Cells[row, 2].GetValue<string>();
                            faculty.IsVisual = worksheet.Cells[row, 3].GetValue<string>() == "Q" ? false : true;
                            faculty.GroupName = groupName;
                            faculty.UniversityName = universityName;

                            string[] scores = worksheet.Cells[row, 4].GetValue<string>().Replace("(", "").Replace(")", "").Replace(" )", "").Replace("( ", "").Trim().Split(' ');

                            if (scores[0] != "-")
                            {
                                faculty.ScoreWithPay = Convert.ToDouble(scores[0]);
                            }
                            if (scores[1] != "-")
                            {
                                faculty.Score = Convert.ToDouble(scores[1]);
                            }

                            faculties.Add(faculty);
                        }
                    }
                }
            }

            foreach (var f in faculties)
            {
                Console.WriteLine($"{f.Id}.{f.UniversityName} {f.Name}  {f.GroupName} qrup  {f.Score}/{f.ScoreWithPay}");
            }
        }
    }
}
