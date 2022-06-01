using System.Collections.Generic;
using System.Linq;
using WebApplicationWizardAPi.Models.DTO;
using WebApplicationWizardAPi.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationWizardAPi.Models.DataManager
{
    public class UserDataManager : IDataRepository<User, UserDTO>
    {
        readonly WizardDBContext _wizardContext;

        public UserDataManager(WizardDBContext Context)
        {
            _wizardContext = Context;
        }


        public void Add(User entity)
        {
            _wizardContext.Users.Add(entity);
            _wizardContext.SaveChanges();
        }

        public void Delete(User entity)
        {
            _wizardContext.Remove(entity);
            _wizardContext.SaveChanges();
        }

        public User Get(long id)
        {
            _wizardContext.ChangeTracker.LazyLoadingEnabled = true;

            var user = _wizardContext.Users.SingleOrDefault(x => x.Id == id);
            /*var wizard = _wizardContext.Wizards.SingleOrDefault(x => x.UserId == id);*/
            if (user == null)
                return null;

           /* if (wizard != null)
            {
                _wizardContext.Entry(wizard)
                .Collection(c => c.Pages)
                .Load();
            }
*/
            _wizardContext.Entry(user)
                .Collection(b => b.Wizards)
                .Load();


            return user;
        }
        //////////////////USE///////
        public UserDTO GetUser(string email,string pass)
        {
            _wizardContext.ChangeTracker.LazyLoadingEnabled = true;

            using (var context = new WizardDBContext())
            {
                var user = context.Users
                    .SingleOrDefault(b => b.Email == email && b.Password == pass);

                return UserDTOMapper.MapToDto(user);
            }
        }

        public IEnumerable<User> GetAll()
        {
            /*return _wizardContext.Users.Include(user => user.Wizards)
                    .ThenInclude(wizard => wizard.Pages)
                    .ThenInclude(Page => Page.WizardData)
                    .ThenInclude(wiardData => wiardData.Answers)
                    .ToList();*/
            return _wizardContext.Users.Include(user => user.Wizards);
        }

        public User GetDto(long id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(User entityToUpdate, User entity)
        {
            entityToUpdate = _wizardContext.Users
                .Include(a => a.Wizards)
                .Single(b => b.Id == entityToUpdate.Id);
            
            entityToUpdate.Name = entity.Name;
            entityToUpdate.Email = entity.Email;
            entityToUpdate.Password = entity.Password;
            entityToUpdate.Type = entity.Type;

           /* var deletedUsers = entityToUpdate.Wizards.Except(entity.Wizards).ToList();
            var addedUsers = entity.Wizards.Except(entityToUpdate.Wizards).ToList();

            foreach (var item in addedUsers)
            {
                _wizardContext.Entry(addedUsers).State = EntityState.Added;
            }*/
            _wizardContext.SaveChanges();
        }

        public bool Check(long v)
        {
            throw new NotImplementedException();
        }

        public bool Check(string v)
        {
            var user = _wizardContext.Users.SingleOrDefault(x => x.Email == v);
            if (user == null)
                return false;
            return true;
        }
    }
}
