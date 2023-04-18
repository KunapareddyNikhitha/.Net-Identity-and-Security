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
using Flipkart.Authorization;

namespace Flipkart.Pages.Products
{
    public class DetailsModel : DI_BasePageModel
    {
       

        public DetailsModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager) : base(context, authorizationService, userManager)
        {
           
        }

      public Product Product { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || Context.Product == null)
            {
                return NotFound();
            }

            Product = await Context.Product.FirstOrDefaultAsync(m => m.Id == id);
            if (Product == null)
            {
                return NotFound();
            }
            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, Product, ProductOperations.Read);
            if (isAuthorized.Succeeded == false)
            {
                return Forbid();
            }
            return Page();
        }
    }
}
