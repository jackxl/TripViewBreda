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
using Windows.Storage;
using System.Diagnostics;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace TripViewBreda.Screens
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HelpPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public HelpPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        #region NavigationHelper registration
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            TextBox_Projectname.Text = AppSettings.APP_NAME;
            TextBox_HelpContent.Text = getHelpText();
        }

        private string getHelpText()
        {
            string helpText = "";
            helpText += "Gebruik van de applicatie." + "\n";
            helpText += "Het startmenu bevat vier items." + "\n";
            helpText += "Door op 'Route' te drukken kunt u de navigatie van een route starten." + "\n";
            helpText += "Door op 'Kaart' te drukken maakt u de kaart zichtbaar en kunt u de omgeving verkennen." + "\n";
            helpText += "De 'Help' knop laat dit helpscherm zien." + "\n";
            helpText += "De 'Exit' knop zal de applicatie afsluiten" + "\n";
            return helpText;

        }
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        { }

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        #region Buttons
        private void Button_F_A_Q_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(FrequentlyAskedQuestionPage), e);
        }

        private void Button_ResetSettings_Click(object sender, RoutedEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values[AppSettings.IsFirstLaunch] = true;
            Debug.WriteLine(AppSettings.IsFirstLaunch + ": " + ApplicationData.Current.LocalSettings.Values[AppSettings.IsFirstLaunch]);
        }
        #endregion
    }
}
