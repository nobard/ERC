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
        private string eeIndication;
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
        }

        public string GvsIndication
        {
            get
            {
                if (GvsCheckBox) return gvsIndication;

                return gvsIndication = "";
            }
        }

        public string EeIndication
        {
            get
            {
                if (EeCheckBox) return eeIndication;

                return eeIndication = "";
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

        private ICommand fillBDCommand;
        public ICommand FillBDCommand
        {
            get
            {
                return fillBDCommand = fillBDCommand ?? new DelegateCommand((o) => App.ServicesDb.FillBD());
            }
        }

        private async void EstimateMeters()
        {
            bd = bd ?? await App.ServicesDb.GetAllServicesAsync();

            if (!float.TryParse(HvsIndication, out var hvsCurrVolume)) throw new Exception();

            CalculateService(hvsCurrVolume, "HVS");

            if (!float.TryParse(GvsIndication, out var gvsCurrVolume)) throw new Exception();

            CalculateService(gvsCurrVolume, "GVSTn");
            CalculateService(gvsCurrVolume, "GVSTe");

            if (!float.TryParse(EeIndication, out var eeCurrVolume)) throw new Exception();

            CalculateService(eeCurrVolume, "EE");
        }

        #endregion

        private void CalculateService(float currVolume, string name)
        {
            var service = bd.Find(e => e.Name == name);
            float result;

            if (name == "GVSTe")
            {
                result = BaseCalculation(GvsTeCalculation(VBaseCalculation(currVolume), service.Norm), service.Tariff);
            }
            else
            {
                result =  BaseCalculation(VBaseCalculation(currVolume), service.Tariff);
            }

            calculatedServices.Add(new CalculatedService { Name = name, CurrIndication = currVolume, Price = result});
        }
    }
}
