using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.Rd.EDA.BusinessLayer.BO.UserInfoBO;
using Recodme.Rd.EDA.Data.UserInfo;
using Recodme.Rd.EDA.DataAccess.Seeders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recodme.Rd.EDA.ApplicationTests.AsyncTests.UserInfoAsyncTests
{
    [TestClass]
    public class ClientAsyncTests
    {
        [TestMethod]
        public void TestCreateAndListClientAsync()
        {
            ApplicationSeeder.Seed();

            var bo = new ClientBO();
            var foreignBO = new ProfileBO();

            var client = new Client(foreignBO.ListUndeleted().Result.First().Id);

            var resCreate = bo.CreateAsync(client).Result;
            var resGet = bo.ReadAsync(client.Id).Result;

            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestLisClientAsync()
        {
            ApplicationSeeder.Seed();

            var bo = new ClientBO();

            var resList = bo.ListAsync().Result;

            Assert.IsTrue(resList.Success && resList.Result.Count > 0);
        }

        [TestMethod]
        public void TestUpdateClientAsync()
        {
            ApplicationSeeder.Seed();

            var bo = new ClientBO();
            var foreignBO = new ProfileBO();

            var resList = bo.ListAsync().Result;

            var item = resList.Result.FirstOrDefault();
            item.ProfileId = foreignBO.ListUndeleted().Result.Last().Id;

            var resUpdate = bo.UpdateAsync(item).Result;

            var list = bo.ListUndeletedAsync().Result;

            Assert.IsTrue(resList.Success && resUpdate.Success && list.Result.First().ProfileId == foreignBO.ListUndeleted().Result.Last().Id);
        }

        [TestMethod]
        public void TestDeleteClientAsync()
        {
            ApplicationSeeder.Seed();

            var bo = new ClientBO();

            var resList = bo.ListAsync().Result;
            var total = resList.Result.Count;

            var resDelete = bo.Delete(resList.Result.First().Id);

            var list = bo.ListUndeletedAsync().Result;

            Assert.IsTrue(resDelete.Success && resList.Success && list.Result.Count == (total - 1));
        }
    }
}