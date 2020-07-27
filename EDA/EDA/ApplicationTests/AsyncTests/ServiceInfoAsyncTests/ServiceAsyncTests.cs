using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.Rd.EDA.BusinessLayer.BO.ServiceInfoBO;
using Recodme.Rd.EDA.Data.ServiceInfo;
using Recodme.Rd.EDA.DataAccess.Seeders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recodme.Rd.EDA.ApplicationTests.AsyncTests.ServiceInfoAsyncTests
{
    [TestClass]
    public class ServiceAsyncTests
    {
        [TestMethod]
        public void TestCreateAndListServiceAsync()
        {
            ApplicationSeeder.Seed();

            var bo = new ServiceBO();
            var service = new Service("Moving property", "Transporting property/belongings", true);

            var resCreate = bo.CreateAsync(service).Result;
            var resGet = bo.ReadAsync(service.Id).Result;

            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListServiceAsync()
        {
            ApplicationSeeder.Seed();

            var bo = new ServiceBO();

            var resList = bo.ListAsync().Result;

            Assert.IsTrue(resList.Success && resList.Result.Count > 0);
        }

        [TestMethod]
        public void TestUpdateServiceAsync()
        {
            ApplicationSeeder.Seed();

            var bo = new ServiceBO();

            var resList = bo.ListAsync().Result;

            var item = resList.Result.FirstOrDefault();
            item.IsActive = false;

            var resUpdate = bo.UpdateAsync(item).Result;

            var list = bo.ListUndeletedAsync().Result;

            Assert.IsTrue(resList.Success && resUpdate.Success && list.Result.First().IsActive == false);
        }

        [TestMethod]
        public void TestDeleteServiceAsync()
        {
            ApplicationSeeder.Seed();

            var bo = new ServiceBO();

            var resList = bo.ListAsync().Result;
            var total = resList.Result.Count;

            var resDelete = bo.Delete(resList.Result.First().Id);

            var list = bo.ListUndeletedAsync().Result;

            Assert.IsTrue(resDelete.Success && resList.Success && list.Result.Count == (total - 1));
        }
    }
}