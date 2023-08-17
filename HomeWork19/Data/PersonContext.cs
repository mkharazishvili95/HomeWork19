using HomeWork19.Domain;
using HomeWork19.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeWork19.Data
{
    public class PersonContext : DbContext
    {
        private DbContextOptionsBuilder<PersonContext> options;

        public PersonContext(DbContextOptions<PersonContext>options) : base(options) { }

        public PersonContext(DbContextOptionsBuilder<PersonContext> options)
        {
            this.options = options;
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(local);Database=HomeWork19SQL;Trusted_Connection=True;MultipleActiveResultSets=True");
        }
    }
}
