using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WusNet.Infrastructure.WusNet
{
    public abstract class IdGenerator
    {
        // Fields
        private static volatile IdGenerator _defaultInstance = null;
        private static readonly object lockObject = new object();

        protected IdGenerator()
        {
        }

        private static IdGenerator Instance()
        {
            if (_defaultInstance==null)
            {
                lock (lockObject)
                {
                    if (_defaultInstance==null)
                    {
                        _defaultInstance = DIContainer.Resolve<IdGenerator>();
                        if (_defaultInstance == null)
                        {
                            throw new ExceptionFacade("未在DIContainer注册IdGenerator的具体实现类", null);
                        }


                    }

                }
            }
            return _defaultInstance;

        }

        public static long Next()
        {
            lock (lockObject)
            {
                return Instance().NextLong();
            }
        }

        protected abstract long NextLong();



    }
}
