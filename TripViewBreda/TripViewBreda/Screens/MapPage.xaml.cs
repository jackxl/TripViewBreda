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

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace TripViewBreda
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapPage : Page
    {
        private NavigationHelper navigationHelper;
        private Geopoint myPoint, myPreviousPoint;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private Subjects subjects;
        private Geolocator locator = new Geolocator();
        private DispatcherTimer VirtualPositionTimer;
        private short DispatchCounter; // no need to set this because it gets set on the first virtual update.

        private Subject requestedSubject;


        public MapPage()
        {
            this.InitializeComponent();

            GeofenceMonitor.Current.Geofences.Clear();
            GeofenceMonitor.Current.GeofenceStateChanged += Current_GeofenceStateChanged;

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            locator.DesiredAccuracy = PositionAccuracy.High;
            locator.DesiredAccuracyInMeters = 10;

            VirtualPositionTimer = new DispatcherTimer();
            VirtualPositionTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000); // 1000 Milliseconds 
            VirtualPositionTimer.Tick += new EventHandler<object>(CalculateCurrentPosition);
            VirtualPositionTimer.Start();
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

        public void CalculateCurrentPosition(object sender, object e) //Dirty fix (http://goo.gl/AzbolV)
        {
            double deltaLatitude;
            double deltaLongitude;

            if (myPreviousPoint == null)
            {
                myPreviousPoint = myPoint;
                DispatchCounter = 0;
            }
            else if (DispatchCounter < 3)
            {
                deltaLatitude = myPoint.Position.Latitude - myPreviousPoint.Position.Latitude;
                deltaLongitude = myPoint.Position.Longitude - myPreviousPoint.Position.Longitude;

                BasicGeoposition postion = new BasicGeoposition();
                postion.Latitude = myPoint.Position.Latitude + deltaLatitude;
                postion.Longitude = myPoint.Position.Longitude + deltaLongitude;
                myPoint = new Geopoint(postion);
            }
            else
            {
                DispatchCounter = 0;
                try
                {
                    getCurrentPosition();
                }
                catch (Exception ex)
                {
                    CalculateCurrentPosition(sender, e);
                }
            }
        }
        private async void getCurrentPosition()
        {
            var position = await locator.GetGeopositionAsync();
            myPoint = position.Coordinate.Point;
        }

        public async Task GoToCurrentPosition()
        {
            //getCurrentPosition();
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
        { return ToGeopointConverter(subject.GetLocation().GetLattitude(), subject.GetLocation().GetLongitude()); }
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
        private async Task GetRouteAndDirections(Subject start, Subject end)
        {
            // Start at start subject
            Geopoint startPoint = ToGeopointConverter(start);

            // End at end subject
            Geopoint endPoint = ToGeopointConverter(end);
           

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
            Debug.WriteLine("Navigate To");
            this.navigationHelper.OnNavigatedTo(e);
            subjects = e.Parameter as Subjects;

            foreach (Subject s in subjects.GetSubjects())
            {
                AddPoint_Map(s.GetLocation().GetLattitude(), s.GetLocation().GetLongitude(), s.GetName());
                CreateGeofence(s);
            }
            double[] lastKnownLocation = (double[])(ApplicationData.Current.LocalSettings.Values[AppSettings.LastKnownLocation]);
            Geopoint point = ToGeopointConverter(lastKnownLocation[0], lastKnownLocation[1]);
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
