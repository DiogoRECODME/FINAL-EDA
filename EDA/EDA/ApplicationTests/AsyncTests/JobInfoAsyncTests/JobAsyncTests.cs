using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.Rd.EDA.BusinessLayer.BO.JobInfoBO;
using Recodme.Rd.EDA.BusinessLayer.BO.ServiceInfoBO;
using Recodme.Rd.EDA.BusinessLayer.BO.UserInfoBO;
using Recodme.Rd.EDA.Data.JobInfo;
using Recodme.Rd.EDA.DataAccess.Seeders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recodme.Rd.EDA.ApplicationTests.AsyncTests.JobInfoAsyncTests
{
    [TestClass]
    public class JobAsyncTests
    {
        [TestMethod]
        public void TestCreateAndListJobAsync()
        {
            ApplicationSeeder.Seed();

            var bo = new JobBO();
            var foreignBO = new ServiceBO();
            var foreignClientBO = new ClientBO();

            var job = new Job("Setúbal N4 1323", DateTime.Now.Date.AddDays(2), DateTime.Now.Date.AddDays(2).AddMinutes(180), 23.99, "Scheduled", 0, false, foreignBO.ListUndeleted().Result.First().Id, foreignClientBO.ListUndeleted().Result.First().Id);

            var resCreate = bo.CreateAsync(job).Result;
            var resGet = bo.ReadAsync(job.Id).Result;

            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListJobAsync()
        {
            ApplicationSeeder.Seed();

            var bo = new JobBO();

            var resList = bo.ListAsync().Result;

            Assert.IsTrue(resList.Success && resList.Result.Count > 0);
        }

        [TestMethod]
        public void TestUpdateJobAsync()
        {
            ApplicationSeeder.Seed();

            var bo = new JobBO();

            var resList = bo.ListAsync().Result;

            var item = resList.Result.FirstOrDefault();
            item.Status = "On going";

            var resUpdate = bo.UpdateAsync(item).Result;

            var list = bo.ListUndeletedAsync().Result;

            Assert.IsTrue(resList.Success && resUpdate.Success && list.Result.First().Status == "On going");
        }

        [TestMethod]
        public void TestDeleteJobAsync()
        {
            ApplicationSeeder.Seed();

            var bo = new JobBO();

            var resList = bo.ListAsync().Result;
            var total = resList.Result.Count;

            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;

            var list = bo.ListUndeletedAsync().Result;

            Assert.IsTrue(resDelete.Success && resList.Success && list.Result.Count == (total - 1));
        }
    }
}
