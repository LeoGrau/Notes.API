using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Notes.API.Security.Authorization.Attributes;
using Notes.API.Security.Domain.Services;
using Notes.API.Security.Domain.Services.Communication;
using Notes.API.Security.Models;
using Notes.API.Security.Resources.Show;
using Swashbuckle.AspNetCore.Annotations;

namespace Notes.API.Security.Interface.Rest;

[Authorize]
[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Testing")]
public class UserController : ControllerBase
{
    private IUserService _userService { get; set; }
    private IMapper _mapper { get; set; }

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<UserResource>> GetAllUsers()
    {
        var result = await _userService.ListAllUsersAsync();
        var mappedResult = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(result); 
        return mappedResult;
    }
    
    [Authorization.Attributes.AllowAnonymous]
    [HttpPost("sign-in")]
    public async Task<IActionResult> Authenticate(AuthRequest authRequest)
    {
        var response = await _userService.AuthenticateAsync(authRequest);
        return Ok(response);
    }

    [Authorization.Attributes.AllowAnonymous]
    [HttpPost("sign-up")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        await _userService.RegisterAsync(registerRequest);
        return Ok(new { message = "Registration successful" });
    }

}