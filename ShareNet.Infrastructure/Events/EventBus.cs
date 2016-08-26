using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WusNet.Infrastructure.Logging;

namespace WusNet.Infrastructure.Events
{
    public class EventBus<TS, TA>:IEventBus<TS,TA> where TA:CommonEventArgs
    {
        // Fields
        private static volatile EventBus<TS, TA> _instance;
        private object EventHandlerKey_After;
        private object EventHandlerKey_AfterWithHistory;
        private object EventHandlerKey_BatchAfter;
        private object EventHandlerKey_BatchBefore;
        private object EventHandlerKey_Before;
        private object EventHandlerKey_BeforeWithHistory;
        private static EventHandlerList Handlers;
        private static readonly object lockObject;


        public event CommonEventHandler<TS, TA> After
        {
            add
            {
                EventBus<TS, TA>.Handlers.AddHandler(this.EventHandlerKey_After, value);
            }
            remove
            {
                EventBus<TS, TA>.Handlers.RemoveHandler(this.EventHandlerKey_After, value);
            }

        }

        public event EventHandlerWithHistory<TS, TA> AfterWithHistory
        {
            add
            {
                EventBus<TS, TA>.Handlers.AddHandler(this.EventHandlerKey_AfterWithHistory, value);
            }
            remove
            {
                EventBus<TS, TA>.Handlers.RemoveHandler(this.EventHandlerKey_AfterWithHistory, value);
            }

        }
        public event BatchEventHandler<TS, TA> BatchAfter
        {
            add
            {
                EventBus<TS, TA>.Handlers.AddHandler(this.EventHandlerKey_BatchAfter, value);
            }
            remove
            {
                EventBus<TS, TA>.Handlers.RemoveHandler(this.EventHandlerKey_BatchAfter, value);
            }

        }
        public event BatchEventHandler<TS, TA> BatchBefore
        {
            add
            {
                EventBus<TS, TA>.Handlers.AddHandler(this.EventHandlerKey_BatchBefore, value);
            }
            remove
            {
                EventBus<TS, TA>.Handlers.RemoveHandler(this.EventHandlerKey_BatchBefore, value);
            }

        }
        public event CommonEventHandler<TS, TA> Before
        {
            add
            {
                EventBus<TS, TA>.Handlers.AddHandler(this.EventHandlerKey_Before, value);
            }
            remove
            {
                EventBus<TS, TA>.Handlers.RemoveHandler(this.EventHandlerKey_Before, value);
            }

        }
        public event EventHandlerWithHistory<TS, TA> BeforeWithHistory
        {
            add
            {
                EventBus<TS, TA>.Handlers.AddHandler(this.EventHandlerKey_BeforeWithHistory, value);
            }
            remove
            {
                EventBus<TS, TA>.Handlers.RemoveHandler(this.EventHandlerKey_BeforeWithHistory, value);
            }

        }

        static EventBus()
        {
            EventBus<TS, TA>._instance = null;
            EventBus<TS, TA>.lockObject = new object();
            EventBus<TS, TA>.Handlers = new EventHandlerList();
        }
        protected EventBus()
        {
            this.EventHandlerKey_Before = new object();
            this.EventHandlerKey_After = new object();
            this.EventHandlerKey_BeforeWithHistory = new object();
            this.EventHandlerKey_AfterWithHistory = new object();
            this.EventHandlerKey_BatchBefore = new object();
            this.EventHandlerKey_BatchAfter = new object();
        }
        public static EventBus<TS, TA> Instance()
        {
            if (EventBus<TS, TA>._instance == null)
            {
                lock (EventBus<TS, TA>.lockObject)
                {
                    if (EventBus<TS, TA>._instance == null)
                    {
                        EventBus<TS, TA>._instance = new EventBus<TS, TA>();
                    }
                }
            }
            return EventBus<TS, TA>._instance;
        }

        /// <summary>
        /// 触发操作执行后事件
        /// </summary>
        /// <param name="sender">触发事件对象</param>
        /// <param name="eventArgs">事件参数</param>
        public void OnAfter(TS sender, TA eventArgs)
        {
            CommonEventHandler<TS, TA> handler = EventBus<TS, TA>.Handlers[this.EventHandlerKey_After] as CommonEventHandler<TS, TA>;
            if (handler!=null)
            {
                foreach (CommonEventHandler<TS, TA> handler2 in handler.GetInvocationList())
                {
                    try
                    {
                        handler2.BeginInvoke(sender, eventArgs, null, null);
                    }
                    catch (Exception e)
                    {

                        LoggerFactory.GetLogger().Log(LogLevel.Error, e, "执行触发操作执行后事件时发生异常");
                    }
                }
            }

            
        }
        /// <summary>
        /// 触发含历史数据操作执行后事件
        /// </summary>
        /// <param name="sender">触发事件对象</param>
        /// <param name="eventArgs"></param>
        /// <param name="historyData">触发事件对象的历史数据（例如S从一种状态变更为另一种状态，historyData指变更前的对象）</param>
        public void OnAfterWithHistory(TS sender, TA eventArgs, TS historyData)
        {
            EventHandlerWithHistory<TS, TA> history = EventBus<TS, TA>.Handlers[this.EventHandlerKey_AfterWithHistory] as EventHandlerWithHistory<TS, TA>;
            if (history != null)
            {
                foreach (EventHandlerWithHistory<TS, TA> history2 in history.GetInvocationList())
                {
                    try
                    {
                        history2.BeginInvoke(sender, eventArgs, historyData, null, null);
                    }
                    catch (Exception exception)
                    {
                        LoggerFactory.GetLogger().Log(LogLevel.Error, exception, "执行触发含历史数据操作执行后事件时发生异常");
                    }
                }
            }

        }
        /// <summary>
        /// 触发批量操作执行后事件
        /// </summary>
        /// <param name="senders"></param>
        /// <param name="eventArgs"></param>
        public void OnBatchAfter(IEnumerable<TS> senders, TA eventArgs)
        {
            BatchEventHandler<TS, TA> handler = EventBus<TS, TA>.Handlers[this.EventHandlerKey_BatchAfter] as BatchEventHandler<TS, TA>;
            if (handler != null)
            {
                foreach (BatchEventHandler<TS, TA> handler2 in handler.GetInvocationList())
                {
                    try
                    {
                        handler2.BeginInvoke(senders, eventArgs, null, null);
                    }
                    catch (Exception exception)
                    {
                        LoggerFactory.GetLogger().Log(LogLevel.Error, exception, "执行触发批量操作执行后事件时发生异常");
                    }
                }
            }

        }
        /// <summary>
        /// 触发批量操作执行前事件
        /// </summary>
        /// <param name="senders"></param>
        /// <param name="eventArgs"></param>
        public void OnBatchBefore(IEnumerable<TS> senders, TA eventArgs)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        ///  触发操作执行前事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void OnBefore(TS sender, TA eventArgs)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 触发含历史数据操作执行前事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        /// <param name="historyData"></param>
        public void OnBeforeWithHistory(TS sender, TA eventArgs, TS historyData)
        {
            throw new NotImplementedException();
        }
    }
}
