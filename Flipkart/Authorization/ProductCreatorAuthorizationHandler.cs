using Flipkart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Flipkart.Authorization
{
    public class ProductCreatorAuthorizationHandler:
       AuthorizationHandler<OperationAuthorizationRequirement,Product>
    {
        UserManager<IdentityUser> _userManager;
        public ProductCreatorAuthorizationHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager; 
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            Product product)
        {
            if (context.User == null || product == null)
                return Task.CompletedTask;
            if(requirement.Name!=Constants.CreateOperationName &&
                requirement.Name!=Constants.ReadOperationName &&
                requirement.Name !=Constants.DeleteOperationName &&
                requirement.Name != Constants.UpdateOperationName &&
                requirement.Name != Constants.ApprovedOperationName &&
                requirement.Name != Constants.ReadOperationName)
            {
                return Task.CompletedTask;
            }

            if (product.CreatorId == _userManager.GetUserId(context.User))
                context.Succeed(requirement);

            return Task.CompletedTask;
          
        }
    }
}
