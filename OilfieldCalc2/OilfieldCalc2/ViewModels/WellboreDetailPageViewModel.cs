using Prism.Commands;
using Prism.Mvvm;
using System;
using OilfieldCalc2.Helpers;
using OilfieldCalc2.Models;
using OilfieldCalc2.Models.DrillstringTubulars;
using Prism.Navigation;
using Prism.Xaml;
using System.Collections.Generic;
using System.Linq;
using OilfieldCalc2.Models.MeasureableUnit;
using OilfieldCalc2.Services;
using OilfieldCalc2.Models.Validators;
using FluentValidation.Results;
using OilfieldCalc2.Models.WellboreTubulars;
using FluentValidation;

namespace OilfieldCalc2.ViewModels
{
    public class WellboreDetailPageViewModel : ViewModelBase
    {
        readonly IDataService _dataService;
        readonly INavigationService _navigationService;

        private int _itemId;
        public int ItemId
        {
            get => _itemId;
            set => SetProperty(ref _itemId, value);
        }

        private int _washoutFactor;
        public int WashoutFactor
        {
            get => _washoutFactor;
            set => SetProperty(ref _washoutFactor, value);
        }

        private List<string> _itemDescriptionTypes;
        public List<string> ItemDescriptionTypes
        {
            get => _itemDescriptionTypes;
            set => SetProperty(ref _itemDescriptionTypes, value);
        }

        private WellboreTubularType _selectedTubularType;
        public WellboreTubularType SelectedTubularType
        {
            get => _selectedTubularType;
            set => SetProperty(ref _selectedTubularType, value);
        }

        private Measurement _startDepth;
        public Measurement StartDepth
        {
            get => _startDepth;
            set => SetProperty(ref _startDepth, value);
        }

        private Measurement _endDepth;
        public Measurement EndDepth
        {
            get => _endDepth;
            set => SetProperty(ref _endDepth, value);
        }

        private Measurement _tubularID;
        public Measurement TubularID
        {
            get => _tubularID;
            set => SetProperty(ref _tubularID, value);
        }

        private bool _washoutSliderIsVisibile;
        public bool WashoutSliderIsVisible
        {
            get => _washoutSliderIsVisibile;
            set => SetProperty(ref _washoutSliderIsVisibile, value);
        }

        private WellboreTubularValidator _validator;
        public WellboreTubularValidator Validator
        {
            get => _validator;
            set => SetProperty(ref _validator, value);
        }

        private ValidationResult _validationResults;
        public ValidationResult ValidationResults
        {
            get => _validationResults;
            set => SetProperty(ref _validationResults, value);
        }

        public int ItemSortOrder { get; set; }

        public DelegateCommand OnSaveCommand { get; }
        public DelegateCommand OnTypeSelectionChangedCommand { get; }

        public WellboreDetailPageViewModel(IDataService dataService, INavigationService navigationService)
            : base(navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;

            Title = "Wellbore Detail Page";

            OnSaveCommand = new DelegateCommand(SaveTubular, CanSave);
            OnTypeSelectionChangedCommand = new DelegateCommand(TypeSelectionChanged);

            Validator = new WellboreTubularValidator();

            //Initialize values
            ItemDescriptionTypes = new List<string>();
            WashoutFactor = 0;
            StartDepth = new Measurement(0, MeasurementUnitService.GetCurrentLongLengthUnit());
            EndDepth = new Measurement(0, MeasurementUnitService.GetCurrentShortLengthUnit());
            TubularID = new Measurement(0, MeasurementUnitService.GetCurrentShortLengthUnit());
            SelectedTubularType = new WellboreTubularType();
            WashoutSliderIsVisible = false;
        }

        private void TypeSelectionChanged()
        {
            if (SelectedTubularType == WellboreTubularType.OpenHole)
            {
                WashoutSliderIsVisible = true;
            }
            else
            {
                WashoutSliderIsVisible = false;
                WashoutFactor = 0;
            }
        }

        /// <summary>
        /// Saves the tubular into the database and exits to the previous list page
        /// Uses reflection to create a new instance of the correct tubular based
        /// on the value of "SelectedTubularType" which will default to 0 if none
        /// is selected
        /// </summary>
        private async void SaveTubular()
        {
            IWellboreTubular wbt = null;
            string ns = typeof(WellboreTubularBase).Namespace;
            string typeName = ns + "." + SelectedTubularType.ToString();
            if (typeName != null)
            {
                //Defaults to DrillPipe is nothing is selected in the Dropdown list for tubular type
                wbt = (IWellboreTubular)Activator.CreateInstance(Type.GetType(typeName));
            }

            //Populate the properties of the newly created instance with data from the UI form.
            wbt.ItemId = ItemId;
            wbt.WashoutFactor = WashoutFactor;
            wbt.ItemSortOrder = ItemSortOrder;
            wbt.StartDepth = new Measurement(StartDepth.Value, MeasurementUnitService.GetCurrentLongLengthUnit());
            wbt.EndDepth = new Measurement(EndDepth.Value, MeasurementUnitService.GetCurrentShortLengthUnit());
            wbt.InsideDiameter = new Measurement(TubularID.Value, MeasurementUnitService.GetCurrentShortLengthUnit());

            ValidationResults = Validator.Validate(wbt);

            if (wbt != null && ValidationResults.IsValid)
            {
                _dataService.SaveItem((WellboreTubularBase)wbt);

                await _navigationService.GoBackAsync().ConfigureAwait(false);
            }
        }

        private bool CanSave()
        {
            return true;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            
            //Load item description picker box from Enum of Wellbore Tubular types.
            //Add the white spaces to be more human readable
            ItemDescriptionTypes = Enum.GetNames(typeof(WellboreTubularType)).Select(b=>b.SplitCamelCase()).ToList();
            ItemSortOrder = 0;

            //Get navigation paramters, if any, and load the tubular to be editted
            if (parameters != null)
            {
                //cast the parameters to IDrillstringTubular
                if (parameters["wellboreTubular"] is IWellboreTubular wellboreTubular)     //If the cast worked and the object is not null...
                {
                    //Load the values into the appropriate properties
                    
                    ItemId = wellboreTubular.ItemId;
                    WashoutFactor = wellboreTubular.WashoutFactor;
                    ItemSortOrder = wellboreTubular.ItemSortOrder;
                    SelectedTubularType = wellboreTubular.TubularType;
                    StartDepth = wellboreTubular.StartDepth;
                    EndDepth = wellboreTubular.EndDepth;
                    TubularID = wellboreTubular.InsideDiameter;
                }
            }
        }
    }
}