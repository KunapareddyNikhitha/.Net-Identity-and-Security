using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Flipkart.Data;
using Flipkart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Flipkart.Authorization;

namespace Flipkart.Pages.Products
{
    public class CreateModel : DI_BasePageModel
    {
       
        public CreateModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager):base(context,authorizationService,userManager)
        {
            
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Product.CreatorId = UserManager.GetUserId(User);

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, Product, ProductOperations.Create);

            if(isAuthorized.Succeeded == false)
            {
                return Forbid();
            }

            Context.Product.Add(Product);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
