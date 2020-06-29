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
        private IDataService _dataService;
        private INavigationService _navigationService;
        private IPageDialogService _pageDialogservice;

#region Properties
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

        //total volume capacity of all the tubulars
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
            get => 0.00;//GetWellDepth();
            set => SetProperty(ref _totalWellDepth, value);
        }

        //selected item length
        private double _currentItemLength;
        public double CurrentItemLength
        {
            get => _currentItemLength;
            set => SetProperty(ref _currentItemLength, value);
        }

        //selected item capacity per meter
        private double _currentItemCapacity;
        public double CurrentItemCapacity
        {
            get => _currentItemCapacity;
            set => SetProperty(ref _currentItemCapacity, value);
        }

        //selected item total internal capacity
        private double _currentItemVolume;
        public double CurrentItemVolume
        {
            get => _currentItemVolume;
            set => SetProperty(ref _currentItemVolume, value);
        }

        //selected item dry displacement per meter
        private double _currentItemDisplacement;
        public double CurrentItemDisplacement
        {
            get => _currentItemDisplacement;
            set => SetProperty(ref _currentItemDisplacement, value);
        }

        //selected item total dry displacement
        private double _currentItemTotalDisplacement;
        public double CurrentItemTotalDisplacement
        {
            get => _currentItemTotalDisplacement;
            set => SetProperty(ref _currentItemTotalDisplacement, value);
        }

        //Total weight of the tubulars
        private double _totalWeight;
        public double TotalWeight
        {
            get => GetTotalWeight();
            set => SetProperty(ref _totalWeight, value);
        }

        //State of the bitOnBottom toggle switch
        private bool _bitOnBottomToggle;

        public bool BitOnBottomToggle
        {
            get => _bitOnBottomToggle;
            set
            {
                SetProperty(ref _bitOnBottomToggle, value);
                //RaisePropertyChanged(nameof(TotalTubularLength));
                TotalTubularLength = GetTotalTublarLength();
            }
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

        public DelegateCommand<object> OnDeleteCommand { get; private set; }
        public DelegateCommand<object> OnEditCommand { get; private set; }
        public DelegateCommand<object> OnUpCommand { get; private set; }
        public DelegateCommand<object> OnDownCommand { get; private set; }
        public DelegateCommand<object> OnItemTappedCommand { get; private set; }
        public DelegateCommand OnBitOnBottomToggledCommand { get; private set; }
        public DelegateCommand OnBitDepthChangedCommand { get; private set; }
        
        public DrillstringListPageViewModel(INavigationService navigationService, IDataService dataService, IPageDialogService pageDialogService) : base(navigationService)
        {
            Title = "Drillstring List Page";

            _dataService = dataService;
            _navigationService = navigationService;
            _pageDialogservice = pageDialogService;

            //Initialize commands
            OnDeleteCommand = new DelegateCommand<object>(DeleteAsync);
            OnEditCommand = new DelegateCommand<object>(EditAsync);
            OnUpCommand = new DelegateCommand<object>(OnUp, CanMoveUp);
            OnDownCommand = new DelegateCommand<object>(OnDown, CanMoveDown);
            OnItemTappedCommand = new DelegateCommand<object>(ItemTappedCommand);
            OnBitOnBottomToggledCommand = new DelegateCommand(BitOnBottomToggled);
            OnBitDepthChangedCommand = new DelegateCommand(BitDepthChangedAsync);
        }

#region Command methods
        /// <summary>
        /// This command is only to update the PER ITEMS properties
        /// The item length property is for the UI to display the length, item capacity & item volume
        /// of the selected item on the screen, usually in the header or footer area
        /// </summary>
        /// <param name="param">DrillStringTubular item</param>
        private void ItemTappedCommand(object param)
        {
            if (param is IDrillstringTubular dst)
            {
                CurrentItemLength = dst.Length.Value;

                CurrentItemCapacity = dst.InternalCapacityPerUnit;
                CurrentItemVolume = dst.TotalInternalCapacity;
                CurrentItemDisplacement = dst.DryDisplacementPerUnit;
                CurrentItemTotalDisplacement = dst.TotalDryDisplacement;
            }
        }

        /// <summary>
        /// This command will active every time the user changes the bit depth
        /// entry. The total drillstring length will need to match this number
        /// but not exceed the total well depth. To make the drill string total
        /// length match, the top drill string item length will be modified
        /// accordingly, possibly the next tubulars in line as well depending
        /// on the user entered value.
        /// </summary>
        private async void BitDepthChangedAsync()
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

            //    await SaveTubularsAsync();

            //TODO: delete the Task.Completed Line
            await Task.CompletedTask.ConfigureAwait(false);
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

        private async void EditAsync(object param)
        {
            IDrillstringTubular dst = param as IDrillstringTubular;
            var navigationParams = new NavigationParameters();
            navigationParams.Add("drillstringTubular", dst);
            await _navigationService.NavigateAsync(nameof(DrillstringDetailPage), navigationParams).ConfigureAwait(false);
        }

        private async void DeleteAsync(object param)
        {
            if (param is IDrillstringTubular dst)
            {
                DrillstringTubulars.Remove(dst);   //remove the item from the collection
                await _dataService.DeleteItemAsync(dst).ConfigureAwait(false); //Delete the record from the database
            }

            //Recalculate to compensate for the deleted item
            TotalTubularLength = GetTotalTublarLength();
            TotalDisplacement = GetTotalDryDisplacement();
            TotalWeight = GetTotalWeight();
            TotalVolume = GetTotalInternalVolume();

            //Clear the selected item vlues
            CurrentItemLength = 0;
            CurrentItemCapacity = 0;
            CurrentItemVolume = 0;
            CurrentItemDisplacement = 0;
            CurrentItemTotalDisplacement = 0;

            OnUpCommand.RaiseCanExecuteChanged();
            OnDownCommand.RaiseCanExecuteChanged();

            BitDepthChangedAsync();
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

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedToAsync(parameters);
            bool answer = false;
            bool corrupted = false;

            //Load the drillstring Tubulars from the dataservice.
            DrillstringTubulars = new ObservableCollection<ITubular>(
                await _dataService.GetTubularItemsAsync<DrillstringTubularBase>().ConfigureAwait(false));

            //check database for null values
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    answer = await _pageDialogservice.DisplayAlertAsync("Warning", "Database appears to be corrupt. Attempting to repair", "Ok", "Cancel").ConfigureAwait(false);
                    if (!answer)
                        Device.BeginInvokeOnMainThread(async () => await _navigationService.NavigateAsync(nameof(MainPage)).ConfigureAwait(false));
                    if (answer)
                    {
                        //await _dataService.ClearTable<DrillstringTubularBase>().ConfigureAwait(false);
                        bool repaired = await _dataService.RepairTable<DrillstringTubularBase>().ConfigureAwait(false);
                        
                        if (!repaired)
                            await _dataService.ClearTable<DrillstringTubularBase>().ConfigureAwait(false);

                        //re-initialize properties after changes.
                        DrillstringTubulars = new ObservableCollection<ITubular>(await _dataService.GetTubularItemsAsync<DrillstringTubularBase>().ConfigureAwait(false));
                        TotalTubularLength = GetTotalTublarLength();
                        TotalVolume = GetTotalInternalVolume();
                        TotalDisplacement = GetTotalDryDisplacement();
                        TotalWeight = GetTotalWeight();
                    }
                });
            }

            //Initialize single item properties to 0 because nothing will be selected when first loaded
            CurrentItemLength = 0.00;
            CurrentItemCapacity = 0.00;
            CurrentItemVolume = 0.00;
            CurrentItemDisplacement = 0.00;
            CurrentItemTotalDisplacement = 0.00;

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

        private async void ItemizeSortOrder()
        {
            foreach (IDrillstringTubular tubular in DrillstringTubulars)
            {
                tubular.ItemSortOrder = DrillstringTubulars.IndexOf(tubular) + 1;
                await _dataService.SaveItemAsync(tubular).ConfigureAwait(false);
            }

            OnUpCommand.RaiseCanExecuteChanged();
            OnDownCommand.RaiseCanExecuteChanged();
        }
    }
}