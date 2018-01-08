using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using TT.Models.BusinessEntities;
using TT.Models.DTOs;
using TT.Services;

namespace TT.Controllers
{
    //The controller handles client requests, but no business logic should be present on this level.

    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        //I have provided an alternative endpoint for all methods, more in accordance with REST programming,
        //since HTTP verbs already state what we are going to do in each case. Still, as an alternative, I have kept
        //the provided endpoints. As a rule of thumb, there should be no actions in the endpoint paths, 
        //and they should be resource oriented.
        [HttpGet]
        [Route("getall")]
        [Route("")]
        public IEnumerable<UserDataDTO> GetAll()
        {
            return UserService.GetAll().Select(x => new UserDataDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Birthdate = x.Birthdate,
            })
            .ToList();
        }

        [HttpGet]
        [Route("get/{id}")]
        [Route("{id}")]
        public UserDataDTO Get(int id)
        {
            try
            {
                var User = UserService.GetById(id);
                return new UserDataDTO()
                {
                    Id = User.Id,
                    Name = User.Name,
                    Birthdate = User.Birthdate,
                };
            }
            catch(HttpResponseException Ex) //catches an exception from a deeper layer, if there were problems retrieving the user
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Route("create")]
        [Route("")]
        public User Create([FromBody] NewUserDTO NewUserData)
        {
            if (!NewUserData.Validate())
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ReasonPhrase = "Trying to Create invalid User",
                    Content = new StringContent("The User is not valid and can't be created")
                });
            }
            try
            {
                var NewUser = new User()
                {
                    Name = NewUserData.Name,
                    Birthdate = NewUserData.Birthdate,
                };
                return UserService.Create(NewUser);
            }
            catch (HttpResponseException Ex) //catches an exception from a deeper layer.
            {
                throw Ex;
            }
        }

        // In this case I have created an alternative method with a different endpoint and input parameters,
        //so the user Id is provided with the http header, this I did in order to keep consistency with other methods that
        //work in the same way, but I have kept anyway the alternative method with the specifications provided, although I 
        //personally think the second option is best.
        [HttpPut]
        [Route("update")]
        public User Update([FromBody] UpdateUserDTO UpdateUserData)
        {
            if (!UpdateUserData.Validate())
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ReasonPhrase = "Trying to update invalid User",
                    Content = new StringContent("The User is not valid and can't be updated")
                });
            }
            try
            {
                var User = new User()
                {
                    Id = UpdateUserData.Id,
                    Name = UpdateUserData.Name,
                    Birthdate = UpdateUserData.Birthdate,

                };
                return UserService.Update(User);
            }
            catch (HttpResponseException Ex) //catches an exception from a deeper layer
            {
                throw Ex;
            }
        }

        [HttpPut]
        [Route("{id}")]
        public User Update(int Id, [FromBody] UpdateUserAltDTO UpdateUserData)
        {
            if (!UpdateUserData.Validate())
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ReasonPhrase = "Trying to update invalid User",
                    Content = new StringContent("The User is not valid and can't be updated")
                });
            }
            try
            {
                var User = new User()
                {
                    Id = Id,
                    Name = UpdateUserData.Name,
                    Birthdate = UpdateUserData.Birthdate,

                };
                return UserService.Update(User);
            }
            catch (HttpResponseException Ex) //catches an exception from a deeper layer
            {
                throw Ex;
            }
        }

        [HttpDelete]
        [Route("remove/{id}")]
        [Route("{id}")]
        public void DeleteUser(int id)
        {
            try
            {
                UserService.Delete(id);
            }
            catch (HttpResponseException Ex) //catches an exception from a deeper layer.
            {
                throw Ex;
            }
        }
    }
}