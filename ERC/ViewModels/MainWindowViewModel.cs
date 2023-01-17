using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ERC.Data;
using ERC.Models;
using ERC.Utilities;

namespace ERC.ViewModels
{
    public class MainWindowViewModel : BaseViewModel, IDataErrorInfo
    {
        public Meters Meters { get; set; }
        private List<Service> bd;
        private string residentsCount;
        private string hvsIndication;
        private string gvsIndication;
        private string eeIndication;

        public string ResidentsCount
        {
            get => residentsCount;
            set => SetProperty(ref residentsCount, value);
        }

        public string HvsIndication
        {
            get => hvsIndication;
            set => SetProperty(ref hvsIndication, value);
        }

        public string GvsIndication
        {
            get => gvsIndication;
            set => SetProperty(ref gvsIndication, value);
        }

        public string EeIndication
        {
            get => eeIndication;
            set => SetProperty(ref eeIndication, value);
        }

        #region VALIDATION
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
        #endregion

        #region COMMANDS

        private ICommand estimateMetersCommand;
        public ICommand EstimateMetersCommand
        {
            get
            {
                return estimateMetersCommand = estimateMetersCommand ?? new DelegateCommand((o) => EstimateMeters());
            }
        }

        private async void EstimateMeters()
        {
            bd = bd ?? await App.ServicesDb.GetServicesAsync();

            if (!float.TryParse(HvsIndication, out var volume)) throw new Exception();

            var hvs = CalculationAlgorithms.BaseCalculation(volume, bd.Find(e => e.Name == "HVS").Tariff);
        }

        private ICommand fillBDCommand;
        public ICommand FillBDCommand
        {
            get
            {
                return fillBDCommand = fillBDCommand ?? new DelegateCommand((o) => App.ServicesDb.FillBD());
            }
        }



        #endregion
    }
}
