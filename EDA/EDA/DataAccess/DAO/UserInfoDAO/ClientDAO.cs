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
    public class ClientDAO
    {
        /*
         * CRUD + LIST
         */

        #region CREATE

        public void Create(Client client)
        {
            using var ctx = new ApplicationContext();
            ctx.Clients.Add(client);
            ctx.SaveChanges();
        }

        public async Task CreateAsync(Client client)
        {
            using var ctx = new ApplicationContext();
            await ctx.Clients.AddAsync(client);
            await ctx.SaveChangesAsync();
        }

        #endregion

        #region READ

        public Client Read(Guid id)
        {
            using var ctx = new ApplicationContext();
            return ctx.Clients.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Client> ReadAsync(Guid id)
        {
            using var ctx = new ApplicationContext();
            return await ctx.Clients.FirstOrDefaultAsync(x => x.Id == id);
        }

        #endregion

        #region UPDATE

        public void Update(Client client)
        {
            using var ctx = new ApplicationContext();
            ctx.Entry(client).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public async Task UpdateAsync(Client client)
        {
            using var ctx = new ApplicationContext();
            ctx.Entry(client).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }

        #endregion

        #region DELETE

        public void Delete(Client client)
        {
            client.IsDeleted = true;
            Update(client);
        }

        public void Delete(Guid id)
        {
            var client = Read(id);

            if (client == null)
                return;

            Delete(client);
        }

        public async Task DeleteAsync(Client client)
        {
            client.IsDeleted = true;
            await UpdateAsync(client);
        }

        public async Task DeleteAsync(Guid id)
        {
            var client = await ReadAsync(id);

            if (client == null)
                return;

            await DeleteAsync(client);
        }

        #endregion

        #region LIST

        public List<Client> List()
        {
            using var ctx = new ApplicationContext();
            return ctx.Clients.ToList();
        }

        public async Task<List<Client>> ListAsync()
        {
            using var ctx = new ApplicationContext();
            return await ctx.Clients.ToListAsync();
        }

        #endregion
    }
}