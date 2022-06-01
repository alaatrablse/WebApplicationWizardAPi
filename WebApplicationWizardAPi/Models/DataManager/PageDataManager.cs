using System.Collections.Generic;
using System.Linq;
using WebApplicationWizardAPi.Models.DTO;
using WebApplicationWizardAPi.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationWizardAPi.Models.DataManager
{
    public class PageDataManager : IDataRepository<Page, PageDTO>
    {
        readonly WizardDBContext _wizardContext;

        public PageDataManager(WizardDBContext Context)
        {
            _wizardContext = Context;
        }

        public IEnumerable<Page> GetAll()
        {
            return _wizardContext.Pages
                .ToList();
        }

        public void Add(Page entity)
        {
            /*_wizardContext.Pages.Add(entity);
            _wizardContext.SaveChanges();*/
            throw new NotImplementedException();
        }

        public void Delete(Page entity)
        {
            throw new NotImplementedException();
        }

        public Page Get(long id)
        {
            _wizardContext.ChangeTracker.LazyLoadingEnabled = true;

            var page = _wizardContext.Pages.SingleOrDefault(b => b.Id == id);

            if (page == null)
                return null;

            /*_wizardContext.Entry(page)
                .Reference(b => b.Wizard)
                .Load();*/

            return page;
        }

        public Page GetDto(long id)
        {
            throw new NotImplementedException();
        }

        public void Update(Page entityToUpdate, Page entity)
        {
            throw new NotImplementedException();
        }

        public PageDTO GetUser(string email, string pass)
        {
            throw new NotImplementedException();
        }

        public bool Check(long v)
        {
            throw new NotImplementedException();
        }

        public bool Check(string v)
        {
            throw new NotImplementedException();
        }
    }
}
