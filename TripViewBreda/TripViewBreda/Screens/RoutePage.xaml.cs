using TripViewBreda.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using TripViewBreda.Model.Information;
using TripViewBreda.GeoLocation;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Devices.Geolocation;
using TripViewBreda.Model.Routes;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace TripViewBreda
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RoutePage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private bool LocationCalculated = false;
        public RoutePage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
            CalculateCurrentGPSLocation();
        }
        private async Task CalculateCurrentGPSLocation()
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey(AppSettings.LastKnownLocation) == true)
            { LocationCalculated = true; }
            var locator = new Geolocator();
            locator.DesiredAccuracyInMeters = 50;
            Debug.WriteLine("Calculating CurrentLocation");
            var position = await locator.GetGeopositionAsync();
            double[] lastKnownPosition = new double[] { position.Coordinate.Latitude, position.Coordinate.Longitude };
            ApplicationData.Current.LocalSettings.Values[AppSettings.LastKnownLocation] = lastKnownPosition;
            Debug.WriteLine("Calculation Done. Location Calculated");
            LocationCalculated = true;
        }

        #region NavigationHelper registration
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            this.TextBox_Project.Text = AppSettings.APP_NAME;
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        { }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RegistrationRouteButtons();
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        #region Registration Route Buttons
        private void RegistrationRouteButtons()
        {
            AddButton("Historische Kilometer", HistorischeKM);
            AddButton("Kroegentocht", Cafes);
            AddButton("School", School);
            AddButton("Tourist Trail", Tourist_Trail);
            AddButton("Remaining", Remaining);
        }

        private void AddButton(string text, Action<object, RoutedEventArgs> Method)
        {
            Button button = new Button();
            button.FontSize = 20;
            button.Content = text;
            button.Click += new RoutedEventHandler(Method);
            this.Route_Buttons_panel.Children.Add(button);
        }
        #region Functions
        private void HistorischeKM(object sender, RoutedEventArgs e)
        { NavigateToMap(new Route.HistorischeKM()); }
        private void School(object sender, RoutedEventArgs e)
        { NavigateToMap(new Route.School()); }
        private void Tourist_Trail(object sender, RoutedEventArgs e)
        { NavigateToMap(new Route.Tourist_Trail()); }
        public void Cafes(object sender, RoutedEventArgs e)
        { NavigateToMap(new Route.Cafes()); }
        public void Remaining(object sender, RoutedEventArgs e)
        { NavigateToMap(new Route.Remaining()); }
        private void NavigateToMap(IRoute route)
        { NavigateToMap(route.GetSubjects()); }
        private void NavigateToMap(Subjects subs)
        {
            if (this.LocationCalculated)
            {
                Debug.WriteLine("Navigate To Map With '" + subs.ToString());
                this.Frame.Navigate(typeof(MapPage), subs);
            }
        }
        #endregion
        #endregion

        #region Other Buttons
        private void Map_bn_Click(object sender, RoutedEventArgs e)
        {
            Subjects subjects = new Subjects();
            this.Frame.Navigate(typeof(MapPage), subjects);
        }
        #endregion
    }
}
