using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.Rd.EDA.BusinessLayer.BO.ExtraInfoBO;
using Recodme.Rd.EDA.BusinessLayer.BO.UserInfoBO;
using Recodme.Rd.EDA.Data.ExtraInfo;
using Recodme.Rd.EDA.DataAccess.Seeders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recodme.Rd.EDA.ApplicationTests.AsyncTests.ExtraInfoAsyncTests
{
    [TestClass]
    public class CommentAsyncTests
    {
        [TestMethod]
        public void TestCreateAndListCommentAsync()
        {
            ApplicationSeeder.Seed();

            var bo = new CommentBO();
            var foreignBO = new ClientBO();

            var comment = new Comment("Tou só a testar", foreignBO.ListUndeleted().Result.First().Id);

            var resCreate = bo.CreateAsync(comment).Result;
            var resGet = bo.ReadAsync(comment.Id).Result;

            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListCommentAsync()
        {
            ApplicationSeeder.Seed();

            var bo = new CommentBO();

            var resList = bo.ListAsync().Result;

            Assert.IsTrue(resList.Success && resList.Result.Count > 0);
        }

        [TestMethod]
        public void TestUpdateCommentAsync()
        {
            ApplicationSeeder.Seed();

            var bo = new CommentBO();

            var resList = bo.ListAsync().Result;

            var item = resList.Result.FirstOrDefault();
            item.Message = "Tou só a editar";

            var resUpdate = bo.UpdateAsync(item).Result;

            var list = bo.ListUndeletedAsync().Result;

            Assert.IsTrue(resList.Success && resUpdate.Success && list.Result.First().Message == "Tou só a editar");
        }

        [TestMethod]
        public void TestDeleteCommentAsync()
        {
            ApplicationSeeder.Seed();

            var bo = new CommentBO();

            var resList = bo.ListAsync().Result;
            var total = resList.Result.Count;

            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;

            var list = bo.ListUndeletedAsync().Result;

            Assert.IsTrue(resDelete.Success && resList.Success && list.Result.Count == (total - 1));
        }
    }
}