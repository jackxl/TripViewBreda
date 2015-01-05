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
using Windows.UI;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace TripViewBreda.Screens
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FrequentlyAskedQuestionPage : Page
    {
        private int FontSizeTopic = 24;
        private int FontSizeDescription = 16;
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public FrequentlyAskedQuestionPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
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
            LoadQuestions();
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
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

        private void LoadQuestions()
        {
            foreach (string[] question in Information.FrequentlyAskedQuestions)
            {
                this.StackPanel_F_A_Q.Children.Add(CreateStackPanel(question));
            }
        }

        #region Create StackPanel
        private StackPanel CreateStackPanel(string[] question)
        {
            StackPanel panel = new StackPanel();
            panel.Orientation = Orientation.Vertical;
            panel.Height = CalculationStackPanelHeigth(question); // TODO: Make Algoritme
            panel.Background = new SolidColorBrush(Colors.Brown);
            panel.Margin = new Thickness(0, 10, 0, 10);

            TextBlock topic = new TextBlock();
            topic.FontSize = FontSizeTopic;
            topic.TextWrapping = TextWrapping.Wrap;
            topic.Text = question[0];

            TextBlock description = new TextBlock();
            description.FontSize = FontSizeDescription;
            description.TextWrapping = TextWrapping.Wrap;
            description.Text = question[1];
            description.Margin = new Thickness(10, 0, 0, 0);

            panel.Children.Add(topic);
            panel.Children.Add(description);

            return panel;
        }
        private double CalculationStackPanelHeigth(string[] question)
        {
            double factor = 1.1;
            int topicLines = (question[0].Length / 29);
            int descriptionLines = (question[1].Length / 44);
            int topicHeight = (int)(FontSizeTopic * factor);
            int descriptionHeight = (int)(FontSizeDescription * factor);
            double total = 0;

            total += topicHeight + (topicHeight * topicLines);
            total += descriptionHeight + (descriptionHeight * descriptionLines);
            total += descriptionHeight / 2;

            return total;
        }
        #endregion
    }
}
