using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recodme.Rd.EDA.Data.Base;
using Recodme.Rd.EDA.Data.ExtraInfo;
using Recodme.Rd.EDA.Data.JobInfo;
using Recodme.Rd.EDA.Data.ServiceInfo;
using Recodme.Rd.EDA.Data.UserInfo;
using Recodme.Rd.EDA.DataAccess.Properties;

namespace Recodme.Rd.EDA.DataAccess.Context
{
    public class ApplicationContext : IdentityDbContext
    {
        public ApplicationContext() : base() { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(Resources.ConnectionString);

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceProvided> ServicesProvided { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Profile> Profiles { get; set; }
    }
}
