using Microsoft.EntityFrameworkCore;

namespace Core.PosTech8Nett.Api.Infra.Data
{
    /// <summary>  
    /// Represents the application's database context, providing access to the database and its entities.  
    /// </summary>  
    public class ApplicationContext : DbContext
    {
        /// <summary>  
        /// Initializes a new instance of the <see cref="ApplicationContext"/> class with the specified options.  
        /// </summary>  
        /// <param name="options">The options to configure the database context.</param>  
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }

        //public DbSet<Usuario> Usuarios { get; set; }  
    }
}
