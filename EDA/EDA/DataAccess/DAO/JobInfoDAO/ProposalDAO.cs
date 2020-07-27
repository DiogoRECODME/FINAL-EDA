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
    public class ProposalDAO
    {
        /*
         * CRUD + LIST
         */

        #region CREATE

        public void Create(Proposal proposal)
        {
            using var ctx = new ApplicationContext();
            ctx.Proposals.Add(proposal);
            ctx.SaveChanges();
        }

        public async Task CreateAsync(Proposal proposal)
        {
            using var ctx = new ApplicationContext();
            await ctx.Proposals.AddAsync(proposal);
            await ctx.SaveChangesAsync();
        }

        #endregion

        #region READ

        public Proposal Read(Guid id)
        {
            using var ctx = new ApplicationContext();
            return ctx.Proposals.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Proposal> ReadAsync(Guid id)
        {
            using var ctx = new ApplicationContext();
            return await ctx.Proposals.FirstOrDefaultAsync(x => x.Id == id);
        }

        #endregion

        #region UPDATE

        public void Update(Proposal proposal)
        {
            using var ctx = new ApplicationContext();
            ctx.Entry(proposal).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public async Task UpdateAsync(Proposal proposal)
        {
            using var ctx = new ApplicationContext();
            ctx.Entry(proposal).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }

        #endregion

        #region DELETE

        public void Delete(Proposal proposal)
        {
            proposal.IsDeleted = true;
            Update(proposal);
        }

        public void Delete(Guid id)
        {
            var proposal = Read(id);

            if (proposal == null)
                return;

            Delete(proposal);
        }

        public async Task DeleteAsync(Proposal proposal)
        {
            proposal.IsDeleted = true;
            await UpdateAsync(proposal);
        }

        public async Task DeleteAsync(Guid id)
        {
            var proposal = await ReadAsync(id);

            if (proposal == null)
                return;

            await DeleteAsync(proposal);
        }

        #endregion

        #region LIST

        public List<Proposal> List()
        {
            using var ctx = new ApplicationContext();
            return ctx.Proposals.ToList();
        }

        public async Task<List<Proposal>> ListAsync()
        {
            using var ctx = new ApplicationContext();
            return await ctx.Proposals.ToListAsync();
        }

        #endregion
    }
}
