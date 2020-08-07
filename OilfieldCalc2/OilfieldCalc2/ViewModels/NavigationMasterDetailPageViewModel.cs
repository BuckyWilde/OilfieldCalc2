using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OilfieldCalc2.Models;
using Xamarin.Forms;
using OilfieldCalc2.Views;

namespace OilfieldCalc2.ViewModels
{
    public class NavigationMasterDetailPageViewModel : ViewModelBase
    {
        readonly INavigationService _navigationService;

        public ObservableCollection<MyMenuItem> MenuItems { get; set; }

        public bool IsEnabled { get; set; }

        private MyMenuItem selectedMenuItem;
        public MyMenuItem SelectedMenuItem
        {
            get => selectedMenuItem;
            set => SetProperty(ref selectedMenuItem, value);
        }

        public DelegateCommand<string> OnNavigateCommand { get; private set; }

        public NavigationMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;

            MenuItems = new ObservableCollection<MyMenuItem>();

            OnNavigateCommand = new DelegateCommand<string>(NavigateAsync, CanNavigate);

            LoadMenuItems();
        }

        public async void NavigateAsync(string page)
        {
            await _navigationService.NavigateAsync(nameof(NavigationPage) + "/" + selectedMenuItem.PageName).ConfigureAwait(false);
        }

        private bool CanNavigate(string item)
        {            
            return selectedMenuItem.IsEnabled;
        }

        public override void OnNavigatedToAsync(INavigationParameters parameters)
        {
            
        }

        private void LoadMenuItems()
        {
            MenuItems.Add(new MyMenuItem()
            {
                Icon = (string)Application.Current.Resources["HomeIcon"],
                PageName = nameof(MainPage),
                Title = "Main",
                IsEnabled = true
            }) ;

            MenuItems.Add(new MyMenuItem()
            {
                Icon = (string)Application.Current.Resources["MoreIcon"],
                PageName = nameof(WellboreListPage),
                Title = "Well Bore",
                IsEnabled = true
            });

            MenuItems.Add(new MyMenuItem()
            {
                Icon = (string)Application.Current.Resources["MoreIcon"],
                PageName = nameof(DrillstringMobileListPage),
                Title = "Drill String",
                IsEnabled = true
            });

            MenuItems.Add(new MyMenuItem()
            {
                Icon = (string)Application.Current.Resources["CalculatorIcon"],
                PageName = nameof(MainPage),
                Title = "Volumes",
                IsEnabled = false
            });

            MenuItems.Add(new MyMenuItem()
            {
                Icon = (string)Application.Current.Resources["SettingsIcon"],
                PageName = nameof(AppSettingsPage),
                Title = "Settings",
                IsEnabled = true
            });
        }
    }
}
