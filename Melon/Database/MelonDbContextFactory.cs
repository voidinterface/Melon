using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace Melon.Database
{
    public class MelonDbContextFactory : IDesignTimeDbContextFactory<MelonDbContext>
    {
        private MelonDbContext _db;
        public MelonDbContextFactory() 
        {
            var collection = new ServiceCollection();

            collection.AddDbContext<MelonDbContext>(options =>
                options.UseSqlite("Data Source=melon.db"));

            collection.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(App).Assembly);
            });

            _db = collection.BuildServiceProvider().GetRequiredService<MelonDbContext>();
        }
        public MelonDbContext CreateDbContext(string[] args)
        {
            return _db;
        }
    }
}
