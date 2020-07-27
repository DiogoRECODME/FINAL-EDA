using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.Rd.EDA.BusinessLayer.BO.ExtraInfoBO;
using Recodme.Rd.EDA.BusinessLayer.BO.UserInfoBO;
using Recodme.Rd.EDA.Data.ExtraInfo;
using Recodme.Rd.EDA.DataAccess.Seeders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recodme.Rd.EDA.ApplicationTests.NormalTests.ExtraInfoTests
{
    [TestClass]
    public class CommentTests
    {
        [TestMethod]
        public void TestCreateAndListComment()
        {
            ApplicationSeeder.Seed();

            var bo = new CommentBO();
            var foreignBO = new ClientBO();

            var comment = new Comment("Tou só a testar", foreignBO.ListUndeleted().Result.First().Id);

            var resCreate = bo.Create(comment);
            var resGet = bo.Read(comment.Id);

            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListComment()
        {
            ApplicationSeeder.Seed();

            var bo = new CommentBO();

            var resList = bo.List();

            Assert.IsTrue(resList.Success && resList.Result.Count > 0);
        }

        [TestMethod]
        public void TestUpdateComment()
        {
            ApplicationSeeder.Seed();

            var bo = new CommentBO();

            var resList = bo.List();

            var item = resList.Result.FirstOrDefault();
            item.Message = "Tou só a editar";

            var resUpdate = bo.Update(item);

            var list = bo.ListUndeleted();

            Assert.IsTrue(resList.Success && resUpdate.Success && list.Result.First().Message == "Tou só a editar");
        }

        [TestMethod]
        public void TestDeleteComment()
        {
            ApplicationSeeder.Seed();

            var bo = new CommentBO();

            var resList = bo.List();
            var total = resList.Result.Count;

            var resDelete = bo.Delete(resList.Result.First().Id);

            var list = bo.ListUndeleted();

            Assert.IsTrue(resDelete.Success && resList.Success && list.Result.Count == (total - 1));
        }
    }
}