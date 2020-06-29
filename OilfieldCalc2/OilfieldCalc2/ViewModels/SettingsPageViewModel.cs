using OilfieldCalc2.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using OilfieldCalc2.Models.DrillstringTubulars;
using OilfieldCalc2.Models;
using OilfieldCalc2.Models.MeasureableUnit;
using Xamarin.Forms;

namespace OilfieldCalc2.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        readonly INavigationService _navigationService;
        readonly IDataService _dataService;

        private List<DrillstringTubularBase> _drillstringTubulars;
        public List<DrillstringTubularBase> DrillstringTubulars
        {
            get => _drillstringTubulars;
            set => SetProperty(ref _drillstringTubulars, value);
        }

        private List<UnitOfMeasure> _longLengthUnits;
        public List<UnitOfMeasure> LongLengthUnits { get => _longLengthUnits; set => SetProperty(ref _longLengthUnits, value); }

        private List<UnitOfMeasure> _shortLengthUnits;
        public List<UnitOfMeasure> ShortLengthUnits { get => _shortLengthUnits; set => SetProperty(ref _shortLengthUnits, value); }

        private List<UnitOfMeasure> _volumeUnits;
        public List<UnitOfMeasure> VolumeUnits { get => _volumeUnits; set => SetProperty(ref _volumeUnits, value); }

        private List<UnitOfMeasure> _capacityUnits;
        public List<UnitOfMeasure> CapacityUnits { get => _capacityUnits; set => SetProperty(ref _capacityUnits, value); }

        private List<UnitOfMeasure> _massUnits;
        public List<UnitOfMeasure> MassUnits { get => _massUnits; set => SetProperty(ref _massUnits, value); }

        private UnitOfMeasure _selectedLongLengthUnit;
        public UnitOfMeasure SelectedLongLengthUnit
        {
            get => _selectedLongLengthUnit;
            set
            {
                SetProperty(ref _selectedLongLengthUnit, value);
                if (value != null)
                {
                    //Update the values in the service
                    MeasurementUnitService.SetLongLengthUnit(value);

                    //Update the values in the database tables with the new values
                    foreach(DrillstringTubularBase tubular in DrillstringTubulars)
                    {
                        if (tubular.Length != null)
                        {
                            if (tubular.Length.GetUnitOfMeasure() != value)
                            {
                                //conversion is required
                                tubular.Length.Convert(value);

                                //update the record in the database
                                _dataService.SaveItemAsync(tubular);
                            }
                        }
                    }
                }
            }
        }

        private UnitOfMeasure _selectedShortLengthUnit;
        public UnitOfMeasure SelectedShortLengthUnit
        {
            get => _selectedShortLengthUnit;
            set
            {
                SetProperty(ref _selectedShortLengthUnit, value);
                if (value != null)
                    MeasurementUnitService.SetShortLengthUnit(value);

                //Update the values in the database tables with the new values
                foreach (DrillstringTubularBase tubular in DrillstringTubulars)
                {
                    if (tubular.OutsideDiameter != null && tubular.InsideDiameter != null)
                    {
                        if (tubular.OutsideDiameter.GetUnitOfMeasure() != value || tubular.InsideDiameter.GetUnitOfMeasure() != value)
                        {
                            //conversion is required
                            tubular.OutsideDiameter.Convert(value);
                            tubular.InsideDiameter.Convert(value);

                            //update the record in the database
                            _dataService.SaveItemAsync(tubular);
                        }
                    }
                }
            }
        }

        private UnitOfMeasure _selectedVolumeUnit;
        public UnitOfMeasure SelectedVolumeUnit
        {
            get => _selectedVolumeUnit;
            set
            {
                SetProperty(ref _selectedVolumeUnit, value);
                if (value != null)
                    MeasurementUnitService.SetVolumeUnit(value);
            }
        }

        private UnitOfMeasure _selectedCapacityUnit;
        public UnitOfMeasure SelectedCapacityUnit
        {
            get => _selectedCapacityUnit;
            set
            {
                SetProperty(ref _selectedCapacityUnit, value);
                if (value != null)
                    MeasurementUnitService.SetCapacityUnit(value);
            }
        }

        private UnitOfMeasure _selectedMassUnit;
        public UnitOfMeasure SelectedMassUnit
        {
            get => _selectedMassUnit;
            set
            {
                SetProperty(ref _selectedMassUnit, value);
                if (value != null)
                    MeasurementUnitService.SetMassUnit(value);
            }
        }

        public SettingsPageViewModel(INavigationService navigationService, IDataService dataService) : base(navigationService)
        {
            _navigationService = navigationService;
            _dataService = dataService;

            Title = "Settings";
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            DrillstringTubulars = new List<DrillstringTubularBase>(await _dataService.GetTubularItemsAsync<DrillstringTubularBase>());

            LongLengthUnits = GetUnitsOfMeasure.LongLengthUnits();
            ShortLengthUnits = GetUnitsOfMeasure.ShortLengthUnits();
            VolumeUnits = GetUnitsOfMeasure.VolumeUnits();
            CapacityUnits = GetUnitsOfMeasure.CapacityUnits();
            MassUnits = GetUnitsOfMeasure.MassUnits();

            //load the current unit selections into the picker boxes for display
            SelectedLongLengthUnit = LongLengthUnits.Where(p => p.UnitName == MeasurementUnitService.GetCurrentLongLengthUnit().UnitName).First();
            SelectedShortLengthUnit = ShortLengthUnits.Where(p => p.UnitName == MeasurementUnitService.GetCurrentShortLengthUnit().UnitName).First();
            SelectedVolumeUnit = VolumeUnits.Where(p => p.UnitName == MeasurementUnitService.GetCurrentVolumeUnit().UnitName).First();
            SelectedCapacityUnit = CapacityUnits.Where(p => p.UnitName == MeasurementUnitService.GetCurrentCapacityUnit().UnitName).First();
            SelectedMassUnit = MassUnits.Where(p => p.UnitName == MeasurementUnitService.GetCurrentMassUnit().UnitName).First();
        }
    }
}
