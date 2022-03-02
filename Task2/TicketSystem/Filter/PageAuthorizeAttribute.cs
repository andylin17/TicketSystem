using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TicketSystem.Models;

namespace TicketSystem.Filter
{
	public class PageAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
	{
        public WebFunction Function { get; set; }

        public PageAuthorizeAttribute(WebFunction function)
        {
            Function = function;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var json = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "p")?.Value;
            if (string.IsNullOrWhiteSpace(json))
            {
                context.Result = new ForbidResult();
                return;
            }

            var permissions = JsonSerializer.Deserialize<int[]>(json);

            if (!CheckAuth(permissions))
                context.Result = new ForbidResult();
        }

        private bool CheckAuth(IEnumerable<int> permissions)
        {
            return permissions.Any(x => Function.HasFlag((WebFunction)x));
        }
    }
}
