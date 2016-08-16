using System.Collections.Generic;
using WusNet.Infrastructure.WusNet;

namespace WusNet.Infrastructure.Repositories
{
    public interface IRepository<TEntity> where TEntity:class,IEntity
    {
        int Delete(TEntity entity);
        int DeleteByEntityId(object primaryKey);
        bool Exists(object primaryKey);
        TEntity Get(object primaryKey);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(string orderBy);
        object Insert(TEntity entity);
        IEnumerable<TEntity> PopulateEntitiesByEntityIds<T>(IEnumerable<T> entityIds);
        void Update(TEntity entity);
    }
}
