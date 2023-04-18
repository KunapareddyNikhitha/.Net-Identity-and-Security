using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Flipkart.Data;
using Flipkart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Flipkart.Pages.Products
{
    [AllowAnonymous]
    public class IndexModel : DI_BasePageModel
    {
       

        public IndexModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager) : base(context, authorizationService, userManager)
        {
            
        }

        public IList<Product> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (Context.Product != null)
            {
                var currentUserID = UserManager.GetUserId(User);

                Product = await Context.Product.Where(i => i.CreatorId == currentUserID).ToListAsync();
            }
        }
    }
}
