using BookStore.Models;
using BookStore.Models.Data;
using BookStore.Models.ViewModels;
using BookStore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Repositories
{
    public class UserRepository
    {
        public UserModel RegisterUser(RegisterUserRequest user)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                //Add user to database
                BookStore.Models.Data.User objUser = new BookStore.Models.Data.User();
                objUser.Firstname = user.FirstName;
                objUser.Lastname = user.LastName;
                objUser.Email = user.Email;
                objUser.Roleid = Convert.ToInt32(user.RoleId);
                objUser.Password = user.Password;
                db.Users.Add(objUser);
                db.SaveChanges();

                //Prepare and return response 
                UserModel userResult = new UserModel();
                userResult.Id = objUser.Id;
                userResult.FirstName = objUser.Firstname;
                userResult.LastName = objUser.Lastname;
                userResult.Email = objUser.Email;
                userResult.Password = objUser.Password;
                userResult.RoleId = objUser.Roleid;
                return userResult;
            }
        }

        public UserModel Login(LoginRequest loginRequest)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                User user = db.Users.Where(x => x.Email == loginRequest.Email && x.Password == loginRequest.Password ).FirstOrDefault();

                if (user == null)
                {
                    throw new Exception(Messages.InvalidCredentialsMessage);
                }
                else
                {
                    UserModel userResult = new UserModel();
                    userResult.Id = user.Id;
                    userResult.FirstName = user.Firstname;
                    userResult.LastName = user.Lastname;
                    userResult.Email = user.Email;
                    userResult.Password = user.Password;
                    userResult.RoleId = user.Roleid;
                    return userResult;
                }
            }
        }

        #region UserMaster
        public User GetUserById(int id)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                User user = db.Users.Where(x => x.Id == id).FirstOrDefault();

                if (user == null)
                {
                    throw new Exception(Messages.InvalidCredentialsMessage);
                }
                else
                {
                    return user;
                }
            }
        }

        public GetUserModel getUserModel(int id)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                GetUserModel getUser = new GetUserModel();
                User user = db.Users.Where(x => x.Id == id).FirstOrDefault();

                if (user == null)
                {
                    throw new Exception(Messages.InvalidCredentialsMessage);
                }
                else
                {
                    getUser.Id = user.Id;
                    getUser.Firstname = user.Firstname;
                    getUser.Lastname = user.Lastname;
                    getUser.Email = user.Email;
                    getUser.Password = user.Password;

                    Role role = db.Roles.Where(r => r.Id == user.Roleid).FirstOrDefault();
                    getUser.Role = role.Name;
                    return getUser;
                }
            }
        }

        public User UpdateUser(UserModel user, User dbUser)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                dbUser.Firstname = user.FirstName;
                dbUser.Email = user.Email;
                dbUser.Lastname = user.LastName;
                dbUser.Roleid = user.RoleId;
                db.Users.Update(dbUser);
                db.SaveChanges();
                return dbUser;
            }
        }

        public User Delete(int userId)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                User user= db.Users.FirstOrDefault(c => c.Id == userId);
                if (user == null)
                    throw new Exception($"User not found with Id {userId}");

                db.Users.Remove(user);
                db.SaveChanges();
                return user;
            }
        }

        public BaseList<GetUserModel> GetAllUsers(int pageIndex, int pageSize, string keyword)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                var query = db.Users.AsQueryable();
                BaseList<GetUserModel> result = new BaseList<GetUserModel>();
                List<GetUserModel> getUsers = new List<GetUserModel>();
                GetUserModel getUser = null;
                Role role = null;
                result.TotalRecords = query.Count();
                if (pageSize != 0)
                {
                    keyword = keyword != null ? keyword : string.Empty;
                    //Fetch all the records where keyword is part of Firstname or Lastname or Email. Then skip first {{pageIndex * pageSize}} records and take next {{pageSize}} records.
                    query = query.Where(x => x.Firstname.ToLower().Contains(keyword.ToLower()) || x.Lastname.Contains(keyword) || x.Email.Contains(keyword)).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    
                }

                foreach(User user in query.ToList())
                {
                    getUser = new GetUserModel();
                    getUser.Id = user.Id;
                    getUser.Firstname = user.Firstname;
                    getUser.Lastname = user.Lastname;
                    getUser.Email = user.Email;
                    getUser.Password = user.Password;

                    role = db.Roles.Where(r => r.Id == user.Roleid).FirstOrDefault();
                    getUser.Role = role.Name;

                    getUsers.Add(getUser);
                }

                result.TotalRecords = query.Count();
                result.Records = getUsers;
                return result;
            }
        }

        public BaseList<Role> GetAllRoles()
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                var query = db.Roles.AsQueryable();
                BaseList<Role> result = new BaseList<Role>();
                result.TotalRecords = query.Count();
                result.Records = query.ToList();
                return result;
            }
        }
        #endregion
    }
}
