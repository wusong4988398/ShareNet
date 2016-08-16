using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;
using PetaPoco.Providers;

namespace ShareNet.Infrastructure.PetaPoco
{
    public abstract class BaseDatabase : IDisposable
    {
      
        protected IDatabase DB { get; set; }
       
        protected BaseDatabase(string connectionStringName)
        {
            DB = new Database(connectionStringName) {EnableAutoSelect = true, EnableNamedParams = true};
        }
        public void Dispose()
        {
            if (DB!=null)
            {
                DB.Dispose();
                DB = null;
            }
        }
    }
}
