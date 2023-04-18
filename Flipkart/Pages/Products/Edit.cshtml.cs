using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Flipkart.Data;
using Flipkart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Flipkart.Authorization;

namespace Flipkart.Pages.Products
{
    public class EditModel : DI_BasePageModel
    {
        
        public EditModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager): base(context, authorizationService, userManager)
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

            Product =  await Context.Product.FirstOrDefaultAsync(m => m.Id == id);

            if (Product == null)
            {
                return NotFound();
            }
         
            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, Product, ProductOperations.Update);
            if (isAuthorized.Succeeded == false)
            {
                return Forbid();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
          

            var product = await Context.Product.AsNoTracking().SingleOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            Product.CreatorId = product.CreatorId;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, Product, ProductOperations.Update);
            if (isAuthorized.Succeeded == false)
            {
                return Forbid();
            }

            Context.Attach(Product).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
          return (Context.Product?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
