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
    public class ProfileAsyncTests
    {
        [TestMethod]
        public void TestCreateAndListProfileAsync()
        {
            ApplicationSeeder.Seed();

            var bo = new ProfileBO();
            var profile = new Profile("John Doe", "Portugal", DateTime.Now);

            var resCreate = bo.CreateAsync(profile).Result;
            var resGet = bo.ReadAsync(profile.Id).Result;

            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListProfileAsync()
        {
            ApplicationSeeder.Seed();

            var bo = new ProfileBO();

            var resList = bo.ListAsync().Result;

            Assert.IsTrue(resList.Success && resList.Result.Count > 0);
        }

        [TestMethod]
        public void TestUpdateProfileAsync()
        {
            ApplicationSeeder.Seed();

            var bo = new ProfileBO();

            var resList = bo.ListAsync().Result;

            var item = resList.Result.FirstOrDefault();
            item.Country = "Spain";

            var resUpdate = bo.UpdateAsync(item).Result;

            var list = bo.ListUndeletedAsync().Result;

            Assert.IsTrue(resList.Success && resUpdate.Success && list.Result.First().Country == "Spain");
        }

        [TestMethod]
        public void TestDeleteProfileAsync()
        {
            ApplicationSeeder.Seed();

            var bo = new ProfileBO();

            var resList = bo.ListAsync().Result;
            var total = resList.Result.Count;

            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;

            var list = bo.ListUndeletedAsync().Result;

            Assert.IsTrue(resDelete.Success && resList.Success && list.Result.Count == (total - 1));
        }
    }
}