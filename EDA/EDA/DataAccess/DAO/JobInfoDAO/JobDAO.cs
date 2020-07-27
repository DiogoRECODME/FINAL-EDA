using Microsoft.EntityFrameworkCore;
using Recodme.Rd.EDA.Data.JobInfo;
using Recodme.Rd.EDA.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodme.Rd.EDA.DataAccess.DAO.JobInfoDAO
{
    public class JobDAO
    {
        /*
         * CRUD + LIST
         */

        #region CREATE

        public void Create(Job job)
        {
            using var ctx = new ApplicationContext();
            ctx.Jobs.Add(job);
            ctx.SaveChanges();
        }

        public async Task CreateAsync(Job job)
        {
            using var ctx = new ApplicationContext();
            await ctx.Jobs.AddAsync(job);
            await ctx.SaveChangesAsync();
        }

        #endregion

        #region READ

        public Job Read(Guid id)
        {
            using var ctx = new ApplicationContext();
            return ctx.Jobs.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Job> ReadAsync(Guid id)
        {
            using var ctx = new ApplicationContext();
            return await ctx.Jobs.FirstOrDefaultAsync(x => x.Id == id);
        }

        #endregion

        #region UPDATE

        public void Update(Job job)
        {
            using var ctx = new ApplicationContext();
            ctx.Entry(job).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public async Task UpdateAsync(Job job)
        {
            using var ctx = new ApplicationContext();
            ctx.Entry(job).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }

        #endregion

        #region DELETE

        public void Delete(Job job)
        {
            job.IsDeleted = true;
            Update(job);
        }

        public void Delete(Guid id)
        {
            var job = Read(id);

            if (job == null)
                return;

            Delete(job);
        }

        public async Task DeleteAsync(Job job)
        {
            job.IsDeleted = true;
            await UpdateAsync(job);
        }

        public async Task DeleteAsync(Guid id)
        {
            var job = await ReadAsync(id);

            if (job == null)
                return;

            await DeleteAsync(job);
        }

        #endregion

        #region LIST

        public List<Job> List()
        {
            using var ctx = new ApplicationContext();
            return ctx.Jobs.ToList();
        }

        public async Task<List<Job>> ListAsync()
        {
            using var ctx = new ApplicationContext();
            return await ctx.Jobs.ToListAsync();
        }

        #endregion
    }
}
