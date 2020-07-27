using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.Rd.EDA.BusinessLayer.BO.JobInfoBO;
using Recodme.Rd.EDA.Data.JobInfo;
using Recodme.Rd.EDA.DataAccess.Seeders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recodme.Rd.EDA.ApplicationTests.AsyncTests.JobInfoAsyncTests
{
    [TestClass]
    public class ProposalAsyncTests
    {
        [TestMethod]
        public void TestCreateAndListProposalAsync()
        {
            ApplicationSeeder.Seed();

            var bo = new ProposalBO();
            var foreignBO = new JobBO();

            var proposal = new Proposal("User Teste", "Mensagem de teste", foreignBO.ListUndeleted().Result.First().Id);

            var resCreate = bo.CreateAsync(proposal).Result;
            var resGet = bo.ReadAsync(proposal.Id).Result;

            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListProposalAsync()
        {
            ApplicationSeeder.Seed();

            var bo = new ProposalBO();

            var resList = bo.ListAsync().Result;

            Assert.IsTrue(resList.Success && resList.Result.Count > 0);
        }

        [TestMethod]
        public void TestUpdateProposalAsync()
        {
            ApplicationSeeder.Seed();

            var bo = new ProposalBO();

            var resList = bo.ListAsync().Result;

            var item = resList.Result.FirstOrDefault();
            item.Message = "Oi";

            var resUpdate = bo.UpdateAsync(item).Result;

            var list = bo.ListUndeletedAsync().Result;

            Assert.IsTrue(resList.Success && resUpdate.Success && list.Result.First().Message == "Oi");
        }

        [TestMethod]
        public void TestDeleteProposalAsync()
        {
            ApplicationSeeder.Seed();

            var bo = new ProposalBO();

            var resList = bo.ListAsync().Result;
            var total = resList.Result.Count;

            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;

            var list = bo.ListUndeletedAsync().Result;

            Assert.IsTrue(resDelete.Success && resList.Success && list.Result.Count == (total - 1));
        }
    }
}
