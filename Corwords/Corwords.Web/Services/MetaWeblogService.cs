using Corwords.Web.Core;
using Corwords.Web.Data;
using Corwords.Web.Models;
using Corwords.Web.Models.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using WilderMinds.MetaWeblog;

namespace Corwords.Web.Services
{
    public class CorMetaWeblogService : IMetaWeblogProvider
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOptionsSnapshot<GeneralSettings> _generalSettings;
        private readonly BlogManager _blogManager;

        private const string unauthorizedMessage = "The username and password combination supplied is invalid, or the user does not have access to this blog. Please check the credentials and try again.";
        private const string blogIdExceptionmessage = "The format of the blog ID was incorrect. Please check the value and try again.";
        private const string postIdExceptionMessage = "The format of the post ID was incorrect. Please check the value and try again.";

        public CorMetaWeblogService(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IOptionsSnapshot<GeneralSettings> generalSettings,
            CorwordsDbContext corwordsDbContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _generalSettings = generalSettings;
            _blogManager = new BlogManager(corwordsDbContext, generalSettings.Value);
        }

        public UserInfo GetUserInfo(string key, string username, string password)
        {
            LoginCheck(username, password);

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
            LoginCheck(username, password);
            return _blogManager.GetBlogs(username).Select(s => new BlogInfo { blogid = s.BlogId.ToString(), blogName = s.Name, url = s.BaseUrl }).ToArray();
        }

        public Post GetPost(string postid, string username, string password)
        {
            LoginCheck(username, password);
            throw new NotImplementedException();
        }

        public Post[] GetRecentPosts(string blogid, string username, string password, int numberOfPosts)
        {
            if (!int.TryParse(blogid, out int bId))
                throw new ArgumentException(blogIdExceptionmessage);

            LoginCheck(username, password, bId);

            /// todo Add Categories to the Recent Posts method
            return _blogManager.GetLatestPosts(bId, numberOfPosts).Select(s => new Post
            {
                postid = s.BlogPostId,
                title = s.Title,
                userid = s.Author,
                description = s.Body,
                permalink = s.Permalink,
                wp_slug = s.Slug,
                dateCreated = s.DateCreated
            }).ToArray();
        }

        // Returns the post ID
        public string AddPost(string blogid, string username, string password, Post post, bool publish)
        {
            if (!int.TryParse(blogid, out int bId))
                throw new ArgumentException(blogIdExceptionmessage);

            LoginCheck(username, password, bId);

            var blogPost = _blogManager.AddPost(bId, post.title, post.description, post.categories, post.dateCreated, username);

            return blogPost.BlogPostId.ToString();
        }

        public bool DeletePost(string key, string postid, string username, string password, bool publish)
        {
            if (!int.TryParse(key, out int bId))
                throw new ArgumentException(blogIdExceptionmessage);

            if (!int.TryParse(postid, out int pId))
                throw new ArgumentException(postIdExceptionMessage);

            LoginCheck(username, password, bId);

            return _blogManager.DeletePost(pId);
        }

        public bool EditPost(string postid, string username, string password, Post post, bool publish)
        {
            LoginCheck(username, password);
            throw new NotImplementedException();
        }

        public CategoryInfo[] GetCategories(string blogid, string username, string password)
        {
            if (!int.TryParse(blogid, out int bId))
                throw new ArgumentException(blogIdExceptionmessage);

            LoginCheck(username, password, bId);
            return _blogManager.GetBlogTags(bId).Select(s => new CategoryInfo { categoryid = s.TagId.ToString(), title = s.Title, description = s.Description, htmlUrl = "", rssUrl = "" }).ToArray();
        }

        public MediaObjectInfo NewMediaObject(string blogid, string username, string password, MediaObject mediaObject)
        {
            if (!int.TryParse(blogid, out int bId))
                throw new ArgumentException(blogIdExceptionmessage);

            LoginCheck(username, password, bId);
            throw new NotImplementedException();
        }

        public int AddCategory(string key, string username, string password, NewCategory category)
        {
            if (!int.TryParse(key, out int blogId))
                throw new ArgumentException(blogIdExceptionmessage);

            LoginCheck(username, password, blogId);

            var tag = _blogManager.AddTag(category.name, category.description);

            return _blogManager.AddBlogTag(blogId, tag);
        }

        private bool Login(string username, string password)
        {
            var signin = _signInManager.PasswordSignInAsync(username, password, false, false);
            signin.Wait();

            return signin.Result.Succeeded;
        }

        private void LoginCheck(string username, string password)
        {
            if (!Login(username, password))
                throw new UnauthorizedAccessException(unauthorizedMessage);
        }

        private void LoginCheck(string username, string password, int blogid)
        {
            LoginCheck(username, password);

            if (!_blogManager.ValidUserBlog(username, blogid))
                throw new UnauthorizedAccessException(unauthorizedMessage);
        }
    }
}