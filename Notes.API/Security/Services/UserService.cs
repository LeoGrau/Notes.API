using AutoMapper;
using Notes.API.Security.Authorization.Middleware.Handlers.Interfaces;
using Notes.API.Security.Domain.Repositories;
using Notes.API.Security.Domain.Services;
using Notes.API.Security.Domain.Services.Communication;
using Notes.API.Security.Domain.Services.Communication.Responses;
using Notes.API.Security.Exceptions;
using Notes.API.Security.Models;
using Notes.API.Shared.Domain.Repositories;

namespace Notes.API.Security.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IJwtHandler _jwtHandler;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper, IJwtHandler jwtHandler)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _jwtHandler = jwtHandler;
    }

    public async Task<IEnumerable<User>> ListAllUsersAsync()
    {
        return await _userRepository.ListAllAsync();
    }

    public async Task<User?> GetByIdAsync(long userId)
    {
        var existingUser = await _userRepository.FindAsync(userId);
        if (existingUser == null) throw new KeyNotFoundException("User not found");
        return existingUser;
    }

    public async Task<AuthResponse> AuthenticateAsync(AuthRequest authRequest)
    {
        var user = await _userRepository.FindByEmailAsync(authRequest.Email!);
        if (user == null)
        {
            Console.WriteLine("User is null");
        }
        Console.WriteLine($"Password Hashed: {user.Password}");
        if (user == null || !BCrypt.Net.BCrypt.Verify(authRequest.Password, user.Password))
        {
            Console.WriteLine("Authentication Error");
            throw new AppException("Username or password is incorrect");
        }
        Console.WriteLine($"Request {authRequest.Email}, {authRequest.Password}");
        Console.WriteLine($"Request {user.UserId}, {user.Firstname}");
        Console.WriteLine("Authentication successful. About to generate token");
        // Authentication successful
        var response = _mapper.Map<AuthResponse>(user);
        Console.WriteLine($"Response: {response.UserId}, {response.FirstName}, {response.LastName}, {response.Email}");
        response.Token = _jwtHandler.GenerateToken(user);
        Console.WriteLine($"Generated token is {response.Token}");
        return response;
    }

    public async Task RegisterAsync(RegisterRequest registerRequest)
    {
        //Validate if email was already taken.
        if (_userRepository.ExistByEmail(registerRequest.Email))
            throw new AppException($"Email {registerRequest.Email} was already taken.");
        
        //Map request to User Object
        var user = _mapper.Map<User>(registerRequest);
        
        //Hash password
        user.Password = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);
        
        //Save User
        try
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception exception)
        {
            throw new AppException($"An Error occurred while saving the user: {exception.Message}");
        }
    }

    public Task UpdateAsync(long userId, UpdateRequest updateRequest)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(long userId)
    {
        throw new NotImplementedException();
    }
}