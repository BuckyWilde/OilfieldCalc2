using OilfieldCalc2.Helpers;
using OilfieldCalc2.Models;
using OilfieldCalc2.Models.DrillstringTubulars;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using OilfieldCalc2.Models.MeasureableUnit;
using OilfieldCalc2.Services;
using OilfieldCalc2.Models.Validators;
using FluentValidation.Results;

namespace OilfieldCalc2.ViewModels
{
    public class DrillstringDetailPageViewModel : ViewModelBase
    {
        private IDataService _dataService;
        private INavigationService _navigationService;

        private int _itemId;
        public int ItemId
        {
            get => _itemId;
            set => SetProperty(ref _itemId, value);
        }

        private List<string> _itemDescriptionTypes;
        public List<string> ItemDescriptionTypes
        {
            get => _itemDescriptionTypes;
            set => SetProperty(ref _itemDescriptionTypes, value);
        }

        private DrillstringTubularType _selectedTubularType;
        public DrillstringTubularType SelectedTubularType
        {
            get => _selectedTubularType;
            set => SetProperty(ref _selectedTubularType, value);
        }

        private Measurement _tubularLength;
        public Measurement TubularLength
        {
            get => _tubularLength;
            set => SetProperty(ref _tubularLength, value);
        }

        private Measurement _tubularOD;
        public Measurement TubularOD
        {
            get => _tubularOD;
            set => SetProperty(ref _tubularOD, value);
        }

        private Measurement _tubularID;
        public Measurement TubularID
        {
            get => _tubularID;
            set => SetProperty(ref _tubularID, value);
        }

        private Measurement _tubularAdjustedWeight;
        public Measurement TubularAdjustedWeight
        {
            get => _tubularAdjustedWeight;
            set => SetProperty(ref _tubularAdjustedWeight, value);
        }

        private DrillstringTubularValidator _validator;
        public DrillstringTubularValidator Validator
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

        public DelegateCommand OnSaveCommand { get; private set; }

        public DrillstringDetailPageViewModel(INavigationService navigationService, IDataService dataservice)
            : base(navigationService)
        {
            Title = "Drillstring Detail Page";

            _navigationService = navigationService;
            _dataService = dataservice;
            
            OnSaveCommand = new DelegateCommand(SaveTubular, CanSave);

            Validator = new DrillstringTubularValidator();

            //initialize properties
            ItemDescriptionTypes = new List<string>();
            TubularLength = new Measurement(0, MeasurementUnitService.GetCurrentLongLengthUnit());
            TubularOD = new Measurement(0, MeasurementUnitService.GetCurrentShortLengthUnit());
            TubularID = new Measurement(0, MeasurementUnitService.GetCurrentShortLengthUnit());
            TubularAdjustedWeight = new Measurement(0, MeasurementUnitService.GetCurrentMassUnit());
            SelectedTubularType = new DrillstringTubularType();
        }

        /// <summary>
        /// Saves the tubular into the database and exits to the previous list page
        /// Uses reflection to create a new instance of the correct tubular based
        /// on the value of "SelectedTubularType" which will default to 0 if none
        /// is selected
        /// </summary>
        private async void SaveTubular()
        {
            IDrillstringTubular pipe = null;
            string ns = typeof(DrillstringTubularBase).Namespace;
            string typeName = ns + "." + SelectedTubularType.ToString();
            if (typeName != null)
            {
                //Defaults to DrillPipe is nothing is selected in the Dropdown list for tubular type
                pipe = (IDrillstringTubular)Activator.CreateInstance(Type.GetType(typeName));
            }

            //Populate the properties of the newly created instance with data from the UI form.
            pipe.ItemId = ItemId;
            pipe.ItemSortOrder = ItemSortOrder;
            pipe.Length = new Measurement(TubularLength.Value, MeasurementUnitService.GetCurrentLongLengthUnit());
            pipe.OutsideDiameter = new Measurement(TubularOD.Value, MeasurementUnitService.GetCurrentShortLengthUnit());
            pipe.InsideDiameter = new Measurement(TubularID.Value, MeasurementUnitService.GetCurrentShortLengthUnit());
            pipe.AdjustedWeightPerUnit = new Measurement(TubularAdjustedWeight.Value, MeasurementUnitService.GetCurrentMassUnit());

            System.Diagnostics.Debug.WriteLine("Value of Selected type = " + SelectedTubularType.ToString());
            ValidationResults = Validator.Validate(pipe);

            if (pipe != null && ValidationResults.IsValid)
            {
                _dataService.SaveItem((DrillstringTubularBase)pipe);

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
            
            //Load item description picker box from Enum of Drillstring Tubular types.
            //Add the white spaces to be more human readable
            ItemDescriptionTypes = Enum.GetNames(typeof(DrillstringTubularType)).Select(b=>b.SplitCamelCase()).ToList();
            ItemSortOrder = 0;

            //Get navigation paramters, if any, and load the tubular to be editted
            if (parameters != null)
            {
                //cast the parameters to IDrillstringTubular
                if (parameters["drillstringTubular"] is IDrillstringTubular drillstringTubular)     //If the cast worked and the object is not null...
                {
                    //Load the values into the appropriate properties
                    
                    ItemId = drillstringTubular.ItemId;
                    ItemSortOrder = drillstringTubular.ItemSortOrder;
                    SelectedTubularType = drillstringTubular.TubularType;
                    TubularLength = drillstringTubular.Length;
                    TubularOD = drillstringTubular.OutsideDiameter;
                    TubularID = drillstringTubular.InsideDiameter;
                    TubularAdjustedWeight = drillstringTubular.AdjustedWeightPerUnit;
                }
            }
        }
    }
}
