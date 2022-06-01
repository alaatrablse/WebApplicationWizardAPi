using System.Collections.Generic;
using System.Linq;
using WebApplicationWizardAPi.Models.DTO;
using WebApplicationWizardAPi.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationWizardAPi.Models.DataManager
{
    public class WizardDataDataManager : IDataRepository<WizardDatum, WizardDataDTO>
    {
        readonly WizardDBContext _wizardContext;

        public WizardDataDataManager(WizardDBContext Context)
        {
            _wizardContext = Context;
        }

        public void Add(WizardDatum entity)
        {
            _wizardContext.WizardData.Add(entity);
            _wizardContext.SaveChanges();
        }

        public bool Check(long v)
        {
            throw new NotImplementedException();
        }

        public bool Check(string v)
        {
            throw new NotImplementedException();
        }

        public void Delete(WizardDatum entity)
        {
            throw new NotImplementedException();
        }

        public WizardDatum Get(long id)
        {
            _wizardContext.ChangeTracker.LazyLoadingEnabled = true;

        
            var wizardData = _wizardContext.WizardData.SingleOrDefault(x => x.Id == id);
            /* var wizarddata = _wizardContext.WizardData.SingleOrDefault*/
            if (wizardData == null)
                return null;

            _wizardContext.Entry(wizardData)
                .Collection(b => b.Answers)
                .Load();
            
            return wizardData;
        }

        public IEnumerable<WizardDatum> GetAll()
        {
            return _wizardContext.WizardData.ToList();
        }

        public WizardDatum GetDto(long id)
        {
            throw new NotImplementedException();
        }

        public WizardDataDTO GetUser(string email, string pass)
        {
            throw new NotImplementedException();
        }

        public void Update(WizardDatum entityToUpdate, WizardDatum entity)
        {
            throw new NotImplementedException();
        }
    }
}
