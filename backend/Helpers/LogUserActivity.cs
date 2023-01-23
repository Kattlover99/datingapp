using DatingApp.Extensions;
using DatingApp.Interface;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            var userId = resultContext.HttpContext.User.GetUserId();

            var uow = resultContext.HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();

            var parsedId = userId.IsNullOrEmpty() ? "1" : userId;

            var user = await uow.UserRepository.GetUserByIdAsync(int.Parse(parsedId));
            user.LastActive = DateTime.UtcNow;

            await uow.Complete();
        }
    }
}
