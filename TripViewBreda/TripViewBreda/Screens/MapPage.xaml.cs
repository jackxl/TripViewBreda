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
using System.Diagnostics;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;


// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace TripViewBreda.Screens
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
        private Geolocator locator;


        private Subject requestedSubject;

        private const uint DesiredAccuracyInMeters = 10;
        private const double zoomLevel = 16D;
        private Image currentPositionIcon;

        public MapPage()
        {
            this.InitializeComponent();
            currentPositionIcon = new Image();
            currentPositionIcon.Source = new BitmapImage(new Uri("ms-appx:///Assets/CurrentLocation.png"));
            currentPositionIcon.Height = 20;
            currentPositionIcon.Width = 20;

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
            addIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Location.png"));
            MyMap.MapElements.Add(addIcon);
        }

        private void AddPoint_CurrentLocation(double lattitude, double longitude, string name)
        {
            MapIcon addIcon = new MapIcon();

            var myPosition = new Windows.Devices.Geolocation.BasicGeoposition();
            myPosition.Longitude = longitude;
            myPosition.Latitude = lattitude;
            addIcon.Location = new Geopoint(myPosition);
            addIcon.NormalizedAnchorPoint = new Point(0.5, 1.0);
            addIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/CurrentLocation.png"));
            MyMap.MapElements.Add(addIcon);
        }

        private async Task startup()
        {
            locator = new Geolocator();
            locator.DesiredAccuracy = PositionAccuracy.High;
            locator.MovementThreshold = 3; // The units are meters.
            locator.PositionChanged += geolocator_PositionChanged;
            Geoposition position = await locator.GetGeopositionAsync();
            myPoint = position.Coordinate.Point;
            MyMap.PedestrianFeaturesVisible = true;
            MyMap.LandmarksVisible = true;
        }

        private void geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
          myPoint = args.Position.Coordinate.Point;
            this.MyMap.Dispatcher.RunAsync(new CoreDispatcherPriority(),new DispatchedHandler(
                
            new Action(delegate()
              {
                  if (!MyMap.Children.Contains(currentPositionIcon))
                  {
                      MyMap.Children.Add(currentPositionIcon);
                  }
                  MapControl.SetLocation(currentPositionIcon, myPoint);
                  MapControl.SetNormalizedAnchorPoint(currentPositionIcon, new Point(0.5, 0.5));
                  Debug.WriteLine("Current position added to map");
              })));


        }


        private void CreateGeofence(Subject subject)
        {
            Geofence geofence = null;

            BasicGeoposition position;
            position.Latitude = subject.GetLocation().GetLattitude();
            position.Longitude = subject.GetLocation().GetLongitude();
            position.Altitude = 0.0;
            double radius = 30;

            Geocircle geocircle = new Geocircle(position, radius);

            MonitoredGeofenceStates mask = 0;

            mask |= MonitoredGeofenceStates.Entered;
            mask |= MonitoredGeofenceStates.Exited;

            geofence = new Geofence(subjects.GetSubjects().IndexOf(subject).ToString(), geocircle, mask, true);
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
                        setRequestedSubject(subject);

                        var dialog = new MessageDialog(subject.GetName(), "You have reach the following hotspot!");

                        UICommand moreInfo = new UICommand("More info");
                        moreInfo.Invoked = moreInfo_Click;
                        dialog.Commands.Add(moreInfo);

                        UICommand close = new UICommand("Close");
                        close.Invoked = close_Click;
                        dialog.Commands.Add(close);

                        await dialog.ShowAsync();
                        // User has entered the area.
                    }
                }
            });
        }

        private void close_Click(IUICommand command)
        { }

        private void setRequestedSubject(Subject subject)
        {
            requestedSubject = subject;
        }

        private Subject getRequestedSubject()
        {
            return requestedSubject;
        }

        private void moreInfo_Click(IUICommand command)
        {
            this.Frame.Navigate(typeof(DetailPage), getRequestedSubject());
        }
        private Geopoint ToGeopointConverter(Subject subject)
        {
            return ToGeopointConverter(subject.GetLocation().GetLattitude(), subject.GetLocation().GetLongitude());
        }
        private Geopoint ToGeopointConverter(double latitude, double longitude)
        {
            BasicGeoposition basicPoint = new BasicGeoposition
            {
                Latitude = latitude,
                Longitude = longitude
            };
            Geopoint point = new Geopoint(basicPoint);
            return point;
        }
        private async Task GetRouteAndDirections(LinkedList<Geopoint> list)
        {
            // Get the route between the points.
            MapRouteFinderResult routeResult = await MapRouteFinder.GetWalkingRouteFromWaypointsAsync(list);

            Debug.WriteLine("Route is opgehaald!");

            //Display route with text
            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                //InstructionsLabel.Text += "\n";
                // Display the directions.
                InstructionsLabel.Inlines.Add(new Run()
                {
                    Text = "Start route bij het VVV."
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

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
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
            await startup();
            this.navigationHelper.OnNavigatedTo(e);
            subjects = e.Parameter as Subjects;
            Debug.WriteLine("NavigateTo");

            LinkedList<Geopoint> geopointList = new LinkedList<Geopoint>();
            geopointList.AddFirst(myPoint);
            foreach (Subject s in subjects.GetSubjects())
            {
                if (s.GetName().Trim() != "")
                {
                    AddPoint_Map(s.GetLocation().GetLattitude(), s.GetLocation().GetLongitude(), s.GetName());
                    CreateGeofence(s);
                }
                BasicGeoposition bg = new BasicGeoposition();
                bg.Latitude = s.location.GetLattitude();
                bg.Longitude = s.location.GetLongitude();
                geopointList.AddLast(new Geopoint(bg));
            }
            Subject lastSub = new Subject(new GPSPoint(myPoint.Position.Latitude, myPoint.Position.Longitude), "Huidige locatie");

            await GetRouteAndDirections(geopointList);
            DestinationLabel.Text = "";
            int i = 1;
            foreach (Subject s in subjects.GetSubjects())
            {
                if (s.GetName().Trim() != "")
                {
                    DestinationLabel.Text += i + ": " + s.GetName() + "\n";
                    i++;
                }
            }
            Debug.WriteLine("Route volledig getekend");
            MyMap.CancelDirectManipulations();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);

            Debug.WriteLine("Navigated From");
        }

        #endregion

        private void MyMap_MapTapped(MapControl sender, MapInputEventArgs args)
        {
            var list = MyMap.FindMapElementsAtOffset(args.Position);

            if (list.Count > 0)
            {
                var query = (from g in subjects.GetSubjects() where (g.GetName()) == ((MapIcon)list.First()).Title select g);
                if (query.ToList().Count > 0)
                    this.Frame.Navigate(typeof(DetailPage), query.ToList().First());
            }
        }
    }
}