using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class RaceFinishedEventArgs : EventArgs
    {
        public List<IParticipant> Participants { get; set; }

        public RaceFinishedEventArgs(List<IParticipant> participants)
        {
            Participants = participants;
        }
    }
}
