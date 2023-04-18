using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Flipkart.Data;
using Flipkart.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Flipkart.Authorization;

namespace Flipkart.Pages.Products
{
    public class DeleteModel : DI_BasePageModel
    {
        

        public DeleteModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager) : base(context, authorizationService, userManager)
        {
            
        }

        [BindProperty]
      public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || Context.Product == null)
            {
                return NotFound();
            }

            var product = await Context.Product.FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            else 
            {
                Product = product;
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, Product, ProductOperations.Delete);

            if(isAuthorized.Succeeded == false)
            {
                return Forbid();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || Context.Product == null)
            {
                return NotFound();
            }
            Product = await Context.Product.FindAsync(id);
            if(Product == null) return NotFound();
          
            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, Product, ProductOperations.Delete);

            if (isAuthorized.Succeeded == false)
            {
                return Forbid();
            }
           
                Context.Product.Remove(Product);
                await Context.SaveChangesAsync();
            

            return RedirectToPage("./Index");
        }
    }
}
