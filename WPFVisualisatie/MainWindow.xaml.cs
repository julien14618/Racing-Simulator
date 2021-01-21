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
        public MainWindow()
        {
            InitializeComponent();
            Data.Initialize();
            Data.NextRace();
            Data.CurrentRace.SetParticipants();
            CreateImages.Initialise();
            RaceVisualisation.Initialize();
            Data.CurrentRace.DriversChanged += OnDriversChanged;
            Data.CurrentRace.RaceFinished += WPFRaceFinished;
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
            InitializeComponent();
            CreateImages.Initialise();
            RaceVisualisation.Initialize();
            Data.CurrentRace.DriversChanged += OnDriversChanged;
            Data.CurrentRace.RaceFinished += WPFRaceFinished;
            Data.CurrentRace.SetParticipants();
            Data.CurrentRace.Start();
        }
    }
}