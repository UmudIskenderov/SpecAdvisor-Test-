using SpecAdvisor.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecAdvisor.Entities
{
    public class Specialty
    {
        public int Id { get; set; }
        public Group Group { get; set; }
        public University University { get; set; }
        public string Name { get; set; }
        public bool IsVisual { get; set; }
        public bool IsPaid { get; set; }
        public double AccessScore { get; set; }
    }
}
