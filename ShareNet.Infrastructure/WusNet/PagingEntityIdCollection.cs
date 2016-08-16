using System;
using System.Collections.Generic;
using System.Linq;

namespace WusNet.Infrastructure.WusNet
{
    [Serializable]
    public class PagingEntityIdCollection
    {
        // Fields
        private List<object> entityIds;
        private bool isContainsMultiplePages;
        private long totalRecords;
        public PagingEntityIdCollection(IEnumerable<object> entityIds)
        {
            this.totalRecords = -1L;
            this.entityIds = entityIds.ToList<object>();
        }
        public PagingEntityIdCollection(IEnumerable<object> entityIds, long totalRecords)
        {
           
            this.entityIds = entityIds.ToList<object>();
            this.totalRecords = totalRecords;
        }

        public IEnumerable<object> GetPagingEntityIds(int pageSize, int pageIndex)
        {
            if (this.entityIds==null)
            {
                return new List<object>();
            }
            if (!this.IsContainsMultiplePages)
            {
                return this.entityIds.GetRange(0, (this.Count > pageSize) ? pageSize : this.Count);
            }
            if (pageIndex<1)
            {
                pageIndex = 1;
            }
            int index = pageSize * (pageIndex - 1);
            int num2 = pageSize * pageIndex;
            int count = this.entityIds.Count;
            if (index >= count)
            {
                return new List<object>();
            }
            if (num2 < count)
            {
                return this.entityIds.GetRange(index, pageSize);
            }



            return this.entityIds.GetRange(index, count - index);

        }
        // Properties
        public int Count
        {
            get
            {
                if (this.entityIds == null)
                {
                    return 0;
                }
                return this.entityIds.Count;
            }
        }
        public bool IsContainsMultiplePages
        {
            get
            {
                return this.isContainsMultiplePages;
            }
            set
            {
                this.isContainsMultiplePages = value;
            }
        }
        public long TotalRecords
        {
            get
            {
                if (this.totalRecords > 0L)
                {
                    return this.totalRecords;
                }
                return (long)this.Count;
            }
        }




    }
}
