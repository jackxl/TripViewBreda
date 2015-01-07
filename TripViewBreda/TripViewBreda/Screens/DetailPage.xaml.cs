using System.Xml.Linq;
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
using TripViewBreda.Controllers;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556
using TripViewBreda.Model.Information;

namespace TripViewBreda.Screens
{
    public sealed partial class DetailPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private DetailController detailController;
        private Subject subject;

        public DetailPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        private void ReadData()
        {
            subjectName.DataContext = detailController.GetSubject().GetName();
            subjectInformation.DataContext = detailController.GetSubject().GetInformation();
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
        { }
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        { }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
            subject = e.Parameter as Subject;

            detailController = new DetailController(subject);
            ReadData();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}
