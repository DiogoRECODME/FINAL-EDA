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
    public class ServiceDAO
    {
        /*
         * CRUD + LIST
         */

        #region CREATE

        public void Create(Service service)
        {
            using var ctx = new ApplicationContext();
            ctx.Services.Add(service);
            ctx.SaveChanges();
        }

        public async Task CreateAsync(Service service)
        {
            using var ctx = new ApplicationContext();
            await ctx.Services.AddAsync(service);
            await ctx.SaveChangesAsync();
        }

        #endregion

        #region READ

        public Service Read(Guid id)
        {
            using var ctx = new ApplicationContext();
            return ctx.Services.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Service> ReadAsync(Guid id)
        {
            using var ctx = new ApplicationContext();
            return await ctx.Services.FirstOrDefaultAsync(x => x.Id == id);
        }

        #endregion

        #region UPDATE

        public void Update(Service service)
        {
            using var ctx = new ApplicationContext();
            ctx.Entry(service).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public async Task UpdateAsync(Service service)
        {
            using var ctx = new ApplicationContext();
            ctx.Entry(service).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }

        #endregion

        #region DELETE

        public void Delete(Service service)
        {
            service.IsDeleted = true;
            Update(service);
        }

        public void Delete(Guid id)
        {
            var service = Read(id);

            if (service == null)
                return;

            Delete(service);
        }

        public async Task DeleteAsync(Service service)
        {
            service.IsDeleted = true;
            await UpdateAsync(service);
        }

        public async Task DeleteAsync(Guid id)
        {
            var service = await ReadAsync(id);

            if (service == null)
                return;

            await DeleteAsync(service);
        }

        #endregion

        #region LIST

        public List<Service> List()
        {
            using var ctx = new ApplicationContext();
            return ctx.Services.ToList();
        }

        public async Task<List<Service>> ListAsync()
        {
            using var ctx = new ApplicationContext();
            return await ctx.Services.ToListAsync();
        }

        #endregion
    }
}
