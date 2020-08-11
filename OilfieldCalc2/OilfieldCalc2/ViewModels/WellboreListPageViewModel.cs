using OilfieldCalc2.Models;
using OilfieldCalc2.Models.MeasureableUnit;
using OilfieldCalc2.Models.WellboreTubulars;
using OilfieldCalc2.Services;
using OilfieldCalc2.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OilfieldCalc2.ViewModels
{
    public class WellboreListPageViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _pageDialogservice;

        #region Properties
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        private IWellboreTubular _selectedItem;
        public IWellboreTubular SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnDeleteCommand.RaiseCanExecuteChanged();
                OnEditCommand.RaiseCanExecuteChanged();
            }
        }

        private ObservableCollection<ITubular> _wellboreTubulars;
        public ObservableCollection<ITubular> WellboreTubulars
        {
            get => _wellboreTubulars;
            set => SetProperty(ref _wellboreTubulars, value);
        }

        private double _startDepth;
        public double StartDepth
        {
            get => _startDepth;
            set => SetProperty(ref _startDepth, value);
        }

        private double _endDepth;
        public double EndDepth
        {
            get => _endDepth;
            set => SetProperty(ref _endDepth, value);
        }

        private double _insideDiameter;
        public double InsideDiameter
        {
            get => _insideDiameter;
            set => SetProperty(ref _insideDiameter, value);
        }

        //total internal volume capacity of all the tubulars
        private double _totalVolume;
        public double TotalVolume
        {
            get => _totalVolume;
            set => SetProperty(ref _totalVolume, value);
        }

        //Total length of all the tubulars
        private double _totalTublarLength;
        public double TotalTubularLength
        {
            get => _totalTublarLength;
            set => SetProperty(ref _totalTublarLength, value);
        }

        private UnitOfMeasure _shortLengthUnit;
        public UnitOfMeasure ShortLengthUnit
        {
            get => _shortLengthUnit;
            set => SetProperty(ref _shortLengthUnit, value);
        }

        private UnitOfMeasure _longLengthUnit;
        public UnitOfMeasure LongLengthUnit
        {
            get => _longLengthUnit;
            set => SetProperty(ref _longLengthUnit, value);
        }

        private UnitOfMeasure _volumeUnit;
        public UnitOfMeasure VolumeUnit
        {
            get => _volumeUnit;
            set => SetProperty(ref _volumeUnit, value);
        }

        private UnitOfMeasure _capacityUnit;
        public UnitOfMeasure CapacityUnit
        {
            get => _capacityUnit;
            set => SetProperty(ref _capacityUnit, value);
        }
        #endregion

        public DelegateCommand OnDeleteCommand { get; }
        public DelegateCommand OnEditCommand { get; }

        public WellboreListPageViewModel(IDataService dataService, INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
            _pageDialogservice = pageDialogService;

            Title = "Wellbore List Page";

            OnDeleteCommand = new DelegateCommand(Delete, CanDelete);
            OnEditCommand = new DelegateCommand(Edit, CanEdit);
        }

        private bool CanEdit()
        {
            return SelectedItem != null;
        }

        #region Command Methods
        /// <summary>
        /// Navigate to the DrillstringDetailPage and pass in the
        /// currently selected DrillstringTubular for editting
        /// </summary>
        private async void Edit()
        {
            if (SelectedItem is IWellboreTubular wbt)
            {
                var navigationParams = new NavigationParameters();
                navigationParams.Add("wellboreTubular", wbt);
                await _navigationService.NavigateAsync(nameof(WellboreDetailPage), navigationParams).ConfigureAwait(false);
            }
        }

        private bool CanDelete()
        {
            return SelectedItem != null;
        }

        private void Delete()
        {
            if (SelectedItem is IWellboreTubular wbt)
            {
                WellboreTubulars.Remove(wbt);   //remove the item from the collection
                _dataService.DeleteItem(wbt); //Delete the record from the database
            }

            //Recalculate to compensate for the deleted item
            TotalTubularLength = GetTotalTublarLength();
            TotalVolume = GetTotalInternalVolume();
        }
        #endregion

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedToAsync(parameters);
            IsRefreshing = true;
            bool corrupted = false;

            //Make sure these reinitialize to false when returning to this screen...
            SelectedItem = null;
            OnDeleteCommand.RaiseCanExecuteChanged();
            OnEditCommand.RaiseCanExecuteChanged();

            //Load the drillstring Tubulars from the dataservice.
            WellboreTubulars = new ObservableCollection<ITubular>(_dataService.GetTubularItems<WellboreTubularBase>());

            //check database for null values or other coruptness
            foreach (WellboreTubularBase wbt in WellboreTubulars)
            {
                if (wbt.StartBlobbed == null ||
                    wbt.EndBlobbed == null ||
                    wbt.IDBlobbed == null)
                {
                    corrupted = true;
                }
            }

            if (corrupted)
            {
                var answer = await _pageDialogservice.DisplayAlertAsync("Warning", "Database appears to be corrupt. Attempting to repair", "Ok", "Cancel");
                if (!answer)
                    await _navigationService.NavigateAsync(nameof(MainPage)).ConfigureAwait(false);
                if (answer)
                {
                    bool repaired = _dataService.RepairTable<WellboreTubularBase>();

                    if (!repaired)
                        _dataService.ClearTable<WellboreTubularBase>();

                    //re-initialize properties after changes.
                    WellboreTubulars = new ObservableCollection<ITubular>(_dataService.GetTubularItems<WellboreTubularBase>());
                    TotalTubularLength = GetTotalTublarLength();
                    TotalVolume = GetTotalInternalVolume();
                }
            }

            //Set Units of measure
            ShortLengthUnit = MeasurementUnitService.GetCurrentShortLengthUnit();
            LongLengthUnit = MeasurementUnitService.GetCurrentLongLengthUnit();
            VolumeUnit = MeasurementUnitService.GetCurrentVolumeUnit();
            CapacityUnit = MeasurementUnitService.GetCurrentCapacityUnit();

            //Initialize the totals on loading the page
            TotalTubularLength = GetTotalTublarLength();
            TotalVolume = GetTotalInternalVolume();

            IsRefreshing = false;
        }

        private double GetTotalTublarLength()
        {
            double totalLength = 0.00;

            if (WellboreTubulars != null && LongLengthUnit != null)
            {
                //return the total length of all tubulars in the collection
                foreach (IWellboreTubular wbtTemp in WellboreTubulars)
                {
                    if (wbtTemp.StartDepth != null && wbtTemp.EndDepth != null)
                    {
                        if (totalLength < wbtTemp.EndDepth.Value)
                        totalLength = wbtTemp.EndDepth.Value;
                    }
                }

                return totalLength;
            }
            return totalLength;
        }

        private double GetTotalInternalVolume()
        {
            double totalVolume = 0.00;

            if (WellboreTubulars != null && VolumeUnit != null)
            {
                //totalVolume = DrillstringTubulars[0].TotalInternalCapacity;
                foreach (IWellboreTubular wbt in WellboreTubulars)
                    totalVolume += wbt.TotalInternalCapacity;

                return totalVolume;
            }

            return totalVolume;
        }
    }
}
