using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUpload.Domain.Infra.Data
{
    //This class was required for database configuration
    public class DataBaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>  
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DatabaseContext>();
            builder.UseMySql("Server=localhost;;Database=BlazorPropertiesFiles;User=root;Password=23112019@arrasca.;", new MySqlServerVersion(new Version(8, 4, 0)));

            return new DatabaseContext(builder.Options);
        }
    }
}
