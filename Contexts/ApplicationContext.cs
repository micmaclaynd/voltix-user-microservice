using Microsoft.EntityFrameworkCore;
using Voltix.UserMicroservice.Models;


namespace Voltix.UserMicroservice.Contexts;

public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options) {
    public required DbSet<UserModel> Users { get; set; }
}
