using BookStore.Models;
using BookStore.Models.Data;
using BookStore.Models.ViewModels;
using BookStore.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpGet]
        [Route("list")]
        public BaseList<GetUserModel> GetUsers(int pageIndex = 1, int pageSize = 10, string keyword = "")
        {
            UserRepository repo = new UserRepository();
            BaseList<GetUserModel> users = repo.GetAllUsers(pageIndex, pageSize, keyword);
            return users;
        }

        [HttpGet]
        [Route("{id}")]
        public GetUserModel GetUser([FromRoute]int id)
        {
            UserRepository repo = new UserRepository();
            GetUserModel user = repo.getUserModel(id);
            return user;
        }

        [HttpPut]
        public UserModel Update(UserModel user)
        {
            UserRepository repo = new UserRepository();
            User dbUser = repo.GetUserById(user.Id);
            return new UserModel(repo.UpdateUser(user, dbUser));
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public UserModel Delete([FromRoute]int id)
        {
            UserRepository repo = new UserRepository();
            return new UserModel(repo.Delete(id));
        }

        [HttpGet]
        [Route("Roles")]
        public BaseList<RoleModel> GetRoles()
        {
            UserRepository repo = new UserRepository();
            BaseList<Role> roles = repo.GetAllRoles();
            return new BaseList<RoleModel> { TotalRecords = roles.TotalRecords, Records = roles.Records.Select(role => new RoleModel(role)).ToList() };
        }
    }
}
