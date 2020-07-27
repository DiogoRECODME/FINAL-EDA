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
    public class ProfileTests
    {
        [TestMethod]
        public void TestCreateAndListProfile()
        {
            ApplicationSeeder.Seed();

            var bo = new ProfileBO();
            var profile = new Profile("John Doe", "Portugal", DateTime.Now);

            var resCreate = bo.Create(profile);
            var resGet = bo.Read(profile.Id);

            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListProfile()
        {
            ApplicationSeeder.Seed();

            var bo = new ProfileBO();

            var resList = bo.List();

            Assert.IsTrue(resList.Success && resList.Result.Count > 0);
        }

        [TestMethod]
        public void TestUpdateProfile()
        {
            ApplicationSeeder.Seed();

            var bo = new ProfileBO();

            var resList = bo.List();

            var item = resList.Result.FirstOrDefault();
            item.Country = "Spain";

            var resUpdate = bo.Update(item);

            var list = bo.ListUndeleted();

            Assert.IsTrue(resList.Success && resUpdate.Success && list.Result.First().Country == "Spain");
        }

        [TestMethod]
        public void TestDeleteProfile()
        {
            ApplicationSeeder.Seed();

            var bo = new ProfileBO();

            var resList = bo.List();
            var total = resList.Result.Count;

            var resDelete = bo.Delete(resList.Result.First().Id);

            var list = bo.ListUndeleted();

            Assert.IsTrue(resDelete.Success && resList.Success && list.Result.Count == (total - 1));
        }
    }
}