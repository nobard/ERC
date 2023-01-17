using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERC.Models;
using SQLite;

namespace ERC.Data
{
    public class ServicesDB
    {
        readonly SQLiteAsyncConnection db;

        public ServicesDB(string connectionString)
        {
            db = new SQLiteAsyncConnection(connectionString);
            db.CreateTableAsync<Service>().Wait();
        }

        public Task<List<Service>> GetServicesAsync()
        {
            return db.Table<Service>().ToListAsync();
        }

        public Task<int> AddServiceAsync(Service service)
        {
            return db.InsertAsync(service);
        }

        // "Заполним" БД
        public void FillBD()
        {
            AddServiceAsync(new Service() { Name = "HVS", Tariff = 35.78f, Norm = 4.85f, Unit = "м^3"});
            AddServiceAsync(new Service() { Name = "GVS", Tariff = 158.98f, Norm = 4.01f, Unit = "м^3" });
            AddServiceAsync(new Service() { Name = "EE", Tariff = 4.28f, Norm = 164f, Unit = "кВт * ч" });
            AddServiceAsync(new Service() { Name = "EEDay", Tariff = 4.9f, Unit = "кВт * ч" });
            AddServiceAsync(new Service() { Name = "EENight", Tariff = 2.31f, Unit = "кВт * ч" });
            AddServiceAsync(new Service() { Name = "GVSTn", Tariff = 35.78f, Norm = 4.01f, Unit = "м^3" });
            AddServiceAsync(new Service() { Name = "GVSTe", Tariff = 998.69f, Norm = 0.05349f, Unit = "Гкал" });
        }
    }
}
