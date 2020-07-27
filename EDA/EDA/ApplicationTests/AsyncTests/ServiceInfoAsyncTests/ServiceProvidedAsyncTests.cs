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
    public class ServiceProvidedAsyncTests
    {
        [TestMethod]
        public void TestCreateAndListServiceProvidedAsync()
        {
            ApplicationSeeder.Seed();

            var bo = new ServiceProvidedBO();
            var foreignBO = new ServiceBO();

            var serviceProvided = new ServiceProvided(foreignBO.ListUndeleted().Result.First().Id);

            var resCreate = bo.CreateAsync(serviceProvided).Result;
            var resGet = bo.ReadAsync(serviceProvided.Id).Result;

            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListServiceProvidedAsync()
        {
            ApplicationSeeder.Seed();

            var bo = new ServiceProvidedBO();

            var resList = bo.ListAsync().Result;

            Assert.IsTrue(resList.Success && resList.Result.Count > 0);
        }

        [TestMethod]
        public void TestUpdateServiceProvidedAsync()
        {
            ApplicationSeeder.Seed();

            var bo = new ServiceProvidedBO();
            var foreignBO = new ServiceBO();

            var resList = bo.ListAsync().Result;

            var item = resList.Result.FirstOrDefault();
            item.ServiceId = foreignBO.ListUndeleted().Result.Last().Id;

            var resUpdate = bo.UpdateAsync(item).Result;

            var list = bo.ListUndeletedAsync().Result;

            Assert.IsTrue(resList.Success && resUpdate.Success && list.Result.First().ServiceId == foreignBO.ListUndeleted().Result.Last().Id);
        }

        [TestMethod]
        public void TestDeleteServiceProvidedAsync()
        {
            ApplicationSeeder.Seed();

            var bo = new ServiceProvidedBO();

            var resList = bo.ListAsync().Result;
            var total = resList.Result.Count;

            var resDelete = bo.Delete(resList.Result.First().Id);

            var list = bo.ListUndeletedAsync().Result;

            Assert.IsTrue(resDelete.Success && resList.Success && list.Result.Count == (total - 1));
        }
    }
}