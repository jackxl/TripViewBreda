﻿using TripViewBreda.Common;
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
using Windows.UI.Popups;
using TripViewBreda.Model.Routes;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace TripViewBreda.Screens
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RoutePage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private bool LocationCalculated = false;

        Model.FileIO.JsonIO model;
        public RoutePage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
            Loading_Feedback_tx.Text = "";
            model = new Model.FileIO.JsonIO();
            CalculateCurrentGPSLocation();
        }
        private async Task CalculateCurrentGPSLocation()
        {
            try
            {
                if (ApplicationData.Current.LocalSettings.Values.ContainsKey(AppSettings.LastKnownLocation) == true)
                {
                    LocationCalculated = true;
                }
                var locator = new Geolocator();
                locator.DesiredAccuracyInMeters = 50;
                Debug.WriteLine("Calculating CurrentLocation");
                var position = await locator.GetGeopositionAsync();
                double[] lastKnownPosition = new double[] {position.Coordinate.Latitude, position.Coordinate.Longitude};
                ApplicationData.Current.LocalSettings.Values[AppSettings.LastKnownLocation] = lastKnownPosition;
                Debug.WriteLine("Calculation Done. Location Calculated");
                LocationCalculated = true;
            }
            catch (Exception e)
            {
                ShowError(); 
            }
            
        }

        private async void ShowError()
        {
            var dialog = new MessageDialog("Localization is not possible! Make sure that GPS is set to ON!", "Error!");
            await dialog.ShowAsync();
            this.Frame.GoBack();
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
        }

        private void AddButton(string text, Action<object, RoutedEventArgs> Method)
        {
            Button button = new Button();
            button.FontSize = 20;
            button.Content = text;
            button.HorizontalContentAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
            button.BorderThickness = new Thickness(0);
            button.Click += new RoutedEventHandler(Method);
            this.Route_Buttons_panel.Children.Add(button);
        }
        #region Functions
        private async void HistorischeKM(object sender, RoutedEventArgs e)
        {
            Subjects route = await Find("Historische Km"); // hier moet de routenaam nog toegevegd worden
            NavigateToMap(route);
        }
        private async void School(object sender, RoutedEventArgs e)
        {
            Subjects route = await Find("School"); // hier moet de routenaam nog toegevegd worden
            NavigateToMap(route);
        }
        public async void Cafes(object sender, RoutedEventArgs e)
        {
            Subjects route = await Find("Cafes"); // hier moet de routenaam nog toegevegd worden
            NavigateToMap(route);
        }
        private async Task<Subjects> Find(string name)
        {
            Loading_Feedback_tx.Text = "";
            try
            {
                Loading_Feedback_tx.Text = name;
                Subjects subjects = await model.Find(name);
                return subjects;
            }
            catch (ArgumentNullException) { return null; }
        }
        private async Task<Subjects> GetAllEvents()
        {
            Subjects allEvents = new Subjects();
            foreach (Subjects subjects in model.GetEvents())
            {
                foreach (Subject subject in subjects.GetSubjects())
                {
                    allEvents.AddSubject(subject);
                }
            }
            return allEvents;
        }
        private void NavigateToMap(IRoute route)
        {
            NavigateToMap(route.GetSubjects());
        }
        private async void NavigateToMap(Subjects subs)
        {
            Subjects events = await GetAllEvents();
            if (this.LocationCalculated)
            {
                if (subs != null)
                {
                    Debug.WriteLine("Navigate To Map With '" + subs.ToString());
                }
                else
                {
                    Loading_Feedback_tx.Text = "Could not load Subjects";
                    return;
                }
                if (events != null)
                {
                    Debug.WriteLine("Navigate To Map With '" + events.ToString());
                }
                else
                {
                    Loading_Feedback_tx.Text = "Could not load Events";
                    return;
                }
            }
            Debug.WriteLine("Navigate to Map Page with: " + subs.GetSubjects().Count + " subjects and " + events.GetSubjects().Count + " events.");
            Tuple<Subjects, Subjects> information = new Tuple<Subjects, Subjects>(subs, events);
            this.Frame.Navigate(typeof(MapPage), information);
        }
        #endregion
        #endregion

    }
}
