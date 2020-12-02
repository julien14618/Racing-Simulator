using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public interface IRecord<T> where T : IRecord<T>
    {
        public IParticipant Driver { get; set; }
        public void Add(List<T> list);
        public string BestDriver(List<T> list);

    }
}
