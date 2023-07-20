using AzureADB2CWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace AzureADB2CWeb.Data
{
    public class ApplicationDBContext :DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContext) : base(dbContext)
        {
        }

        public DbSet<UserDto> Users { get; set; }
    }
}

