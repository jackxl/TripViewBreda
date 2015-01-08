﻿using System.Xml.Linq;
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

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556
using TripViewBreda.Model.Information;
using Windows.UI.Xaml.Media.Imaging;
using System.Diagnostics;

namespace TripViewBreda.Screens
{
    public sealed partial class DetailPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private Subject subject;

        public DetailPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        #region Functions
        private void UpdateInfo()
        {
            subjectName.DataContext = subject.GetName();
            subjectInformation.DataContext = subject.GetInformation();
        }

        private void ShowImageFlyout()
        { this.Flyout_ImageShower.Visibility = Windows.UI.Xaml.Visibility.Visible; }
        private void HideImageFlyout()
        { this.Flyout_ImageShower.Visibility = Windows.UI.Xaml.Visibility.Collapsed; }
        private void SetFlyoutImage(string name)
        { this.Flyout_Image_img.Source = LoadBitmapImage(name); }
        private BitmapImage LoadBitmapImage(string name)
        {
            Debug.WriteLine("Loading Bitmap Image: " + name);
            return new BitmapImage(new Uri("ms-appx:///Assets/SubjectResources/"+ name));
        }
        #endregion

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
            UpdateInfo();
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
        private void imageButton_Click(object sender, RoutedEventArgs e)
        {
            ShowImageFlyout();
            SetFlyoutImage(subject.GetImagePath());
        }

        private void Flyout_Close_bn_Click(object sender, RoutedEventArgs e)
        {
            HideImageFlyout();
        }

    }
}
