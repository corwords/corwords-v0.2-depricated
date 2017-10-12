using Corwords.Web.Core;
using Corwords.Web.Core.Configuration;
using Corwords.Web.Core.MVC;
using Corwords.Web.Core.Types;
using Corwords.Web.Data;
using Corwords.Web.Models;
using Corwords.Web.Models.Configuration;
using Corwords.Web.Models.CoreViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Corwords.Web.Controllers
{
    [IsAppFirstRun]
    public class InitController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IWritableOptions<GeneralSettings> _generalSettings;
        private readonly CorwordsDbContext _corwordsDbContext;
        private const string roleName = "Administrators";

        public InitController(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IWritableOptions<GeneralSettings> generalSettings,
            CorwordsDbContext corwordsDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _generalSettings = generalSettings;
            _corwordsDbContext = corwordsDbContext;
        }

        public IActionResult Success()
        {
            return View();
        }

        public async Task<IActionResult> Begin(InitViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // Add the blog
                var blogManager = new BlogManager(_corwordsDbContext, _generalSettings.Value);
                var blogId = blogManager.CreateBlog(vm.BlogName, "/" + vm.BlogUrl, vm.EmailAddress);

                // Add the route
                var routeManager = new RouteManager(_corwordsDbContext);
                var routeId = routeManager.AddRoute(vm.BlogUrl, DynamicRouteType.Blog);

                // Next, add a default role called Administrators
                if (ModelState.ErrorCount == 0 && blogId > 0)
                {
                    var roleResult = await _roleManager.CreateAsync(new ApplicationRole { Name = roleName });
                    if (!roleResult.Succeeded)
                        ModelState.AddModelError("EmailAddress", roleResult.Errors.First().Description);
                }

                // Next, Add the user
                if (ModelState.ErrorCount == 0)
                {
                    var user = new ApplicationUser { UserName = vm.EmailAddress, Email = vm.EmailAddress, FirstName = vm.FirstName, LastName = vm.LastName };
                    var userResult = await _userManager.CreateAsync(user, vm.Password);
                    if (!userResult.Succeeded)
                        ModelState.AddModelError("EmailAddress", userResult.Errors.First().Description);
                }

                // Next, Add the user to the Administrators role
                if (ModelState.ErrorCount == 0)
                {
                    var firstUser = await _userManager.FindByEmailAsync(vm.EmailAddress);
                    var userRoleResult = await _userManager.AddToRoleAsync(firstUser, roleName);
                    if (!userRoleResult.Succeeded)
                        ModelState.AddModelError("EmailAddress", userRoleResult.Errors.First().Description);
                }

                // Finally, if there were no errors, flip the flag for the init controller
                if (ModelState.ErrorCount == 0)
                {
                    _generalSettings.Update(vals =>
                    {
                        vals.FirstRun = false;
                        vals.WebsiteUrl = vm.WebsiteUrl;
                        vals.SiteName = vm.SiteName;
                        vals.SiteTheme = vm.SiteTheme;
                    });

                    // Redirect to home page
                    vm.IsSaved = true;
                    return View(vm);
                } else
                {
                    /// TODO We need to clean-up the saved data per Issue #2 - https://github.com/corwords/corwords/issues/2
                }
            }

            var currentUrl = Request.Scheme + "://" + Request.Host;
            vm.ResolveNulls(_generalSettings.Value, currentUrl);

            return View(vm);
        }
    }
}