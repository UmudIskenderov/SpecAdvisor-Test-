using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisLogic
{
    public class SpecialtyData
    {
        //public string Code { get; set; }
        //public string UniversityName { get; set; }
        //public int Group { get; set; }
        //public string SpecialtyName { get; set; }
        //public char KindOfLesson { get; set; }
        //public double PaidScore { get; set; }
        //public double DSScore { get; set; }


        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsVisual { get; set; }
        public double Score { get; set; }
        public double ScoreWithPay { get; set; }
        public string GroupName { get; set; }
        public string UniversityName { get; set; }
    }
}
