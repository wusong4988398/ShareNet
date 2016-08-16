using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WusNet.Infrastructure.Events
{
    public delegate void CommonEventHandler<S, A>(S sender, A eventArgs);

    public delegate void EventHandlerWithHistory<S, A>(S sender, A eventArgs, S historyData);

    public delegate void BatchEventHandler<S, A>(IEnumerable<S> senders, A eventArgs);

 



   
}
