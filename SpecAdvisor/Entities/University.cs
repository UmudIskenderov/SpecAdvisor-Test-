using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecAdvisor.Entities
{
    public class University
    {
        public byte Id { get; set; }
        public City City { get; set; }
        public string Name { get; set; }
        public bool IsPrivate { get; set; }
    }
}
