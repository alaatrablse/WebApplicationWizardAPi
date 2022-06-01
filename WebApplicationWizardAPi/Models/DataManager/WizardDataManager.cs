using System.Collections.Generic;
using System.Linq;
using WebApplicationWizardAPi.Models.DTO;
using WebApplicationWizardAPi.Models.Repository;
using Microsoft.EntityFrameworkCore;


namespace WebApplicationWizardAPi.Models.DataManager
{
    public class WizardDataManager : IDataRepository<Wizard, WizardDTO>
    {
        readonly WizardDBContext _wizardContext;

        public WizardDataManager(WizardDBContext Context)
        {
            _wizardContext = Context;
        }

        public void Add(Wizard entity)
        {
            _wizardContext.Wizards.Add(entity);
            _wizardContext.SaveChanges();
        }

        public void Delete(Wizard entity)
        {
            _wizardContext.Remove(entity);
            _wizardContext.SaveChanges();
        }

        public Wizard Get(long id)
        {
            _wizardContext.ChangeTracker.LazyLoadingEnabled = true;

            var pages = _wizardContext.Pages.SingleOrDefault(x => x.WizardId == id);
            var wizard = _wizardContext.Wizards.SingleOrDefault(x => x.Id == id);
           /* var wizarddata = _wizardContext.WizardData.SingleOrDefault*/
            if (wizard == null)
                return null;

            _wizardContext.Entry(wizard)
                .Collection(b => b.Pages)
                .Load();
            _wizardContext.Entry(pages)
               .Collection(b => b.WizardData)
               .Load();
            return wizard;
        }

        public IEnumerable<Wizard> GetAll()
        {
            //return _wizardContext.Wizards.Include(page => page.Pages).ThenInclude(wizard => wizard.WizardData).ThenInclude(w => w.Answers).ToList();
            return _wizardContext.Wizards.ToList();
        }
        public bool Check(long v)
        {
            var wizard = _wizardContext.Wizards.SingleOrDefault(x => x.Hashnum == v);
            if (wizard == null)
                return false;
            return true;
        }
        public Wizard GetDto(long id)
        {
            var wizard = _wizardContext.Wizards.SingleOrDefault(x => x.Hashnum == id);
            if (wizard == null)
                return null;

            _wizardContext.Entry(wizard)
                .Collection(b => b.Pages)
                .Query().Include(c => c.WizardData).ThenInclude(a => a.Answers).Load();
        
            return wizard;
        }

        public WizardDTO GetUser(string email, string pass)
        {
            throw new NotImplementedException();
        }

        public void Update(Wizard entityToUpdate, Wizard entity)
        {
            throw new NotImplementedException();
        }

        public bool Check(string v)
        {
            throw new NotImplementedException();
        }
    }
}
