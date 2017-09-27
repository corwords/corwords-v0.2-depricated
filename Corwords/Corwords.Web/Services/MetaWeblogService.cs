using Corwords.Web.Core;
using Corwords.Web.Data;
using Corwords.Web.Models;
using Corwords.Web.Models.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using WilderMinds.MetaWeblog;

namespace Corwords.Web.Services
{
    public class CorMetaWeblogService : IMetaWeblogProvider
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOptionsSnapshot<GeneralSettings> _generalSettings;
        private readonly BlogManager _blogManager;

        public CorMetaWeblogService(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IOptionsSnapshot<GeneralSettings> generalSettings,
            CorwordsDbContext corwordsDbContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _generalSettings = generalSettings;
            _blogManager = new BlogManager(corwordsDbContext);
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
                url = _generalSettings.Value.WebsiteUrl
            };
        }

        public BlogInfo[] GetUsersBlogs(string key, string username, string password)
        {
            if (!Login(username, password))
                throw new UnauthorizedAccessException("The username and password combination supplied is invalid. Please check the credentials and try again.");

            return _blogManager.GetBlogs(username).Select(s => new BlogInfo() { blogid = s.BlogId.ToString(), blogName = s.Name, url = s.BaseUrl }).ToArray();
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
            var signin = _signInManager.PasswordSignInAsync(username, password, false, false);
            signin.Wait();

            return signin.Result.Succeeded;
        }
    }
}