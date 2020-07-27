using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.Rd.EDA.BusinessLayer.BO.ServiceInfoBO;
using Recodme.Rd.EDA.Data.ServiceInfo;
using Recodme.Rd.EDA.DataAccess.Seeders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recodme.Rd.EDA.ApplicationTests.NormalTests.ServiceInfoTests
{
    [TestClass]
    public class ServiceProvidedTests
    {
        [TestMethod]
        public void TestCreateAndListServiceProvided()
        {
            ApplicationSeeder.Seed();

            var bo = new ServiceProvidedBO();
            var foreignBO = new ServiceBO();

            var serviceProvided = new ServiceProvided(foreignBO.ListUndeleted().Result.First().Id);

            var resCreate = bo.Create(serviceProvided);
            var resGet = bo.Read(serviceProvided.Id);

            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListServiceProvided()
        {
            ApplicationSeeder.Seed();

            var bo = new ServiceProvidedBO();

            var resList = bo.List();

            Assert.IsTrue(resList.Success && resList.Result.Count > 0);
        }

        [TestMethod]
        public void TestUpdateServiceProvided()
        {
            ApplicationSeeder.Seed();

            var bo = new ServiceProvidedBO();
            var foreignBO = new ServiceBO();

            var resList = bo.List();

            var item = resList.Result.FirstOrDefault();
            item.ServiceId = foreignBO.ListUndeleted().Result.Last().Id;

            var resUpdate = bo.Update(item);

            var list = bo.ListUndeleted();

            Assert.IsTrue(resList.Success && resUpdate.Success && list.Result.First().ServiceId == foreignBO.ListUndeleted().Result.Last().Id);
        }

        [TestMethod]
        public void TestDeleteServiceProvided()
        {
            ApplicationSeeder.Seed();

            var bo = new ServiceProvidedBO();

            var resList = bo.List();
            var total = resList.Result.Count;

            var resDelete = bo.Delete(resList.Result.First().Id);

            var list = bo.ListUndeleted();

            Assert.IsTrue(resDelete.Success && resList.Success && list.Result.Count == (total - 1));
        }
    }
}