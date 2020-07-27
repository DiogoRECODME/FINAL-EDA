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
    public class ServiceTests
    {
        [TestMethod]
        public void TestCreateAndListService()
        {
            ApplicationSeeder.Seed();

            var bo = new ServiceBO();
            var service = new Service("Moving property", "Transporting property/belongings", true);

            var resCreate = bo.Create(service);
            var resGet = bo.Read(service.Id);

            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListService()
        {
            ApplicationSeeder.Seed();

            var bo = new ServiceBO();

            var resList = bo.List();

            Assert.IsTrue(resList.Success && resList.Result.Count > 0);
        }

        [TestMethod]
        public void TestUpdateService()
        {
            ApplicationSeeder.Seed();

            var bo = new ServiceBO();

            var resList = bo.List();

            var item = resList.Result.FirstOrDefault();
            item.IsActive = false;

            var resUpdate = bo.Update(item);

            var list = bo.ListUndeleted();

            Assert.IsTrue(resList.Success && resUpdate.Success && list.Result.First().IsActive == false);
        }

        [TestMethod]
        public void TestDeleteService()
        {
            ApplicationSeeder.Seed();

            var bo = new ServiceBO();

            var resList = bo.List();
            var total = resList.Result.Count;

            var resDelete = bo.Delete(resList.Result.First().Id);

            var list = bo.ListUndeleted();

            Assert.IsTrue(resDelete.Success && resList.Success && list.Result.Count == (total - 1));
        }
    }
}