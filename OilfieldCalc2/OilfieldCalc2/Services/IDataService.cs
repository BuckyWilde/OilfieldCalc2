using OilfieldCalc2.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OilfieldCalc2.Services
{
    public interface IDataService
    {
        IEnumerable<T> GetTubularItems<T>() where T : ITubular, new();
        ITubular GetItem(int tubularId);
        int SaveItem(ITubular tubular);
        int DeleteItem(ITubular tubular);
        int ClearTable<T>();
        bool RepairTable<T>() where T : ITubular, new();
    }
}
