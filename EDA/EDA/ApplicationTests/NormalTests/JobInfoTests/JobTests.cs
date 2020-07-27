using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.Rd.EDA.BusinessLayer.BO.JobInfoBO;
using Recodme.Rd.EDA.BusinessLayer.BO.ServiceInfoBO;
using Recodme.Rd.EDA.BusinessLayer.BO.UserInfoBO;
using Recodme.Rd.EDA.Data.JobInfo;
using Recodme.Rd.EDA.DataAccess.Seeders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Recodme.Rd.EDA.ApplicationTests.NormalTests.JobInfoTests
{
    [TestClass]
    public class JobTests
    {
        [TestMethod]
        public void TestCreateAndListJob()
        {
            ApplicationSeeder.Seed();

            var bo = new JobBO();
            var foreignBO = new ServiceBO();
            var foreignClientBO = new ClientBO();

            var job = new Job("Setúbal N4 1323", DateTime.Now.Date.AddDays(2), DateTime.Now.Date.AddDays(2).AddMinutes(180), 23.99, "Scheduled", 0, false, foreignBO.ListUndeleted().Result.First().Id, foreignClientBO.ListUndeleted().Result.First().Id);

            var resCreate = bo.Create(job);
            var resGet = bo.Read(job.Id);

            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListJob()
        {
            ApplicationSeeder.Seed();

            var bo = new JobBO();

            var resList = bo.List();

            Assert.IsTrue(resList.Success && resList.Result.Count > 0);
        }

        [TestMethod]
        public void TestUpdateJob()
        {
            ApplicationSeeder.Seed();

            var bo = new JobBO();

            var resList = bo.List();

            var item = resList.Result.FirstOrDefault();
            item.Status = "On going";

            var resUpdate = bo.Update(item);

            var list = bo.ListUndeleted();

            Assert.IsTrue(resList.Success && resUpdate.Success && list.Result.First().Status == "On going");
        }

        [TestMethod]
        public void TestDeleteJob()
        {
            ApplicationSeeder.Seed();

            var bo = new JobBO();

            var resList = bo.List();
            var total = resList.Result.Count;

            var resDelete = bo.Delete(resList.Result.First().Id);

            var list = bo.ListUndeleted();

            Assert.IsTrue(resDelete.Success && resList.Success && list.Result.Count == (total-1));
        }
    }
}
