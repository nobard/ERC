using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ERC.Models
{
    public class Meters : IDataErrorInfo
    {
        public string ResidentsCount { get; set; }
        public string HvsIndication { get; set; }
        public string GvsIndication { get; set; }
        public string EeIndication { get; set; }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "ResidentsCount":
                        if (String.IsNullOrEmpty(ResidentsCount)) break;

                        if (!int.TryParse(ResidentsCount, out var a))
                        {
                            error = "Только числа";
                        }
                        break;
                    case "HvsIndication":
                        //Обработка ошибок для свойства Name
                        break;
                    case "GvsIndication":
                        //Обработка ошибок для свойства Position
                        break;
                    case "EeIndication":
                        //Обработка ошибок для свойства Position
                        break;
                }
                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
