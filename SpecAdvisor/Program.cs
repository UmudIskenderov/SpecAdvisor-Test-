using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecAdvisor.Entities;
using SpecAdvisor.Enums;
using SpecAdvisor.ExcelDataReader;
using SpecAdvisor.Models;

namespace SpecAdvisor
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Console.WriteLine("Please enter your score...");
            double studentScore = Convert.ToDouble(Console.ReadLine());

            //Console.WriteLine("Please, enter university name or names which separated with comma...");
            string[] universityNames = { 
                "Bakı Dövlət Universiteti", 
                "Azərbaycan Texniki Universiteti",
                "Azərbaycan Dövlət Neft və Sənaye Universiteti", 
                "Azərbaycan Memarlıq və İnşaat Universiteti",
                "Azərbaycan Dövlət Pedaqoji Universiteti", 
                "Azərbaycan Respublikası Prezidenti yanında Dövlət İdarəçilik Akademiyası",
                "Bakı Mühəndislik Universiteti"
            };

            Console.WriteLine("The faculty is visible?");
            string isFormal = Console.ReadLine();
            bool isVisual = isFormal == "Y" ? true : false;

            Console.WriteLine("The faculty is non-visible?");
            string isNotFormal = Console.ReadLine();
            bool isNotVisual = isNotFormal == "Y" ? true : false;

            Console.WriteLine("Should the faculty be free?");
            bool isFree = Console.ReadLine() == "Y" ? true : false;

            Console.WriteLine("Should the faculty be paid?");
            bool isPaid = Console.ReadLine() == "Y" ? true : false;

            Console.WriteLine("What group is  the faculty?");
            int group = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("How many choice do you want to see?");
            int totalChoices = Convert.ToInt32(Console.ReadLine());
            int countOfChoices = totalChoices / 5;

            List<Specialty> specialties = Reader.Read();

            List<Specialty> chosenFaculties = specialties.Where(f => ((f.IsVisual == isVisual) || (!(f.IsVisual) == isNotVisual)) &&
                                                                     ((f.IsPaid == isPaid) || (!(f.IsPaid) == isFree)) &&
                                                                     universityNames.Contains(f.University.Name) &&
                                                                     (f.Group == (Group)group)).ToList();

            double plusFifty = studentScore + 50;
            double plusTwentyFive = studentScore + 25;
            double minusTwentyFive = studentScore - 25;
            double minusFifty = studentScore - 50;
            double minusEighty = studentScore - 80;

            List<Specialty> isBetweenPlus25Plus50 = new List<Specialty>();
            List<Specialty> isBetweenScorePlus25 = new List<Specialty>();
            List<Specialty> isBetweenMinus25Score = new List<Specialty>();
            List<Specialty> isBetweenMinus25Minus50 = new List<Specialty>();
            List<Specialty> isBetweenMinus50Minus80 = new List<Specialty>();

            isBetweenPlus25Plus50 = chosenFaculties.Where(f => (f.AccessScore >= plusTwentyFive & f.AccessScore <= plusFifty)).ToList();
            isBetweenScorePlus25 = chosenFaculties.Where(f => (f.AccessScore >= studentScore & f.AccessScore <= plusTwentyFive)).ToList();
            isBetweenMinus25Score = chosenFaculties.Where(f => (f.AccessScore >= minusTwentyFive & f.AccessScore <= studentScore)).ToList();
            isBetweenMinus25Minus50 = chosenFaculties.Where(f => (f.AccessScore >= minusFifty & f.AccessScore <= minusTwentyFive)).ToList();
            isBetweenMinus50Minus80 = chosenFaculties.Where(f => (f.AccessScore >= minusEighty & f.AccessScore <= minusFifty)).ToList();

            List<Specialty> facultiesForStudent = new List<Specialty>();

            //int sumOfFirstThree = totalChoices * 3 / 5;
            //int sumOfLastTwo = totalChoices * 2 / 5;
            //if (isBetweenMinus25Minus50Count + isBetweenMinus50Minus80Count < (totalChoices * 2 / 5))
            //{
            //    sumOfFirstThree = totalChoices - (isBetweenMinus25Minus50Count + isBetweenMinus50Minus80Count);
            //    sumOfLastTwo = isBetweenMinus25Minus50Count + isBetweenMinus50Minus80Count;
            //}

            List<Specialty> addedBetweenPlus25Plus50 = new List<Specialty>();
            List<Specialty> addedBetweenScorePlus25 = new List<Specialty>();
            List<Specialty> addedBetweenMinus25Score = new List<Specialty>();
            List<Specialty> addedBetweenMinus25Minus50 = new List<Specialty>();
            List<Specialty> addedBetweenMinus50Minus80 = new List<Specialty>();

            for (int i = 0; ; i++)
            {
                if (i < isBetweenPlus25Plus50.Count)
                    addedBetweenPlus25Plus50.Add(isBetweenPlus25Plus50[i]);
                if (i < isBetweenScorePlus25.Count)
                    addedBetweenScorePlus25.Add(isBetweenScorePlus25[i]);
                if (i < isBetweenMinus25Score.Count)
                    addedBetweenMinus25Score.Add(isBetweenMinus25Score[i]);
                if (i < isBetweenMinus25Minus50.Count)
                    addedBetweenMinus25Minus50.Add(isBetweenMinus25Minus50[i]);
                if (i < isBetweenMinus50Minus80.Count)
                    addedBetweenMinus50Minus80.Add(isBetweenMinus50Minus80[i]);
                if ((addedBetweenPlus25Plus50.Count + addedBetweenScorePlus25.Count + addedBetweenMinus25Score.Count + addedBetweenMinus25Minus50.Count + addedBetweenMinus50Minus80.Count) == totalChoices) break;
            }

            //int maxForFirstThree = Math.Max(Math.Max(isBetweenPlus25Plus50Count, isBetweenScorePlus25Count), isBetweenMinus25ScoreCount);
            //for (int i = 0; i < maxForFirstThree; i++)
            //{
            //    if (i < isBetweenPlus25Plus50Count)
            //        addedBetweenPlus25Plus50.Add(isBetweenPlus25Plus50[i]);
            //    if (i < isBetweenScorePlus25Count)
            //        addedBetweenScorePlus25.Add(isBetweenScorePlus25[i]);
            //    if (i < isBetweenMinus25ScoreCount)
            //        addedBetweenMinus25Score.Add(isBetweenMinus25Score[i]);
            //    if (facultiesForStudent.Count == sumOfFirstThree) break;
            //}

            //int maxForLastTwo = Math.Max(isBetweenMinus25Minus50Count, isBetweenMinus50Minus80Count);
            //for (int i = 0; i < maxForLastTwo; i++)
            //{
            //    if (i < isBetweenMinus25Minus50Count)
            //        addedBetweenMinus25Minus50.Add(isBetweenMinus25Minus50[i]);
            //    if (i < isBetweenMinus50Minus80Count)
            //        addedBetweenMinus50Minus80.Add(isBetweenMinus50Minus80[i]);
            //    if (facultiesForStudent.Count == totalChoices)
            //        break;

            //}

            facultiesForStudent.AddRange(addedBetweenPlus25Plus50);
            facultiesForStudent.AddRange(addedBetweenScorePlus25);
            facultiesForStudent.AddRange(addedBetweenMinus25Score);
            facultiesForStudent.AddRange(addedBetweenMinus25Minus50);
            facultiesForStudent.AddRange(addedBetweenMinus50Minus80);

            int num = 1;
            foreach (var f in facultiesForStudent)
            {
                string paid = f.IsPaid ? "Ödənişli" : "Ödənişsiz";
                string visual = f.IsVisual ? "Əyani" : "Qiyabi";
                Console.WriteLine($"{num++}.{f.University.Name} {f.Group} qrup {f.Name} {visual} {paid} {f.AccessScore}");
            }


            #region
            //InputBachelorData inputBachelorData = new InputBachelorData()
            //{
            //    Universities =
            //    {
            //        new University()
            //        {
            //            Name = "Bakı Dövlət Universiteti"
            //        },
            //        new University()
            //        {
            //            Name = "Azərbaycan Dövlət Neft və Sənaye Universiteti"
            //        },
            //        new University()
            //        {
            //            Name = "Azərbaycan Dövlət İqtisad Universiteti "
            //        },
            //        new University()
            //        {
            //            Name = "Bakı Mühəndislik Universiteti"
            //        }
            //    },
            //    Group = Group.I,
            //    IsVisual = true,
            //    IsNotVisual = true,
            //    isPaid = true,
            //    isNotPaid = true,
            //    Score = 378.6
            //};





            //List<Specialty> specialties = Reader.Read();

            //specialties = specialties.Where(x => inputBachelorData.Universities.FirstOrDefault(y => y.Name == x.University.Name) != null &&
            //                        x.Group == inputBachelorData.Group).ToList();

            //if(inputBachelorData.IsVisual && !inputBachelorData.IsNotVisual)
            //{
            //    specialties = specialties.Where(x => x.IsVisual == true).ToList();
            //}
            //else if(!inputBachelorData.IsVisual && inputBachelorData.IsNotVisual)
            //{
            //    specialties = specialties.Where(x => x.IsVisual == false).ToList();
            //}




            //if (inputBachelorData.isPaid && !inputBachelorData.isNotPaid)
            //{
            //    specialties = specialties.Where(x => x.PaidScore <= (inputBachelorData.Score + 60) == true &&
            //                                         x.PaidScore >= (inputBachelorData.Score - 140) == true).ToList();
            //}
            //else if(!inputBachelorData.isPaid && inputBachelorData.isNotPaid)
            //{
            //    specialties = specialties.Where(x => x.OrderedByStateScore <= (inputBachelorData.Score + 60) == true &&
            //                                         x.OrderedByStateScore >= (inputBachelorData.Score - 140) == true).ToList();
            //}
            //else if (inputBachelorData.isPaid && inputBachelorData.isNotPaid)
            //{
            //    specialties = specialties.Where(x => (x.PaidScore <= (inputBachelorData.Score + 60) == true && x.PaidScore >= (inputBachelorData.Score - 140) == true) ||
            //                                         (x.OrderedByStateScore <= (inputBachelorData.Score + 60) == true && x.OrderedByStateScore >= (inputBachelorData.Score - 140) == true)).ToList();
            //}





            //specialties = specialties.OrderBy(x => x.OrderedByStateScore).ToList();

            //List<Specialty> selectedSpecialties = new List<Specialty>();
            //if(inputBachelorData.IsVisual && inputBachelorData.IsNotVisual && inputBachelorData.isPaid && inputBachelorData.isNotPaid)
            //{
            //    var isVisual = specialties.Where(x => x.IsVisual == true);

            //    var isVisualIsNotPaidFiveBetweenTwenty = isVisual.Where(x=>x.OrderedByStateScore<=(inputBachelorData.Score+60) && x.OrderedByStateScore > (inputBachelorData.Score + 20)).ToList();
            //    if (isVisualIsNotPaidFiveBetweenTwenty.Count > 20)
            //    {
            //        for (int i = 0; i < 20; i++)
            //        {
            //            selectedSpecialties.Add(isVisualIsNotPaidFiveBetweenTwenty[i]);
            //        }
            //    }
            //    else
            //    {
            //        for (int i = 0; i < isVisualIsNotPaidFiveBetweenTwenty.Count; i++)
            //        {
            //            selectedSpecialties.Add(isVisualIsNotPaidFiveBetweenTwenty[i]);
            //        }
            //    }

            //    var isVisualIsNotPaidMinusFiveBetweenPlusFive = isVisual.Where(x => x.OrderedByStateScore <= (inputBachelorData.Score + 20) && x.OrderedByStateScore > (inputBachelorData.Score - 20)).ToList();
            //    if (isVisualIsNotPaidMinusFiveBetweenPlusFive.Count > 20)
            //    {
            //        for (int i = 0; i < 20; i++)
            //        {
            //            selectedSpecialties.Add(isVisualIsNotPaidMinusFiveBetweenPlusFive[i]);
            //        }
            //    }
            //    else
            //    {
            //        for (int i = 0; i < isVisualIsNotPaidMinusFiveBetweenPlusFive.Count; i++)
            //        {
            //            selectedSpecialties.Add(isVisualIsNotPaidMinusFiveBetweenPlusFive[i]);
            //        }
            //    }

            //    var isVisualIsNotPaidMinusFiveBetweenMinusTwenty = isVisual.Where(x => x.OrderedByStateScore <= (inputBachelorData.Score - 20) && x.OrderedByStateScore > (inputBachelorData.Score - 60)).ToList();
            //    if (isVisualIsNotPaidMinusFiveBetweenMinusTwenty.Count > 20)
            //    {
            //        for (int i = 0; i < 20; i++)
            //        {
            //            selectedSpecialties.Add(isVisualIsNotPaidMinusFiveBetweenMinusTwenty[i]);
            //        }
            //    }
            //    else
            //    {
            //        for (int i = 0; i < isVisualIsNotPaidMinusFiveBetweenMinusTwenty.Count; i++)
            //        {
            //            selectedSpecialties.Add(isVisualIsNotPaidMinusFiveBetweenMinusTwenty[i]);
            //        }
            //    }

            //    var isVisualIsNotPaidMinusTwentyBetweenMinusFifty = isVisual.Where(x => x.OrderedByStateScore <= (inputBachelorData.Score - 60) && x.OrderedByStateScore > (inputBachelorData.Score - 100)).ToList();
            //    if (isVisualIsNotPaidMinusTwentyBetweenMinusFifty.Count > 20)
            //    {
            //        for (int i = 0; i < 20; i++)
            //        {
            //            selectedSpecialties.Add(isVisualIsNotPaidMinusTwentyBetweenMinusFifty[i]);
            //        }
            //    }
            //    else
            //    {
            //        for (int i = 0; i < isVisualIsNotPaidMinusTwentyBetweenMinusFifty.Count; i++)
            //        {
            //            selectedSpecialties.Add(isVisualIsNotPaidMinusTwentyBetweenMinusFifty[i]);
            //        }
            //    }

            //    var isVisualIsNotPaidMinusFiftyBetweenMinusEighty = isVisual.Where(x => x.OrderedByStateScore <= (inputBachelorData.Score - 100) && x.OrderedByStateScore >= (inputBachelorData.Score - 140)).ToList();
            //    if (isVisualIsNotPaidMinusFiftyBetweenMinusEighty.Count > 20)
            //    {
            //        for (int i = 0; i < 20; i++)
            //        {
            //            selectedSpecialties.Add(isVisualIsNotPaidMinusFiftyBetweenMinusEighty[i]);
            //        }
            //    }
            //    else
            //    {
            //        for (int i = 0; i < isVisualIsNotPaidMinusFiftyBetweenMinusEighty.Count; i++)
            //        {
            //            selectedSpecialties.Add(isVisualIsNotPaidMinusFiftyBetweenMinusEighty[i]);
            //        }
            //    }

            //    if(selectedSpecialties.Count < 100) 
            //    {
            //        int count = 100 - selectedSpecialties.Count;
            //        var isVisualIsPaidFiveBetweenTwenty = isVisual.Where(x => x.PaidScore <= (inputBachelorData.Score + 60) && x.PaidScore > (inputBachelorData.Score + 20)).ToList();
            //        if (isVisualIsPaidFiveBetweenTwenty.Count > 20)
            //        {
            //            for (int i = 0; i < 20; i++)
            //            {
            //                if (count == 0) break;
            //                selectedSpecialties.Add(isVisualIsPaidFiveBetweenTwenty[i]);
            //                count--;
            //            }
            //        }
            //        else
            //        {
            //            for (int i = 0; i < isVisualIsPaidFiveBetweenTwenty.Count; i++)
            //            {
            //                if (count == 0) break;
            //                selectedSpecialties.Add(isVisualIsPaidFiveBetweenTwenty[i]);
            //                count--;
            //            }
            //        }

            //        var isVisualIsPaidMinusFiveBetweenPlusFive = isVisual.Where(x => x.PaidScore <= (inputBachelorData.Score + 20) && x.PaidScore > (inputBachelorData.Score - 20)).ToList();
            //        if (isVisualIsPaidMinusFiveBetweenPlusFive.Count > 20)
            //        {
            //            for (int i = 0; i < 20; i++)
            //            {
            //                if (count == 0) break;
            //                selectedSpecialties.Add(isVisualIsPaidMinusFiveBetweenPlusFive[i]);
            //                count--;
            //            }
            //        }
            //        else
            //        {
            //            for (int i = 0; i < isVisualIsPaidMinusFiveBetweenPlusFive.Count; i++)
            //            {
            //                if (count == 0) break;
            //                selectedSpecialties.Add(isVisualIsPaidMinusFiveBetweenPlusFive[i]);
            //                count--;
            //            }
            //        }

            //        var isVisualIsPaidMinusFiveBetweenMinusTwenty = isVisual.Where(x => x.PaidScore <= (inputBachelorData.Score - 20) && x.PaidScore > (inputBachelorData.Score - 60)).ToList();
            //        if (isVisualIsPaidMinusFiveBetweenMinusTwenty.Count > 20)
            //        {
            //            for (int i = 0; i < 20; i++)
            //            {
            //                if (count == 0) break;
            //                selectedSpecialties.Add(isVisualIsPaidMinusFiveBetweenMinusTwenty[i]);
            //                count--;
            //            }
            //        }
            //        else
            //        {
            //            for (int i = 0; i < isVisualIsPaidMinusFiveBetweenMinusTwenty.Count; i++)
            //            {
            //                if (count == 0) break;
            //                selectedSpecialties.Add(isVisualIsPaidMinusFiveBetweenMinusTwenty[i]);
            //                count--;
            //            }
            //        }

            //        var isVisualIsPaidMinusTwentyBetweenMinusFifty = isVisual.Where(x => x.PaidScore <= (inputBachelorData.Score - 60) && x.PaidScore > (inputBachelorData.Score - 100)).ToList();
            //        if (isVisualIsPaidMinusTwentyBetweenMinusFifty.Count > 20)
            //        {
            //            for (int i = 0; i < 20; i++)
            //            {
            //                if (count == 0) break;
            //                selectedSpecialties.Add(isVisualIsPaidMinusTwentyBetweenMinusFifty[i]);
            //                count--;
            //            }
            //        }
            //        else
            //        {
            //            for (int i = 0; i < isVisualIsPaidMinusTwentyBetweenMinusFifty.Count; i++)
            //            {
            //                if (count == 0) break;
            //                selectedSpecialties.Add(isVisualIsPaidMinusTwentyBetweenMinusFifty[i]);
            //                count--;
            //            }
            //        }

            //        var isVisualIsPaidMinusFiftyBetweenMinusEighty = isVisual.Where(x => x.PaidScore <= (inputBachelorData.Score - 100) && x.PaidScore >= (inputBachelorData.Score - 140)).ToList();
            //        if (isVisualIsPaidMinusFiftyBetweenMinusEighty.Count > 20)
            //        {
            //            for (int i = 0; i < 20; i++)
            //            {
            //                if (count == 0) break;
            //                selectedSpecialties.Add(isVisualIsPaidMinusFiftyBetweenMinusEighty[i]);
            //                count--;
            //            }
            //        }
            //        else
            //        {
            //            for (int i = 0; i < isVisualIsPaidMinusFiftyBetweenMinusEighty.Count; i++)
            //            {
            //                if (count == 0) break;
            //                selectedSpecialties.Add(isVisualIsPaidMinusFiftyBetweenMinusEighty[i]);
            //                count--;
            //            }
            //        }

            //        if (selectedSpecialties.Count < 100)
            //        {
            //            count = 100 - selectedSpecialties.Count;
            //            var isNotVisual = specialties.Where(x => x.IsVisual == false);

            //            var isNotVisualIsNotPaidFiveBetweenTwenty = isNotVisual.Where(x => x.OrderedByStateScore <= (inputBachelorData.Score + 60) && x.OrderedByStateScore > (inputBachelorData.Score + 20)).ToList();
            //            if (isNotVisualIsNotPaidFiveBetweenTwenty.Count > 20)
            //            {
            //                for (int i = 0; i < 20; i++)
            //                {
            //                    if (count == 0) break;
            //                    selectedSpecialties.Add(isNotVisualIsNotPaidFiveBetweenTwenty[i]);
            //                    count--;
            //                }
            //            }
            //            else
            //            {
            //                for (int i = 0; i < isNotVisualIsNotPaidFiveBetweenTwenty.Count; i++)
            //                {
            //                    if (count == 0) break;
            //                    selectedSpecialties.Add(isNotVisualIsNotPaidFiveBetweenTwenty[i]);
            //                    count--;
            //                }
            //            }

            //            var isNotVisualIsNotPaidMinusFiveBetweenPlusFive = isNotVisual.Where(x => x.OrderedByStateScore <= (inputBachelorData.Score + 20) && x.OrderedByStateScore > (inputBachelorData.Score - 20)).ToList();
            //            if (isNotVisualIsNotPaidMinusFiveBetweenPlusFive.Count > 20)
            //            {
            //                for (int i = 0; i < 20; i++)
            //                {
            //                    if (count == 0) break;
            //                    selectedSpecialties.Add(isNotVisualIsNotPaidMinusFiveBetweenPlusFive[i]);
            //                    count--;
            //                }
            //            }
            //            else
            //            {
            //                for (int i = 0; i < isNotVisualIsNotPaidMinusFiveBetweenPlusFive.Count; i++)
            //                {
            //                    if (count == 0) break;
            //                    selectedSpecialties.Add(isNotVisualIsNotPaidMinusFiveBetweenPlusFive[i]);
            //                    count--;
            //                }
            //            }

            //            var isNotVisualIsNotPaidMinusFiveBetweenMinusTwenty = isNotVisual.Where(x => x.OrderedByStateScore <= (inputBachelorData.Score - 20) && x.OrderedByStateScore > (inputBachelorData.Score - 60)).ToList();
            //            if (isNotVisualIsNotPaidMinusFiveBetweenMinusTwenty.Count > 20)
            //            {
            //                for (int i = 0; i < 20; i++)
            //                {
            //                    if (count == 0) break;
            //                    selectedSpecialties.Add(isNotVisualIsNotPaidMinusFiveBetweenMinusTwenty[i]);
            //                    count--;
            //                }
            //            }
            //            else
            //            {
            //                for (int i = 0; i < isNotVisualIsNotPaidMinusFiveBetweenMinusTwenty.Count; i++)
            //                {
            //                    if (count == 0) break;
            //                    selectedSpecialties.Add(isNotVisualIsNotPaidMinusFiveBetweenMinusTwenty[i]);
            //                    count--;
            //                }
            //            }

            //            var isNotVisualIsNotPaidMinusTwentyBetweenMinusFifty = isNotVisual.Where(x => x.OrderedByStateScore <= (inputBachelorData.Score - 60) && x.OrderedByStateScore > (inputBachelorData.Score - 100)).ToList();
            //            if (isNotVisualIsNotPaidMinusTwentyBetweenMinusFifty.Count > 20)
            //            {
            //                for (int i = 0; i < 20; i++)
            //                {
            //                    if (count == 0) break;
            //                    selectedSpecialties.Add(isNotVisualIsNotPaidMinusTwentyBetweenMinusFifty[i]);
            //                    count--;
            //                }
            //            }
            //            else
            //            {
            //                for (int i = 0; i < isNotVisualIsNotPaidMinusTwentyBetweenMinusFifty.Count; i++)
            //                {
            //                    if (count == 0) break;
            //                    selectedSpecialties.Add(isNotVisualIsNotPaidMinusTwentyBetweenMinusFifty[i]);
            //                    count--;
            //                }
            //            }

            //            var isNotVisualIsNotPaidMinusFiftyBetweenMinusEighty = isNotVisual.Where(x => x.OrderedByStateScore <= (inputBachelorData.Score - 100) && x.OrderedByStateScore >= (inputBachelorData.Score - 140)).ToList();
            //            if (isNotVisualIsNotPaidMinusFiftyBetweenMinusEighty.Count > 20)
            //            {
            //                for (int i = 0; i < 20; i++)
            //                {
            //                    if (count == 0) break;
            //                    selectedSpecialties.Add(isNotVisualIsNotPaidMinusFiftyBetweenMinusEighty[i]);
            //                    count--;
            //                }
            //            }
            //            else
            //            {
            //                for (int i = 0; i < isNotVisualIsNotPaidMinusFiftyBetweenMinusEighty.Count; i++)
            //                {
            //                    if (count == 0) break;
            //                    selectedSpecialties.Add(isNotVisualIsNotPaidMinusFiftyBetweenMinusEighty[i]);
            //                    count--;
            //                }
            //            }

            //            if (selectedSpecialties.Count < 100)
            //            {
            //                count = 100 - selectedSpecialties.Count;

            //                var isNotVisualIsPaidFiveBetweenTwenty = isNotVisual.Where(x => x.PaidScore <= (inputBachelorData.Score + 60) && x.PaidScore > (inputBachelorData.Score + 20)).ToList();
            //                if (isNotVisualIsPaidFiveBetweenTwenty.Count > 20)
            //                {
            //                    for (int i = 0; i < 20; i++)
            //                    {
            //                        if (count == 0) break;
            //                        selectedSpecialties.Add(isNotVisualIsPaidFiveBetweenTwenty[i]);
            //                        count--;
            //                    }
            //                }
            //                else
            //                {
            //                    for (int i = 0; i < isNotVisualIsPaidFiveBetweenTwenty.Count; i++)
            //                    {
            //                        if (count == 0) break;
            //                        selectedSpecialties.Add(isNotVisualIsPaidFiveBetweenTwenty[i]);
            //                        count--;
            //                    }
            //                }

            //                var isNotVisualIsPaidMinusFiveBetweenPlusFive = isNotVisual.Where(x => x.PaidScore <= (inputBachelorData.Score + 20) && x.PaidScore > (inputBachelorData.Score - 20)).ToList();
            //                if (isNotVisualIsPaidMinusFiveBetweenPlusFive.Count > 20)
            //                {
            //                    for (int i = 0; i < 20; i++)
            //                    {
            //                        if (count == 0) break;
            //                        selectedSpecialties.Add(isNotVisualIsPaidMinusFiveBetweenPlusFive[i]);
            //                        count--;
            //                    }
            //                }
            //                else
            //                {
            //                    for (int i = 0; i < isNotVisualIsPaidMinusFiveBetweenPlusFive.Count; i++)
            //                    {
            //                        if (count == 0) break;
            //                        selectedSpecialties.Add(isNotVisualIsPaidMinusFiveBetweenPlusFive[i]);
            //                        count--;
            //                    }
            //                }

            //                var isNotVisualIsPaidMinusFiveBetweenMinusTwenty = isNotVisual.Where(x => x.PaidScore <= (inputBachelorData.Score - 20) && x.PaidScore > (inputBachelorData.Score - 60)).ToList();
            //                if (isNotVisualIsPaidMinusFiveBetweenMinusTwenty.Count > 20)
            //                {
            //                    for (int i = 0; i < 20; i++)
            //                    {
            //                        if (count == 0) break;
            //                        selectedSpecialties.Add(isNotVisualIsPaidMinusFiveBetweenMinusTwenty[i]);
            //                        count--;
            //                    }
            //                }
            //                else
            //                {
            //                    for (int i = 0; i < isNotVisualIsPaidMinusFiveBetweenMinusTwenty.Count; i++)
            //                    {
            //                        if (count == 0) break;
            //                        selectedSpecialties.Add(isNotVisualIsPaidMinusFiveBetweenMinusTwenty[i]);
            //                        count--;
            //                    }
            //                }

            //                var isNotVisualIsPaidMinusTwentyBetweenMinusFifty = isNotVisual.Where(x => x.PaidScore <= (inputBachelorData.Score - 60) && x.PaidScore > (inputBachelorData.Score - 100)).ToList();
            //                if (isNotVisualIsPaidMinusTwentyBetweenMinusFifty.Count > 20)
            //                {
            //                    for (int i = 0; i < 20; i++)
            //                    {
            //                        if (count == 0) break;
            //                        selectedSpecialties.Add(isNotVisualIsPaidMinusTwentyBetweenMinusFifty[i]);
            //                        count--;
            //                    }
            //                }
            //                else
            //                {
            //                    for (int i = 0; i < isNotVisualIsPaidMinusTwentyBetweenMinusFifty.Count; i++)
            //                    {
            //                        if (count == 0) break;
            //                        selectedSpecialties.Add(isNotVisualIsPaidMinusTwentyBetweenMinusFifty[i]);
            //                        count--;
            //                    }
            //                }

            //                var isNotVisualIsPaidMinusFiftyBetweenMinusEighty = isNotVisual.Where(x => x.PaidScore <= (inputBachelorData.Score - 100) && x.PaidScore >= (inputBachelorData.Score - 140)).ToList();
            //                if (isNotVisualIsPaidMinusFiftyBetweenMinusEighty.Count > 20)
            //                {
            //                    for (int i = 0; i < 20; i++)
            //                    {
            //                        if (count == 0) break;
            //                        selectedSpecialties.Add(isNotVisualIsPaidMinusFiftyBetweenMinusEighty[i]);
            //                        count--;
            //                    }
            //                }
            //                else
            //                {
            //                    for (int i = 0; i < isNotVisualIsPaidMinusFiftyBetweenMinusEighty.Count; i++)
            //                    {
            //                        if (count == 0) break;
            //                        selectedSpecialties.Add(isNotVisualIsPaidMinusFiftyBetweenMinusEighty[i]);
            //                        count--;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            //else if (inputBachelorData.IsVisual && inputBachelorData.IsNotVisual && !inputBachelorData.isPaid && inputBachelorData.isNotPaid)
            //{

            //}
            //else if (inputBachelorData.IsVisual && inputBachelorData.IsNotVisual && inputBachelorData.isPaid && !inputBachelorData.isNotPaid)
            //{

            //}
            //else if (inputBachelorData.IsVisual && !inputBachelorData.IsNotVisual && inputBachelorData.isPaid && inputBachelorData.isNotPaid)
            //{

            //}
            //else if (inputBachelorData.IsVisual && !inputBachelorData.IsNotVisual && !inputBachelorData.isPaid && inputBachelorData.isNotPaid)
            //{

            //}
            //else if (inputBachelorData.IsVisual && !inputBachelorData.IsNotVisual && inputBachelorData.isPaid && !inputBachelorData.isNotPaid)
            //{

            //}
            //else if (!inputBachelorData.IsVisual && inputBachelorData.IsNotVisual && inputBachelorData.isPaid && inputBachelorData.isNotPaid)
            //{

            //}
            //else if (!inputBachelorData.IsVisual && inputBachelorData.IsNotVisual && !inputBachelorData.isPaid && inputBachelorData.isNotPaid)
            //{

            //}
            //else if (!inputBachelorData.IsVisual && inputBachelorData.IsNotVisual && inputBachelorData.isPaid && !inputBachelorData.isNotPaid)
            //{

            //}
            #endregion

        }
    }
}