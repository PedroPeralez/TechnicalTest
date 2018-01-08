using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TT.Context;
using TT.Models.BusinessEntities;

namespace TT.Repository
{
    public class UserRepository : Repository<User>
    {
        //this is a generic method for retrieving a single user based on the provided criteria, here it's only used
        //by the GetById method, but could be used for future hypothetical methods.
        public static User Get(Func<User, bool> Criteria)
        {

            return TTContext.SharedTTContext.Users.FirstOrDefault(Criteria);

        }

        public static User GetById(int Id)
        {
            return Get(x => x.Id == Id);
        }

        public static IEnumerable<User> GetAll()
        {
            return TTContext.SharedTTContext.Users;
        }

        public static User Insert(User User)
        {
            TTContext.SharedTTContext.Users.Add(User);
            TTContext.SharedTTContext.SaveChanges();
            return User;
            
        }

        public static User Update(User User)
        {
            TTContext.SharedTTContext.Entry(User).State = System.Data.Entity.EntityState.Modified;
            TTContext.SharedTTContext.SaveChanges();
            return User;
        }

        public static void Delete(int Id)
        {
            var User = GetById(Id);
            if (User == null) //checks if the User we are trhying to delete exists in database.
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ReasonPhrase = "User Id not found",
                    Content = new StringContent($"A User with Id = {Id} does not exist")
                });
            }
            TTContext.SharedTTContext.Users.Attach(User);
            TTContext.SharedTTContext.Users.Remove(User);
            TTContext.SharedTTContext.SaveChanges();
        }
    }
}