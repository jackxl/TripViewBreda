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
using TripViewBreda.Screens;
using Windows.Phone.UI.Input;
using System.Diagnostics;
using Windows.Storage;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace TripViewBreda
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public MainPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
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
            this.TextBox_Project.Text = AppSettings.APP_NAME;
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey(AppSettings.IsFirstLaunch))
            { ApplicationData.Current.LocalSettings.Values[AppSettings.IsFirstLaunch] = true; }
            else if (ApplicationData.Current.LocalSettings.Values[AppSettings.IsFirstLaunch] == null)
            { ApplicationData.Current.LocalSettings.Values[AppSettings.IsFirstLaunch] = true; }

            bool IsFirstLaunch = (bool)(ApplicationData.Current.LocalSettings.Values[AppSettings.IsFirstLaunch]);
            if (IsFirstLaunch)
            {
                MakeButtonsCollapsed();
                this.TextBlok_WelcomeMessage.Text = CreateWelcomeMessage();
            }
            else
            {
                this.Flyout_Welcome.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }
        private string CreateWelcomeMessage()
        {
            string text = "Welkom user\n";
            text += "This is the guide for Breda.\n\n";
            text += "For more information contact the VVV\n\n";
            text += "To continue press '" + this.Button_Close.Content + "'";
            return text;
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
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void Button_Routes_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RoutePage), e);
        }
        private void Button_Help_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HelpPage));
        }
        private void MapButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MapPage));
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            if (this.CheckBox_Dont_Show_Again.IsChecked == true)
            {
                ApplicationData.Current.LocalSettings.Values[AppSettings.IsFirstLaunch] = false;
            }
            MakeButtonsVisible();
            this.Flyout_Welcome.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
        public void MakeButtonsCollapsed()
        {
            this.TextBox_Project.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.TextBox_Title.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.Button_Routes.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.Button_Map.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.Button_Help.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.Button_Exit.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
        public void MakeButtonsVisible()
        {
            this.TextBox_Project.Visibility = Windows.UI.Xaml.Visibility.Visible;
            this.TextBox_Title.Visibility = Windows.UI.Xaml.Visibility.Visible;
            this.Button_Routes.Visibility = Windows.UI.Xaml.Visibility.Visible;
            this.Button_Map.Visibility = Windows.UI.Xaml.Visibility.Visible;
            this.Button_Help.Visibility = Windows.UI.Xaml.Visibility.Visible;
            this.Button_Exit.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }
    }
}
