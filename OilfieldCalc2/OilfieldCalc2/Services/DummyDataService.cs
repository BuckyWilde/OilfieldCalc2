using OilfieldCalc2.Models;
using OilfieldCalc2.Models.DrillstringTubulars;
using OilfieldCalc2.Models.MeasureableUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OilfieldCalc2.Services
{
    public class DummyDataService : IDataService
    {
        public Task<int> DeleteItemAsync(ITubular tubular)
        {
            System.Diagnostics.Debug.WriteLine("Deleting Tubular... " + tubular.ToString());
            return Task.FromResult(0);
        }

        public Task<ITubular> GetItemAsync(int tubularId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ITubular>> GetDrillstringItemsAsync()
        {
            //return list of made up items
            List<IDrillstringTubular> tubularList = new List<IDrillstringTubular>();

            var dst = (IDrillstringTubular)new Tubular().Create(DrillstringTubularType.DrillPipe);
            var dst2 = (IDrillstringTubular)new Tubular().Create(DrillstringTubularType.DrillCollar);
            var dst1 = (IDrillstringTubular)new Tubular().Create(DrillstringTubularType.HeviWateDrillPipe);

            dst.Length = new Measurement(1000d * MeasurementUnitService.GetCurrentLongLengthUnit().ConversionFactor, MeasurementUnitService.GetCurrentLongLengthUnit());
            dst.OutsideDiameter = new Measurement(139.7d * MeasurementUnitService.GetCurrentShortLengthUnit().ConversionFactor, MeasurementUnitService.GetCurrentShortLengthUnit());
            dst.InsideDiameter = new Measurement(81.26d * MeasurementUnitService.GetCurrentShortLengthUnit().ConversionFactor, MeasurementUnitService.GetCurrentShortLengthUnit());
            dst.AdjustedWeightPerUnit = new Measurement(32.85d * MeasurementUnitService.GetCurrentMassUnit().ConversionFactor, MeasurementUnitService.GetCurrentMassUnit());
            dst.ItemSortOrder = 0;
            tubularList.Add(dst);
            
            dst2.Length = new Measurement(1500d * MeasurementUnitService.GetCurrentLongLengthUnit().ConversionFactor, MeasurementUnitService.GetCurrentLongLengthUnit());
            dst2.OutsideDiameter = new Measurement(139.7d * MeasurementUnitService.GetCurrentShortLengthUnit().ConversionFactor, MeasurementUnitService.GetCurrentShortLengthUnit());
            dst2.InsideDiameter = new Measurement(83.66d * MeasurementUnitService.GetCurrentShortLengthUnit().ConversionFactor, MeasurementUnitService.GetCurrentShortLengthUnit());
            dst2.AdjustedWeightPerUnit = new Measurement(36.11d * MeasurementUnitService.GetCurrentMassUnit().ConversionFactor, MeasurementUnitService.GetCurrentMassUnit());
            dst2.ItemSortOrder = 2;
            tubularList.Add(dst2);

            dst1.Length = new Measurement(526.12d * MeasurementUnitService.GetCurrentLongLengthUnit().ConversionFactor, MeasurementUnitService.GetCurrentLongLengthUnit());
            dst1.OutsideDiameter = new Measurement(139.7d * MeasurementUnitService.GetCurrentShortLengthUnit().ConversionFactor, MeasurementUnitService.GetCurrentShortLengthUnit());
            dst1.InsideDiameter = new Measurement(45.6d * MeasurementUnitService.GetCurrentShortLengthUnit().ConversionFactor, MeasurementUnitService.GetCurrentShortLengthUnit());
            dst1.AdjustedWeightPerUnit = new Measurement(122.32d * MeasurementUnitService.GetCurrentMassUnit().ConversionFactor, MeasurementUnitService.GetCurrentMassUnit());
            dst1.ItemSortOrder = 1;
            tubularList.Add(dst1);

            //Return the list sorted by the ItemSortOrder field
            return Task.FromResult((IEnumerable<ITubular>)tubularList.OrderBy(x => x.ItemSortOrder));
        }

        public Task<IEnumerable<ITubular>> GetWellboreItemsAsync()
        {
            List<IWellboreTubular> tubularList = new List<IWellboreTubular>();

            var wbt = (IWellboreTubular)new Tubular().Create(WellboreTubularType.Casing);
            var wbt2 = (IWellboreTubular)new Tubular().Create(WellboreTubularType.OpenHole);

            wbt.StartDepth = 0;
            wbt.EndDepth = 524;
            wbt.InsideDiameter = new Measurement(224.4d * MeasurementUnitService.GetCurrentShortLengthUnit().ConversionFactor, MeasurementUnitService.GetCurrentShortLengthUnit());
            tubularList.Add(wbt);

            wbt2.StartDepth = 524;
            wbt2.EndDepth = 5120;
            wbt2.InsideDiameter = new Measurement(211d * MeasurementUnitService.GetCurrentShortLengthUnit().ConversionFactor, MeasurementUnitService.GetCurrentShortLengthUnit());
            tubularList.Add(wbt2);

            return Task.FromResult((IEnumerable<ITubular>)tubularList);
        }

        public Task SaveItemAsync(ITubular tubular)
        {
            System.Diagnostics.Debug.WriteLine("Saving Tubular... " + tubular.ToString());
            return Task.FromResult(0);
        }

        public Task<IEnumerable<T>> GetTubularItemsAsync<T>() where T : ITubular, new()
        {
            throw new NotImplementedException();
        }

        Task<int> IDataService.SaveItemAsync(ITubular tubular)
        {
            throw new NotImplementedException();
        }

        public Task<int> ClearTable<T>()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RepairTable<T>() where T : ITubular, new()
        {
            throw new NotImplementedException();
        }
    }
}
