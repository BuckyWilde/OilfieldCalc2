using Prism;
using Prism.Ioc;
using OilfieldCalc2.ViewModels;
using OilfieldCalc2.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using OilfieldCalc2.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace OilfieldCalc2
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationMasterDetailPage/NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<NavigationMasterDetailPage, NavigationMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<DrillstringListPage, DrillstringListPageViewModel>();
            containerRegistry.RegisterForNavigation<DrillstringDetailPage, DrillstringDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<WellboreDetailPage, WellboreDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<WellboreListPage, WellboreListPageViewModel>();

            containerRegistry.RegisterSingleton<IDataService, DataService>();

            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
        }        
    }
}
