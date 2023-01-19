using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ERC.Models;
using ERC.Utilities;
using ERC.Views;

namespace ERC.ViewModels
{
    public class ResultWindowViewModel : BaseViewModel
    {
        public List<CalculatedService> CalculatedServicesList { get; set; }
        public float ResultPrice { get; set; }

        public ResultWindowViewModel(List<CalculatedService> calculatedServicesList)
        {
            CalculatedServicesList = calculatedServicesList;
            ResultPrice = calculatedServicesList.Sum(x => x.Price);
        }

        private ICommand recalculateCommand;
        public ICommand RecalculateCommand
        {
            get
            {
                return recalculateCommand = recalculateCommand ?? new DelegateCommand((o) => OpenMainWindow());
            }
        }

        private void OpenMainWindow()
        {
            var currWindow = Application.Current.MainWindow;

            Application.Current.MainWindow = new MainWindow();
            Application.Current.MainWindow.Show();

            CloseCurrWindow(currWindow);
        }
        private void CloseCurrWindow(Window currWindow)
        {
            currWindow.Close();
        }
    }
}
