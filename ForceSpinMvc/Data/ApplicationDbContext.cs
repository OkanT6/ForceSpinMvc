using ForceSpinMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace ForceSpinMvc.Data
{
    public class ApplicationDbContext:DbContext
    {
        // Constructor'da dışarıdan DbContextOptions alıyoruz
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
    }
}
