using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Records<T>
    {
        public List<T> _list { get; set; }
        

        public void AddToList(T item)
        {
            _list.Add(item);
        }
       
    }
}
