using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ERC.Data;
using ERC.Models;
using ERC.Utilities;
using ERC.Views;
using static ERC.Models.CalculationAlgorithms;


namespace ERC.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private Meters meters;
        private List<Service> bd;
        private List<CalculatedService> calculatedServices = new List<CalculatedService>();
        private string hvsIndication;
        private string gvsIndication;
        private string eeDayIndication;
        private string eeNightIndication;
        public bool HvsCheckBox { get; set; }
        public bool GvsCheckBox { get; set; }
        public bool EeCheckBox { get; set; }
        public string ResidentsCount { get; set; }

        public string HvsIndication
        {
            get
            {
                if (HvsCheckBox) return hvsIndication;

                return hvsIndication = "";
            }
            set
            {
                hvsIndication = value;
            }
        }

        public string GvsIndication
        {
            get
            {
                if (GvsCheckBox) return gvsIndication;

                return gvsIndication = "";
            }
            set
            {
                gvsIndication = value;
            }
        }

        public string EeDayIndication
        {
            get
            {
                if (EeCheckBox) return eeDayIndication;

                return eeDayIndication = "";
            }
            set
            {
                eeDayIndication = value;
            }
        }

        public string EeNightIndication
        {
            get
            {
                if (EeCheckBox) return eeNightIndication;

                return eeNightIndication = "";
            }
            set
            {
                eeNightIndication = value;
            }
        }

        public Meters Meters
        {
            get
            {
                return meters = meters ?? new Meters();
            }
        }

        #region COMMANDS

        private ICommand estimateMetersCommand;
        public ICommand EstimateMetersCommand
        {
            get
            {
                return estimateMetersCommand = estimateMetersCommand ?? new DelegateCommand((o) => EstimateMeters());
            }
        }

        //private ICommand fillBDCommand;
        //public ICommand FillBDCommand
        //{
        //    get
        //    {
        //        return fillBDCommand = fillBDCommand ?? new DelegateCommand((o) => App.ServicesDb.FillBD());
        //    }
        //}

        private async void EstimateMeters()
        {
            bd = bd ?? await App.ServicesDb.GetAllServicesAsync();

            if (HvsCheckBox)
            { 
                calculatedServices.Add(GetCalculatedService(HvsIndication, "HVS"));
            }
            else
            {
                calculatedServices.Add(GetNormCalculatedService("HVS"));
            }

            if (GvsCheckBox)
            {
                calculatedServices.AddRange(new[]
                {
                    GetCalculatedService(GvsIndication, "GVSTn"),
                    GetCalculatedService(GvsIndication, "GVSTe")
                });
            }
            else
            {
                calculatedServices.Add(GetNormCalculatedService("GVS"));
            }

            if (EeCheckBox)
            {
                calculatedServices.AddRange(new[]
                {
                    GetCalculatedService(EeDayIndication, "EEDay"),
                    GetCalculatedService(EeNightIndication, "EENight")
                });
            }
            else
            { 
                calculatedServices.Add(GetNormCalculatedService("EE"));
            }

            // показываем окно результатов и закрываем главное

            OpenResultWindow();
        }

        #endregion

        #region HELP METHODS

        private CalculatedService GetCalculatedService(string currIndication, string name)
        {
            if (!float.TryParse(currIndication, out var currVolume)) throw new Exception();

            var service = bd.Find(e => e.Name == name);

            if (service == null) throw new NullReferenceException();

            float result;
            float volume = VBaseCalculation(currVolume);

            if (name == "GVSTe")
            {
                result = BaseCalculation(GvsTeCalculation(volume, service.Norm), service.Tariff);
            }
            else
            {
                result =  BaseCalculation(volume, service.Tariff);
            }

            return new CalculatedService
            {
                Name = name, 
                CurrIndication = currVolume.ToString(), 
                PrevIndication = "0",
                Volume = volume, 
                Unit = service.Unit, 
                Price = result
            };
        }

        private CalculatedService GetNormCalculatedService(string name)
        {
            if (!int.TryParse(ResidentsCount, out var residents)) throw new Exception();

            var service = bd.Find(e => e.Name == name);

            if (service == null) throw new NullReferenceException();

            float result;
            float volume = VIndicationCalculation(residents, service.Norm);

            result = BaseCalculation(volume, service.Tariff);

            return new CalculatedService
            {
                Name = name,
                CurrIndication = "-",
                PrevIndication = "-",
                Volume = volume,
                Unit = service.Unit,
                Price = result
            };
        }

        private void OpenResultWindow()
        {
            var currWindow = Application.Current.MainWindow;

            Application.Current.MainWindow = new ResultWindow(calculatedServices);
            Application.Current.MainWindow.Show();

            CloseCurrWindow(currWindow);
        }
        private void CloseCurrWindow(Window currWindow)
        {
            currWindow.Close();
        }

        #endregion
    }
}
