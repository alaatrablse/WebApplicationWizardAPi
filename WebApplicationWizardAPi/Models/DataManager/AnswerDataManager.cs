using System.Collections.Generic;
using System.Linq;
using WebApplicationWizardAPi.Models.DTO;
using WebApplicationWizardAPi.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationWizardAPi.Models.DataManager
{
    public class AnswerDataManager : IDataRepository<Answer, AnswerDTO>
    {
        readonly WizardDBContext _wizardContext;

        public AnswerDataManager(WizardDBContext Context)
        {
            _wizardContext = Context;
        }

        public void Add(Answer entity)
        {
            _wizardContext.Answers.Add(entity);
            _wizardContext.SaveChanges();
        }
        public void Delete(Answer entity)
        {
            throw new NotImplementedException();
        }

        public Answer Get(long id)
        {
            _wizardContext.ChangeTracker.LazyLoadingEnabled = true;

            var user = _wizardContext.Answers.SingleOrDefault(x => x.Id == id);
            return user;
        }

        public IEnumerable<Answer> GetAll()
        {
            return _wizardContext.Answers
               .ToList();
        }

        public bool Check(long v)
        {
            throw new NotImplementedException();
        }

        public bool Check(string v)
        {
            throw new NotImplementedException();
        }


        public Answer GetDto(long id)
        {
            throw new NotImplementedException();
        }

        public AnswerDTO GetUser(string email, string pass)
        {
            throw new NotImplementedException();
        }

        public void Update(Answer entityToUpdate, Answer entity)
        {
            throw new NotImplementedException();
        }
    }
}
