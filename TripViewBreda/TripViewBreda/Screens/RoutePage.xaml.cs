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
            AddButton("Home", Home);
            AddButton("Cafes", Cafes);
            AddButton("School", School);
            AddButton("Tourist Trail", Tourist_Trail);
            AddButton("Pubs Trip", Pubs_Trip);
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
        private void Tourist_Trail(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Tourist Trail");
            Subjects subjects = new Subjects();

            NavigateToMap(sender, subjects);
        }
        private void Pubs_Trip(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Pubs Trip");
            Subjects subjects = new Subjects();

            NavigateToMap(sender, subjects);
        }
        private void School(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("School");
            Subjects subjects = new Subjects();
            subjects.AddSubject(new Subject(new GPSPoint(51.585477, 4.793091), "School", "Avans Hogeschool info enzo...GG"));

            NavigateToMap(sender, subjects);
        }

        private void Cafes(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Cafes");
            Subjects subjects = new Subjects();
            subjects.AddSubject(new Subject(new GPSPoint(51.587768, 4.776655), "'t Hart"));
            subjects.AddSubject(new Subject(new GPSPoint(51.587631, 4.776749), "Cafe SamSam"));
            subjects.AddSubject(new Subject(new GPSPoint(51.589645, 4.773857), "Studio Dependance"));
            subjects.AddSubject(new Subject(new GPSPoint(51.588621, 4.77313), "Millertime"));

            NavigateToMap(sender, subjects);
        }
        private void Home(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Home");
            Subjects subjects = new Subjects();
            subjects.AddSubject(new Subject(new GPSPoint(51.592342, 4.548881), "Thuis"));

            NavigateToMap(sender, subjects);
        }
        private void Remaining(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Remaining");
            Subjects subjects = new Subjects();

            NavigateToMap(sender, subjects);
        }
        private void NavigateToMap(object sender, Subjects subs)
        {
            if (this.LocationCalculated)
            {
                Debug.WriteLine("Navigate To Map With '" + sender.ToString() + "'.");
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
