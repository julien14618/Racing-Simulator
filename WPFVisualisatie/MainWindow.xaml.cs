using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            Data.CurrentRace.DriversChanged += OnDriversChanged;
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
    }
}