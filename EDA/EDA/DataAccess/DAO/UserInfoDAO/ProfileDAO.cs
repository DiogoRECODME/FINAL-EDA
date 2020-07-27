using Microsoft.EntityFrameworkCore;
using Recodme.Rd.EDA.Data.UserInfo;
using Recodme.Rd.EDA.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodme.Rd.EDA.DataAccess.DAO.UserInfoDAO
{
    public class ProfileDAO
    {
        /*
         * CRUD + LIST
         */

        #region CREATE

        public void Create(Profile profile)
        {
            using var ctx = new ApplicationContext();
            ctx.Profiles.Add(profile);
            ctx.SaveChanges();
        }

        public async Task CreateAsync(Profile profile)
        {
            using var ctx = new ApplicationContext();
            await ctx.Profiles.AddAsync(profile);
            await ctx.SaveChangesAsync();
        }

        #endregion

        #region READ

        public Profile Read(Guid id)
        {
            using var ctx = new ApplicationContext();
            return ctx.Profiles.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Profile> ReadAsync(Guid id)
        {
            using var ctx = new ApplicationContext();
            return await ctx.Profiles.FirstOrDefaultAsync(x => x.Id == id);
        }

        #endregion

        #region UPDATE

        public void Update(Profile profile)
        {
            using var ctx = new ApplicationContext();
            ctx.Entry(profile).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public async Task UpdateAsync(Profile profile)
        {
            using var ctx = new ApplicationContext();
            ctx.Entry(profile).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }

        #endregion

        #region DELETE

        public void Delete(Profile profile)
        {
            profile.IsDeleted = true;
            Update(profile);
        }

        public void Delete(Guid id)
        {
            var profile = Read(id);

            if (profile == null)
                return;

            Delete(profile);
        }

        public async Task DeleteAsync(Profile profile)
        {
            profile.IsDeleted = true;
            await UpdateAsync(profile);
        }

        public async Task DeleteAsync(Guid id)
        {
            var profile = await ReadAsync(id);

            if (profile == null)
                return;

            await DeleteAsync(profile);
        }

        #endregion

        #region LIST

        public List<Profile> List()
        {
            using var ctx = new ApplicationContext();
            return ctx.Profiles.ToList();
        }

        public async Task<List<Profile>> ListAsync()
        {
            using var ctx = new ApplicationContext();
            return await ctx.Profiles.ToListAsync();
        }

        #endregion
    }
}