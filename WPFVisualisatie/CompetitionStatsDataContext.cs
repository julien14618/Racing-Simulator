using Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WPFVisualisatie
{
    public class CompetitionStatsDataContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<Model.PointsPerParticipant> PointsPerParticipants
        {
            get
            {
                if(Data.Competition != null)
                    return (Data.Competition.PointsPerParticipant._list.OrderByDescending(driver => driver.Points).ToList());
                return null;
            }
        }
        public List<Model.IParticipant> Participants
        {
            get
            {
                if (Data.Competition != null)
                    return Data.Competition.Participants;
                return null;
            }
        }
        public CompetitionStatsDataContext()
        {
            if(Data.CurrentRace != null)
                Data.CurrentRace.DriversChanged += DataChanged;
            else
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

        public void DataChanged(Object sender, EventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

        
    }
}