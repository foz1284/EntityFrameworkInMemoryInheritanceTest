using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EntityFrameworkInMemoryInheritanceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var databaseRoot = new InMemoryDatabaseRoot();
            var options = new DbContextOptionsBuilder()
                                .UseInMemoryDatabase(databaseName: "DBName", databaseRoot: databaseRoot)
                                .Options;

            using (var context = new BaseContext(options))
            {
                context.Users.Add(new User { Name="Joe"});
                context.SaveChanges();
            }

            using (var context = new InheritedContext(options))
            {
                context.Users.Add(new User { Name = "Fred" });
                context.SaveChanges();
            }
        }
    }

    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions options) : base(options)
        {
                   
        }
        public DbSet<User> Users { get; set; }
    }
    public class InheritedContext : BaseContext
    {
        public InheritedContext(DbContextOptions options) : base(options)
        {
             
        }
        public DbSet<UserActions> UserActions { get; set; }
    }

    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class UserActions
    {
        public int ID { get; set; }
    }
}


