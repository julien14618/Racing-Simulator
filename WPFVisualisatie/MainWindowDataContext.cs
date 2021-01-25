using Controller;
using System;
using System.ComponentModel;

namespace WPFVisualisatie
{
    public class MainWindowDataContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string TrackName
        {
            get
            {
                if (Data.CurrentRace != null)
                {
                    return Data.CurrentRace.Track.Name;
                }
                return "Not a race";
            }
            set
            {
                Data.CurrentRace.Track.Name = value;
            }
        }

        public MainWindowDataContext()
        {
            if (Data.CurrentRace != null)
                Data.CurrentRace.DriversChanged += OnDriversChanged;
        }

        public void OnDriversChanged(Object sender, EventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}