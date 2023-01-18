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
        public float CurrIndication { get; set; }
        public float PrevIndication { get; set; }
        public float Volume { get; set; }
        public float Price { get; set; }
    }
}