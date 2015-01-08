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
using TripViewBreda.Screens;
using Windows.Phone.UI.Input;
using System.Diagnostics;
using Windows.Storage;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace TripViewBreda.Screens
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

        #region NavigationHelper registration

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            this.TextBox_Project.Text = AppSettings.APP_NAME;
            if (!ApplicationData.Current.LocalSettings.Values.ContainsKey(AppSettings.IsFirstLaunch))
            { ApplicationData.Current.LocalSettings.Values[AppSettings.IsFirstLaunch] = true; }
            else if (ApplicationData.Current.LocalSettings.Values[AppSettings.IsFirstLaunch] == null)
            { ApplicationData.Current.LocalSettings.Values[AppSettings.IsFirstLaunch] = true; }

            bool IsFirstLaunch = (bool)(ApplicationData.Current.LocalSettings.Values[AppSettings.IsFirstLaunch]);
            if (IsFirstLaunch)
            {
                MakeButtonsCollapsed();
                this.TextBlok_WelcomeMessage.Text = CreateWelcomeMessage();
                this.Flyout_Welcome.Visibility = Windows.UI.Xaml.Visibility.Visible;
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
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        { }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        { this.navigationHelper.OnNavigatedTo(e); }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        { this.navigationHelper.OnNavigatedFrom(e); }
        public NavigationHelper NavigationHelper
        { get { return this.navigationHelper; } }

        public ObservableDictionary DefaultViewModel
        { get { return this.defaultViewModel; } }

        #endregion

        #region Buttons

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
            this.Flyout_Welcome.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            MakeButtonsVisible();
        }
        #endregion Buttons

        #region Flyout Functions
        public void MakeButtonsCollapsed()
        {
            this.TextBox_Project.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.TextBox_Title.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.Button_Routes.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.Button_Help.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.Button_Exit.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
        public void MakeButtonsVisible()
        {
            this.TextBox_Project.Visibility = Windows.UI.Xaml.Visibility.Visible;
            this.TextBox_Title.Visibility = Windows.UI.Xaml.Visibility.Visible;
            this.Button_Routes.Visibility = Windows.UI.Xaml.Visibility.Visible;
            this.Button_Help.Visibility = Windows.UI.Xaml.Visibility.Visible;
            this.Button_Exit.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }
        #endregion Flyout Functions
    }
}
