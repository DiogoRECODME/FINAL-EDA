using Microsoft.EntityFrameworkCore;
using Recodme.Rd.EDA.Data.ServiceInfo;
using Recodme.Rd.EDA.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodme.Rd.EDA.DataAccess.DAO.ServiceInfoDAO
{
    public class ServiceProvidedDAO
    {
        /*
         * CRUD + LIST
         */

        #region CREATE

        public void Create(ServiceProvided serviceProvided)
        {
            using var ctx = new ApplicationContext();
            ctx.ServicesProvided.Add(serviceProvided);
            ctx.SaveChanges();
        }

        public async Task CreateAsync(ServiceProvided serviceProvided)
        {
            using var ctx = new ApplicationContext();
            await ctx.ServicesProvided.AddAsync(serviceProvided);
            await ctx.SaveChangesAsync();
        }

        #endregion

        #region READ

        public ServiceProvided Read(Guid id)
        {
            using var ctx = new ApplicationContext();
            return ctx.ServicesProvided.FirstOrDefault(x => x.Id == id);
        }

        public async Task<ServiceProvided> ReadAsync(Guid id)
        {
            using var ctx = new ApplicationContext();
            return await ctx.ServicesProvided.FirstOrDefaultAsync(x => x.Id == id);
        }

        #endregion

        #region UPDATE

        public void Update(ServiceProvided serviceProvided)
        {
            using var ctx = new ApplicationContext();
            ctx.Entry(serviceProvided).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public async Task UpdateAsync(ServiceProvided serviceProvided)
        {
            using var ctx = new ApplicationContext();
            ctx.Entry(serviceProvided).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }

        #endregion

        #region DELETE

        public void Delete(ServiceProvided serviceProvided)
        {
            serviceProvided.IsDeleted = true;
            Update(serviceProvided);
        }

        public void Delete(Guid id)
        {
            var serviceProvided = Read(id);

            if (serviceProvided == null)
                return;

            Delete(serviceProvided);
        }

        public async Task DeleteAsync(ServiceProvided serviceProvided)
        {
            serviceProvided.IsDeleted = true;
            await UpdateAsync(serviceProvided);
        }

        public async Task DeleteAsync(Guid id)
        {
            var serviceProvided = await ReadAsync(id);

            if (serviceProvided == null)
                return;

            await DeleteAsync(serviceProvided);
        }

        #endregion

        #region LIST

        public List<ServiceProvided> List()
        {
            using var ctx = new ApplicationContext();
            return ctx.ServicesProvided.ToList();
        }

        public async Task<List<ServiceProvided>> ListAsync()
        {
            using var ctx = new ApplicationContext();
            return await ctx.ServicesProvided.ToListAsync();
        }

        #endregion
    }
}
