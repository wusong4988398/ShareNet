using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WusNet.Infrastructure.Events
{
    public class EventBus<S>:EventBus<S,CommonEventArgs>
    {
        // Fields
        private static volatile EventBus<S> _instance;
        private static readonly object lockObject;
        // Methods
        static EventBus()
        {
            EventBus<S>._instance = null;
            EventBus<S>.lockObject = new object();
        }
        public static EventBus<S> Instance()
        {
            if (EventBus<S>._instance == null)
            {
                lock (EventBus<S>.lockObject)
                {
                    if (EventBus<S>._instance == null)
                    {
                        EventBus<S>._instance = new EventBus<S>();
                    }
                }
            }
            return EventBus<S>._instance;
        }


    }
}
