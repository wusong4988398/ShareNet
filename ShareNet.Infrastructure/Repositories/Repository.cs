using System.Collections.Generic;
using System.Linq;
using PetaPoco;
using PetaPoco.Core;
using ShareNet.Infrastructure.PetaPoco;
using WusNet.Infrastructure.Caching;
using WusNet.Infrastructure.WusNet;

namespace WusNet.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity:class ,IEntity
    {

        private int cacheablePageCount;
        public ICacheService cacheService;
        private Database database;
        private int primaryMaxRecords;
        private int secondaryMaxRecords;

        public Repository()
        {
            this.cacheService = DIContainer.Resolve<ICacheService>();

            this.cacheablePageCount = 30;
            this.primaryMaxRecords = 0xc350;
            this.secondaryMaxRecords = 0x3e8;

        }

        protected virtual Database CreateDao()
        {
            
            if (this.database==null)
            {
                this.database = Database.CreateInstance(null);
            }
            return database;
        }

        public int CacheablePageCount
        {
            get { return cacheablePageCount; }
        }

        public int PrimaryMaxRecords
        {
            get { return primaryMaxRecords; }
        }

        public int SecondaryMaxRecords
        {
            get { return secondaryMaxRecords; }
        }


        public int Delete(TEntity entity)
        {
            if (entity == null)
            {
                return 0;
            }
            int num = this.CreateDao().Delete(entity);
            if (num > 0)
            {
                this.OnDeleted(entity);
            }
            return num;

        }

        public int DeleteByEntityId(object entityId)
        {
            TEntity entity = this.Get(entityId);
            if (entity == null)
            {
                return 0;
            }
            return this.Delete(entity);

        }

        public bool Exists(object entityId)
        {
            return this.CreateDao().Exists<TEntity>(entityId);

        }

        public TEntity Get(object entityId)
        {
            TEntity local = default(TEntity);
            if (Repository<TEntity>.RealTimeCacheHelper.EnableCache)
            {
                local = this.cacheService.Get<TEntity>(Repository<TEntity>.RealTimeCacheHelper.GetCacheKeyOfEntity(entityId));
            }
            if (local == null)
            {
                local = this.CreateDao().SingleOrDefault<TEntity>(entityId);
                if (Repository<TEntity>.RealTimeCacheHelper.EnableCache && (local != null))
                {
                    if (Repository<TEntity>.RealTimeCacheHelper.PropertyNameOfBody != null)
                    {
                        Repository<TEntity>.RealTimeCacheHelper.PropertyNameOfBody.SetValue(local, null, null);
                    }
                    this.cacheService.Add(Repository<TEntity>.RealTimeCacheHelper.GetCacheKeyOfEntity(local.EntityId), local, Repository<TEntity>.RealTimeCacheHelper.CachingExpirationType);
                }
            }
            if ((local != null) && !local.IsDeletedInDatabase)
            {
                return local;
            }
            return default(TEntity);

        }
        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll()
        {
          return  this.GetAll(null);
        }

        public IEnumerable<TEntity> GetAll(string orderBy)
        {
            IEnumerable<object> enumerable = null;
            string cacheKey = null;
            if (Repository<TEntity>.RealTimeCacheHelper.EnableCache)
            {
                cacheKey = Repository<TEntity>.RealTimeCacheHelper.GetListCacheKeyPrefix(CacheVersionType.GlobalVersion);
                if (!string.IsNullOrEmpty(orderBy))
                {
                    cacheKey = cacheKey + "SB-" + orderBy;
                }
                enumerable = this.cacheService.Get<IEnumerable<object>>(cacheKey);
            }
            if (enumerable == null)
            {
                PocoData data = PocoData.ForType(typeof(TEntity));
                Sql sql = Sql.Builder.Select(new object[] { data.TableInfo.PrimaryKey }).From(new object[] { data.TableInfo.TableName });
                if (!string.IsNullOrEmpty(orderBy))
                {
                    sql.OrderBy(new object[] { orderBy });
                }
                enumerable = this.CreateDao().FetchFirstColumn(sql);
                if (Repository<TEntity>.RealTimeCacheHelper.EnableCache)
                {
                    this.cacheService.Add(cacheKey, enumerable, Repository<TEntity>.RealTimeCacheHelper.CachingExpirationType);
                }
            }
            return this.PopulateEntitiesByEntityIds<object>(enumerable);
 

        }

        public object Insert(TEntity entity)
        {
            object obj2 = this.CreateDao().Insert(entity);
            this.OnInserted(entity);
            return obj2;

        }

        public virtual IEnumerable<TEntity> PopulateEntitiesByEntityIds<T>(IEnumerable<T> entityIds)
        {
            TEntity[] localArray = new TEntity[entityIds.Count<T>()];
            Dictionary<object, int> dictionary = new Dictionary<object, int>();
            for (int i = 0; i < entityIds.Count<T>(); i++)
            {
                TEntity local = this.cacheService.Get<TEntity>(Repository<TEntity>.RealTimeCacheHelper.GetCacheKeyOfEntity(entityIds.ElementAt<T>(i)));
                if (local != null)
                {
                    localArray[i] = local;
                }
                else
                {
                    localArray[i] = default(TEntity);
                    dictionary[entityIds.ElementAt<T>(i)] = i;
                }
            }
            if (dictionary.Count > 0)
            {
                foreach (TEntity local2 in this.CreateDao().FetchByPrimaryKeys<TEntity>(dictionary.Keys))
                {
                    localArray[dictionary[local2.EntityId]] = local2;
                    if (Repository<TEntity>.RealTimeCacheHelper.EnableCache && (local2 != null))
                    {
                        if ((Repository<TEntity>.RealTimeCacheHelper.PropertyNameOfBody != null) && (Repository<TEntity>.RealTimeCacheHelper.PropertyNameOfBody != null))
                        {
                            Repository<TEntity>.RealTimeCacheHelper.PropertyNameOfBody.SetValue(local2, null, null);
                        }
                        this.cacheService.Set(Repository<TEntity>.RealTimeCacheHelper.GetCacheKeyOfEntity(local2.EntityId), local2, Repository<TEntity>.RealTimeCacheHelper.CachingExpirationType);
                    }
                }
            }
            List<TEntity> list = new List<TEntity>();
            foreach (TEntity local3 in localArray)
            {
                if ((local3 != null) && !local3.IsDeletedInDatabase)
                {
                    list.Add(local3);
                }
            }
            return list;

        }

        public void Update(TEntity entity)
        {
            int num;
            Database database = this.CreateDao();
            //if (entity is ISerializableProperties)
            //{
            //    ISerializableProperties properties = entity as ISerializableProperties;
            //    if (properties != null)
            //    {
            //        properties.Serialize();
            //    }
            //}

            if ((Repository<TEntity>.RealTimeCacheHelper.PropertyNameOfBody != null) && (Repository<TEntity>.RealTimeCacheHelper.PropertyNameOfBody.GetValue(entity, null) == null))
            {
                PocoData data = PocoData.ForType(typeof(TEntity));
                List<string> list = new List<string>();
                foreach (KeyValuePair<string, PocoColumn> pair in data.Columns)
                {
                    if (((string.Compare(pair.Key, data.TableInfo.PrimaryKey, true) != 0) && ((SqlBehaviorFlags.Update & pair.Value.SqlBehavior) != 0)) && ((string.Compare(pair.Key, Repository<TEntity>.RealTimeCacheHelper.PropertyNameOfBody.Name, true) != 0) && !pair.Value.ResultColumn))
                    {
                        list.Add(pair.Key);
                    }
                }
                num = database.Update(entity, (IEnumerable<string>)list);
            }
            else
            {
                num = database.Update(entity);
            }
            if (num > 0)
            {
                this.OnUpdated(entity);
            }

        }

        protected virtual void OnUpdated(TEntity entity)
        {
            if (Repository<TEntity>.RealTimeCacheHelper.EnableCache)
            {
                Repository<TEntity>.RealTimeCacheHelper.IncreaseEntityCacheVersion(entity.EntityId);
                Repository<TEntity>.RealTimeCacheHelper.IncreaseListCacheVersion(entity);
                if ((Repository<TEntity>.RealTimeCacheHelper.PropertyNameOfBody != null) && (Repository<TEntity>.RealTimeCacheHelper.PropertyNameOfBody.GetValue(entity, null) != null))
                {
                    string str = Repository<TEntity>.RealTimeCacheHelper.PropertyNameOfBody.GetValue(entity, null) as string;
                    this.cacheService.Set(Repository<TEntity>.RealTimeCacheHelper.GetCacheKeyOfEntityBody(entity.EntityId), str, Repository<TEntity>.RealTimeCacheHelper.CachingExpirationType);
                    Repository<TEntity>.RealTimeCacheHelper.PropertyNameOfBody.SetValue(entity, null, null);
                }
                this.cacheService.Set(Repository<TEntity>.RealTimeCacheHelper.GetCacheKeyOfEntity(entity.EntityId), entity, Repository<TEntity>.RealTimeCacheHelper.CachingExpirationType);
            }

        }

        protected virtual void OnDeleted(TEntity entity)
        {
            if (Repository<TEntity>.RealTimeCacheHelper.EnableCache)
            {
                Repository<TEntity>.RealTimeCacheHelper.IncreaseEntityCacheVersion(entity.EntityId);
                Repository<TEntity>.RealTimeCacheHelper.IncreaseListCacheVersion(entity);
                this.cacheService.MarkDeletion(Repository<TEntity>.RealTimeCacheHelper.GetCacheKeyOfEntity(entity.EntityId), entity, CachingExpirationType.SingleObject);
            }
        }

        protected virtual void OnInserted(TEntity entity)
        {
            if (Repository<TEntity>.RealTimeCacheHelper.EnableCache)
            {
                Repository<TEntity>.RealTimeCacheHelper.IncreaseListCacheVersion(entity);
                if (Repository<TEntity>.RealTimeCacheHelper.PropertyNameOfBody != null)
                {
                    string str = Repository<TEntity>.RealTimeCacheHelper.PropertyNameOfBody.GetValue(entity, null) as string;
                    this.cacheService.Add(Repository<TEntity>.RealTimeCacheHelper.GetCacheKeyOfEntityBody(entity.EntityId), str, Repository<TEntity>.RealTimeCacheHelper.CachingExpirationType);
                    Repository<TEntity>.RealTimeCacheHelper.PropertyNameOfBody.SetValue(entity, null, null);
                }
                this.cacheService.Add(Repository<TEntity>.RealTimeCacheHelper.GetCacheKeyOfEntity(entity.EntityId), entity, Repository<TEntity>.RealTimeCacheHelper.CachingExpirationType);
            }
        }


        protected static RealTimeCacheHelper RealTimeCacheHelper
        {
            get
            {
                return EntityData.ForType(typeof(TEntity)).RealTimeCacheHelper;
            }
        }


    }
}
