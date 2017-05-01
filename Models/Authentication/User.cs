using Microsoft.EntityFrameworkCore;

namespace Core_Server.Models.Authentication
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        { }

        public UserContext() {}

        public virtual DbSet<User> Users { get; set; }
    }

    public class User 
    {
        public int Id { get; protected set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
    }
}