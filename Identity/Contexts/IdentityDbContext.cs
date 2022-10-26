using Core.Common;
using Core.Interfaces;
using Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Contexts
{
    public class IdentityDbContext : IdentityDbContext<ApplicationUser>
    {
    
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options, IDateTimeService dateTimeService) : base(options)
        {
            
        }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
