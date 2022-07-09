using BookStore.Models;
using BookStore.Models.ViewModels;
using BookStore.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;

namespace BookStore.Controllers
{
    [Route("api/BookStore")]
    [ApiController]
    public class BookStoreController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public BookStoreController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Register

        /// <summary>
        /// Registers user to database.
        /// </summary>
        /// <param name="user">user</param>
        /// <returns>RegisterUserResponse</returns>
        [HttpPost("RegisterUser")]
        public IActionResult RegisterUser([FromBody] RegisterUserRequest user)
        {
            UserModel registerUserResponse = new UserModel();
            try
            {
                UserRepository userRepo = new UserRepository();
                var userResult = userRepo.RegisterUser(user);
                ResultStateWithModel<UserModel> resultState = new ResultStateWithModel<UserModel>();
                resultState.Data = userResult;
                return StatusCode((int)HttpStatusCode.OK, resultState);
            }
            catch (Exception ex)
            {
                ResultState resultState = new ResultState(Messages.GeneralExceptionCode, "Failed", ex.GetBaseException().Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, resultState);
            }

        }

        #endregion

        #region Login

        /// <summary>
        /// Validates login credentials
        /// </summary>
        /// <param name="loginRequest">loginRequest</param>
        /// <returns>LoginResponse</returns>
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                UserRepository userRepo = new UserRepository();
                var userResult = userRepo.Login(loginRequest);
                ResultStateWithModel<UserModel> resultState = new ResultStateWithModel<UserModel>();
                resultState.Data = userResult;
                return StatusCode((int)HttpStatusCode.OK, resultState);
            }
            catch (Exception ex)
            {
                ResultState resultState = new ResultState(Messages.GeneralExceptionCode, "Failed", ex.GetBaseException().Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, resultState);
            }
        }

        #endregion
    }
}