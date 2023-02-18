using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Notes.API.Security.Domain.Services;
using Notes.API.Security.Models;
using Notes.API.Security.Resources.Show;
using Notes.API.Security.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Notes.API.Security.Interface.Rest;

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
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _userService.ListAllUsersAsync();
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserByUserId(long userId)
    {
        var result = await _userService.FindUserAsync(userId);
        if (!result.Success)
            return BadRequest(result.Resource);
        var mappedResult = _mapper.Map<User, UserResource>(result.Resource);
        return Ok(mappedResult);
    }
}