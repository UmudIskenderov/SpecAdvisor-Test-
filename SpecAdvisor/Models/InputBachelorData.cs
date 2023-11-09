using SpecAdvisor.Entities;
using SpecAdvisor.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecAdvisor.Models
{
    public class InputBachelorData
    {
        public InputBachelorData()
        {
            Universities = new List<University>();
        }
        public List<University> Universities { get; set; }
        public Group Group { get; set; }
        public bool IsVisual { get; set; }
        public bool IsNotVisual { get; set; }
        public bool isPaid { get; set; }
        public bool isNotPaid { get; set; }
        public double Score { get; set; }
    }
}
