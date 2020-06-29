using OilfieldCalc2.Models;
using OilfieldCalc2.Models.WellboreTubulars;
using OilfieldCalc2.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OilfieldCalc2.ViewModels
{
    public class WellboreListPageViewModel : ViewModelBase
    {
        IDataService _dataService;
        INavigationService _navigationService;

        #region Properties
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
        #endregion

        public WellboreListPageViewModel(IDataService dataService, INavigationService navigationService)
            : base(navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;

            Title = "Wellbore List Page";
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedToAsync(parameters);

            WellboreTubulars = new ObservableCollection<ITubular>(await _dataService.GetTubularItemsAsync<WellboreTubularBase>());
        }
    }
}
