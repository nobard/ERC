using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERC.Models
{
    public class CalculatedService
    {
        public string Name { get; set; }
        public string CurrIndication { get; set; }
        public string PrevIndication { get; set; }
        public float Volume { get; set; }
        public string Unit { get; set; }
        public float Price { get; set; }
    }
}