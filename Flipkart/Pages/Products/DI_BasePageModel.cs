using Flipkart.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Flipkart.Pages.Products
{
    public class DI_BasePageModel:PageModel
    {
        protected ApplicationDbContext Context { get;  }
        protected IAuthorizationService AuthorizationService { get; }
        protected UserManager<IdentityUser> UserManager { get; }
        public DI_BasePageModel(ApplicationDbContext context,
            IAuthorizationService authorizationService, 
            UserManager<IdentityUser> userManager)
        {
            Context = context;
            AuthorizationService = authorizationService;
            UserManager = userManager;
        }
    }
}
