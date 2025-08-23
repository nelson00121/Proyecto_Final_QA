using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProFinQA.Data;
using ProFinQA.Extensions;

namespace ProFinQA.Attributes;

public class RoleRequirementAttribute : Attribute, IAsyncActionFilter
{
    private readonly string[] _requiredRoles;

    public RoleRequirementAttribute(params string[] requiredRoles)
    {
        _requiredRoles = requiredRoles;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var dbContext = context.HttpContext.RequestServices.GetRequiredService<BodegaContext>();
        var user = context.HttpContext.User;

        if (!user.Identity?.IsAuthenticated ?? true)
        {
            context.Result = new RedirectToPageResult("/Login");
            return;
        }

        var hasRequiredRole = await user.HasAnyRoleAsync(dbContext, _requiredRoles);
        
        if (!hasRequiredRole)
        {
            context.Result = new ForbidResult();
            return;
        }

        await next();
    }
}