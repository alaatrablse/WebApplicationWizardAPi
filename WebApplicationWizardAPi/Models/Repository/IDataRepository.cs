using System.Collections.Generic;

namespace WebApplicationWizardAPi.Models.Repository
{
    public interface IDataRepository<TEntity, TDto>
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(long id);
        TDto GetUser(string email, string pass);
        TEntity GetDto(long id);
        void Add(TEntity entity);
        void Update(TEntity entityToUpdate, TEntity entity);
        void Delete(TEntity entity);
        bool Check(long v);
        bool Check(string v);
    }
}
