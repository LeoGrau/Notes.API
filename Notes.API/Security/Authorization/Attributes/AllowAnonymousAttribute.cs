using Microsoft.AspNetCore.Mvc.Filters;

namespace Notes.API.Security.Authorization.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class AllowAnonymousAttribute : Attribute
{
}