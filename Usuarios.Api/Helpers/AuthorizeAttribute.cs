using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Usuarios.Api.Helpers
{
    /// <summary>
    /// Atributo para autorizar la ejecución de una clase o un método
    /// Si el contexto contiene el Item["UserId"], se permite la ejecución
    /// de lo contrario se retorna un erro 401 Unauthorized
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userId = context.HttpContext.Items["UserId"];
            var header = context.HttpContext.Request.Headers.Authorization;

            if (string.IsNullOrEmpty(header))
            {
                context.Result = new JsonResult(new { message = "El Cabecero Authorization es requerido" }) { StatusCode = StatusCodes.Status400BadRequest };
            }
            else if (userId == null) 
            {
                context.Result = new JsonResult(new { message = "Acceso no autorizado" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
