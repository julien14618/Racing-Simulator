using Controller;
using Model;
using System;
using System.Windows;
using System.Windows.Threading;

namespace WPFVisualisatie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CompetitionStats competitionStats;
        private CurrentRaceStats CurrentRaceStats;

        public MainWindow()
        {
            Data.Initialize();
            Data.NextRace();
            Data.CurrentRace.SetParticipants();
            CreateImages.Initialise();
            RaceVisualisation.Initialize();
            Data.CurrentRace.DriversChanged += OnDriversChanged;
            Data.CurrentRace.RaceFinished += WPFRaceFinished;
            InitializeComponent();
            Data.CurrentRace.Start();
        }

        public void OnDriversChanged(object sender, EventArgs e)
        {
            DriversChangedEventArgs eventArgs = (DriversChangedEventArgs)e;
            this.TrackImage.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() =>
            {
                this.TrackImage.Source = null;
                this.TrackImage.Source = RaceVisualisation.DrawTrack(eventArgs.Track);
            }));
        }

        public void WPFRaceFinished(object sender, EventArgs e)
        {
            Data.CurrentRace.DisposeEventHandlers();
            CreateImages.ClearCache();
            CreateImages.Initialise();
            RaceVisualisation.Initialize();
            Data.CurrentRace.DriversChanged += OnDriversChanged;
            Data.CurrentRace.RaceFinished += WPFRaceFinished;
            Data.CurrentRace.SetParticipants();
            InitializeComponent();
            Data.CurrentRace.Start();
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_OpenCompetitionStats_Click(object sender, RoutedEventArgs e)
        {
            competitionStats = new CompetitionStats();
            competitionStats.Show();
        }

        private void MenuItem_OpenCurrentRaceStats_Click(object sender, RoutedEventArgs e)
        {
            CurrentRaceStats = new CurrentRaceStats();
            CurrentRaceStats.Show();
        }
    }
}