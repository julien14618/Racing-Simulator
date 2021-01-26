using Controller;
using System;
using Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;

namespace WPFVisualisatie
{
    public class CurrentRaceStatsDataContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<Model.FailedEquipmentPerParticipant> FailedEquipment
        {
            get
            {
                if (Data.Competition != null)
                    return (Data.Competition.FailedEquipmentPerParticipant._list.OrderByDescending(driver => driver.Count).ToList());
                return null;
            }
        }
        public List<Model.DriversOvertaken> DriversOvertaken
        {
            get
            {
                if (Data.Competition != null)
                    return Data.Competition.DriversOvertaken._list.OrderByDescending(driver => driver.Count).ToList();
                return null;
            }
        }
        public List<Model.IParticipant> participants
        {
            get
            {
                if (Data.Competition != null)
                    return Data.Competition.Participants;
                return null;
            }
        }
        public CurrentRaceStatsDataContext()
        {
            Data.CurrentRace.DriversChanged += DataChanged;
            Data.CurrentRace.RaceFinished += RaceFinished;
        }

        public void DataChanged(Object sender, EventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
        public void RaceFinished(Object sender, EventArgs e)
        {
            Data.Competition.FailedEquipmentPerParticipant._list.ForEach(x => x.Count = 0);
            Data.Competition.DriversOvertaken._list.ForEach(x => x.Count = 0);
        }
    }
}
