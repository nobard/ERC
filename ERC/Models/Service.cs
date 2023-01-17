using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ERC.Models
{
    public class Service
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public float Tariff { get; set; }
        public float Norm { get; set; }
        public string Unit { get; set; }
    }
}
