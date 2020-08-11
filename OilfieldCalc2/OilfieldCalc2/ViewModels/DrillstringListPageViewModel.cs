using OilfieldCalc2.Helpers;
using OilfieldCalc2.Models;
using OilfieldCalc2.Models.DrillstringTubulars;
using OilfieldCalc2.Models.MeasureableUnit;
using OilfieldCalc2.Services;
using OilfieldCalc2.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OilfieldCalc2.ViewModels
{
    public class DrillstringListPageViewModel : ViewModelBase
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

        private IDrillstringTubular _selectedItem;
        public IDrillstringTubular SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnDeleteCommand.RaiseCanExecuteChanged();
                OnEditCommand.RaiseCanExecuteChanged();
            }
        }

        private ObservableCollection<ITubular> _drillstringTubulars;
        public ObservableCollection<ITubular> DrillstringTubulars
        {
            get => _drillstringTubulars;
            set => SetProperty(ref _drillstringTubulars, value);
        }

        //total dry displacement of the steel
        private double _totalDisplacement;
        public double TotalDisplacement
        {
            get => _totalDisplacement;
            set => SetProperty(ref _totalDisplacement, value);
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

        //Total well depth based on the length of the wellbore tubulars
        private double _totalWellDepth;
        public double TotalWellDepth
        {
            get => _totalWellDepth;
            set => SetProperty(ref _totalWellDepth, value);
        }

        //Total weight of the tubulars (weight of the steel in air)
        private double _totalWeight;
        public double TotalWeight
        {
            get => _totalWeight;
            set => SetProperty(ref _totalWeight, value);
        }

        //State of the bitOnBottom toggle switch
        private bool _bitOnBottomToggle;
        public bool BitOnBottomToggle
        {
            get => _bitOnBottomToggle;
            set => SetProperty(ref _bitOnBottomToggle, value);                
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

        private UnitOfMeasure _massUnit;
        public UnitOfMeasure MassUnit
        {
            get => _massUnit;
            set => SetProperty(ref _massUnit, value);
        }
        #endregion Properties

        public DelegateCommand OnDeleteCommand { get; }
        public DelegateCommand OnEditCommand { get; }
        public DelegateCommand<object> OnUpCommand { get; }
        public DelegateCommand<object> OnDownCommand { get; }
        public DelegateCommand OnBitOnBottomToggledCommand { get; }
        public DelegateCommand OnBitDepthChangedCommand { get; }

        public DrillstringListPageViewModel(INavigationService navigationService, IDataService dataService, IPageDialogService pageDialogService) : base(navigationService)
        {
            Title = "Drillstring List Page";

            _dataService = dataService;
            _navigationService = navigationService;
            _pageDialogservice = pageDialogService;

            //Initialize commands
            OnDeleteCommand = new DelegateCommand(Delete, CanDelete);
            OnEditCommand = new DelegateCommand(Edit, CanEdit);
            OnUpCommand = new DelegateCommand<object>(OnUp, CanMoveUp);
            OnDownCommand = new DelegateCommand<object>(OnDown, CanMoveDown);
            OnBitOnBottomToggledCommand = new DelegateCommand(BitOnBottomToggled);
            OnBitDepthChangedCommand = new DelegateCommand(BitDepthChanged);            
        }

#region Command methods        
        /// <summary>
        /// This command will active every time the user changes the bit depth
        /// entry. The total drillstring length will need to match this number
        /// but not exceed the total well depth. To make the drill string total
        /// length match, the top drill string item length will be modified
        /// accordingly, possibly the next tubulars in line as well depending
        /// on the user entered value.
        /// </summary>
        private void BitDepthChanged()
        {
            //if (GetTotalTublarLength() != TotalTubularLength)
            //{
            //    //Cannot exceed the well depth for obvious reasons
            //    if (TotalTubularLength > TotalWellDepth)
            //        TotalTubularLength = GetWellDepth();

            //    double difference = GetOriginalTotalTubularLength() - TotalTubularLength;
            //    int index = 0;

            //    do
            //    {
            //        DrillstringTubulars[index].Length = DrillstringTubulars[index].OriginalLength - difference;

            //        if (DrillstringTubulars[index].Length < 0)
            //        {
            //            difference = DrillstringTubulars[index].Length * -1;
            //            DrillstringTubulars[index].Length = 0.00;
            //            DrillstringTubulars[index + 1].Length = DrillstringTubulars[index + 1].OriginalLength - difference;
            //        }
            //        else
            //        {
            //            difference = 0.00;
            //            if (index + 1 < DrillstringTubulars.Count)
            //                DrillstringTubulars[index + 1].Length = DrillstringTubulars[index + 1].OriginalLength;
            //        }

            //        index++;
            //    } while (index < DrillstringTubulars.Count);

            //    RaisePropertyChanged(nameof(TotalDisplacement));
            //    RaisePropertyChanged(nameof(TotalVolume));
            //    RaisePropertyChanged(nameof(TotalWeight));

            //    SaveTubulars();


            //}
        }

        /// <summary>
        /// Command method to modify the saved state of the toggle property
        /// </summary>
        private void BitOnBottomToggled()
        {

            //Save the state of the toggle switch
            Preferences.Set("BitOnBottomToggle", BitOnBottomToggle);

            //change relevant properties too
            //if (BitOnBottomToggle == true)
            //    TotalTubularLength = GetWellDepth();
            
        }

        private bool CanEdit()
        {
            return SelectedItem != null;
        }

        /// <summary>
        /// Navigate to the DrillstringDetailPage and pass in the
        /// currently selected DrillstringTubular for editting
        /// </summary>
        private async void Edit()
        {
            if (SelectedItem is IDrillstringTubular dst)
            {
                var navigationParams = new NavigationParameters();
                navigationParams.Add("drillstringTubular", dst);
                await _navigationService.NavigateAsync(nameof(DrillstringDetailPage), navigationParams).ConfigureAwait(false);
            }
        }

        private bool CanDelete()
        {
            return SelectedItem != null;
        }

        private void Delete()
        {
            if (SelectedItem is IDrillstringTubular dst)
            {
                DrillstringTubulars.Remove(dst);   //remove the item from the collection
                _dataService.DeleteItem(dst); //Delete the record from the database
            }

            //Recalculate to compensate for the deleted item
            TotalTubularLength = GetTotalTublarLength();
            TotalDisplacement = GetTotalDryDisplacement();
            TotalWeight = GetTotalWeight();
            TotalVolume = GetTotalInternalVolume();
           
            OnUpCommand.RaiseCanExecuteChanged();
            OnDownCommand.RaiseCanExecuteChanged();

            BitDepthChanged();
        }

        private void OnUp(object param)
        {
            IDrillstringTubular dst = param as IDrillstringTubular;

            //Move the item up one slot in the collection
            if (DrillstringTubulars.IndexOf(dst) > 0)
            {
                DrillstringTubulars.Move(DrillstringTubulars.IndexOf(dst), DrillstringTubulars.IndexOf(dst) - 1);
                ItemizeSortOrder();
            }
        }

        private bool CanMoveUp(object param)
        {
            if (param != null && param is IDrillstringTubular)
            {
                DrillstringTubularBase dst = param as DrillstringTubularBase;
                return dst.ItemSortOrder != 1;
            }
            return false;
        }

        private void OnDown(object param)
        {
            IDrillstringTubular dst = param as IDrillstringTubular;

            if (DrillstringTubulars.IndexOf(dst) < DrillstringTubulars.Count)
            {
                DrillstringTubulars.Move(DrillstringTubulars.IndexOf(dst), DrillstringTubulars.IndexOf(dst) + 1);
                ItemizeSortOrder();
            }
        }

        private bool CanMoveDown(object param)
        {
            if (param != null && param is IDrillstringTubular)
            {
                DrillstringTubularBase dst = param as DrillstringTubularBase;
                return dst.ItemSortOrder != DrillstringTubulars.Count;
            }

            return false;
        }
#endregion Command methods

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
            DrillstringTubulars = new ObservableCollection<ITubular>(_dataService.GetTubularItems<DrillstringTubularBase>());

            //check database for null values or other coruptness
            foreach (DrillstringTubularBase dst in DrillstringTubulars)
            {
                if (dst.LengthBlobbed == null ||
                    dst.IDBlobbed == null ||
                    dst.ODBlobbed == null ||
                    dst.WeightBlobbed == null ||
                    dst.ItemDescription == null)
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
                    bool repaired = _dataService.RepairTable<DrillstringTubularBase>();

                    if (!repaired)
                        _dataService.ClearTable<DrillstringTubularBase>();

                    //re-initialize properties after changes.
                    DrillstringTubulars = new ObservableCollection<ITubular>(_dataService.GetTubularItems<DrillstringTubularBase>());
                    TotalTubularLength = GetTotalTublarLength();
                    TotalVolume = GetTotalInternalVolume();
                    TotalDisplacement = GetTotalDryDisplacement();
                    TotalWeight = GetTotalWeight();
                }
            }

            //get state of bit on bottom toggle from preferences
            BitOnBottomToggle = Preferences.Get("BitOnBottomToggle", false);

            //Set Units of measure
            ShortLengthUnit = MeasurementUnitService.GetCurrentShortLengthUnit();
            LongLengthUnit = MeasurementUnitService.GetCurrentLongLengthUnit();
            VolumeUnit = MeasurementUnitService.GetCurrentVolumeUnit();
            CapacityUnit = MeasurementUnitService.GetCurrentCapacityUnit();
            MassUnit = MeasurementUnitService.GetCurrentMassUnit();

            //Initialize the totals on loading the page
            TotalTubularLength = GetTotalTublarLength();
            TotalVolume = GetTotalInternalVolume();
            TotalDisplacement = GetTotalDryDisplacement();
            TotalWeight = GetTotalWeight();

            IsRefreshing = false;
        }

        private double GetTotalTublarLength()
        {
            double totalLength = 0.00;

            if (DrillstringTubulars != null && LongLengthUnit != null)
            {
                //return the total length of all tubulars in the collection
                foreach (IDrillstringTubular dstTemp in DrillstringTubulars)
                {
                    if (dstTemp.Length != null)
                        totalLength += dstTemp.Length.Value;
                }
                
                return totalLength;
            }
            return totalLength;
        }

        private double GetTotalInternalVolume()
        {
            double totalVolume = 0.00;

            if (DrillstringTubulars != null && VolumeUnit != null)
            {
                //totalVolume = DrillstringTubulars[0].TotalInternalCapacity;
                foreach (IDrillstringTubular dst in DrillstringTubulars)
                    totalVolume += dst.TotalInternalCapacity;

                return totalVolume;
            }

            return totalVolume;
        }

        private double GetTotalDryDisplacement()
        {
            double totalDisplacement = 0.00;

            if (DrillstringTubulars != null)
            {
                foreach (IDrillstringTubular dst in DrillstringTubulars)
                    totalDisplacement += dst.TotalDryDisplacement;

                return totalDisplacement;
            }

            return totalDisplacement;
        }

        private double GetTotalWeight()
        {
            double totalWeight = 0.00;

            if (DrillstringTubulars != null)
            {
                foreach (IDrillstringTubular dst in DrillstringTubulars)
                    totalWeight += dst.TotalWeight;

                return totalWeight;
            }

            return totalWeight;
        }

        private void ItemizeSortOrder()
        {
            foreach (IDrillstringTubular tubular in DrillstringTubulars)
            {
                tubular.ItemSortOrder = DrillstringTubulars.IndexOf(tubular) + 1;
                _dataService.SaveItem(tubular);
            }

            OnUpCommand.RaiseCanExecuteChanged();
            OnDownCommand.RaiseCanExecuteChanged();
        }
    }
}