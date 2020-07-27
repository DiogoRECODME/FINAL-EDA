using Microsoft.EntityFrameworkCore;
using Recodme.Rd.EDA.Data.ExtraInfo;
using Recodme.Rd.EDA.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodme.Rd.EDA.DataAccess.DAO.ExtraInfoDAO
{
    public class CommentDAO
    {
        /*
         * CRUD + LIST
         */

        #region CREATE

        public void Create(Comment comment)
        {
            using var ctx = new ApplicationContext();
            ctx.Comments.Add(comment);
            ctx.SaveChanges();
        }

        public async Task CreateAsync(Comment comment)
        {
            using var ctx = new ApplicationContext();
            await ctx.Comments.AddAsync(comment);
            await ctx.SaveChangesAsync();
        }

        #endregion

        #region READ

        public Comment Read(Guid id)
        {
            using var ctx = new ApplicationContext();
            return ctx.Comments.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Comment> ReadAsync(Guid id)
        {
            using var ctx = new ApplicationContext();
            return await ctx.Comments.FirstOrDefaultAsync(x => x.Id == id);
        }

        #endregion

        #region UPDATE

        public void Update(Comment comment)
        {
            using var ctx = new ApplicationContext();
            ctx.Entry(comment).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public async Task UpdateAsync(Comment comment)
        {
            using var ctx = new ApplicationContext();
            ctx.Entry(comment).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }

        #endregion

        #region DELETE

        public void Delete(Comment comment)
        {
            comment.IsDeleted = true;
            Update(comment);
        }

        public void Delete(Guid id)
        {
            var comment = Read(id);

            if (comment == null)
                return;

            Delete(comment);
        }

        public async Task DeleteAsync(Comment comment)
        {
            comment.IsDeleted = true;
            await UpdateAsync(comment);
        }

        public async Task DeleteAsync(Guid id)
        {
            var comment = await ReadAsync(id);

            if (comment == null)
                return;

            await DeleteAsync(comment);
        }

        #endregion

        #region LIST

        public List<Comment> List()
        {
            using var ctx = new ApplicationContext();
            return ctx.Comments.ToList();
        }

        public async Task<List<Comment>> ListAsync()
        {
            using var ctx = new ApplicationContext();
            return await ctx.Comments.ToListAsync();
        }

        #endregion
    }
}
