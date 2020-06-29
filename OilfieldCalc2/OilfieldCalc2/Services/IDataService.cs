using OilfieldCalc2.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OilfieldCalc2.Services
{
    public interface IDataService
    {
        Task<IEnumerable<T>> GetTubularItemsAsync<T>() where T : ITubular, new();
        Task<ITubular> GetItemAsync(int tubularId);
        Task<int> SaveItemAsync(ITubular tubular);
        Task<int> DeleteItemAsync(ITubular tubular);
        Task<int> ClearTable<T>();
        Task<bool> RepairTable<T>() where T : ITubular, new();
    }
}
