using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareNet.Infrastructure.PetaPoco
{
    [Flags]
    public enum SqlBehaviorFlags
    {
        All = 3,
        Insert = 1,
        Update = 2
    }

 

}
