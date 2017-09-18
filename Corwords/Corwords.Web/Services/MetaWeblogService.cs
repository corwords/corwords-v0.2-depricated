using Corwords.Web.Models;
using Microsoft.AspNetCore.Identity;
using System;
using WilderMinds.MetaWeblog;

namespace Corwords.Web.Services
{
    public class CorMetaWeblogService : IMetaWeblogProvider
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public CorMetaWeblogService(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public UserInfo GetUserInfo(string key, string username, string password)
        {
            if (!Login(username, password))
                throw new UnauthorizedAccessException("The username and password combination supplied is invalid. Please check the credentials and try again.");

            var mgr = _userManager.FindByNameAsync(username);
            mgr.RunSynchronously();

            var appUser = mgr.Result;

            return new UserInfo()
                        {
                            email = appUser.Email,
                            firstname = appUser.FirstName,
                            lastname = appUser.LastName,
                            nickname = appUser.FirstName,
                            url = ""
                        };
        }

        public BlogInfo[] GetUsersBlogs(string key, string username, string password)
        {
            throw new NotImplementedException();
        }

        public Post GetPost(string postid, string username, string password)
        {
            throw new NotImplementedException();
        }

        public Post[] GetRecentPosts(string blogid, string username, string password, int numberOfPosts)
        {
            throw new NotImplementedException();
        }

        public string AddPost(string blogid, string username, string password, Post post, bool publish)
        {
            throw new NotImplementedException();
        }

        public bool DeletePost(string key, string postid, string username, string password, bool publish)
        {
            throw new NotImplementedException();
        }

        public bool EditPost(string postid, string username, string password, Post post, bool publish)
        {
            throw new NotImplementedException();
        }

        public CategoryInfo[] GetCategories(string blogid, string username, string password)
        {
            throw new NotImplementedException();
        }

        public MediaObjectInfo NewMediaObject(string blogid, string username, string password, MediaObject mediaObject)
        {
            throw new NotImplementedException();
        }

        public int AddCategory(string key, string username, string password, NewCategory category)
        {
            throw new NotImplementedException();
        }

        private bool Login(string username, string password)
        {
            var result = _signInManager.PasswordSignInAsync(username, password, false, false);
            result.RunSynchronously();

            return result.Result.Succeeded;
        }
    }
}