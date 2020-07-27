using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.Rd.EDA.BusinessLayer.BO.UserInfoBO;
using Recodme.Rd.EDA.Data.UserInfo;
using Recodme.Rd.EDA.DataAccess.Seeders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recodme.Rd.EDA.ApplicationTests.NormalTests.UserInfoTests
{
    [TestClass]
    public class ClientTests
    {
        [TestMethod]
        public void TestCreateAndListClient()
        {
            ApplicationSeeder.Seed();

            var bo = new ClientBO();
            var foreignBO = new ProfileBO();

            var client = new Client(foreignBO.ListUndeleted().Result.First().Id);

            var resCreate = bo.Create(client);
            var resGet = bo.Read(client.Id);

            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListClient()
        {
            ApplicationSeeder.Seed();

            var bo = new ClientBO();

            var resList = bo.List();

            Assert.IsTrue(resList.Success && resList.Result.Count > 0);
        }

        [TestMethod]
        public void TestUpdateClient()
        {
            ApplicationSeeder.Seed();

            var bo = new ClientBO();
            var foreignBO = new ProfileBO();

            var resList = bo.List();

            var item = resList.Result.FirstOrDefault();
            item.ProfileId = foreignBO.ListUndeleted().Result.Last().Id;

            var resUpdate = bo.Update(item);

            var list = bo.ListUndeleted();

            Assert.IsTrue(resList.Success && resUpdate.Success && list.Result.First().ProfileId == foreignBO.ListUndeleted().Result.Last().Id);
        }

        [TestMethod]
        public void TestDeleteClient()
        {
            ApplicationSeeder.Seed();

            var bo = new ClientBO();

            var resList = bo.List();
            var total = resList.Result.Count;

            var resDelete = bo.Delete(resList.Result.First().Id);

            var list = bo.ListUndeleted();

            Assert.IsTrue(resDelete.Success && resList.Success && list.Result.Count == (total - 1));
        }
    }
}