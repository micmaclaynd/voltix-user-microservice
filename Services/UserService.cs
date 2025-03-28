using Microsoft.EntityFrameworkCore;
using Voltix.UserMicroservice.Contexts;
using Voltix.UserMicroservice.Models;


namespace Voltix.UserMicroservice.Services;

public interface IUserService {
    public Task<UserModel?> GetUserAsync(int id);
    public Task<UserModel?> GetUserAsync(string email);

    public Task CreateUserAsync(UserModel userModel);

    public Task UpdateUserAsync(UserModel userModel);

    public Task RemoveUserAsync(UserModel userModel);
}

public class UserService(ApplicationContext context) : IUserService {
    private readonly ApplicationContext _context = context;

    public async Task<UserModel?> GetUserAsync(int id) {
        return await _context.Users.FirstOrDefaultAsync(userModel => userModel.Id == id);
    }

    public async Task<UserModel?> GetUserAsync(string email) {
        return await _context.Users.FirstOrDefaultAsync(userModel => userModel.Email == email);
    }

    public async Task CreateUserAsync(UserModel userModel) {
        await _context.Users.AddAsync(userModel);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(UserModel userModel) {
        _context.Users.Update(userModel);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveUserAsync(UserModel userModel) {
        _context.Users.Remove(userModel);
        await _context.SaveChangesAsync();
    }
}
