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
using TripViewBreda.GeoLocation;
using Windows.Devices.Geolocation;
using TripViewBreda.Navigation;
using Windows.UI.Xaml.Controls.Maps;
using TripViewBreda.Model.Information;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Xaml.Documents;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace TripViewBreda
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private GPS gps= new GPS();
        private Subjects subjects = new Subjects();
        public MapPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
            subjects.AddSubject(new Subject(new GPSPoint(51.592342, 4.548881), "Thuis"));
            subjects.AddSubject(new Subject(new GPSPoint(51.585477, 4.793091), "School"));
            foreach(Subject s in subjects.GetSubjects())
            {
                AddPoint_Map(s.GetLocation().GetLattitude(), s.GetLocation().GetLongitude(), s.GetName());
                
            }
            GetRouteAndDirections(subjects.GetSubjects().First<Subject>(), subjects.GetSubjects().Last<Subject>());
        }

        private void AddPoint_Map(double lattitude, double longitude, String name)
        {
            MapIcon addIcon = new MapIcon();
            
            var myPosition = new Windows.Devices.Geolocation.BasicGeoposition();
            myPosition.Longitude = longitude;
            myPosition.Latitude = lattitude;
            addIcon.Location = new Geopoint(myPosition);
            addIcon.Title = name;
            addIcon.NormalizedAnchorPoint = new Point(0.5, 1.0);
            MyMap.MapElements.Add(addIcon);
        }

        public async void GoToCurrentPosition()
        {
            var locator = new Geolocator();
            locator.DesiredAccuracyInMeters = 50;

            var position = await locator.GetGeopositionAsync();
            Geopoint myPoint = position.Coordinate.Point;
            await MyMap.TrySetViewAsync(myPoint, 16D);

        }

        private async void GetRouteAndDirections(Subject start, Subject end)
        {
            // Start at start subject
            BasicGeoposition startLocation = new BasicGeoposition();
            startLocation.Latitude = start.GetLocation().GetLattitude();
            startLocation.Longitude = start.GetLocation().GetLongitude();
            Geopoint startPoint = new Geopoint(startLocation);

            // End at end subject
            BasicGeoposition endLocation = new BasicGeoposition();
            endLocation.Latitude = end.GetLocation().GetLattitude();
            endLocation.Longitude = end.GetLocation().GetLongitude();
            Geopoint endPoint = new Geopoint(endLocation);

            // Get the route between the points.
            MapRouteFinderResult routeResult =
                await MapRouteFinder.GetDrivingRouteAsync(
                startPoint,
                endPoint,
                MapRouteOptimization.Time,
                MapRouteRestrictions.None);

            //Display route with text
            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                // Display summary info about the route.
                InstructionsLabel.Inlines.Add(new Run()
                {
                    Text = "Total estimated time (minutes) = "
                        + routeResult.Route.EstimatedDuration.TotalMinutes.ToString()
                });
                InstructionsLabel.Inlines.Add(new LineBreak());
                InstructionsLabel.Inlines.Add(new Run()
                {
                    Text = "Total length (kilometers) = "
                        + (routeResult.Route.LengthInMeters / 1000).ToString()
                });
                InstructionsLabel.Inlines.Add(new LineBreak());
                InstructionsLabel.Inlines.Add(new LineBreak());

                // Display the directions.
                InstructionsLabel.Inlines.Add(new Run()
                {
                    Text = "DIRECTIONS"
                });
                InstructionsLabel.Inlines.Add(new LineBreak());

                foreach (MapRouteLeg leg in routeResult.Route.Legs)
                {
                    foreach (MapRouteManeuver maneuver in leg.Maneuvers)
                    {
                        InstructionsLabel.Inlines.Add(new Run()
                        {
                            Text = maneuver.InstructionText
                        });
                        InstructionsLabel.Inlines.Add(new LineBreak());
                    }
                }
            }
            else
            {
                InstructionsLabel.Text =
                    "A problem occurred: " + routeResult.Status.ToString();
            }

           // Displaying route on map
            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                // Use the route to initialize a MapRouteView.
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.Yellow;
                viewOfRoute.OutlineColor = Colors.Black;

                // Add the new MapRouteView to the Routes collection
                // of the MapControl.
                MyMap.Routes.Add(viewOfRoute);

                // Fit the MapControl to the route.
                await MyMap.TrySetViewBoundsAsync(
                    routeResult.Route.BoundingBox,
                    null,
                    Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
            }
            else
            {
                InstructionsLabel.Text =
                   "A problem occurred: " + routeResult.Status.ToString();
                
            }

        }
        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        /// 
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
            NavigateToVVV();
        }
        private async void NavigateToVVV()
        {
            var locator = new Geolocator();
            locator.DesiredAccuracyInMeters = 50;

            var position = await locator.GetGeopositionAsync();
            Geopoint myPoint = position.Coordinate.Point;
            await MyMap.TrySetViewAsync(myPoint);
            MyMap.ZoomLevel = 16;
            MyMap.LandmarksVisible = true;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}
