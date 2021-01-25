using Controller;
using System;
using System.ComponentModel;

namespace WPFVisualisatie
{
    public class CompetitionStatsDataContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public CompetitionStatsDataContext()
        {
            Data.CurrentRace.DriversChanged += DataChanged;
        }

        public void DataChanged(Object sender, EventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}