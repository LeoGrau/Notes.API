using Notes.API.Security.Domain.Repositories;
using Notes.API.Security.Domain.Services;
using Notes.API.Security.Exceptions;
using Notes.API.Security.Models;
using Notes.API.Security.Repositories;
using Notes.API.Security.Services.Communication;
using Notes.API.Shared.Domain.Repositories;

namespace Notes.API.Security.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<User?>> ListAllUsersAsync()
    {
        return await _userRepository.ListAllAsync();
    }

    public async Task<UserResponse> FindUserAsync(long userId)
    {
        var existingUser = await _userRepository.FindAsync(userId);
        if (existingUser == null)
            return new UserResponse("User does not exist.");
        return new UserResponse(existingUser);
    }

    public async Task<UserResponse> AddAsync(User newUser)
    {
        try
        {
            await _userRepository.AddAsync(newUser);
            await _unitOfWork.CompleteAsync();
            return new UserResponse(newUser);
        }
        catch (Exception exception)
        {
            return new UserResponse($"{exception.Message}");
        }
    }

    public async Task<UserResponse> Update(long userId, User updatedUser)
    {
        var existingUser = await _userRepository.FindAsync(userId);
        if (existingUser == null)
            return new UserResponse("User does not exist");
        
        existingUser.SetUser(updatedUser);
        
        try
        {
            _userRepository.Update(existingUser);
            await _unitOfWork.CompleteAsync();
            return new UserResponse(existingUser);
        }
        catch (Exception exception)
        {
            return new UserResponse($"{exception.Message}");
        }
    }

    public async Task<UserResponse> Remove(long userId)
    {
        var existingUser = await _userRepository.FindAsync(userId);
        if (existingUser == null)
            return new UserResponse("User does not exist");

        try
        {
            _userRepository.Remove(existingUser);
            await _unitOfWork.CompleteAsync();
            return new UserResponse(existingUser);
        }
        catch (Exception exception)
        {
            return new UserResponse($"{exception.Message}");
        }
    }
}