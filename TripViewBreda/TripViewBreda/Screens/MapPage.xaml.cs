using Windows.Devices.Geolocation.Geofencing;
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
using TripViewBreda.Model.Information;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Shapes;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls.Maps;
using TripViewBreda.Screens;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace TripViewBreda
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapPage : Page
    {
        private NavigationHelper navigationHelper;
        private Geopoint myPoint;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private Subjects subjects;
        public MapPage()
        {
            this.InitializeComponent();

            GeofenceMonitor.Current.Geofences.Clear();
            GeofenceMonitor.Current.GeofenceStateChanged += Current_GeofenceStateChanged;

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;


            
  
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



        public async Task GoToCurrentPosition()
        {
            var locator = new Geolocator();
            locator.DesiredAccuracyInMeters = 50;

            var position = await locator.GetGeopositionAsync();
            myPoint = position.Coordinate.Point;
            await MyMap.TrySetViewAsync(myPoint, 16D);
            MyMap.ZoomLevel = 16;
            MyMap.LandmarksVisible = true;

            Ellipse myCircle = new Ellipse();
            myCircle.Fill = new SolidColorBrush(Colors.Blue);
            myCircle.Height = 20;
            myCircle.Width = 20;
            myCircle.Opacity = 50;

            MyMap.WatermarkMode = new MapWatermarkMode();


        }

        private void CreateGeofence(Subject subject)
        {
                Geofence geofence = null;

                BasicGeoposition position;
                position.Latitude = subject.GetLocation().GetLattitude();
                position.Longitude = subject.GetLocation().GetLongitude();
                position.Altitude = 0.0;

                double radius = 30;

                Geocircle geocircle = new Geocircle(position,radius);

                MonitoredGeofenceStates mask = 0;

                mask |= MonitoredGeofenceStates.Entered;
                mask |= MonitoredGeofenceStates.Exited;

                geofence = new Geofence(subjects.GetSubjects().IndexOf(subject).ToString(), geocircle, mask,true);
                GeofenceMonitor.Current.Geofences.Add(geofence);
            
        }

        private async void Current_GeofenceStateChanged(GeofenceMonitor sender, object args)
        {
            var reports = sender.ReadReports();

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                foreach (var report in reports)
                {
                    var state = report.NewState;
                    var geofence = report.Geofence;
                    Subject subject = null;


                    if (state == GeofenceState.Entered)
                    {
                        int id = Convert.ToInt32(geofence.Id);

                        for (int i = 0; i < subjects.GetSubjects().Count; i++)
                        {
                            if (id == i)
                            {
                                subject = subjects.GetSubjects().ElementAt(id);
                            }
                        }

                        var dialog = new MessageDialog(subject.GetName(), "You have reach the following hotspot!");
                        await dialog.ShowAsync();

                        // User has entered the area.
                        this.Frame.Navigate(typeof(DetailPage), subject);

                    }
                }
            });
        }
        
        private async Task GetRouteAndDirections(Subject start, Subject end)
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
                await MapRouteFinder.GetWalkingRouteAsync(
                startPoint,
                endPoint);

            //Display route with text
            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                InstructionsLabel.Text += "\n";
                // Display the directions.
                InstructionsLabel.Inlines.Add(new Run()
                {
                    Text = "Route van " + start.GetName() + " naar " + end.GetName()
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
                viewOfRoute.RouteColor = Colors.LightBlue;
                viewOfRoute.OutlineColor = Colors.DarkBlue;

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
        }
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
            subjects = e.Parameter as Subjects;
            
            foreach (Subject s in subjects.GetSubjects())
            {
                AddPoint_Map(s.GetLocation().GetLattitude(), s.GetLocation().GetLongitude(), s.GetName());
                CreateGeofence(s);
            }
            await GoToCurrentPosition();
            Subject lastSub = new Subject(new GPSPoint(myPoint.Position.Latitude, myPoint.Position.Longitude), "Huidige locatie");
            DestinationLabel.Text = "";
            foreach (Subject s in subjects.GetSubjects())
            {
                DestinationLabel.Text += s.GetName() + "\n";
                if (lastSub != null)
                {
                   await GetRouteAndDirections(lastSub, s);
                   MyMap.CancelDirectManipulations();

                }
                lastSub = s;
            }
            MyMap.CancelDirectManipulations();
            await GoToCurrentPosition();
            
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

    }
}
