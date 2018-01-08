using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TT.Models.BusinessEntities;
using TT.Repository;

namespace TT.Services
{
    //The service is the layer were we keep our business logic. No business logic should exist outside this layer.
    public class UserService
    {
        public static IEnumerable<User> GetAll()
        {
            return UserRepository.GetAll();
        }

        public static User GetById(int Id)
        {
            var UserQuery = UserRepository.GetById(Id);
            if (UserQuery == null) //checks if a valid user was recovered from the database.
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ReasonPhrase = "User Id not found",
                    Content = new StringContent($"A User with Id = {Id} does not exist")
                });
            }
            return UserQuery;
        }

        public static User Create(User User)
        {
            if (!User?.Validate() ?? true) //checks if the received User is not null and has valid content.
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ReasonPhrase = "Not valid User",
                    Content = new StringContent("The User definition is not valid")
                });
            }
            return UserRepository.Insert(User);
        }

        public static User Update(User User)
        {
            if (!User?.Validate() ?? true) //checks if the received User is not null and has valid content.
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ReasonPhrase = "User information not valid",
                    Content = new StringContent("The provided User information is not valid")
                });
            }
            var OldUser = GetById(User.Id);
            OldUser.Name = User.Name;
            OldUser.Birthdate = User.Birthdate;
            if (!OldUser.Validate()) //checks if the updated user has valid format.
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ReasonPhrase = "Trying to update invalid User",
                    Content = new StringContent("The User is not valid and can't be updated")
                });
            }

            return UserRepository.Update(OldUser);
        }

        public static void Delete(int Id)
        {
            try
            {
                UserRepository.Delete(Id);
            }
            catch (HttpResponseException Ex) //catches exception from a deeper layer.
            {
                throw Ex;
            }
        }
    }
}