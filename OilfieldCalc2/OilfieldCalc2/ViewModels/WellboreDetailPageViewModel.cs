using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using OilfieldCalc2.Services;
using Prism.Navigation;

namespace OilfieldCalc2.ViewModels
{
    public class WellboreDetailPageViewModel : ViewModelBase
    {
        IDataService _dataService;
        INavigationService _NavigationService;

        public WellboreDetailPageViewModel(IDataService dataService, INavigationService navigationService)
            : base(navigationService)
        {
            _dataService = dataService;
            _NavigationService = navigationService;

            Title = "Wellbore Detail Page";
        }
    }
}
