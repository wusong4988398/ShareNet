using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WusNet.Infrastructure.Events
{
    /// <summary>
    /// 业务事件类
    /// </summary>
    public interface IEventBus<TS, TA>
    {

        // Events
        event CommonEventHandler<TS, TA> After;
        event EventHandlerWithHistory<TS, TA> AfterWithHistory;
        event BatchEventHandler<TS, TA> BatchAfter;
        event BatchEventHandler<TS, TA> BatchBefore;
        event CommonEventHandler<TS, TA> Before;
        event EventHandlerWithHistory<TS, TA> BeforeWithHistory;

        // Methods
        void OnAfter(TS sender, TA eventArgs);
        void OnAfterWithHistory(TS sender, TA eventArgs, TS historyData);
        void OnBatchAfter(IEnumerable<TS> senders, TA eventArgs);
        void OnBatchBefore(IEnumerable<TS> senders, TA eventArgs);
        void OnBefore(TS sender, TA eventArgs);
        void OnBeforeWithHistory(TS sender, TA eventArgs, TS historyData);



    }
}
