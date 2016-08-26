using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WusNet.Infrastructure.Logging;
using WusNet.Infrastructure.WusNet;

namespace WusNet.Infrastructure.Events
{
    public class CommonEventArgs:EventArgs
    {
        private int _applicationId;
        private string _eventOperationType;
        private OperatorInfo operatorInfo;


        public CommonEventArgs(string eventOperationType)
            : this(eventOperationType, 0)
        {
        }


        public CommonEventArgs(string eventOperationType,int applicationId)
        {
            this._eventOperationType = eventOperationType;
            this._applicationId = applicationId;
            IOperatorInfoGetter getter = DIContainer.Resolve<IOperatorInfoGetter>();
            if (getter==null)
            {
                throw new ApplicationException("IOperatorInfoGetter not registered to DIContainer");

            }
            this.operatorInfo = getter.GetOperatorInfo();
        }
        // Properties
        public int ApplicationId
        {
            get
            {
                return this._applicationId;
            }
        }

        public string EventOperationType
        {
            get
            {
                return this._eventOperationType;
            }
        }

        public OperatorInfo OperatorInfo
        {
            get
            {
                return this.operatorInfo;
            }
        }


    }
}
