using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ERC.Data;

namespace ERC
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static ServicesDB servicesDb;

        public static ServicesDB ServicesDb
        {
            get
            {
                return servicesDb = servicesDb ?? new ServicesDB(Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ServicesDatabase.db3"));
            }
        }
    }
}
