using OilfieldCalc2.Models.DrillstringTubulars;
using OilfieldCalc2.Models.WellboreTubulars;
using OilfieldCalc2.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OilfieldCalc2.ViewModels
{
    public class SettingsAdvancedPageViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly IPageDialogService _pageDialogservice;


        public DelegateCommand OnClearDatabaseCommand { get; private set; }

        public SettingsAdvancedPageViewModel(INavigationService navigationService, IDataService dataService, IPageDialogService pageDialogService) : base(navigationService)
        {
            Title = "Advanced";

            _pageDialogservice = pageDialogService;
            _dataService = dataService;

            OnClearDatabaseCommand = new DelegateCommand(ClearDatabase);
        }

        private void ClearDatabase()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (await _pageDialogservice.DisplayAlertAsync("Warning",
                    "You are about to detroy all wellbore AND drillstring data! Press OK to procede",
                    "Ok", "Cancel").ConfigureAwait(false))
                {
                    await _dataService.ClearTable<DrillstringTubularBase>().ConfigureAwait(false);
                    await _dataService.ClearTable<WellboreTubularBase>().ConfigureAwait(false);
                }
            });
        }
    }
}
